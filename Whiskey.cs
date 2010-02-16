
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
		// its Func<T, TResult>  - last param is return value!!
		public static IComparer<Whiskey> PriceComparer {
			get { return new FunctionalComparer<Whiskey> ((left, right) => left.Price.CompareTo (right.Price)); }
		}

		class WhiskeyPriceComparer : IComparer<Whiskey>
		{
			// if left is greater return -1, if right is greater return 1
			public int Compare (Whiskey left, Whiskey right)
			{
				var comparer = Comparer<decimal>.Default;
				return comparer.Compare (left.Price, right.Price);
			}
		}


		public string Name { get; set; }
		public int Age { get; set; }
		public decimal Price { get; set; }
		public string Country { get; set; }
	}
}
