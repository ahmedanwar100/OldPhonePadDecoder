using System.Text;

namespace OldPhonePadDecoder.Core
{
    /// <summary>
    /// Contains the implementation to decode keypad input into text using old phone key layout.
    /// </summary>
    public class OldPhonePadDecoder
    {

        /// <summary>
        /// Decodes a keypad string input into a text based output using the old mobile phone layout.
        /// </summary>
        /// <param name="input">
        /// A string containing numeric characters (2–9), `*` for backspace, and `#` for send.
        /// A space indicates a pause to allow typing the same button multiple times for different letters.
        /// </param>
        /// <returns>The decoded text up to the first `#` character.</returns>
        /// <example>
        /// Input: "8 88777444666*664#"  
        /// Output: "TURING"
        /// </example>
        public static string OldPhonePad(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException(nameof(input), "Provided input should have a proper value.");
            }

            //to store the final result
            var result = new StringBuilder();

            //temporary storage for processing
            var tempStorage = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char current = input[i];

                if (current == '#')
                    break;

                if (current == '*')
                {
                    if (tempStorage.Length > 0)
                    {
                        //this will delete the digit(s) being typed
                        tempStorage.Clear();
                    }

                    continue;
                }

                //if there is space then map the existing digits to characters
                if (current == ' ')
                {
                    if (tempStorage.Length > 0)
                    {
                        result.Append(MapDigitToCharacter(tempStorage.ToString()));

                        //clear the temporary storage
                        tempStorage.Clear();
                    }

                    continue;
                }

                //if the tempStorage is empty or the digits are repeating then add in the tempStorage
                if (tempStorage.Length == 0 || tempStorage[tempStorage.Length - 1] == current)
                {
                    tempStorage.Append(current);
                }
                else
                {
                    //different digit found
                    result.Append(MapDigitToCharacter(tempStorage.ToString()));

                    //clear the temporary storage
                    tempStorage.Clear();

                    //add the new digit
                    tempStorage.Append(current);
                }
            }

            //if the temp storage still has some characters left then process them
            if (tempStorage.Length > 0)
            {
                result.Append(MapDigitToCharacter(tempStorage.ToString()));

                //clear the temp storage
                tempStorage.Clear();
            }

            return result.ToString();
        }

        private static char MapDigitToCharacter(string sequenceOfDigits)
        {
            char key = sequenceOfDigits[0];

            int count = sequenceOfDigits.Length;

            string keyCharacters = string.Empty;

            //find the mapping against the key(digit)
            switch (key)
            {
                case '2': { keyCharacters = "ABC"; break; }
                case '3': { keyCharacters = "DEF"; break; }
                case '4': { keyCharacters = "GHI"; break; }
                case '5': { keyCharacters = "JKL"; break; }
                case '6': { keyCharacters = "MNO"; break; }
                case '7': { keyCharacters = "PQRS"; break; }
                case '8': { keyCharacters = "TUV"; break; }
                case '9': { keyCharacters = "WXYZ"; break; }
                case '0': { keyCharacters = " "; break; }
                default: keyCharacters = ""; break;
            };

            if (string.IsNullOrEmpty(keyCharacters))
            {
                throw new InvalidOperationException($"Not found any character against the key:{key}");
            }

            int index = (count - 1) % keyCharacters.Length;

            return keyCharacters[index];
        }
    }
}
