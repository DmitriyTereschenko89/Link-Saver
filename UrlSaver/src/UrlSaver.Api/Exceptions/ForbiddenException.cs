namespace UrlSaver.Api.Exceptions
{
    [Serializable]
    public class ForbiddenException : Exception
    {
        public override string Message => "Foribidden. You do not have permission to access this resource";
    }
}
