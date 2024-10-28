namespace Wireshelves.Models
{
    public class Language(string name, string fileName)
    {
        public string FileName { get; } = fileName;
        public string Name { get; } = name;
    }
}