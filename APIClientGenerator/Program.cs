namespace APIClientGenerator
{
    class Program
    {
        static async Task Main(string[] args)
        {

            if (args.Length != 2)
                throw new ArgumentException("Expecting 2 arguments: filepath/URL, language");

            var url = args[0];
            var language = args[1];
            var generatePath = Directory.GetCurrentDirectory();

            if (language != "TypeScript" && language != "CSharp")
                throw new ArgumentException("Invalid language parameter; valid values are TypeScript and CSharp");

            if (language == "TypeScript"){
                generatePath = Path.Combine(generatePath, "client.ts");
                await Generator.GenerateTypeScriptClientUrl(url, generatePath);
            }
            else
                generatePath = Path.Combine(generatePath, "Client.cs");
                await Generator.GenerateCSharpClientUrl(url, generatePath);
        }
    }
}