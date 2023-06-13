namespace Mitrol.Framework.Domain.SignalR
{
    using Microsoft.AspNetCore.SignalR.Client;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Abstract base class for each SignalR client that hides some basic implementation details.
    /// </summary>
    public abstract class HubClient : IHubClient
    {
        // A connection used to invoke hub methods on the SignalR Server.
        private HubConnection _hubConnection;

        // Established connection count
        private int _connectedClientsCount = 0;

        /// <summary>
        /// Default constructor that initializes the hub connection.
        /// </summary>
        public HubClient()
        {
            InitializeHubConnection();
        }

        /// <summary>
        /// Default constructor that initializes the hub connection at the specified URL.
        /// </summary>
        /// <param name="hubUrl">A System.String representing the hub's URL.</param>
        public HubClient(string hubUrl, int connectDelay = 5000)
        {
            if (string.IsNullOrWhiteSpace(hubUrl)) throw new ArgumentException($"'{nameof(hubUrl)}' cannot be null or whitespace.", nameof(hubUrl));

            HubUrl = hubUrl;

            InitializeHubConnection(connectDelay);
        }

        /// <summary>
        /// Initializes and connects the client to the hub.
        /// </summary>
        protected void InitializeHubConnection(int connectDelay = 5000)
        {
            if(Uri.TryCreate(HubUrl, UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            { 
                _hubConnection = new HubConnectionBuilder()
                    .AddNewtonsoftJsonProtocol(config => {
                        config.PayloadSerializerSettings = new JsonSerializerSettings
                        {
                            ContractResolver = new DefaultContractResolver(),
                            TypeNameHandling = TypeNameHandling.Auto,
                            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                        };
                    })
                    .WithAutomaticReconnect()
                    .WithUrl(HubUrl)
                    .Build();

                //_hubConnection.Closed += ex => Task.Factory.StartNew(() => Debug.WriteLine(ex));
                //_hubConnection.Reconnecting += _ => Task.Factory.StartNew(() => Debug.WriteLine("Reconnecting"));
                //_hubConnection.Reconnected += _ => Task.Factory.StartNew(() => Debug.WriteLine("Reconnected"));

               Task.Delay(connectDelay)
                    .ContinueWith(_ => ConnectAsync())
                    .Wait();
            }
        }

        /// <summary>
        /// A connection used to invoke hub methods on the SignalR Server.
        /// </summary>
        public HubConnection HubConnection => _hubConnection;

        /// <summary>
        /// The URL used to contact to the server.
        /// </summary>
        protected virtual string HubUrl { get; }

        /// <summary>
        /// Connects to the server.
        /// </summary>
        public Task ConnectAsync(CancellationToken cancellationToken = default) => _hubConnection.StartAsync(cancellationToken);

        /// <summary>
        /// Disconnects from the server.
        /// </summary>
        public Task DisconnectAsync() => _hubConnection.StopAsync();

        /// <summary>
        /// True if there is any established connection but the requester; otherwise false.
        /// </summary>
        public bool AnyClientOnline => _connectedClientsCount > 1;

        /// <summary>
        /// Enables the connections counter functionality.
        /// </summary>
        public void UseConnectedClientCount() => HubConnection.On<int>("ConnectedClientCount", connectedClientsCount => _connectedClientsCount = connectedClientsCount);

        /// <summary>
        /// server-only method
        /// </summary>
        public Task ConnectedClientCount(int _) => throw new NotImplementedException();
    }
}
