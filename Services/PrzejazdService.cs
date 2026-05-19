using Microsoft.EntityFrameworkCore;
using TransportApp.Helpers;
using TransportApp.Models;
using TransportApp.Repositories;
namespace TransportApp.Services;

public class PrzejazdService
{
    private readonly IRepository<Przejazd> _repository;
    private readonly IDbContextFactory<AppDbContext> _factory;

    public PrzejazdService(
        IRepository<Przejazd> repository,
        IDbContextFactory<AppDbContext> factory)
    {
        _repository = repository;
        _factory = factory;
    }

    public async Task<List<Przejazd>> GetAllAsync()
    {
        try
        {
            using var context =
                await _factory.CreateDbContextAsync();

            return await context.Przejazd
                .Include(x => x.IdKierowcaNavigation)
                .Include(x => x.IdPojazdNavigation)
                .Include(x => x.AdresStartuNavigation)
                .Include(x => x.AdresDocelowyNavigation)
                .OrderByDescending(x => x.DataRozpoczecia)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);

            return new List<Przejazd>();
        }
    }

    public async Task<bool> AddAsync(Przejazd przejazd)
    {
        try
        {
            await _repository.AddAsync(przejazd);
            await _repository.SaveAsync();

            return true;
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);

            return false;
        }
    }

    public async Task<bool> UpdateAsync(Przejazd przejazd)
    {
        try
        {
            await _repository.UpdateAsync(przejazd);
            await _repository.SaveAsync();

            return true;
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);

            return false;
        }
    }

    public async Task<bool> DeleteAsync(Przejazd przejazd)
    {
        try
        {
            await _repository.DeleteAsync(przejazd);
            await _repository.SaveAsync();

            return true;
        }
        catch (Exception)
        {
            DialogHelper.Error(
                "Nie można usunąć przejazdu.");

            return false;
        }
    }

    public async Task<decimal> PobierzZyskAsync(
    int idPrzejazdu)
    {
        try
        {
            using var context =
                await _factory.CreateDbContextAsync();

            var conn =
                context.Database.GetDbConnection();

            await conn.OpenAsync();

            using var command =
                conn.CreateCommand();

            command.CommandText =
                """
            SELECT Zysk
            FROM Widok_Zysk_Przejazdu
            WHERE ID_Przejazd = @id
            """;

            var p = command.CreateParameter();

            p.ParameterName = "@id";
            p.Value = idPrzejazdu;

            command.Parameters.Add(p);

            var result =
                await command.ExecuteScalarAsync();

            if (result == null)
                return 0;

            return Convert.ToDecimal(result);
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);

            return 0;
        }
    }
    public async Task<bool> CzyPojazdDostepny(
        int idPojazdu,
        DateTime start,
        DateTime stop)
    {
        try
        {
            using var context =
                await _factory.CreateDbContextAsync();

            var conn = context.Database.GetDbConnection();

            await conn.OpenAsync();

            using var command = conn.CreateCommand();

            command.CommandText =
                "SELECT dbo.fn_CzyPojazdDostepny(@p,@s,@e)";

            var p1 = command.CreateParameter();
            p1.ParameterName = "@p";
            p1.Value = idPojazdu;

            var p2 = command.CreateParameter();
            p2.ParameterName = "@s";
            p2.Value = start;

            var p3 = command.CreateParameter();
            p3.ParameterName = "@e";
            p3.Value = stop;

            command.Parameters.Add(p1);
            command.Parameters.Add(p2);
            command.Parameters.Add(p3);

            var result =
                await command.ExecuteScalarAsync();

            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            DialogHelper.Error(ex.Message);

            return false;
        }
    }
}