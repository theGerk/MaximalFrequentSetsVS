#include "TS.h"


TransactionSet::TransactionSet(size_t* input)
	: data(input) { }

inline bool TransactionSet::isFrequent(const std::vector<size_t>& set)
{
	size_t count = 0;
	for (size_t i = 0; i < 10; i++)
	{
		count += exists(i, set);
	}
	return count >= 5;
}

inline size_t TransactionSet::at(size_t row, size_t col)
{
	return data[row * 10 + col];
}

inline bool TransactionSet::exists(size_t row, const std::vector<size_t>& set)
{
	for (size_t i = 0; i < set.size(); i++)
	{
		if (!at(row, set[i]))
			return false;
	}
	return true;
}
