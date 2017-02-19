#include "RectangularArray.h"

template<typename T>
RectangularArray<T>::RectangularArray(size_t rows, size_t columns)
{
	r = rows;
	c = columns;
	data = new T[rows*columns];
}

template<typename T>
RectangularArray<T>::~RectangularArray()
{
	std::cout << "hit";
	delete[] data;
}

template<typename T>
size_t RectangularArray<T>::rows() const
{
	return r;
}

template<typename T>
size_t RectangularArray<T>::columns() const
{
	return c;
}

template<typename T>
T & RectangularArray<T>::operator()(size_t row, size_t column)
{
	return data[position(row, column)];
}

template<typename T>
T RectangularArray<T>::get(size_t row, size_t column)
{
	return data[position(row,column)];
}

template<typename T>
void RectangularArray<T>::set(size_t row, size_t column, T value)
{
	data[position(row, column)] = value;
}
