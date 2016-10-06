using System;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;

namespace ScjnUtilities
{
    public class DataBaseUtilities
    {

        /// <summary>
        /// Devuelve el número del identificador a ser utilizado incrementandolo en 1
        /// </summary>
        /// <param name="nombreTabla">Tabla de la cual se busca el ID</param>
        /// <param name="nombreCampo">Nombre del Identificador</param>
        /// <param name="connection">Conexion a la base de datos de Access</param>
        /// <returns></returns>
        public static int GetNextIdForUse(string nombreTabla, string nombreCampo, OleDbConnection connection)
        {
            OleDbCommand cmd;
            OleDbDataReader reader = null;

            int idForUse = 0;

            try
            {
                connection.Open();

                string sqlCadena = String.Format("SELECT MAX({0}) + 1 AS Id FROM {1}", nombreCampo, nombreTabla);

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    idForUse = Convert.ToInt32(reader["Id"]);
                }
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,DataBaseUtilities", "ScjnUtilities");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,DataBaseUtilities", "ScjnUtilities");
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return idForUse;
        }

        public static int GetNextIdForUse(string nombreTabla, string nombreCampo, SqlConnection connection)
        {
            SqlCommand cmd;
            SqlDataReader reader = null;

            int idForUse = 0;

            try
            {
                connection.Open();

                string sqlCadena = String.Format("SELECT MAX({0}) + 1 AS Id FROM {1}", nombreCampo, nombreTabla);

                cmd = new SqlCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    idForUse = Convert.ToInt32(reader["Id"]);
                }
            }
            catch (SqlException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,DataBaseUtilities", "ScjnUtilities");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,DataBaseUtilities", "ScjnUtilities");
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return idForUse;
        }

        /// <summary>
        /// Obtiene el último ID utilizado y lo incrementa en base al parámetro ingresado
        /// </summary>
        /// <param name="incremento">Cantidad en la que se habra de incrementar el ID</param>
        /// <param name="nombreTabla"></param>
        /// <param name="nombreCampo"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static int GetNextIdForUse(int incremento, string nombreTabla, string nombreCampo, OleDbConnection connection)
        {
            OleDbCommand cmd;
            OleDbDataReader reader = null;

            int idForUse = 0;

            try
            {
                connection.Open();

                string sqlCadena = String.Format("SELECT MAX({0}) + {1} AS Id FROM {2}", nombreCampo, incremento, nombreTabla);

                cmd = new OleDbCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    idForUse = Convert.ToInt32(reader["Id"]);
                }
            }
            catch (OleDbException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,DataBaseUtilities", "ScjnUtilities");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,DataBaseUtilities", "ScjnUtilities");
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return idForUse;
        }

        /// <summary>
        /// Obtiene el último ID utilizado y lo incrementa en base al parámetro ingresado
        /// </summary>
        /// <param name="incremento">Cantidad en la que se habra de incrementar el ID</param>
        /// <param name="nombreTabla"></param>
        /// <param name="nombreCampo"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static int GetNextIdForUse(int incremento, string nombreTabla, string nombreCampo, SqlConnection connection)
        {
            SqlCommand cmd;
            SqlDataReader reader = null;

            int idForUse = 0;

            try
            {
                connection.Open();

                string sqlCadena = String.Format("SELECT MAX({0}) + {1} AS Id FROM {2}", nombreCampo, incremento, nombreTabla);

                cmd = new SqlCommand(sqlCadena, connection);
                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    idForUse = Convert.ToInt32(reader["Id"]);
                }
            }
            catch (SqlException ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,DataBaseUtilities", "ScjnUtilities");
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                ErrorUtilities.SetNewErrorMessage(ex, methodName + " Exception,DataBaseUtilities", "ScjnUtilities");
            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return idForUse;
        }

        /// <summary>
        /// Verifica que el reader en la columna seleccionada no tenga null como valor
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public static string VerifyDbNullForStrings(SqlDataReader reader, string property)
        {
            if (reader[property] == DBNull.Value)
                return string.Empty;
            else
                return reader[property].ToString();
        }

        /// <summary>
        /// Convierte un objeto en el tipo solicitado
        /// </summary>
        /// <typeparam name="T">Tipo de dato al que se convertirá el objeto</typeparam>
        /// <param name="value">Valor que se pretende convertir</param>
        /// <param name="defaultValue">Valor por defecto</param>
        /// <returns></returns>
        public static T To<T>(object value, T defaultValue)
        {
            if (value == DBNull.Value) return defaultValue;
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
