using System.Collections.Generic;

namespace Hahn.ApplicationProcess.February2021.Domain.Models
{
    public class DepartmentType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Asset> Assets { get; set; }
    }
}
