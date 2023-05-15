using SignatureEmailParser.BusinessLogic.Services.Interfaces;
using SignatureEmailParser.EFCore.Entities;
using SignatureEmailParser.EFCore.Respository.Interfaces;
using SignatureEmailParser.Models.EqualityComparers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SignatureEmailParser.BusinessLogic.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<List<Company>> GetByFilterAsync(string companyName)
        {
           return await _companyRepository.GetByNames(new string[] { companyName });
        }
    }
}
