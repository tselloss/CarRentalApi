using AutoMapper;
using CarRentalApi.Model;
using CarRentalApi.Responses;
using CarRentalManagment.PostgresContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Postgres.Context.Entities;
using Rental.Info.Response;
using RentInfo.Entities;
using Statistics.Info.Interface;
using Statistics.Info.Responses;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Entities;

namespace Statistics.Info.Repository
{
    public class StatisticsService : IStatistics
    {
        private readonly PostgresDbContext _context;
        private readonly IConfiguration _config;
        private readonly ILogger<StatisticsController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public StatisticsService(IConfiguration config, PostgresDbContext postgresContext, ILogger<StatisticsController> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = postgresContext ?? throw new ArgumentException(nameof(postgresContext));
            _config = config ?? throw new ArgumentException(nameof(config));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentException(nameof(logger));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        
        public async Task<IActionResult> GetHistory(ControllerBase controller)
        {
            UserEntity userEntity = await Tools.GetUser(_httpContextAccessor, _context);
            if (userEntity == null) { return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.INVALID_TOKEN }); }
            List<RentalEntity> rents = GetRentsBasedOnUser(userEntity);
            return controller.Ok(RentPresenter.getPresenter(rents));
        }

        public async Task<IActionResult> GetTotalSpendings(ControllerBase controller)
        {
            UserEntity userEntity = await Tools.GetUser(_httpContextAccessor, _context);
            if (userEntity == null) { return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.INVALID_TOKEN }); }
            List<RentalEntity> rents = GetRentsBasedOnUser(userEntity);
            List<TotalSpendingResponse> totalSpendings = new List<TotalSpendingResponse>();
            foreach (RentalEntity rentalEntity in rents)
            {
                if (rentalEntity.Value == 0) {
                    continue;
                }
                int idx = totalSpendings.FindIndex(_ => _.Brand == rentalEntity.Car.Brand);
                if (idx == -1)
                {
                    totalSpendings.Add(new TotalSpendingResponse()
                    {
                        Brand = rentalEntity.Car.Brand,
                        Value = rentalEntity.Value,
                        Months = GetDateTimeDiffInMonths(rentalEntity.DateTo, rentalEntity.DateFrom),
                    });
                }
                else
                {
                    totalSpendings[idx].Value += rentalEntity.Value;
                    totalSpendings[idx].Months += GetDateTimeDiffInMonths(rentalEntity.DateTo, rentalEntity.DateFrom);
                }
            }
            return controller.Ok(totalSpendings);
        }

        public async Task<IActionResult> GetTotalStatistics(ControllerBase controller)
        {
            UserEntity userEntity = await Tools.GetUser(_httpContextAccessor, _context);
            if (userEntity == null) { return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.INVALID_TOKEN }); }
            List<RentalEntity> rents = GetRentsBasedOnUser(userEntity);
            TotalStatisticsResponse totalStatistics = new TotalStatisticsResponse();
            
            Dictionary<string, int> countCars = new Dictionary<string, int>();

            totalStatistics.TotalRent = rents.Count;
            foreach (RentalEntity rentalEntity in rents)
            {
                totalStatistics.TotalMonths += GetDateTimeDiffInMonths(rentalEntity.DateTo, rentalEntity.DateFrom);
                totalStatistics.TotalPriceRents += rentalEntity.Value;
                if (countCars.ContainsKey(rentalEntity.Car.Brand))
                {
                    countCars[rentalEntity.Car.Brand] += 1; 
                }
                else
                {
                    countCars.Add(rentalEntity.Car.Brand, 1);
                }
            }
            totalStatistics.Favourite = countCars.ToList().First(_ => _.Value == countCars.Values.Max()).Key;
            return controller.Ok(totalStatistics);
        }
        private List<RentalEntity> GetRentsBasedOnUser(UserEntity userEntity)
        {
            if (userEntity is ClientEntity)
            {
                return _context.RentalInfo.Where(_ => _.Client.UserId == userEntity.UserId)
                                          .OrderBy(_ => _.DateFrom)
                                          .ToList();
            }
            else
            {
                return _context.RentalInfo.OrderBy(_ => _.DateFrom).ToList();
            }
        }
        public DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
        private int GetDateTimeDiffInMonths(long date1Unix, long date2Unix)
        {
            DateTime date1 = UnixTimeStampToDateTime(date1Unix);
            DateTime date2 = UnixTimeStampToDateTime(date2Unix);
            return ((date1.Year - date2.Year) * 12) + date1.Month - date2.Month;
        }

        public async Task<IActionResult> GetUserSpendings(ControllerBase controller)
        {
            AdminEntity adminEntity = (AdminEntity) await Tools.GetUser(_httpContextAccessor, _context);
            if (adminEntity == null) { return controller.BadRequest(new ErrorResponse() { message = ErrorMessages.INVALID_TOKEN }); }
            
            Dictionary<string,int> userSpengings = new Dictionary<string,int>();
            foreach (var rent in _context.RentalInfo.ToList())
            {
                if (userSpengings.ContainsKey(rent.Client.Username))
                {
                    userSpengings[rent.Client.Username] += rent.Value;
                }
                else
                {
                    userSpengings.Add(rent.Client.Username, rent.Value);
                }
            }

            List<UserSpendingsResponse> userSpendingsResponses = new List<UserSpendingsResponse>();
            foreach (var username in userSpengings.Keys.ToList())
            {
                userSpendingsResponses.Add(new UserSpendingsResponse() { Username = username, Value = userSpengings[username] });
            }
            return controller.Ok(userSpendingsResponses);
        }
    }
}
