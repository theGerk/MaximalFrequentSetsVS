#pragma once
#include <cstring>

#ifndef UNSAFEMODE
#include <stdexcept>
#endif

namespace Struct
{
	template<typename T>
	class Array
	{
	private:
		T* data;
		size_t _length;

#ifndef UNSAFEMODE
		inline void saftyCheck(const size_t pos)
		{
			if (pos >= length)
				throw std::out_of_range("");
		}
#endif

	public:
		Array() :
			data(nullptr),
			_length(0) {}

		Array(const size_t size) :
			data(new T[size]),
			_length(size) {}

		Array(const T* copyFrom, const size_t size) :
			data(new T[size]),
			_length(size)
		{
			memcpy(data, copyFrom, size * sizeof(T));
		}

		~Array()
		{
			delete[] data;
		}

		void reconstruct(const size_t size)
		{
			delete[] data;
			data = new T[size];
			_length = size;
		}

		void reconstruct(const T* copyFrom, const size_t size)
		{
			delete[] data;
			_length = size;
			data = new T[size];
			memcpy(data, copyFrom, size * sizeof(T));
		}

		inline T & operator[](const size_t position)
		{
#ifndef UNSAFEMODE
			saftyCheck(position);
#endif
			return data[position];
		}

		inline T & at(const size_t position)
		{
#ifndef UNSAFEMODE
			saftyCheck(position);
#endif
			return data[position];
		}

		inline T get(const size_t position) const 
		{
#ifndef UNSAFEMODE
			saftyCheck();
#endif
			return data[position];
		}

		inline void set(const size_t position, const T & val)
		{
#ifndef UNSAFEMODE
			saftyCheck(position);
#endif
			data[position] = val;
		}

		inline T* arrayLiteral() const { return data; }
		inline size_t length() const { return _length; }
	};
}
