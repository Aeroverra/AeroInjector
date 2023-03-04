using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoreInjectee
{
    /// <summary>
    /// Core Injectee Doesn't seem to work if the environment is not .net core. May work in framework but have not tested
    /// </summary>
    public class InjecteeStart
    {

        // namespace: CoreInjectee.InjecteeStart
        // method: would be MyMethod
        // args: anystring you want. you can even rename the var name here
        // reflection methods listed here are an example of the reflection you could do if you inject
        // into an app running C#. Commented out because it crashes in native applications. likely because
        // it tries to read entry assembly. Reflection will still work if its a native app but some things obv 
        // don't.
        public static int MyMethod(String pwzArgument)
        {
            Console.WriteLine("Hello World From C# .NET Core!");

            //ReflectionOutput();
            while (true)
            {
                Thread.Sleep(2000);
                Console.WriteLine("Hello World From C# .NET Core!");

            }

            return 0;
        }
        public static void ReflectionOutput()
        {
           // File.WriteAllLines(@"youroutputpath\Desktop\start.txt", new string[] { "tes" });
            //this will crash app if its not a managed app (core / framework)
            var entrya = Assembly.GetEntryAssembly();

            Console.WriteLine($"<-- Assembly {entrya.FullName} -->\r\n");
            System.Reflection.Assembly asm = System.Reflection.Assembly.Load(entrya.ToString());
           // File.WriteAllLines(@"youroutputpath\Desktop\start2.txt", asm.GetTypes().Select(x => x.Name).ToArray());
            foreach (Type type in asm.GetTypes())
            {
                Console.WriteLine($"##### {type.Name} #####");
                //PROPERTIES
                foreach (System.Reflection.PropertyInfo property in type.GetProperties())
                {
                    if (property.CanRead)
                    {
                        Console.WriteLine("\t[P]" + type.Name + "." + property.Name);
                    }
                }
                //METHODS
                var methods = type.GetMethods();
                foreach (System.Reflection.MethodInfo method in methods)
                {
                    Console.Write($"\t[M] {method.Name} ( ");
                    foreach (System.Reflection.ParameterInfo param in method.GetParameters())
                    {
                        Console.Write($"{{{param.Name}:{param.ParameterType.Name}}} ");
                        // Console.WriteLine("--->  Position=" + param.Position.ToString());
                        //Console.WriteLine("--->  Optional=" + param.IsOptional.ToString() + "</i>");
                    }
                    Console.WriteLine($")");
                    if (method.Name == "NotCalledNormally")
                    {
                        method.Invoke(null, null);//Dont need to pass the type since its static
                    }
                }
            }


        }
    }
}
