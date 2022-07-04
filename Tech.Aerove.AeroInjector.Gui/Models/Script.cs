namespace Tech.Aerove.AeroInjector.Gui.Models
{
    public class Script
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Content { get; set; } 
        public bool IsLastModified { get; set; }
    }
}
