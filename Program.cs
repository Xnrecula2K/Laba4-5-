using System;
using System.Diagnostics;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\nВыберите пункт:");
                Console.WriteLine("1) Об авторе");
                Console.WriteLine("2) Отгадай ответ");
                Console.WriteLine("3) Выход");
                Console.WriteLine("4) Сортировка");
                Console.Write("Ваш выбор: ");

                bool isParsed = int.TryParse(Console.ReadLine(), out int ch);

                if (!isParsed)
                {
                    Console.WriteLine("Неверный ввод");
                }
                else
                {
                    switch (ch)
                    {
                        case 1:
                            ShowAuthorInfo();
                            break;

                        case 2:
                            PlayGuessGame(ref isRunning);
                            break;

                        case 3:
                            ExitProgram(ref isRunning);
                            break;

                        case 4:
                            Sorting();
                            break;

                        default:
                            Console.WriteLine("Неверный пункт меню.");
                            break;
                    }
                }
            }
        }

        static void ShowAuthorInfo()
        {
            Console.WriteLine("");
        }
        // всё вместе склеиваю
        static void PlayGuessGame(ref bool isRunning)
        {
            // ввод данных
            if (!GameInputs(out int a, out int b))
            {
                return;
            }

            // сама логика
            GameLogic(a, b, ref isRunning);
        }

        // проверка ограничений и проверка данных
        static bool GameInputs(out int a, out int b)
        {
            a = 0;
            b = 0;

            Console.Write("Введите значение a: ");
            bool isParsedA = int.TryParse(Console.ReadLine(), out a);

            Console.Write("Введите значение b: ");
            bool isParsedB = int.TryParse(Console.ReadLine(), out b);

            if (!isParsedA || !isParsedB)
            {
                Console.WriteLine("Неверный ввод чисел.");
                return false;
            }

            if (b <= 0)
            {
                Console.WriteLine("Ошибка значение b должно быть больше 0.");
                return false;
            }

            double cosVal = Math.Cos(2 * a);
            if (cosVal <= 0)
            {
                Console.WriteLine("Ошибка cos(2a) должен быть строго больше 0.");
                return false;
            }

            return true;
        }
        // логика игрушки
        static void GameLogic(int a, int b, ref bool isRunning)
        {
            double cosVal = Math.Cos(2 * a);
            double res1 = Math.Log(b, 5);
            double res2 = Math.Sqrt(cosVal);

            double res3 = res1 / res2;
            double result = Math.Pow(Math.Sin(res3), 2);
            double resultr = Math.Round(result, 2);

            bool isGuessed = false;

            for (int attempt = 1; attempt <= 3; attempt++)
            {
                Console.Write($"Попытка {attempt}/3. Введите ваш ответ: ");
                bool isParsedAns = double.TryParse(Console.ReadLine(), out double ans);

                if (!isParsedAns)
                {
                    Console.WriteLine("Неверный формат числа.");
                    continue;
                }

                if (Math.Abs(ans - resultr) < 0.001)
                {
                    Console.WriteLine("Ответ верный");
                    isGuessed = true;
                    isRunning = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Ответ неверный.");
                }
            }

            if (!isGuessed)
            {
                Console.WriteLine("Попытки закончились.");
            }

            Console.WriteLine($"Правильный ответ: {resultr}");
        }
        // выход из cli через bool
        static void ExitProgram(ref bool isRunning)
        {
            Console.Write("Завершить программу (y/n)? ");
            string answer = Console.ReadLine();
            if (answer == "y")
            {
                isRunning = false;
            }
        }
        // сортировка
        static void Sorting()
        {
            Console.Write("Введите размер массива от 1 до 10: ");
            bool isLength = int.TryParse(Console.ReadLine(), out int length);

            if (!isLength || length <= 0 || length >= 10)
            {
                Console.WriteLine("Неправильный ввод длины массива от 1 до 10.");
                return;
            }

            int[] array = new int[length];
            Random rand = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rand.Next(-50, 50);
            }

            int[] preArray = (int[])array.Clone();
            int[] backup = (int[])array.Clone();

            Stopwatch sw = new Stopwatch();

            sw.Start();
            GnomeSort(array);
            sw.Stop();
            double gnomeTime = sw.Elapsed.TotalMicroseconds;

            Array.Copy(backup, array, array.Length);

            sw.Restart();
            InsertionSort(array);
            sw.Stop();
            double insertionTime = sw.Elapsed.TotalMicroseconds;

            Console.WriteLine("\nИсходный массив:      " + string.Join(", ", preArray));
            Console.WriteLine("После сортировки:     " + string.Join(", ", array));

            Console.WriteLine($"1) Гномья сортировка:    {gnomeTime:F2} мкс");
            Console.WriteLine($"2) Сортировка вставками: {insertionTime:F2} мкс");

            if (gnomeTime < insertionTime)
            {
                Console.WriteLine("Гномья сортировка быстрее");
            }
            else if (insertionTime < gnomeTime)
            {
                Console.WriteLine("Сортировка вставками быстрее");
            }
            else
            {
                Console.WriteLine("Оба алгоритма показали одинаковое время.");
            }
        }
        // гномья
        static void GnomeSort(int[] array)
        {
            int index = 0;
            while (index < array.Length)
            {
                if (index == 0 || array[index] >= array[index - 1])
                {
                    index++;
                }
                else
                {
                    int temp = array[index];
                    array[index] = array[index - 1];
                    array[index - 1] = temp;
                    index--;
                }
            }
        }
        // вставками
        static void InsertionSort(int[] array)
        {
            for (int i = 1; i < array.Length; i++)
            {
                int key = array[i];
                int j = i - 1;

                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j--;
                }

                array[j + 1] = key;
            }
        }
    }
}
