using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recomentation.Info.Interface;

namespace Recomentation.Info
{
    [ApiController]
    [Route("api/recomentations")]
    public class RecomentationController : ControllerBase
    {
        private IRecomentation _recomentation;
        public RecomentationController(IRecomentation recomentation)
        {
            _recomentation = recomentation;
        }

        [HttpGet("dataset")]
        [Authorize]
        public async Task<IActionResult> getDataset()
        {
            return await _recomentation.getDataset(this);
        }

        [HttpGet("cars")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> getCars()
        {
            return await _recomentation.getCars(this);
        }
    }
}