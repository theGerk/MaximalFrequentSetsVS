#include "TransactionSet.h"
#include <vector>
#include <algorithm>

bool TransactionSet::RowType::operator>(const RowType& other) const
{
#ifndef UNSAFEMODE
	if (other.val.length() != val.length())
		throw std::out_of_range("");
#endif

	for (size_t i = 0; i < val.length(); i++)
	{
		if (val.get(i) != other.val.get(i))
			return val.get(i);
	}
	return false;
}

bool TransactionSet::RowType::operator<(const RowType& other) const
{
#ifndef UNSAFEMODE
	if (other.val.length() != val.length())
		throw std::out_of_range("");
#endif

	for (size_t i = 0; i < val.length(); i++)
	{
		if (val.get(i) != other.val.get(i))
			return other.val.get(i);
	}
	return false;
}

bool TransactionSet::RowType::operator==(const RowType& other) const
{
#ifndef UNSAFEMODE
	if (other.val.length() != val.length())
		throw std::out_of_range("");
#endif

	for (size_t i = 0; i < val.length(); i++)
	{
		if (val.get(i) != other.val.get(i))
			return false;
	}
	return true;
}

bool TransactionSet::RowType::operator>=(const RowType& other) const
{
#ifndef UNSAFEMODE
	if (other.val.length() != val.length())
		throw std::out_of_range("");
#endif

	for (size_t i = 0; i < val.length(); i++)
	{
		if (val.get(i) != other.val.get(i))
			return val.get(i);
	}
	return true;
}

bool TransactionSet::RowType::operator<=(const RowType& other) const
{
#ifndef UNSAFEMODE
	if (other.val.length() != val.length())
		throw std::out_of_range("");
#endif

	for (size_t i = 0; i < val.length(); i++)
	{
		if (val.get(i) != other.val.get(i))
			return other.val.get(i);
	}
	return true;
}

TransactionSet::TransactionSet(const Struct::RectangularArray<bool> & values, size_t minFrequency) :
	minimumForFrequency(minFrequency)
{
	std::vector<RowType> rows;

	for (size_t i = 0; i < values.rows(); i++)
	{
		Struct::Array<bool> myArr(values.arrayLiteral(i), values.columns());
	}
}
