using finproject.DAL;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace finproject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            while (true)
            {
                Console.WriteLine("\nMcDonald's");
                Console.WriteLine("1 — Categories");
                Console.WriteLine("2 — Menu Items");
                Console.WriteLine("3 — Customers");
                Console.WriteLine("4 — Employees");
                Console.WriteLine("5 — Orders");
                Console.WriteLine("6 — Exit");
                Console.WriteLine("7 — Thread");
                Console.WriteLine("8 — New process");
                Console.WriteLine("9 — Task");

                Console.Write("Choose: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": CategoryMenu(); break;
                    case "2": MenuItemsMenu(); break;
                    case "3": CustomerMenu(); break;
                    case "4": EmployeeMenu(); break;
                    case "5": OrderMenu(); break;
                    case "7": MultiThreadReport(); break;
                    case "8": RunNewProcess(); break;
                    case "9": TaskReport(); break;
                    case "6": return;
                }
            }
        }
        static void MultiThreadReport()
        {
            Console.WriteLine("\n--- thread ---");

            var catRepo = new CategoryRepository();
            var custRepo = new CustomerRepository();
            var empRepo = new EmployeeRepository();

            Thread t1 = new Thread(() =>
                Console.WriteLine($"Categories: {catRepo.GetAll().Count}")
            );

            Thread t2 = new Thread(() =>
                Console.WriteLine($"Customers: {custRepo.GetAll().Count}")
            );

            Thread t3 = new Thread(() =>
                Console.WriteLine($"Employees: {empRepo.GetAll().Count}")
            );

            t1.Start();
            t2.Start();
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();
        }

        static void TaskReport()
        {
            Console.WriteLine("\n--- tasku ---");

            var catRepo = new CategoryRepository();
            var custRepo = new CustomerRepository();
            var empRepo = new EmployeeRepository();

            var task1 = Task.Run(() => catRepo.GetAll().Count);
            var task2 = Task.Run(() => custRepo.GetAll().Count);
            var task3 = Task.Run(() => empRepo.GetAll().Count);

            Task.WaitAll(task1, task2, task3);

            Console.WriteLine($"Categories: {task1.Result}");
            Console.WriteLine($"Customers: {task2.Result}");
            Console.WriteLine($"Employees: {task3.Result}");
        }

        static void RunNewProcess()
        {
            string exePath = Environment.ProcessPath;

            Process.Start(new ProcessStartInfo
            {
                FileName = exePath,
                UseShellExecute = true
            });

            Console.WriteLine("Program started in new process!");
        }

        static void CategoryMenu()
        {
            var repo = new CategoryRepository();

            while (true)
            {
                Console.WriteLine("\n--- CATEGORY MENU ---");
                Console.WriteLine("1 — Add");
                Console.WriteLine("2 — Update");
                Console.WriteLine("3 — Delete");
                Console.WriteLine("4 — Show all");
                Console.WriteLine("5 — Back");

                Console.Write("Choose: ");
                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Taste: ");
                    string taste = Console.ReadLine();

                    repo.Add(new Category { Name = name, Taste = taste });
                    Console.WriteLine("Category added successfully.");
                }
                else if (choice == "2")
                {
                    Console.Write("ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id)) continue;

                    var existing = repo.GetById(id);
                    if (existing == null) { Console.WriteLine("Category not found!"); continue; }

                    Console.Write($"New Name (current: {existing.Name}): ");
                    string newName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newName)) existing.Name = newName;

                    Console.Write($"New Taste (current: {existing.Taste}): ");
                    string newTaste = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(newTaste)) existing.Taste = newTaste;

                    repo.Update(existing);
                    Console.WriteLine("Updated successfully.");
                }
                else if (choice == "3")
                {
                    Console.Write("ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id)) continue;

                    var existing = repo.GetById(id);
                    if (existing == null) { Console.WriteLine("Category not found!"); continue; }

                    repo.Delete(existing);
                    Console.WriteLine("Deleted.");
                }
                else if (choice == "4")
                {
                    foreach (var c in repo.GetAll())
                        Console.WriteLine($"{c.Id}. {c.Name} — {c.Taste}");
                }
                else if (choice == "5") return;
                else Console.WriteLine("Invalid choice");
            }
        }

        static void MenuItemsMenu()
        {
            var repo = new MenuItemRepository();

            while (true)
            {
                Console.WriteLine("\n--- MENU ITEMS ---");
                Console.WriteLine("1 — Add");
                Console.WriteLine("2 — Update");
                Console.WriteLine("3 — Delete");
                Console.WriteLine("4 — Show all");
                Console.WriteLine("5 — Back");

                Console.Write("Choose: ");
                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Price: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal price)) price = 0;

                    Console.Write("Category ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int catId)) catId = 0;

                    var category = new CategoryRepository().GetById(catId);
                    if (category == null) { Console.WriteLine("Category not found!"); continue; }

                    repo.Add(new MenuItem { Name = name, Price = price, CategoryId = catId });
                    Console.WriteLine("MenuItem added.");
                }
                else if (choice == "2")
                {
                    Console.Write("ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id)) continue;

                    var existing = repo.GetById(id);
                    if (existing == null) { Console.WriteLine("MenuItem not found!"); continue; }

                    Console.Write($"Name (current: {existing.Name}): ");
                    string name = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(name)) existing.Name = name;

                    Console.Write($"Price (current: {existing.Price}): ");
                    string priceInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out decimal price)) existing.Price = price;

                    Console.Write($"Category ID (current: {existing.CategoryId}): ");
                    string catInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(catInput) && int.TryParse(catInput, out int catId))
                    {
                        var category = new CategoryRepository().GetById(catId);
                        if (category != null) existing.CategoryId = catId;
                        else { Console.WriteLine("Category does not exist!"); continue; }
                    }

                    repo.Update(existing);
                    Console.WriteLine("Updated.");
                }
                else if (choice == "3")
                {
                    Console.Write("ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id)) continue;

                    var existing = repo.GetById(id);
                    if (existing == null) { Console.WriteLine("Item not found!"); continue; }

                    repo.Delete(existing);
                    Console.WriteLine("Deleted.");
                }
                else if (choice == "4")
                {
                    foreach (var i in repo.GetAll())
                        Console.WriteLine($"{i.Id}. {i.Name} — {i.Price}₴ (CatId={i.CategoryId})");
                }
                else if (choice == "5") return;
                else Console.WriteLine("Invalid choice");
            }
        }

        static void CustomerMenu()
        {
            var repo = new CustomerRepository();

            while (true)
            {
                Console.WriteLine("\n--- CUSTOMERS ---");
                Console.WriteLine("1 — Add");
                Console.WriteLine("2 — Update");
                Console.WriteLine("3 — Delete");
                Console.WriteLine("4 — Show all");
                Console.WriteLine("5 — Back");

                Console.Write("Choose: ");
                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Phone: ");
                    string phone = Console.ReadLine();

                    repo.Add(new Customer { Name = name, Phone = phone });
                    Console.WriteLine("Customer added.");
                }
                else if (choice == "2")
                {
                    Console.Write("ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id)) continue;

                    var existing = repo.GetById(id);
                    if (existing == null) { Console.WriteLine("Customer not found!"); continue; }

                    Console.Write($"New Name (current: {existing.Name}): ");
                    string name = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(name)) existing.Name = name;

                    Console.Write($"New Phone (current: {existing.Phone}): ");
                    string phone = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(phone)) existing.Phone = phone;

                    repo.Update(existing);
                    Console.WriteLine("Updated.");
                }
                else if (choice == "3")
                {
                    Console.Write("ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id)) continue;

                    var existing = repo.GetById(id);
                    if (existing == null) { Console.WriteLine("Customer not found!"); continue; }

                    repo.Delete(existing);
                    Console.WriteLine("Deleted.");
                }
                else if (choice == "4")
                {
                    foreach (var c in repo.GetAll())
                        Console.WriteLine($"{c.Id}. {c.Name} — Phone: {c.Phone}");
                }
                else if (choice == "5") return;
                else Console.WriteLine("Invalid choice");
            }
        }

        static void EmployeeMenu()
        {
            var repo = new EmployeeRepository();

            while (true)
            {
                Console.WriteLine("\n--- EMPLOYEES ---");
                Console.WriteLine("1 — Add");
                Console.WriteLine("2 — Update");
                Console.WriteLine("3 — Delete");
                Console.WriteLine("4 — Show all");
                Console.WriteLine("5 — Back");

                Console.Write("Choose: ");
                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Name: ");
                    string name = Console.ReadLine();

                    Console.Write("Salary: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal salary)) salary = 0;

                    repo.Add(new Employee { Name = name, Salary = salary });
                    Console.WriteLine("Employee added.");
                }
                else if (choice == "2")
                {
                    Console.Write("ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id)) continue;

                    var existing = repo.GetById(id);
                    if (existing == null) { Console.WriteLine("Employee not found!"); continue; }

                    Console.Write($"New Name (current: {existing.Name}): ");
                    string name = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(name)) existing.Name = name;

                    Console.Write($"New Salary (current: {existing.Salary}): ");
                    string salaryInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(salaryInput) && decimal.TryParse(salaryInput, out decimal salary))
                        existing.Salary = salary;

                    repo.Update(existing);
                    Console.WriteLine("Updated.");
                }
                else if (choice == "3")
                {
                    Console.Write("ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id)) continue;

                    var existing = repo.GetById(id);
                    if (existing == null) { Console.WriteLine("Employee not found!"); continue; }

                    repo.Delete(existing);
                    Console.WriteLine("Deleted.");
                }
                else if (choice == "4")
                {
                    foreach (var e in repo.GetAll())
                        Console.WriteLine($"{e.Id}. {e.Name} — Salary: {e.Salary}₴");
                }
                else if (choice == "5") return;
                else Console.WriteLine("Invalid choice");
            }
        }

        static void OrderMenu()
        {
            var repo = new OrderRepository();

            while (true)
            {
                Console.WriteLine("\n--- ORDERS ---");
                Console.WriteLine("1 — Add");
                Console.WriteLine("2 — Update");
                Console.WriteLine("3 — Delete");
                Console.WriteLine("4 — Show all");
                Console.WriteLine("5 — Back");

                Console.Write("Choose: ");
                var choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Write("Customer ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int cId)) continue;

                    Console.Write("Menu Item ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int mId)) continue;

                    Console.Write("Employee ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int eId)) continue;

                    Console.Write("Payment Method: ");
                    string payment = Console.ReadLine();

                    var cust = new CustomerRepository().GetById(cId);
                    var item = new MenuItemRepository().GetById(mId);
                    var emp = new EmployeeRepository().GetById(eId);

                    if (cust == null || item == null || emp == null)
                    {
                        Console.WriteLine("One of the entities does not exist.");
                        continue;
                    }

                    repo.Add(new Order
                    {
                        CustomerId = cId,
                        MenuItemId = mId,
                        EmployeeId = eId,
                        PaymentMethod = payment,
                        TotalPrice = item.Price
                    });

                    Console.WriteLine("Order added.");
                }
                else if (choice == "2")
                {
                    Console.Write("Order ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id)) continue;

                    var existing = repo.GetById(id);
                    if (existing == null) { Console.WriteLine("Order not found!"); continue; }

                    Console.Write($"Customer ID (current: {existing.CustomerId}): ");
                    string custInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(custInput) && int.TryParse(custInput, out int cId))
                        existing.CustomerId = cId;

                    Console.Write($"Menu Item ID (current: {existing.MenuItemId}): ");
                    string itemInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(itemInput) && int.TryParse(itemInput, out int mId))
                        existing.MenuItemId = mId;

                    Console.Write($"Employee ID (current: {existing.EmployeeId}): ");
                    string empInput = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(empInput) && int.TryParse(empInput, out int eId))
                        existing.EmployeeId = eId;

                    Console.Write($"Payment Method (current: {existing.PaymentMethod}): ");
                    string payment = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(payment))
                        existing.PaymentMethod = payment;

                    var menuItem = new MenuItemRepository().GetById(existing.MenuItemId);
                    existing.TotalPrice = menuItem.Price;

                    repo.Update(existing);
                    Console.WriteLine("Updated.");
                }
                else if (choice == "3")
                {
                    Console.Write("ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id)) continue;

                    var existing = repo.GetById(id);
                    if (existing == null) { Console.WriteLine("Order not found!"); continue; }

                    repo.Delete(existing);
                    Console.WriteLine("Deleted.");
                }
                else if (choice == "4")
                {
                    foreach (var o in repo.GetAll())
                        Console.WriteLine(
                            $"Order {o.Id}: Cust {o.CustomerId}, Item {o.MenuItemId}, Emp {o.EmployeeId}, Pay={o.PaymentMethod}, Total={o.TotalPrice}₴");
                }
                else if (choice == "5") return;
                else Console.WriteLine("Invalid choice");
            }
        }
    }
}
