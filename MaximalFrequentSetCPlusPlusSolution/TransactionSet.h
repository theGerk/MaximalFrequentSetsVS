#pragma once
#include "RectangularArray.h"
class TransactionSet :
	private RectangularArray<bool>
{
private:
	size_t minimumForFrequency;
public:
	TransactionSet();
};

