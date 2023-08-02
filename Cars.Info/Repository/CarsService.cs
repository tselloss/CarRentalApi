using AutoMapper;
using CarRentalApi.Model;
using CarRentalApi.Responses;
using CarRentalManagment.Controllers;
using CarRentalManagment.PostgresContext;
using Cars.Entities;
using Cars.Info.Interface;
using Cars.Info.Model;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Postgres.Context.Entities;
using System.Dynamic;

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

        public async Task<IActionResult> CreateNewCar(ControllerBase controller,CarsInfo request)
        {
            AdminEntity adminEntity = (AdminEntity) await Tools.GetUser(_httpContextAccessor, _context);
            if (adminEntity == null) { return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.INVALID_TOKEN }); }

            CarEntity newCar = _mapper.Map<CarEntity>(request);
            newCar.Admin = adminEntity;
            newCar = _context.CarsInfo.Add(newCar).Entity;
            _context.SaveChanges();

            return controller.Ok(newCar);
        }

        public async Task<IActionResult> DeleteCarAsync(ControllerBase controller, int id)
        {
            AdminEntity admin = (AdminEntity) await Tools.GetUser(_httpContextAccessor, _context);
            if (admin == null)
            {
                controller.BadRequest(new ErrorResponse() { message = ErrorMessages.INVALID_TOKEN });
            }

            var car = await _context.CarsInfo.Where(_=>_.CarId == id).FirstOrDefaultAsync();
            if (car == null)
            {
                _logger.LogInformation(ErrorMessages.ITEM_NOT_FOUND + $" to delete by id: {id} ");
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.CAR_NOT_FOUND });
            }

            _context.CarsInfo.Remove(car);
            _context.SaveChanges();
            return controller.Ok();
        }

        public async Task<IActionResult> GetAllCarsAsync(ControllerBase controller)
        {
            List<CarsInfo> carsInfos = new List<CarsInfo>();
            foreach (CarEntity carEntity in await _context.CarsInfo.OrderBy(_ => _.CarId).ToListAsync())
            {
                CarsInfo carsInfo = _mapper.Map<CarsInfo>(carEntity);
                carsInfos.Add(carsInfo);
            }
            return controller.Ok(await _context.CarsInfo.OrderBy(_ => _.CarId).ToListAsync());
        }

        public async Task<IActionResult> GetCarInfoByIdAsync(ControllerBase controller, int id)
        {
            var car = await _context.CarsInfo.Where(_ => _.CarId == id).FirstOrDefaultAsync();
            if (car == null)
            {
                _logger.LogInformation(ErrorMessages.ITEM_NOT_FOUND + $" car find by id: {id} ");
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.CAR_NOT_FOUND });
            }
            return controller.Ok(_mapper.Map<CarsInfo>(car));
        }
    }
}
