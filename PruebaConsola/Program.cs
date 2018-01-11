using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Fare;

namespace PruebaConsola
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                //const string pathFormat = "Servicio de transporte hasta la fecha {0:dd/MM-yyyy}, según detalle adjunto";
                //var path = new Regex("{0:([/^fdDmMyYs .-]+)}").Replace(pathFormat, o => DateTime.Now.ToString(o.Groups[1].Value));
                //Console.WriteLine(path);
                //Console.ReadLine();
                //var cuadrado = new Cuadrado(15);
                //var rectangulo = new Rectangulo(8, 4);
                //var figuras = new List<Figura> { cuadrado, rectangulo };
                //foreach (var figura in figuras)
                //{
                //    figura.CalcularArea();
                //    figura.CalcularPerimetro();
                //    figura.Dibujar();
                //}
                //var xeger = new Xeger("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W]).{8,15})");
                //var generatedString = xeger.Generate();
                //var password = GenerateFromRegex.RegexToString("((?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W]).{8,15})");
                //var password = GenerateFromRegex.RegexToString("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,15}");
                var password = ValidarRegex();
                Console.WriteLine($"Password: {password}");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private static string ValidarRegex()
        {
            var reg = new Regex("^(?=.*\\d)(?=.*\\W)(?=.*[A-Z])(?=.*[a-z])\\S{8,15}$");
            var password = CreateRandomPassword(10);
            if (!Regex.IsMatch(password, reg.ToString()))
            {
                return ValidarRegex();
            }
            return password;
        }

        private static string CreateRandomPassword(int passwordLength)
        {
            const string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            var chars = new char[passwordLength];
            var rd = new Random();

            for (var i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}
