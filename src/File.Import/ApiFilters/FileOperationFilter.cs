﻿using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace File.Import.ApiFilters
{
    public class FileOperationFilter : IOperationFilter
    {
        private const string ExportFromCsvFileKey = "exportfromcsvfile";
        private const string ExportFromExcelFileKey = "exportfromexcelfile";

        /// <summary>
        /// Applies the specified operation.
        /// </summary>
        /// <param name="operation">The operation.</param>
        /// <param name="context">The context.</param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.OperationId.ToLower() == ExportFromCsvFileKey ||
                operation.OperationId.ToLower() == ExportFromExcelFileKey)
            {
                // Clear parameters
                operation.Parameters.Clear();
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "File",
                    In = "formData",
                    Description = "File to upload",
                    Required = true,
                    Type = "file"
                });
                operation.Consumes.Add("application/form-data");
            }
        }
    }
}
