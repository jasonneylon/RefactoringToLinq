using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RefactoringToLinq
{
	class Program
	{
	    static void Main (string[] args)
		{
			LoopHappyVersion();	
		}
		
		static void LoopHappyVersion()
		{
			Whiskey ardbeg = new Whiskey { Name = "Ardbeg 1998", Age = 12, Price = 49.95m, Country = "Scotland" };
			Whiskey glenmorangie = new Whiskey { Name = "Glenmorangie", Age = 10, Price = 28.95m, Country = "Scotland" };
			Whiskey talisker = new Whiskey { Name = "Talisker", Age = 18, Price = 57.95m, Country = "Scotland" };
			Whiskey cragganmore = new Whiskey { Name = "Cragganmore", Age = 12, Price = 30.95m, Country = "Scotland" };
			Whiskey redbreast = new Whiskey { Name = "Redbreast", Age = 12, Price = 27.95m, Country = "Ireland" };
			Whiskey greenspot = new Whiskey { Name = "Green spot", Age = 8, Price = 44.48m, Country = "Ireland" };
			
			List<Whiskey> whiskies = new List<Whiskey> { ardbeg, glenmorangie, talisker, cragganmore, redbreast, greenspot };

		    // project to a different type
		    var whiskeyNames = new List<string> ();
		    foreach (var whiskey in whiskies) {
		        whiskeyNames.Add (whiskey.Name);
		    }
			
	        Console.WriteLine("Whiskey names: {0}", String.Join(", ", whiskeyNames.ToArray()));
			
		    // filter down to good value whiskey
			
		    var goodValueWhiskeies = new List<Whiskey> ();
		    foreach (var whiskey in (IEnumerable<Whiskey>) whiskies) {
		        if (whiskey.Price < 30m) {
		            goodValueWhiskeies.Add (whiskey);
		        }
		    }
		    Console.WriteLine("Found {0} good value whiskeys", goodValueWhiskeies.Count);
			
		    var howMany12YearOldWhiskies = 0;
		    foreach (var whiskey in (IEnumerable<Whiskey>) whiskies) {
		        if (whiskey.Age == 12) {
		            howMany12YearOldWhiskies++;
		        }
		    }
			
		    Console.WriteLine ("How many 12 year old whiskies do we have {0}", howMany12YearOldWhiskies);
			
		    // Quantifier examples - all 
		    var allAreScottish = true;
		    foreach (var whiskey in (IEnumerable<Whiskey>) whiskies) {
		        if (whiskey.Country != "Scotland") {
		            allAreScottish = false;
		            break;
		        }
		    }
			
		    Console.Out.WriteLine ("All are scottish? {0}", allAreScottish);
			
		    var isThereIrishWhiskey = false;
		    foreach (var whiskey in (IEnumerable<Whiskey>) whiskies) {
		        if (whiskey.Country == "Ireland") {
		            isThereIrishWhiskey = true;
		            break;
		        }
		    }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Out.WriteLine("Is there Irish whiskey? {0}", isThereIrishWhiskey);
			
			
		    // doing too much example - first split up the for 
            var scottishWhiskiesCount = 0;
		    var scottishWhiskeyTotal = 0m;
		    foreach (var whiskey in (IEnumerable<Whiskey>) whiskies) {
		        if (whiskey.Country == "Scotland") {
		            scottishWhiskiesCount++;
		            scottishWhiskeyTotal += whiskey.Price;
		        }
		    }
			
		    scottishWhiskiesCount = 0;
		    scottishWhiskeyTotal = 0m;
			
		    List<Whiskey> scottishWhiskies = new List<Whiskey> ();
		    foreach (var whiskey in (IEnumerable<Whiskey>) whiskies) {
		        if (whiskey.Country == "Scotland") {
		            scottishWhiskies.Add (whiskey);
		        }
		    }
		    foreach (var whiskey in scottishWhiskies) {
		        scottishWhiskiesCount++;
		    }
			
		    foreach (var whiskey in scottishWhiskies) {
		        scottishWhiskeyTotal += whiskey.Price;
		    }
            Console.ForegroundColor = ConsoleColor.Blue;
		    Console.WriteLine ("{0} scottish whiskies costing {1}", scottishWhiskiesCount, scottishWhiskeyTotal);
			
			
		    var blendedWhiskey = new Whiskey() { Name="Tesco value whiskey", Age=3, Country="Scotland" };
		    foreach (var whiskey in (IEnumerable<Whiskey>) whiskies)
		    {
		        if (whiskey.Country != "Scotland")
		        {
		            continue;
		        }
				
		        blendedWhiskey.Ingredients.Add(whiskey);
                blendedWhiskey.Price = blendedWhiskey.Price + (whiskey.Price / 10);
		    };

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Blended Whiskey");
		    ObjectDumper.Write (blendedWhiskey);
			
			
/*   		 List<string> whiskeyNamesFromOwners = new List<string>();
            foreach (var owner in owners)
            {
                foreach (var whiskey in owner.Whiskies)
                {
                    whiskeyNamesFromOwners.Add(whiskey.Name);
                }
            }
			 */
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
            Console.WriteLine("mostExpensive is {0}", mostExpensiveWhiskey.Name);
            
		}
	}
}
