using System;
using System.Runtime.Serialization;

namespace GoalCalendar.Utilities.Exceptions
{
    [Serializable]
    public class BaseHttpException : Exception
    {
        public int Code { get; }
        public string Description { get; }
        public BaseHttpException(string message, string description, int code) : base(message)
        {
            Code = code;
            Description = description;
        }

        protected BaseHttpException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
