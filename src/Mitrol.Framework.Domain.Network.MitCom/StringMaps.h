#pragma once

#include "pch.h" // precompiled headers
#include "MitCom.h"

#include <unordered_map>

using namespace Mitrol::Framework::Domain::Network::MitCom;

#pragma region < RESULT >

struct ResultStringMap : public std::unordered_map<RESULT, const char*>
{
	ResultStringMap()
	{
		this->operator[](RESULT::FAIL) = "FAIL";
		this->operator[](RESULT::SUCCESS) = "SUCCESS";
		this->operator[](RESULT::UNRECOGNIZED_COMMAND) = "UNRECOGNIZED_COMMAND";
		this->operator[](RESULT::INCOMPLETE_DATA) = "INCOMPLETE_DATA";
		this->operator[](RESULT::INVALID_DATA) = "INVALID_DATA";
		this->operator[](RESULT::RISP_ERR_COMMAND_FAILED) = "RISP_ERR_COMMAND_FAILED";
		this->operator[](RESULT::RISP_ERR_TIMEOUT) = "RISP_ERR_TIMEOUT";
		this->operator[](RESULT::RISP_ERR_PLA_OFF) = "RISP_ERR_PLA_OFF";
		this->operator[](RESULT::IO_CONTROL_FAILED) = "IO_CONTROL_FAILED";
		this->operator[](RESULT::CONNECT_FAILED) = "CONNECT_FAILED";
		this->operator[](RESULT::INVALID_ADDRESS) = "INVALID_ADDRESS";
		this->operator[](RESULT::TIMEOUT) = "TIMEOUT";
		this->operator[](RESULT::BUFFER_OVERRUN) = "BUFFER_OVERRUN";
		this->operator[](RESULT::COMMAND_MISMATCH) = "COMMAND_MISMATCH";
		this->operator[](RESULT::PEEK_HEAD_FAIL) = "PEEK_HEAD_FAIL";
		this->operator[](RESULT::RECV_HEAD_FAIL) = "RECV_HEAD_FAIL";
		this->operator[](RESULT::PEEK_DATA_FAIL) = "PEEK_DATA_FAIL";
		this->operator[](RESULT::RECV_DATA_FAIL) = "RECV_DATA_FAIL";
	}
	~ResultStringMap() {}
} result_string_map;

std::ostream& operator<< (std::ostream& out, const RESULT& result)
{
	out << result_string_map[result];
	return out;
}

#pragma endregion

#pragma region < READ_COMMAND >

struct ReadCommandStringMap : public std::unordered_map<READ_COMMAND, const char*>
{
	ReadCommandStringMap()
	{
		this->operator[](READ_COMMAND::CONFIG_ETHERCAT) = "CONFIG_ETHERCAT";
		this->operator[](READ_COMMAND::SYNC_FLAG) = "SYNC_FLAG";
		this->operator[](READ_COMMAND::GEN_CONF) = "GEN_CONF";
		this->operator[](READ_COMMAND::GEN_CONF) = "RELE";
		this->operator[](READ_COMMAND::GEN_CONF) = "ALARM";
		this->operator[](READ_COMMAND::GEN_CONF) = "ACTIVE_NUM_ALL";
		this->operator[](READ_COMMAND::GEN_CONF) = "ACTIVE_PAR_ALL";
	}
	~ReadCommandStringMap() {}
} read_command_string_map;

std::ostream& operator<< (std::ostream& out, const READ_COMMAND& result)
{
	out << read_command_string_map[result];
	return out;
}

#pragma endregion

#pragma region < WRITE_COMMAND >

struct WriteCommandStringMap : public std::unordered_map<WRITE_COMMAND, const char*>
{
	WriteCommandStringMap()
	{
		this->operator[](WRITE_COMMAND::CONFIG_ETHERCAT) = "CONFIG_ETHERCAT";
		this->operator[](WRITE_COMMAND::SYNC_FLAG) = "SYNC_FLAG";
		this->operator[](WRITE_COMMAND::GEN_CONF) = "GEN_CONF";
	}
	~WriteCommandStringMap() {}
} write_command_string_map;

std::ostream& operator<< (std::ostream& out, const WRITE_COMMAND& result)
{
	out << write_command_string_map[result];
	return out;
}

#pragma endregion