namespace RepoDbVsEF.Domain.Models
{
    using RepoDbVsEF.Domain.Enums;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class Result
    {
        public Result()
        {

        }

        public bool Success { get; }
        public bool Failure => !Success;
        public bool Aborted { get; }

        [Obsolete] public string Error => Errors?.First().ToString();

        public List<ErrorDetail> Errors { get; }

        protected Result(bool success, IEnumerable<ErrorDetail> errors, bool aborted = false)
        {
            Debug.Assert(success || errors.Any());
            Debug.Assert(!success || errors == null);

            Success = success;
            Aborted = aborted;

            if (!Success)
                Errors = errors.ToList();
        }

        public static Result Ok() => new Result(success: true, errors: default);

        public static Result<T> Ok<T>(T value) => new Result<T>(value: value, success: true, errors: default);

        public static Result Fail(string error)
        {
            Debug.Assert(!string.IsNullOrEmpty(error));
            return Fail(new ErrorDetail(error));
        }

        public static Result Fail(Exception error)
        {
            return Fail(error.InnerException?.Message ?? error.Message);
        }

        public static Result Fail(ErrorDetail errorDetail)
        {
            Debug.Assert(errorDetail != null);
            return Fail(new[] { errorDetail });
        }

        public static Result Fail(IEnumerable<ErrorDetail> errorDetails)
        {
            Debug.Assert(errorDetails != null && errorDetails.Any());
            return new Result(false, errorDetails.ToList());
        }

        public static Result<T> Fail<T>(string error)
        {
            Debug.Assert(!string.IsNullOrEmpty(error));
            return new Result<T>(default, false, new[] { new ErrorDetail(error) });
        }

        public static Result<T> Fail<T>(ErrorDetail errorDetail)
        {
            Debug.Assert(errorDetail != null);
            return new Result<T>(default, false, new[] { errorDetail });
        }

        public static Result<T> Fail<T>(IEnumerable<ErrorDetail> errorDetails)
        {
            Debug.Assert(errorDetails != null && errorDetails.Any());
            return new Result<T>(default, false, errorDetails.ToList());
        }

        public static Result<T> Fail<T>(Exception exception)
        {
            Debug.Assert(exception != null);
            return new Result<T>(default, false, new[] { new ErrorDetail(exception.InnerException?.Message ?? exception.Message) });
        }

        public static Result<T> Fail<T>(Result result) => new Result<T>(default, result.Success, result.Errors);

        [DebuggerStepThrough]
        public static Result Combine(params Result[] results) => Combine(results.AsEnumerable());

        [DebuggerStepThrough]
        public static Result Combine(IEnumerable<Result> results) => results.Where(result => result.Failure).FirstOrDefault() ?? Ok();

        [DebuggerStepThrough]
        public static Result AggregateIfFails(params Result[] results)
        {
            var errors = new List<ErrorDetail>();
            foreach (Result result in results)
            {
                if (result.Failure)
                {
                    errors.AddRange(result.Errors);
                }
            }

            return errors.Any()
                        ? Result.Fail(errors)
                        : Ok();
        }

        public static Result Abort()
            => new Result(success: false,
                          errors: new[] { new ErrorDetail(ErrorCodesEnum.ERR_GEN009.ToString()) },
                          aborted: true);

        public static Result<T> Abort<T>()
            => new Result<T>(value: default,
                             success: false,
                             errors: new[] { new ErrorDetail(ErrorCodesEnum.ERR_GEN009.ToString()) },
                             aborted: true);
    }

    public class Result<T> : Result
    {
        private T _value;

        public T Value
        {
            get
            {
                Debug.Assert(Success);

                return _value;
            }
            private set { _value = value; }
        }

        protected internal Result(T value, bool success, IEnumerable<ErrorDetail> errors, bool aborted = false)
                : base(success, errors, aborted)
        {
            if (success)
                Value = value;
        }
    }

    public static class ResultExtensions
    {
        [DebuggerStepThrough]
        public static Result OnSuccessIf(this Result result, bool condition, Action action)
        {
            if (condition)
                return OnSuccess(result, action);

            return result;
        }

        [DebuggerStepThrough]
        public static Result OnSuccessIf<T>(this Result result, bool condition, Func<T> func)
        {
            if (condition)
                return OnSuccess<T>(result, func);
            return result;
        }

        public static Result OnSuccessIf<T>(this Result<T> result, Func<T, bool> predicate, Action<T> action)
        {
            return result.Failure ? result
                : predicate(result.Value) ? OnSuccess<T>(result, action)
                : result;
        }

        [DebuggerStepThrough]
        public static Result OnSuccess(this Result result, Func<Result> func)
            => result.Failure ? result : func();

        [DebuggerStepThrough]
        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.Failure)
                return result;

            action();

            return Result.Ok();
        }

        [DebuggerStepThrough]
        public static Result OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            if (result.Failure)
                return result;

            action(result.Value);

            return Result.Ok();
        }

        /*
         * Result
         * .OnSuccess(() => obj)
         */
        [DebuggerStepThrough]
        public static Result<T> OnSuccess<T>(this Result result, Func<T> func)
            => result.Failure ? Result.Fail<T>(result) : Result.Ok(func());

        /*
         * Result
         * .OnSuccess(() => Result.Ok(obj))
         */
        [DebuggerStepThrough]
        public static Result<T> OnSuccess<T>(this Result result, Func<Result<T>> func)
        {
            if (result.Failure)
                return Result.Fail<T>(result);

            return func();
        }

        [DebuggerStepThrough]
        public static Result OnSuccess<T>(this Result<T> result, Func<T, Result> func)
            => result.Failure ? result : func(result.Value);

        /*
         * Result<T>
         * .OnSuccess(t => FuncReturnsTResult(t))
         */
        [DebuggerStepThrough]
        public static Result<TResult> OnSuccess<T, TResult>(this Result<T> result, Func<T, Result<TResult>> func)
            => result.Failure ? Result.Fail<TResult>(result) : func(result.Value);

        [DebuggerStepThrough]
        public static Result<TResult> OnSuccess<T, TResult>(this Result<T> result, Func<T, TResult> func)
            => result.Failure ? Result.Fail<TResult>(result) : Result.Ok(func(result.Value));

        [DebuggerStepThrough]
        public static Result<TResult> OnFailure<T, TResult>(this Result<T> result, Func<Result<T>, Result<TResult>> func)
            where T : class
            where TResult : class
            => result.Failure ? func(result) : Result.Ok(result.Value as TResult ?? throw new InvalidCastException());

        [DebuggerStepThrough]
        public static Result<T> OnFailure<T>(this Result<T> result, Func<Result<T>> func)
            => result.Failure ? func() : result;

        //[DebuggerStepThrough]
        //public static Result OnFailure(this Result result, Func<Result> func)
        //    => result.Failure ? func() : result;

        [DebuggerStepThrough]
        public static Result OnFailure(this Result result, Action action)
        {
            if (result.Failure)
            {
                action();
            }
            return result;
        }

        [DebuggerStepThrough]
        public static Result OnFailure(this Result result, Action<Result> action)
        {
            if (result.Failure)
            {
                action(result);
            }
            return result;
        }

        [DebuggerStepThrough]
        public static Result<T> OnFailure<T>(this Result<T> result, Action action)
        {
            if (result.Failure)
            {
                action();
            }
            return result;
        }

        [DebuggerStepThrough]
        public static Result<T> OnFailure<T>(this Result<T> result, Action<Result> action)
        {
            if (result.Failure)
            {
                action(result);
            }
            return result;
        }

        [DebuggerStepThrough]
        public static Result OnBoth(this Result result, Action action)
        {
            action();
            return result;
        }

        [DebuggerStepThrough]
        public static Result OnBoth(this Result result, Action<Result> action)
        {
            action(result);
            return result;
        }

        [DebuggerStepThrough]
        public static Result<T> OnBoth<T>(this Result<T> result, Action<Result<T>> action)
        {
            action(result);
            return result;
        }

        [DebuggerStepThrough]
        public static Result<TResult> OnBoth<T, TResult>(this Result<T> result, Func<Result<T>, Result<TResult>> func)
            => func(result);

        [DebuggerStepThrough]
        public static Result OnBoth(this Result result, Func<Result, Result> func)
            => func(result);

        [DebuggerStepThrough]
        public static Result AddError(this Result result, string errorMessage)
            => AddError(result, new ErrorDetail(errorMessage));

        [DebuggerStepThrough]
        public static Result AddError(this Result result, ErrorDetail errorDetail)
        {
            result.Errors.Insert(0, errorDetail);

            return result;
        }

        [DebuggerStepThrough]
        public static Result OnAborted(this Result result, Action action)
        {
            if (result.Aborted)
            {
                action();
            }
            return result;
        }

        [DebuggerStepThrough]
        public static Result OnAborted(this Result result, Action<Result> action)
        {
            if (result.Aborted)
            {
                action(result);
            }
            return result;
        }

        [DebuggerStepThrough]
        public static Result<T> OnAborted<T>(this Result<T> result, Action<Result<T>> action)
        {
            if (result.Aborted)
            {
                action(result);
            }
            return result;
        }
        [DebuggerStepThrough]
        public static Result OnFailureIfNotAbort(this Result result, Action action)
        {
            if (result.Failure && !result.Aborted)
            {
                action();
            }
            return result;
        }
    }
}
