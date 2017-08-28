using System;

namespace PruebaConsola
{
    public abstract class Figura : IDisposable
    {
        public int Aristas { get; set; } = 1;

        public abstract int Lados { get; set; }

        public double Area { get; set; }

        public double Perimetro { get; set; }

        public double Volumen { get; set; }

        public abstract void Dibujar();

        public virtual void CalcularArea()
        {
            Console.WriteLine($"El area de la figura es: {Area}");
            Console.ReadLine();
        }

        public virtual void CalcularPerimetro()
        {
            Console.WriteLine($"El perimetro de la figura es: {Perimetro}");
            Console.ReadLine();
        }

        public abstract void Dispose();
    }
}
