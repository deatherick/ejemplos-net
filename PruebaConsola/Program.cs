using System.Collections.Generic;

namespace PruebaConsola
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var cuadrado = new Cuadrado(15);
            var rectangulo = new Rectangulo(8, 4);
            var figuras = new List<Figura> { cuadrado, rectangulo };
            foreach (var figura in figuras)
            {
                figura.CalcularArea();
                figura.CalcularPerimetro();
                figura.Dibujar();
            }

        }
    }
}
