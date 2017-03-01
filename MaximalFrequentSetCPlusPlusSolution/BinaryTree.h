#pragma once

namespace Struct
{ 
	template<typename T>
	class BinaryTree
	{
	protected:
		T value;
		BinaryTree<T>* left;
		BinaryTree<T>* right;

	public:
		BinaryTree(T val) :
			value(val),
			left(nullptr),
			right(nullptr) {}

		void setLeft(BinaryTree<T>* ptr) { left = ptr; }
		void setRight(BinaryTree<T>* ptr) { right = ptr; }
	};
}