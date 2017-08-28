using System;

namespace PruebaConsola
{
    public class Rectangulo : Figura
    {
        public double X { get; }

        public double Y { get; }

        public sealed override int Lados { get; set; } = 4;

        public override void Dibujar()
        {
            var width = Math.Round(X);
            var height = Math.Round(Y);
            var result = "╔";
            var space = "";

            //PRIMER FILA
            for (var i = 0; i < width; i++)
            {
                space += "  ";
                result += "══";
            }

            result += "╗ ┐" + "\n";

            //CUERPO
            for (var i = 0; i < height; i++)
                result += "║" + space + (i + 1 == Math.Round(height / 2) ? "║ " + Y : "║ │") + "\n";

            //ULTIMA FILA
            result += "╚";
            for (var i = 0; i < width; i++)
            {
                result += "══";
            }

            result += "╝ ┘" + "\n";

            //ULTIMA FILA
            result += "└";
            for (var i = 0; i < width; i++)
            {
                result += i + 1 == Math.Round(width / 2) ? "" + X : "──";
            }

            result += "─┘" + "\n";

            Console.Write(result);
            Console.ReadLine();
        }

        public override void CalcularArea()
        {
            Console.WriteLine("Calculando el area...");
            Area = X * Y;
            base.CalcularArea();
        }

        public override void CalcularPerimetro()
        {
            Console.WriteLine("Calculando el perimetro...");
            Perimetro = 2 * X + 2 * Y;
            base.CalcularPerimetro();
        }

        public Rectangulo(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
