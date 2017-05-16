#include <iostream>
#include <fstream>


using namespace std;

int main() {
	int count1 = 1, count2 = 0, sum_of_special_numbers = 0;
	int input_data[100][100];		//array for input file
	int sum[100];		//array for sum of each item that appears numbers
	int special[100];
	int threshold = 0;
	int max_special_counter;
	int combo[10];


	cout << "input threshold value?";
	cin >> threshold;

	ifstream myfile("input.txt");

	for (int transaction = 0; transaction < 100; transaction++)		//reads in the input file into 2d array
	{
		for (int item = 0; item < 100; item++) {
			if (!(myfile >> input_data[transaction][item])) {
				std::cerr << "error while reading file";
				break;
			}
			//cout <<input_data[transaction][item];
			/*if(item==99){
			cout << '\n';
			}*/
		}
		if (!myfile) break;
	}

	for (int i = 0; i<100; i++) {							// Initiates the array to 0 for each spot.
		sum[i] = 0;
	}

	for (int j = 0; j < 100; j++)					// This nested for loop sums the columns in the array data3
	{
		for (int i = 0; i < 100; i++) {
			if ((input_data[j][i] == 1)) {
				sum[i] = sum[i] + count1;
			} else {
				//	data3[j]=0;
			}
			//	   data3[j]=data3[j];
		}

	}

	for (int j = 0; j < 100; j++) {				// This is a test to see if the numbers match. J is row and 9 is the 9th column held constant
		if (input_data[j][90] == 1)
			count2 = count2 + 1;
	}

	for (int i = 0; i<100; i++)							// This prints out the special items in each row.
	{
		//sum_of_special_numbers=sum_of_special_numbers+1;
		if (sum[i] >= threshold) {
			special[sum_of_special_numbers] = i;
			sum_of_special_numbers++;
		}
	}
	//cout<<sum_of_special_numbers <<"is sum of special set";
	int temp;
	for (int i = 1; i<1024; i++) //generates all combinations of special numbers
	{
		int binary_array_input = 0;
		int n = 0;
		temp = i;

		while (n < sum_of_special_numbers) {
			binary_array_input = i % 2;   //puts 0 or 1 into the binary_array_input_variable
			combo[n] = binary_array_input;	//0 or 1 in input into the array
			i = i / 2;
			n++;						//array will grow to size 10 regardless of x value that was input
		}
		/*for(int h=0; h<10; h++)
		{
		cout<<combo[h];
		}
		cout<<endl;*/
		int match_counter = 0;
		for (int j = 0; j <100; j++) {

			for (int h = 0; h<sum_of_special_numbers; h++) {
				//checks for special sets in input data sets
				if (input_data[j][special[h]] == combo[h]) {
					match_counter++;
					if (match_counter == sum_of_special_numbers) {
						max_special_counter++;
						match_counter == 0;
					}
				}
			}


		}
		//if threshold is reached output set
		if (max_special_counter >= threshold) {
			cout << "[";
			for (int k = 0; k<sum_of_special_numbers; k++) {
				if (combo[k] == 1 && k != (sum_of_special_numbers - 1)) {
					cout << special[k] << " ";
				}
				if (k == (sum_of_special_numbers - 1) && combo[k] == 1) {
					cout << special[k];
				}
				if (k == (sum_of_special_numbers - 1)) {
					cout << "]" << endl;
				}
			}
			//cout << special[9]<< "]" <<" frequency is "<< max_special_counter << endl;
			max_special_counter = 0;
		}
		i = temp;
	}

}
