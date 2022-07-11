// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include <iostream>
#include <Windows.h>
#define sleep(x) Sleep(1000 * (x))
#include <metahost.h>
#include <string>
#include <fstream>
#pragma comment(lib, "mscoree.lib")

typedef HRESULT(STDAPICALLTYPE* FnGetNETCoreCLRRuntimeHost)(REFIID riid, IUnknown** pUnk);
std::string managedDLL;

ICLRRuntimeHost* GetNETCoreCLRRuntimeHost()
{

	auto* const coreCLRModule = ::GetModuleHandle(L"coreclr.dll");

	if (!coreCLRModule)
	{
		return nullptr;
	}

	const auto pfnGetCLRRuntimeHost = reinterpret_cast<FnGetNETCoreCLRRuntimeHost>(::GetProcAddress(coreCLRModule, "GetCLRRuntimeHost"));
	if (!pfnGetCLRRuntimeHost)
	{

		return nullptr;
	}



	ICLRRuntimeHost* clrRuntimeHost = nullptr;
	const auto hr = pfnGetCLRRuntimeHost(IID_ICLRRuntimeHost, reinterpret_cast<IUnknown**>(&clrRuntimeHost));

	if (FAILED(hr))
	{
		return nullptr;
	}
	return clrRuntimeHost;
}
void StartCSharpFramework()
{
	sleep(10);//uncomment for time to turn on debugger after manual injection
	std::cout << "Setting Up C# .NetFramework/Core From C++!\n";
	std::cout << std::endl;

	HRESULT hr;
	ICLRMetaHost* pMetaHost = NULL;
	ICLRRuntimeInfo* pRuntimeInfo = NULL;
	ICLRRuntimeHost* pClrRuntimeHost = NULL;

	// Bind to the CLR runtime..
	hr = CLRCreateInstance(CLSID_CLRMetaHost, IID_PPV_ARGS(&pMetaHost));
	hr = pMetaHost->GetRuntime(L"v4.0.30319", IID_PPV_ARGS(&pRuntimeInfo));
	hr = pRuntimeInfo->GetInterface(CLSID_CLRRuntimeHost,
		IID_PPV_ARGS(&pClrRuntimeHost));

	std::cout << "Starting C# .NetFramework From C++!\n";
	std::cout << std::endl;
	// Push the big START button shown above
	hr = pClrRuntimeHost->Start();

	std::wstring stemp = std::wstring(managedDLL.begin(), managedDLL.end());
	LPCWSTR managedInjectee = stemp.c_str();
	bool UseCore = true;
	// Okay, the CLR is up and running in this (previously native) process.
	// Now call a method on our managed C# class library.
	DWORD dwRet = 0;
	if (!UseCore) {
		hr = pClrRuntimeHost->ExecuteInDefaultAppDomain(
			managedInjectee, //<--
			L"FrameworkInjectee.InjecteeStart", L"MyMethod", L"pwzArgument", &dwRet);
	}
	else {
		pClrRuntimeHost = GetNETCoreCLRRuntimeHost();
		hr = pClrRuntimeHost->ExecuteInDefaultAppDomain(
			managedInjectee, //<--
			L"CoreInjectee.InjecteeStart", L"MyMethod", L"pwzArgument", &dwRet);
	}



	std::cout << "C# DLL closed with response " + dwRet;

	// Optionally stop the CLR runtime (we could also leave it running)
	hr = pClrRuntimeHost->Stop();

	// Don't forget to clean up.
	pClrRuntimeHost->Release();
}

DWORD WINAPI Main(LPVOID lpParam)
{
	//AllocConsole(); //opens console

	//StartCLR();
	StartCSharpFramework();
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

