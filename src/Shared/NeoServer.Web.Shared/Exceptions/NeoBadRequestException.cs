using System.Net;

namespace NeoServer.Web.Shared.Exceptions
{
    public class NeoBadRequestException : NeoException
    {
        #region constructors

        public NeoBadRequestException(string message = "", object customData = null) : base(message, HttpStatusCode.BadRequest, customData) { }

        #endregion
    }
}
