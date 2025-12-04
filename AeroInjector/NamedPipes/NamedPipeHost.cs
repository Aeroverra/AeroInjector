using System.IO.Pipes;

namespace Tech.Aerove.AeroInjector.NamedPipes
{
    /// <summary>
    /// these need to be cleaned up and finished. Named pipes can only handle 1 connection so
    /// to handle multitenant it would be a good idea to use the default named pipe in host
    /// and on first connect the host send a reconnect to a new spawned instance and then restarts
    /// the original.
    /// </summary>
    public class NamedPipeHost
    {
        private readonly List<Task> Tasks = new List<Task>();
        private readonly List<StreamWriter> Writers = new List<StreamWriter>();
        private readonly string Name;
        public NamedPipeHost(string name)
        {
            Name = name;
        }

        public void Start()
        {
            _ = Listener();
        }
        private async Task Listener()
        {

            while (true)
            {
                var server = new NamedPipeServerStream(Name);
                await server.WaitForConnectionAsync();
                var task = PipeHandler(server);
                Tasks.Add(task);
            }


        }
        private async Task PipeHandler(NamedPipeServerStream pipe)
        {
            var reader = new StreamReader(pipe);
            var writer = new StreamWriter(pipe);
            Writers.Add(writer);
        }
        public void WriteLine(string line)
        {
            foreach (var writer in Writers)
            {
                writer.WriteLine(line);
                writer.Flush();
            }
        }
        public void Stop()
        {

        }
    }
}
