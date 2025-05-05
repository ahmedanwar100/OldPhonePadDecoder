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

                //come out of the loop as '#' has been found
                if (current == '#')
                    break;

                //empty the tempStorage as '*' has been found
                if (current == '*')
                {
                    if (tempStorage.Length > 0)
                    {
                        //this will delete the digit(s) being typed
                        tempStorage.Clear();
                    }

                    continue;
                }

                //if there is space then map the existing digit(s) to a character
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

                //if the tempStorage is empty or the digits are repeating then keep adding them in the tempStorage
                //otherwise since a different digit has been found and hence first map the existing ones in the tempStorage it to a relevant character,
                //clear the tempStorage and then add the new digit to the tempStorage.
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
            //the digits will either repeat or will be just one e.g. '222', '2'
            //so we can just take the first character i.e. sequenceOfDigits[0] 
            char key = sequenceOfDigits[0];

            //it will be used to find the index
            int count = sequenceOfDigits.Length;

            //for storing the mapping
            string keyCharacters = string.Empty;

            //find the mapping against the key -> digit
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

            //get the index of the character
            int index = (count - 1) % keyCharacters.Length;

            return keyCharacters[index];
        }
    }
}
