using User.Application.Common.Errors;

namespace User.WebAPI.Common
{
    public static class ErrorMappings
    {
        public static int GetStatusCode(Error error)
        {
            return error.Code switch
            {
                "Validation.Invalid" => 400,

                "User.NotFound" => 404,

                "User.EmailAlreadyExists" => 409,

                "User.InvalidPassword" => 401,

                _ => 400
            };
        }
    }
}
