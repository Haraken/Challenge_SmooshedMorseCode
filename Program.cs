using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Challenge_SmooshedMorseCode
{
    class Program
    {
        private static Dictionary<char, string> mMorseTranslator;

        static void Main(string[] args)
        {
            //Variables
            string userInput;
            string translation;
            bool looping = true;

            do
            {
                //Setting up Dictionnary to store alphanumeric character combined with their morse equivalent sequence.
                InitializeDictionnary();

                //Basic Header to use on Console start
                InterfaceInitialDisplay();

                //Asking for user input, reading it and sending it back.
                userInput = GetUserInput();

                translation = Translate(userInput);

                Console.WriteLine("\nTranslation result.\n===================");
                Console.WriteLine(translation + "\n");


                //Asking User for a restart.
                looping = GetUserRestart();
                Console.Clear();

                //Might be unessesary, as the application would normally close in the GetUserRestart, 
                //but the possibility of having some method changing the looping is left open.
            } while (looping);

            //End of ConsoleApp
        }

        /// <summary>
        /// Ask the user if he want to exit the app.
        /// </summary>
        /// <returns>If the user want to continue using the app</returns>
        private static bool GetUserRestart()
        {
            ConsoleKey userKey;

            Console.WriteLine("\nDo you want to Continue using the app? Y/N?");

            userKey = Console.ReadKey().Key;
            if (userKey != ConsoleKey.Y)
            {
                Console.WriteLine("\nExiting app... \n");
                Environment.Exit(0);
            }

            Console.WriteLine("\nReturning to begining... \n");
            return true;
        }

        /// <summary>
        /// Will determinate if userinput is alphanumeric, morse code or invalid after which it will call the appropriated conversion.
        /// </summary>
        /// <param name="userInput">user input to test.</param>
        /// <returns>Converted string or error code</returns>
        private static string Translate(string userInput)
        {
            //Test if input contains only Alphanumeric characters.
            if (Regex.IsMatch(userInput, "^[a-z0-9,\\s\\\"]*$"))
                return ToMorse(userInput);
            //Test if input contains only dashes and dots.
            if (Regex.IsMatch(userInput, "^[-.\\/\\s]*$"))
                return ToAlphanumeric(userInput);

            //If both test failed, return instead an error code to display on the console.
            return "The user input contains characters that isn't handled or cannot recognize which way to translate";
        }

        private static string ToAlphanumeric(string userInput)
        {
            //TODO: Deal with spaceless morse
            StringBuilder builder = new StringBuilder();
            string[] splittedInput = userInput.Trim().Split(' ');

            foreach (string s in splittedInput)
                if (mMorseTranslator.ContainsValue(s))
                    builder.Append(mMorseTranslator.FirstOrDefault(x => x.Value == s).Key);

            return builder.ToString();
        }

        private static string ToMorse(string userInput)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var character in userInput)
            {
                if (mMorseTranslator.ContainsKey(character))
                {
                    builder.Append(mMorseTranslator[character] + " ");
                }
                else if (character == ' ')
                {
                    builder.Append("/ ");
                }
                else
                {
                    builder.Append(character + " ");
                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Asking for user input, reading it and sending it back.
        /// </summary>
        /// <returns>User input as a string</returns>
        private static string GetUserInput()
        {
            Console.Write("Input:  ");
            return Console.ReadLine().ToLower();
        }

        /// <summary>
        /// Basic Header to use on Console start and after console clear.
        /// </summary>
        private static void InterfaceInitialDisplay()
        {
            Console.WriteLine("===========================================\nMorse Code Translator for Reddit Challenge.\n===========================================\n..");
        }

        /// <summary>
        /// Setting up Dictionnary to store alphanumeric character combined with their morse equivalent sequence.
        /// Credit to some guy on Reddit for listing the whole morse alphanumeric conversion
        /// </summary>
        private static void InitializeDictionnary()
        {
            //Dictionnary
            mMorseTranslator = new Dictionary<char, string>() {
                {'a', ".-"},
                {'b', "-..."},
                {'c', "-.-."},
                {'d', "-.."},
                {'e', "."},
                {'f', "..-."},
                {'g', "--."},
                {'h', "...."},
                {'i', ".."},
                {'j', ".---"},
                {'k', "-.-"},
                {'l', ".-.."},
                {'m', "--"},
                {'n', "-."},
                {'o', "---"},
                {'p', ".--."},
                {'q', "--.-"},
                {'r', ".-."},
                {'s', "..."},
                {'t', "-"},
                {'u', "..-"},
                {'v', "...-"},
                {'w', ".--"},
                {'x', "-..-"},
                {'y', "-.--"},
                {'z', "--.."},

                {'0', "-----"},
                {'1', ".----"},
                {'2', "..---"},
                {'3', "...--"},
                {'4', "....-"},
                {'5', "....."},
                {'6', "-...."},
                {'7', "--..."},
                {'8', "---.."},
                {'9', "----."},

                {' ', "/"},
                {',', "--..--"},
                {'\'', ".----."},
                {'/', "-..-."},
                {'"', ".-..-."}
            };
        }
    }
}
