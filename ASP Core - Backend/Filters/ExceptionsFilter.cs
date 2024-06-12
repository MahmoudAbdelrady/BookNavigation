using aspcorebackend.Utils.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace aspcorebackend.Filters {
    public class ExceptionsFilter : IExceptionFilter {
        public void OnException(ExceptionContext context) {
            Response<object> response = ResponseMaker<object>.ErrorRes(context.Exception.Message);
            context.Result = new JsonResult(response) {
                StatusCode = 500
            };
        }
    }
}
