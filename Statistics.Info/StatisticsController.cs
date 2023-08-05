using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Statistics.Info.Interface;

namespace Statistics.Info
{
    [ApiController]
    [Route("api/statistics")]
    [Authorize]
    public class StatisticsController : ControllerBase
    {
        private IStatistics _statistics;

        public StatisticsController(IStatistics statistics) 
        {
            _statistics = statistics ?? throw new ArgumentException(nameof(statistics));
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetUserHistory()
        {
            return await _statistics.GetHistory(this);
        }

        [HttpGet("total_spendings")]
        public async Task<IActionResult> GetTotalSpendings()
        {
            return await _statistics.GetTotalSpendings(this);
        }

        [HttpGet("total_statistics")]
        public async Task<IActionResult> GetTotalStatistics()
        {
            return await _statistics.GetTotalStatistics(this);
        }

    }
}