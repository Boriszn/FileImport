using System.Collections.Generic;
using System.Linq;
using File.Import.Model;
using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.POIFS.FileSystem;
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

            // 1. get excel sheet
            var sheet = GetSheet(file);
            
            // get cell count (also get Header Row) 
            int cellCount = GetCellCount(sheet);

            // 2. get excel sheet rows (exclude header row)
            for (int rowIndex = sheet.FirstRowNum + 1; rowIndex <= sheet.LastRowNum; rowIndex++) 
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

        private static int GetCellCount(ISheet sheet)
        {
            IRow headerRow = sheet.GetRow(0);
            return headerRow.LastCellNum;
        }

        private static ISheet GetSheet(IFormFile file)
        {
            ISheet sheet;

            try
            {
                // the part of POI that deals with OLE2 Office Documents. 
                var hssfWorkbook = new HSSFWorkbook(file.OpenReadStream());
                sheet = hssfWorkbook.GetSheetAt(0);
            }
            catch (OfficeXmlFileException ex)
            {
                // The supplied data for the Office 2007+ XML.  
                var xssfWorkbook = new XSSFWorkbook(file.OpenReadStream());
                sheet = xssfWorkbook.GetSheetAt(0);
            }

            return sheet;
        }
    }
}
