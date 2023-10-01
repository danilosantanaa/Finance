using Microsoft.AspNetCore.Http;

namespace Finance.Application.Contratos;

public interface IUploadService
{
    bool Delete(string fileName, string directory_path = "");
    bool Delete(string[] filesName, string directory_path = "");
    Task<string> Save(IFormFile file, string directory_path = "");
    Task<string[]> Save(IFormFile[] file, string directory_path = "");
}