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
			
var whiskeyNames = new [] {"Ardbeg 1998", "Glenmorangie","Talisker", "Cragganmore"};

var listOfWhiskies = whiskeyNames.Aggregate("Whiskies: ", (accumulated, next) => 
{
	Console.Out.WriteLine("(Adding [{0}] to the list [{1}])", next, accumulated);
	return accumulated + " " + next;
});
			
Console.Out.WriteLine(listOfWhiskies);

			
listOfWhiskies = whiskeyNames.Aggregate((accumulated, next) => 
{
	Console.Out.WriteLine("(Adding [{0}] to the list [{1}])", next, accumulated);
	return accumulated + " " + next;
});
			
Console.Out.WriteLine(listOfWhiskies);

			listOfWhiskies = new string[] {"Single item"}.Aggregate((accumulated, next) => 
{
	Console.Out.WriteLine("(Adding [{0}] to the list [{1}])", next, accumulated);
	return accumulated + " " + next;
});
			
Console.Out.WriteLine(listOfWhiskies);

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
			
		    var goodValueWhiskies = whiskies.Where(x=> x.Price <= 30m).ToList();
		    Console.WriteLine("Found {0} good value whiskeys", goodValueWhiskies.Count);
			
		    var howMany12YearOldWhiskies = whiskies.Count(x=> x.Age == 12);
			
		    Console.WriteLine ("How many 12 year old whiskies do we have {0}", howMany12YearOldWhiskies);
					     
		    var allAreScottish = whiskies.All(x=> x.Country == "Scotland");
		    
			
		    Console.Out.WriteLine ("All are scottish? {0}", allAreScottish);
			
		    var isThereIrishWhiskey = whiskies.Any(x=> x.Country == "Ireland");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Out.WriteLine("Is there Irish whiskey? {0}", isThereIrishWhiskey);
			
			
		    // doing too much example - first split up the for 
var scottishWhiskiesCount = 0;
var scottishWhiskeyTotal = 0m;
foreach (var whiskey in whiskies) {
    if (whiskey.Country == "Scotland") {
        scottishWhiskiesCount++;
        scottishWhiskeyTotal += whiskey.Price;
    }
}

var scottishWhiskies = whiskies.Where(x=> x.Country == "Scotland").ToList();
scottishWhiskiesCount = scottishWhiskies.Count();
scottishWhiskeyTotal = scottishWhiskies.Sum(x=> x.Price);

scottishWhiskies = new List<Whiskey> ();
foreach (var whiskey in whiskies) {
    if (whiskey.Country == "Scotland") {
        scottishWhiskies.Add (whiskey);
    }
}
			
foreach (var whiskey in scottishWhiskies) {
    if (whiskey != null)
	scottishWhiskiesCount++;
}

foreach (var whiskey in scottishWhiskies) {
    scottishWhiskeyTotal += whiskey.Price;
}
            Console.ForegroundColor = ConsoleColor.Blue;
		    Console.WriteLine ("{0} scottish whiskies costing {1}", scottishWhiskiesCount, scottishWhiskeyTotal);
			
			/*
var blendedWhiskey = new Whiskey() { Name="Tesco value whiskey", Age=3, Country="Scotland" };
foreach (var whiskey in whiskies)
{
    if (whiskey.Country != "Scotland")
    {
        continue;
    }
	
    blendedWhiskey.Ingredients.Add(whiskey);
    blendedWhiskey.Price = blendedWhiskey.Price + (whiskey.Price / 10);
};
			
Console.WriteLine("Blended Whiskey Name: {0}", blendedWhiskey.Name);
Console.WriteLine("Blended Whiskey Price: {0}", blendedWhiskey.Price);
Console.WriteLine("Blended Whiskey Ingredients: {0}", blendedWhiskey.IngredientsAsString);
			*/
var blendedWhiskey = whiskies.Where(x=> x.Country == "Scotland")
.Aggregate(new Whiskey() { Name="Tesco value whiskey", Age=3, Country="Scotland" },
	(newWhiskey, nextWhiskey) => 
	{
		newWhiskey.Ingredients.Add(nextWhiskey);
		newWhiskey.Price += (nextWhiskey.Price / 10);
		return newWhiskey;
	});

Console.WriteLine("Blended Whiskey Name: {0}", blendedWhiskey.Name);
Console.WriteLine("Blended Whiskey Price: {0}", blendedWhiskey.Price);
Console.WriteLine("Blended Whiskey Ingredients: {0}", blendedWhiskey.IngredientsAsString);
			
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
Console.WriteLine("Most expensive is {0}", mostExpensiveWhiskey.Name);

			
mostExpensiveWhiskey = whiskies.Aggregate((champion, challenger) => challenger.Price > champion.Price ? challenger : champion);
Console.WriteLine("Most expensive is {0}", mostExpensiveWhiskey.Name);

		}
	}
}
