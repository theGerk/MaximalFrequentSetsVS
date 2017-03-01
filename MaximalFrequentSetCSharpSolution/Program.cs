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
			for(int i = 0; i < input.Length; i++)
			{
				input[i] = new bool[16];
				byte[] buff = new byte[2];
				rand.NextBytes(buff);
				using (System.IO.BinaryReader br = new System.IO.BinaryReader(new System.IO.MemoryStream(buff)))
				{
					ushort shor = br.ReadUInt16();
					for (int j = 0; j < input[i].Length; j++)
					{
						input[i][j] = (shor & 1) == 1;
					}
					shor >>= 1;
				}
			}

			TransactionSet my = new TransactionSet(input, 100);

			Console.WriteLine(my.IsFrequent(0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15));

			//expected solution is:
			/* 02
			 * 12
			 * 01
			 * 13
			 */

			var output = my.MaxFrequentSet();

			foreach (var o in output)
			{
				foreach (var p in o)
					Console.Write(p);
				Console.WriteLine();
			}
		}
	}
}