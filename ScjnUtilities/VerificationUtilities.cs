using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ScjnUtilities
{
    public class VerificationUtilities
    {

        /// <summary>
        /// Caracteres no permitidos para agregar datos.
        /// </summary>
        public static String[] NoPermitidos = new String[] { "+", "=", "'", "&", "^", "$", "#", "@","-","\\",
                                 "!","¡","¿","?","<",">","~","¬","|","°",",",";",":","%","\n","\"","/",
                                 "(",")","[","]","{","}","´","¨","_","`","¥","€"};



        /// <summary>
        /// Verifica si el caracter ingresado es un caracter numérico
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsTextAllowed(string text)
        {
            // Regex NumEx = new Regex(@"^\d+(?:.\d{0,2})?$"); 
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text 
            return regex.IsMatch(text);
        }

        /// <summary>
        /// Verifica si el caracter ingresado es un caracter numérico
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsTextAllowed(char text)
        {
            if (Char.IsDigit(text))
                return true;
            else
                return false;
        }

        public static bool IsNumberOrGuion(string text)
        {
            // Regex NumEx = new Regex(@"^\d+(?:.\d{0,2})?$"); 
            Regex regex = new Regex("[^0-9-]+"); //regex that matches disallowed text 
            return regex.IsMatch(text);
        }

        public static bool IsNumber(string text)
        {
            // Regex NumEx = new Regex(@"^\d+(?:.\d{0,2})?$"); 
            Regex regex = new Regex("[^0-9]+"); //regex that matches disallowed text 
            return regex.IsMatch(text);
        }

        public static bool IsMailAddress(string mail)
        {
            Regex regex = new Regex("^[_a-z0-9-]+(.[_a-z0-9-]+)*@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$");
            return regex.IsMatch(mail);
        }


        public static string TextBoxStringValidation(string textoValidar)
        {
            while (textoValidar.Length > 0 && !Char.IsLetterOrDigit(textoValidar[0]))
            {
                if (!Char.IsLetterOrDigit(textoValidar[0]))
                    textoValidar = textoValidar.Substring(1);

                textoValidar = textoValidar.Trim();

            }

            return textoValidar.Trim();
        }

        public static bool IsbnValidation(string isbn)
        {

            Regex regex = new Regex(@"ISBN(-1(?:(0)|3))?:?\x20+(?(1)(?(2)(?:(?=.{13}$)\d{1,5}([ -])\d{1,7}\3\d{1,6}\3(?:\d|x)$)|(?:(?=.{17}$)97(?:8|9)([ -])\d{1,5}\4\d{1,7}\4\d{1,6}\4\d$))|(?(.{13}$)(?:\d{1,5}([ -])\d{1,7}\5\d{1,6}\5(?:\d|x)$)|(?:(?=.{17}$)97(?:8|9)([ -])\d{1,5}\6\d{1,7}\6\d{1,6}\6\d$)))");
            //Regex regex = new Regex(@"ISBN(-1(?:(0)|3))?:?\x20(\s)*[0-9]+[- ][0-9]+[- ][0-9]+[- ][0-9]*[- ]*[xX0-9]");

            return regex.IsMatch("ISBN: " + isbn);

        }

        public static bool ContieneCaractNoPermitidos(string texto)
        {
            return NoPermitidos.Contains(texto);
        }

    }
}
