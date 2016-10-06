using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScjnUtilities
{
    public class FilesUtilities
    {
        /// <summary>
        /// Copia un archivo a la carpeta destinada dentro de la ubicación en que se 
        /// encuentra la aplicacion
        /// </summary>
        public static string CopyToLocalResource(string docPath)
        {
            try
            {
                string newPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

                newPath += @"\Docs\" + Path.GetFileName(docPath);

                File.Copy(docPath, newPath);

                return newPath;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Copiar un archivo de la ubicación actual a la ubicación deseada
        /// </summary>
        /// <param name="currentPath">Ubicación actual</param>
        /// <param name="newPath">Nueva ubicación</param>
        /// <returns></returns>
        public static bool CopyToLocalResource(string currentPath, string newPath)
        {
            try
            {
                File.Copy(currentPath, newPath, true);

                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("No se pudo copiar el archivo a la ubicación deseada");
                return false;
            }
        }


        /// <summary>
        /// Obtiene del archivo seleccionado, en este caso el filtro mostrará unicamente
        /// archivos de Word 
        /// </summary>
        /// <param name="winTitle">Título de la ventana que se mostrará</param>
        /// <param name="restoreDirectory">Establece si se muestra o no la el último directorio utilizado</param>
        /// <returns></returns>
        public static String GetWordFilePath(string winTitle, bool restoreDirectory)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Office Documents|*.doc;*.docx| RichTextFiles |*.rtf";
            dialog.Title = winTitle;

            if (!restoreDirectory)
                dialog.InitialDirectory = @"C:\Users\" + Environment.UserName + @"\Documents";
            else
                dialog.RestoreDirectory = true;

            dialog.ShowDialog();

            return dialog.FileName;
        }

        public static bool IsFileLocked(string fileName)
        {
            FileStream stream = null;

            FileInfo file = new FileInfo(fileName);

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //El archivo esta bloqueado
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return false;
        }

    }
}
