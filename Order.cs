using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMedicalStore
{
    public enum OrderStatus
    {
        Purchased,
        Cancelled
    }
    public class Order
    {
        private static int s_orderID = 3000;
        public string OrderID { get; set; }
        public string UserID { get; set; }
        public string MedicineID { get; set; }
        public int TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public Order(string userID, string medicineID, int quantity, int totalPrice, DateTime orderDate, OrderStatus orderStatus)
        {
            OrderID = $"OID{s_orderID++}";
            UserID = userID;
            MedicineID = medicineID;
            Quantity = quantity;
            TotalPrice = totalPrice;
            OrderDate = orderDate;
            OrderStatus = orderStatus;
        }
    }
}