using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using File.Import.Model;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace File.Import.Services
{
    public class ExcelProcessingService : IExcelProcessingService
    {
        /// <summary>
        /// Gets the users from Excel.
        /// </summary>
        /// <param name="file">The file.</param>
        public List<User> GetUsersFromExcel(IFormFile file)
        {
            var users = new List<User>();

            try
            {
                var hssfWorkbook = new HSSFWorkbook(file.OpenReadStream());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            XSSFWorkbook xssfWorkbook = new XSSFWorkbook(file.OpenReadStream());

            ISheet sheet = xssfWorkbook.GetSheetAt(0);
            
            // Get Header Row
            IRow headerRow = sheet.GetRow(0); 
            int cellCount = headerRow.LastCellNum;

            // Get Others rows
            for (int rowIndex = (sheet.FirstRowNum + 1); rowIndex <= sheet.LastRowNum; rowIndex++) 
            {
                IRow row = sheet.GetRow(rowIndex);

                if (row == null)
                {
                    continue;
                }

                if (row.Cells.All(d => d.CellType == CellType.Blank))
                {
                    continue;
                }

                users.Add(new User
                {
                    Company = row.GetCell(0).ToString(),
                    FirstName = row.GetCell(1).ToString(),
                    LastName = row.GetCell(2).ToString(),
                    CompanyAddress = row.GetCell(3).ToString()
                });
            }

            return users;
        }
    }
}
