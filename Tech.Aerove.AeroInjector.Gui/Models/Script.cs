using Tech.Aerove.AeroInjector.Scripting;
using Tech.Aerove.AeroInjector.Scripting.Commands;

namespace Tech.Aerove.AeroInjector.Gui.Models
{
    public class Script
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Content { get; set; }
        public bool IsLastModified { get; set; }
        public int ContentLineCount
        {
            get
            {
                if(Content == null)
                {
                    return 0;
                }
                return Content.Split("\n").Count();
            }
        }
        public string GetLine(int line)
        {
            if (Content == null)
            {
                return "";
            }
            return Content.Split("\n")[line];
        }
        public List<string> GetLines()
        {
            return Content.Split("\n").ToList();
        }
        public void SetLines(List<string> lines)
        {
            Content = string.Join("\n",lines);
        }
        public List<IScriptCommand> GetAsCommands()
        {
            return ScriptParser.Parse(Content);
        }
    }
}
