using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment
{
	class Program
	{
		public static void Main(string[] args)
		{
			do
			{
				// Read number of salaries to input
				int numSalaries = readInt( Assignment.Resource.promptNumberSalaries );

				// Read all the salaries
				int[] salaries = readSalaries( Assignment.Resource.promptEnterSalary, numSalaries );

				// Report them salaries
				viewResult( salaries );

			} while ( isContinuing() );
		}

		/// <summary>
		///		Read integer value from console
		/// </summary>
		/// <param name="prompt">text prompt to accompany input</param>
		private static int readInt( string prompt )
		{
			int value = -1;

			while ( true )
			{
				Console.Write( prompt );

				try
				{
					value = int.Parse( Console.ReadLine() );

					break;
				}
				catch ( FormatException )
				{
					viewMessage( Assignment.Resource.errorFormatException, ConsoleColor.Red );
				}
				catch ( OverflowException )
				{
					viewMessage( Assignment.Resource.errorOverflowException, ConsoleColor.Red );
				}
			}

			return value;
		}
		
		/// <summary>
		///		Read list of salaries from console
		/// </summary>
		/// <param name="prompt">text prompt to accompany input</param>
		/// <param name="count">number of salaries to read</param>
		/// <returns>unsorted integer array of salaries</returns>
		private static int[] readSalaries( string prompt, int count )
		{
			int[] salaries = new int[count];

			// Read each salary in turn
			for ( int i = 0; i < count; i++ )
			{
				int salary = -1;

				// No one should be allowed to have a negative salary (owing the company money?),
				// so let's wrap this while thing in a loop to check for that.
				// 0 is technically an allowed salary, but I wouldn't want to be that guy.
				while ( true )
				{
					salary = readInt( string.Format( prompt, i+1 ) );

					// If salary is not negative, break while loop to assign value
					if ( salary >= 0 )
						break;

					viewMessage( Assignment.Resource.errorNegativeSalary, ConsoleColor.Red );
				}

				salaries[i] = salary;
			}

			return salaries;
		}

		/// <summary>
		///		Report salary statistics to user
		/// </summary>
		private static void viewResult( int[] salaries )
		{
			string headerLine = new String( '-', 39 );

			// Calculate all our values with our handy dandy aggregates
			int median = salaries.median();
			int average = (int) salaries.Average();
			int dispersion = salaries.dispersion();

			Console.WriteLine();
			Console.WriteLine( headerLine );
			
			Console.WriteLine( Assignment.Resource.formatSalaryStats, Assignment.Resource.statsMedianSalary, median );
			Console.WriteLine( Assignment.Resource.formatSalaryStats, Assignment.Resource.statsAverageSalary, average );
			Console.WriteLine( Assignment.Resource.formatSalaryStats, Assignment.Resource.statsSalaryDispersion, dispersion );

			Console.WriteLine( headerLine );

			// Echo back each salary entered to the user, in the order entered
			for ( int i = 0; i < salaries.Length; i++ )
			{
				if ( i > 0 && i % 3 == 0 )
					Console.WriteLine();

				Console.Write( Assignment.Resource.formatSalaryPrintout, salaries[i] );
			}
		}

		/// <summary>
		///		Query user whether program should continue.
		/// </summary>
		private static bool isContinuing()
		{
			viewMessage( Assignment.Resource.promptContinue );

			// If next key pressed is esc (key code 27), return false - do not continue
			bool value = Console.ReadKey().KeyChar != 27;

			// Clear line after query
			Console.WriteLine();

			return value;
		}

		/// <summary>
		///		Standard method to print status messages to the user.
		/// </summary>
		private static void viewMessage( string message, ConsoleColor backgroundColor = ConsoleColor.Blue,
									ConsoleColor foregroundColor = ConsoleColor.White )
		{
			Console.WriteLine( "" );

			Console.ForegroundColor = foregroundColor;
			Console.BackgroundColor = backgroundColor;

			Console.WriteLine( message );

			Console.ResetColor();
			Console.WriteLine( "" );
		}
	}
}
