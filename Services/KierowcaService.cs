using Microsoft.EntityFrameworkCore;
using TransportApp.Helpers;
using TransportApp.Models;

namespace TransportApp.Services;

public class KierowcaService
{
    private readonly IDbContextFactory<AppDbContext> _factory;

    public KierowcaService(
        IDbContextFactory<AppDbContext> factory)
    {
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
            using var context =
                await _factory.CreateDbContextAsync();

            context.Kierowca.Add(kierowca);

            await context.SaveChangesAsync();

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
            using var context =
                await _factory.CreateDbContextAsync();

            context.Kierowca.Update(kierowca);

            await context.SaveChangesAsync();

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
            using var context =
                await _factory.CreateDbContextAsync();

            context.Kierowca.Remove(kierowca);

            await context.SaveChangesAsync();

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