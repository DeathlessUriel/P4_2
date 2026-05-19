using Microsoft.EntityFrameworkCore;
using TransportApp.Helpers;
using TransportApp.Models;
using TransportApp.Repositories;

namespace TransportApp.Services;

public class KosztService
{
    private readonly IRepository<Koszty> _repository;   
    private readonly IDbContextFactory<AppDbContext> _factory;

    public KosztService(
        IRepository<Koszty> repository,
        IDbContextFactory<AppDbContext> factory)
    {
        _repository = repository;
        _factory = factory;
    }

    public async Task<List<Koszty>> GetAllAsync()
    {
        try
        {
            using var context =
                await _factory.CreateDbContextAsync();

            return await context.Koszty
                .Include(x => x.IdPrzejazdNavigation)
                .Include(x => x.IdKursuNavigation)
                .ThenInclude(x => x.IdValutyNavigation)
                .OrderByDescending(x => x.IdKoszty)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);

            return new List<Koszty>();
        }
    }

    public async Task<bool> AddAsync(Koszty koszt)
    {
        try
        {
            await _repository.AddAsync(koszt);
            await _repository.SaveAsync();

           

            return true;
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);

            return false;
        }
    }

    public async Task<bool> UpdateAsync(Koszty koszt)
    {
        try
        {
            await _repository.UpdateAsync(koszt);
            await _repository.SaveAsync();

            return true;
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);

            return false;
        }
    }

    public async Task<bool> DeleteAsync(Koszty koszt)
    {
        try
        {
            await _repository.DeleteAsync(koszt);
            await _repository.SaveAsync();

            return true;
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);

            return false;
        }
    }
}