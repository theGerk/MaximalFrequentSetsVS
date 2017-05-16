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
	/// Graphs, graphs and more graphs!
	/// </summary>
	namespace Graph
	{
		/// <summary>
		/// Extension functions for graphs
		/// </summary>
		public static class Extension
		{
			/// <summary>
			/// Enumerates in standard breath first across Graph nodes, does not allow for double counting nodes
			/// </summary>
			/// <param name="self">starting node</param>
			/// <returns>Enumerates the graph nodes</returns>
			public static IEnumerable<IGraphNode> Enumerate_BreathFirst(this IGraphNode self)
			{
				var crossed = new HashSet<IGraphNode>();
				var nodeQueue = new Queue<IGraphNode>();
				nodeQueue.Enqueue(self);
				crossed.Add(self);

				do
				{
					var current = nodeQueue.Dequeue();
					yield return current;
					foreach (var child in current.Connections)
						if(crossed.Add(child))
							nodeQueue.Enqueue(child);
				} while (nodeQueue.Count != 0);
			}

			/// <summary>
			/// Enumreates depth first, does not allow for double counting nodes
			/// </summary>
			/// <param name="self"></param>
			/// <returns></returns>
			public static IEnumerable<IGraphNode> Enumerate_DepthFirst_TopDown(this IGraphNode self)
			{
				var crossed = new HashSet<IGraphNode>();
				var nodeStack = new Stack<IGraphNode>();
				nodeStack.Push(self);
				crossed.Add(self);

				do
				{
					var current = nodeStack.Pop();
					yield return current;
					foreach (var child in current.Connections.Reverse())
						if (crossed.Add(child))
							nodeStack.Push(child);
				} while (nodeStack.Count != 0);
			}

		}

		/// <summary>
		/// Interface for abstraction of a graph
		/// </summary>
		public interface IGraphNode
		{
			/// <summary>
			/// Set of nodes, this node has connection to.
			/// </summary>
			IEnumerable<IGraphNode> Connections { get; }
		}

		/// <summary>
		/// Interface for a node within a graph of certain type T
		/// </summary>
		/// <typeparam name="T">Type contained in nodes of graph</typeparam>
		public interface IGraphNode<out T> : IGraphNode
		{
			/// <summary>
			/// set of connections
			/// </summary>
			new IEnumerable<IGraphNode<T>> Connections { get; }
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
			public abstract class TreeNodeBase<T>
			{
				/// <summary>
				/// All children of the node
				/// </summary>
				abstract public IEnumerable<TreeNodeBase<T>> Children { get; }

				/// <summary>
				/// the value contained in the node
				/// </summary>
				abstract public T Value { get; set; }

				/// <summary>
				/// Enumerates across the array with standard breadth first search
				/// </summary>
				/// <returns>IEnumerable</returns>
				public IEnumerable<T> Read_BreadthFirst()
				{
					Queue<TreeNodeBase<T>> nodeQueue = new Queue<TreeNodeBase<T>>();
					nodeQueue.Enqueue(this);

					do
					{
						TreeNodeBase<T> current = nodeQueue.Dequeue();
						yield return current.Value;
						foreach (TreeNodeBase<T> child in current.Children)
							nodeQueue.Enqueue(child);
					} while (nodeQueue.Count != 0);
				}

				/// <summary>
				/// Enumerates Depth first, first outputing itself then children in order, and so on recursivly.
				/// (Does not acutally use recursion)
				/// </summary>
				/// <returns>Enumerable of values at each point</returns>
				public IEnumerable<T> Read_DepthFirst_TopDown()
				{
					Stack<TreeNodeBase<T>> nodeStack = new Stack<TreeNodeBase<T>>();
					nodeStack.Push(this);

					do
					{
						TreeNodeBase<T> current = nodeStack.Pop();
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

			public abstract class BinaryTreeNodeBase<T> : TreeNodeBase<T>
			{
				abstract public BinaryTreeNodeBase<T> GetFirstChild();
				abstract public BinaryTreeNodeBase<T> GetSecondChild();

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

				public override IEnumerable<TreeNodeBase<T>> Children
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

			public class BinarySearchTree<T> : IEnumerable<T>, IEnumerable<ValueCount<T>> where T : IComparable<T>
			{
				protected class BinarySearchTreeNode : BinaryTreeNodeBase<ValueCount<T>>
				{
					public BinarySearchTreeNode FirstChild { get; set; }
					public BinarySearchTreeNode SecondChild { get; set; }
					public override ValueCount<T> Value { get { return val; } set { val = value; } }
					protected ValueCount<T> val;

					public BinarySearchTreeNode(T val)
					{
						Value = new ValueCount<T>(val, 1);
					}

					public override BinaryTreeNodeBase<ValueCount<T>> GetFirstChild()
					{
						return FirstChild;
					}
					public override BinaryTreeNodeBase<ValueCount<T>> GetSecondChild()
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
}