using Translator;

string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
string englishGeorgianFilePath = Path.Combine(desktopPath, "githubProjects", "Translator", "Translator", "bin", "Debug", "net6.0", "TranslatorE-G.txt");
string englishSpanishFilePath = Path.Combine(desktopPath, "githubProjects", "Translator", "Translator", "bin", "Debug", "net6.0", "TranslatorE-S.txt");


TranslationManager translationManager = new TranslationManager();

string languagePairChoice = "";
while (languagePairChoice.ToLower() != "exit")
{
    Console.WriteLine("Choose the language pair:");
    Console.WriteLine("1. English-Georgian");
    Console.WriteLine("2. English-Spanish");
    Console.WriteLine("Type 'exit' to quit.");
    languagePairChoice = Console.ReadLine();

    if (languagePairChoice == "1")
    {
        translationManager.LoadTranslationsFromFile(englishGeorgianFilePath);
    }
    else if (languagePairChoice == "2")
    {
        translationManager.LoadTranslationsFromFile(englishSpanishFilePath);
    }
    else if (languagePairChoice.ToLower() == "exit")
    {
        break;
    }
    else
    {
        Console.WriteLine("Invalid choice.");
        continue;
    }

    string originalWord = "";
    while (originalWord.ToLower() != "back")
    {
        Console.WriteLine("Enter the word or phrase to translate (type 'back' to go back to language selection, 'exit' to quit):");
        originalWord = Console.ReadLine();

        if (originalWord.ToLower() == "exit")
        {
            languagePairChoice = "exit";
            break;

        }

        if (originalWord.ToLower() != "back")
        {
            string translatedWord = translationManager.Translate(originalWord, languagePairChoice == "1" ? "Georgian" : "Spanish");
            if (translatedWord.StartsWith("Translation not found"))
            {
                Console.WriteLine(translatedWord);
                string response = Console.ReadLine();
                if (response.ToLower() == "yes")
                {
                    Console.WriteLine("Enter the translation:");
                    string newTranslation = Console.ReadLine();
                    translationManager.AddTranslation(originalWord, newTranslation, languagePairChoice == "1" ? "G" : "S", languagePairChoice == "1" ? englishGeorgianFilePath : englishSpanishFilePath);
                    Console.WriteLine("Translation added.");
                }
            }
            else
            {
                Console.WriteLine($"Translated word: {translatedWord}");
            }
        }
    }
}
