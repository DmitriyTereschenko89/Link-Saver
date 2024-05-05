namespace UrlSaver.Api.Extentions
{
    public static class ResultExtention
    {
        public static IResult ToProblemDetails(this IResult result)
        {
            return Results.Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Bad request",
                extensions: new Dictionary<string, object>
                {
                    ["errors"] = new[] { result.ToString() }
                });
        }
    }
}
