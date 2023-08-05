using AutoMapper;
using CarRentalApi.Model;
using CarRentalApi.Responses;
using CarRentalManagment.Controllers;
using CarRentalManagment.PostgresContext;
using Cars.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Postgres.Context.Entities;
using Rental.Info.Requests;
using Rental.Info.Response;
using RentInfo.Entities;
using RentInfo.Interface;
using RentInfo.Model;
using User.Info.Model;

namespace RentInfo.Repository
{
    public class RentalService : IRental
    {
        private readonly PostgresDbContext _context;
        private readonly ILogger<UserActionsController> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public RentalService(PostgresDbContext postgresContext, ILogger<UserActionsController> logger, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _context = postgresContext ?? throw new ArgumentException(nameof(postgresContext));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpContext = httpContext;
        }

        public async Task<IActionResult> CreateReservation(ControllerBase controller, RentRequest request)
        {
            ClientEntity client = (ClientEntity) await Tools.GetUser(_httpContext, _context);
            if (client == null)
            {
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.INVALID_TOKEN });
            }

            CarEntity car = null;
            if (!_context.CarsInfo.Any(_=>_.CarId == request.CarId))
            {
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.CAR_NOT_FOUND });
            }
            car = _context.CarsInfo.Where(_ => _.CarId == request.CarId).FirstOrDefault();
            
            RentalEntity rent = new RentalEntity()
            {
                Client = client,
                Car = car,
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                Value = request.Value,
            };
            rent = _context.RentalInfo.Add(rent).Entity;
            _context.SaveChanges();

            return controller.Ok(RentPresenter.getPresenter(rent));
        }

        public async Task<IActionResult> DeleteReservationAsync(ControllerBase controller, int id)
        {
            var rental = await _context.RentalInfo.Where(_ => _.RentalId == id).FirstOrDefaultAsync();
            if (rental == null)
            {
                _logger.LogInformation($"We have no rent on Db with this id: {id} ");
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.RENT_NOT_FOUND });
            }
            _context.RentalInfo.Remove(rental);
            await _context.SaveChangesAsync();
            return controller.Ok();
        }

        public async Task<IActionResult> GetAllReservationsAsync(ControllerBase controller)
        {
            var rental = await _context.RentalInfo.OrderBy(_ => _.RentalId).ToListAsync();
            if (rental == null)
            {
                _logger.LogInformation("We have no reservations on Db");
            }
            return controller.Ok(RentPresenter.getPresenter(rental));
        }

        public async Task<IActionResult> GetReservationInfoByIdAsync(ControllerBase controller, int id)
        {
            RentalEntity rentEntity = await _context.RentalInfo.Where(_ => _.RentalId == id).FirstOrDefaultAsync();
            if (rentEntity == null)
            {
                _logger.LogInformation($"We have no reservation on Db with this id: {id} ");
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.RENT_NOT_FOUND });
            }
            return controller.Ok(RentPresenter.getPresenter(rentEntity));
        }

        public Task UpdateReservationAsync(RentalEntity rentalEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
