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

            // var hssfwb = new HSSFWorkbook(file.OpenReadStream());

            var xssfWorkbook = new XSSFWorkbook(file.OpenReadStream());

            ISheet sheet = xssfWorkbook.GetSheetAt(0);

            IRow headerRow = sheet.GetRow(0); //Get Header Row
            int cellCount = headerRow.LastCellNum;

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
            {
                IRow row = sheet.GetRow(i);

                if (row == null)
                {
                    continue;
                }

                if (row.Cells.All(d => d.CellType == CellType.Blank))
                {
                    continue;
                }

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        // 
                        string cell = row.GetCell(j).ToString();
                    }
                }
            }

            return users;
        }
    }
}
