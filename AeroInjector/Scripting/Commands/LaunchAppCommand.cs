using LibPublic.NamedPipes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tech.Aerove.AeroInjector.Scripting.Attribute;

namespace Tech.Aerove.AeroInjector.Scripting.Commands
{
    /// <summary>
    /// launch app 
    /// [LaunchApp] {Executable Path} {args...}
    /// </summary>
    [ScriptName("LaunchApp")]
    [ScriptArg("Path", "Specifies the path to the executable you want to Launch")]
    [ScriptArg("Args", "Specifies the arguments you want to pass to the executable on launch.")]
    public class LaunchAppCommand : IScriptCommand
    {
        public List<KeyValuePair<string, string>> Arguments { get; set; }
        public Dictionary<string, string> Variables { get; set; }


        public void Execute()
        {

            var exePath = Arguments.First(x => x.Key.ToLower() == "path").Value;
            var args = "";
            if (Arguments.Any(x => x.Key.ToLower() == "args"))
            {
                args = Arguments.FirstOrDefault(x => x.Key.ToLower() == "args").Value;
            }
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    Arguments = args,
                    FileName = exePath,
                    WorkingDirectory = new FileInfo(exePath).Directory.FullName,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                },
                EnableRaisingEvents = true,

            };
            process.Start();
            var host = new NamedPipeHost($"Aero{process.Id}");
            Console.WriteLine($"[Gui] Aero{process.Id}");
            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                var tHost = host;
                tHost.WriteLine(e.Data);
                //if (e.Data.Contains("Injectee"))
                //{
                    Console.WriteLine($"[{process.ProcessName}] {e.Data}");
                //}
            };
            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
            {
                var tHost = host;
                tHost.WriteLine(e.Data);
                //if (e.Data.Contains("Injectee"))
                //{
                    Console.WriteLine($"[{process.ProcessName}] {e.Data}");
                //}
            };
            process.Exited += delegate
            {
                process.Dispose();
            };
            host.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            Console.WriteLine(process.Id);
            Variables["processid"] = $"{process.Id}";
            //var tt = new Thread(() =>
            //{
            //    while (true)
            //    {
            //        var tHost = host;
            //        var g = Guid.NewGuid();
            //        Console.WriteLine($"[Gui] {g}");
            //        tHost.WriteLine($"{g}");
            //        Thread.Sleep(5000);
            //    }
            //});
            //tt.Start();
        }

    }
}
