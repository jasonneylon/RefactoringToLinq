
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace RefactoringToLinq
{
	
public class Whiskey
    {
		public static IComparer<Whiskey> PriceComparer{
			get{
				return new WhiskeyPriceComparer();
			}
		}
		
		class WhiskeyPriceComparer : IComparer<Whiskey>
		{
			public int Compare(Whiskey left, Whiskey right)
			{
				var comparer = Comparer<decimal>.Default;
				return comparer.Compare(left.Price, right.Price);
				//return left.Price.CompareTo(right.Price);
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
		}
		
		
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Price { get; set; }
        public string Country { get; set; }
    }
}
