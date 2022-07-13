// Tech.Aerove.AeroInjector.InjecteeCPP.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include "coreclrhost.h"
//#include <dirent.h>
//#include <dlfcn.h>
#include <iostream>
#include <limits.h>
#include <set>
#include <stdlib.h>
#include <string.h>
#include <sys/stat.h>
#include <string>
#include "clrnetcore.cpp"
#include <fstream>
#include <metahost.h>


int main()
{
    std::cout << "Hello World!\n";

    HINSTANCE hGetProcIDDLL = LoadLibrary(L"C:\\Users\\Nicholas\\Desktop\\AeroMods\\AeroMods\\CoreConsoleApp\\bin\\Release\\net6.0\\publish\\win-x64\\coreclr.dll");
    if (!hGetProcIDDLL) {
        std::cout << "could not load the dynamic library" << std::endl;
        return EXIT_FAILURE;
    }
    std::cout << "Success Load CoreCLR!\n";
    coreclr_initialize_ptr coreclr_init = (coreclr_initialize_ptr)GetProcAddress(hGetProcIDDLL, "coreclr_initialize");
    if (!coreclr_init) {
        std::cout << "could not locate the function" << std::endl;
        return EXIT_FAILURE;
    }
    std::cout << "Found Function!\n";



    const char appPath[] = "C:\\Users\\Nicholas\\Desktop\\AeroMods\\AeroMods\\CoreConsoleApp\\bin\\Release\\net6.0\\publish\\win-x64";

    //std::string managedDLL;
    //std::string str(appPath);
    //str += ".txt";
    //std::ifstream file(str);
    //std::getline(file, managedDLL);


    const char* property_keys[] = { "APP_PATHS"/*, "TRUSTED_PLATFORM_ASSEMBLIES"*/ };
    const char* property_values[] = {// APP_PATHS
                                     appPath,
                                     // TRUSTED_PLATFORM_ASSEMBLIES
                                    // tpa_list.c_str() 
    };

    void* coreclr_handle;
    unsigned int domain_id;
    int ret =
        coreclr_init(appPath, // exePath
            "host",   // appDomainFriendlyName
            sizeof(property_values) / sizeof(char*), // propertyCount
            property_keys,                            // propertyKeys
            property_values,                          // propertyValues
            &coreclr_handle,                          // hostHandle
            &domain_id                                // domainId
        );

    if (ret < 0) {
        std::cout << "failed to initialize coreclr. cerr = " << ret;
        return -1;
    }

    std::cout << "core clr loaded!\n";

    std::string coreInjectee = "C:\\Users\\Nicholas\\Desktop\\AeroMods\\AeroMods\\CoreInjectee\\bin\\Debug\\net6.0\\CoreInjectee.dll";
    StartCSharpCore(coreInjectee);
}

