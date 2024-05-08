namespace UrlSaver.Api.Exceptions
{
    [Serializable]
    public class ForbiddenException : Exception
    {
        public ForbiddenException() { }

        public ForbiddenException(string message) : base(message) { }

        public ForbiddenException(string message, Exception innerException) : base(message, innerException) { }

        public override string Message => "Foribidden. You do not have permission to access this resource";
    }
}
