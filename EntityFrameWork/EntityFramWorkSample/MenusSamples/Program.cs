using System;
using System.Threading.Tasks;
using System.Linq;


namespace MenusSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=======");
            CreateDatabaseAsync().Wait();
            AddRecordsAsync().Wait();
            Console.ReadLine();

        }

        private static async Task CreateDatabaseAsync() {
            using (var context = new MenusContext()) {
                bool created = await context.Database.EnsureDeletedAsync();
                string cretateText = created ? "created" : "already exists";
                Console.WriteLine($"database {cretateText}");
            }         
        }

        private static async Task AddRecordsAsync() {
            Console.WriteLine(nameof(AddRecordsAsync));
            try
            {
                using (MenusContext context=new MenusContext())
                {
                    MenuCard soupCard = new MenuCard();
                    Menu[] soups = {
                        new Menu { Text = "Consommé Célestine (with shredded pancake)", Price = 4.8m, MenuCard =soupCard},
                        new Menu { Text = "Baked Potato Soup", Price = 4.8m, MenuCard = soupCard },
                        new Menu { Text = "Cheddar Broccoli Soup", Price = 4.8m, MenuCard = soupCard },
                    };

                    soupCard.Title = "Soups";
                    soupCard.Menus.AddRange(soups);
                    context.MenuCard.Add(soupCard);

                    int recoreds = await context.SaveChangesAsync();
                    Console.WriteLine($"{recoreds} added");                   

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);               
            }

        }



    }
}
