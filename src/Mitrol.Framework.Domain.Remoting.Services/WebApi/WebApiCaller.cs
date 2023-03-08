namespace Mitrol.Framework.Domain.Remoting.Services.WebApi
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Extensions;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.Remoting.Services.Configuration;
    using Mitrol.Framework.Domain.Remoting.Services.Extensions;
    using Newtonsoft.Json;
    using RestSharp;
    using RestSharp.Serializers.NewtonsoftJson;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    /// <summary>
    /// WebApiResponse returned from all Web Api Manager request
    /// </summary>
    public class WebApiResponse : Disposable
    {
        public HttpStatusCode StatusCode { get; private set; }
        public List<ErrorDetail> Errors { get; private set; }
        public string JsonResult { get; private set; }

        internal WebApiResponse(HttpStatusCode code, string json, List<ErrorDetail> errors = null)
        {
            StatusCode = code;
            Errors = errors;
            JsonResult = json;
        }
    }

    public class WebApiRequest : Disposable
    {
        public WebApiRequest() { }
        public WebApiRequest(IUserSession userSession)
        {
            AccessToken = userSession?.AccessToken;
            RefreshToken = userSession?.RefreshToken;
            SessionId = userSession?.SessionId;
            IsOldStyle = true;
        }

        public string Uri { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string SessionId { get; set; }
        public bool IsOData => Uri?.ToLower().Contains("odata") ?? false;
        public bool IsOldStyle { get; set; }
    }

    public class WebApiRequest<T> : WebApiRequest
    {
        public WebApiRequest() { }
        public WebApiRequest(IUserSession userSession) : base(userSession) { }

        public T Model { get; set; }

        public void SetUserSession(IUserSession userSession)
        {
            AccessToken = userSession?.AccessToken;
            RefreshToken = userSession?.RefreshToken;
            SessionId = userSession?.SessionId;
        }
    }

    public class AccessTokenRefreshedEventArgs : EventArgs
    {
        public string NewAccessToken { get; internal set; }
        public string NewRefreshToken { get; internal set; }

        public AccessTokenRefreshedEventArgs(string accessToken, string refreshToken)
        {
            NewAccessToken = accessToken;
            NewRefreshToken = refreshToken;
        }
    }

    /// <summary>
    /// Class for handling Web Api Requests
    /// </summary>
    public class WebApiCaller
    {
        private readonly string _baseUri;
        private readonly string _apiPrefix;

        public WebApiCaller(StartApplicationSetupSection config)
            : this(config.BaseUri, config.ApiVersion)
        {

        }

        public WebApiCaller(string baseUri, string apiVersion)
        {
            _baseUri = baseUri;
            _apiPrefix = $"/api/{apiVersion}/";
        }

        public event EventHandler<AccessTokenRefreshedEventArgs> AccessTokenRefreshed;

        protected virtual void OnAccessTokenRefreshed(AccessTokenRefreshedEventArgs e)
        {
            AccessTokenRefreshed?.Invoke(this, e);
        }

        /// <summary>
        /// Create WebApi Reponse from httpResponse
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <param name="isODataRequest"></param>
        /// <returns></returns>
        private WebApiResponse CreateWebApiResponse(RestResponse httpResponse,
                                                    WebApiRequest webApiRequest)
        {
            WebApiResponse webApiResponse = null;
            if (httpResponse.IsSuccessful)
            {
                if (webApiRequest.IsOData)
                {
                    webApiResponse = new WebApiResponse(HttpStatusCode.OK, httpResponse.Content.Normalize());
                }
                else
                {
                    //Gestione nuovo modello di response
                    //TODO: Da eliminare quando tutte le webapi non avranno più il ResponseModel
                    if (webApiRequest.IsOldStyle)
                    {
                        var responseDetails = JsonConvert.DeserializeObject<ResponseModel<object>>(httpResponse.Content);
                        if (responseDetails.ResponseType == ResponseTypeEnum.Ok)
                        {
                            webApiResponse = new WebApiResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(responseDetails.Result));
                            CheckIsRefreshed(webApiResponse, httpResponse);
                        }
                        else
                        {
                            webApiResponse = new WebApiResponse(HttpStatusCode.BadRequest, string.Empty,
                                                responseDetails.ErrorDetails);
                        }
                    }
                    else
                    {
                        webApiResponse = new WebApiResponse(HttpStatusCode.OK, httpResponse.Content.Normalize());
                        CheckIsRefreshed(webApiResponse, httpResponse);
                    }
                }
            }
            else
            {
                // Due to network timeout (server is busy, slow internet connection, proxy, or firewall) httpResponse is incomplete.
                // As a consequence ResponseUri will be empty. To show a more detailed error we put the request uri instead.
                var responseUri = httpResponse.ResponseUri?.ToString();
                var errorDetailUri = responseUri.IsNullOrEmpty() ? responseUri : webApiRequest.Uri;

                Dictionary<string, string> errorResponse = null;
                try
                {
                    errorResponse = JsonConvert.DeserializeObject<Dictionary<string, string>>(httpResponse.Content);
                }
                catch { }

                webApiResponse = new WebApiResponse(httpResponse.StatusCode, errorResponse?["error"], new List<ErrorDetail>()
                {
                    new ErrorDetail(
                        string.IsNullOrEmpty(errorResponse?["error"])
                        ? $"{errorDetailUri} failed. Service unavailable."
                        : errorResponse["error"])
                });
            }


            return webApiResponse;
        }

        /// <summary>
        /// Make Request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private WebApiResponse MakeRequest(WebApiRequest request, Method method)
        {
            var httpClient = new RestClient();

            try
            {
                var restRequest = new RestRequest($"{_baseUri}{_apiPrefix}{request.Uri}", method);
                if (!string.IsNullOrEmpty(request.AccessToken))
                    restRequest.AddHeader("Authorization", "Bearer " + request.AccessToken);

                if (!string.IsNullOrEmpty(request.SessionId))
                    restRequest.AddHeader("Session", request.SessionId);

                var restResponse = httpClient.Execute(restRequest);
                return CreateWebApiResponse(restResponse, request);
            }
            catch (Exception ex)
            {
                return new WebApiResponse(HttpStatusCode.BadRequest, string.Empty
                                            , ex.ToErrorDetails());
            }
        }

        /// <summary>
        /// Check if Access Token has been refreshed after a call
        /// </summary>
        /// <param name="webApiResponse"></param>
        /// <param name="httpResponse"></param>
        private void CheckIsRefreshed(WebApiResponse webApiResponse, RestResponse httpResponse)
        {
            var accessToken = httpResponse.Headers.SingleOrDefault(x => x.Name == "access_token");
            var refreshToken = httpResponse.Headers.SingleOrDefault(x => x.Name == "refresh_token");
            if (accessToken?.Value != null
                && refreshToken?.Value != null)
            {
                OnAccessTokenRefreshed(
                                    new AccessTokenRefreshedEventArgs(accessToken.Value.ToString()
                                                                    , refreshToken.Value.ToString()));
            }
        }

        /// <summary>
        /// Post without data
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public WebApiResponse Post(WebApiRequest request)
        {
            return MakeRequest(request, Method.Post);
        }

        /// <summary>
        /// Do a Post to Mitrol Web Api uri
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="accessToken"></param>
        /// <param name="refreshToken"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public WebApiResponse Post<T>(WebApiRequest<T> request)
        {
            try
            {
                var httpClient = new RestClient();
                httpClient.UseNewtonsoftJson();
                RestRequest restRequest = new RestRequest($"{_baseUri}{_apiPrefix}{request.Uri}", Method.Post);
                restRequest.AddHeader("Session", request.SessionId);
                restRequest.AddHeader("Authorization", $"Bearer {request.AccessToken}");
                restRequest.AddHeader("cache-control", "no-cache");
                restRequest.AddHeader("content-type", "application/json; charset=utf-8");
                restRequest.AddParameter("application/json", JsonConvert.SerializeObject(request.Model),
                   ParameterType.RequestBody);
                var httpResponse = httpClient.Post(restRequest);
                return CreateWebApiResponse(httpResponse, request);
            }
            catch (Exception ex)
            {
                return new WebApiResponse(HttpStatusCode.BadRequest, string.Empty
                                            , ex.ToErrorDetails());
            }
        }

        public WebApiResponse WarmUp(WebApiRequest request)
        {
            var options = new RestClientOptions
            {
                Timeout = 15000,
                MaxTimeout = 15000,
            };
            var httpClient = new RestClient(options);
            try
            {
                var restRequest = new RestRequest($"{_baseUri}{_apiPrefix}{request.Uri}", Method.Get);
                restRequest.Timeout = 15000;
                var restResponse = httpClient.Execute(restRequest);
                return CreateWebApiResponse(restResponse, request);
            }
            catch (Exception ex)
            {
                return new WebApiResponse(HttpStatusCode.BadRequest, string.Empty
                                            , ex.ToErrorDetails());
            }
        }

        /// <summary>
        /// Get Request
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="accessToken">Access Token for query web api</param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public WebApiResponse Get(WebApiRequest request)
        {
            return MakeRequest(request, Method.Get);
        }

        public Result<T> Get<T>(WebApiRequest request)
        {
            var webApiResponse = Get(request);
            return webApiResponse.StatusCode == HttpStatusCode.OK
                ? Result.Ok(JsonConvert.DeserializeObject<T>(webApiResponse.JsonResult))
                : Result.Fail<T>(webApiResponse.Errors);
        }

        public Result<T> Get<T>(string url, IUserSession userSession)
            => Get<T>(new WebApiRequest(userSession) { Uri = url });

        // TODO: Implement Result pattern
        public IEnumerable<T> GetAll<T>(WebApiRequest request)
        {
            var webApiResponse = Get(request);
            return webApiResponse.StatusCode == HttpStatusCode.OK
                ? JsonConvert.DeserializeObject<IEnumerable<T>>(webApiResponse.JsonResult)
                : null;
            //: throw new Exception($"{webApiResponse.StatusCode}");
        }

        // TODO: Implement Result pattern
        public IEnumerable<T> GetAll<T>(string url, IUserSession userSession)
            => GetAll<T>(new WebApiRequest(userSession) { Uri = url });

        /// <summary>
        /// Http Action Delete Wrapping
        /// </summary>
        /// <param name="url"></param>
        /// <param name="accessToken"></param>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public WebApiResponse Delete(WebApiRequest request)
        {
            return MakeRequest(request, Method.Delete);
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <param name="timeout">10 second</param>
        /// <returns></returns>
        public Result<IUserSession> Login(LoginRequest loginRequest, int timeout = 10000)
        {
            try
            {
                var options = new RestClientOptions
                {
                    ThrowOnAnyError = true,
                    MaxTimeout = timeout
                };

                var client = new RestClient(options);

                var request = new RestRequest($"{_baseUri}{_apiPrefix}login", Method.Post);
                //var request = new RestRequest($"http://localhost:1000{_apiPrefix}token", Method.Post);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", JsonConvert.SerializeObject(loginRequest), ParameterType.RequestBody);
                var response = client.Execute(request);
                var webApiResponse = new WebApiResponse(response.StatusCode, response.Content.Normalize());

                if (webApiResponse.StatusCode == HttpStatusCode.OK)
                {
                    return Result.Ok(JsonConvert.DeserializeObject<UserSession>(webApiResponse.JsonResult) as IUserSession);
                }
                else if (webApiResponse.Errors != null && webApiResponse.Errors.Any())
                {
                    return Result.Fail<IUserSession>(webApiResponse.Errors);
                }
                else if (JsonConvert.DeserializeObject<Dictionary<string, string>>(webApiResponse.JsonResult) is Dictionary<string, string> errors)
                {
                    return Result.Fail<IUserSession>(errors["error"]);
                }
                else
                {
                    return Result.Fail<IUserSession>(webApiResponse.StatusCode.ToString());
                }
            }
            catch (Exception ex)
            {
                return Result.Fail<IUserSession>(ex);
            }


        }

        /// <summary>
        /// Put Action
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="accessToken"></param>
        /// <param name="refreshToken"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public WebApiResponse Put<T>(WebApiRequest<T> request)
        {
            try
            {
                var httpClient = new RestClient();
                httpClient.UseNewtonsoftJson();
                var restRequest = new RestRequest($"{_baseUri}{_apiPrefix}{request.Uri}", Method.Put);
                if (!string.IsNullOrEmpty(request.AccessToken))
                    restRequest.AddHeader("Authorization", "Bearer " + request.AccessToken);
                if (!string.IsNullOrEmpty(request.AccessToken))
                    restRequest.AddHeader("Session", request.SessionId);

                restRequest.AddHeader("Content-Type", "application/json");
                restRequest.AddParameter("application/json", JsonConvert.SerializeObject(request.Model), ParameterType.RequestBody);
                var httpResponse = httpClient.Execute(restRequest);
                return CreateWebApiResponse(httpResponse, request);
            }
            catch (Exception ex)
            {
                return new WebApiResponse(HttpStatusCode.BadRequest, string.Empty
                                            , ex.ToErrorDetails());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webApiRequest"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public WebApiResponse Upload(WebApiRequest webApiRequest, string filePath)
        {
            try
            {
                var client = new RestClient();
                var request = new RestRequest($"{_baseUri}{_apiPrefix}{webApiRequest.Uri}", Method.Post);
                request.AddHeader("Session", webApiRequest.SessionId);
                request.AddHeader("Authorization", $"Bearer {webApiRequest.AccessToken}");
                request.AddFile("content", filePath);

                var response = client.Execute(request);
                return CreateWebApiResponse(response, webApiRequest);
            }
            catch (Exception ex)
            {
                return new WebApiResponse(HttpStatusCode.BadRequest, string.Empty
                                            , ex.ToErrorDetails());
            }
        }

        /// <summary>
        /// Session Logout
        /// </summary>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public WebApiResponse Logout(IUserSession userSession)
        {
            return Put(new WebApiRequest<string>(userSession)
            {
                Uri = $"users/{userSession.SessionId}/logout",
            });
        }

        public WebApiResponse Patch<T>(WebApiRequest<T> request)
        {
            try
            {
                var httpClient = new RestClient();
                httpClient.UseNewtonsoftJson();
                RestRequest restRequest = new RestRequest($"{_baseUri}{_apiPrefix}{request.Uri}", Method.Patch);
                restRequest.AddHeader("Session", request.SessionId);
                restRequest.AddHeader("Authorization", $"Bearer {request.AccessToken}");
                restRequest.AddHeader("cache-control", "no-cache");
                restRequest.AddHeader("content-type", "application/json; charset=utf-8");
                restRequest.AddParameter("application/json", JsonConvert.SerializeObject(request.Model),
                   ParameterType.RequestBody);
                var httpResponse = httpClient.Patch(restRequest);
                return CreateWebApiResponse(httpResponse, request);
            }
            catch (Exception ex)
            {
                return new WebApiResponse(HttpStatusCode.BadRequest, string.Empty
                                            , ex.ToErrorDetails());
            }
        }
    }
}