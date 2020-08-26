using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _6
{
    class Program
    {
        public static int amount1 = 0;
        public static int amount2 = 0;

        static void Main(string[] args)
        {
            int sizeY = 15;   //Размер карты
            int sizeX = 15;
            Console.Clear();

            int quantity;
            tryagain:
            Console.Write("Введите количество целей: ");
            quantity = Convert.ToInt32(Console.ReadLine());
            if(quantity > sizeY * sizeX)
            {
                Console.WriteLine("Количество целей больше размера карты, повторите ввод.");
                goto tryagain;
            }
            Console.Clear();

            Random rnd = new Random();

            int[] xQ = new int[quantity];
            int[] yQ = new int[quantity];
            for (int i = 0; i < quantity; i++)
            {
                xQ[i] = rnd.Next(1, sizeY);                      //Координаты целец
            }
            for (int j = 0; j < quantity; j++)
            {
                yQ[j] = rnd.Next(1, sizeX);
            }

            Thread thread = new Thread(() => { Print(sizeY, sizeX, xQ, yQ); });  //Запуск разведчиков
            Thread thread2 = new Thread(() => { Print2(sizeY, sizeX, xQ, yQ); });
            thread.Start();
            thread2.Start();

            for (int q = 0; q < 1; q++)
            {
                PrintMap(sizeY, sizeX); //Отрисовка карты с целями
                for (int i = 0; i < xQ.Length; i++)
                {
                    Console.SetCursorPosition(xQ[i], yQ[i]);
                    Console.Write("O");
                }
            }
        }

        static void PrintMap(int sizeX, int sizeY)
        {
            for (int c = 1; c < sizeX; c++)
            {
                Console.SetCursorPosition(c, 0);
                Console.Write("_");
            }
            Console.WriteLine();
            for (int c = 1; c < sizeY / 2; c++)
            {
                Console.SetCursorPosition(0, c);
                Console.Write("|");
            }
            for (int c = sizeX / 2 - 1; c > 0; c--)
            {
                Console.SetCursorPosition(sizeX, c);
                Console.Write("|");
            }
            ///////////////////////////////////////////////
            for (int c = sizeX - 1; c > sizeX / 2 - 1; c--)
            {
                Console.SetCursorPosition(0, c);
                Console.Write("|");
            }
            for (int c = sizeY - 1; c > sizeY / 2 - 1; c--)
            {
                Console.SetCursorPosition(sizeX, c);
                Console.Write("|");
            }
            for (int c = sizeX - 1; c > 0; c--)
            {
                Console.SetCursorPosition(c, sizeX);
                Console.Write("-");
            }
        }

        static void Print(int sizeX, int sizeY, int[] xQ, int[] yQ)
        {
            for (int i = 1; i < sizeY / 2; i++)
            {
                for (int j = 1; j < sizeX; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write("X");

                    for (int q = 0; q < xQ.Length; q++)
                        if (j == xQ[q] && i == yQ[q])
                        {
                            amount1++;
                        }

                    Thread.Sleep(110);
                    Console.SetCursorPosition(j, i);
                    Console.Write(" ");
                }
                Console.SetCursorPosition(sizeX + 1, i);
                Console.WriteLine(amount1);
            }
        }

        static void Print2(int sizeX, int sizeY, int[] xQ, int[] yQ)
        {
            for (int x = sizeY - 1; x > sizeY / 2 - 1; x--)
            {
                for (int z = sizeX - 1; z > 0; z--)
                {
                    Console.SetCursorPosition(z, x);
                    Console.Write("X");

                    for (int i = 0; i < xQ.Length; i++)
                        if (z == xQ[i] && x == yQ[i])
                        {
                            amount2++;
                        }

                    Thread.Sleep(150);
                    Console.SetCursorPosition(z, x);
                    Console.WriteLine(" ");
                }
                Console.SetCursorPosition(sizeX + 1, x);
                Console.WriteLine(amount2);
            }
            Console.SetCursorPosition(sizeX, sizeY + 1);
            Console.Write("Для продолжения нажмите любую клавишу...");

            Console.ReadKey();
            Console.Clear();
            int amount = amount1 + amount2;
            Console.WriteLine("Обнаружено целей на карте: " + amount);
        }
    }
}