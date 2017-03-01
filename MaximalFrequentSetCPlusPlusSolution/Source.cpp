
#include <iostream>
#include "TransactionSet.h"
#include <vector>


int main()
{
	Struct::RectangularArray<bool> rect(4, 10);
	bool my[4][10] = {
		0, 1, 0, 1,
		1, 0, 0, 0,
		1, 1, 0, 1,
		0, 0, 1, 0,
		0, 0, 1, 0,
		0, 1, 0, 1,
		1, 0, 1, 0,
		0, 1, 1, 0,
		0, 1, 0, 1,
		0, 1, 1, 1
	};
	for (size_t i = 0; i < rect.rows(); i++)
		for (size_t j = 0; j < rect.columns(); j++)
			rect.set(i, j, my[i][j]);
	TransactionSet set(rect, 3);
	return 0;
}

