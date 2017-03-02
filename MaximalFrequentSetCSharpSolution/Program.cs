using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximalFrequentSet
{
	static class Program
	{
		public static Random rand = new Random(0);
		public static void Main(string[] argv)
		{
			bool[][] input = new bool[1000][];
			for (int i = 0; i < input.Length; i++)
			{
				bool[] row = new bool[16];
				for (int j = 0; j < row.Length; j++)
					row[j] = rand.Next(2) == 1;
				input[i] = row;
			}

			TransactionSet my = new TransactionSet(input, 1);

			Console.WriteLine(my.IsFrequent(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15));

			//expected solution is:
			/* 02
			 * 12
			 * 01
			 * 13
			 */

			for (int i = 0; i < input.Length; i++)
			{
				var output = my.MaxFrequentSet(i);

				//foreach (var o in output)
				//{
				//	foreach (var p in o)
				//		Console.Write("{0}, ", p);
				//	Console.WriteLine();
				//}

				Console.WriteLine(output.Count);
			}
		}
	}
}