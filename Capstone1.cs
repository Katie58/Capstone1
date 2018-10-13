using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace PigLatin
{
    class Program
    {
        static void Main(string[] args)
        {
            bool retry = true;

            while (retry)
            {
                Translator();
                retry = Retry(retry);
            }
            Exit();
        }

        static void Translator()
        {
            Console.Clear();
            Console.WriteLine("Welcome, to the PIG LATIN translator!\n\nPlease enter a word or phrase: ");

            string input = null;
            Regex alpha = new Regex("[a-zA-Z]");

            do//check for valid entry
            {
                if (input != null)
                {
                    Console.WriteLine("There is nothing to translate, please try again: ");
                }
                input = Console.ReadLine();
            }
            while (!alpha.IsMatch(input));

            string[] wordsArray = input.Split(' ');//create array of words
            string translation = null;

            for (int i = 0; i < wordsArray.Length; i++)//for each word
            {
                bool consonant = false;
                bool symbolPresent = false;
                string element = wordsArray[i];

                //allow puncuation point
                bool punctuationMove = false;
                char punctuation = ' ';
                string punctuations = ",;:.?!-";
                if (punctuations.Contains(element[element.Length - 1]))
                {
                    punctuation = element[element.Length - 1];
                    punctuationMove = true;
                }

                foreach (char symbol in element)//for each character
                {
                    foreach (char character in element)//check for symbols
                    {
                        string charTemp = Convert.ToString(character);
                        if (!alpha.IsMatch(charTemp) && !charTemp.Contains('\''))
                        {
                            symbolPresent = true;
                        }
                    }
                    if (!symbolPresent || punctuationMove)//**translate**
                    {
                        string vowels = "aeiouAEIOU";
                        while (!vowels.Contains(element[0]))
                        {
                            if (punctuationMove)//remove punctuation point
                            {
                                element = element.Substring(0, element.Length - 1);
                            }
                            element = element.Substring(1, element.Length - 1) + element.Substring(0, 1);//shift letters
                            consonant = true;
                        }
                        if (consonant)//add suffixes
                        {
                            wordsArray[i] = element + "ay";
                        }
                        else
                        {
                            wordsArray[i] = element + "way";
                        }
                        if (punctuationMove)//reinsert punctuation point
                        {
                            wordsArray[i] = wordsArray[i] + punctuation;
                        }
                    }
                }
            }
            foreach (string word in wordsArray)//recreate string to display on one line
            {
                translation = translation + word + " ";
            }
            Console.WriteLine("\nPIG LATIN:\n" + translation);
            Console.WriteLine("\nWould you like to enter another word/phrase? (y/n)");
        }


        static bool Retry(bool retry)
        {
            char answer = Console.ReadKey().KeyChar;
            if (answer == 'y' || answer == 'Y')
            {
                return true;
            }
            else if (answer == 'n' || answer == 'N')
            {
                Console.WriteLine("\nGOOD BYE! (Press ESCAPE to Exit)");
                return false;
            }
            else
            {
                Retry(retry);
                return retry;
            }
        }

        static void Exit()
        {
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                Exit();
            }
        }
    }
}//October 12, 2018
