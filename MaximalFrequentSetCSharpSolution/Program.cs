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
			TransactionSet ts = new TransactionSet("input.txt", 2);
			foreach (var val in ts.MaxFrequentSet())
			{
				var addIn = val.Reverse().ToArray();
				Console.Write("{");
				for (int i = 0; i < addIn.Length; i++)
					Console.Write("{0}{1}", addIn[i], (i == addIn.Length - 1) ? "" : ", ");
				Console.WriteLine("}");
			}
		}
	}
}