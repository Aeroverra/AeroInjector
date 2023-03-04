## **AeroInjector**
Aeroinjector is an experimental research project I created to expand my understanding of the .Net CLR, native dll injection and reverse engineering techniques. I was also experimenting with different UI frameworks. This is not in active development but I wanted to make it public in order to share the many hours of research it took to build. 

##### Features

 - Inject a .Net Core or .Net Framework DLL into another C# managed application.
 - **Also Inject a .Net Core or .Net Framework DLL into a C / C++ or any other natively compiled application**
 - Automatic .Net boostrapping for native application injection
 - Dump information from managed dlls including methods parameters and more
 - Invoke managed DLL functions and add on to closed source software with ease
 - Command line or Gui
 - Very basic custom script environment with a visual editor

##### Languages / Tools / Frameworks used
- C# / C / C++
- Blazor
- Photino


### Getting Started With Gui
Once the solution is compiled and the gui is ran the first thing you will see is this.

![First Screen](https://cdn.discordapp.com/attachments/521970463052922891/1081369012379848826/image.png)

We are first going to demonstrate .Net injection so to do that Its easiest to create a new .net core (Version 6) console app which just loops the same text forever. Here is what it should look like.

![basic console app](https://cdn.discordapp.com/attachments/521970463052922891/1081370899925708952/image.png)

Next go back to the gui and in the second tab add our compiled console app exe as an application. 

![Application](https://cdn.discordapp.com/attachments/521970463052922891/1081371286619574393/image.png)

We can now go to the third tab and inject the InjecteeCore Example dll provided.

![DLL To Inject](https://media.discordapp.net/attachments/521970463052922891/1081372274508185670/image.png?width=1674&height=882)

The next step is telling the application what to do with these. The 4th tab provides a very basic custom script setup. Click on the script drop down and select "New Script"

![Injector](https://cdn.discordapp.com/attachments/521970463052922891/1081372936398721144/image.png)

Name the script and add a command. The left side allows adding and editing known commands or parameters while the right side allows easy text editing of these commands. 

![Example](https://cdn.discordapp.com/attachments/521970463052922891/1081375092656844901/image.png)


It is important that our Inject command have the right namespace and method name. "MyMethod" refers to the actual name of the method within the provided injectee example. 
NameSpace: CoreInjectee.InjecteeStart
Method: MyMethod

![Inject command](https://cdn.discordapp.com/attachments/521970463052922891/1081379833377988688/image.png)
Once all 3 parameters are set up to Launch, Sleep and Inject, your right side script should look like this.

    [LaunchApp] {Path:TestConsoleAppPathHere}
    [Sleep] {Length:1000}
    [Inject] {path:YourPathToSource\AeroInjector\Examples\CoreInjectee\bin\Debug\net6.0\CoreInjectee.dll} {namespace:CoreInjectee.InjecteeStart} {method:MyMethod}



Make sure you can see the console of the gui as this is where the output for our test app will end up going. Now click the home tab (First Tab) and you will see a new button which will launch our test program and inject our example C# dll into it.


The result should look like this

![injection result](https://cdn.discordapp.com/attachments/521970463052922891/1081381881888641034/image.png)

To demonstrate the flexibility, lets inject a .Net Core DLL into a Native C/C++ DLL.
Here is a script to demonstrate launching the remote desktop client and injecting the same C# dll. Modify it as needed


    [LaunchApp] {Path:C:\WINDOWS\system32\mstsc.exe}
    [Sleep] {Length:5000}
    [Inject] {path:YourPathToSource\AeroInjector\Examples\CoreInjectee\bin\Debug\net6.0\CoreInjectee.dll} {namespace:CoreInjectee.InjecteeStart} {method:MyMethod}

The result will look like this

![Native example](https://cdn.discordapp.com/attachments/521970463052922891/1081395401908826203/image.png)

Don't believe that its actually injecting C# into the native DLL? Verify it by opening [Process Explorer](https://learn.microsoft.com/en-us/sysinternals/downloads/process-explorer) as administrator . Select the rdp client with the bottom pane activated (CTRL + L) and there you will see the .Net Core DLL we injected. Before injection it is moved to a temporary folder for development purposes so the path will not line up.

![enter image description here](https://cdn.discordapp.com/attachments/521970463052922891/1081396191780155503/image.png)

#### How Does it work?
The Gui tells our main application ["AeroInjector"](https://github.com/Aeroverra/AeroInjector) to inject a our dll. AeroInjector (C#)  detects the framework of the dll you want to inject. It then uses the Marshal Interop Service to allocate memory space within our running target application and uses remote thread execution to inject and run the provided "InjecteeCPP.dll" passing along the framework type of your dll.  Now that our native dll is within the running applications namespace it will check whether the .Net CLR is present. If not it will handle bootstrapping the .Net CLR into the running application and finally Inject and load your provided DLL. 

#### Whats the point of injecting an managed assembly into an unmanaged assembly?
The most simple answer is because it allows you to run code within the namespace of a native application which is required for some things like targeted video capture. 

#### But why not write it in C / C++ to begin with?
The biggest reason is the same reason people use C# in a lot of major enterprise applications. It makes development much quicker, cleaner, and easier. Now you have a bit of a roadblock here because you need to model native functions and while that can be a bit cumbersome, strides have been made in this department and it has become much more reasonable. 

#### How do I dump methods from a C# managed application?
Simply using reflection ofcourse! Within the provided CoreInjectee project you can uncomment the ReflectionOutput Method. Don't forget to compile.

![reflection output comment](https://cdn.discordapp.com/attachments/521970463052922891/1081409381104627762/image.png)

The result will look like this showing all the methods, parameters and more. Very useful for modifying closed source software lacking that one tiny feature you want. 

![reflection result](https://cdn.discordapp.com/attachments/521970463052922891/1081409692804337725/image.png)

#### Known Limitations
 - Mixing .Net Core and .Net Framework may not work in all cases
 - Windows store apps don't usually work due to how they are launched. Small modifications could be made to support it though.

These utilities were all made by Aeroverra and the repo can be found at 
[https://github.com/Aeroverra/AeroInjector](https://github.com/Aeroverra/AeroInjector)