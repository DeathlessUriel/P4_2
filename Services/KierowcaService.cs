using Microsoft.EntityFrameworkCore;
using TransportApp.Helpers;
using TransportApp.Models;
using TransportApp.Repositories;

namespace TransportApp.Services;

public class KierowcaService
{
    private readonly IRepository<Kierowca> _repository;
    private readonly IDbContextFactory<AppDbContext> _factory;

    public KierowcaService(
        IRepository<Kierowca> repository,
        IDbContextFactory<AppDbContext> factory)
    {
        _repository = repository;
        _factory = factory;
    }

    public async Task<List<Kierowca>> GetAllAsync()
    {
        try
        {
            using var context =
                await _factory.CreateDbContextAsync();

            return await context.Kierowca
                .OrderBy(x => x.Nazwisko)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);

            return new List<Kierowca>();
        }
    }

    public async Task<bool> AddAsync(Kierowca kierowca)
    {
        try
        {
            await _repository.AddAsync(kierowca);
            await _repository.SaveAsync();

            return true;
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);

            return false;
        }
    }

    public async Task<bool> UpdateAsync(Kierowca kierowca)
    {
        try
        {

            await _repository.UpdateAsync(kierowca);
            await _repository.SaveAsync();


            return true;
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);

            return false;
        }
    }

    public async Task<bool> DeleteAsync(Kierowca kierowca)
    {
        try
        {
            await _repository.DeleteAsync(kierowca);
            await _repository.SaveAsync();

            return true;
        }
        catch (Exception)
        {
            DialogHelper.Error(
                "Nie można usunąć kierowcy.\n" +
                "Może być używany w przejazdach.");

            return false;
        }
    }
}