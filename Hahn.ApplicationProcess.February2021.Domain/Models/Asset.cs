using System;

namespace Hahn.ApplicationProcess.February2021.Domain.Models
{
    public class Asset
    {
        public int ID { get; set; }
        public string AssetName { get; set; }
        public DepartmentType Deparment { get; set; }
        public int DepartmentId { get; set; }
        public string CountryOfDepartment { get; set; }
        public string EMailAdressOfDepartment { get; set; }
        public DateTimeOffset PurchaseDate { get; set; }
        public bool IsBroken { get; set; }
    }
}
