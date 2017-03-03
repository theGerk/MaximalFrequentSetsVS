using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Munchers;

namespace MaximalFrequentSet
{
	public class TransactionSet
	{
		private struct Row : IComparable<Row>, IComparable<IList<bool>>
		{
			bool[] data;

			public int Length { get { return data.Length; } }
			public int count;
			public int NumberContained(IEnumerable<int> columnSet)
			{
				bool included = true;
				foreach (var value in columnSet)
				{
					included &= data[value];
					if (!included)
						break;
				}
				return count;
			}

			public int CompareTo(Row other)
			{
				if (data.Length != other.data.Length)
					return data.Length.CompareTo(other.data.Length);

				for(int i = 0; i < data.Length; i++)
				{
					if (data[i] != other.data[i])
						return data[i].CompareTo(other.data[i]);
				}
				return 0;
			}
			public int CompareTo(IList<bool> other)
			{
				if (data.Length != other.Count)
					return data.Length.CompareTo(other.Count);

				for (int i = 0; i < data.Length; i++)
				{
					if (data[i] != other[i])
						return data[i].CompareTo(other[i]);
				}
				return 0;
			}

			public Row(IEnumerable<bool> values, int cnt = 1)
			{
				data = values.ToArray();
				count = cnt;
			}
		}


		Row[] data;      //[row][column]
		public int MinimumForFrequency { get; set; }        //must be greater then 0, if set at <= 0 will act the same as if set to 1		
		public int Columns { get { return data[0].Length; } }
		public int Rows { get { return data.Length; } }


		// initilizes with an input file
		public TransactionSet(string fileName, int minFrequency)
		{
			MinimumForFrequency = minFrequency;
			Sort<Row> set = new SortedSet<Row>();
			using (StreamReader read = new StreamReader(File.OpenRead(fileName)))
			{
				string line = read.ReadLine();
				while(!read.EndOfStream)
				{
					using (Task<string> t = read.ReadLineAsync())
					{
						t.Start();
						var row = line.MunchToBool(StringMuncher.Formating.Decimal);
						
						line = t.Result;
					}
				}
			}
		}

		// checks if a set of columns is frequent.
		public bool IsFrequent(params int[] columnSet)
		{
			int total = 0;
			foreach (var row in data)
			{
				total += row.NumberContained(columnSet);
				if (total >= MinimumForFrequency)
					return true;
			}
			return false;
		}
		public bool IsFrequent(IEnumerable<int> columnSet)
		{
			int total = 0;
			foreach (var row in data)
			{
				total += row.NumberContained(columnSet);
				if (total >= MinimumForFrequency)
					return true;
			}
			return false;
		}

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
				int[] addIn;
				try
				{
					addIn = build.ToArray();
				}
				catch (ArgumentNullException)
				{
					addIn = new int[]{ };
				}
				// check that build is not a subset of any element in output
				ISet<int> buildSet = new HashSet<int>(addIn);
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
