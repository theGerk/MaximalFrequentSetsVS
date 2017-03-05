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
			var ts = new TransactionSet("..\\..\\input.txt", 5);
			var freq = ts.MaxFrequentSet();
			File.Delete("..\\..\\output.txt");
			using (var fs = File.CreateText("..\\..\\output.txt"))
			{
				foreach (var val in freq)
				{
					var addIn = val.Reverse().ToArray();
					Console.Write("{");
					for (int i = 0; i < addIn.Length - 1; i++)
					{
						fs.Write("{0}\t", addIn[i]);
						Console.Write("{0}, ", addIn[i]);
					}
					try
					{
						fs.WriteLine(addIn[addIn.Length - 1]);
						Console.WriteLine("{0}{1}", addIn[addIn.Length - 1], '}');
					}
					catch (Exception) { Console.WriteLine('}'); }
				}
			}
			Console.WriteLine(freq.Count);
		}
	}
}