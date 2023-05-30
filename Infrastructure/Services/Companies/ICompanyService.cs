using Infrastructure.Models.Companies;

namespace Infrastructure.Services.Companies;

public interface ICompanyService
{
    Task<Company?> GetCompanyAsync(Guid id);
    
    Task<IEnumerable<Company>> GetCompaniesAsync(int page, int pageSize);
    
    Task<int> GetCompaniesCountAsync();
    
    Task<bool> CreateCompanyAsync(Company company);
    
    Task<bool> UpdateCompanyAsync(Company companyToUpdate);
    
    Task<bool> DeleteCompanyAsync(Guid id);
}