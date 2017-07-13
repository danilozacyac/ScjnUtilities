using System;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace ScjnUtilities
{
    public class DateTimeUtilities
    {

        /// <summary>
        /// Convierte una fecha al formato yyyyMMdd
        /// </summary>
        /// <param name="fecha">Fecha de la que se quiere su valor numérico</param>
        /// <returns></returns>
        public static string DateToInt(DateTime? fecha)
        {
            return fecha.Value.Year + fecha.Value.Month.ToString("00") + fecha.Value.Day.ToString("00");
        }

        /// <summary>
        /// Devuelve un a fecha a partir de un número con el formato yyyyMMdd
        /// </summary>
        /// <param name="fechaInt">Fecha en valor entero que se desea convertir</param>
        /// <returns></returns>
        public static DateTime? IntToDate(int fechaInt)
        {

            int year = Convert.ToInt32(fechaInt.ToString().Substring(0, 4));
            int month = Convert.ToInt32(fechaInt.ToString().Substring(4, 2));
            int day = Convert.ToInt32(fechaInt.ToString().Substring(6, 2));

            DateTime? fecha = new DateTime(year, month, day);

            return fecha;
        }


        public static DateTime? IntToDate(DbDataReader reader, string columnName)
        {
            if (reader[columnName] == DBNull.Value)
                return null;
            else if (Convert.ToInt32(reader[columnName]) == 0)
                return null;
            else
            {
                int fechaInt = Convert.ToInt32(reader[columnName]);

                int year = Convert.ToInt32(fechaInt.ToString().Substring(0, 4));
                int month = Convert.ToInt32(fechaInt.ToString().Substring(4, 2));
                int day = Convert.ToInt32(fechaInt.ToString().Substring(6, 2));

                DateTime? fecha = new DateTime(year, month, day);

                return fecha;
            }
        }

        /// <summary>
        /// Obtiene el valor de la fecha contenida en una colección de datos
        /// </summary>
        /// <param name="reader">Colección de datos de DB</param>
        /// <param name="columnName">Nombre de la columana de tipo Fecha</param>
        /// <returns></returns>
        public static DateTime? GetDateFromReader(DbDataReader reader, string columnName)
        {
            if (reader[columnName] == DBNull.Value)
            {
                return null;
            }
            else
            {
                return Convert.ToDateTime(reader[columnName]);
            }
        }

        /// <summary>
        /// Devuelve la fecha en formato largo ?? de ?????? de ????
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns></returns>
        public static string ToLongDateFormat(DateTime? fecha)
        {
            return String.Format("{0} de {1} de {2}", fecha.Value.Day, DateTimeUtilities.ToMonthName(fecha.Value.Month), fecha.Value.Year);
        }


        /// <summary>
        /// Devuelve la fecha en formato dd?MMM?yyyy donde ? es el spliter
        /// </summary>
        /// <param name="fecha"></param>
        /// <param name="spliter">Caracter designado para separar los valores</param>
        /// <returns></returns>
        public static string ToAbrevMonthFormat(DateTime? fecha, char spliter)
        {
            return String.Format("{0}{1}{2}{1}{3}", fecha.Value.Day, spliter, DateTimeUtilities.ToShortMonthName(fecha.Value.Month), fecha.Value.Year);
        }

        /// <summary>
        /// Devuelve la fecha en formato corto dd/MM/yyyy
        /// </summary>
        /// <param name="textDate">Fecha en forma de cadena de texto en formato ?? de ?????? de ???? </param>
        /// <returns></returns>
        public static DateTime? ToShortDateFormat(string textDate, string orTexto)
        {
            try
            {
                textDate = textDate.Replace("DE", "&");
                textDate = textDate.Replace('\r', ' ');
                string[] date = textDate.Split('&');

                string newDate = String.Format("{0}/{1}/{2}", date[0].Trim(), DateTimeUtilities.GetMonthNumberByName(date[1].Trim()), date[2].Trim());

                return Convert.ToDateTime(newDate);
            }
            catch (FormatException)
            {
                return null;
            }
            catch (IndexOutOfRangeException)
            {
                return null;
            }
        }

        /// <summary>
        /// Devuelve el nombre del mes señalado
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static string ToMonthName(int month)
        {
            string mes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);

            return mes;
        }

        /// <summary>
        /// Devuelve el nombre del mes señalado, abreviado
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static string ToShortMonthName(int month)
        {
            string mes = StringUtilities.UppercaseFirst(CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month));

            return mes;
        }

        
        private static string GetMonthNumberByName(string month)
        {
            switch (month.ToLower())
            {
                case "enero":
                    return "01";
                case "febrero":
                    return "02";
                case "marzo":
                    return "03";
                case "abril":
                    return "04";
                case "mayo":
                    return "05";
                case "junio":
                    return "06";
                case "julio":
                    return "07";
                case "agosto":
                    return "08";
                case "septiembre":
                    return "09";
                case "octubre":
                    return "10";
                case "noviembre":
                    return "11";
                case "diciembre":
                    return "12";
                default:
                    return "";
            }
        }

        public static bool StringContainsCompleteDate(string fecha)
        {
            Regex regex = new Regex(@"(?:[0-9]|[1-3][0-9]) (?:DE) \b(?:ENE(?:RO)?|FEB(?:RERO)?|MAR(?:ZO)?|ABR(?:IL)?|MAY(?:O)?|JUN(?:IO)?|JUL(?:IO)?|AGO(?:STO)?|SEP(?:TIEMBRE)?|OCT(?:UBRE)?|NOV(?:IEMBRE)?|DIC(?:IEMBRE)?) (?:DE) (?:19[7-9]\d|2\d{3})");
            return regex.IsMatch(fecha.ToUpper());
        }

        public static Match GetCompleteDate(string fecha)
        {
            Regex regex = new Regex(@"(?:[0-9]|[1-3][0-9]) (?:DE) \b(?:ENE(?:RO)?|FEB(?:RERO)?|MAR(?:ZO)?|ABR(?:IL)?|MAY(?:O)?|JUN(?:IO)?|JUL(?:IO)?|AGO(?:STO)?|SEP(?:TIEMBRE)?|OCT(?:UBRE)?|NOV(?:IEMBRE)?|DIC(?:IEMBRE)?) (?:DE) (?:19[7-9]\d|2\d{3})");
            return regex.Match(fecha.ToUpper());
        }


        /// <summary>
        /// De una cadena de texto dada obtiene la primera ocurrencia de una fecha en formato dd.MM.yyyy donde el "." indica cualquier caracter
        /// </summary>
        /// <param name="textEntry">Cadena de texto donde se presume hay una fecha</param>
        /// <returns>Devuelve la cadena de texto de la fecha si es que encuentra el patrón, de lo contrario regresa null</returns>
        public static string GetDateFromString(string textEntry)
        {
            Regex regex = new Regex(@"\d{2}.\d{2}.\d{4}");

            Match match = regex.Match(textEntry);

            if (match.Success)
                return match.Value;
            else
                return null;
        }


    }
}
