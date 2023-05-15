using SignatureEmailParser.BusinessLogic.Constants;
using SignatureEmailParser.BusinessLogic.Helpers.Interfaces;
using SignatureEmailParser.EFCore.Entities;
using SignatureEmailParser.EFCore.Enums;
using SignatureEmailParser.EFCore.Respository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SignatureEmailParser.BusinessLogic.Helpers
{
    public class LogHelper : ILogHelper
    {
        private readonly ILogRepository _logRepository;
        private readonly ILogger _logger;
        public LogHelper(ILogRepository logRepository, ILogger<LogHelper> logger)
        {
            _logRepository = logRepository;
            _logger = logger;
        }

        public async Task LogSuccessRequest(IHeaderDictionary headers, string source, string message = null)
        {
            var log = GenerateLog(headers, source, LogStatusType.Normal, message);

            if (log is null)
            {
                return;
            }

            _logger.LogInformation(JsonConvert.SerializeObject(log));

            await _logRepository.Create(log);
        }

        public async Task LogErrorRequest(IHeaderDictionary headers, string source, string message = null)
        {
            var log = GenerateLog(headers, source, LogStatusType.Error, message);

            if (log is null)
            {
                return;
            }

            _logger.LogInformation(JsonConvert.SerializeObject(log));

            await _logRepository.Create(log);
        }

        public async Task LogSevereRequest(IHeaderDictionary headers, string source, string message = null)
        {
            var log = GenerateLog(headers, source, LogStatusType.Severe, message);

            if (log is null)
            {
                return;
            }

            _logger.LogInformation(JsonConvert.SerializeObject(log));

            await _logRepository.Create(log);
        }

        public async Task LogSuccessRequest(IQueryCollection queryCollection, string source, string message = null)
        {
            var log = GenerateLog(queryCollection, source, LogStatusType.Normal, message);

            if (log is null)
            {
                return;
            }

            _logger.LogInformation(JsonConvert.SerializeObject(log));

            await _logRepository.Create(log);
        }

        public async Task LogErrorRequest(IQueryCollection queryCollection, string source, string message = null)
        {
            var log = GenerateLog(queryCollection, source, LogStatusType.Error, message);

            if (log is null)
            {
                return;
            }

            _logger.LogInformation(JsonConvert.SerializeObject(log));

            await _logRepository.Create(log);
        }

        public async Task LogServerRequest(IQueryCollection queryCollection, string source, string message = null)
        {
            var log = GenerateLog(queryCollection, source, LogStatusType.Severe, message);

            if (log is null)
            {
                return;
            }

            _logger.LogInformation(JsonConvert.SerializeObject(log));

            await _logRepository.Create(log);
        }

        public async Task<long> GetRequestsCountByLicenseId(string licenseId, string source = null)
        {
            long count = await _logRepository.GetRequestsCountByLicenseId(licenseId, source);

            return count;
        }

        public async Task<long> GetRequestsCountByDate(DateTime date, string licenseId, string source = null)
        {
            long count = await _logRepository.GetRequestsCountByDate(date, licenseId, source);

            return count;
        }

        private Log GenerateLog(IHeaderDictionary headers, string source, LogStatusType status, string message)
        {
            headers.TryGetValue("email", out StringValues emailValues);
            headers.TryGetValue("licenseId", out StringValues licenseValues);

            string licenseId = licenseValues.FirstOrDefault();
            string email = emailValues.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(licenseId) || string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            var log = new Log
            {
                Email = email,
                LicenseId = licenseId,
                Source = source,
                Status = status,
                Message = message
            };

            return log;
        }

        private Log GenerateLog(IQueryCollection queryCollection, string source, LogStatusType status, string message)
        {
            string licenseId = queryCollection[SettingConstant.LICENSE_ID_URL_KEY];
            string email = queryCollection[SettingConstant.EMAIL_URL_KEY];

            if (string.IsNullOrWhiteSpace(licenseId) || string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            var log = new Log
            {
                Email = email,
                LicenseId = licenseId,
                Source = source,
                Status = status,
                Message = message
            };

            return log;
        }
    }
}
