// dllmain.cpp : Define el punto de entrada de la aplicación DLL.
#include "stdafx.h"
#include <Windows.h>

//https://docs.microsoft.com/en-us/windows/desktop/dlls/dynamic-link-libraries

extern "C" __declspec(dllexport) bool APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {

	case DLL_PROCESS_ATTACH:
		MessageBox(NULL, L"Me han llamado desde C#", L"Inject DLL", MB_OK);
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

