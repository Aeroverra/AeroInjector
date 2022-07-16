// dllmain.cpp : Defines the entry point for the DLL application.
//thrown together with A LOT of research. many links not listed
//https://github.com/dotnet/runtime/blob/main/docs/design/features/native-hosting.md
//https://github.com/renkun-ken/cpp-coreclr/blob/master/src/main.cpp
//https://github.com/dotnet/coreclr/blob/master/src/coreclr/hosts/corerun/corerun.cpp
//https://yizhang82.dev/hosting-coreclr
//https://github.com/dotnet/runtime/blob/main/src/native/corehost/hostfxr.h

#include <iostream>
#include <Windows.h>

#include <metahost.h>
#include <string>
#include <fstream>
#pragma comment(lib, "mscoree.lib")
#define sleep(x) Sleep(1000 * (x))
#include <iostream>
#include "coreclrhost.h"
#include <iostream>
#include <limits.h>
#include <set>
#include <stdlib.h>
#include <string.h>
#include <sys/stat.h>
#include <string>
#include <fstream>
#include <metahost.h>



typedef HRESULT(STDAPICALLTYPE* FnGetNETCoreCLRRuntimeHost)(REFIID riid, IUnknown** pUnk);


namespace
{

	/// <summary>
	/// Creates an instance of .Net Core
	/// </summary>
	/// <param name="clrDirectoryPath">The path coreclr is located in</param>
	/// <returns></returns>
	HMODULE InjectCoreCLR(char clrDirectoryPath[])
	{
		std::string coreCLRDLL = std::string(clrDirectoryPath) + "\\coreclr.dll";
		std::wstring stemp = std::wstring(coreCLRDLL.begin(), coreCLRDLL.end());
		LPCWSTR lpcCoreCLRDLL = stemp.c_str();
		HINSTANCE hGetProcIDDLL = LoadLibrary(lpcCoreCLRDLL);
		if (!hGetProcIDDLL) {
			std::cout << "[Aero C++] Bootstrap Failed. Could not load the dynamic library." << std::endl;
			return NULL;
		}
		std::cout << "[Aero C++] Coreclr loaded as dynamic library.\n";
		coreclr_initialize_ptr coreclr_init = (coreclr_initialize_ptr)GetProcAddress(hGetProcIDDLL, "coreclr_initialize");
		if (!coreclr_init) {
			std::cout << "[Aero C++] Could not bind coreclr Init function." << std::endl;
			return NULL;
		}
		std::cout << "[Aero C++] Coreclr Init Function bound.\n";





		const char* property_keys[] = { "APP_PATHS"/*, "TRUSTED_PLATFORM_ASSEMBLIES"*/ };
		const char* property_values[] = {// APP_PATHS
										 clrDirectoryPath,
										 // TRUSTED_PLATFORM_ASSEMBLIES
										// tpa_list.c_str() 
		};

		void* coreclr_handle;
		unsigned int domain_id;
		int ret =
			coreclr_init(clrDirectoryPath, // exePath
				"host",   // appDomainFriendlyName
				sizeof(property_values) / sizeof(char*), // propertyCount
				property_keys,                            // propertyKeys
				property_values,                          // propertyValues
				&coreclr_handle,                          // hostHandle
				&domain_id                                // domainId
			);

		if (ret < 0) {
			std::cout << "[Aero C++] Failed to initialize coreclr. cerr = " << ret;
			return NULL;
		}

		std::cout << "[Aero C++] Bootstrap Success, coreclr loaded!\n";
		return  ::GetModuleHandle(L"coreclr.dll");
	}
	ICLRRuntimeHost* GetNETCoreCLRRuntimeHost(char clrDirectoryPath[])
	{
		HMODULE coreCLRModule = ::GetModuleHandle(L"coreclr.dll");

		if (!coreCLRModule)
		{
			std::cout << "[Aero C++] Could not find coreclr. Attempting to bootstrap...\n";
			//not currently loaded in this process so we need to start an instance
			coreCLRModule = InjectCoreCLR(clrDirectoryPath);
			if (!coreCLRModule)
			{
				//failed to start instance of .net core
				return nullptr;
			}
		}
		const auto pfnGetCLRRuntimeHost = reinterpret_cast<FnGetNETCoreCLRRuntimeHost>(::GetProcAddress(coreCLRModule, "GetCLRRuntimeHost"));

		if (!pfnGetCLRRuntimeHost)
		{

			return nullptr;
		}



		ICLRRuntimeHost* clrRuntimeHost = nullptr;
		const auto hr = pfnGetCLRRuntimeHost(IID_ICLRRuntimeHost, reinterpret_cast<IUnknown**>(&clrRuntimeHost));
		clrRuntimeHost->Start();
		if (FAILED(hr))
		{
			return nullptr;
		}
		return clrRuntimeHost;
	}

	void StartCSharpCore(std::string clrDirectoryPath, std::string managedDll, std::string managedNamespace, std::string managedMethod, std::string managedArgs)
	{
		//sleep(10);//uncomment for time to turn on debugger after manual injection
		std::cout << "[Aero C++] Setting C# .NET Core Injection From C++!\n";



		std::wstring lManagedDll = std::wstring(managedDll.begin(), managedDll.end());
		std::wstring lmanagedNamespace = std::wstring(managedNamespace.begin(), managedNamespace.end());
		std::wstring lmanagedMethod = std::wstring(managedMethod.begin(), managedMethod.end());
		std::wstring lmanagedArgs = std::wstring(managedMethod.begin(), managedMethod.end());

		//Access Violation Exception? Sucks to suck..
		//jk fix is usually making sure both this and the injectee is 64 bit and likely the app.

		DWORD dwRet = 0;
		ICLRRuntimeHost* pClrRuntimeHost = GetNETCoreCLRRuntimeHost(const_cast<char*>(clrDirectoryPath.c_str()));
		printf("[Aero C++] Calling C# Now! => %s.%s(%s)\n", managedNamespace.c_str(), managedMethod.c_str(), managedArgs.c_str());
		HRESULT hr = pClrRuntimeHost->ExecuteInDefaultAppDomain(
			lManagedDll.c_str(), //<--
			lmanagedNamespace.c_str(), lmanagedMethod.c_str(), lmanagedArgs.c_str(), &dwRet);


		std::cout << "[Aero C++] C# DLL  " + managedDll + "  closed with response ";
		std::cout << dwRet;
		std::cout << std::endl;
		// Optionally stop the CLR runtime 
		hr = pClrRuntimeHost->Stop();

		// Don't forget to clean up.
		pClrRuntimeHost->Release();
	}
}