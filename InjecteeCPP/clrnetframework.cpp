// dllmain.cpp : Defines the entry point for the DLL application.
#include <iostream>
#include <Windows.h>
#include <metahost.h>
#include <string>
#include <fstream>
#pragma comment(lib, "mscoree.lib")

namespace {


	void StartCSharpFramework(std::string clrDirectoryPath, std::string managedDll, std::string managedNamespace, std::string managedMethod, std::string managedArgs)
	{

		std::cout << "[Aero C++] Setting Up C# .Net Framework From C++!\n";
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

		std::cout << "[Aero C++] Starting C# .Net Framework From C++!\n";
		std::cout << std::endl;
		// Push the big START button shown above
		hr = pClrRuntimeHost->Start();

		std::wstring lManagedDll = std::wstring(managedDll.begin(), managedDll.end());
		std::wstring lmanagedNamespace = std::wstring(managedNamespace.begin(), managedNamespace.end());
		std::wstring lmanagedMethod = std::wstring(managedMethod.begin(), managedMethod.end());
		std::wstring lmanagedArgs = std::wstring(managedMethod.begin(), managedMethod.end());

		// Okay, the CLR is up and running in this (previously native) process.
		// Now call a method on our managed C# class library.
		DWORD dwRet = 0;

		hr = pClrRuntimeHost->ExecuteInDefaultAppDomain(
			lManagedDll.c_str(), //<--
			lmanagedNamespace.c_str(), lmanagedMethod.c_str(), lmanagedArgs.c_str(), &dwRet);


		std::cout << "[Aero C++] C# DLL closed with response " + dwRet;

		// Optionally stop the CLR runtime (we could also leave it running)
		hr = pClrRuntimeHost->Stop();

		// Don't forget to clean up.
		pClrRuntimeHost->Release();
	}
}