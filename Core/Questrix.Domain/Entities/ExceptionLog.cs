using Questrix.Domain.Common;

namespace Questrix.Domain.Entities
{
    public class ExceptionLog : EntityBase<int>
    {
        public string Message { get; set; }
        public string? Source { get; set; }
        public string? StackTrace { get; set; }

        public static ExceptionLog Cast(Exception ex) => new()
        {
            Message = ex.Message,
            Source = ex.Source,
            StackTrace = ex.StackTrace,
        };
    }
}
