
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
			public int Compare (Whiskey left, Whiskey right)
			{
				var comparer = Comparer<decimal>.Default;
				return comparer.Compare (left.Price, right.Price);
				//return left.Price.CompareTo(right.Price);
			}
				/*
				if (left.Price > right.Price)
				{
					return 1;
				}
				// if the first object is less than the second
				if (left.Price < right.Price)
				{
					return -1;
				}
				return 0;
				*/				
		}


		public string Name { get; set; }
		public int Age { get; set; }
		public decimal Price { get; set; }
		public string Country { get; set; }
	}
}
