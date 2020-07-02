using System;
using System.Collections.Generic;
using System.Text;

namespace DogusTeknoloji.SmartKPIMiner.Tests.Mocks
{
    public static class RandomStringGenerator
    {
        private readonly static Random _randomObj = new Random();
        private readonly static char[] _alphabet = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private readonly static char[] _numbers = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private readonly static char[] _specialChars = new[] { '@', '.', '_', ',', '?', '*', '!', '\\', '/', '&', '%', '+', '-', '=' };

        public static char GetRandomChar()
        {
            char result = GetRandomChar(includeNumbers: false, includeSpecialChars: false);
            return result;
        }
        public static char GetRandomChar(bool includeNumbers)
        {
            char result = GetRandomChar(includeNumbers, includeSpecialChars: false);
            return result;
        }
        public static char GetRandomChar(bool includeNumbers, bool includeSpecialChars)
        {
            List<char> searchDictionary = new List<char>();
            searchDictionary.AddRange(_alphabet);
            if (includeNumbers) { searchDictionary.AddRange(_numbers); }
            if (includeSpecialChars) { searchDictionary.AddRange(_specialChars); }

            int random = _randomObj.Next(0, searchDictionary.Count);
            char generatedChar = (char)random;

            return generatedChar;
        }

        public static string GetRandomString(int length)
        {
            string result = GetRandomString(length, includeNumbers: false, includeSpecialChars: false);
            return result;
        }
        public static string GetRandomString(int length, bool IncludeNumbers)
        {
            string result = GetRandomString(length, IncludeNumbers, includeSpecialChars: false);
            return result;
        }
        public static string GetRandomString(int length, bool includeNumbers, bool includeSpecialChars)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                char generatedChar = GetRandomChar(includeNumbers, includeSpecialChars);
                stringBuilder.Append(generatedChar);
            }
            return stringBuilder.ToString();
        }
    }
}