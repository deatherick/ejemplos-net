using System;

namespace PruebaConsola
{
    public class Cuadrado : Figura
    {
        public double Side { get; }

        public sealed override int Lados { get; set; } = 4;

        public override void Dibujar()
        {
            var side = Math.Round(Side);
            var result = "╔";
            var space = "";

            //PRIMER FILA
            for (var i = 0; i < side; i++)
            {
                space += "  ";
                result += "══";
            }

            result += "╗ ┐" + "\n";

            //CUERPO
            for (var i = 0; i < side; i++)
                result += "║" + space + (i + 1 == Math.Round(side / 2) ? "║ " + Side : "║ │") + "\n";

            //ULTIMA FILA
            result += "╚";
            for (var i = 0; i < side; i++)
            {
                result += "══";
            }

            result += "╝ ┘" + "\n";

            //ULTIMA FILA
            result += "└";
            for (var i = 0; i < side; i++)
            {
                result += i + 1 == Math.Round(side / 2) ? "" + Side : "──";
            }

            result += "─┘" + "\n";

            Console.Write(result);
            Console.ReadLine();
        }

        public override void CalcularArea()
        {
            Console.WriteLine("Calculando el area...");
            Area = Math.Pow(Side, 2);
            base.CalcularArea();
        }

        public override void CalcularPerimetro()
        {
            Console.WriteLine("Calculando el perimetro...");
            Perimetro = Lados * Side;
            base.CalcularPerimetro();
        }

        public Cuadrado(int side)
        {
            Side = side;
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
