using NJsonSchema.CodeGeneration.TypeScript;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.TypeScript;

namespace APIClientGenerator
{
    public static class Generator
    {


        private async static Task GenerateClient(OpenApiDocument document, string generatePath, Func<OpenApiDocument, string> generateCode)
        {
            Console.WriteLine($"Generating {generatePath}...");

            var code = generateCode(document);

            await System.IO.File.WriteAllTextAsync(generatePath, code);
        }

        private static CSharpClientGeneratorSettings CSharpGeneratorSettings()
        {
            return new CSharpClientGeneratorSettings
            {
                UseBaseUrl = false
            };
        }

        private static TypeScriptClientGeneratorSettings TypeScriptGeneratorSettings()
        {
            var settings = new TypeScriptClientGeneratorSettings();
            settings.TypeScriptGeneratorSettings.TypeStyle = TypeScriptTypeStyle.Interface;
            settings.TypeScriptGeneratorSettings.TypeScriptVersion = 3.5M;
            settings.TypeScriptGeneratorSettings.DateTimeType = TypeScriptDateTimeType.String;
            return settings;
        }
    }
}
