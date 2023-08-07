using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Info.Interface
{
    public interface IStatistics
    {
        Task<IActionResult> GetHistory(ControllerBase controller);
        Task<IActionResult> GetTotalSpendings(ControllerBase controller);
        Task<IActionResult> GetTotalStatistics(ControllerBase controller);
        Task<IActionResult> GetUserSpendings(ControllerBase controller);
    }
}
