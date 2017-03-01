#pragma once
#include "RectangularArray.h"

class TransactionSet
{
public:
	class RowType
	{
	public:
		Struct::Array<bool> val;
		size_t freq;

		bool operator>(const RowType& other) const;
		bool operator<(const RowType& other) const;
		bool operator==(const RowType& other) const;
		bool operator>=(const RowType& other) const;
		bool operator<=(const RowType& other) const;
	};

private:
	Struct::Array<RowType> data;
	size_t minimumForFrequency;

public:
	TransactionSet(const Struct::RectangularArray<bool> & values, size_t minFrequency);
	bool isFrequent() const;
	inline size_t columns() const;
};

