using Microsoft.AspNetCore.Mvc;

namespace UserAndNoteManager
{
    static class Common
    {
        public static JsonResult BadRequest(string Error = null)
        {
            return new JsonResult(new BadRequestObjectResult(Error));
        }

        public static JsonResult NotFound()
        {
            return new JsonResult(new NotFoundResult());
        }

        public static JsonResult NoContent()
        {
            return new JsonResult(new NoContentResult());
        }

        public static JsonResult OkResult()
        {
            return new JsonResult(new OkResult());
        }
          
    }
}
