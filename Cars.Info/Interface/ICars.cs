using CarRentalManagment.Controllers;
using Cars.Entities;
using Cars.Info.Model;
using Microsoft.AspNetCore.Mvc;

namespace Cars.Info.Interface
{
    public interface ICars
    {
        Task<IActionResult> CreateNewCar(ControllerBase controller, CarsInfo carEntity);
        Task<IActionResult> DeleteCarAsync(ControllerBase controller, int id);
        Task<IActionResult> EditCar(ControllerBase controller, int id, CarsInfo request);
        Task<IActionResult> GetAllCarsAsync(ControllerBase controller);
        Task<IActionResult> GetCarInfoByIdAsync(ControllerBase controller ,int id);
    }
}
