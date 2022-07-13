// dllmain.cpp : Defines the entry point for the DLL application.

#include <iostream>
#include <Windows.h>
#include <metahost.h>
#include <string>
#include <fstream>
#include "clrnetframework.cpp"
#include "clrnetcore.cpp"
#define sleep(x) Sleep(1000 * (x))

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
		//StartCSharpCore(managedDLL);
	}
	while (true) {
		std::cout << "Hello World From C++!\n";
		std::cout << std::endl;
		sleep(2);
	}
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
		str += ".txt";
		std::ifstream file(str);
		std::getline(file, managedDLL);
		CreateThread(nullptr, 0, Main, hModule, 0, nullptr);
	}
	if (dwReason == DLL_PROCESS_DETACH) {}
	return true;
}

