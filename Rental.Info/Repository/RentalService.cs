using AutoMapper;
using CarRentalApi.Model;
using CarRentalApi.Responses;
using CarRentalManagment.Controllers;
using CarRentalManagment.PostgresContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

        public RentalService(PostgresDbContext postgresContext, ILogger<UserActionsController> logger, IMapper mapper)
        {
            _context = postgresContext ?? throw new ArgumentException(nameof(postgresContext));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IActionResult> CreateReservation(ControllerBase controller, RentalInfo request)
        {
            var newRent = _mapper.Map<RentalEntity>(request);
            _context.Add(newRent);
            _context.SaveChanges();
            return controller.Ok(newRent);
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
            return controller.Ok(_mapper.Map<IEnumerable<RentalInfo>>(rental));
        }

        public async Task<IActionResult> GetReservationInfoByIdAsync(ControllerBase controller, int id)
        {
            var rentalId = await _context.RentalInfo.Where(_ => _.RentalId == id).FirstOrDefaultAsync();
            if (rentalId == null)
            {
                _logger.LogInformation($"We have no reservation on Db with this id: {id} ");
            }
            return controller.Ok(_mapper.Map<UserInfo>(rentalId));
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
