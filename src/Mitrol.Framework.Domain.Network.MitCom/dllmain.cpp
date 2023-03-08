// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include "Ws2tcpip.h"

int Init()
{
	WSADATA wsaData;
	WORD wVersionRequested;
	// Ask for Winsock 1.1 functionality
	wVersionRequested = MAKEWORD(2, 2);
	return WSAStartup(wVersionRequested, &wsaData);
}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    Init();

    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

