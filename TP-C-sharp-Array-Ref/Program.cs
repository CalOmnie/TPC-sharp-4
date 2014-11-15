using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace TP_C_sharp_Array_Ref
{
    class Program
    {
        static int width = 3;
        static void swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        static int[][] string_of_int_array(string a, int width)
        {
            int[][] sol = new int[a.Length / width][];
            for (int i = 0; i < a.Length / width; i++)
            {
                sol[i] = new int[width];
                for (int j = 0; j < width; ++j)
                {
                    if (a[(i * width) + j] - 48 == 0)
                        sol[i][j] = 0;
                    else
                        sol[i][j] = 1;
                }
            }
            return sol;
        }

        static bool print_road_array(int[] arr, bool p_car, int car)
        {
            bool sol = true;
            Console.Write('|');
            for (int i = 0; i < arr.Length; i++)
            {
                if (p_car && i == car && arr[i] == 1)
                    sol = false;
                if (p_car && i == car)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("ooo");
                    Console.ResetColor();
                    continue;
                }
                if (arr[i] == 1)
                    Console.Write("xxx");
                else
                    Console.Write("   ");
            }
            Console.Write('|');
            Console.WriteLine("");
            return sol;
        }

        static bool draw_road(int[][] race, ref int where, int car)
        {
            Console.Clear();
            bool sol = true;
            int road_pos = where / 3;
            int shown = where % 3;
            if (race.GetLength(0) - (road_pos) < 3)
                return false;
            int[] order = new int[] { 1 }; ;
            if (shown == 0)
                order = new int[] { 2, 1, 1, 1, 0, 0, 0 }; // they can code a shift array for that...
            else if (shown == 1)
                order = new int[] { 2, 2, 1, 1, 1, 0, 0 };
            else if (shown == 2)
                order = new int[] { 2, 2, 2, 1, 1, 1, 0 };
            for (int i = 0; i < 7; i++)
            {
                if (!print_road_array(race[order[i] + road_pos], i > 3, car))
                    sol = false;
            }
            where++;
            return sol;
        }

        static void Main(string[] args)
        {
            int[][] race = string_of_int_array("000000000000110000000011000000000", width);

            int where = 0;
            int car = 1;
            while (draw_road(race, ref where, car))
            {
                Console.WriteLine(where);
                
                Thread.Sleep(500);
                if (Console.KeyAvailable)
                {
                    ConsoleKey temp = Console.ReadKey().Key;
                    if (temp == ConsoleKey.LeftArrow && car > 0)
                        car--;
                    if (temp == ConsoleKey.RightArrow && car < width)
                        car++;
                }
            }
            if ((where / 3) == (race.GetLength(0) - 2))
                Console.WriteLine("YOU WIN MADAFACKA");
            else
                Console.WriteLine("YOU LOOSE NOOB");
            Console.Read();
        }
    }
}
