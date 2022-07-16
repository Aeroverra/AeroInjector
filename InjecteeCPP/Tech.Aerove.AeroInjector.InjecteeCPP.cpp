//// Tech.Aerove.AeroInjector.InjecteeCPP.cpp : This file contains the 'main' function. Program execution begins and ends there.
////
//
//#include <iostream>
//#include <Windows.h>
//#include <metahost.h>
//#include <string>
//#include <fstream>
//#include "clrnetframework.cpp"
//#include "clrnetcore.cpp"
//
//std::string CLRDirectory;
//std::string AssemblyFramework;
//std::string ManagedDLL;
//std::string ManagedNamespace;
//std::string ManagedMethod;
//std::string ManagedArgs;
//int main()
//{
//	std::cout << "Hello World!\n";
//
//	//Read Params from File
//	char Filename[] = "C:\\Users\\Nicholas\\Desktop\\AeroInjector\\x64\\Debug\\InjecteeCPP.exe";
//
//	std::string str(Filename);
//	int dllNameLength = 16;//\\InjecteeCPP.dll
//	CLRDirectory = str.substr(0, str.length() - dllNameLength);
//	str += ".txt";
//	std::ifstream file(str);
//	std::getline(file, AssemblyFramework);
//	std::getline(file, ManagedDLL);
//	std::getline(file, ManagedNamespace);
//	std::getline(file, ManagedMethod);
//	std::getline(file, ManagedArgs);
//	bool UseCore = AssemblyFramework == "NetCore";
//	if (UseCore) {
//		StartCSharpCore(CLRDirectory, ManagedDLL, ManagedNamespace, ManagedMethod, ManagedArgs);
//	}
//	else {
//		StartCSharpFramework(CLRDirectory, ManagedDLL, ManagedNamespace, ManagedMethod, ManagedArgs);
//	}
//}
//
