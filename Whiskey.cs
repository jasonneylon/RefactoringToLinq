
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace RefactoringToLinq
{

	public class FunctionalComparer<T> : IComparer<T>
	{
		Func<T, T, int> compareFunction;

		public FunctionalComparer (Func<T, T, int> compareFunction)
		{
			this.compareFunction = compareFunction;
		}

		public int Compare (T x, T y)
		{
			return compareFunction (x, y);
		}
		
	}

	public class Whiskey
	{
	
		public Whiskey()
		{
			Ingredients = new List<Whiskey>();
		}
		
		public string Name { get; set; }
		public int Age { get; set; }
		public decimal Price { get; set; }
		public string Country { get; set; }
		
		public List<Whiskey> Ingredients {get; set;}
	}
}
