using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PruebaConsola
{
    public class GenerateFromRegex
    {
        private static bool IsExemptedChar(string input)
        {

            var charlist = new char[] { '^', '$', '<', '>' };
            return input != null
                   && input.Length > 0
                   && charlist.Any(c => c == input[0]);
        }

        public static string RegexToString(string input)
        {
            var regs = new List<Regs>();
            char[] delimiters = { '[', '}', '^', '$' };
            string[] parts = input.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < parts.Length; i++)
            {
                bool manyparts = parts[i].Length > 1;

                string charNo = parts[i].Replace("]{", " ");

                if (manyparts && After(charNo, " ") != string.Empty)
                {
                    regs.Add(new Regs { RegKeyChar = BeforeChar(charNo, " "), RegKeyNo = After(charNo, " ") });
                }
                else
                {
                    if (!IsExemptedChar(charNo))
                    {
                        regs.Add(new Regs { RegKeyChar = charNo, RegKeyNo = "0" });
                    }
                }

            }

            var sbBuilder = new StringBuilder("");
            Random ran;
            foreach (var reg in regs)
            {
                ran = new Random();
                if (reg.RegKeyNo == "0")
                {
                    sbBuilder.Append(reg.RegKeyChar);
                }
                else
                {

                    int charQty;
                    if (reg.RegKeyNo.Contains(','))
                    {
                        var minvalue = int.Parse(BeforeChar(reg.RegKeyNo, ","));
                        var maxvalue = int.Parse(After(reg.RegKeyNo, ","));
                        charQty = ran.Next(minvalue, maxvalue);
                    }
                    else
                    {
                        charQty = int.Parse(reg.RegKeyNo);
                    }

                    switch (reg.RegKeyChar)
                    {
                        case "A-Z":
                            sbBuilder.Append(Regs.GetLetters(charQty));
                            break;
                        case "a-z":
                            sbBuilder.Append(Regs.GetLetters(charQty, true));
                            break;
                        case "A-Za-z":
                            sbBuilder.Append(Regs.GetLetters(charQty, false, true));
                            break;
                        case "0-9":
                            sbBuilder.Append(Regs.GetNumbers(charQty));
                            break;
                        case "A-Z0-9":
                            sbBuilder.Append(Regs.GetAlphanumeric(charQty, true));
                            break;
                        case "A-Za-z0-9":
                            sbBuilder.Append(Regs.GetAlphanumeric(charQty, true, true));
                            break;
                    }
                }
                Thread.Sleep(50);
            }

            return sbBuilder.ToString();

        }
        private static string BetweenChars(string value, string indxa, string indxb)
        {
            int positionA = value.IndexOf(indxa);
            int positionB = value.LastIndexOf(indxb);
            if (positionA == -1)
            {
                return "";
            }
            if (positionB == -1)
            {
                return "";
            }
            int adjustedPositionA = positionA + indxa.Length;
            if (adjustedPositionA >= positionB)
            {
                return "";
            }
            return value.Substring(adjustedPositionA, positionB - adjustedPositionA);
        }

        /// <summary>
        /// Get string value after [first] a.
        /// </summary>
        private static string BeforeChar(string value, string chara)
        {
            int positionA = value.IndexOf(chara);
            if (positionA == -1)
            {
                return "";
            }
            return value.Substring(0, positionA);
        }

        /// <summary>
        /// Get string value after [last] a.
        /// </summary>
        private static string After(string value, string chara)
        {
            int positionA = value.LastIndexOf(chara);
            if (positionA == -1)
            {
                return "";
            }
            int adjustedPosA = positionA + chara.Length;
            if (adjustedPosA >= value.Length)
            {
                return "";
            }
            return value.Substring(adjustedPosA);
        }
    }

    internal class Regs
    {
        public string RegKeyChar { get; set; }
        public string RegKeyNo { get; set; }
        private const string Nums = "1234567890";
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string GetNumbers(int num)
        {
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(Nums, num)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());
            return result;
        }

        public static string GetLetters(int num, bool toLower = false, bool withLower = false)
        {
            string chars = withLower ? Chars + Chars.ToLower() : Chars;
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, num)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());
            if (toLower)
            {
                return result.ToLower();
            }
            return result;
        }

        public static string GetAlphanumeric(int num, bool uppercase = false, bool withLowercase = false)
        {
            string alphanum = withLowercase ? Nums + Chars + Chars.ToLower() : Nums + Chars;

            var random = new Random();
            var result = new string(
                Enumerable.Repeat(alphanum, num)
                    .Select(s => s[random.Next(s.Length)])
                    .ToArray());

            return uppercase ? result.ToUpper() : result;
        }
    }
}
