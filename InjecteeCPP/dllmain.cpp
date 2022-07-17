// dllmain.cpp : Defines the entry point for the DLL application.

#include <iostream>
#include <Windows.h>
#include <metahost.h>
#include <string>
#include <fstream>
#include "clrnetframework.cpp"
#include "clrnetcore.cpp"
#define sleep(x) Sleep(1000 * (x))

std::string CLRDirectory;
std::string AssemblyFramework;
std::string ManagedDLL;
std::string ManagedNamespace;
std::string ManagedMethod;
std::string ManagedArgs;


DWORD WINAPI Main(LPVOID lpParam)
{
	//sleep(10);//uncomment for time to turn on debugger after manual injection
	//AllocConsole(); //opens console
	//FILE* fp;
	//freopen_s(&fp, "CONOUT$", "w", stdout);
	//std::cout.clear();
	//printf("[Aero C++] Console Opened!\r\n");


	bool UseCore = AssemblyFramework == "NetCore";
	if (UseCore) {
		StartCSharpCore(CLRDirectory, ManagedDLL, ManagedNamespace, ManagedMethod, ManagedArgs);
	}
	else {
		StartCSharpFramework(CLRDirectory, ManagedDLL, ManagedNamespace, ManagedMethod, ManagedArgs);
	}

		std::cout << "[Aero C++] Thread Exiting...\r\n";

	return 1;
}


/// <summary>
/// Injection Remote Execution start point
/// </summary>
BOOL APIENTRY DllMain(HMODULE hModule, DWORD dwReason, LPVOID lpReserved)
{
	if (dwReason == DLL_PROCESS_ATTACH) {
		//Read Params from File
		char Filename[MAX_PATH];
		GetModuleFileNameA(hModule, Filename, sizeof(Filename));
		std::string str(Filename);
		int dllNameLength = 16;//\\InjecteeCPP.dll
		CLRDirectory = str.substr(0, str.length() - dllNameLength);
		str += ".txt";
		std::ifstream file(str);
		std::getline(file, AssemblyFramework);
		std::getline(file, ManagedDLL);
		std::getline(file, ManagedNamespace);
		std::getline(file, ManagedMethod);
		std::getline(file, ManagedArgs);
		CreateThread(nullptr, 0, Main, hModule, 0, nullptr);
	}
	if (dwReason == DLL_PROCESS_DETACH) {}
	return true;
}

