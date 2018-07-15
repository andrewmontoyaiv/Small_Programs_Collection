// ISBN.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>

using namespace std;

int main()
{
	int checkDigit = 0;
	int position = 1;
	int result = 0;
	char input;

	cout << "Enter an ISBN" << endl;
	input = cin.get();

	while (input != 10 && position <= 12) {
		if (position % 2) result += input * 3;
		else result += input * 1;

		input = cin.get();
		position++;
	}



	checkDigit = result % 10;

	if (checkDigit != 0) {
		result = abs(checkDigit - 10);
	}

	cout << "Check Digit is: " << result << endl;


	
	


    return 0;
}

