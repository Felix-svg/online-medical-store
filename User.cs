using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMedicalStore
{
    public class User
    {
        private static int s_userID = 1000;
        public string UserID { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Balance { get; set; }

        public User(string userName, int age, string city, string phoneNumber, decimal balance)
        {
            UserID = $"UID{s_userID++}";
            UserName = userName;
            Age = age;
            City = city;
            PhoneNumber = phoneNumber;
            Balance = balance;
        }
    }
}