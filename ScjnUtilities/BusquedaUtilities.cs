using System;
using System.Linq;

namespace ScjnUtilities
{
    public class BusquedaUtilities
    {

        /// <summary>
        /// Las palabras que no se incluyen en busquedas o que no deben ser pintadas en los
        /// resultados de las mismas.
        /// </summary>
        public static String[] Stopers = new String[]{"el","la","las", "le","lo", "los", "no", ".", 
            "pero", "puede","se", "sus", "y", "o", "n","a", "al", "aquel", "aun", "cada", "como", "con", "cual", 
            "de", "debe", "deben", "del", "el", "en", "este", "esta", "la", "las", "le", "lo", "los", 
            "para", "pero", "por", "puede", "que", "se", "sin", "sus", "un", "una"};


        /// <summary>
        /// Los separadores comunes de las palabras.
        /// </summary>
        public static String[] Separadores = new String[] { " ", ",", ".", "\n", "\"", ";", ":", "'", "´", "‘", ")", "(" };

        /// <summary>
        /// Caracteres no permitidos en un formulario, concretamente en el campo de correo electrónico
        /// </summary>
        public static String[] NOPermitidosCorreo = new String[] { "+", "=", "'", "&", "^", "$", "#",
                                 "!","¡","¿","?","<",">","~","¬","|","°",",",";",";","%","\n",
                                 "(",")","[","]","{","}","´","¨","`","¥","€", "\""};
        /// <summary>
        /// Caracteres no permitidos en una búsqueda por palabra.
        /// </summary>
        public static String[] NOPermitidos = new String[] { "+", "=", "'", "&", "^", "$", "#", "@","-","\\",
                                 "!","¡","¿","?","<",">","~","¬","|","°",";",";","%","\n",
                                 "(",")","[","]","{","}","´","¨","_","`","¥","€"};

        private static readonly String empiezaCon = "|";
        private static readonly String terminaCon = "'";

        public static string Normaliza(string item)
        {
            String resultado = item.ToLower();
            resultado = resultado.Trim(empiezaCon.ToCharArray());
            resultado = resultado.Trim(terminaCon.ToCharArray());
            resultado = resultado.Replace('ñ', 'n');
            resultado = resultado.Replace('á', 'a');
            resultado = resultado.Replace('é', 'e');
            resultado = resultado.Replace('í', 'i');
            resultado = resultado.Replace('ó', 'o');
            resultado = resultado.Replace('ú', 'u');
            resultado = resultado.Replace('ä', 'a');
            resultado = resultado.Replace('ë', 'e');
            resultado = resultado.Replace('ï', 'i');
            resultado = resultado.Replace('ö', 'o');
            resultado = resultado.Replace('ü', 'u');
            resultado = resultado.Replace("*", "");
            return resultado.ToUpper();
        }

        public static string NormalizaSinAsterisco(string item)
        {
            String resultado = item.ToLower();
            resultado = resultado.Trim(empiezaCon.ToCharArray());
            resultado = resultado.Trim(terminaCon.ToCharArray());
            //resultado = resultado.Replace('ñ', 'n');
            resultado = resultado.Replace('á', 'a');
            resultado = resultado.Replace('é', 'e');
            resultado = resultado.Replace('í', 'i');
            resultado = resultado.Replace('ó', 'o');
            resultado = resultado.Replace('ú', 'u');
            resultado = resultado.Replace('ä', 'a');
            resultado = resultado.Replace('ë', 'e');
            resultado = resultado.Replace('ï', 'i');
            resultado = resultado.Replace('ö', 'o');
            resultado = resultado.Replace('ü', 'u');
            return resultado.ToUpper();
        }
    }
}
