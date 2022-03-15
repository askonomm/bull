namespace Bull;

class Program
{
    private static void Build(string dir)
    {
        // Get content
        var content = new Content(dir);
        var contentItems = content.GetAll();
        
        foreach(var contentItem in contentItems)
        {
            Console.WriteLine(ObjectDumper.Dump(contentItem));
        }
        
        // Write content
    }
    
    private static void Main(string[] args)
    {
        Build("/Users/asko/projects/bull-test");
        // Potentially watch for changes
    }
}