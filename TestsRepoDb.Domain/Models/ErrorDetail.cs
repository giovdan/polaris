namespace RepoDbVsEF.Domain.Models
{
    using FluentValidation.Results;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ErrorDetail
    {
        [JsonProperty("Field")]
        public string Field { get; set; }

        [JsonProperty("ErrorCodes")]
        public List<string> ErrorCodes { get; set; }

        public ErrorDetail()
        {
            ErrorCodes = new List<string>();
        }

        public ErrorDetail(string field, string errorCode) : this()
        {
            Field = field;
            ErrorCodes.Add(errorCode);
        }

        public ErrorDetail(string errorCode) : this(string.Empty, errorCode)
        {

        }

        public ErrorDetail(string field, List<string> errors)
        {
            Field = field;
            ErrorCodes = errors;
        }

        public override string ToString()
        {
            return Field.IsNotNullOrEmpty()
                ? $"{Field} => {string.Join(",", ErrorCodes)}"
                : $"{string.Join(",", ErrorCodes)}";
        }
    }

    public static class ErrorDetailExtensions
    {
        public static string ToErrorString(this List<ErrorDetail> errorDetails)
        {
            var builder = new StringBuilder();

            errorDetails.ForEach(e =>
            {
                foreach (var errorCode in e.ErrorCodes)
                {
                    builder.AppendLine(errorCode);
                }
            });

            return builder.ToString().Trim();
        }

        public static IEnumerable<ErrorDetail> GetErrorDetails(this IList<ValidationFailure> failures)
        {
            return failures.Select(failure => new ErrorDetail
            {
                Field = failure.PropertyName,
                ErrorCodes = new List<string>
                    {
                        failure.ErrorMessage ?? failure.ErrorCode
                    }
            });
        }
    }
}
