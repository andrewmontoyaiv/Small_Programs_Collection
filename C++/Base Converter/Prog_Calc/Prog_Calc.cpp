// Prog_Calc.cpp : Converts a number with any base to base 10
//

#include "stdafx.h"
#include <iostream>
#include <string>
#include <math.h>
#include <list>
#include <cmath>

using namespace std;


int main()
{

	int number = 0;
	int value = 0;
	int nBase = 10;
	int oBase = 10;
	int position = 0;
	int decimalPlace = 0;

	string valueGap = "";


	std::cout << "input nBase: ";
	cin >> nBase;

	std::cout << "input oBase: ";
	cin >> oBase;

	cin.ignore(numeric_limits<streamsize>::max(), '\n');
	std::cout << "input number: ";

	char input = 0;
	bool foundDec = false;
	input = cin.get();
	while (input != 10) {

		if (input > '@') {
			number = (number * 100) + (input - 55);
			valueGap += '2';
			position++;

			if (foundDec) {
				decimalPlace++;
			}
		}
		else if (input == '.') {
			foundDec = true;
		}	
		else {
			number = (number * 10) + (input - '0'); // calculate corrent value gap
			valueGap += '1';
			position++;

			if (foundDec) {
				decimalPlace++;
			}
		}
		input = cin.get();
	}
	
	
	int gap = 0;
	for (int i = 0; i < valueGap.length(); i++) {
		gap += valueGap[i] - '0';
	}



	int decExp = 1;
	double basemod = 1;
	double runningval = 0;

	double currentnum;
	double nextnum = number; 

	int position_till_decimal = position - decimalPlace;
	std::cout << position << " " << position_till_decimal << endl;

	for (int i = 0; i < gap; i++) {
		basemod *= 10;
	}
	std::cout << "basemod: " << basemod << endl;

	for (int i = 0, exp = position - 1; i < position; exp--, position_till_decimal--, i++) {

		currentnum = nextnum;


		for (int j = 0; j < valueGap[i] - '0'; j++) { 
			basemod = basemod / 10;
		}

		nextnum = std::fmod(currentnum, basemod);
		int temp = currentnum - nextnum;


		if (position_till_decimal <= 0) {
			runningval = runningval + ((temp / basemod) / pow(oBase, decExp));

			for (int j = 0; j < valueGap[i] - '0'; j++) {

				decExp++;
			}
		}
		else {
			runningval = runningval + ((temp / basemod) * pow(oBase, (exp - decimalPlace)));
		}

	}
	std::cout << runningval << endl;

	double wholeValue = (int)runningval;

	string convertedVal = "";


	for (double i = wholeValue; i > 1; ) {

		char temp = (int)std::fmod(i, nBase);
		cout << (int)temp << endl;


		if (temp > 9) {
			temp = '@' + (temp - 9);
		}
		else {
			temp = '0' + temp;
		}
		convertedVal += temp;

		i = i / nBase;

	}


	std::cout << convertedVal << endl;


	int cValLength = convertedVal.length() - 1;
	for (int i = 0; i < (cValLength + 1) / 2; i++) {
		int temp = convertedVal[i];
		convertedVal[i] = convertedVal[cValLength - i];
		convertedVal[cValLength - i] = temp;
	}


	double decValue = runningval - (int)wholeValue;
	if (decExp != 0) {
		convertedVal += '.';

		std::cout << "HI" << decValue << endl;

		int temp = (int)(decExp - 1);
		std::cout << "HELLO" << temp << endl;
		for (int i = 0; i < temp; i++) {

			decValue *= 10;
		}
		std::cout << "HI" << decValue << endl;


		for (double i = decValue; i > 1; ) {


			char temp = (int)std::fmod(i, nBase);
			cout << (int)temp << endl;


			if (temp > 9) {
				temp = '@' + (temp - 9);
			}
			else {
				temp = '0' + temp;
			}
			convertedVal += temp;

			i = i / nBase;

		}


	}




	std::cout << convertedVal << endl;


    return 0;
}

