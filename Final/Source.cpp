/*
* To change this license header, choose License Headers in Project Properties.
* To change this template file, choose Tools | Templates
* and open the template in the editor.
*/

/*
* File:   main.cpp
* Author: bendy
*
* Created on May 15, 2017, 2:24 PM
*/

#include <cstdlib>
#include <random>
#include <chrono>
#include <iostream>
#include <functional>
#include <algorithm>
#define TESTS(n) (n * 10000)

using namespace std;

struct ValueFrequencyPair {

	ValueFrequencyPair(size_t val, size_t freq) : val(val), freq(freq) {
	}

	ValueFrequencyPair() : val(0), freq(0) {
	}

	void set(size_t val, size_t freq) {
		this->val = val;
		this->freq = freq;
	}

	bool operator<(ValueFrequencyPair other) const {
		return freq < other.freq;
	}

	bool operator>(ValueFrequencyPair other) const {
		return freq > other.freq;
	}

	bool operator<=(ValueFrequencyPair other) const {
		return freq <= other.freq;
	}

	bool operator>=(ValueFrequencyPair other) const {
		return freq >= other.freq;
	}

	bool operator==(ValueFrequencyPair other) const {
		return freq == other.freq;
	}

	bool operator!=(ValueFrequencyPair other) const {
		return freq != other.freq;
	}

	ValueFrequencyPair operator=(ValueFrequencyPair other) {
		freq = other.freq;
		val = other.val;
		return other;
	}

	ValueFrequencyPair greatest(ValueFrequencyPair other) const {
		if (*this > other) {
			return *this;
		} else {
			return other;
		}
	}

	size_t val;
	size_t freq;
};

size_t** makeTestArrays(size_t n) {
	static auto randIndex = std::bind(std::uniform_int_distribution<size_t>(0, n - 1), std::mt19937_64(std::chrono::system_clock::now().time_since_epoch().count()));
	size_t** output = new size_t*[TESTS(n)];
	for (size_t i = 0; i < TESTS(n); i++) {
		size_t* current = new size_t[n];
		for (size_t i = 0; i < n; i++)
			current[i] = i;
		for (size_t i = 0; i < n; i++)
			std::swap(current[i], current[randIndex()]);
		output[i] = current;
	}
	return output;
}

size_t testKVal(size_t k, size_t n) {
	static size_t** testArrays = makeTestArrays(n);
	size_t output = 0;
	for (size_t i = 0; i < TESTS(n); i++) {
		size_t* current = testArrays[i];
		size_t greatest = 0;
		for (size_t i = 0; i < k; i++)
			if (current[i] > greatest)
				greatest = current[i];
		for (size_t i = k; i < n; i++)
			if (current[i] >= greatest) {
				output += current[i] == n - 1;
				break;
			}
	}
	std::cout << "K: " << k << "\t ==> " << output << std::endl;
	return output;
}

size_t findKVal(size_t n) {
	ValueFrequencyPair min(0, TESTS(n) / n);
	ValueFrequencyPair max(n - 1, TESTS(n) / n);
	ValueFrequencyPair guessA;
	ValueFrequencyPair guessB;


	for (size_t offset = (max.val - min.val) / 3; offset; offset = (max.val - min.val) / 3) {
		guessA.set(offset + min.val, testKVal(min.val + offset, n));
		guessB.set(offset + offset + min.val, testKVal(min.val + offset + offset, n));


		if (guessA <= guessB)
			min = guessA;
		if (guessA >= guessB)
			max = guessB;
	}


	ValueFrequencyPair greatest = min.greatest(max);
	for (size_t i = min.val + 1; i < max.val; i++) {
		greatest = greatest.greatest(ValueFrequencyPair(i, testKVal(i, n)));
	}

	return greatest.val;
}

int main(int argc, char** argv) {
	size_t n;
	std::cout << "Number of values to be input: ";
	std::cin >> n;

	size_t k = findKVal(n);

	std::cout << "Probably good k value: " << k << std::endl;

	std::cout << "Input values now:" << std::endl;

	//do computation
	double max = -INFINITY;
	size_t i = 0;
	for (double current; i < k; i++) {
		std::cin >> current;
		if (max < current)
			max = current;
	}
	for (double current; i < n; i++) {
		std::cin >> current;
		if (max < current) {
			std::cout << "Selected: " << current << std::endl;
			max = current;
			break;
		}
	}

	return 0;
}

