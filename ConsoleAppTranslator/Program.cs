using Azure;
using Azure.AI.Translation.Text;
using ConsoleAppTranslator.Inputs;

Console.WriteLine("***** Utilizando o Azure Text Translation com SDK *****");
Console.WriteLine();

var regionTextTranslation = InputHelper.GetRegionTextTranslation();
Console.WriteLine();

var subscriptionKey = InputHelper.GetSubscriptionKey();
Console.WriteLine();

var client = new TextTranslationClient(
    new AzureKeyCredential(subscriptionKey), regionTextTranslation);

string continuar;
do
{
    var textToTranslate = InputHelper.GetTextToTranslate();
    Console.WriteLine();

    var targetLanguage = InputHelper.GetTargetLanguage();
    Console.WriteLine();

    var requestBody = $$"""[ { "Text": "{{textToTranslate}}" } ]""";
    Console.WriteLine($"Request Body: {requestBody}");

    var response = await client.TranslateAsync(
        targetLanguage, textToTranslate).ConfigureAwait(false);
    var translations = response.Value;
    var translation = translations.FirstOrDefault();

    Console.WriteLine();
    Console.WriteLine($"Linguagem detectada: {translation?.DetectedLanguage?.Language} | " +
        $"Score: {translation?.DetectedLanguage?.Score}");
    Console.WriteLine($"Conteudo traduzido: {translation?.Translations?.FirstOrDefault()?.Text}");
    Console.WriteLine();

    continuar = InputHelper.GetAnswerContinue();
    Console.WriteLine();
} while (continuar == "Sim");

Console.WriteLine("Testes concluidos!");