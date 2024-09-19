using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMedicalStore
{
    public class Medicine
    {
        private static int s_medicineID = 2000;
        public string MedicineID { get; set; }
        public string MedicineName { get; set; }
        public int AvailableCount { get; set; }
        public int Price { get; set; }
        public DateTime DateOfExpiry { get; set; }

        public Medicine(string medicineName, int availableCount, int price, DateTime dateOfExpiry)
        {
            MedicineID = $"MD{s_medicineID++}";
            MedicineName = medicineName;
            AvailableCount = availableCount;
            Price = price;
            DateOfExpiry = dateOfExpiry;
        }
    }
}