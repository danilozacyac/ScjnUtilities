using System;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Windows.Forms;

namespace ScjnUtilities
{
    public class ErrorUtilities
    {

        /// <summary>
        /// Añade la información del error generado a un archivo de texto
        /// </summary>
        /// <param name="ex">Excepcion lanzada</param>
        /// <param name="methodName">Nombre del método donde se genera el error</param>
        /// <param name="numIus">Número de registro IUS (En caso de existir)</param>
        public static void SetNewErrorMessage(Exception ex, String methodName, long numIus)
        {
            String errorFilePath = ConfigurationManager.AppSettings.Get("RutaTxtErrorFile");

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(errorFilePath, true))
            {
                file.WriteLine(" ");
                file.WriteLine(" ");
                file.WriteLine(" ");
                file.WriteLine(String.Format("*********************{0}***************************", DateTime.Now));
                file.WriteLine("Equipo:  " + Environment.MachineName);
                file.WriteLine("Usuario:   " + Environment.UserName);
                file.WriteLine("Método:    " + methodName);
                file.WriteLine("Núm. IUS:  " + numIus);
                file.WriteLine(ex.Message);
                file.WriteLine(ex.StackTrace);
                file.WriteLine("***************************************************************************************");
            }
        }

        /// <summary>
        /// Añade la información del error generado a un archivo de texto, además genera una entrada en el
        /// registro de eventos del sistema operativo
        /// </summary>
        /// <param name="ex">Excepción generada</param>
        /// <param name="methodInfo">Nombre del método donde se produce el error</param>
        /// <param name="appName">Nombre de la aplicación que genera el error</param>
        public static void SetNewErrorMessage(Exception ex, String methodInfo, String appName)
        {
            MessageBox.Show(String.Format("Error ({0}) : {1}{0}{1}", ex.Source, ex.Message), methodInfo, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            String errorFilePath = ConfigurationManager.AppSettings.Get("RutaTxtErrorFile");

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(errorFilePath, true))
            {
                file.WriteLine(" ");
                file.WriteLine(" ");
                file.WriteLine(" ");
                file.WriteLine(String.Format("*********************{0}***************************", DateTime.Now));
                file.WriteLine("Equipo:  " + Environment.MachineName);
                file.WriteLine("Usuario:   " + Environment.UserName);
                file.WriteLine("Método:    " + methodInfo);
                file.WriteLine(ex.Message);
                file.WriteLine(ex.StackTrace);
                file.WriteLine("***************************************************************************************");
            }

            WriteEventLog(ex, methodInfo, appName);
        }

        /// <summary>
        /// Muestra los errores de validación de los atributos de los objetos
        /// </summary>
        /// <param name="sTitle">Título del error</param>
        /// <param name="sMsj">Errores de validación</param>
        public static void MostrarMensajes(string sTitle, string sMsj)
        {
            MessageBox.Show(sMsj, sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /// <summary>
        /// Genera una entrada en el visor de documentos del SO con los datos de la excepcion
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="methodExc"></param>
        private static void WriteEventLog(Exception ex, String methodExc, String appName)
        {
            try
            {
                if (!EventLog.SourceExists(appName))
                {
                    EventLog.CreateEventSource(appName, appName);
                }
                EventLog logg = new EventLog(appName) { Source = appName };
                String mensaje = String.Format("{0}\n{1}{2}", methodExc, ex.Message, ex.StackTrace);
                logg.WriteEntry(mensaje);
                logg.Close();
            }
            catch (SecurityException) { }

        }

    }
}
