using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace MiBot.Helpers
{
    public class GlobalDatos
    {
        public static string ConexionTracker
        {
            get
            {
                var cn = "Server=192.100.1.231; Initial Catalog=Tracker; Integrated Security=false; User Id=Desarrollo; Password=Caex2016.; MultipleActiveResultSets=True";
                return cn;
            }
        }

        public static string ConexionCore
        {
            get
            {
                var cn = "Server=192.100.1.237; Port=3306; Database=cusser; Uid=Desarrollo; Pwd=dEsaCaex..; Allow User Variables=true;";
                return cn;
            }
        }
    }
    /// <summary>
    /// Funciones para ejecutar Stored Procedures dinamicamente para SQL Server
    /// </summary>
    public class ModuloAccionSql
    {
        /// <summary>
        /// Obtiene un DataSet del Stored Procedure indicado en la BD Tracker
        /// </summary>
        /// <param name="procedimientoAlmacenado">Nombre del Stored Procedure</param>
        /// <param name="parametros">Lista de parametros SQL a mandar en el Stored Procedure</param>
        /// <returns></returns>
        public DataSet ObtenerDatosTracker(string procedimientoAlmacenado, List<SqlParameter> parametros = null)
        {
            //var resultado = new DataSet();
            var ds = new DataSet();
            using (var cn = new SqlConnection(GlobalDatos.ConexionTracker))
            {
                cn.Open();
                var cmd = new SqlCommand
                {
                    Connection = cn,
                    CommandText = procedimientoAlmacenado,
                    CommandType = CommandType.StoredProcedure
                };
                using (cmd)
                {
                    if (parametros != null)
                        foreach (var parametro in parametros)
                            cmd.Parameters.Add(parametro);
#if DEBUG
                    //Console.WriteLine($"SQL query: {cmd.GetGeneratedQuery()}");
#endif
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
                cn.Close();
            }
            //var cantidadTables = (ds.Tables.Count - 1);
            //var queryResult = ds.Tables[cantidadTables].Copy();
            //resultado.Tables.Add(queryResult);
            return ds;
        }

        /// <summary>
        /// Ejecuta una accion de tipo Stored Procedure
        /// </summary>
        /// <param name="procedimientoAlmacenado">Nombre del Stored Procedure</param>
        /// <param name="parametros">Lista de parametros SQL a mandar en el Stored Procedure</param>
        /// <returns></returns>
        public bool EjecutarStoredProcedure(string procedimientoAlmacenado, List<SqlParameter> parametros = null)
        {
            try
            {
                using (var cn = new SqlConnection(GlobalDatos.ConexionTracker))
                {
                    cn.Open();
                    var cmd = new SqlCommand
                    {
                        Connection = cn,
                        CommandText = procedimientoAlmacenado,
                        CommandType = CommandType.StoredProcedure
                    };
                    using (cmd)
                    {
                        if (parametros != null)
                            foreach (var parametro in parametros)
                                cmd.Parameters.Add(parametro);
#if DEBUG
                        //Debug.WriteLine($"SQL query: {cmd.GetGeneratedQuery()}");
#endif
                        cmd.ExecuteNonQuery();
                    }
                    cn.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Ejecuta una accion de tipo Stored Procedure
        /// </summary>
        /// <param name="sqlCn"></param>
        /// <param name="sqlTran"></param>
        /// <param name="procedimientoAlmacenado">Nombre del Stored Procedure</param>
        /// <param name="parametros">Lista de parametros SQL a mandar en el Stored Procedure</param>
        /// <returns></returns>
        public bool EjecutarStoredProcedure(SqlConnection sqlCn, SqlTransaction sqlTran, string procedimientoAlmacenado,
            List<SqlParameter> parametros = null)
        {
            try
            {
                var cmd = new SqlCommand
                {
                    Connection = sqlCn,
                    CommandText = procedimientoAlmacenado,
                    CommandType = CommandType.StoredProcedure,
                    Transaction = sqlTran
                };
                using (cmd)
                {
                    if (parametros != null)
                        foreach (var parametro in parametros)
                            cmd.Parameters.Add(parametro);
#if DEBUG
                    //Debug.WriteLine($"SQL query: {cmd.GetGeneratedQuery()}");
#endif
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Ejecuta una accion de tipo Texto
        /// </summary>
        /// <param name="commandText">Texto a ejecutar</param>
        /// <param name="parametros">Lista de parametros SQL a mandar en el Stored Procedure</param>
        /// <returns></returns>
        public bool EjecutarAccion(string commandText, List<SqlParameter> parametros = null)
        {
            try
            {
                using (var cn = new SqlConnection(GlobalDatos.ConexionTracker))
                {
                    cn.Open();
                    var cmd = new SqlCommand
                    {
                        Connection = cn,
                        CommandText = commandText,
                        CommandType = CommandType.Text
                    };
                    using (cmd)
                    {
                        if (parametros != null)
                            foreach (var parametro in parametros)
                                cmd.Parameters.Add(parametro);
#if DEBUG
                        //Debug.WriteLine($"SQL query: {cmd.GetGeneratedQuery()}");
#endif
                        cmd.ExecuteNonQuery();
                    }
                    cn.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Ejecuta una accion de tipo Texto
        /// </summary>
        /// <param name="sqlCn"></param>
        /// <param name="sqlTran"></param>
        /// <param name="commandText">Texto a ejecutar</param>
        /// <param name="parametros">Lista de parametros SQL a mandar en el Stored Procedure</param>
        /// <returns></returns>
        public bool EjecutarAccion(SqlConnection sqlCn, SqlTransaction sqlTran, string commandText,
            List<SqlParameter> parametros = null)
        {
            try
            {
                var cmd = new SqlCommand
                {
                    Connection = sqlCn,
                    CommandText = commandText,
                    CommandType = CommandType.Text,
                    Transaction = sqlTran
                };
                using (cmd)
                {
                    if (parametros != null)
                        foreach (var parametro in parametros)
                            cmd.Parameters.Add(parametro);
#if DEBUG
                    //Debug.WriteLine($"SQL query: {cmd.GetGeneratedQuery()}");
#endif
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Devuelve el <see cref="SqlCommand"/> para una transacción
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="transaction"></param>
        /// <param name="procedimientoAlmacenado"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public SqlCommand RetornarComando(SqlConnection cn, SqlTransaction transaction, string procedimientoAlmacenado,
            List<SqlParameter> parametros = null)
        {
            try
            {
                var cmd = new SqlCommand
                {
                    Connection = cn,
                    CommandText = procedimientoAlmacenado,
                    CommandType = CommandType.StoredProcedure,
                    Transaction = transaction
                };
                using (cmd)
                {
                    if (parametros != null)
                        foreach (var parametro in parametros)
                            cmd.Parameters.Add(parametro);
#if DEBUG
                    //Debug.WriteLine($"SQL query: {cmd.GetGeneratedQuery()}");
#endif
                }
                return cmd;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new SqlCommand();
            }
        }

        /// <summary>
        /// Metedo para transacciones
        /// </summary>
        /// <param name="cn">Conexion</param>
        /// <param name="transaction"> Transaccion</param>
        /// <param name="procedimientoAlmacenado"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public DataSet ObtenerDatos(SqlConnection cn, SqlTransaction transaction, string procedimientoAlmacenado,
            List<SqlParameter> parametros = null)
        {
            //var resultado = new DataSet();
            var ds = new DataSet();
            //using (cn)
            //{
            // cn.Open();
            // var transaction = cn.BeginTransaction();
            var cmd = new SqlCommand
            {
                Connection = cn,
                CommandText = procedimientoAlmacenado,
                CommandType = CommandType.StoredProcedure,
                Transaction = transaction
            };
            using (cmd)
            {
                if (parametros != null)
                    foreach (var parametro in parametros)
                        cmd.Parameters.Add(parametro);
                //cmd.ExecuteNonQuery();
#if DEBUG
                //Debug.WriteLine($"SQL query: {cmd.GetGeneratedQuery()}");
#endif
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds);
                }
                //}
                // cn.Close();
                // transaction.Commit();
            }
            //var cantidadTables = (ds.Tables.Count - 1);
            //var queryResult = ds.Tables[cantidadTables].Copy();
            //resultado.Tables.Add(queryResult);
            return ds;
        }

    }

    /// <summary>
    /// Funciones para ejecutar Stored Procedures dinamicamente para SQL Server
    /// </summary>
    public class ModuloAccionMySql
    {
        /// <summary>
        /// Obtiene un DataSet del Stored Procedure indicado en la BD Tracker
        /// </summary>
        /// <param name="procedimientoAlmacenado">Nombre del Stored Procedure</param>
        /// <param name="parametros">Lista de parametros SQL a mandar en el Stored Procedure</param>
        /// <returns></returns>
        public DataSet ObtenerDatosCore(string procedimientoAlmacenado, List<MySqlParameter> parametros = null)
        {
            //var resultado = new DataSet();
            var ds = new DataSet();
            using (var cn = new MySqlConnection(GlobalDatos.ConexionCore))
            {
                cn.Open();
                var cmd = new MySqlCommand
                {
                    Connection = cn,
                    CommandText = procedimientoAlmacenado,
                    CommandType = CommandType.StoredProcedure
                };
                using (cmd)
                {
                    if (parametros != null)
                        foreach (var parametro in parametros)
                            cmd.Parameters.Add(parametro);

                    using (var da = new MySqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                }
                cn.Close();
            }
            //var cantidadTables = (ds.Tables.Count - 1);
            //var queryResult = ds.Tables[cantidadTables].Copy();
            //resultado.Tables.Add(queryResult);
            return ds;
        }

        /// <summary>
        /// Ejecuta una accion de tipo Stored Procedure
        /// </summary>
        /// <param name="procedimientoAlmacenado">Nombre del Stored Procedure</param>
        /// <param name="parametros">Lista de parametros SQL a mandar en el Stored Procedure</param>
        /// <returns></returns>
        public bool EjecutarStoredProcedure(string procedimientoAlmacenado, List<MySqlParameter> parametros = null)
        {
            try
            {
                using (var cn = new MySqlConnection(GlobalDatos.ConexionCore))
                {
                    cn.Open();
                    var cmd = new MySqlCommand
                    {
                        Connection = cn,
                        CommandText = procedimientoAlmacenado,
                        CommandType = CommandType.StoredProcedure
                    };
                    using (cmd)
                    {
                        if (parametros != null)
                            foreach (var parametro in parametros)
                                cmd.Parameters.Add(parametro);

                        cmd.ExecuteNonQuery();
                    }
                    cn.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Ejecuta una accion de tipo Stored Procedure
        /// </summary>
        /// <param name="procedimientoAlmacenado">Nombre del Stored Procedure</param>
        /// <param name="oFilasAfectadas">Devuelve el número de filas afectadas en caso de que hubieran</param>
        /// <param name="parametros">Lista de parametros SQL a mandar en el Stored Procedure</param>
        /// <returns></returns>
        public bool EjecutarStoredProcedure(string procedimientoAlmacenado, out int oFilasAfectadas, List<MySqlParameter> parametros = null)
        {
            oFilasAfectadas = 0;
            try
            {
                using (var cn = new MySqlConnection(GlobalDatos.ConexionCore))
                {
                    cn.Open();
                    var cmd = new MySqlCommand
                    {
                        Connection = cn,
                        CommandText = procedimientoAlmacenado,
                        CommandType = CommandType.StoredProcedure
                    };
                    using (cmd)
                    {
                        if (parametros != null)
                            foreach (var parametro in parametros)
                                cmd.Parameters.Add(parametro);

                        oFilasAfectadas = cmd.ExecuteNonQuery();
                    }
                    cn.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Ejecuta una accion de tipo Stored Procedure
        /// </summary>
        /// <param name="sqlCn"></param>
        /// <param name="sqlTran"></param>
        /// <param name="procedimientoAlmacenado">Nombre del Stored Procedure</param>
        /// <param name="parametros">Lista de parametros SQL a mandar en el Stored Procedure</param>
        /// <returns></returns>
        public bool EjecutarStoredProcedure(MySqlConnection sqlCn, MySqlTransaction sqlTran, string procedimientoAlmacenado,
            List<MySqlParameter> parametros = null)
        {
            try
            {
                var cmd = new MySqlCommand
                {
                    Connection = sqlCn,
                    CommandText = procedimientoAlmacenado,
                    CommandType = CommandType.StoredProcedure,
                    Transaction = sqlTran
                };
                using (cmd)
                {
                    if (parametros != null)
                        foreach (var parametro in parametros)
                            cmd.Parameters.Add(parametro);

                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Ejecuta una accion de tipo Texto
        /// </summary>
        /// <param name="commandText">Texto a ejecutar</param>
        /// <param name="parametros">Lista de parametros SQL a mandar en el Stored Procedure</param>
        /// <returns></returns>
        public bool EjecutarAccion(string commandText, List<MySqlParameter> parametros = null)
        {
            try
            {
                using (var cn = new MySqlConnection(GlobalDatos.ConexionCore))
                {
                    cn.Open();
                    var cmd = new MySqlCommand
                    {
                        Connection = cn,
                        CommandText = commandText,
                        CommandType = CommandType.Text
                    };
                    using (cmd)
                    {
                        if (parametros != null)
                            foreach (var parametro in parametros)
                                cmd.Parameters.Add(parametro);

                        cmd.ExecuteNonQuery();
                    }
                    cn.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Ejecuta una accion de tipo Texto
        /// </summary>
        /// <param name="sqlCn"></param>
        /// <param name="sqlTran"></param>
        /// <param name="commandText">Texto a ejecutar</param>
        /// <param name="parametros">Lista de parametros SQL a mandar en el Stored Procedure</param>
        /// <returns></returns>
        public bool EjecutarAccion(MySqlConnection sqlCn, MySqlTransaction sqlTran, string commandText,
            List<MySqlParameter> parametros = null)
        {
            try
            {
                var cmd = new MySqlCommand
                {
                    Connection = sqlCn,
                    CommandText = commandText,
                    CommandType = CommandType.Text,
                    Transaction = sqlTran
                };
                using (cmd)
                {
                    if (parametros != null)
                        foreach (var parametro in parametros)
                            cmd.Parameters.Add(parametro);

                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Devuelve el <see cref="MySqlCommand"/> para una transacción
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="transaction"></param>
        /// <param name="procedimientoAlmacenado"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public MySqlCommand RetornarComando(MySqlConnection cn, MySqlTransaction transaction, string procedimientoAlmacenado,
            List<MySqlParameter> parametros = null)
        {
            try
            {
                var cmd = new MySqlCommand
                {
                    Connection = cn,
                    CommandText = procedimientoAlmacenado,
                    CommandType = CommandType.StoredProcedure,
                    Transaction = transaction
                };
                using (cmd)
                {
                    if (parametros != null)
                        foreach (var parametro in parametros)
                            cmd.Parameters.Add(parametro);

                }
                return cmd;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new MySqlCommand();
            }
        }

        /// <summary>
        /// Metodo para transacciones
        /// </summary>
        /// <param name="cn">Conexion</param>
        /// <param name="transaction"> Transaccion</param>
        /// <param name="procedimientoAlmacenado"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        public DataSet ObtenerDatos(MySqlConnection cn, MySqlTransaction transaction, string procedimientoAlmacenado,
            List<MySqlParameter> parametros = null)
        {
            //var resultado = new DataSet();
            var ds = new DataSet();
            //using (cn)
            //{
            // cn.Open();
            // var transaction = cn.BeginTransaction();
            var cmd = new MySqlCommand
            {
                Connection = cn,
                CommandText = procedimientoAlmacenado,
                CommandType = CommandType.StoredProcedure,
                Transaction = transaction
            };
            using (cmd)
            {
                if (parametros != null)
                    foreach (var parametro in parametros)
                        cmd.Parameters.Add(parametro);

                using (var da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(ds);
                }
            }
            return ds;
        }
    }

}