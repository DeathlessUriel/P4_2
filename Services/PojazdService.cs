using Microsoft.EntityFrameworkCore;
using TransportApp.Helpers;
using TransportApp.Models;
using TransportApp.Repositories;

namespace TransportApp.Services;

public class PojazdService
{
    private readonly IRepository<Pojazd> _repository;
    private readonly IDbContextFactory<AppDbContext> _factory;

    public PojazdService(IRepository<Pojazd> repository, 
        IDbContextFactory<AppDbContext> factory)
    {
        _repository = repository;
        _factory = factory;
    }

    public async Task<List<Pojazd>> GetAllAsync()
    {
        try
        {
            using var context = await _factory.CreateDbContextAsync();

            return await context.Pojazd
                .Include(x => x.IdModeluNavigation)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);
            return new List<Pojazd>();
        }
    }

    public async Task<bool> AddAsync(Pojazd pojazd)
    {
        try
        {
            await _repository.AddAsync(pojazd);

            await _repository.SaveAsync();

            return true;
        }
        catch (DbUpdateException)
        {
            DialogHelper.Error(
                "Nie udało się dodać pojazdu.\n" +
                "Sprawdź dane.");

            return false;
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);
            return false;
        }
    }

    public async Task<bool> UpdateAsync(Pojazd pojazd)
    {
        try
        {
            await _repository.UpdateAsync(pojazd);

            await _repository.SaveAsync();

            return true;
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Pojazd pojazd)
    {
        try
        {
            await _repository.DeleteAsync(pojazd);

            await _repository.SaveAsync();

            return true;
        }
        catch (Exception ex)
        {
            DialogHelper.Error(
                "Nie można usunąć pojazdu.\n" +
                "Może być używany w przejazdach.");

            return false;
        }
    }
}