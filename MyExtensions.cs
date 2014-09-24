using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment
{
	static class MyExtensions
	{
		/// <summary>
		///		Extension method for int[] to calculate value dispersion
		/// </summary>
		/// <returns>difference between smallest and largest values in array</returns>
		public static int dispersion( this int[] source )
		{
			return source.Max() - source.Min();
		}

		/// <summary>
		///		Extension method for int[] to calculate median value
		/// </summary>
		/// <returns>median value in provided array</returns>
		public static int median( this int[] source )
		{
			// If source is one long, the choice is pretty simple
			if ( source.Length == 1 )
				return source[0];

			// Produce a sorted array of our numbers
			int[] sorted = source.OrderBy( a => a ).ToArray();

			// If array is even length, average middle two
			if ( ( sorted.Length & 1 ) == 0 )
			{
				int mid = sorted.Length / 2;

				return ( sorted[mid] - sorted[mid-1] ) / 2;
			}
			// If array is odd length, return middle value
			else
			{
				return sorted[sorted.Length/2 + 1];
			}
		}
	}
}
