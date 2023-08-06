using AutoMapper;
using CarRentalApi.Model;
using CarRentalApi.Responses;
using CarRentalManagment.Controllers;
using CarRentalManagment.PostgresContext;
using Cars.Entities;
using Cars.Info.Interface;
using Cars.Info.Model;
using Cars.Info.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Postgres.Context.Entities;
using RentInfo.Entities;

namespace Cars.Info.Repository
{
    public class CarsService : ICars
    {
        private readonly PostgresDbContext _context;
        private readonly ILogger<CarsController> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHostingEnvironment _environment;

        public CarsService(PostgresDbContext postgresContext, ILogger<CarsController> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, IHostingEnvironment environment)
        {
            _context = postgresContext ?? throw new ArgumentException(nameof(postgresContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpContextAccessor = httpContextAccessor;
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        public async Task<IActionResult> CreateNewCar(ControllerBase controller, CarsInfo request)
        {
            AdminEntity adminEntity = (AdminEntity)await Tools.GetUser(_httpContextAccessor, _context);
            if (adminEntity == null) { return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.INVALID_TOKEN }); }

            CarEntity newCar = new CarEntity()
            {
                Admin = adminEntity,
                Brand = request.Brand,
                Color = request.Color,
                Model = request.Model,
                Price = request.Price,
                Seats = request.Seats,
                Status = request.Status
            };
            newCar = _context.CarsInfo.Add(newCar).Entity;
            _context.SaveChanges();
            if (request.Image != null)
            {
                newCar.Image = SaveImage(newCar.CarId, request.Image);
            }
            _context.SaveChanges();

            return controller.Ok(CarPresenter.GetPresenter(newCar));
        }

        public async Task<IActionResult> DeleteCarAsync(ControllerBase controller, int id)
        {
            AdminEntity admin = (AdminEntity)await Tools.GetUser(_httpContextAccessor, _context);
            if (admin == null)
            {
                controller.BadRequest(new ErrorResponse() { message = ErrorMessages.INVALID_TOKEN });
            }

            var car = await _context.CarsInfo.Where(_ => _.CarId == id).FirstOrDefaultAsync();
            if (car == null)
            {
                _logger.LogInformation(ErrorMessages.ITEM_NOT_FOUND + $" to delete by id: {id} ");
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.CAR_NOT_FOUND });
            }

            foreach (RentalEntity rent in car.Rents)
            {
                _context.RentalInfo.Remove(rent);
            }
            car.Rents.Clear();
            _context.SaveChanges();
            _context.CarsInfo.Remove(car);
            _context.SaveChanges();
            return controller.Ok();
        }

        public async Task<IActionResult> EditCar(ControllerBase controller, int id, CarsInfo request)
        {
            if (!_context.CarsInfo.Any(_=>_.CarId == id))
            {
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.CAR_NOT_FOUND });
            }
            CarEntity car = _context.CarsInfo.Where(_=>_.CarId == id).First();
            
            if (request.Brand != null)
            {
                car.Brand = request.Brand;
            }

            if (request.Model != null)
            {
                car.Model = request.Model;
            }

            if (request.Seats != null)
            {
                car.Seats = request.Seats;
            }

            if (request.Price != null)
            {
                car.Price = request.Price;
            }

            if (request.Image != null)
            {
                car.Image = SaveImage(car.CarId, request.Image);
            }

            if (request.Color != null)
            {
                car.Color = request.Color;
            }

            if (request.Status != null)
            {
                car.Status = request.Status;
            }

            _context.SaveChanges();
            return controller.Ok(CarPresenter.GetPresenter(car));
        }

        public async Task<IActionResult> GetAllCarsAsync(ControllerBase controller)
        {
            return controller.Ok(CarPresenter.GetPresenter(await _context.CarsInfo.OrderBy(_ => _.CarId).ToListAsync()));
        }

        public async Task<IActionResult> GetCarImage(ControllerBase controller, int id)
        {
            CarEntity car = _context.CarsInfo.Where(_ => _.CarId == id).FirstOrDefault();
            if (car == null)
            {
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.CAR_NOT_FOUND });
            }

            string wwwPath = Path.GetFullPath("wwwroot");
            string path = Path.Combine(wwwPath, "Uploads");
            Byte[] b;
            try
            {
               b = File.ReadAllBytes(Path.Combine(path, car.Image));
            }
            catch (Exception)
            {
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.IMAGE_NOT_FOUND });
            }

            return controller.File(b, "image/jpeg");
        }

        public async Task<IActionResult> GetCarInfoByIdAsync(ControllerBase controller, int id)
        {
            var car = await _context.CarsInfo.Where(_ => _.CarId == id).FirstOrDefaultAsync();
            if (car == null)
            {
                _logger.LogInformation(ErrorMessages.ITEM_NOT_FOUND + $" car find by id: {id} ");
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.CAR_NOT_FOUND });
            }
            return controller.Ok(CarPresenter.GetPresenter(car));
        }

        private string SaveImage(int carId, IFormFile image)
        {
            string wwwPath = Path.GetFullPath("wwwroot");
            string path = Path.Combine(wwwPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filename = carId + Path.GetExtension(image.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path, filename), FileMode.Create))
            {
                image.CopyTo(stream);
            }
            return filename;
        }
    }
}
