using System.Collections.Generic;
using System.IO;
using CsvHelper;
using File.Import.Mappings;
using File.Import.Model;
using Microsoft.AspNetCore.Http;
using TinyCsvParser;

namespace File.Import.Services
{
    public class CsvProcessingService : ICsvProcessingService
    {
        /// <summary>
        /// Gets the users from CSV v2.
        /// </summary>
        public void GetUsersFromCsvV2()
        {
            CsvParserOptions csvParserOptions = new CsvParserOptions(false, ';');

            var csvParser = new CsvParser<User>(csvParserOptions, new CsvUserMapping());

            // csvParser.ReadFromString()
        }

        /// <summary>
        /// Gets the users from CSV.
        /// </summary>
        /// <param name="file">The file.</param>
        public List<User> GetUsersFromCsv(IFormFile file)
        {
            var users = new List<User>();

            using (var myReader = new StreamReader(file.OpenReadStream()))
            {
                var csvReader = new CsvReader(myReader);

                csvReader.Read();
                csvReader.ReadHeader();
                while (csvReader.Read())
                {
                    users.Add(csvReader.GetRecord<User>());
                }
            }

            return users;
        }

        /// <summary>
        /// Converts the file to byte array.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public static byte[] ConvertFileToByteArray(IFormFile file)
        {
            byte[] fileBytes;

            using (var fileStream = file.OpenReadStream())
            {
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    fileBytes = ms.ToArray();
                }
            }

            return fileBytes;
        }
    }
}
