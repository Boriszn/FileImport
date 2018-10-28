using File.Import.Model;
using TinyCsvParser.Mapping;

namespace File.Import.Mappings
{
    public class CsvUserMapping : CsvMapping<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvUserMapping"/> class.
        /// </summary>
        public CsvUserMapping() 
            : base()
        {
            MapProperty(0, x => x.FirstName);
            MapProperty(1, x => x.LastName);
        }
    }
}
