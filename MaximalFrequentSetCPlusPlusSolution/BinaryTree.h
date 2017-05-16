#pragma once

namespace Structures
{
	template<typename T>
	class ValueCount
	{
	public:
		T value;
		size_t count;

		ValueCount(T val, size_t cnt) :
			value(val),
			count(cnt) {}
	};


		template<typename T>
		class BinaryTreeBase
		{
		protected:
			T value;
			BinaryTreeBase<T>* left;
			BinaryTreeBase<T>* right;

		public:
			BinaryTreeBase(T val) :
				value(val),
				left(nullptr),
				right(nullptr) {}

			void setLeft(BinaryTreeBase<T>* ptr) { left = ptr; }
			void setRight(BinaryTreeBase<T>* ptr) { right = ptr; }
		};

}