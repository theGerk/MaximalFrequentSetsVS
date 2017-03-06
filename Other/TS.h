#pragma once
#include <vector>
#include <algorithm>

class TransactionSet
{
	size_t* data;
	size_t at(size_t row, size_t col);
	bool exists(size_t row, const std::vector<size_t>& set);


public:
	TransactionSet(size_t* input);

	bool isFrequent(const std::vector<size_t>& set);
};

