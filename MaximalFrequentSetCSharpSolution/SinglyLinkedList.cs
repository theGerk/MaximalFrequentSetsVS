using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{

	public class SinglyLinkedList<T> : IEnumerable<T>
	{
		public T Value { get; set; }
		public SinglyLinkedList<T> Next { get; set; }

		public SinglyLinkedList(T value, SinglyLinkedList<T> next)
		{
			Value = value;
			Next = next;
		}

		public IEnumerator<T> GetEnumerator()
		{
			for (var current = this; current != null; current = current.Next)
			{
				yield return current.Value;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}