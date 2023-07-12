using System.Net;

namespace NeoServer.Web.Shared.Exceptions
{
    public class NeoNotFoundException : NeoException
    {
        #region constructors

        public NeoNotFoundException(string message = "") : base(message, HttpStatusCode.NotFound) { }

        #endregion
    }
}
