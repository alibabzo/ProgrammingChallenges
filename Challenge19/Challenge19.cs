﻿/* Challenge 19
 * Write a program to perform a basic ‘Caesar’ encryption
 * and decryption on text. This algorithm works by moving
 * letters along by an ‘offset’.
 * If the offset is 2 then b —> d, h—>j etc.
 * Try to write two functions—One to encrypt, one to decrypt. Both will return a string.
 *
 * - The user selects whether the wish to encrypt or decrypt.
 * - The user enters sentence to encrypt and the encryption key
 *   (i.e. How many we move the letters along—this is a smallish integer)
 *
 * The program responds with the encrypted or decrypted version.
 *
 * Challenge 19 Continued
 * The Caesar algorithms may have fooled the Gauls but it doesn't
 * take a genius to crack. So a much better algorithm would be one
 * that has a different offset for every letter. We can do this
 * using the random number generator because it generates the same
 * sequence of random numbers from a ‘seed’ position. As long as the
 * sender and receiver agree where to ‘seed’ (the encryption key)
 * they can both work out the same offsets.
 *
 * Extension
 * Try to use a single function with an extra parameter to
 * indicate whether the text is being encrypted or decrypted
 * rather than having two different functions.
 */

using System;

namespace Challenge19
{
    class Challenge19
    {
        static readonly char[] Chars = {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h',
            'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',
            'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
            'y', 'z', '0', '1', '2', '3', '4', '5',
            '6', '7', '8', '9', 'A', 'B', 'C', 'D',
            'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L',
            'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
            'U', 'V', 'W', 'X', 'Y', 'Z', '!', '@',
            '#', '$', '%', '^', '&', '(', ')', '+',
            '-', '*', '/', '[', ']', '{', '}', '=',
            '<', '>', '?', '_', '"', '.', ',', ' '
        };

        static void Main(string[] args)
        {
            var program = true;
            do {
                int seedposition = OffsetInput("Set seed position: ");

                Console.Write("\nEncrypt (e) or decrypt (d)?: ");
                switch (char.ToLower(Console.ReadKey().KeyChar)) {
                    case 'e':
                        string enc = Encrypt(UserInput("Enter text to encrypt: "), seedposition);
                        Console.WriteLine("Encrypted text: " + enc);
                        break;
                    case 'd':
                        string dec = Decrypt(UserInput("Enter text to decrypt: "), seedposition);
                        Console.WriteLine("Decrypted text: " + dec);
                        break;
                    default:
                        Console.WriteLine("That's not an option!");
                        break;
                }
                Console.Write("\nEncrypt or decrypt another string? [y/n]: ");
                program = (char.ToLower(Console.ReadKey().KeyChar) != 'n');
            } while (program);
        }

        static int OffsetInput(string prompt)
        {
            int providedvalue;
            bool isnumber;
            do {
                Console.Write("\n" + prompt);
                isnumber = int.TryParse(Console.ReadLine(), out providedvalue);
                if (!isnumber) {
                    Console.WriteLine("That's not a number");
                }
            } while (!isnumber);
            return providedvalue;
        }

        static string UserInput(string prompt)
        {
            Console.Write("\n" + prompt);
            return Console.ReadLine();
        }

        static string Encrypt(string input, int seed)
        {
            char[] output = input.ToCharArray();
            var random = new Random(seed);
            for (int i = 0; i < output.Length; i++) {
                int offset = random.Next();
                for (int j = 0; j < Chars.Length; j++) {
                    if (output[i] == Chars[j]) {
                        output[i] = Chars[(j + offset) % Chars.Length];
                        break;
                    }
                }
            }
            return new string(output);
        }

        static string Decrypt(string input, int seed)
        {
            char[] output = input.ToCharArray();
            var random = new Random(seed);

            for (int i = 0; i < output.Length; i++) {
                int offset = random.Next();
                for (int j = 0; j < Chars.Length; j++) {
                    if (output[i] == Chars[j]) {
                        for (int k = 1; k < Chars.Length; k++) {    // couldn't find a better way to reverse the modulo
                            if ((k + offset) % Chars.Length == j) {
                                output[i] = Chars[k];
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            return new string(output);
        }
    }
}
