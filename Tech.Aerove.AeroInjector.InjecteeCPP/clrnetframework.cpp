// dllmain.cpp : Defines the entry point for the DLL application.
#include <iostream>
#include <Windows.h>
#include <metahost.h>
#include <string>
#include <fstream>
#pragma comment(lib, "mscoree.lib")

namespace {


	void StartCSharpFramework(std::string managedDLL)
	{

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

		// Okay, the CLR is up and running in this (previously native) process.
		// Now call a method on our managed C# class library.
		DWORD dwRet = 0;

		hr = pClrRuntimeHost->ExecuteInDefaultAppDomain(
			managedInjectee, //<--
			L"FrameworkInjectee.InjecteeStart", L"MyMethod", L"pwzArgument", &dwRet);


		std::cout << "C# DLL closed with response " + dwRet;

		// Optionally stop the CLR runtime (we could also leave it running)
		hr = pClrRuntimeHost->Stop();

		// Don't forget to clean up.
		pClrRuntimeHost->Release();
	}
}