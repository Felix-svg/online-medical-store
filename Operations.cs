using System;
using System.Collections.Generic;

namespace OnlineMedicalStore
{
    public class Operations
    {
        private static List<User> users = [];
        private static List<Medicine> medicines = [];
        private static List<Order> orders = [];
        private static User currentLoggedInUser;

        public static void MainMenu()
        {
            bool flag = true;

            do
            {
                Console.WriteLine("Choose an option");
                Console.WriteLine("1. User Registration\n2. User Login\n3. Exit");

                string userChoice = Console.ReadLine().Trim();
                switch (userChoice)
                {
                    case "1":
                        UserRegistration();
                        break;
                    case "2":
                        UserLogin();
                        break;
                    case "3":
                        flag = false;
                        Console.WriteLine("Goodbye");
                        break;
                }
            } while (flag);
        }

        public static void UserRegistration()
        {
            Console.WriteLine("Enter your name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your age");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter your city");
            string city = Console.ReadLine();
            Console.WriteLine("Enter your phone number");
            string phoneNumber = Console.ReadLine();
            Console.WriteLine("Enter your balance");
            decimal balance = decimal.Parse(Console.ReadLine());

            User user = new(name, age, city, phoneNumber, balance);
            Console.WriteLine($"Your ID is {user.UserID}");
            users.Add(user);
        }

        public static void UserLogin()
        {
            Console.WriteLine("Enter User ID");
            string userID = Console.ReadLine().ToUpper().Trim();

            bool flag = true;
            foreach (User user in users)
            {
                if (userID == user.UserID)
                {
                    flag = false;
                    currentLoggedInUser = user;
                    break;
                }
            }
            if (flag)
            {
                Console.WriteLine("Invalid User ID. Please enter a valid one");
                UserLogin();
            }
            SubMenu();
        }

        public static void SubMenu()
        {
            Console.WriteLine("Choose an option to continue");
            Console.WriteLine("a. Show medicine list\nb. Purchase medicine\nc. Cancel purchase\nd. Show purchase history\ne. Recharge\nf. Show wallet balance\ng. Exit");

            string userChoice = Console.ReadLine().ToLower().Trim();
            switch (userChoice)
            {
                case "a":
                    ShowMedicineList();
                    break;
                case "b":
                    PurchaseMedicine();
                    break;
                case "c":
                    CancelPurchase();
                    break;
                case "d":
                    ShowPurchaseHistory();
                    break;
                case "e":
                    Recharge();
                    break;
                case "f":
                    ShowWalletBalance();
                    break;
                case "g":
                    MainMenu();
                    break;
            }
        }

        public static void ShowMedicineList()
        {
            bool flag = true;
            foreach (Medicine medicine in medicines)
            {
                flag = false;
                Console.Write($"Medicine ID: {medicine.MedicineID} | Medicine Name: {medicine.MedicineName} | In Stock: {medicine.AvailableCount} | Price: {medicine.Price} | Date of Expiry: {medicine.DateOfExpiry}\n");
            }
            if (flag)
            {
                Console.WriteLine("No medicine found");
            }
            SubMenu();
        }

        public static void PurchaseMedicine()
        {
            Console.WriteLine("Enter the ID of the medicine you want to purchase");
            string medicineID = Console.ReadLine().ToUpper().Trim();
            Console.WriteLine("Enter the quantity you want to purchase");
            int quantity = int.Parse(Console.ReadLine().Trim());

            bool flag = true;
            foreach (Medicine medicine in medicines)
            {
                if (medicineID == medicine.MedicineID && medicine.AvailableCount >= quantity && DateTime.Now < medicine.DateOfExpiry)
                {
                    flag = false;
                    foreach (User user in users)
                    {
                        if (user.UserID == currentLoggedInUser.UserID)
                        {
                            int totalPrice = medicine.Price * quantity;
                            if (user.Balance >= totalPrice)
                            {
                                medicine.AvailableCount -= quantity;
                                user.Balance -= totalPrice;
                                Order order = new(user.UserID, medicine.MedicineID, quantity, totalPrice, DateTime.Now, OrderStatus.Purchased);
                                orders.Add(order);
                                Console.WriteLine("Medicine was purchased successfull");
                            }
                            else
                            {
                                Console.WriteLine("Insufficient funds. Please recharge to continue.");
                            }
                        }
                    }
                }
            }
            if (flag)
            {
                Console.WriteLine("Medicine is not available");
            }
            SubMenu();
        }

        public static void CancelPurchase()
        {
            bool flag = true;
            foreach (User user in users)
            {
                if (user.UserID == currentLoggedInUser.UserID)
                {
                    flag = false;
                    foreach (Order order in orders)
                    {
                        if (order.UserID == user.UserID && order.OrderStatus == OrderStatus.Purchased)
                        {
                            Console.Write($"Order ID: {order.OrderID} | User ID: {order.UserID} | Medicine ID: {order.MedicineID} | Total Price: {order.TotalPrice} | Order Date: {order.OrderDate} | Quantity: {order.Quantity} | Status: {order.OrderStatus}\n");
                        }
                    }

                    Console.WriteLine("Enter the ID of the order you want to cancel");
                    string orderID = Console.ReadLine().ToUpper().Trim();

                    bool flag1 = true;
                    foreach (Order order1 in orders)
                    {
                        if (orderID == order1.OrderID && order1.OrderStatus == OrderStatus.Purchased)
                        {
                            flag1 = false;
                            bool flag2 = true;
                            foreach (Medicine medicine in medicines)
                            {
                                if (medicine.MedicineID == order1.MedicineID)
                                {
                                    flag2 = false;
                                    medicine.AvailableCount += order1.Quantity;
                                    user.Balance += order1.TotalPrice;
                                    order1.OrderStatus = OrderStatus.Cancelled;
                                    Console.WriteLine($"Order {order1.OrderID} was cancelled successfully");
                                }
                            }
                            if (flag2)
                            {
                                Console.WriteLine("Medicine not found");
                            }
                        }
                    }
                    if (flag1)
                    {
                        Console.WriteLine("Order not found");
                    }
                }
            }
            if (flag)
            {
                Console.WriteLine("Invalid User ID");
            }
            SubMenu();
        }

        public static void ShowPurchaseHistory()
        {
            bool flag = true;
            foreach (User user in users)
            {
                if (user.UserID == currentLoggedInUser.UserID)
                {
                    flag = false;

                    bool flag1 = true;
                    foreach (Order order in orders)
                    {
                        if (order.UserID == currentLoggedInUser.UserID)
                        {
                            flag1 = false;
                            Console.Write($"Order ID: {order.OrderID} | User ID: {order.UserID} | Medicine ID: {order.MedicineID} | Total Price: {order.TotalPrice} | Order Date: {order.OrderDate} | Quantity: {order.Quantity} | Status: {order.OrderStatus}\n");
                        }
                    }
                    if (flag1)
                    {
                        Console.WriteLine("Purchase History not found");
                    }
                }
            }
            if (flag)
            {
                Console.WriteLine("Invalid User ID");
                MainMenu();
            }
            SubMenu();
        }

        public static void Recharge()
        {
            bool flag = true;
            foreach (User user in users)
            {
                if (user.UserID == currentLoggedInUser.UserID)
                {
                    flag = false;
                    Console.WriteLine("Enter recharge amount");
                    int amount = int.Parse(Console.ReadLine().Trim());
                    user.Balance += amount;
                }
            }
            if (flag)
            {
                Console.WriteLine("Reecharge failed");
            }
            SubMenu();
        }

        public static void ShowWalletBalance()
        {
            bool flag = true;
            foreach (User user in users)
            {
                if (user.UserID == currentLoggedInUser.UserID)
                {
                    flag = false;
                    Console.WriteLine($"Your wallet balance is: {string.Format("{0:C}", user.Balance)}");
                }
            }
            if (flag)
            {
                Console.WriteLine("Invalid user ID");
            }
            SubMenu();
        }

        public static void DefaultData()
        {
            users.Add(new("John", 33, "Knowhere", "9877774440", 400));
            users.Add(new("Jane", 33, "Vomir", "9888475757", 500));

            medicines.Add(new("Paracentamol", 40, 5, DateTime.Parse("30/12/2024")));
            medicines.Add(new("Calpol", 10, 5, DateTime.Parse("30/11/2024")));
            medicines.Add(new("Gelucil", 3, 40, DateTime.Parse("30/04/2025")));
            medicines.Add(new("Metrogel", 5, 50, DateTime.Parse("30/12/2024")));
            medicines.Add(new("Povidin Iodin", 10, 50, DateTime.Parse("30/10/2026")));


            orders.Add(new(users[0].UserID, medicines[0].MedicineID, 3, 15, DateTime.Parse("15/04/2024"), OrderStatus.Purchased));
            orders.Add(new(users[0].UserID, medicines[1].MedicineID, 2, 10, DateTime.Parse("15/05/2024"), OrderStatus.Cancelled));
            orders.Add(new(users[0].UserID, medicines[3].MedicineID, 2, 100, DateTime.Parse("15/06/2024"), OrderStatus.Purchased));
            orders.Add(new(users[1].UserID, medicines[2].MedicineID, 3, 120, DateTime.Parse("15/07/2024"), OrderStatus.Cancelled));
            orders.Add(new(users[1].UserID, medicines[4].MedicineID, 5, 250, DateTime.Parse("15/08/2024"), OrderStatus.Purchased));
            orders.Add(new(users[1].UserID, medicines[2].MedicineID, 3, 15, DateTime.Parse("15/09/2024"), OrderStatus.Purchased));
        }
    }
}