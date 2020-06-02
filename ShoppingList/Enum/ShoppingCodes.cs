using System;
namespace ShoppingList.Enum
{
    public enum ShoppingCodes
    {
        Success = 200,
        NoContent = 204,
        PartialContent = 206,

        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,

        InternalServerError = 500,
        NotImplemented = 501,
        ServiceUnavailable = 503

    }
}
