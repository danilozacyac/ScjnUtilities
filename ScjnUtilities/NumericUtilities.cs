using System;
using System.Collections.Generic;
using System.Linq;

namespace ScjnUtilities
{
    public class NumericUtilities
    {

        /// <summary>
        /// Convierte un número binario en su equivalente en decimal
        /// </summary>
        /// <param name="binary">Número binario</param>
        /// <returns></returns>
        public static int BinaryToDecimal(string binary)
        {
            long l = Convert.ToInt64(binary, 2);
            int i = (int)l;
            return i;
        }

        /// <summary>
        /// Devuelve un número decinal en su representación binaria
        /// </summary>
        /// <param name="nDecimal"></param>
        /// <returns></returns>
        public static string ToBinary(Int64 nDecimal)
        {
            Int64 binaryHolder;
            char[] binaryArray;
            string binaryResult = "";

            while (nDecimal > 0)
            {
                binaryHolder = nDecimal % 2;
                binaryResult += binaryHolder;
                nDecimal = nDecimal / 2;
            }

            // The algoritm gives us the binary number in reverse order (mirrored)
            // We store it in an array so that we can reverse it back to normal
            binaryArray = binaryResult.ToCharArray();
            Array.Reverse(binaryArray);
            binaryResult = new string(binaryArray);

            return binaryResult;
        }

        /// <summary>
        /// Devuelve la representación binaria INVERTIDA de un número decimal
        /// </summary>
        /// <param name="nDecimal"></param>
        /// <returns></returns>
        public static string ToBinaryInvert(Int64 nDecimal)
        {
            // Declare a few variables we're going to need
            Int64 binaryHolder;
            char[] binaryArray;
            string binaryResult = "";

            while (nDecimal > 0)
            {
                binaryHolder = nDecimal % 2;
                binaryResult += binaryHolder;
                nDecimal = nDecimal / 2;
            }

            // The algoritm gives us the binary number in reverse order (mirrored)
            // We store it in an array so that we can reverse it back to normal
            binaryArray = binaryResult.ToCharArray();
            //Array.Reverse(BinaryArray);
            binaryResult = new string(binaryArray);

            return binaryResult;
        }


        public static List<int> GetDecimalsInBinary(int orNumber)
        {
            List<int> permission = new List<int>();

            char[] binary = Convert.ToString(orNumber, 2).ToCharArray();
            binary = binary.Reverse().ToArray();

            int index = 0;


            while (binary.Count() > 0 && index < binary.Count())
            {

                char pot = binary[index];

                if (pot.Equals('1'))
                {
                    double result = Math.Pow(2, index);
                    permission.Add(Convert.ToInt32(result));
                }

                index++;
            }

            return permission;
        }
    }
}
