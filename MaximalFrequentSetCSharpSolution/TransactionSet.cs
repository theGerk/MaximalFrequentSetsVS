using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Munchers;
using System.Collections.Generic.Tree;
using System.Collections;

namespace MaximalFrequentSet
{
	public class TransactionSet
	{
		private struct Row
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
						return 0;
				}
				return count;
			}

			public Row(IEnumerable<bool> values, int cnt = 1)
			{
				data = values.ToArray();
				count = cnt;
			}
		}
		private struct CompareableEnumerable<T> : IComparable<IEnumerable<T>>, IComparable<CompareableEnumerable<T>>, IEnumerable<T> where T : IComparable<T>
		{
			IEnumerable<T> content;

			public CompareableEnumerable(IEnumerable<T> input)
			{
				content = input;
			}

			public int CompareTo(CompareableEnumerable<T> other)
			{
				return CompareTo((IEnumerable<T>)other);
			}

			public int CompareTo(IEnumerable<T> other)
			{
				using (IEnumerator<T> thisOne = content.GetEnumerator(), otherOne = other.GetEnumerator())
				{
					bool thisComplete = false, otherComplete = false;
					while((thisComplete = thisOne.MoveNext()) && (otherComplete = otherOne.MoveNext()))
					{
						int comp = thisOne.Current.CompareTo(otherOne.Current);
						if (comp != 0)
							return comp;
					}
					return thisComplete.CompareTo(otherComplete);
				}
			}

			public IEnumerator<T> GetEnumerator()
			{
				return content.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return content.GetEnumerator();
			}
		}


		Row[] data;

		public int MinimumForFrequency { get; set; }		//must be greater then 0, if set at <= 0 will act the same as if set to 1		
		public int Columns { get { return data[0].Length; } }
		public int Rows { get { return data.Length; } }


		// initilizes with an input file
		public TransactionSet(string fileName, int minFrequency)
		{
			MinimumForFrequency = minFrequency;
			List<Row> set = new List<Row>();
			BinarySearchTree<CompareableEnumerable<bool>> tree = new BinarySearchTree<CompareableEnumerable<bool>>();
			using (StreamReader read = new StreamReader(File.OpenRead(fileName)))
			{
				string line = read.ReadLine();
				while(!read.EndOfStream)
				{
					using (Task<string> asyncReadLineTask = read.ReadLineAsync())
					{
						tree.Insert(new CompareableEnumerable<bool>(line.MunchToBool(StringMuncher.Formating.Decimal)));
						line = asyncReadLineTask.Result;
					}
				}
				tree.Insert(new CompareableEnumerable<bool>(line.MunchToBool(StringMuncher.Formating.Decimal)));
			}
			foreach (ValueCount<CompareableEnumerable<bool>> item in (IEnumerable<ValueCount<CompareableEnumerable<bool>>>)tree)
				set.Add(new Row(item.value, item.count));
			data = set.ToArray();
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
		public bool  IsFrequent(IEnumerable<int> columnSet)
		{
			int total = 0;
			foreach (Row row in data)
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
				HashSet<int> buildSet = new HashSet<int>(addIn);
				if (!output.Exists(p => buildSet.IsSubsetOf(p)))
				{
					output.Add(addIn);

					//Console.Write("{");
					//for (int i = 0; i < addIn.Length; i++)
					//	Console.Write("{0}{1}", addIn[i], (i == addIn.Length - 1) ? "" : ", ");
					//Console.WriteLine("}");
				}
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
