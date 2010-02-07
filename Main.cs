using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq; 

namespace RefactoringToLinq
{
    public class Whiskey
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Price { get; set; }
        public string Country { get; set; }
    }

    class Program
    {
        static Whiskey ardbeg = new Whiskey { Name = "Ardbeg 1998", Age = 12, Price = 49.95m, Country = "Scotland" };
        static Whiskey glenmorangie = new Whiskey { Name = "Glenmorangie", Age = 10, Price = 28.95m, Country = "Scotland" };
        static Whiskey talisker = new Whiskey { Name = "Talisker", Age = 18, Price = 57.95m, Country = "Scotland" };
        static Whiskey cragganmore = new Whiskey { Name = "Cragganmore", Age = 12, Price = 30.95m, Country = "Scotland" };
        static Whiskey redbreast = new Whiskey { Name = "Redbreast", Age = 12, Price = 27.95m, Country = "Ireland" };
        static Whiskey greenspot = new Whiskey { Name = "Green spot", Age = 8, Price = 44.48m, Country = "Ireland" };

        static List<Whiskey>  whiskies = new List<Whiskey>() {
                ardbeg,
                glenmorangie,
                talisker,
                cragganmore,
                redbreast,
                greenspot
            };

        static List<Owner> owners = new List<Owner>()
        {
            new Owner("Glenmorangie", ardbeg, glenmorangie),
            new Owner("Diageo", talisker, cragganmore),
            new Owner("Pernod", redbreast, greenspot)
        };

        static void LoopHappyVersion()
        {

            // project to a different type
            var whiskeyNames = new List<string>();
            foreach (var whiskey in whiskies)
            {
                whiskeyNames.Add(whiskey.Name);
            }

            ObjectDumper.Write(whiskeyNames);

            // filter down to good value whiskey

            var goodValueWhiskeies = new List<Whiskey>();
            foreach (var whiskey in whiskies)
            {
                if (whiskey.Price < 25m)
                {
                    goodValueWhiskeies.Add(whiskey);
                }
            }
            ObjectDumper.Write(goodValueWhiskeies);

            var howMany12YearOldWhiskies = 0;
            foreach (var whiskey in whiskies)
            {
                if (whiskey.Age == 12)
                {
                    howMany12YearOldWhiskies++;
                }
            }

            Console.WriteLine("How many 12 year old whiskies do we have {0}", howMany12YearOldWhiskies); 
            

            // Quantifier examples - all 
            var allAreScottish = true;
            foreach (var whiskey in whiskies)
            {
                if (whiskey.Country != "Scotland")
                {
                    allAreScottish = false;
                    break;
                }
            }

            Console.Out.WriteLine("All are scottish? {0}", allAreScottish);

            var isThereIrishWhiskey = false;
            foreach (var whiskey in whiskies)
            {
                if (whiskey.Country == "Ireland")
                {
                    isThereIrishWhiskey = true;
                    break;
                }
            }

            Console.Out.WriteLine("Is there Irish whiskey? {0}", isThereIrishWhiskey);

            // max - aggregation examples
            Whiskey mostExpensiveWhiskey = null;
            foreach (var challenger in whiskies)
            {
                if (mostExpensiveWhiskey == null)
                {
                    mostExpensiveWhiskey = challenger;
                }
                if (challenger.Price > mostExpensiveWhiskey.Price)
                {
                    mostExpensiveWhiskey = challenger;
                }
            }
            Console.WriteLine("mostExpensive is");
            ObjectDumper.Write(mostExpensiveWhiskey);

            // doing too much
            var scottishWhiskiesCount = 0;
            var scottishWhiskeyTotal = 0m;
            foreach (var whiskey in whiskies)
            {
                if (whiskey.Country == "Scotland")
                {
                    scottishWhiskiesCount++;
                    scottishWhiskeyTotal += whiskey.Price; 
                }
            }

            scottishWhiskiesCount = 0;
            scottishWhiskeyTotal = 0m;

            List<Whiskey> scottishWhiskies = new List<Whiskey>();
            foreach (var whiskey in whiskies)
            {
                if (whiskey.Country == "Scotland")
                {
                    scottishWhiskies.Add(whiskey);
                }
            }
            foreach (var whiskey in scottishWhiskies)
            {
                scottishWhiskiesCount++;
            }
            
            foreach (var whiskey in scottishWhiskies)
            {
                scottishWhiskeyTotal += whiskey.Price;
            }

            Console.WriteLine("{0} scottish whiskies costing {1}", scottishWhiskiesCount, scottishWhiskeyTotal);

            // whiskey names from owners

            List<string> whiskeyNamesFromOwners = new List<string>();
            foreach (var owner in owners)
            {
                foreach (var whiskey in owner.Whiskies)
                {
                    whiskeyNamesFromOwners.Add(whiskey.Name);
                }
            }

            ObjectDumper.Write(whiskeyNamesFromOwners);
        }

        static void LinqVersion()
        {
            // project to a different type
            var whiskeyNames = whiskies.Select(x => x.Name).ToList();

            ObjectDumper.Write(whiskeyNames);

            var goodValueWhiskies = whiskies.Where(x => x.Price < 25m).ToList();

            ObjectDumper.Write(goodValueWhiskies);

            var howMany12YearOldWhiskies = whiskies.Count(x => x.Age == 12);

            Console.WriteLine("How many 12 year old whiskies do we have {0}", howMany12YearOldWhiskies); 

            // Quantifier examples - all 
            var allAreScottish = whiskies.All(x => x.Country == "Scotland");

            Console.Out.WriteLine("All are scottish? {0}", allAreScottish);

            var isThereIrishWhiskey = whiskies.Any(x=> x.Country == "Ireland");

            Console.Out.WriteLine("Is there Irish whiskey? {0}", isThereIrishWhiskey);

            // order and first - aggregation examples
            Whiskey mostExpensiveWhiskey = whiskies.OrderByDescending(x=> x.Price).First();
            Console.WriteLine("mostExpensive is");

            Whiskey mostExpensiveWhiskey2 = whiskies.Aggregate(
                (champion, challenger) => challenger.Price > champion.Price ? challenger : champion
                );
            Console.WriteLine("mostExpensive using aggregate :");
            ObjectDumper.Write(mostExpensiveWhiskey2);

            var scottishWhiskiesCount = 0;
            var scottishWhiskeyTotal = 0m;
            var scottishWhiskies = whiskies.Where(x => x.Country == "Scotland");
            scottishWhiskeyTotal = scottishWhiskies.Sum(x => x.Price);
            scottishWhiskiesCount = scottishWhiskies.Count();

            Console.WriteLine("{0} scottish whiskies costing {1}", scottishWhiskiesCount, scottishWhiskeyTotal);

            var whiskeyNamesFromOwners = owners.SelectMany(x => x.Whiskies).Select(x=> x.Name).ToList();

            whiskeyNamesFromOwners = (from owner in owners
                                      from whiskey in owner.Whiskies
                                      select whiskey.Name).ToList();

            ObjectDumper.Write(whiskeyNamesFromOwners);

            
            // http://code.google.com/p/morelinq/ - has this
        }
        static void Main(string[] args)
        {
            LoopHappyVersion();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Out.WriteLine("Linq version");
            LinqVersion();
        }
    }



        public class Owner
        {
            public Owner(string name, params Whiskey[] whiskies)
            {
                Name = name;
                Whiskies = whiskies;
            }
            public string Name {get;set; }
            public IEnumerable<Whiskey> Whiskies { get; set; }
        }

}
