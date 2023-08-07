using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recomentation.Info.Interface
{
    public interface IRecomentation
    {
        Task<IActionResult> getCars(ControllerBase controller);
        Task<IActionResult> getDataset(ControllerBase controller);
    }
}
