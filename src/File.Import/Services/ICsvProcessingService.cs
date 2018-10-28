using System.Collections.Generic;
using File.Import.Model;
using Microsoft.AspNetCore.Http;

namespace File.Import.Services
{
    public interface ICsvProcessingService
    {
        /// <summary>
        /// Gets the users from CSV v2.
        /// </summary>
        void GetUsersFromCsvV2();

        /// <summary>
        /// Gets the users from CSV.
        /// </summary>
        /// <param name="file">The file.</param>
        List<User> GetUsersFromCsv(IFormFile file);
    }
}