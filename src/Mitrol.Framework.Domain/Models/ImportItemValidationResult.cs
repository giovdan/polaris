namespace Mitrol.Framework.Domain.Models
{
    public class ImportItemValidationResult
    {
        public int Key { get; set; }
        public bool CanImport { get; set; }
        public string ValidationError { get; set; }

        public ImportItemValidationResult(int key)
        {
            Key = key;
        }
    }
}