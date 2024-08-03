using Microsoft.AspNetCore.Mvc;
using SkinetAPI.Errors;

namespace SkinetAPI.Controllers
{
    /// <summary>
    /// This controller will be re-directed if the no endpoints are not matching. The primary goal of this controller for providing consistent response object to the client
    /// </summary>
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
