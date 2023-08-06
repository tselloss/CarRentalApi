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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Postgres.Context.Entities;
using RentInfo.Entities;
using System;

namespace Cars.Info.Repository
{
    public class CarsService : ICars
    {
        private readonly PostgresDbContext _context;
        private readonly ILogger<CarsController> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarsService(PostgresDbContext postgresContext, ILogger<CarsController> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = postgresContext ?? throw new ArgumentException(nameof(postgresContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpContextAccessor = httpContextAccessor;
            _logger = logger ?? throw new ArgumentException(nameof(logger));
        }

        public async Task<IActionResult> AddCarImage(ControllerBase controller, IFormFile request)
        {
            //try
            //{
            //    List<Customer> list = JsonConvert.DeserializeObject<List<Customer>>(objFile.Customers);
            //    obj.LstCustomer = list;
            //    obj._fileAPI.ImgID = objFile.ImgID;
            //    obj._fileAPI.ImgName = "\\Upload\\" + objFile.files.FileName;
            //    if (objFile.files.Length > 0)
            //    {
            //        if (!Directory.Exists(_environment.WebRootPath + "\\Upload"))
            //        {
            //            Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
            //        }
            //        using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + objFile.files.FileName))
            //        {
            //            objFile.files.CopyTo(filestream);
            //            filestream.Flush();
            //            //  return "\\Upload\\" + objFile.files.FileName;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
            return controller.NoContent();
        }

        public async Task<IActionResult> CreateNewCar(ControllerBase controller, CarsInfo request)
        {
            AdminEntity adminEntity = (AdminEntity)await Tools.GetUser(_httpContextAccessor, _context);
            if (adminEntity == null) { return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.INVALID_TOKEN }); }

            CarEntity newCar = _mapper.Map<CarEntity>(request);
            newCar.Admin = adminEntity;
            newCar = _context.CarsInfo.Add(newCar).Entity;
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

            if (request.Seats != 0)
            {
                car.Seats = request.Seats;
            }

            if (request.Price != 0)
            {
                car.Price = request.Price;
            }

            if (request.Image != null)
            {
                car.Image = request.Image;
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
    }
}
