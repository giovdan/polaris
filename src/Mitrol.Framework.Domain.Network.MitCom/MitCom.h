//-----------------------------------------------------------------------------
// COPYRIGHT (C) 2016  MITROL  S.r.l.
//
// ALL RIGHTS RESERVED.
//
// NO PART OF THIS PROGRAM OR PUBLICATION MAY BE REPRODUCED, TRANSMITTED,
// TRANSCRIBED, STORED IN A RETRIEVAL SYSTEM, OR TRANSLATED INTO ANY
// LANGUAGE OR COMPUTER LANGUAGE, IN ANY FORM OR BY ANY MEANS, ELECTRONIC,
// MANUAL OR OTHERWISE, WITHOUT THE PRIOR WRITTEN CONSENT OF THE ABOVE COMPANY.
//-----------------------------------------------------------------------------
#pragma once


#ifndef _MITROL_MITCOM_H_
#define _MITROL_MITCOM_H_

#ifndef MITCOMAPI // Macro for dllexport/dllimport switching.
#ifdef MITCOM_EXPORTS
#define MITCOMAPI __declspec(dllexport)
#else
#define MITCOMAPI __declspec(dllimport)
#endif // MITCOM_EXPORTS
#endif

namespace Mitrol {
	namespace Framework {
		namespace Domain {
			namespace Network {
				namespace MitCom {

					const int kMaxLogEntries = 500;

					enum class RESULT : unsigned short
					{
						FAIL = 0,
						SUCCESS = 1,
						UNRECOGNIZED_COMMAND = 2,       // Comando non riconosciuto
						INCOMPLETE_DATA = 3,			// Lunghezza contenuta nel comando diversa da lunghezza ricevuta
						INVALID_DATA = 4,				// Dimensione dei dati ricevuti diversa dalla dimensione di destinazione
			            RISP_ERR_COMMAND_FAILED = 5,    // Errore comando non riuscito
						RISP_ERR_TIMEOUT = 6,			// Errore timeout
						RISP_ERR_PLA_OFF = 7,			// Comando non eseguibile per apparecchiatura plasma spenta
						IO_CONTROL_FAILED,
						CONNECT_FAILED,
						INVALID_ADDRESS,
						TIMEOUT,
						BUFFER_OVERRUN,
						COMMAND_MISMATCH,
						PEEK_HEAD_FAIL,
						RECV_HEAD_FAIL,
						PEEK_DATA_FAIL,
						RECV_DATA_FAIL
					};

					enum class READ_COMMAND : unsigned short
					{
						MEM_MAP_SIZE = 0, // w32 in bytes
						CONFIG_ETHERCAT = 1,
						SYNC_FLAG = 2,
						GEN_CONF = 3,
						RELE = 4,
						ALARM = 5,
						PLASMA_STATUS = 6,
						CONFSW = 7,
						REFRESH_DATA = 8,
						MACRO = 9,

					};

					enum class WRITE_COMMAND : unsigned short
					{
						CONFIG_ETHERCAT = 32001,
						SYNC_FLAG = 32002,
						GEN_CONF = 32003,
			            RELE = 32004,
			            RESYNC_ETC =32005,
			            RESET_ARM =32006,
			            PARAMETER = 32007,
			            PLASMA_CONFIGURATION = 32008,
			            PLASMA_PARAMETER = 32009,
						PLASMA_MODALITY = 32010,
						TOOL_FORA = 32014,
						SETUP_FORA = 32015
					};

					extern "C" MITCOMAPI RESULT WINAPI Connect(const char* pAddress, unsigned short iPort, long timeout, unsigned __int64* idSocket);
					extern "C" MITCOMAPI RESULT WINAPI Disconnect(unsigned __int64* idSocket);

					extern "C" MITCOMAPI RESULT WINAPI SendReadCommand(unsigned __int64 idSocket, READ_COMMAND commandId, void *pData, unsigned short dataLen);
					extern "C" MITCOMAPI RESULT WINAPI SendWriteCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId, const void *pData, unsigned short dataLen);

					extern "C" MITCOMAPI RESULT WINAPI SendReadReleCommand(unsigned __int64 idSocket, READ_COMMAND commandId, void* pData, unsigned short dataLen, unsigned short offset);
					extern "C" MITCOMAPI RESULT WINAPI SendWriteReleCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId, const void* pData, unsigned short dataLen, unsigned short offset);

					extern "C" MITCOMAPI RESULT WINAPI SendReadMacroCommand(unsigned __int64 idSocket, READ_COMMAND commandId, void* retStr, unsigned short retStrLen, unsigned short numAll, unsigned short parAll, const void* strMacro);

					extern "C" MITCOMAPI RESULT WINAPI SendWriteParameterCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId, float value, unsigned short index, unsigned short parameterType);

					extern "C" MITCOMAPI RESULT WINAPI SendWriteAxisConfigurationCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId,
						unsigned short id, unsigned char type, unsigned char index, unsigned char bus, unsigned char node,
						unsigned char channel, unsigned char encsync, unsigned short analogInputIndex, unsigned short analogOutputIndex);

					extern "C" MITCOMAPI RESULT WINAPI SendWriteAxisJogCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId, unsigned char index);

					extern "C" MITCOMAPI RESULT WINAPI SendWriteAxisDisplacementCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId, float displacement);

					extern "C" MITCOMAPI RESULT WINAPI SendWriteAxisOverrideCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId, short value);

					extern "C" MITCOMAPI RESULT WINAPI SendWriteSpindlesOverrideCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId, short value);

					extern "C" MITCOMAPI RESULT WINAPI SendWritePlasmaConfigurationCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId, unsigned short torchId, bool disable, unsigned char torchType, unsigned char etcCnfNode, bool logEnable);

					extern "C" MITCOMAPI RESULT WINAPI SendWritePlasmaParameterCommandXPR(unsigned __int64 idSocket, WRITE_COMMAND commandId, unsigned short torchId, unsigned short processId, unsigned short current, unsigned short plasma, unsigned short shield, unsigned short pierce, unsigned char anticipo);

					extern "C" MITCOMAPI RESULT WINAPI SendWritePlasmaParameterCommandHPR(unsigned __int64 idSocket, WRITE_COMMAND commandId, unsigned short indTorcia, unsigned short Current, unsigned short GasPlasma, unsigned short GasShield, unsigned short PreFlowPlasma, unsigned short PreFlowShield, unsigned short CutFlowPlasma, unsigned short CutFlowShield, unsigned short MixGas1, unsigned short MixGas2, unsigned char anticipo);

					extern "C" MITCOMAPI RESULT WINAPI SendWritePlasmaModalityCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId, unsigned short indTorcia, unsigned char on, bool test);

					extern "C" MITCOMAPI RESULT WINAPI SendReadPlasmaStatusCommand(unsigned __int64 idSocket, READ_COMMAND commandId, void* pData, unsigned short dataLen, unsigned short indTorcia);
		
					extern "C" MITCOMAPI RESULT WINAPI SendReadAxisQuotas(unsigned __int64 idSocket, READ_COMMAND commandId, void* pData, unsigned short dataLen);

					extern "C" MITCOMAPI int WINAPI GetLogEntries(char* ppLogEntries[], int logEntriesSize);

					extern "C" MITCOMAPI RESULT WINAPI SendWriteDrillToolInSpindleCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId, unsigned char testa, unsigned char utmand);

					extern "C" MITCOMAPI RESULT WINAPI SendReadDrillToolInSpindleCommand(unsigned __int64 idSocket, READ_COMMAND commandId, void* pData, unsigned short dataLen, unsigned char testa);

					extern "C" MITCOMAPI RESULT WINAPI SendReadDrillToolLifeCommand(unsigned __int64 idSocket, READ_COMMAND commandId, void* pData, unsigned short dataLen, unsigned char testa, unsigned char punta);

					extern "C" MITCOMAPI RESULT WINAPI SendReadSawToolLifeCommand(unsigned __int64 idSocket, READ_COMMAND commandId, void* pData, unsigned short dataLen);

					extern "C" MITCOMAPI RESULT WINAPI SendReadSawRodaggioCommand(unsigned __int64 idSocket, READ_COMMAND commandId, void* pData, unsigned short dataLen);

				} // namespace MitCom
			} // namespace Network
		} // namespace Domain
	} // namespace Framework
} // namespace Mitrol

#endif // !_MITROL_MITCOM_H_
