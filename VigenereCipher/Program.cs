using System;

namespace VigenereCipher
{
    public class VigenereCipher
    {
        //alphabets
        const string RUalphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        const string ENalphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public string alphabet;

        //key generation
        private string GetRepeatKey(string s, int n)
        {
            var p = s;
            while (p.Length < n)
            {
                p += p;
            }
            return p.Substring(0, n);
        }

        private string EncryptDecrypt(string language, string text, string key, bool encrypting = true)
        {
            if (language == "RU")
            {
                alphabet = RUalphabet;
            }
            else if (language == "EN")
            {
                alphabet = ENalphabet;
            }
            var fullAlphabet = alphabet + alphabet.ToLower();
            var gKey = GetRepeatKey(key, text.Length);
            var returnValue = "";
            var q = fullAlphabet.Length;
            for (int i = 0; i < text.Length; i++)
            {
                var letterIndex = fullAlphabet.IndexOf(text[i]);
                var codeIndex = fullAlphabet.IndexOf(gKey[i]);
                if (letterIndex < 0)
                {
                    //if the symbol is not found, then add it unchanged
                    returnValue += text[i].ToString();
                }
                else
                {
                    returnValue += fullAlphabet[(q + letterIndex + ((encrypting ? 1 : -1) * codeIndex)) % q].ToString();
                }
            }
            return returnValue;
        }

        //text encryption
        public string Encrypt(string language, string plainMessage, string key)
            => EncryptDecrypt(language, plainMessage, key);

        //text decryption
        public string Decrypt(string language, string encryptedMessage, string key)
            => EncryptDecrypt(language, encryptedMessage, key, false);
    }

    class Program
    {
        static void Main(string[] args)
        {
            var cipher = new VigenereCipher();
            string language;
            while (true)
            {
                Console.Write("Enter the required language (\"RU\" or \"EN\"): ");
                var answer = Console.ReadLine();
                if (answer == "RU")
                {
                    language = "RU";
                    break;
                }
                else if (answer == "EN")
                {
                    language = "EN";
                    break;
                }
            }
            Console.Write("Enter text: ");
            var message = Console.ReadLine().ToUpper();
            Console.Write("Enter key: ");
            var key = Console.ReadLine().ToUpper();
            var encryptedText = cipher.Encrypt(language, message, key);
            Console.WriteLine("Encrypted message: {0}", encryptedText);
            Console.WriteLine("Decrypted message: {0}", cipher.Decrypt(language, encryptedText, key));
            Console.ReadLine();
        }
    }
}