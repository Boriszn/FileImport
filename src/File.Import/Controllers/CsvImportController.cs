using System.Threading.Tasks;
using File.Import.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace File.Import.Controllers
{
    [Route("api/file/import")]
    public class CsvImportController : ControllerBase
    {
        private readonly ICsvProcessingService csvProcessingService;
        private readonly IExcelProcessingService excelProcessingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvImportController" /> class.
        /// </summary>
        /// <param name="csvProcessingService">The CSV processing service.</param>
        /// <param name="excelProcessingService">The excel processing service.</param>
        public CsvImportController(
            ICsvProcessingService csvProcessingService,
            IExcelProcessingService excelProcessingService)
        {
            this.csvProcessingService = csvProcessingService;
            this.excelProcessingService = excelProcessingService;
        }

        /// <summary>
        /// Exports from CSV file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation("ExportFromCsvFile")]
        public async Task<IActionResult> Post([FromForm] IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return new BadRequestObjectResult("File cannot be empty.");
            }
            
            return new ObjectResult(this.csvProcessingService.GetUsersFromCsv(file));
        }

        /// <summary>
        /// Exports from excel file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        [HttpPost("excel")]
        [SwaggerOperation("ExportFromExcelFile")]
        public async Task<IActionResult> ExportFromExcelFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return new BadRequestObjectResult("File cannot be empty.");
            }

            return new ObjectResult(this.excelProcessingService.GetUsersFromExcel(file));
        }
    }
}
