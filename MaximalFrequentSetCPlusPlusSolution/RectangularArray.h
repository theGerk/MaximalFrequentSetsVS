#pragma once

#ifndef UNSAFEMODE
#include <stdexcept>
#endif

#include <iostream>
#include "Array.h"

namespace Struct
{
	template <typename T>
	class RectangularArray :
		private Array<T>
	{
	private:
		size_t c;

		inline size_t position(size_t row, size_t column) const
		{
#ifndef UNSAFEMODE
			if (column >= c)
				throw std::out_of_range("");
#endif
			return row * c + column;
		}

	public:
		RectangularArray() :
			c(0),
			Array<T>() {}

		RectangularArray(size_t rows, size_t columns) :
			c(columns),
			Array<T>(rows * columns) {}

		RectangularArray(const T* copyFrom, size_t rows, size_t columns) :
			c(columns),
			Array<T>(copyFrom, rows * columns) {}

		RectangularArray(const T** copyFrom, size_t rows, size_t columns) :
			c(columns),
			Array<T>(rows * columns)
		{
			const size_t rowsize = c * sizeof(T);
			for (size_t i = 0; i < rows; i++)
				memcpy(Array<T>::arrayLiteral() + i * c, copyFrom[i], rowsize);
		}

		RectangularArray(const Array<T> & copyFrom, size_t rows, size_t columns) :
			c(columns)
		{
			const size_t totalLength = rows * columns;
#ifndef UNSAFEMODE
			if(copyFrom.length < totalLength)
				throw std::out_of_range("copyFrom array is to small");
#endif
			memcpy(Array<T>::arrayLiteral(), copyFrom.arrayLiteral(), totalLength);
		}

		RectangularArray(const Array<Array<T>> & copyFrom, size_t rows, size_t columns) :
			c(columns)
		{
#ifndef UNSAFEMODE
			if (rows > copyFrom.length())
				throw std::out_of_range("rows > copyFrom.length()");
			for (size_t i = 0; i < copyFrom.length(); i++)
				if (columns > copyFrom[i].length())
					throw std::out_of_range("columns > copyFrom[" + i + "].length()");
#endif
			const size_t rowsize = c * sizeof(T);
			for (size_t i = 0; i < rows; i++)
				memcpy(Array<T>::arrayLiteral() + i * c, copyFrom[i].arrayLitteral(), rowsize);
		}

		void reconstruct(size_t rows, size_t columns)
		{
			c = columns;
			Array<T>::reconstruct(rows * columns);
		}

		void reconstruct(const T* copyFrom, size_t rows, size_t columns)
		{
			c = columns;
			Array<T>::reconstruct(copyFrom, rows * columns);
		}

		void reconstruct(const T** copyFrom, size_t rows, size_t columns)
		{
			c = columns;
			Array<T>::reconstruct(rows * columns);
			const size_t rowsize = c * sizeof(T);
			for (size_t i = 0; i < rows; i++)
				memcpy(Array<T>::arrayLiteral + i * c, copyFrom[i], rowsize);
		}

		void reconstruct(const Array<T> & copyFrom, size_t rows, size_t columns)
		{
			c = columns;
			const size_t totalLength = rows * columns;
#ifndef UNSAFEMODE
			if (copyFrom.length < totalLength)
				throw std::out_of_range("copyFrom array is to small");
#endif
			memcpy(Array<T>::arrayLiteral(), copyFrom.arrayLiteral(), totalLength);
		}

		void reconstruct(const Array<Array<T>> & copyFrom, size_t rows, size_t columns)
		{
			c = columns;
#ifndef UNSAFEMODE
			if (rows > copyFrom.length())
				throw std::out_of_range("rows > copyFrom.length()");
			for (size_t i = 0; i < copyFrom.length(); i++)
				if (columns > copyFrom[i].length())
					throw std::out_of_range("columns > copyFrom[" + i + "].length()");
#endif
			const size_t rowsize = c * sizeof(T);
			for (size_t i = 0; i < rows; i++)
				memcpy(Array<T>::arrayLiteral() + i * c, copyFrom[i].arrayLitteral(), rowsize);
		}

		inline size_t rows() const { return Array<T>::length() / c; }
		inline size_t columns() const { return c; }
		inline T & at(size_t row, size_t column) { return Array<T>::at(position(row, column)); }
		inline T get(size_t row, size_t column) const { return Array<T>::get(position(row, column)); }
		inline void set(size_t row, size_t column, T value) { Array<T>::set(position(row, column),value); }
		inline T* arrayLiteral() const { return Array<T>::arrayLiteral(); }
		inline T* arrayLiteral(size_t row) const { return Array<T>::arrayLiteral() + c * row; }
	};
}
