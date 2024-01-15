using AnnarComMICROSESV60.Utilities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnnarComMICROSESV60.Dao
{
    public class ConnectionDB
    {
        RegistroLog log = new RegistroLog();
        NpgsqlConnection connection;
        NpgsqlCommand command;
        NpgsqlDataAdapter da;
        string ERROR = "ERROR";


        public class ResultadoQuery
        {
            public DataTable Tabla { get; set; }
            public string Resultado { get; set; }
            public string ResultadoMensaje { get; set; }
        }

        public class ResultadoStatement
        {
            public string Resultado { get; set; }
            public string ResultadoMensaje { get; set; }

            public int filasAfectadas { get; set; }
        }

        public ResultadoQuery RunQuery(string sqlStr, CommandType type)
        {
            ResultadoQuery resultadoQuery = new ResultadoQuery();
            resultadoQuery.Tabla = new DataTable();
            resultadoQuery.Resultado = "";
            resultadoQuery.ResultadoMensaje = "";
            DataTable dt = new DataTable();

            bool respuestaPersistencia = false;
            int intentosConexion = 0;

            while (respuestaPersistencia == false)
            {
                try
                {
                    connection = new NpgsqlConnection(InterfaceConfig.StrCadenaConeccion);
                    command = new NpgsqlCommand();
                    intentosConexion += 1;

                    if (connection.State == ConnectionState.Open) { connection.Close(); connection.Open(); } else { connection.Open(); }

                    command = connection.CreateCommand();
                    command.CommandType = type;
                    command.CommandText = sqlStr;

                    da = new NpgsqlDataAdapter(command);

                    da.Fill(dt);
                    connection.Close();

                    respuestaPersistencia = true;
                    if (InterfaceConfig.imprimirQueriesDBLog.Equals("S")) log.RegistraEnLog("Sentencia ejecutada --> [" + sqlStr + "]", InterfaceConfig.nombreLog);
                    resultadoQuery.Tabla = dt;
                }
                catch (Exception ex)
                {
                    //REGISTRAR LOG
                    log.RegistraEnLog("Error consultando la base de datos --> Mensaje[" + ex.Message + "]", InterfaceConfig.nombreLog);
                    log.RegistraEnLog("Sentencia ejecutada --> [" + sqlStr + "]", InterfaceConfig.nombreLog);
                    respuestaPersistencia = false;
                    resultadoQuery.Resultado = "ERROR";
                    resultadoQuery.ResultadoMensaje = ex.Message;
                }
                finally
                {
                    connection.Dispose();
                    connection.Close();
                }

                if (respuestaPersistencia == false && intentosConexion == InterfaceConfig.intentosReconexionDB)
                {
                    //SALE DEL CICLO PARA DEVOLVER EL ERROR
                    respuestaPersistencia = true;
                    log.RegistraEnLog("Error consultando la base de datos --> Mensaje[" + resultadoQuery.ResultadoMensaje + "]", InterfaceConfig.nombreLog);
                }
            }

            return resultadoQuery;
        }

        public ResultadoStatement RunStatement(string sqlStr, List<NpgsqlParameter> ParametersList, CommandType type)
        {
            ResultadoStatement resultadoStatement = new ResultadoStatement();
            resultadoStatement.Resultado = "";
            resultadoStatement.ResultadoMensaje = "";

            bool respuestaPersistencia = false;
            int intentosConexion = 0;
            string mensajeError = string.Empty;
            bool logRegistrado = false;

            while (respuestaPersistencia == false)
            {
                NpgsqlConnection connection = new NpgsqlConnection(InterfaceConfig.StrCadenaConeccion);
                NpgsqlCommand command = new NpgsqlCommand();
                intentosConexion += 1;

                try
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                        connection.Open();
                    }
                    else
                    {
                        connection.Open();
                    }

                    command = connection.CreateCommand();
                    command.CommandType = type;
                    command.CommandText = sqlStr;

                    if (ParametersList != null)
                    {
                        foreach (var parameter in ParametersList)
                        {
                            command.Parameters.Add(new NpgsqlParameter(parameter.ParameterName, parameter.Value));
                        }

                    }

                    command.ExecuteNonQuery();
                    connection.Close();
                    respuestaPersistencia = true;
                }
                catch (Exception ex)
                {
                    if (!logRegistrado)
                    {
                        //REGISTRAR LOG
                        mensajeError = $"Error ejecutando el siguiente script en el LIS --> script[{sqlStr}], mensaje[{ex.Message}]";
                        log.RegistraEnLog($"Error ejecutando el siguiente script en el LIS --> script[{sqlStr}], mensaje[{ex.Message}]", InterfaceConfig.nombreLog);
                        respuestaPersistencia = false;
                        resultadoStatement.Resultado = ERROR;
                        resultadoStatement.ResultadoMensaje = mensajeError;
                        logRegistrado = true;
                    }
                }
                finally
                {
                    connection.Dispose();
                    connection.Close();
                }
            }
            return resultadoStatement;
        }


    }
}
