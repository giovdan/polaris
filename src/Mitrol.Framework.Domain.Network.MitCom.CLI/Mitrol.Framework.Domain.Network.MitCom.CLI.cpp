// Mitrol.MitCom.CLI.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"
#include "Mitrol.Framework.Domain.Network.MitCom.CLI.h"

int main()
{
	std::string input;

	std::cout << "Starting up... " << std::endl;

	SwitchCommand("usage");

	do
	{
		input.clear();
		std::cout << std::endl << ">";

		if (!getline(std::cin, input) || std::cin.eof()) {
			break;
		}
		else if (std::cin.fail()) {
			return false;
		}

		if (input.compare("exit") == 0)
			break;

		SwitchCommand(input);

	} while (true);

	return 0;
}

