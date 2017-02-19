using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximalFrequentSet
{
	static class Program
	{
		public static void Main(string[] argv)
		{
			Console.WriteLine("HELLO FROM C#");
		}
		public static List<int[]> MaxFrequentSet(TransactionSet input)
		{
			throw new NotImplementedException();
		}

		//checks through all elements and finds which can be a frequent set with the current held set
		// is depth first, but does sort of one level of bredth (sorta)
		private static void MaxFrequentSetMethod0(List<int[]> output, TransactionSet transactionSet, int[] lastBuilt, SinglyLinkedList<int> questionMark)
		{

		}
	}
}
