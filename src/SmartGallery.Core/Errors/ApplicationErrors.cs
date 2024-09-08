using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartGallery.Core.Errors
{
    public static class ApplicationErrors
    {
        public static Error NotFoundError = new Error(404, "Resource Not found");
        public static Error BadRequestError = new Error(400, "A bad Request, you have made");
        public static Error InternalServerError = new Error(500, "Try in other time ");
        public static Error UnauthorizedError = new Error(401, "Invalid Email or password");
    }
}
