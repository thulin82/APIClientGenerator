using NJsonSchema.CodeGeneration.TypeScript;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.TypeScript;

namespace APIClientGenerator
{
    public static class Generator
    {
        /// <summary>
        /// Generates a TypeScript client from a URL
        /// </summary>
        /// <param name="url">The URL of the OpenAPI document</param>
        /// <param name="generatePath">The path to generate the client to</param>
        /// <returns></returns>
        async public static Task GenerateTypeScriptClientUrl(string url, string generatePath) =>
            await GenerateClient(
                document: await OpenApiDocument.FromUrlAsync(url),
                generatePath: generatePath,
                generateCode: (OpenApiDocument document) =>
                {
                    var generator = new TypeScriptClientGenerator(document, TypeScriptGeneratorSettings());
                    var code = generator.GenerateFile();

                    return code;
                }
            );

        /// <summary>
        /// Generates a TypeScript client from a file
        /// </summary>
        /// <param name="path">The path of the OpenAPI document</param>
        /// <param name="generatePath">The path to generate the client to</param>
        /// <returns></returns>
        async public static Task GenerateTypeScriptClientFile(string path, string generatePath) =>
            await GenerateClient(
                document: await OpenApiDocument.FromFileAsync(path),
                generatePath: generatePath,
                generateCode: (OpenApiDocument document) =>
                {
                    var generator = new TypeScriptClientGenerator(document, TypeScriptGeneratorSettings());
                    var code = generator.GenerateFile();

                    return code;
                }
            );

        /// <summary>
        /// Generates a C# client from a URL
        /// </summary>
        /// <param name="url">The URL of the OpenAPI document</param>
        /// <param name="generatePath">The path to generate the client to</param>
        /// <returns></returns>
        async public static Task GenerateCSharpClientUrl(string url, string generatePath) =>
            await GenerateClient(
                document: await OpenApiDocument.FromUrlAsync(url),
                generatePath: generatePath,
                generateCode: (OpenApiDocument document) =>
                {
                    var generator = new CSharpClientGenerator(document, CSharpGeneratorSettings());
                    var code = generator.GenerateFile();
                    return code;
                }
            );

        /// <summary>
        /// Generates a C# client from a file
        /// </summary>
        /// <param name="path">The path of the OpenAPI document</param>
        /// <param name="generatePath">The path to generate the client to</param>
        /// <returns></returns>
        async public static Task GenerateCSharpClientFile(string path, string generatePath) =>
            await GenerateClient(
                document: await OpenApiDocument.FromFileAsync(path),
                generatePath: generatePath,
                generateCode: (OpenApiDocument document) =>
                {
                    var generator = new CSharpClientGenerator(document, CSharpGeneratorSettings());
                    var code = generator.GenerateFile();
                    return code;
                }
            );

        /// <summary>
        /// Generates a client from an OpenAPI document and writes it to a file
        /// </summary>
        /// <param name="document">The OpenAPI document</param>
        /// <param name="generatePath">The path to generate the client to</param>
        /// <param name="generateCode">The function to generate the client code</param>
        /// <returns></returns>
        private async static Task GenerateClient(OpenApiDocument document, string generatePath, Func<OpenApiDocument, string> generateCode)
        {
            Console.WriteLine($"Generating {generatePath}...");

            var code = generateCode(document);

            await System.IO.File.WriteAllTextAsync(generatePath, code);
        }

        /// <summary>
        /// Returns the C# client generator settings
        /// </summary>
        /// <returns></returns>
        private static CSharpClientGeneratorSettings CSharpGeneratorSettings()
        {
            return new CSharpClientGeneratorSettings
            {
                UseBaseUrl = false,
                GenerateSyncMethods = true,
                WrapResponses = false
            };
        }

        /// <summary>
        /// Returns the TypeScript client generator settings
        /// </summary>
        /// <returns></returns>
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
