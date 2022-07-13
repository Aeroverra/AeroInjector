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

	char appPath[] = "C:\\Users\\Nicholas\\Desktop\\AeroMods\\AeroMods\\CoreConsoleApp\\bin\\Release\\net6.0\\publish\\win-x64";
	std::string coreInjectee = "C:\\Users\\Nicholas\\Desktop\\AeroMods\\AeroMods\\CoreInjectee\\bin\\Debug\\net6.0\\CoreInjectee.dll";
	StartCSharpCore(coreInjectee, appPath);
}

