using AutoMapper;
using CarRentalApi.Model;
using CarRentalApi.Responses;
using CarRentalManagment.Controllers;
using CarRentalManagment.PostgresContext;
using Cars.Info.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.ML;
using Microsoft.ML.Data;
using Postgres.Context.Entities;
using Recomentation.Info.Interface;
using Recomentation.Info.Model;
using Recomentation.Info.Response;
using System.Linq;
using System.Text;
using Users.Entities;

namespace Recomentation.Info.Repository
{
    public class RecomentationService : IRecomentation
    {
        private readonly PostgresDbContext _context;
        private readonly ILogger<UserActionsController> _logger;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public RecomentationService(PostgresDbContext postgresContext, ILogger<UserActionsController> logger, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _context = postgresContext ?? throw new ArgumentException(nameof(postgresContext));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpContext = httpContext;
        }

        public async Task<IActionResult> getCars(ControllerBase controller)
        {
            UserEntity user = await Tools.GetUser(_httpContext, _context);
            if (user == null) { return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.INVALID_TOKEN }); }

            double priceDeviation = 0.2;
            double avgPrice = _context.PreferenceInfo.Where(_ => _.Client.UserId == user.UserId).Average(_ => _.Price);
            
            var recCar = _context.CarsInfo.Where(car =>
                (Math.Abs(car.Price - avgPrice) <= avgPrice * priceDeviation) &&
                _context.PreferenceInfo.Any(_ => _.Brand == car.Brand) &&
                _context.PreferenceInfo.Any(_ => _.Model == car.Model)).ToList();

            return controller.Ok(CarPresenter.GetPresenter(recCar));
        }

        public async Task<IActionResult> getDataset(ControllerBase controller)
        {
            List<PreferenceEntity> entities = new List<PreferenceEntity>();
            UserEntity user = await Tools.GetUser(_httpContext, _context);
            if (user == null)
            {
                return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.INVALID_TOKEN });
            }
            if (user is ClientEntity)
            {
                entities = _context.PreferenceInfo.Where(_ => _.Client == (ClientEntity)user).ToList();
            }
            else
            {
                entities = _context.PreferenceInfo.ToList();
            }
            return controller.Ok(RecomentationPresenter.getPresenter(entities));
        }
    }

}
