using System;

namespace MiBot.Entities
{
    [Serializable]
    public class Guia
    {
        public string NumeroGuia { get; set; }
        public string NumeroGuiaMadre { get; set; }
        public DateTime Fecha { get; set; }
        public string RemitenteNombre { get; set; }
        public string RemitenteDireccion { get; set; }
        public string RemitenteTelefono { get; set; }
        public string CodigoCredito { get; set; }
        public string DestinatarioNombre { get; set; }
        public string DestinatarioDireccion { get; set; }
        public string DestinatarioTelefono { get; set; }
        public double Peso { get; set; }
        public string TipoServicio { get; set; }
        public string TipoPieza { get; set; }
        public DateTime PodFecha { get; set; }
        public string PodNombre { get; set; }
    }
}