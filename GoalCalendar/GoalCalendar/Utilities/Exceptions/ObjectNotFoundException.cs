using System;
using System.Net;
using System.Runtime.Serialization;

namespace GoalCalendar.Utilities.Exceptions
{
    [Serializable]
    public class ObjectNotFoundException : BaseHttpException
    {
        public ObjectNotFoundException(string message, string description)
            : base(message, description, (int)HttpStatusCode.NotFound)
        {
        }

        protected ObjectNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
