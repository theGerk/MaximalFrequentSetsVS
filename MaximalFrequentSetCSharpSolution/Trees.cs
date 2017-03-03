using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic.Tree
{
	public abstract class BaseTree<T>
	{
		public IEnumerable<BaseTree<T>> Children { get; }
		public T Value { get; }

		public IEnumerable<T> Read_BreadthFirst_TopDown()
		{
			Queue<BaseTree<T>> nodeQueue = new Queue<BaseTree<T>>();
			nodeQueue.Enqueue(this);

			while(nodeQueue.Count != 0)
			{
				BaseTree<T> current = nodeQueue.Dequeue();
				yield return current.Value;
				foreach (BaseTree<T> child in current.Children)
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

	public abstract class BaseBinaryTree<T> : BaseTree<T>
	{
		public BaseBinaryTree<T> FirstChild { get; }
		public BaseBinaryTree<T> SecondChild { get; }

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

		public new IEnumerable<BaseBinaryTree<T>> Children
		{
			get
			{
				yield return FirstChild;
				yield return SecondChild;
			}
		}
	}
}
