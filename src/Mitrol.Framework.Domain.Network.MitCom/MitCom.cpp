//-----------------------------------------------------------------------------
// COPYRIGHT (C) 2020  MITROL  S.r.l.
//
// ALL RIGHTS RESERVED.
//
// NO PART OF THIS PROGRAM OR PUBLICATION MAY BE REPRODUCED, TRANSMITTED,
// TRANSCRIBED, STORED IN A RETRIEVAL SYSTEM, OR TRANSLATED INTO ANY
// LANGUAGE OR COMPUTER LANGUAGE, IN ANY FORM OR BY ANY MEANS, ELECTRONIC,
// MANUAL OR OTHERWISE, WITHOUT THE PRIOR WRITTEN CONSENT OF THE ABOVE COMPANY.
//-----------------------------------------------------------------------------

#include "pch.h" // precompiled headers
#include "MitCom.h"
#include "StringMaps.h"

namespace Mitrol {
	namespace Framework {
		namespace Domain {
			namespace Network {
				namespace MitCom {

					// Dimensioni della testa del messaggio da inviare al server.
					const int kMessageTxHeadLength = 4;

					// Dimensioni della testa del messaggio da ricevere dal server.
					const int kMessageRxHeadLength = 6;

					// Dimensioni massime dei messaggi con i client (0x4000 (16k) + 16 bytes di testa del messaggio).
					const int kTransmissionBufferSize = 0x4010;

					// Tempo di attesa nel loop di ricezione dei dati.
					const int kRxLoopSleepMilliseconds = 1;

					// %c = Local date and time
					const char* kLogDateTimeFormat = "[%Y/%m/%d %H:%M:%S]: ";

#pragma region < Log management >

					// Buffer "circolare" dei messaggi di log.
					std::deque<std::string> _logBuffer(0);

					// Formatta una stringa con date e ora corrente.
					std::string get_date_time(const std::string format) {
						struct tm newtime;
						auto now = time(nullptr);
						localtime_s(&newtime, &now);
						char buf[1024];
						strftime(buf, sizeof(buf) - 1, format.c_str(), &newtime);
						return std::string(buf);
					}

					// Inserisce un messaggio formattato nel buffer di log.
					template<typename ... Args>
					void PushLogMessage(const std::string format, Args ... args)
					{
						if (_logBuffer.size() == kMaxLogEntries) {
							_logBuffer.erase(_logBuffer.begin());
						}

						std::ostringstream oss;
						oss << "%s " << format;
						auto finalFormat = oss.str();
						auto dateTimeText = get_date_time(kLogDateTimeFormat);

						size_t size = snprintf(nullptr, 0, finalFormat.c_str(), dateTimeText.c_str(), args ...) + (size_t)1; // Extra space for '\0'

						if (size <= 0) {
							throw std::runtime_error("Error during formatting.");
						}

						std::unique_ptr<char[]> buf(new char[size]);

						snprintf(buf.get(), size, finalFormat.c_str(), dateTimeText.c_str(), args ...);

						std::string finalText = std::string(buf.get(), buf.get() + size - 1); // We don't want the '\0' inside

						_logBuffer.push_back(finalText);
					}

#pragma endregion

#pragma region < internal methods >

					// Buffer di trasmissione (usato in trasmissione e ricezione).
					char _transmissionBuffer[kTransmissionBufferSize];

					// Creazione ed inizializzazione dell'oggetto sockaddr_in con le informazioni dell'indirizzo del server.
					RESULT AddressAndPortToStructure(const char* ip_address, unsigned short iPort, sockaddr_in* address_struct)
					{

						RESULT function_result = RESULT::INVALID_ADDRESS;

						//#if _DEBUG
						//			PushLogMessage("Gathering server address details.");
						//#endif

						if (ip_address != NULL)
						{
#pragma warning(disable : 4996) // wstring_convert is deprecated
							// Converto la stringa alla codifica appropriata
							// per poter convertire l'indirizzo IP da "Dotted Decimal Notation" a numerico.
							static std::wstring_convert<std::codecvt_utf8_utf16<wchar_t>> _converter;
							std::wstring wAddress = _converter.from_bytes(ip_address);
#pragma warning(default : 4996)

							// Tento di convertire l'indirizzo IP.
							if (InetPton(AF_INET, wAddress.c_str(), &address_struct->sin_addr.S_un.S_addr) != 1) {
								function_result = RESULT::INVALID_ADDRESS;
								PushLogMessage("%s: Address %s is invalid.", result_string_map[function_result], ip_address);
							}
							else {
								// Imposto gli altri campi della struttura.
								address_struct->sin_port = htons(iPort);
								address_struct->sin_family = AF_INET;
								function_result = RESULT::SUCCESS;
							}
						}

						return function_result;
					}

					// Abilitazione/disabilitazione della modalità bloccante per il socket specificato.
					RESULT EnableBlockingMode(SOCKET socket, bool enable)
					{
						RESULT function_result = RESULT::FAIL;

						//#if _DEBUG
						//			PushLogMessage("%s blocking mode on socket: %d.", enable ? "Enabling" : "Disabling", socket);
						//#endif

						// Se iMode == 0, blocking; 
						// Se iMode != 0, non-blocking.
						unsigned long iMode = (enable) ? 1 : 0;

						// Imposta la modalità I/O del socket.
						if (ioctlsocket(socket, FIONBIO, &iMode) == NO_ERROR) {
							function_result = RESULT::SUCCESS;
						}
						else {
							function_result = RESULT::IO_CONTROL_FAILED;
							PushLogMessage("%s: Cannot set I/O mode.", result_string_map[function_result]);
						}

						return function_result;
					}

					// Stabilimento della connessione con il server.
					RESULT Connect(SOCKET socket, sockaddr_in address_struct, long timeout)
					{
						RESULT function_result = RESULT::FAIL;

						int socket_error = 0;

						//#if _DEBUG
						//			PushLogMessage("Connecting to server on socket: %d.", socket);
						//#endif

						// Invio la richiesta di connessione al server.
						auto connect_result = connect(socket, (struct sockaddr*)&address_struct, sizeof(sockaddr_in));
						if (connect_result < 0)
						{
							// Verifico che la funzione connect abbia fallito 
							// perché il nuovo socket non è ancora disponibile.
							socket_error = WSAGetLastError();
							if (socket_error == WSAEWOULDBLOCK) {

								// File descriptor per verificare la validità del socket.
								fd_set wait_set;
								FD_ZERO(&wait_set);
								FD_SET(socket, &wait_set);

								// Timeout entro il quale il socket deve essere pronto.
								TIMEVAL timeval;
								timeval.tv_sec = timeout;
								timeval.tv_usec = 0;

								// Usando il socket in modalità NON-bloccante, devo attendere
								// che la connessione venga stabilita.
								// Nota1: timeout infinito in DEBUG mode.
								// Nota2: Nel caso in cui ci sia un altro software che intercetta la connessione (3proxy),
								//		select restituirà esito positivo anche quando il server è spento/non presente.
								connect_result = select(0, NULL, &wait_set, NULL, /*(_debugMode) ? NULL : */&timeval);
							}
						}

						// Controllo la presenza di un eventuale errore.
						if (connect_result < 0) {
							function_result = RESULT::FAIL;

							PushLogMessage("%s: Server unreachable. Error code: %d"
								, result_string_map[function_result]
								, socket_error);
						}
						else if (connect_result == 0) {
							function_result = RESULT::TIMEOUT;

							PushLogMessage("%s: Server unreachable."
								, result_string_map[function_result]);
						}
						else {
							function_result = RESULT::SUCCESS;
						}

						return function_result;
					}

					// Verifica che i dati possano essere copiati all'interno del buffer di trasmissione.
					RESULT PreventBufferOverrun(unsigned short dataLen)
					{
						RESULT function_result = RESULT::FAIL;

						//#if _DEBUG
						//			PushLogMessage("Checking data size [%d bytes].", dataLen);
						//#endif

						// Controllo se lunghezza dati da scrivere eccede il buffer.
						if (kMessageTxHeadLength + dataLen > kTransmissionBufferSize) {
							function_result = RESULT::BUFFER_OVERRUN;

							PushLogMessage("%s: Unable to send. Data is greather than %d bytes."
								, result_string_map[function_result]
								, kTransmissionBufferSize);
						}
						else {
							function_result = RESULT::SUCCESS;
						}

						return function_result;
					}

					// Invio di un comando di scrittura dati al server.
					RESULT SendWriteCmd(SOCKET socket, unsigned short commandId, const void* pData, unsigned short dataLen)
					{
						RESULT function_result = RESULT::FAIL;

						// Scrittura della lungezza del dato nei primi 2 byte.
						*(unsigned short*)&_transmissionBuffer[0] = dataLen;

						// Scrittura dell'id comando nei 2 byte seguenti.
						*(unsigned short*)&_transmissionBuffer[2] = commandId;

						// Scrittura del payload con la struttura da inviare.
						memcpy(&_transmissionBuffer[kMessageTxHeadLength], pData, dataLen);

						// Invio del messaggio.
						if (send(socket, _transmissionBuffer, kMessageTxHeadLength + dataLen, 0) == SOCKET_ERROR) {
							int socket_error = WSAGetLastError();
							function_result = RESULT::FAIL;

							PushLogMessage("%s: Message not sent. Error code: %d"
								, result_string_map[function_result]
								, socket_error);
						}
						else {
							function_result = RESULT::SUCCESS;
						}

						return function_result;
					}

					// Invio di un comando di lettura dati dal server.
					// I comandi di lettura trasmettono la testa (4 byte) + 2 byte di dati conteneti la lunghezza attesa dei dati da leggere
					RESULT SendReadCmd(SOCKET socket, unsigned short commandId, const void* pData, unsigned short dataLen)
					{
						RESULT function_result = RESULT::FAIL;

						// Scrittura della lungezza del dato nei primi 2 byte.
						*(unsigned short*)&_transmissionBuffer[0] = 2;

						// Scrittura dell'id comando nei 2 byte seguenti.
						*(unsigned short*)&_transmissionBuffer[2] = commandId;

						// Scrittura della lunghezza dei dati attesa nei primi due byte dati.
						*(unsigned short*)&_transmissionBuffer[kMessageTxHeadLength] = dataLen;

						// Invio del messaggio.
						if (send(socket, _transmissionBuffer, kMessageTxHeadLength + 2, 0) == SOCKET_ERROR) {
							int socket_error = WSAGetLastError();
							function_result = RESULT::FAIL;

							PushLogMessage("%s: Message not sent. Error code: %d"
								, result_string_map[function_result]
								, socket_error);
						}
						else {
							function_result = RESULT::SUCCESS;
						}

						return function_result;
					}

					// Attesa e lettura dati dal buffer di ricezione.
					RESULT WaitForData(SOCKET socket, unsigned short dataLength)
					{
						RESULT function_result = RESULT::FAIL;

						int recv_result = 0;
						while (recv_result < dataLength)
						{
							// Testo che arrivi almeno la testa del messaggio
							recv_result = recv(socket, _transmissionBuffer, dataLength, MSG_PEEK);

							if (recv_result < 0)
							{
								function_result = RESULT::PEEK_DATA_FAIL;
								break;
							}

							if (recv_result >= dataLength)
								break;

							Sleep(kRxLoopSleepMilliseconds);
						}

						// Leggo solo la testa del messaggio ricevuto
						recv_result = recv(socket, _transmissionBuffer, dataLength, 0);
						function_result = (recv_result == SOCKET_ERROR) ? RESULT::RECV_DATA_FAIL : RESULT::SUCCESS;

						return function_result;
					}

					// Attesa e lettura dei dell'intestazione del messaggio dal buffer di ricezione.
					RESULT WaitForMessageHead(SOCKET socket)
					{
						RESULT function_result = RESULT::FAIL;

						auto result = WaitForData(socket, kMessageRxHeadLength);

						function_result = (result == RESULT::PEEK_DATA_FAIL) ? RESULT::PEEK_HEAD_FAIL
							: (result == RESULT::RECV_DATA_FAIL) ? RESULT::RECV_HEAD_FAIL
							: result;

						return function_result;
					}

					RESULT CheckResponseResult(char* buffer, short expectedCommand)
					{
						RESULT function_result = RESULT::FAIL;

						auto command = *(short*)&buffer[2];
						if (command != expectedCommand)
							return RESULT::COMMAND_MISMATCH;

						return *(RESULT*)&buffer[4];
					}

#pragma endregion

#pragma region < public methods >

					extern "C" MITCOMAPI RESULT WINAPI Connect(const char* pAddress, unsigned short iPort, long timeout, unsigned __int64* idSocket)
					{
						RESULT function_result = RESULT::FAIL;

						PushLogMessage("Trying to connect to %s:%i.", pAddress, iPort);

						// Inizializzazione della struttura sockaddr_in con le informazioni passate.
						sockaddr_in addr_srv;
						if ((function_result = AddressAndPortToStructure(pAddress, iPort, &addr_srv)) == RESULT::SUCCESS) {

							// Apertura del canale socket.
							// Nota: La connessione non è ancora stata stabilita.
							auto socketId = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

							// Abilitazione della modalità bloccante
							if ((function_result = EnableBlockingMode(socketId, /*enable =*/ true)) == RESULT::SUCCESS) {

								// Stabilisce della connessione.
								if ((function_result = Connect(socketId, addr_srv, timeout)) == RESULT::SUCCESS) {

									// Disabilitazione della modalità bloccante
									if ((function_result = EnableBlockingMode(socketId, /*enable =*/ false)) == RESULT::SUCCESS) {

										// Assegno l'id del socket per tornare il valore al chiamante.
										*idSocket = socketId;

										function_result = RESULT::SUCCESS;

										PushLogMessage("Connection established.", socketId);
									}
								}
							}
						}

						return function_result;
					}

					extern "C" MITCOMAPI RESULT WINAPI Disconnect(unsigned __int64* idSocket)
					{
						if (idSocket == 0)
							return RESULT::SUCCESS;

						RESULT function_result = RESULT::FAIL;

						auto socket = (SOCKET)*idSocket;

						if (shutdown(socket, SD_BOTH) != SOCKET_ERROR)
						{
							if (closesocket(socket) != SOCKET_ERROR)
							{
								PushLogMessage("Disconnected socket: %d.", *idSocket);
								function_result = RESULT::SUCCESS;
							}
						}

						if (function_result != RESULT::SUCCESS)
						{
							int socket_error = WSAGetLastError();

							PushLogMessage("%s: Unable to close socket: %d. Error code: %d"
								, result_string_map[function_result]
								, *idSocket
								, socket_error);
						}

						*idSocket = 0;

						return function_result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendReadCommand(unsigned __int64 idSocket, READ_COMMAND commandId, void* pData, unsigned short dataLen)
					{
						RESULT result;
						auto command = (unsigned short)commandId;

						PushLogMessage("Sending command %s on socket: %d."
							, read_command_string_map[commandId]
							, idSocket);

						// Controllo se lunghezza dati da scrivere eccede il buffer
						if ((result = PreventBufferOverrun(dataLen)) == RESULT::SUCCESS)
						{
							if ((result = SendReadCmd(idSocket, command, pData, dataLen)) == RESULT::SUCCESS)
							{
								if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
								{
									if ((result = CheckResponseResult(_transmissionBuffer, command)) == RESULT::SUCCESS)
									{
										auto rxDataLen = *(unsigned short*)&_transmissionBuffer[0];

										if ((result = WaitForData(idSocket, rxDataLen)) == RESULT::SUCCESS)
										{
											memcpy(pData, &_transmissionBuffer, dataLen);
										}
									}
								}
							}
						}

						return result;
					}
					
					extern "C" MITCOMAPI RESULT WINAPI SendWriteCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId, const void* pData, unsigned short dataLen)
					{
						RESULT result;
						auto command = (unsigned short)commandId;

						PushLogMessage("Sending command %s on socket %d."
							, write_command_string_map[commandId]
							, idSocket);

						// Controllo se lunghezza dati da scrivere eccede il buffer
						if ((result = PreventBufferOverrun(dataLen)) == RESULT::SUCCESS)
						{
							if ((result = SendWriteCmd(idSocket, command, pData, dataLen)) == RESULT::SUCCESS)
							{
								if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
								{
									result = CheckResponseResult(_transmissionBuffer, command);
								}
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendReadReleCommand(unsigned __int64 idSocket, READ_COMMAND commandId, void* pData, unsigned short dataLen, unsigned short offset)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 4;

						// Controllo se lunghezza dati che riverò eccede il buffer
						if ((result = PreventBufferOverrun(dataLen)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza del dato =  2 byte per numero di relè da leggere + 2 byte per offset.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura nei dati del numero di relè da leggere
							*(unsigned short*)&_transmissionBuffer[4] = dataLen;

							// Scrittura nei dati dell'offset dal quale iniziare a leggere i relè
							*(unsigned short*)&_transmissionBuffer[6] = offset;

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
								result = RESULT::FAIL;
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								if ((result = CheckResponseResult(_transmissionBuffer, command)) == RESULT::SUCCESS)
								{
									auto rxDataLen = *(unsigned short*)&_transmissionBuffer[0];

									if ((result = WaitForData(idSocket, rxDataLen)) == RESULT::SUCCESS)
									{
										memcpy(pData, &_transmissionBuffer, dataLen);
									}
								}
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendWriteReleCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId, const void* pData, unsigned short dataLen, unsigned short offset)
					{
						RESULT result;
						auto command = (unsigned short)commandId;

						// Controllo se lunghezza dati da scrivere eccede il buffer
						if ((result = PreventBufferOverrun(dataLen + 2)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza del dato + lunghezza offset nei primi 2 byte.
							*(unsigned short*)&_transmissionBuffer[0] = dataLen + 2;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura dell'offset al quale scrivere i relè nei primi 2 byte di dati 
							*(unsigned short*)&_transmissionBuffer[4] = offset;

							// Scrittura del payload con la struttura da inviare.
							memcpy(&_transmissionBuffer[kMessageTxHeadLength + 2], pData, dataLen);

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLen + 2, 0) == SOCKET_ERROR)
							{
								result = RESULT::FAIL;
							}
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								result = CheckResponseResult(_transmissionBuffer, command);
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendReadMacroCommand(unsigned __int64 idSocket, READ_COMMAND commandId, void* retStr, unsigned short retStrLen, unsigned short numAll, unsigned short parAll, const void* strMacro)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 32;

						// Controllo se lunghezza dati che riceverò eccede il buffer
						if ((result = PreventBufferOverrun(retStrLen)) == RESULT::SUCCESS)
						{
							// Scrittura della lunghezza del dato =  2*len str resa + 2*num all + 2*par all + 26*len str comando.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura nei dati della lunghezza massima della stringa da rendere
							*(unsigned short*)&_transmissionBuffer[4] = retStrLen;

							// Scrittura nei dati del numero dell'allarme
							*(unsigned short*)&_transmissionBuffer[6] = numAll;

							// Scrittura nei dati del parametro dell'allarme
							*(unsigned short*)&_transmissionBuffer[8] = parAll;

							// Scrittura nei dati del parametro dell'allarme
							strcpy_s((char*)&_transmissionBuffer[10], 26, (char*)strMacro);

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
								result = RESULT::FAIL;
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								if ((result = CheckResponseResult(_transmissionBuffer, command)) == RESULT::SUCCESS)
								{
									auto rxDataLen = *(unsigned short*)&_transmissionBuffer[0];

									if ((result = WaitForData(idSocket, rxDataLen)) == RESULT::SUCCESS)
									{
										strcpy_s((char*)retStr, retStrLen, (char*)&_transmissionBuffer);
									}
								}
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendWriteParameterCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId, const float Valore, unsigned short index, unsigned short parameterType)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 8;

						// Controllo se lunghezza dati da scrivere eccede il buffer
						if ((result = PreventBufferOverrun(8)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza del dato + lunghezza offset nei primi 2 byte.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura del tipo di parametro da scrivere nei primi 2 byte di dati (GEN=1, TMP=2, BAR=3)
							*(unsigned short*)&_transmissionBuffer[4] = (unsigned short)parameterType;

							// Scrittura dell'indice del parametro da scrivere nei successivi 2 byte di dati 
							*(unsigned short*)&_transmissionBuffer[6] = index;

							// Scrittura del payload con la struttura da inviare.
							memcpy(&_transmissionBuffer[kMessageTxHeadLength + 4], &Valore, 4);

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
							{
								result = RESULT::FAIL;
							}
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								result = CheckResponseResult(_transmissionBuffer, command);
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendWriteAxisConfigurationCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId,
									unsigned short id, unsigned char type, unsigned char index, unsigned char bus, unsigned char node,
									unsigned char channel, unsigned char encsync, unsigned short analogInputIndex, unsigned short analogOutputIndex)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 12;

						// Controllo se lunghezza dati da scrivere eccede il buffer
						if ((result = PreventBufferOverrun(dataLength)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza dei dati trasmessi nei primi 2 byte.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura dell'Id dell'asse nei primi 2 byte di dati ed i successivi dati
							*(unsigned short*)&_transmissionBuffer[4] = (unsigned short)id;
							*(unsigned char*)&_transmissionBuffer[6] = type;
							*(unsigned char*)&_transmissionBuffer[7] = index;
							*(unsigned char*)&_transmissionBuffer[8] = bus;
							*(unsigned char*)&_transmissionBuffer[9] = node;
							*(unsigned char*)&_transmissionBuffer[10] = channel;
							*(unsigned char*)&_transmissionBuffer[11] = encsync;
							*(unsigned short*)&_transmissionBuffer[12] = analogInputIndex;
							*(unsigned short*)&_transmissionBuffer[14] = analogOutputIndex;

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
							{
								result = RESULT::FAIL;
							}
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								result = CheckResponseResult(_transmissionBuffer, command);
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendWriteAxisJogCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId, unsigned char index)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 1;

						// Controllo se lunghezza dati da scrivere eccede il buffer
						if ((result = PreventBufferOverrun(dataLength)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza dei dati trasmessi nei primi 2 byte.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura dell'indice dell'asse in JOG nel primo byte di dati
							*(unsigned char*)&_transmissionBuffer[4] = index;

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
							{
								result = RESULT::FAIL;
							}
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								result = CheckResponseResult(_transmissionBuffer, command);
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendWriteAxisDisplacementCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId, float displacement)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 4;

						// Controllo se lunghezza dati da scrivere eccede il buffer
						if ((result = PreventBufferOverrun(dataLength)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza dei dati trasmessi nei primi 2 byte.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura del valore del displacement nei primi 4 bytes di dati (float)
							*(float*)&_transmissionBuffer[4] = displacement;

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
							{
								result = RESULT::FAIL;
							}
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								result = CheckResponseResult(_transmissionBuffer, command);
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendWriteAxisOverrideCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId, short value)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 2;

						// Controllo se lunghezza dati da scrivere eccede il buffer
						if ((result = PreventBufferOverrun(dataLength)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza dei dati trasmessi nei primi 2 byte.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura del valore del feed nei primi 2 bytes di dati
							*(short*)&_transmissionBuffer[4] = value;

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
							{
								result = RESULT::FAIL;
							}
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								result = CheckResponseResult(_transmissionBuffer, command);
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendWriteSpindlesOverrideCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId, short value)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 2;

						// Controllo se lunghezza dati da scrivere eccede il buffer
						if ((result = PreventBufferOverrun(dataLength)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza dei dati trasmessi nei primi 2 byte.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura del valore del feed nei primi 2 bytes di dati
							*(short*)&_transmissionBuffer[4] = value;

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
							{
								result = RESULT::FAIL;
							}
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								result = CheckResponseResult(_transmissionBuffer, command);
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendWritePlasmaConfigurationCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId, unsigned short indTorcia, bool disattiva, unsigned char tipo, unsigned char etcCnfNode, bool logEnable)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 6;

						// Controllo se lunghezza dati da scrivere eccede il buffer
						if ((result = PreventBufferOverrun(dataLength)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza dei dati trasmessi nei primi 2 byte.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura dell'indTorcia nei primi 2 byte di dati ed i successivi dati
							*(unsigned short*)&_transmissionBuffer[4] = indTorcia;
							*(unsigned short*)&_transmissionBuffer[6] = disattiva;
							*(unsigned short*)&_transmissionBuffer[7] = tipo;
							*(unsigned short*)&_transmissionBuffer[8] = etcCnfNode;
							*(unsigned short*)&_transmissionBuffer[9] = logEnable;

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
							{
								result = RESULT::FAIL;
							}
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								result = CheckResponseResult(_transmissionBuffer, command);
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendWritePlasmaParameterCommandXPR(unsigned __int64 idSocket, WRITE_COMMAND commandId, unsigned short indTorcia, unsigned short ProcessId, unsigned short Current, unsigned short Plasma, unsigned short Shield, unsigned short Pierce, unsigned char anticipo)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 13;

						// Controllo se lunghezza dati da scrivere eccede il buffer
						if ((result = PreventBufferOverrun(dataLength)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza dei dati trasmessi nei primi 2 byte.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura dell'indTorcia nei primi 2 byte di dati ed i successivi dati
							*(unsigned short*)&_transmissionBuffer[4] = indTorcia;
							*(unsigned short*)&_transmissionBuffer[6] = ProcessId;
							*(unsigned short*)&_transmissionBuffer[8] = Current;
							*(unsigned short*)&_transmissionBuffer[10] = Plasma;
							*(unsigned short*)&_transmissionBuffer[12] = Shield;
							*(unsigned short*)&_transmissionBuffer[14] = Pierce;
							*(unsigned char*)&_transmissionBuffer[16] = anticipo;


							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
							{
								result = RESULT::FAIL;
							}
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								result = CheckResponseResult(_transmissionBuffer, command);
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendWritePlasmaParameterCommandHPR(unsigned __int64 idSocket, WRITE_COMMAND commandId, unsigned short indTorcia, unsigned short Current, unsigned short GasPlasma, unsigned short GasShield, unsigned short PreFlowPlasma, unsigned short PreFlowShield, unsigned short CutFlowPlasma, unsigned short CutFlowShield, unsigned short MixGas1, unsigned short MixGas2, unsigned char anticipo)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 21;

						// Controllo se lunghezza dati da scrivere eccede il buffer
						if ((result = PreventBufferOverrun(dataLength)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza dei dati trasmessi nei primi 2 byte.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura dell'indTorcia nei primi 2 byte di dati ed i successivi dati
							*(unsigned short*)&_transmissionBuffer[4] = indTorcia;
							*(unsigned short*)&_transmissionBuffer[6] = Current;
							*(unsigned short*)&_transmissionBuffer[8] = GasPlasma;
							*(unsigned short*)&_transmissionBuffer[10] = GasShield;
							*(unsigned short*)&_transmissionBuffer[12] = PreFlowPlasma;
							*(unsigned short*)&_transmissionBuffer[14] = PreFlowShield;
							*(unsigned short*)&_transmissionBuffer[16] = CutFlowPlasma;
							*(unsigned short*)&_transmissionBuffer[18] = CutFlowShield;
							*(unsigned short*)&_transmissionBuffer[20] = MixGas1;
							*(unsigned short*)&_transmissionBuffer[22] = MixGas2;
							*(unsigned char*)&_transmissionBuffer[24] = anticipo;

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
							{
								result = RESULT::FAIL;
							}
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								result = CheckResponseResult(_transmissionBuffer, command);
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendWritePlasmaModalityCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId, unsigned short indTorcia, unsigned char on, bool test)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 4;

						// Controllo se lunghezza dati da scrivere eccede il buffer
						if ((result = PreventBufferOverrun(dataLength)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza dei dati trasmessi nei primi 2 byte.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura dell'indTorcia nei primi 2 byte di dati ed i successivi dati
							*(unsigned short*)&_transmissionBuffer[4] = indTorcia;
							*(unsigned short*)&_transmissionBuffer[6] = on;
							*(unsigned short*)&_transmissionBuffer[7] = test;

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
							{
								result = RESULT::FAIL;
							}
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								result = CheckResponseResult(_transmissionBuffer, command);
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendWriteCommandOnly(unsigned __int64 idSocket, WRITE_COMMAND commandId)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 0;

						// Controllo se lunghezza dati da scrivere eccede il buffer
						if ((result = PreventBufferOverrun(dataLength)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza dei dati trasmessi nei primi 2 byte.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
							{
								result = RESULT::FAIL;
							}
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								result = CheckResponseResult(_transmissionBuffer, command);
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendWritePlcPanelsBool(unsigned __int64 idSocket, WRITE_COMMAND commandId, byte index, bool set)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 2;

						// Controllo se lunghezza dati da scrivere eccede il buffer
						if ((result = PreventBufferOverrun(dataLength)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza dei dati trasmessi nei primi 2 byte.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura dei dati
							*(unsigned short*)&_transmissionBuffer[4] = index;
							*(unsigned short*)&_transmissionBuffer[5] = set;

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
							{
								result = RESULT::FAIL;
							}
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								result = CheckResponseResult(_transmissionBuffer, command);
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendWritePlcPanelsFloat(unsigned __int64 idSocket, WRITE_COMMAND commandId, byte index, float value)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 2;

						// Controllo se lunghezza dati da scrivere eccede il buffer
						if ((result = PreventBufferOverrun(dataLength)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza dei dati trasmessi nei primi 2 byte.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura dei dati
							*(float*)&_transmissionBuffer[4] = value;
							*(unsigned short*)&_transmissionBuffer[8] = index;

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
							{
								result = RESULT::FAIL;
							}
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								result = CheckResponseResult(_transmissionBuffer, command);
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendReadPlasmaStatusCommand(unsigned __int64 idSocket, READ_COMMAND commandId, void* pData, unsigned short dataLen, unsigned short indTorcia)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 4;

						// Controllo se lunghezza dati che riverò eccede il buffer
						if ((result = PreventBufferOverrun(dataLen)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza del dato =  2 byte per indTorcia + 2 byte len dati da leggere.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura della lunghezza dei dati da leggere
							*(unsigned short*)&_transmissionBuffer[4] = dataLen;

							// Scrittura nei dati dell'indTorcia
							*(unsigned short*)&_transmissionBuffer[6] = indTorcia;

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
								result = RESULT::FAIL;
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								if ((result = CheckResponseResult(_transmissionBuffer, command)) == RESULT::SUCCESS)
								{
									auto rxDataLen = *(unsigned short*)&_transmissionBuffer[0];

									if ((result = WaitForData(idSocket, rxDataLen)) == RESULT::SUCCESS)
									{
										memcpy(pData, &_transmissionBuffer, dataLen);
									}
								}
							}
						}

						return result;
					}

					extern "C" MITCOMAPI int WINAPI GetLogEntries(char* ppLogEntries[], int logEntriesSize)
					{
						int count = 0;
						STRSAFE_LPSTR temp;
						size_t alloc_size;

						for (auto entry : _logBuffer)
						{
							alloc_size = entry.length() + 1;
							temp = (STRSAFE_LPSTR)CoTaskMemAlloc(alloc_size);
							if(temp != NULL) StringCchCopyA(temp, alloc_size, (STRSAFE_LPCSTR)entry.c_str());
							CoTaskMemFree(ppLogEntries[count]);
							ppLogEntries[count] = (char*)temp;
							++count;
							if (count == logEntriesSize)
								break;
						}

						return count;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendReadAxisQuotas(unsigned __int64 idSocket, READ_COMMAND commandId, void* pData, unsigned short dataLen)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						auto dataLength = 2;

						// Controllo se lunghezza dati che riverò eccede il buffer
						if ((result = PreventBufferOverrun(dataLen)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza del dato =  2 byte per numero di quote assi da leggere
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura della lunghezza dei dati da leggere
							*(unsigned short*)&_transmissionBuffer[4] = dataLen;

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
								result = RESULT::FAIL;
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								if ((result = CheckResponseResult(_transmissionBuffer, command)) == RESULT::SUCCESS)
								{
									auto rxDataLen = *(unsigned short*)&_transmissionBuffer[0];

									if ((result = WaitForData(idSocket, rxDataLen)) == RESULT::SUCCESS)
									{
										memcpy(pData, &_transmissionBuffer, dataLen);
									}
								}
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendWriteDrillToolInSpindleCommand(unsigned __int64 idSocket, WRITE_COMMAND commandId, unsigned char testa, unsigned char utmand)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 2;

						// Controllo se lunghezza dati da scrivere eccede il buffer
						if ((result = PreventBufferOverrun(dataLength)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza dei dati trasmessi nei primi 2 byte.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura dell'indice della testa nel primo byte di dati
							*(unsigned char*)&_transmissionBuffer[4] = testa;
							// Scrittura dell'indice dell'utensile nel mandrino nel secondo byte di dati
							*(unsigned char*)&_transmissionBuffer[5] = utmand;

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
							{
								result = RESULT::FAIL;
							}
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								result = CheckResponseResult(_transmissionBuffer, command);
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendReadDrillToolInSpindleCommand(unsigned __int64 idSocket, READ_COMMAND commandId, void* pData, unsigned short dataLen, unsigned char testa)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 3;

						// Controllo se lunghezza dati che riverò eccede il buffer
						if ((result = PreventBufferOverrun(dataLen)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza del dato =  2 byte per testa + 2 byte len dati da leggere.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura della lunghezza dei dati da leggere
							*(unsigned short*)&_transmissionBuffer[4] = dataLen;

							// Scrittura nei dati dell'indice della testa
							*(unsigned char*)&_transmissionBuffer[6] = (unsigned char)testa;

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
								result = RESULT::FAIL;
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								if ((result = CheckResponseResult(_transmissionBuffer, command)) == RESULT::SUCCESS)
								{
									auto rxDataLen = *(unsigned short*)&_transmissionBuffer[0];

									if ((result = WaitForData(idSocket, rxDataLen)) == RESULT::SUCCESS)
									{
										memcpy(pData, &_transmissionBuffer, dataLen);
									}
								}
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendReadDrillToolLifeCommand(unsigned __int64 idSocket, READ_COMMAND commandId, void* pData, unsigned short dataLen, unsigned char testa, unsigned char punta)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 4;

						// Controllo se lunghezza dati che riverò eccede il buffer
						if ((result = PreventBufferOverrun(dataLen)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza del dato =  1 byte per testa + 1 byte per punta + 2 byte len dati da leggere.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura della lunghezza dei dati da leggere
							*(unsigned short*)&_transmissionBuffer[4] = dataLen;

							// Scrittura nei dati dell'indice della testa
							*(unsigned char*)&_transmissionBuffer[6] = testa;
							// Scrittura nei dati dell'indice della punta
							*(unsigned char*)&_transmissionBuffer[7] = punta;

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
								result = RESULT::FAIL;
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								if ((result = CheckResponseResult(_transmissionBuffer, command)) == RESULT::SUCCESS)
								{
									auto rxDataLen = *(unsigned short*)&_transmissionBuffer[0];

									if ((result = WaitForData(idSocket, rxDataLen)) == RESULT::SUCCESS)
									{
										memcpy(pData, &_transmissionBuffer, dataLen);
									}
								}
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendReadSawToolLifeCommand(unsigned __int64 idSocket, READ_COMMAND commandId, void* pData, unsigned short dataLen)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 4;

						// Controllo se lunghezza dati che riceverò eccede il buffer
						if ((result = PreventBufferOverrun(dataLen)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza del dato = 2 byte len dati da leggere.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura della lunghezza dei dati da leggere
							*(unsigned short*)&_transmissionBuffer[4] = dataLen;

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
								result = RESULT::FAIL;
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								if ((result = CheckResponseResult(_transmissionBuffer, command)) == RESULT::SUCCESS)
								{
									auto rxDataLen = *(unsigned short*)&_transmissionBuffer[0];

									if ((result = WaitForData(idSocket, rxDataLen)) == RESULT::SUCCESS)
									{
										memcpy(pData, &_transmissionBuffer, dataLen);
									}
								}
							}
						}

						return result;
					}

					extern "C" MITCOMAPI RESULT WINAPI SendReadSawRodaggioCommand(unsigned __int64 idSocket, READ_COMMAND commandId, void* pData, unsigned short dataLen)
					{
						RESULT result;
						auto command = (unsigned short)commandId;
						unsigned short dataLength = 4;

						// Controllo se lunghezza dati che riceverò eccede il buffer
						if ((result = PreventBufferOverrun(dataLen)) == RESULT::SUCCESS)
						{
							// Scrittura della lungezza del dato = 2 byte len dati da leggere.
							*(unsigned short*)&_transmissionBuffer[0] = dataLength;

							// Scrittura dell'id comando nei 2 byte seguenti.
							*(unsigned short*)&_transmissionBuffer[2] = (unsigned short)commandId;

							// Scrittura della lunghezza dei dati da leggere
							*(unsigned short*)&_transmissionBuffer[4] = dataLen;

							// Invio del messaggio.
							if (send(idSocket, _transmissionBuffer, kMessageTxHeadLength + dataLength, 0) == SOCKET_ERROR)
								result = RESULT::FAIL;
							else if ((result = WaitForMessageHead(idSocket)) == RESULT::SUCCESS)
							{
								if ((result = CheckResponseResult(_transmissionBuffer, command)) == RESULT::SUCCESS)
								{
									auto rxDataLen = *(unsigned short*)&_transmissionBuffer[0];

									if ((result = WaitForData(idSocket, rxDataLen)) == RESULT::SUCCESS)
									{
										memcpy(pData, &_transmissionBuffer, dataLen);
									}
								}
							}
						}

						return result;
					}

#pragma endregion

				}
			}
		}
	}
}