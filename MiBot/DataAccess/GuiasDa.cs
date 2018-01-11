using System;
using System.Collections.Generic;
using MiBot.Entities;
using MiBot.Helpers;
using MySql.Data.MySqlClient;

namespace MiBot.DataAccess
{
    public class GuiasDa : IDisposable
    {
        public Response<Guia> ObtenerGuia(string pNumeroGuia)
        {
            var moduloAccion = new ModuloAccionMySql();
            try
            {
                var ds = moduloAccion.ObtenerDatosCore("sp_BuscarGuiaEL", new List<MySqlParameter>() { new MySqlParameter() {ParameterName = "pNumeroGuia", Value = pNumeroGuia } });
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var guia = ds.Tables[0].ToList<Guia>()[0];
                    return new Response<Guia>(ResponseCode.Success, true, "Guia obtenida exitosamente", guia);
                }
                return new Response<Guia>(ResponseCode.SinDatos, false, "No existen datos de guia.");
            }
            catch (Exception ex)
            {
                return new Response<Guia>(ResponseCode.ErrorDeSistema, false, ex.Message);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}