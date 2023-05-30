using Infrastructure.Data;
using Infrastructure.Models.Companies;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.Companies;

public class CompanyService : ICompanyService
{
    private readonly DataContext _dataContext;

    public CompanyService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<Company?> GetCompanyAsync(Guid id)
    {
        return await _dataContext.Companies.FirstOrDefaultAsync(company => company.Id == id);
    }

    public async Task<IEnumerable<Company>> GetCompaniesAsync(int page, int pageSize)
    {
        return await _dataContext.Companies.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetCompaniesCountAsync()
    {
        return await _dataContext.Companies.CountAsync();
    }

    public async Task<bool> CreateCompanyAsync(Company company)
    {
        await _dataContext.Companies.AddAsync(company);
        var created = await _dataContext.SaveChangesAsync();
        return created > 0;
    }

    public async Task<bool> UpdateCompanyAsync(Company companyToUpdate)
    {
        _dataContext.Companies.Update(companyToUpdate);
        var updated = await _dataContext.SaveChangesAsync();
        return updated > 0;
    }

    public async Task<bool> DeleteCompanyAsync(Guid id)
    {
        var company = await GetCompanyAsync(id);
        if (company is null)
        {
            return false;
        }
        
        _dataContext.Companies.Remove(company);
        var deleted = await _dataContext.SaveChangesAsync();
        return deleted > 0;
    }
}