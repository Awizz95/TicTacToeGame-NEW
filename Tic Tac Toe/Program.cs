﻿class Program
{
    static int SIZE_WIN = 4; //кол-во заполненных подряд полей для победы
    static int SIZE_X = 5;
    static int SIZE_Y = 5;

    static char[,] field = new char[SIZE_Y, SIZE_X];

    static char PLAYER_DOT = 'X';
    static char AI_DOT = 'O';
    static char EMPTY_DOT = '.';

    static Random random = new Random();

    private static void InitField()
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                field[i, j] = EMPTY_DOT;
            }
        }
    }

    private static void PrintField()
    {
        Console.Clear();
        Console.WriteLine("-----------");
        for (int i = 0; i < SIZE_Y; i++)
        {
            Console.Write("|");
            for (int j = 0; j < SIZE_X; j++)
            {
                Console.Write(field[i, j] + "|");
            }
            Console.WriteLine();
        }
        Console.WriteLine("-----------");
    }

    private static void SetSym(int y, int x, char sym)
    {
        field[y, x] = sym;
    }

    private static bool IsCellValid(int y, int x)
    {
        if (x < 0 || y < 0 || x > SIZE_X - 1 || y > SIZE_Y - 1)
        {
            return false;
        }

        return field[y, x] == EMPTY_DOT;
    }

    private static bool IsFieldFull()
    {
        for (int i = 0; i < SIZE_Y; i++)
        {
            for (int j = 0; j < SIZE_X; j++)
            {
                if (field[i, j] == EMPTY_DOT)
                {
                    return false;
                }
            }
        }
        return true;
    }
    private static void playerMove()
    {
        int x, y;
        do
        {
            Console.WriteLine("Введите координаты вашего хода в диапозоне от 1 до " + SIZE_Y);
            Console.WriteLine("Координат по строке ");
            x = Int32.Parse(Console.ReadLine()) - 1;
            Console.WriteLine("Координат по столбцу ");
            y = Int32.Parse(Console.ReadLine()) - 1;
        } while (!IsCellValid(y, x));
        SetSym(y, x, PLAYER_DOT);
    }
    private static void AiMove()
    {
        int x, y;

        //игра на победу
        for (int v = 0; v < SIZE_Y; v++)
        {
            for (int h = 0; h < SIZE_X; h++)
            {
                //анализ наличия достаточного количества ячеек
                if (h + SIZE_WIN <= SIZE_X)
                {                           //по горизонтали
                    if (CheckLineHorisont(v, h, AI_DOT) == SIZE_WIN - 1)
                    {
                        if (MoveAiLineHorisont(v, h, AI_DOT)) return;
                    }

                    if (v - SIZE_WIN > -2)
                    {                            //вниз по диагонали
                        if (CheckDiaDown(v, h, AI_DOT) == SIZE_WIN - 1)
                        {
                            if (MoveAiDiaDown(v, h, AI_DOT)) return;
                        }
                    }
                    if (v + SIZE_WIN <= SIZE_Y)
                    {                       //вверх по диагонали
                        if (CheckDiaUp(v, h, AI_DOT) == SIZE_WIN - 1)
                        {
                            if (MoveAiDiaUp(v, h, AI_DOT)) return;
                        }
                    }

                }
                if (v + SIZE_WIN <= SIZE_Y)
                {                       //по вертикали
                    if (CheckLineVertical(v, h, AI_DOT) == SIZE_WIN - 1)
                    {
                        if (MoveAiLineVertical(v, h, AI_DOT)) return;
                    }
                }
            }
        }

        //блокировка ходов человека
        for (int v = 0; v < SIZE_Y; v++)
        {
            for (int h = 0; h < SIZE_X; h++)
            {
                if (h + SIZE_WIN <= SIZE_X)
                {                           
                    if (CheckLineHorisont(v, h, PLAYER_DOT) == SIZE_WIN - 1)
                    {
                        if (MoveAiLineHorisont(v, h, AI_DOT)) return;
                    }

                    if (v - SIZE_WIN > -2)
                    {                            
                        if (CheckDiaDown(v, h, PLAYER_DOT) == SIZE_WIN - 1)
                        {
                            if (MoveAiDiaDown(v, h, AI_DOT)) return;
                        }
                    }
                    if (v + SIZE_WIN <= SIZE_Y)
                    {                       
                        if (CheckDiaUp(v, h, PLAYER_DOT) == SIZE_WIN - 1)
                        {
                            if (MoveAiDiaUp(v, h, AI_DOT)) return;
                        }
                    }
                }
                if (v + SIZE_WIN <= SIZE_Y)
                {                       
                    if (CheckLineVertical(v, h, PLAYER_DOT) == SIZE_WIN - 1)
                    {
                        if (MoveAiLineVertical(v, h, AI_DOT)) return;
                    }
                }
            }
        }

        //случайный ход
        do
        {
            x = random.Next(0, SIZE_X);
            y = random.Next(0, SIZE_Y);
        } while (!IsCellValid(y, x));
        SetSym(y, x, AI_DOT);
    }

    private static bool MoveAiLineHorisont(int v, int h, char dot)
    {
        for (int j = h; j < SIZE_WIN; j++)
        {
            if ((field[v, j] == EMPTY_DOT))
            {
                field[v, j] = dot;
                return true;
            }
        }
        return false;
    }
    private static bool MoveAiLineVertical(int v, int h, char dot)
    {
        for (int i = v; i < SIZE_WIN; i++)
        {
            if ((field[i, h] == EMPTY_DOT))
            {
                field[i, h] = dot;
                return true;
            }
        }
        return false;
    }
    //проверка заполнения всей линии по диагонали вниз
    private static bool MoveAiDiaDown(int v, int h, char dot)
    {
        for (int i = 0, j = 0; j < SIZE_WIN; i--, j++)
        {
            if ((field[v + i, h + j] == EMPTY_DOT))
            {
                field[v + i, h + j] = dot;
                return true;
            }
        }
        return false;
    }
    //проверка заполнения всей линии по диагонали вверх
    private static bool MoveAiDiaUp(int v, int h, char dot)
    {

        for (int i = 0; i < SIZE_WIN; i++)
        {
            if ((field[i + v, i + h] == EMPTY_DOT))
            {
                field[i + v, i + h] = dot;
                return true;
            }
        }
        return false;
    }

    private static bool CheckWin(char dot)
    {
        for (int v = 0; v < SIZE_Y; v++)
        {
            for (int h = 0; h < SIZE_X; h++)
            {
                if (h + SIZE_WIN <= SIZE_X)
                {                           
                    if (CheckLineHorisont(v, h, dot) >= SIZE_WIN) return true;

                    if (v - SIZE_WIN > -2)
                    {                            
                        if (CheckDiaDown(v, h, dot) >= SIZE_WIN) return true;
                    }
                    if (v + SIZE_WIN <= SIZE_Y)
                    {                       
                        if (CheckDiaUp(v, h, dot) >= SIZE_WIN) return true;
                    }
                }
                if (v + SIZE_WIN <= SIZE_Y)
                {                      
                    if (CheckLineVertical(v, h, dot) >= SIZE_WIN) return true;
                }
            }
        }
        return false;
    }

    //проверка заполнения всей линии по диагонали вниз
    private static int CheckDiaDown(int v, int h, char dot)
    {
        int count = 0;
        for (int i = 0, j = 0; j < SIZE_WIN; i--, j++)
        {
            if ((field[v + i, h + j] == dot)) count++;
        }
        return count;
    }
    //проверка заполнения всей линии по диагонали вверх
    private static int CheckDiaUp(int v, int h, char dot)
    {
        int count = 0;
        for (int i = 0; i < SIZE_WIN; i++)
        {
            if ((field[i + v, i + h] == dot)) count++;
        }
        return count;
    }

    private static int CheckLineHorisont(int v, int h, char dot)
    {
        int count = 0;
        for (int j = h; j < SIZE_WIN + h; j++)
        {
            if ((field[v, j] == dot)) count++;
        }
        return count;
    }
    private static int CheckLineVertical(int v, int h, char dot)
    {
        int count = 0;
        for (int i = v; i < SIZE_WIN + v; i++)
        {
            if ((field[i, h] == dot)) count++;
        }
        return count;
    }

    static void Main(string[] args)
    {
        InitField();
        PrintField();
        do
        {
            playerMove();
            Console.WriteLine("Ваш ход на поле");
            PrintField();
            if (CheckWin(PLAYER_DOT))
            {
                Console.WriteLine("Вы выйграли!");
                break;
            }
            else if (IsFieldFull()) break;
            AiMove();
            Console.WriteLine("Ход Компьютера");
            PrintField();
            if (CheckWin(AI_DOT))
            {
                Console.WriteLine("Выиграл Компьютер!");
                break;
            }
            else if (IsFieldFull()) break;
        } while (true);
        Console.WriteLine("!Конец игры!");
    }
}