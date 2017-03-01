using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MaximalFrequentSet
{
	public class TransactionSet
	{
		bool[][] data;      //[row][column]
		int minimumForFrequency;

		// initilizes with a csv file
		public TransactionSet(string csvFile, int minFrequency)
		{
			minimumForFrequency = minFrequency;

			var dataList = new List<List<bool>>();
			using (var readStream = File.OpenRead(csvFile))
			{
				using (var reader = new StreamReader(readStream))
				{
					for(var line = reader.ReadLine().Split(','); !reader.EndOfStream; line = reader.ReadLine().Split(','))
					{
						var row = new List<bool>();
						foreach(var value in line)
						{
							row.Add(bool.Parse(value));
						}
						dataList.Add(row);
					}
				}
			}


			data = new bool[dataList.Count][];
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = dataList[i].ToArray();
			}
		}

		// initilizes baseed on set litteral
		public TransactionSet(bool[][] input, int minFrequency)
		{
			data = input;
			minimumForFrequency = minFrequency;
		}

		// checks if a set of columns is frequent.
		public bool IsFrequent(params int[] columnSet)
		{
			int total = 0;
			foreach (var row in data)
			{
				bool included = true;
				foreach (var value in columnSet)
				{
					included &= row[value];
					if (!included)
						break;
				}
				if (included)
					if (++total >= minimumForFrequency)
						return true;
			}
			return false;
		}
		public bool IsFrequent(IEnumerable<int> columnSet)
		{
			int total = 0;
			foreach (var row in data)
			{
				bool included = true;
				foreach (var value in columnSet)
				{
					included &= row[value];
				}
				if (included)
					if (++total >= minimumForFrequency)
						return true;
					
			}
			return false;
		}

		// number of columns
		public int Columns { get { return data[0].Length; } }

		public List<int[]> MaxFrequentSet()
		{
			List<int[]> output = new List<int[]>();
			HashSet<int> available = new HashSet<int>();
			for (int i = 0; i < Columns; i++)
				available.Add(i);
			MaxFrequentSet(output, available, null);
			return output;
		}

		private void MaxFrequentSet(List<int[]> output, HashSet<int> available, SinglyLinkedList<int> build)
		{
			available.RemoveWhere(p => !IsFrequent(new SinglyLinkedList<int>(p, build)));

			if (available.Count == 0)
			{
				var addIn = build.ToArray();
				// check that build is not a subset of any element in output
				ISet<int> buildSet = new HashSet<int>(build);
				if (!output.Exists(p => buildSet.IsSubsetOf(p)))
					output.Add(addIn);
			}
			else
			{
				foreach (var i in available.ToArray())
				{
					available.Remove(i);
					MaxFrequentSet(output, new HashSet<int>(available), new SinglyLinkedList<int>(i, build));
				}
			}
		}
	}
}
