using System.Collections.Generic;
using File.Import.Model;
using Microsoft.AspNetCore.Http;

namespace File.Import.Services
{
    public interface IExcelProcessingService
    {
        /// <summary>
        /// Gets the users from CSV.
        /// </summary>
        /// <param name="file">The file.</param>
        List<User> GetUsersFromExcel(IFormFile file);
    }
}