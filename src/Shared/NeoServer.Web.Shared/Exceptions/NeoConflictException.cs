using System.Net;

namespace NeoServer.Web.Shared.Exceptions
{
    public class NeoConflictException : NeoException
    {
        #region constructors

        public NeoConflictException(string message = "", object customData = null) : base(message, HttpStatusCode.Conflict, customData) { }

        #endregion
    }
}
