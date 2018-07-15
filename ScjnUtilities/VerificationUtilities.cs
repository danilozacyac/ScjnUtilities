using KellermanSoftware.CompareNetObjects;
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


       
        /// <summary>
        /// Verifica si la cadena ingresada corresponde a la máscara de un RFC sin importar
        /// si se trata de personas físicas o morales. No se valida que el RFC exista, tan
        /// solo que el formato sea correcto
        /// </summary>
        /// <param name="rfc"></param>
        /// <returns></returns>
        public static bool IsValidRfc(string rfc)
        {
            Regex regex = new Regex(@"^([A-ZÑ&]{3,4}) ?(?:- ?)?(\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\d|3[01])) ?(?:- ?)?([A-Z\d]{2})([A\d])$");
            return regex.IsMatch(rfc);
        }

        /// <summary>
        /// Verifica si la cadena ingresada corresponde a la máscara de una CURP sin importar
        /// si se trata de personas físicas o morales. No se valida que el CURP exista, tan
        /// solo que el formato sea correcto
        /// </summary>
        /// <param name="curp"></param>
        /// <returns></returns>
        public static bool IsValidCurp(string curp)
        {
            Regex regex = new Regex(@"[A-Z]{4}\d{6}[HM][A-Z]{2}[B-DF-HJ-NP-TV-Z]{3}[A-Z0-9][0-9]");
            return regex.IsMatch(curp);
        }

        /// <summary>
        /// Agrega las propiedades que se deben de excluir de la comparasión que se realizará entre dos objetos
        /// </summary>
        /// <param name="compareLogic">Objeto que se configura para realizar comparación</param>
        /// <param name="excludedProperties">Propiedades que se habrán de excluir al momento de comparar</param>
        /// <returns></returns>
        public static CompareLogic IsEqualObjectExclusion(CompareLogic compareLogic, string[] excludedProperties)
        {
            foreach (string property in excludedProperties)
                compareLogic.Config.MembersToIgnore.Add(property);

            return compareLogic;
        }

        /// <summary>
        /// Agerga las propiedades que se deben de incluir durante la comparasión que se realizará de dos objetos
        /// Documentación
        /// https://github.com/GregFinzer/Compare-Net-Objects/wiki/Getting-Started
        /// </summary>
        /// <param name="compareLogic">Objeto que se configura para realizar comparación</param>
        /// <param name="includedProperties">Propiedades que se habrán de incluir al momento de comparar</param>
        /// <returns></returns>
        public static CompareLogic IsEqualObjectInclusion(CompareLogic compareLogic, string[] includedProperties)
        {
            foreach (string property in includedProperties)
                compareLogic.Config.MembersToInclude.Add(property);

            return compareLogic;
        }

    }
}
