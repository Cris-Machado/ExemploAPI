using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PointNow.API.Presentation.Controllers.Base
{

    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        #region Ctor
        public BaseController()
        {
        }
        #endregion

    }
}
