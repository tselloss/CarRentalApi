using AutoMapper;
using CarRentalApi.Model;
using CarRentalApi.Responses;
using CarRentalManagment.Controllers;
using CarRentalManagment.PostgresContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Postgres.Context.Entities;
using Recomentation.Info.Interface;
using Recomentation.Info.Response;
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

        public Task<IActionResult> getCars(ControllerBase controller)
        {
            throw new NotImplementedException();
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
