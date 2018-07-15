using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace ScjnUtilities
{
    public class StringUtilities
    {
        private static Dictionary<char, int> valorRomano;

        private static void InitializeRomanos()
        {
            if (valorRomano == null)
            {
                valorRomano = new Dictionary<char, int>();

                valorRomano.Add('I', 1);
                valorRomano.Add('V', 5);
                valorRomano.Add('X', 10);
                valorRomano.Add('L', 50);
                valorRomano.Add('C', 100);
                valorRomano.Add('D', 500);
                valorRomano.Add('M', 1000);
            }
        }


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

        /// <summary>
        /// Convierte un númeor romano a su equivalente en decimal
        /// </summary>
        /// <param name="romano"></param>
        /// <returns></returns>
        public static int RomanosADecimal(String romano)
        {
            StringUtilities.InitializeRomanos();

            char[] nums = romano.ToCharArray();
            //List<int> dec = new List<int>();
            char ultimoNumero = ' ';
            int numeroDecimal = 0;

            foreach (char letra in nums)
            {
                if (ultimoNumero.Equals(' '))
                {
                    numeroDecimal = valorRomano[letra];
                }
                else if (valorRomano[letra] > valorRomano[ultimoNumero])
                {
                    numeroDecimal -= valorRomano[ultimoNumero];

                    numeroDecimal += ((valorRomano[letra] - valorRomano[ultimoNumero]));
                }
                else if ((valorRomano[ultimoNumero] > valorRomano[letra]) || (valorRomano[ultimoNumero] == valorRomano[letra]))
                {
                    numeroDecimal += valorRomano[letra];
                }

                ultimoNumero = letra;
            }

            return numeroDecimal;
        }

        /// <summary>
        /// Verifica si todas las letras de una palabra son mayúsculas
        /// </summary>
        /// <param name="input">Texto a verificar</param>
        /// <returns></returns>
        public static bool IsAllUpper(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsLetter(input[i]) && !Char.IsUpper(input[i]))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Devuelve la cadena con la primer letra, SOLO LA PRIMERA,  en mayúscula
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        /// <summary>
        /// Devuelve una cadena tipo título, es decir todas las primeras letras
        /// de las palabras serán mayúsculas
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public static string CambiaLtr123(string cLineaPr, string cCarOld, string cCarNvo)
        {
            string cLinea = cLineaPr;
            string cLin = String.Empty;
            string cLineaTmp;
            int nPos;
            int nLengthLinea;
            int nLengthCarOld = cCarOld.Length;
            const int nAct = 1;
            nPos = cLinea.IndexOf(cCarOld, nAct);

            while (nPos > 0)
            {
                cLin = cLin + cLinea.Substring(0, nPos) + cCarNvo;
                nLengthLinea = cLinea.Length - (nPos + nLengthCarOld);
                cLineaTmp = cLinea.Substring(nPos + nLengthCarOld, nLengthLinea);
                cLinea = cLineaTmp;

                if (cLinea != "")
                {
                    nPos = cLinea.IndexOf(cCarOld, nAct);
                }
                else
                {
                    nPos = 0;
                }

            }
            cLin = cLin + cLinea;

            return cLin;
        }  // fin CambiaLtr123

        /// <summary>
        /// Elimina los carácteres especiales de una cadena de texto, manteniendo 
        /// únicamente los alfanuméricos
        /// </summary>
        /// <param name="cCadena">Cadena de Texto</param>
        /// <returns></returns>
        public static string QuitaCarCad(string cCadena)
        {
            string cChr = "";
            string sCadena = cCadena;

            sCadena = CambiaLtr123(sCadena, "+", " ");
            sCadena = CambiaLtr123(sCadena, "=", " ");
            sCadena = CambiaLtr123(sCadena, "*", " ");
            sCadena = CambiaLtr123(sCadena, "&", " ");
            sCadena = CambiaLtr123(sCadena, "^", " ");
            sCadena = CambiaLtr123(sCadena, "$", " ");

            sCadena = CambiaLtr123(sCadena, "#", " ");
            sCadena = CambiaLtr123(sCadena, "@", " ");
            sCadena = CambiaLtr123(sCadena, "!", " ");
            sCadena = CambiaLtr123(sCadena, "¡", " ");
            sCadena = CambiaLtr123(sCadena, "?", " ");
            sCadena = CambiaLtr123(sCadena, "¿", " ");
            sCadena = CambiaLtr123(sCadena, "<", " ");
            sCadena = CambiaLtr123(sCadena, ">", " ");
            sCadena = CambiaLtr123(sCadena, "~", " ");

            sCadena = CambiaLtr123(sCadena, "|", " ");
            sCadena = CambiaLtr123(sCadena, "°", " ");
            sCadena = CambiaLtr123(sCadena, "ª", " ");
            sCadena = CambiaLtr123(sCadena, "º", " ");

            sCadena = CambiaLtr123(sCadena, ".", " ");
            sCadena = CambiaLtr123(sCadena, ",", " ");
            sCadena = CambiaLtr123(sCadena, ":", " ");
            sCadena = CambiaLtr123(sCadena, ";", " ");
            sCadena = CambiaLtr123(sCadena, "%", " ");

            sCadena = CambiaLtr123(sCadena, "(", " ");
            sCadena = CambiaLtr123(sCadena, ")", " ");
            sCadena = CambiaLtr123(sCadena, "[", " ");
            sCadena = CambiaLtr123(sCadena, "]", " ");
            sCadena = CambiaLtr123(sCadena, "{", " ");
            sCadena = CambiaLtr123(sCadena, "}", " ");
            sCadena = CambiaLtr123(sCadena, "`", " ");
            sCadena = CambiaLtr123(sCadena, "-", " ");
            sCadena = CambiaLtr123(sCadena, "_", " ");
            sCadena = CambiaLtr123(sCadena, "/", " ");


            cChr = Convert.ToChar(92).ToString();
            sCadena = CambiaLtr123(sCadena, cChr, " ");
            sCadena = CambiaLtr123(sCadena, "'", " ");

            cChr = Convert.ToChar(34).ToString();
            sCadena = CambiaLtr123(sCadena, cChr, " ");

            cChr = Convert.ToChar(13).ToString();
            sCadena = CambiaLtr123(sCadena, cChr, " ");

            cChr = Convert.ToChar(10).ToString();
            sCadena = CambiaLtr123(sCadena, cChr, " ");


            return sCadena;
        }  // fin QuitaCarCad

        /// <summary>
        /// Elimina los caracteres especiales y numéricos de una cadena de texto,
        /// prepara el texto para ser ordenado
        /// </summary>
        /// <param name="cCadena"></param>
        /// <returns></returns>
        public static string QuitaCarOrden(string cCadena)
        {
            string cChr = "";
            string sCadena = cCadena;

            sCadena = CambiaLtr123(sCadena, "+", "");
            sCadena = CambiaLtr123(sCadena, "=", "");
            sCadena = CambiaLtr123(sCadena, "*", "");
            sCadena = CambiaLtr123(sCadena, "&", "");
            sCadena = CambiaLtr123(sCadena, "^", "");
            sCadena = CambiaLtr123(sCadena, "$", "");

            sCadena = CambiaLtr123(sCadena, "#", "");
            sCadena = CambiaLtr123(sCadena, "@", "");
            sCadena = CambiaLtr123(sCadena, "!", "");
            sCadena = CambiaLtr123(sCadena, "¡", "");
            sCadena = CambiaLtr123(sCadena, "?", "");
            sCadena = CambiaLtr123(sCadena, "¿", "");
            sCadena = CambiaLtr123(sCadena, "<", "");
            sCadena = CambiaLtr123(sCadena, ">", "");
            sCadena = CambiaLtr123(sCadena, "~", "");

            sCadena = CambiaLtr123(sCadena, "|", "");
            sCadena = CambiaLtr123(sCadena, "°", "");
            sCadena = CambiaLtr123(sCadena, "ª", "");
            sCadena = CambiaLtr123(sCadena, "º", "");

            sCadena = CambiaLtr123(sCadena, ".", "");
            sCadena = CambiaLtr123(sCadena, ",", "");
            sCadena = CambiaLtr123(sCadena, ":", "");
            sCadena = CambiaLtr123(sCadena, ";", "");
            sCadena = CambiaLtr123(sCadena, "%", "");

            sCadena = CambiaLtr123(sCadena, "(", "");
            sCadena = CambiaLtr123(sCadena, ")", "");
            sCadena = CambiaLtr123(sCadena, "[", "");
            sCadena = CambiaLtr123(sCadena, "]", "");
            sCadena = CambiaLtr123(sCadena, "{", "");
            sCadena = CambiaLtr123(sCadena, "}", "");
            sCadena = CambiaLtr123(sCadena, "`", "");
            sCadena = CambiaLtr123(sCadena, "-", "");
            sCadena = CambiaLtr123(sCadena, "_", "");
            sCadena = CambiaLtr123(sCadena, "/", "");



            cChr = Convert.ToChar(92).ToString();
            sCadena = CambiaLtr123(sCadena, cChr, "");
            sCadena = CambiaLtr123(sCadena, "'", "");

            cChr = Convert.ToChar(34).ToString();
            sCadena = CambiaLtr123(sCadena, cChr, "");

            cChr = Convert.ToChar(13).ToString();
            sCadena = CambiaLtr123(sCadena, cChr, "");

            cChr = Convert.ToChar(10).ToString();
            sCadena = CambiaLtr123(sCadena, cChr, "");

            char comienza = Convert.ToChar(sCadena.Substring(0, 1));
            if (Char.IsDigit(comienza))
            {
                sCadena = sCadena.Replace("0", "");
                sCadena = sCadena.Replace("1", "");
                sCadena = sCadena.Replace("2", "");
                sCadena = sCadena.Replace("3", "");
                sCadena = sCadena.Replace("4", "");
                sCadena = sCadena.Replace("5", "");
                sCadena = sCadena.Replace("6", "");
                sCadena = sCadena.Replace("7", "");
                sCadena = sCadena.Replace("8", "");
                sCadena = sCadena.Replace("9", "");

            }


            return sCadena;
        }  // fin QuitaCarCad

        /// <summary>
        /// Convierte las vocales minúsculas en mayúsculas eliminando los acentos,
        /// también elimina diéresis y la tilde de la ñ
        /// </summary>
        /// <param name="cCadena">Cadena de Texto que será modificada</param>
        /// <returns></returns>
        public static string ConvMay(string cCadena)
        {
            string sCadena = cCadena;

            sCadena = sCadena.Replace("á", "A");
            sCadena = sCadena.Replace("é", "E");
            sCadena = sCadena.Replace("í", "I");
            sCadena = sCadena.Replace("ó", "O");
            sCadena = sCadena.Replace("ú", "U");

            sCadena = sCadena.Replace("ñ", "Ñ");

            sCadena = sCadena.Replace("ü", "U");
            sCadena = sCadena.Replace("Ü", "U");
            sCadena = sCadena.Replace("Á", "A");
            sCadena = sCadena.Replace("É", "E");
            sCadena = sCadena.Replace("Í", "I");
            sCadena = sCadena.Replace("Ó", "O");
            sCadena = sCadena.Replace("Ú", "U");

            sCadena.ToUpper();
            return sCadena;
        } // fin ConvMay

        /// <summary>
        /// Convierte las vocales minúsculas en mayúsculas eliminando los acentos,
        /// también elimina diéresis
        /// </summary>
        /// <param name="cCadena"></param>
        /// <returns></returns>
        public static string ConvMayEne(string cCadena)
        {
            string sCadena = cCadena;

            sCadena = CambiaLtr123(sCadena, "á", "A");
            sCadena = CambiaLtr123(sCadena, "é", "E");
            sCadena = CambiaLtr123(sCadena, "í", "I");
            sCadena = CambiaLtr123(sCadena, "ó", "O");
            sCadena = CambiaLtr123(sCadena, "ú", "U");
            sCadena = CambiaLtr123(sCadena, "ü", "U");
            sCadena = CambiaLtr123(sCadena, "Ü", "U");
            sCadena = CambiaLtr123(sCadena, "Á", "A");
            sCadena = CambiaLtr123(sCadena, "É", "E");
            sCadena = CambiaLtr123(sCadena, "Í", "I");
            sCadena = CambiaLtr123(sCadena, "Ó", "O");
            sCadena = CambiaLtr123(sCadena, "Ú", "U");

            sCadena.ToUpper();
            return sCadena;
        } // fin ConvMay


        /// <summary>
        /// Prepara una cadena para ser ordenada alfabéticamente
        /// </summary>
        /// <param name="cCadena">Cadena que será ordenada</param>
        /// <returns></returns>
        public static string PrepareToAlphabeticalOrder(string cCadena)
        {

            cCadena = ConvMay(cCadena.ToUpper());
            cCadena = QuitaCarCad(cCadena);
            cCadena = QuitaCarOrden(cCadena);
            cCadena = QuitaCarOrden(cCadena);
            cCadena = QuitaCarOrden(cCadena);
            cCadena = ReplaceDoubleSpaces(cCadena);
            cCadena = ConvMay(cCadena);
            cCadena = ConvMayEne(cCadena);
            cCadena = NumericAlphabeticalOrder(cCadena);
            cCadena = cCadena.Trim();

            return cCadena.ToUpper();
        }


        private static String NumericAlphabeticalOrder(String cCadena)
        {
            String texto = "";
            cCadena = StringUtilities.ConvMay(StringUtilities.QuitaCarCad(cCadena)).ToUpper();

            foreach (String palabra in cCadena.Split(' '))
            {
                int x = 0;
                bool result = Int32.TryParse(palabra, out x);

                if (!result)
                {
                    String numeric = "";
                    String complement = "";
                    foreach (char letra in palabra.ToCharArray())
                    {
                        if (Char.IsDigit(letra))
                        {
                            numeric += letra;
                        }
                        else
                            complement += letra;

                    }
                    numeric = SetCeros(numeric) + complement;

                    texto += numeric;
                }
                else
                {
                    texto += SetCeros(palabra);
                }

            }

            return cCadena;
        }

        /// <summary>
        /// Agrega ceros a la izquierda del número que recibe para que la longitud de la cadena resultante siempre
        /// sea de 4 
        /// </summary>
        /// <param name="cCadena">Cadena que se reemplazará</param>
        /// <returns></returns>
        public static String SetCeros(String cCadena)
        {
            switch (cCadena.Length)
            {
                case 1: return cCadena = "000" + cCadena;

                case 2: return cCadena = "00" + cCadena;

                case 3: return cCadena = "0" + cCadena;

                default: return cCadena;
            }

        }

        public static string ReplaceSaltoDeLinea(string cCadena)
        {
            string cChr = "";
            string sCadena = cCadena;

            cChr = Convert.ToChar(92).ToString();
            sCadena = CambiaLtr123(sCadena, cChr, "");
            sCadena = CambiaLtr123(sCadena, "'", "");

            cChr = Convert.ToChar(34).ToString();
            sCadena = CambiaLtr123(sCadena, cChr, "");

            cChr = Convert.ToChar(13).ToString();
            sCadena = CambiaLtr123(sCadena, cChr, "");

            cChr = Convert.ToChar(10).ToString();
            sCadena = CambiaLtr123(sCadena, cChr, "");

            return sCadena;
        }


        public static string ReplaceDoubleSpaces(string texto)
        {
            return Regex.Replace(texto, @"\s+", " ");
        }

        public static int LevenshteinDistance(string s, string t, out double porcentaje)
        {
            porcentaje = 0;

            // d es una tabla con m+1 renglones y n+1 columnas
            int costo = 0;
            int m = s.Length;
            int n = t.Length;
            int[,] d = new int[m + 1, n + 1];

            // Verifica que exista algo que comparar
            if (n == 0) return m;
            if (m == 0) return n;

            // Llena la primera columna y la primera fila.
            for (int i = 0; i <= m; d[i, 0] = i++) ;
            for (int j = 0; j <= n; d[0, j] = j++) ;


            /// recorre la matriz llenando cada unos de los pesos.
            /// i columnas, j renglones
            for (int i = 1; i <= m; i++)
            {
                // recorre para j
                for (int j = 1; j <= n; j++)
                {
                    /// si son iguales en posiciones equidistantes el peso es 0
                    /// de lo contrario el peso suma a uno.
                    costo = (s[i - 1] == t[j - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1,  //Eliminacion
                                  d[i, j - 1] + 1),                             //Inserccion 
                                  d[i - 1, j - 1] + costo);                     //Sustitucion
                }
            }

            /// Calculamos el porcentaje de cambios en la palabra.
            if (s.Length > t.Length)
                porcentaje = ((double)d[m, n] / (double)s.Length);
            else
                porcentaje = ((double)d[m, n] / (double)t.Length);
            return d[m, n];
        }



        
    }
}
