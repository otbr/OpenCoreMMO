using System.Net;

namespace NeoServer.Web.Shared.Exceptions
{
    public class NeoForbiddenException : NeoException
    {
        #region constructors

        public NeoForbiddenException(string message = "") : base(message, HttpStatusCode.Forbidden) { }

        #endregion
    }
}
