namespace System.Collections.Generic
{
	public struct Pair<TypeOne, TypeTwo>
	{
		public TypeOne ValOne;
		public TypeTwo ValTwo;

		public Pair(TypeOne one, TypeTwo two)
		{
			ValOne = one;
			ValTwo = two;
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
				if (FirstChild != null)
					foreach (T val in FirstChild.Read_DepthFirst())
						yield return val;

				//this node
				yield return Value;

				//second child
				if (SecondChild != null)
					foreach (T val in SecondChild.Read_DepthFirst())
						yield return val;
			}

			public override IEnumerable<BaseTreeNode<T>> Children
			{
				get
				{
					return new BaseTreeNode<T>[2] { FirstChild, SecondChild };
				}
			}
		}

		public class BinarySearchTree<T> where T : IComparable<T>
		{
			protected class BinarySearchTreeNode : BaseBinaryTreeNode<Pair<T, int>>
			{
				public BinarySearchTreeNode FirstChild { get; set; }
				public BinarySearchTreeNode SecondChild { get; set; }
				public override Pair<T, int> Value { get { return val; } set { val = value; } }
				protected Pair<T, int> val;

				public BinarySearchTreeNode(T val)
				{
					Value = new Pair<T, int>(val, 1);
				}

				public override BaseBinaryTreeNode<Pair<T, int>> GetFirstChild()
				{
					return FirstChild;
				}
				public override BaseBinaryTreeNode<Pair<T, int>> GetSecondChild()
				{
					return SecondChild;
				}

				public void Increment()
				{
					val.ValTwo++;
				}
			}

			protected BinarySearchTreeNode head;

			public BinarySearchTree() { }

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
						compareResult = current.Value.ValOne.CompareTo(value);
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
		}
	}
}
