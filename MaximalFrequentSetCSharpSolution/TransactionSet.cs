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
				}
				total += included ? 1 : 0;
			}
			return total >= minimumForFrequency;
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
				total += included ? 1 : 0;
			}
			return total >= minimumForFrequency;
		}

		// number of columns
		public int Columns { get { return data[0].Length; } }
	}
}
