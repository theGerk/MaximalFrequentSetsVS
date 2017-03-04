namespace System.Collections.Generic
{
	public struct ValueCount<T>
	{
		public T Value;
		public int Count;

		public ValueCount(T val, int cnt)
		{
			Value = val;
			Count = cnt;
		}
	}

	namespace Tree
	{
		public abstract class BaseTreeNode<T>
		{
			abstract public IEnumerable<BaseTreeNode<T>> Children { get; }
			abstract public T Value { get; set; }

			public IEnumerable<T> Read_BreadthFirst_TopDown()
			{
				Queue<BaseTreeNode<T>> nodeQueue = new Queue<BaseTreeNode<T>>();
				nodeQueue.Enqueue(this);

				while (nodeQueue.Count != 0)
				{
					BaseTreeNode<T> current = nodeQueue.Dequeue();
					yield return current.Value;
					foreach (BaseTreeNode<T> child in current.Children)
						nodeQueue.Enqueue(child);
				}
			}
			public IEnumerable<T> Read_BreadthFirst_BottomUp()
			{
				throw new NotImplementedException();
			}
			public IEnumerable<T> Read_DepthFirst_TopDown()
			{
				throw new NotImplementedException();
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
					return new BaseTreeNode<T>[2] { GetFirstChild(), GetSecondChild() };
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
					val.Count++;
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
						compareResult = current.Value.Value.CompareTo(value);
						if (compareResult < 0)
							current = current.FirstChild;
						else if (compareResult == 0)
						{
							current.Increment();
							return;
						}
						else
							current = current.SecondChild;
					} while (current != null);

					if (compareResult < 0)
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
					for (int i = 0; i < item.Count; i++)
						yield return item.Value;
			}
			IEnumerator IEnumerable.GetEnumerator()
			{
				foreach (ValueCount<T> item in head.Read_DepthFirst())
					for (int i = 0; i < item.Count; i++)
						yield return item.Value;
			}
			IEnumerator<ValueCount<T>> IEnumerable<ValueCount<T>>.GetEnumerator()
			{
				return head.Read_DepthFirst().GetEnumerator();
			}
		}
	}
}
