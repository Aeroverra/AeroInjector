// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include <iostream>
#include <Windows.h>
#define sleep(x) Sleep(1000 * (x))
#include <metahost.h>
#include <string>
#include <fstream>
#include "NetFramework.cpp"
#include "NetCore.cpp"
#pragma comment(lib, "mscoree.lib")

typedef HRESULT(STDAPICALLTYPE* FnGetNETCoreCLRRuntimeHost)(REFIID riid, IUnknown** pUnk);
std::string managedDLL;




DWORD WINAPI Main(LPVOID lpParam)
{
	//sleep(10);//uncomment for time to turn on debugger after manual injection
	//AllocConsole(); //opens console

	bool UseCore = true;
	if (!UseCore) {
		StartCSharpFramework(managedDLL);
	}
	else {
		StartCSharpCore(managedDLL);
	}
	while (true) {
		std::cout << "Hello World From C++!\n";
		std::cout << std::endl;
		sleep(2);
	}
	return 1;
}

BOOL APIENTRY DllMain(HMODULE hModule, DWORD dwReason, LPVOID lpReserved)
{
	

	if (dwReason == DLL_PROCESS_ATTACH) {
		char Filename[MAX_PATH]; //this is a char buffer
		GetModuleFileNameA(hModule, Filename, sizeof(Filename));
		std::string str(Filename);
		str += ".txt";
		std::ifstream file(str);
		std::getline(file, managedDLL);
		CreateThread(nullptr, 0, Main, hModule, 0, nullptr);
	}
	if (dwReason == DLL_PROCESS_DETACH) {}
	return true;
}

