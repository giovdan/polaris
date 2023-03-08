#pragma once

#include "..\Mitrol.Framework.Domain.Network.MitCom\MitCom.h"
#include "..\Mitrol.Framework.Domain.Network.MitCom\StringMaps.h"

//#include "magic_enum.hpp"
#include "DataStructures.h"

using namespace Mitrol::Framework::Domain::Network::MitCom;

std::vector<std::string> functionNames{
	"1 -> Connect [addr] [port] [timeout]",
	"2 -> Disconnect",
	"3 -> Write SYNC_FLAG [short value]",
	"4 -> Read SYNC_FLAG",
	"5 -> Write GEN_CONF",
	"6 -> Read GEN_CONF",
	"7 -> Write CONFIG_ETHERCAT",
	"8 -> Read CONFIG_ETHERCAT",
	"9 -> Write RELE [offset] [num]",
	"10 -> Read RELE [offset] [num]",
	"11 -> Read current alarm",
	"12 -> Resync ethercat",
	"13 -> Write PAR [INDEX] [VALUE]",
	"14 -> Write Plasma configuration",
	"15 -> Write Plasma parameter",
	"16 -> Write Plasma modality",
	"17 -> Read Plasma status",
	"18 -> Read Macro command",
	"19 -> Write Tool Fora",
	"20 -> Write Setup Fora",
	"100 -> Shut down AHMI",

	"log -> Show log entries" };

const char* default_pAddress = "10.0.0.71";
const unsigned short default_iPort = 4500;
const long default_timeout = 5;
const bool default_debugMode = true;

unsigned __int64 idSocket = 0;

void PrintErrors()
{
	char* logEntries[10];
	memset(logEntries, 0, sizeof(logEntries));

	if (GetLogEntries(logEntries, 10) > 0)
	{
		for (auto logEntry : logEntries)
		{
			if (logEntry == NULL)
				break;
			std::cout << logEntry << std::endl;
		}
	}
}

void Connect_Test(const std::vector<std::string>& args)
{
	// Default or takes the first argument as ip address
	auto pAddress = (args.size() > 1) ? args[1].c_str() : default_pAddress;

	// Default or takes the second argument as ip port
	unsigned short iPort = (args.size() > 2) ? (unsigned short)std::stoul(args[2]) : default_iPort;

	// Default or takes the third argument as timeout
	auto timeout = (args.size() > 3) ? std::stol(args[3]) : default_timeout;

	//// Default or takes the fourth argument as debugMode
	//bool debugMode = default_debugMode;
	//if (args.size() > 4)
	//{
	//	// if 1-0 notation
	//	if (args[4].length() == 1) std::istringstream(args[4]) >> debugMode;
	//	// or true-false
	//	else std::istringstream(args[4]) >> std::boolalpha >> debugMode;
	//}

	// Close existing connection if necessary
	if (idSocket == 0) Disconnect(&idSocket);

	std::cout << "Calling: Connect(addr: " << pAddress << ", port: " << iPort << ", timeout: " << timeout << /*", debugMode: " << debugMode << */")... ";

	auto result = Connect(pAddress, iPort, timeout, &idSocket);

	std::cout << result << " socket = " << idSocket << std::endl;
}

void Disconnect_Test(const std::vector<std::string>& args)
{
	std::cout << "Calling: Disconnect(sock: " << idSocket << ")... ";

	auto result = Disconnect(&idSocket);

	std::cout << result << std::endl;
}

void SendWriteSyncFlag(const std::vector<std::string>& args)
{
	short buff = (args.size() > 1) ? std::stoi(args[1]) : 0;
	std::cout << "Calling: SendWriteCommand(sock: " << idSocket << ", WRITE_COMMAND::SYNC_FLAG, " << buff << ", " << sizeof(buff) << ")... ";
	auto result = SendWriteCommand(idSocket, WRITE_COMMAND::SYNC_FLAG, &buff, sizeof(buff));

	std::cout << result << std::endl;
}

void SendReadSyncFlag(const std::vector<std::string>& args)
{
	std::cout << "Calling: SendReadCommand(sock: " << idSocket << ")... ";

	short buff;
	auto result = SendReadCommand(idSocket, READ_COMMAND::SYNC_FLAG, &buff, sizeof(buff));

	std::cout << result << " value: " << buff << std::endl;
}

void SendWriteGenConf(const std::vector<std::string>& args)
{
	SYS_GENERAL_CONF buff;
	buff.FlgEmulator = 3;
	buff.scansioneINOUT = 8;
	buff.scansioneIOMANAG = 2;
	buff.scansioneCanBusFAST = 2;
	buff.sysTimer = 1;


	std::cout << "Calling: SendWriteCommand(sock: " << idSocket << ", WRITE_COMMAND::GEN_CONF, )... ";
	auto result = SendWriteCommand(idSocket, WRITE_COMMAND::GEN_CONF, &buff, sizeof(buff));

	std::cout << result << std::endl;
}

void SendReadGenConf(const std::vector<std::string>& args)
{

	std::cout << "Calling: SendReadCommand(sock: " << idSocket << ")... ";
	SYS_GENERAL_CONF buff;
	auto result = SendReadCommand(idSocket, READ_COMMAND::GEN_CONF, &buff, sizeof(buff));

	std::cout << result << " Inout: " << buff.scansioneINOUT << " Fast: " << buff.scansioneIOMANAG << "Systimer: " << buff.sysTimer << std::endl;
}

void SendWriteEthercatConf(const std::vector<std::string>& args)
{
	// Default or takes the first argument as ip address

	ETHERCAT_INFO Etc;
	Etc.Pres = true;
	strcpy_s(Etc.PathEniFile, "/D/POLARIS/PLC/");
	Etc.Node[0].typeID = MOD_ETC_DS301;


	std::cout << "Calling: SendWriteCommand(sock: " << idSocket << ", WRITE_COMMAND::CONFIG_ETHERCAT, )... ";
	auto result = SendWriteCommand(idSocket, WRITE_COMMAND::CONFIG_ETHERCAT, &Etc, sizeof(Etc));

	std::cout << result << std::endl;
}

void SendReadEthercatConf(const std::vector<std::string>& args)
{

	std::cout << "Calling: SendReadCommand(sock: " << idSocket << ")... ";
	ETHERCAT_INFO Etc;
	auto result = SendReadCommand(idSocket, READ_COMMAND::CONFIG_ETHERCAT, &Etc, sizeof(Etc));

	std::cout << result << " Etc: Name=" << Etc.Node[0].devname << " Pres=" << Etc.Node[0].pres << " StrError=" << Etc.strError << std::endl;
}

void SendWriteRele(const std::vector<std::string>& args)
{
	char rele[4196];
	memset(rele, 0, sizeof(rele));
	rele[0] = 5;
	rele[1] = 6;


	short offs = (args.size() > 1) ? std::stoi(args[1]) : 0;
	short len = (args.size() > 2) ? std::stoi(args[2]) : sizeof(rele);

	std::cout << "Calling: SendWriteReleCommand(sock: " << idSocket << ", WRITE_COMMAND::RELE, )... ";
	auto result = SendWriteReleCommand(idSocket, WRITE_COMMAND::RELE, &rele[offs], len, offs);

	std::cout << result << std::endl;
}

void SendReadRele(const std::vector<std::string>& args)
{
	BYTE rele[4196];
	memset(rele, 0, sizeof(rele));

	short offs = (args.size() > 1) ? std::stoi(args[1]) : 0;
	short len = (args.size() > 2) ? std::stoi(args[2]) : sizeof(rele);

	std::cout << "Calling: SendReadReleCommand(sock: " << idSocket << ")... ";
	auto result = SendReadReleCommand(idSocket, READ_COMMAND::RELE, &rele, len, offs);

	std::cout << result << std::endl;
	if (result == RESULT::SUCCESS)
	{
		for (int i = 0; i < 10; ++i)
		{
			std::cout << std::endl << " rele[" << i << "]=" << (int)rele[i];
		}
	}
}

void SendWriteParameter(const std::vector<std::string>& args)
{
	short Index = (args.size() > 1) ? std::stoi(args[1]) : 0;
	float Value = (args.size() > 2) ? std::stof(args[2]) : 1.123;

	std::cout << "Calling: SendWriteParameterCommand(sock: " << idSocket << ", WRITE_COMMAND::PARAMETER, )... ";
	auto result = SendWriteParameterCommand(idSocket, WRITE_COMMAND::PARAMETER, Value, Index);

	std::cout << result << std::endl;
}


void SendReadMacro(const std::vector<std::string>& args)
{
	//short offs = (args.size() > 1) ? std::stoi(args[1]) : 0;
	//short len = (args.size() > 2) ? std::stoi(args[2]) : sizeof(rele);
	char retStr[32];
	strcpy_s(retStr, "");

	std::cout << "Calling: SendReadMacroCommand(sock: " << idSocket << ")... ";
	//auto result = SendReadMacroCommand(idSocket, READ_COMMAND::MACRO, retStr, 22, 1, 2, "DB[200.100.20,F]");
	//auto result = SendReadMacroCommand(idSocket, READ_COMMAND::MACRO, retStr, 22, 1, 101, "NP");
	auto result = SendReadMacroCommand(idSocket, READ_COMMAND::MACRO, retStr, 22, 1, 1, "NE");

	std::cout << result << " Valore macro=" << retStr << std::endl;
}

void SendWritePlasmaConfiguration(const std::vector<std::string>& args)
{
	unsigned short indTorcia = (args.size() > 1) ? std::stoi(args[1]) : 0;
	bool disattiva = (args.size() > 2) ? std::stoi(args[2]) : 0;
	unsigned char tipo = (args.size() > 3) ? std::stoi(args[3]) : 7;
	unsigned char etcCnfNode = (args.size() > 4) ? std::stoi(args[4]) : 5;

	std::cout << "Calling: SendWritePlasmaConfigurationCommand(sock: " << idSocket << ", WRITE_COMMAND::PLASMA_CONFIGURATION, )... ";
	auto result = SendWritePlasmaConfigurationCommand(idSocket, WRITE_COMMAND::PLASMA_CONFIGURATION, indTorcia, disattiva, tipo, etcCnfNode);

	std::cout << result << std::endl;
}

void SendWritePlasmaParameter(const std::vector<std::string>& args)
{
	unsigned short indTorcia = (args.size() > 1) ? std::stoi(args[1]) : 0;
	unsigned short ProcessId = (args.size() > 2) ? std::stoi(args[2]) : 1001;
	unsigned short Current = (args.size() > 3) ? std::stoi(args[3]) : 10;
	unsigned short Plasma = (args.size() > 4) ? std::stof(args[4]) : 20;
	unsigned short Shield = (args.size() > 5) ? std::stof(args[5]) : 30;
	unsigned short Pierce = (args.size() > 6) ? std::stof(args[6]) : 40;
	unsigned char anticipo  = (args.size() > 7) ? std::stof(args[7]) : 0;

	std::cout << "Calling: SendWritePlasmaParameterCommandXPR(sock: " << idSocket << ", WRITE_COMMAND::PLASMA_PARAMETER, )... ";
	auto result = SendWritePlasmaParameterCommandXPR(idSocket, WRITE_COMMAND::PLASMA_PARAMETER, indTorcia, ProcessId, Current, Plasma, Shield, Pierce, anticipo);

	std::cout << result << std::endl;
}

void SendWritePlasmaModality(const std::vector<std::string>& args)
{
	unsigned short indTorcia = (args.size() > 1) ? std::stoi(args[1]) : 0;
	unsigned short on = (args.size() > 2) ? std::stoi(args[2]) : 1;
	bool test = (args.size() > 3) ? std::stoi(args[3]) : 0;

	std::cout << "Calling: SendWritePlasmaModalityCommand(sock: " << idSocket << ", WRITE_COMMAND::PLASMA_MODALITY, )... ";
	auto result = SendWritePlasmaModalityCommand(idSocket, WRITE_COMMAND::PLASMA_MODALITY, indTorcia, on, test);

	std::cout << result << std::endl;
}

void SendReadPlasmaStatus(const std::vector<std::string>& args)
{
	unsigned short indTorcia = (args.size() > 1) ? std::stoi(args[1]) : 0;
	unsigned char statoPlasma[19];

	std::cout << "Calling: SendReadPlasmaStatusCommand(sock: " << idSocket << ")... ";
	auto result = SendReadPlasmaStatusCommand(idSocket, READ_COMMAND::PLASMA_STATUS, &statoPlasma, sizeof(statoPlasma), indTorcia);

	std::cout << result << std::endl;
	if (result == RESULT::SUCCESS)
	{
		for (int i = 0; i < 19; ++i)
		{
			std::cout << std::endl << " rele[" << i << "]=" << (int)statoPlasma[i];
		}
	}
}

void SendReadAlarm(const std::vector<std::string>& args)
{

	std::cout << "Calling: SendReadCommand(sock: " << idSocket << ")... ";
	ALL_DB CurrentAlarm;
	auto result = SendReadCommand(idSocket, READ_COMMAND::ALARM, &CurrentAlarm, sizeof(CurrentAlarm));

	std::cout << result << " Current alarm num=" << +CurrentAlarm.all_num << " Par=" << +CurrentAlarm.all_par << std::endl;
}

void SendWriteResync(const std::vector<std::string>& args)
{
	bool buff = true;
	std::cout << "Calling: SendWriteCommand(sock: " << idSocket << ", WRITE_COMMAND::RESYNC_ETC, )... ";
	auto result = SendWriteCommand(idSocket, WRITE_COMMAND::RESYNC_ETC, &buff, sizeof(buff));

	std::cout << result << std::endl;
}

void SendResetArm(const std::vector<std::string>& args)
{
	short buff = 0x5A5A;
	std::cout << "Calling: SendWriteCommand(sock: " << idSocket << ", WRITE_COMMAND::RESET_ARM, " << buff << ", " << sizeof(buff) << ")... ";
	auto result = SendWriteCommand(idSocket, WRITE_COMMAND::RESET_ARM, &buff, sizeof(buff));

	std::cout << result << std::endl;
}

void SendWriteToolFora(const std::vector<std::string>& args)
{
	STRU_ID_TOOL_F sup_f;
	memset(&sup_f, 0, sizeof(sup_f));
	sup_f.testa = (args.size() > 1) ? std::stoi(args[1]) : 0;
	sup_f.punta = (args.size() > 2) ? std::stoi(args[2]) : 0;
	sup_f.tool.ts = 1;
	//....
	sup_f.tool.dt = 1.23;

	std::cout << "Calling: SendWriteCommand(sock: " << idSocket << ", WRITE_COMMAND::TOOL_FORA... ";
	auto result = SendWriteCommand(idSocket, WRITE_COMMAND::TOOL_FORA, &sup_f, sizeof(sup_f));

	std::cout << result << std::endl;
}

void SendWriteSetupFora(const std::vector<std::string>& args)
{
	STRU_ID_SETUP_F sup_f;
	memset(&sup_f, 0, sizeof(sup_f));
	sup_f.testa = (args.size() > 1) ? std::stoi(args[1]) : 0;
	sup_f.punta = (args.size() > 2) ? std::stoi(args[2]) : 0;
	sup_f.data.cut = 11;
	sup_f.data.Stato = true;
	sup_f.data.IndTool = 21;
	sup_f.data.MaxLut = 3.45;


	std::cout << "Calling: SendWriteCommand(sock: " << idSocket << ", WRITE_COMMAND::TOOL_FORA... ";
	auto result = SendWriteCommand(idSocket, WRITE_COMMAND::SETUP_FORA, &sup_f, sizeof(sup_f));

	std::cout << result << std::endl;
}

void PrintStringVector(std::ostream& output_stream, std::vector<std::string> string_vector)
{
	for (auto item : string_vector) {
		output_stream << item << std::endl;
	}
}

void PrintFunctionNames(std::ostream& output_stream)
{
	PrintStringVector(output_stream, functionNames);
}

std::vector<std::string> Split(std::string str, std::string sep) {
	char* string_to_break = const_cast<char*>(str.c_str());
	char* next_token = NULL;
	std::vector<std::string> substrings;
	char* current_token = strtok_s(string_to_break, sep.c_str(), &next_token);
	while (current_token != NULL) {
		substrings.push_back(current_token);
		current_token = strtok_s(NULL, sep.c_str(), &next_token);
	}
	return substrings;
}

void PrintUsage()
{
	std::cout << "Usage:" << std::endl << "Choose a function and enter the corrisponding number followed by space separated input parameters." << std::endl;
	std::cout << "Type \"exit\" to terminate the application or \"usage\" to print this again." << std::endl << std::endl;

	std::cout << std::endl << "Here's the list of exposed functions:" << std::endl << std::endl;
	PrintFunctionNames(std::cout);
}

void SwitchCommand(const std::string& command)
{
	std::cout << std::endl;
	std::vector<std::string> args = Split(command, " ");

	if (args[0].compare("usage") == 0) {
		PrintUsage();
	}
	else if (args[0].compare("log") == 0) {
		PrintErrors();
	}
	else if (args[0].compare("1") == 0) {
		Connect_Test(args);
	}
	else if (args[0].compare("2") == 0) {
		Disconnect_Test(args);
	}
	else if (args[0].compare("3") == 0) {
		SendWriteSyncFlag(args);
	}
	else if (args[0].compare("4") == 0) {
		SendReadSyncFlag(args);
	}
	else if (args[0].compare("5") == 0) {
		SendWriteGenConf(args);
	}
	else if (args[0].compare("6") == 0) {
		SendReadGenConf(args);
	}
	else if (args[0].compare("7") == 0) {
		SendWriteEthercatConf(args);
	}
	else if (args[0].compare("8") == 0) {
		SendReadEthercatConf(args);
	}
	else if (args[0].compare("9") == 0) {
		SendWriteRele(args);
	}
	else if (args[0].compare("10") == 0) {
		SendReadRele(args);
	}
	else if (args[0].compare("11") == 0) {
		SendReadAlarm(args);
	}
	else if (args[0].compare("12") == 0) {
		SendWriteResync(args);
	}
	else if (args[0].compare("13") == 0) {
		SendWriteParameter(args);
	}
	else if (args[0].compare("14") == 0) {
		SendWritePlasmaConfiguration(args);
	}
	else if (args[0].compare("15") == 0) {
		SendWritePlasmaParameter(args);
	}
	else if (args[0].compare("16") == 0) {
		SendWritePlasmaModality(args);
	}
	else if (args[0].compare("17") == 0) {
		SendReadPlasmaStatus(args);
	}
	else if (args[0].compare("18") == 0) {
		SendReadMacro(args);
	}
	else if (args[0].compare("19") == 0) {
		SendWriteToolFora(args);
	}
	else if (args[0].compare("20") == 0) {
		SendWriteSetupFora(args);
	}
	else if (args[0].compare("100") == 0) {
		SendResetArm(args);
	}
	else {
		std::cout << "ERROR: Please enter a valid value. Type \"usage\" for more info." << std::endl;
	}
}
