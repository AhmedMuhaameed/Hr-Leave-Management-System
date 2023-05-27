using System.Runtime.Serialization;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequestCommand
{
    [Serializable]
    internal class NotFoundExecption : Exception
    {
        public NotFoundExecption()
        {
        }

        public NotFoundExecption(string? message) : base(message)
        {
        }

        public NotFoundExecption(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NotFoundExecption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}