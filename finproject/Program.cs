using finproject.DAL;
using System;
using Dapper;
using System.Data.SqlClient;

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
                Console.WriteLine("7 — Favorite Movies");

                Console.Write("Choose: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": CategoryMenu(); break;
                    case "2": MenuItemsMenu(); break;
                    case "3": CustomerMenu(); break;
                    case "4": EmployeeMenu(); break;
                    case "5": OrderMenu(); break;
                    case "6": return;
                    case "7": FavoriteMovies(); break;


                    default: Console.WriteLine("Invalid choice!"); break;
                }
            }
        }
        static void FavoriteMovies()
        {
            var repo = new FavoriteMovieRepository();

            while (true)
            {
                Console.WriteLine("\n--- FAVORITE MOVIES ---");
                Console.WriteLine("1 — Add 3 movies");
                Console.WriteLine("2 — Show all movies");
                Console.WriteLine("3 — Delete all movies");
                Console.WriteLine("4 — Back");

                Console.Write("Choose: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    FavoriteMovie movie1 = new FavoriteMovie();
                    movie1.Title = "Побег из Шоушенка";

                    FavoriteMovie movie2 = new FavoriteMovie();
                    movie2.Title = "Интерстеллар";

                    FavoriteMovie movie3 = new FavoriteMovie();
                    movie3.Title = "Дедпул";

                    repo.Add(movie1);
                    repo.Add(movie2);
                    repo.Add(movie3);

                    Console.WriteLine("3 movies added!");
                }
                else if (choice == "2")
                {
                    Console.WriteLine("\n--- MOVIES ---");
                    foreach (FavoriteMovie m in repo.GetAll())
                        Console.WriteLine(m.Id + ". " + m.Title);
                }
                else if (choice == "3")
                {
                    repo.DeleteAll();
                    Console.WriteLine("All movies deleted");
                }
                else if (choice == "4")
                    return;
                else
                    Console.WriteLine("Invalid choice");
            }
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
            }
        }
    }
}