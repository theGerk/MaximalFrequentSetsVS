#pragma once

#ifndef UNSAFEMODE
#include <stdexcept>
#endif

#include <iostream>

template <typename T>
class RectangularArray
{
private:
	size_t r, c;
	T* data;	// [rows][columns]
	inline size_t position(size_t row, size_t column) const
	{
#ifndef UNSAFEMODE
		if (row >= r || column >= c)
			throw std::exception("");
#endif

		return row * c + column;
	}
public:
	RectangularArray(size_t rows, size_t columns);
	~RectangularArray();
	size_t rows() const;
	size_t columns() const;
	T & operator()(size_t row, size_t column);
	T get(size_t row, size_t column);
	void set(size_t row, size_t column, T value);
};
