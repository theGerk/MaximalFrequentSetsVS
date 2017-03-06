#include<iostream>
#include<fstream>
#include<cstdlib>
#include<vector>

using namespace std;


const size_t numRows = 100;
const size_t numCols = 100;
const size_t OptRows = 10;
const size_t OptCols = 10;

size_t main(char** argv, int argc)
{
	printf("%s", argv[0]);
	vector<size_t> vCol, vRow;

	size_t matrix[numRows][numCols];
	size_t matrix2[OptRows][OptCols];
	ifstream input;
	input.open("input.txt");
	cout << (input.is_open());

	ofstream output("firstmatrix.txt");

	//24-30 fill matrix with input.txt file
	for (size_t n = 0; n < numRows; n++)
	{
		for (size_t i = 0; i < numCols; i++)
		{
			input >> matrix[n][i];
		}
	}

	input.close();

	size_t threshold_counter = 0;
	size_t threshold = 5;

	//38-57 Check if columns meet threshold and store them into a vector vCol
	for (size_t index = 0; index<numCols; index++)
	{
		threshold_counter = 0;

		for (size_t next = 0; next < numRows; next++)
		{

			if (matrix[next][index] == 1)
			{
				threshold_counter++;

				if (threshold_counter == threshold)
				{
					vCol.push_back(index);
					break;
				}

			}
		}
	}

	//60-70 are supposed to refine the 10x100 matrix to a 10x10
	for (size_t next = 0; next<numRows; next++)
	{
		for (size_t index = 0; index<vCol.size(); index++)
		{
			if (matrix[next][index] == 1)
			{
				vRow.push_back(next);
				break;
			}
		}
	}

	//73-81 put the refined 10x10 matrix into a new matrix		
	for (size_t row = 0; row < vRow.size(); row++)
	{
		for (size_t col = 0; col < vCol.size(); col++)
		{
			size_t x = vCol[col];
			size_t y = vRow[row];
			matrix2[row][col] = matrix[y][x];
		}
	}

	//84-86 just output the column numbers
	for (size_t i = 0; i < vCol.size(); i++)
		output << vCol[i] << " ";
	output << endl;


	//90-100 output the 10x10 matrix
	for (size_t row = 0; row < vRow.size(); row++)
	{
		output << endl;

		for (size_t j = 0; j < vCol.size(); j++)
		{
			//	size_t x = vCol[j];
			//	size_t y = vRow[row];
			output << matrix2[row][j] << "  ";
		}
	}
	return 0;
}


