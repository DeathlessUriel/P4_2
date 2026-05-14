using Microsoft.EntityFrameworkCore;
using TransportApp.Helpers;
using TransportApp.Models;

namespace TransportApp.Services;

public class KosztService
{
    private readonly IDbContextFactory<AppDbContext> _factory;

    public KosztService(
        IDbContextFactory<AppDbContext> factory)
    {
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
            using var context =
                await _factory.CreateDbContextAsync();

            context.Koszty.Add(koszt);

            await context.SaveChangesAsync();

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
            using var context =
                await _factory.CreateDbContextAsync();

            context.Koszty.Update(koszt);

            await context.SaveChangesAsync();

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
            using var context =
                await _factory.CreateDbContextAsync();

            context.Koszty.Remove(koszt);

            await context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);

            return false;
        }
    }
}