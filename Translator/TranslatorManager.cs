using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
    public class TranslationManager
    {
        private List<Translation> translations;

        public TranslationManager()
        {
            translations = new List<Translation>();
        }

        public void LoadTranslationsFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Translation file not found.");
            }

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split('-');
                if (parts.Length == 3)
                {
                    translations.Add(new Translation
                    {
                        OriginalWord = parts[0],
                        TranslatedWord = parts[1],
                        Language = parts[2]
                    });
                }
            }
        }

        public string Translate(string originalWord, string targetLanguage)
        {
            string targetLanguageCode = targetLanguage == "Georgian" ? "G" : targetLanguage == "Spanish" ? "S" : "E";
            Translation translation = translations.FirstOrDefault(t => t.OriginalWord == originalWord && t.Language == targetLanguageCode);
            if (translation != null)
            {
                return translation.TranslatedWord;
            }
            else
            {
                return "Translation not found. Would you like to add it to the dictionary? (yes/no)";
            }
        }


        public void AddTranslation(string originalWord, string translatedWord, string targetLanguage, string filePath)
        {
            translations.Add(new Translation
            {
                OriginalWord = originalWord,
                TranslatedWord = translatedWord,
                Language = targetLanguage
            });

            File.AppendAllText(filePath, $"{originalWord}-{translatedWord}-{targetLanguage}\n");
        }
    }
}
