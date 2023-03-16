namespace APIClientGenerator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Check if the correct number of arguments are passed
            if (args.Length != 2)
                throw new ArgumentException("Expecting 2 arguments: filepath/URL, language");

            var url = args[0];
            var language = args[1];
            var generatePath = Directory.GetCurrentDirectory();

            // Check if the language parameter is valid
            if (language != "TypeScript" && language != "CSharp")
            {
                throw new ArgumentException("Invalid language parameter; valid values are TypeScript and CSharp");
            }

            // Check if the first argument is a URL or a file path
            if (url.StartsWith("http") || url.StartsWith("www"))
            {
                if (language == "TypeScript")
                {
                    generatePath = Path.Combine(generatePath, "client.ts");
                    await Generator.GenerateTypeScriptClientUrl(url, generatePath);
                }
                else
                {
                    generatePath = Path.Combine(generatePath, "Client.cs");
                    await Generator.GenerateCSharpClientUrl(url, generatePath);
                }
            }
            else
            {
                if (language == "TypeScript")
                {
                    generatePath = Path.Combine(generatePath, "client.ts");
                    await Generator.GenerateTypeScriptClientFile(url, generatePath);
                }
                else
                {
                    generatePath = Path.Combine(generatePath, "Client.cs");
                    await Generator.GenerateCSharpClientFile(url, generatePath);
                }
            }
        }
    }
}
