using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using TransportApp.Models;
using System.IO;
namespace TransportApp.Services;

public class PojazdZdjeciaService
{
    private readonly IDbContextFactory<AppDbContext> _factory;

    public PojazdZdjeciaService(
        IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;
    }

    public async Task DodajZdjecieAsync(int idPojazdu)
    {
        var dialog = new OpenFileDialog();

        dialog.Filter =
            "Obrazy|*.jpg;*.jpeg;*.png";

        if (dialog.ShowDialog() != true)
            return;

        var imagesDir =
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Images");

        Directory.CreateDirectory(imagesDir);

        var fileName =
            Guid.NewGuid() +
            Path.GetExtension(dialog.FileName);

        var targetPath =
            Path.Combine(imagesDir, fileName);

        File.Copy(
            dialog.FileName,
            targetPath);

        using var context =
            await _factory.CreateDbContextAsync();

        context.ZdjeciaPojazdu.Add(
            new ZdjeciaPojazdu
            {
                IdPojazd = idPojazdu,
                Sciezka = targetPath
            });

        await context.SaveChangesAsync();
    }

    public async Task<List<ZdjeciaPojazdu>>
        GetZdjeciaAsync(int idPojazdu)
    {
        using var context =
            await _factory.CreateDbContextAsync();

        return await context.ZdjeciaPojazdu
            .Where(x => x.IdPojazd == idPojazdu)
            .ToListAsync();
    }
}