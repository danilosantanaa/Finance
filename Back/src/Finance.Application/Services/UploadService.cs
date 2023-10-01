using Finance.Application.Contratos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;

namespace Finance.Application.Services;
public class UploadService : IUploadService
{
    private readonly IWebHostEnvironment _hostEnvironment;

    public UploadService(IWebHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }

    private void CreateDirectoryIfNotExists(string dir)
    {
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
    }

    public bool Delete(string fileName, string directory_path = "")
    {
        try
        {

            if (string.IsNullOrEmpty(fileName)) return false;

            var directory = Path.Combine(_hostEnvironment.ContentRootPath, "Resources", directory_path);
            CreateDirectoryIfNotExists(directory);

            var filePath = Path.Combine(directory, fileName);


            if (!File.Exists(filePath)) return false;


            File.Delete(filePath);

            return true;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public bool Delete(string[] filesName, string directory_path = "")
    {
        bool allDeleted = true;

        try
        {
            foreach (var file in filesName)
            {
                allDeleted = allDeleted && Delete(file, directory_path);
            }

            return allDeleted;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<string> Save(IFormFile file, string directory_path = "")
    {
        string fileName = new string(
            Path.GetFileNameWithoutExtension(file.FileName)
                .Take(10)
                .ToArray())
            .Replace(' ', '-');

        fileName = $"{fileName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(file.FileName)}";

        var directory = Path.Combine(_hostEnvironment.ContentRootPath, "Resources", directory_path);
        CreateDirectoryIfNotExists(directory);

        var filePath = Path.Combine(directory, fileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return fileName;
    }

    public async Task<string[]> Save(IFormFile[] files, string directory_path = "")
    {
        List<string> filesName = new List<string>();

        foreach (var file in files)
        {
            filesName.Add(await Save(file, directory_path));
        }

        return filesName.ToArray();
    }
}