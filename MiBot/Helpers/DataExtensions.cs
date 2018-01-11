using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace MiBot.Helpers
{
    public static class DataExtensions
    {
        /// <summary>
        /// Extension que convierte el DataTable a una lista del objeto especificado
        /// </summary>
        /// <typeparam name="T">Objeto a ser convertido</typeparam>
        /// <param name="table">Tabla a convertir</param>
        /// <returns></returns>
        public static IList<T> ToList<T>(this DataTable table) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();

            return (from object row in table.Rows select CreateItemFromRow<T>((DataRow)row, properties)).ToList();
        }

        /// <summary>
        /// Extension que convierte el DataTable a una lista del objeto especificado
        /// </summary>
        /// <typeparam name="T">Objeto a ser convertido</typeparam>
        /// <param name="table">Tabla a convertir</param>
        /// <param name="mappings">Mapeado de columnas</param>
        /// <returns></returns>
        public static IList<T> ToList<T>(this DataTable table, Dictionary<string, string> mappings) where T : new()
        {
            IList<PropertyInfo> properties = typeof(T).GetProperties().ToList();

            return (from object row in table.Rows select CreateItemFromRow<T>((DataRow)row, properties, mappings)).ToList();
        }

        private static T CreateItemFromRow<T>(DataRow row, IEnumerable<PropertyInfo> properties) where T : new()
        {
            var item = new T();
            foreach (var property in properties)
            {
                try
                {
                    var propertyInfo = item.GetType().GetProperty(property.Name);
                    propertyInfo?.SetValue(item, Convert.ChangeType(row[property.Name], propertyInfo.PropertyType), null);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                //property.SetValue(item, row[property.Name], null);
            }
            return item;
        }

        private static T CreateItemFromRow<T>(DataRow row, IEnumerable<PropertyInfo> properties, IReadOnlyDictionary<string, string> mappings) where T : new()
        {
            var item = new T();
            foreach (var property in properties)
            {
                if (mappings.ContainsKey(property.Name))
                {
                    try
                    {
                        var propertyInfo = item.GetType().GetProperty(property.Name);
                        propertyInfo?.SetValue(item, Convert.ChangeType(row[mappings[property.Name]], propertyInfo.PropertyType), null);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    //property.SetValue(item, row[mappings[property.Name]], null);
                }
            }
            return item;
        }

        public static DataTable ToTitleCase(this DataTable table)
        {
            foreach (DataColumn column in table.Columns)
            {
                column.ColumnName = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(column.ColumnName.ToLower());
            }
            return table;
        }

        public static Dictionary<string, object> ToDictionary(this DataTable table)
        {
            var dict = new Dictionary<string, object>();
            for (var i = 0; i < table.Columns.Count; i++)
            {
                dict.Add(table.Columns[i].ColumnName, table.Rows.Cast<DataRow>().Select(k => k[table.Columns[i]]).ToArray());
            }
            return dict;
        }

        public static string GetGeneratedQuery(this SqlCommand dbCommand)
        {
            if (dbCommand.CommandType == CommandType.StoredProcedure)
            {
                var query = "EXEC " + dbCommand.CommandText;
                foreach (SqlParameter prm in dbCommand.Parameters)
                {
                    try
                    {
                        switch (prm.SqlDbType)
                        {
                            case SqlDbType.Bit:
                                int valor;
                                int.TryParse(prm.Value.ToString(), out valor);
                                var boolToInt = valor == 1 ? 1 : 0;
                                query += $" \r\n@{prm.ParameterName} = {boolToInt},";
                                break;
                            case SqlDbType.Int:
                                query += $" \r\n@{prm.ParameterName} = {prm.Value},";
                                break;
                            case SqlDbType.VarChar:
                                query += $" \r\n@{prm.ParameterName} = N'{prm.Value}',";
                                break;
                            default:
                                query += $" \r\n@{prm.ParameterName} = N'{prm.Value}',";
                                break;
                        }
                        var place = query.LastIndexOf(',');
                        return query.Remove(place, 1).Insert(place, ";");
                    }
                    catch (Exception e)
                    {
                        return e.Message;
                    }
                }
            }
            return dbCommand.Parameters.Cast<SqlParameter>().Aggregate(dbCommand.CommandText, (current, parameter) => current.Replace(parameter.ParameterName, parameter.Value.ToString()));
        }
    }
}