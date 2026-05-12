using Microsoft.EntityFrameworkCore;
using TransportApp.Helpers;
using TransportApp.Models;

namespace TransportApp.Services;

public class PojazdService
{
    private readonly IDbContextFactory<AppDbContext> _factory;

    public PojazdService(IDbContextFactory<AppDbContext> factory)
    {
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
            using var context = await _factory.CreateDbContextAsync();

            context.Pojazd.Add(pojazd);

            await context.SaveChangesAsync();

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
            using var context = await _factory.CreateDbContextAsync();

            context.Pojazd.Update(pojazd);

            await context.SaveChangesAsync();

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
            using var context = await _factory.CreateDbContextAsync();

            context.Pojazd.Remove(pojazd);

            await context.SaveChangesAsync();

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