using System.Net;

namespace NeoServer.Web.Shared.Exceptions
{
    public class NeoUnauthorizedException : NeoException
    {
        #region constructors

        public NeoUnauthorizedException(string message = "") : base(message, HttpStatusCode.Unauthorized) { }

        #endregion
    }
}
