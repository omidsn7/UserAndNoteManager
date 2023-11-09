using Microsoft.AspNetCore.Mvc;

namespace UserAndNoteManager
{
    static class Common
    {
        public static JsonResult BadRequest()
        {
            return new JsonResult(new BadRequestResult());
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
