using System.Linq;
namespace System.Collections.Generic
{
	/// <summary>
	/// A value and an 32 bit signed integer representing the number of it that exists.
	/// </summary>
	/// <typeparam name="T">The type of the value contained in ValueCount</typeparam>
	public struct ValueCount<T>
	{
		/// <summary>
		/// The value stored in ValueCount
		/// </summary>
		public T value;

		/// <summary>
		/// the number associated with the value
		/// </summary>
		public int count;

		/// <summary>
		/// Initilizes new instance of ValueCount
		/// </summary>
		/// <param name="val">The value</param>
		/// <param name="cnt">The count</param>
		public ValueCount(T val, int cnt)
		{
			value = val;
			count = cnt;
		}
	}

	/// <summary>
	/// Structures for all sorts of tree structures
	/// </summary>
	namespace Tree
	{
		/// <summary>
		/// Abstract class for any sort of tree node
		/// </summary>
		/// <!--Could be used for graphs as well, later update?-->
		/// <typeparam name="T">Tree type</typeparam>
		public abstract class BaseTreeNode<T>
		{
			abstract public IEnumerable<BaseTreeNode<T>> Children { get; }
			abstract public T Value { get; set; }

			public IEnumerable<T> Read_BreadthFirst()
			{
				Queue<BaseTreeNode<T>> nodeQueue = new Queue<BaseTreeNode<T>>();
				nodeQueue.Enqueue(this);

				do
				{
					BaseTreeNode<T> current = nodeQueue.Dequeue();
					yield return current.Value;
					foreach (BaseTreeNode<T> child in current.Children)
						nodeQueue.Enqueue(child);
				} while (nodeQueue.Count != 0);
			}
			public IEnumerable<T> Read_DepthFirst_TopDown()
			{
				Stack<BaseTreeNode<T>> nodeStack = new Stack<BaseTreeNode<T>>();
				nodeStack.Push(this);

				do
				{
					BaseTreeNode<T> current = nodeStack.Pop();
					yield return current.Value;
					foreach (var child in current.Children.Reverse())
						nodeStack.Push(child);
				} while (nodeStack.Count != 0);
			}
			public IEnumerable<T> Read_DepthFirst_BottomUp()
			{
				throw new NotImplementedException();
			}
		}

		public abstract class BaseBinaryTreeNode<T> : BaseTreeNode<T>
		{
			abstract public BaseBinaryTreeNode<T> GetFirstChild();
			abstract public BaseBinaryTreeNode<T> GetSecondChild();

			public IEnumerable<T> Read_DepthFirst()
			{
				//first child
				if (GetFirstChild() != null)
					foreach (T val in GetFirstChild().Read_DepthFirst())
						yield return val;

				//this node
				yield return Value;

				//second child
				if (GetSecondChild() != null)
					foreach (T val in GetSecondChild().Read_DepthFirst())
						yield return val;
			}

			public override IEnumerable<BaseTreeNode<T>> Children
			{
				get
				{
					if (GetFirstChild() != null)
						yield return GetFirstChild();
					if (GetSecondChild() != null)
						yield return GetSecondChild();
				}
			}
		}

		public class BinarySearchTree<T>: IEnumerable<T>, IEnumerable<ValueCount<T>> where T : IComparable<T>
		{
			protected class BinarySearchTreeNode : BaseBinaryTreeNode<ValueCount<T>>
			{
				public BinarySearchTreeNode FirstChild { get; set; }
				public BinarySearchTreeNode SecondChild { get; set; }
				public override ValueCount<T> Value { get { return val; } set { val = value; } }
				protected ValueCount<T> val;

				public BinarySearchTreeNode(T val)
				{
					Value = new ValueCount<T>(val, 1);
				}

				public override BaseBinaryTreeNode<ValueCount<T>> GetFirstChild()
				{
					return FirstChild;
				}
				public override BaseBinaryTreeNode<ValueCount<T>> GetSecondChild()
				{
					return SecondChild;
				}

				public void Increment()
				{
					val.count++;
				}
			}

			protected BinarySearchTreeNode head;

			public BinarySearchTree() { }
			public BinarySearchTree(T value) { head = new BinarySearchTreeNode(value); }
			public BinarySearchTree(IEnumerable<T> values) { Insert(values); }

			public void Insert(T value)
			{
				if (head == null)
					head = new BinarySearchTreeNode(value);
				else
				{
					BinarySearchTreeNode parent = null;
					BinarySearchTreeNode current = head;
					int compareResult;

					do
					{
						parent = current;
						compareResult = current.Value.value.CompareTo(value);
						if (compareResult > 0)
							current = current.FirstChild;
						else if (compareResult == 0)
						{
							current.Increment();
							return;
						}
						else
							current = current.SecondChild;
					} while (current != null);

					if (compareResult > 0)
						parent.FirstChild = new BinarySearchTreeNode(value);
					else
						parent.SecondChild = new BinarySearchTreeNode(value);
				}
			}
			public void Insert(IEnumerable<T> values)
			{
				foreach (T val in values)
					Insert(val);
			}

			public static IEnumerable<T> Sort(IEnumerable<T> input)
			{
				return new BinarySearchTree<T>(input);
			}
			

			public IEnumerator<T> GetEnumerator()
			{
				foreach (ValueCount<T> item in head.Read_DepthFirst())
					for (int i = 0; i < item.count; i++)
						yield return item.value;
			}
			IEnumerator IEnumerable.GetEnumerator()
			{
				foreach (ValueCount<T> item in head.Read_DepthFirst())
					for (int i = 0; i < item.count; i++)
						yield return item.value;
			}
			IEnumerator<ValueCount<T>> IEnumerable<ValueCount<T>>.GetEnumerator()
			{
				return head.Read_DepthFirst().GetEnumerator();
			}
		}
	}
}
