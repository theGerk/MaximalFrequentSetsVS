using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace MaximalFrequentSet
{
	static class Program
	{
		public static Random rand = new Random(0);

		public static void Main(string[] argv)
		{
			var ts = new TransactionSet("input.txt", 2).MaxFrequentSet();
			using (var fs = File.CreateText("output.txt"))
			{
				foreach (var val in ts)
				{
					var addIn = val.Reverse().ToArray();
					Console.Write("{");
					for (int i = 0; i < addIn.Length - 1; i++)
					{
						fs.Write("{0}\t", addIn[i]);
						Console.Write("{0},", addIn[i]);
					}
					fs.WriteLine(addIn[addIn.Length - 1]);
					Console.WriteLine("{0}{1}", addIn[addIn.Length - 1], '}');
				}
			}
		}
	}
}