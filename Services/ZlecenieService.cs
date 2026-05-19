using Microsoft.EntityFrameworkCore;
using TransportApp.Helpers;
using TransportApp.Models;
using TransportApp.Repositories;
namespace TransportApp.Services;

public class ZlecenieService
{
    private readonly IRepository<Zlecenie> _repository;
    private readonly IDbContextFactory<AppDbContext> _factory;

    public ZlecenieService(
        IRepository<Zlecenie> repository,
        IDbContextFactory<AppDbContext> factory)
    {
        _repository = repository;
        _factory = factory;
    }

    public async Task<List<Zlecenie>> GetAllAsync()
    {
        try
        {
            using var context =
                await _factory.CreateDbContextAsync();

            return await context.Zlecenie
                .Include(x => x.AdresRozladunkuNavigation)
                .Include(x => x.AdresZaladunkuNavigation)
                .OrderByDescending(x => x.DataPrzyjecia)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);

            return new List<Zlecenie>();
        }
    }

    public async Task<bool> AddAsync(Zlecenie zlecenie)
    {
        try
        {
            await _repository.AddAsync(zlecenie);
            await _repository.SaveAsync();

            return true;
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);

            return false;
        }
    }

    public async Task<bool> UpdateAsync(Zlecenie zlecenie)
    {
        try
        {
            await _repository.UpdateAsync(zlecenie);
            await _repository.SaveAsync();

            return true;
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);

            return false;
        }
    }

    public async Task<bool> DeleteAsync(Zlecenie zlecenie)
    {
        try
        {
            await _repository.DeleteAsync(zlecenie);
            await _repository.SaveAsync();

            return true;
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);

            return false;
        }
    }

    public async Task<decimal> GetKosztAsync(int id)
    {
        try
        {
            using var context =
                await _factory.CreateDbContextAsync();

            var conn = context.Database.GetDbConnection();

            await conn.OpenAsync();

            using var command = conn.CreateCommand();

            command.CommandText =
                "SELECT dbo.fn_KosztZlecenia(@id)";

            var param = command.CreateParameter();

            param.ParameterName = "@id";
            param.Value = id;

            command.Parameters.Add(param);

            var result = await command.ExecuteScalarAsync();

            if (result == DBNull.Value || result == null)
                return 0;

            return Convert.ToDecimal(result);
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);

            return 0;
        }
    }
}