//Figures.cs
using System;
using System.Collections;
using static System.Console;
using static System.Math;
using System.Threading;

namespace House_Construction
{
    public class Point //точка
    {
        private int x;
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        private int y;
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public ConsoleColor Color;
        
        public Point() { }
        public Point(int x, int y, ConsoleColor color) { X = x; Y = y; Color = color; }
        public Point(int x, int y) { X = x; Y = y; }

        public override string ToString()
        {
            return $"x = {X} y = {Y}";
        }
    }

    public abstract class My_Object //объект
    {
        public Point P { get; set; } //координаты начала оси координат отсчета на экране
        public double Scale_x;   //масштаб вывода изображения по оси х
        public string Symbol;  //символ, с помощью которого выводится изображение       
        public ArrayList ArrPoint; //массив точек, из которых состоит объект
        public My_Object() { }
        public My_Object(ConsoleColor color)
        {
            P = new Point(0, 50, color);
            Scale_x = 1; Symbol = "*";
            ArrPoint = new ArrayList();
        }

        //приведение функции SetCursorPosition к отсчету от заданного
        //начала системы координат
        public void My_SetCursorPosition(Point XY)
        {
            SetCursorPosition((int)(XY.X + P.X * Scale_x), P.Y - XY.Y);
        }
        public void My_SetCursorPosition(int x, int y)
        {
            SetCursorPosition((int)(x + P.X * Scale_x), P.Y - y);
        }

        //заливка фигуры
        public void Fill()
        {
            int minY = P.Y, maxY = -P.Y, minX = (int)(P.X * Scale_x),
                maxX = (int)(-P.X * Scale_x);
            foreach (Point elem in ArrPoint)
            {
                minY = minY < elem.Y ? minY : elem.Y;
                maxY = maxY > elem.Y ? maxY : elem.Y;
                minX = minX < elem.X ? minX : elem.X;
                maxX = maxX > elem.X ? maxX : elem.X;
            }
            Point P_begin = null;
            Point P_end = null;
            ArrayList Arr_Revers = new ArrayList();
            Arr_Revers = ArrPoint;
            Arr_Revers.Reverse();

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    Point temp = new Point(x, y);
                    foreach (Point elem in ArrPoint)
                        if (elem.ToString() == temp.ToString())
                        {
                            P_begin = temp;
                            break;
                        }
                }
                for (int x = maxX; x >= minX; x--)
                {
                    Point temp = new Point(x, y);
                    foreach (Point elem in Arr_Revers)
                        if (elem.ToString() == temp.ToString())
                        {
                            P_end = temp;
                            break;
                        }
                }
                if (P_begin != null && P_end != null)
                    for (int x = P_begin.X; x >= P_end.X; x--)
                    {
                        My_SetCursorPosition(x, y);
                        //ForegroundColor = P.Color;
                        Write(Symbol);
                    }
            }
        }

        public abstract Double Formula(int i, int j);
        public virtual void Show(int time, bool full)
        {
            //ForegroundColor = P.Color;
            SetCursorPosition(P.X, P.Y); Write(Symbol);
        }
    }

    public class Section : My_Object //отрезок
    {
        Point P1;
        Point P2;
        public Section(int x1, int y1, int x2, int y2,
            ConsoleColor color) : base(color)
        {
            P1 = new Point(x1, y1);
            P2 = new Point(x2, y2);
        }
        public Section(Point p1, Point p2, ConsoleColor color) : base(color)
        {
            P1 = p1;
            P2 = p2;
        }

        //(x-x1)(y2-y1)-(y-y1)(x2-x1)=0 - ур-е прямой, проходящей через 2 точки
        //y = ((x-x1)*(y2-y1))/(x2-x1) + y1
        public override Double Formula(int x, int b)
        {
            double y = (x - P1.X * Scale_x) * (P2.Y - P1.Y) * 1 /
                (P2.X * Scale_x - P1.X * Scale_x) + P1.Y;
            return y;
        }
        //x = ((y-y1)*(x2-x1))/(y2-y1) + x1
        public Double Formula_x(int b, int y)
        {
            double x = ((y - P1.Y) * (P2.X * Scale_x - P1.X * Scale_x)
                * 1 / (P2.Y - P1.Y) + P1.X * Scale_x);
            return x;
        }

        public void MakeArrPoint()
        {
            int X_min, X_max, Y_min, Y_max;
            X_min = P1.X < P2.X ? P1.X : P2.X;
            X_max = P1.X > P2.X ? P1.X : P2.X;
            Y_min = P1.Y < P2.Y ? P1.Y : P2.Y;
            Y_max = P1.Y > P2.Y ? P1.Y : P2.Y;
            //ForegroundColor = P.Color;

            //создаем массив точек, из которых состоит фигура
            for (int x = (int)(X_min * Scale_x); x <= (int)(X_max * Scale_x); x++)
            {
                int y = (int)(Formula(x, 0));
                if (y > -P.Y && y < P.Y)
                {
                    Point temp = new Point(x, y);
                    ArrPoint.Add(temp);
                }
            }
            for (int y = Y_min; y < Y_max; y++)
            {
                int x = (int)(Formula_x(0, y));
                if (x > 0 && x < 250)
                {
                    Point temp = new Point(x, y);
                    ArrPoint.Add(temp);
                }
            }
        }

        public override void Show(int time, bool full)
        {
            //ForegroundColor = P.Color;
            MakeArrPoint();

            //вывод на экран массива точек
            foreach (Point item in ArrPoint)
            {
                My_SetCursorPosition(item);
                if (full == false) Symbol = ".";
                else Symbol = "*";
                Write(Symbol); 
                Thread.Sleep(10);
            }
        }
    }

    public class Rectangle : My_Object
    {
        public Point Begin;  //начальная точка
        public int Height;   //высота
        public int Width;    //ширина
        public Section S1, S2, S3, S4;

        public Rectangle(int x, int y, int height, int width, 
             ConsoleColor color) : base(color)
        {
            Begin = new Point(x, y);
            Height = height;
            Width = width;
            S1 = new Section(x, y, x, y + height, color);
            S2 = new Section(x, y + height, x + width, y + height, color);
            S3 = new Section(x + width, y + height, x + width, y, color);
            S4 = new Section(x + width, y, x, y, color);            
        }
        public override Double Formula(int i, int j) { return 1; }
        public override void Show(int time, bool full)
        {

            if (full == true) ForegroundColor = P.Color;
            else ForegroundColor = ConsoleColor.Black;
            if (full == true) BackgroundColor = P.Color;
            else BackgroundColor = ConsoleColor.White;

            S1.MakeArrPoint();
            S2.MakeArrPoint();
            S3.MakeArrPoint();
            S4.MakeArrPoint();
            ArrPoint.AddRange(S1.ArrPoint);
            ArrPoint.AddRange(S2.ArrPoint);
            ArrPoint.AddRange(S3.ArrPoint);
            ArrPoint.AddRange(S4.ArrPoint);
             
            int size_sleep = (time * 1000) / ArrPoint.Count;
            //вывод на экран массива точек
            foreach (Point item in ArrPoint)
            {
                My_SetCursorPosition(item);
                Write(Symbol); Thread.Sleep(size_sleep);
            }
            if (full == true)
            { Fill(); }
            ForegroundColor = ConsoleColor.Black;
            BackgroundColor = ConsoleColor.White;
        }
    }

    public class Trapezoid : My_Object //трапеция
    {
        public Section S1, S2, S3, S4;
        public Trapezoid(int x1, int y1, int x2, int y2, int x3, int y3,
            int x4, int y4, ConsoleColor color) : base(color)
        {

            S1 = new Section(x1, y1, x2, y2, color);
            S2 = new Section(x3, y3, x4, y4, color);
            S3 = new Section(x1, y1, x3, y3, color);
            S4 = new Section(x2, y2, x4, y4, color);          
        }

        public Trapezoid(int x, int y, int height, int wight, int incline,
          ConsoleColor color) : base(color)
        {

            S1 = new Section(x, y, x + incline, y + height, color);
            S2 = new Section(x + incline, y + height, x + wight - incline, y + height, color);
            S3 = new Section(x + wight - incline, y + height, x + wight, y, color);
            S4 = new Section(x + wight, y, x, y, color);
        }

        public override Double Formula(int i, int j) { return 1; }
        public override void Show(int time, bool full)
        {

            if (full == true) ForegroundColor = P.Color;
            else ForegroundColor = ConsoleColor.Black;

            if (full == true) BackgroundColor = P.Color;
            else BackgroundColor = ConsoleColor.White;
            S1.MakeArrPoint();
            S2.MakeArrPoint();
            S3.MakeArrPoint();
            S4.MakeArrPoint();
            ArrPoint.AddRange(S1.ArrPoint);
            ArrPoint.AddRange(S2.ArrPoint);
            ArrPoint.AddRange(S3.ArrPoint);
            ArrPoint.AddRange(S4.ArrPoint);
            int size_sleep = (time * 1000) / ArrPoint.Count;
            //вывод на экран массива точек
            foreach (Point item in ArrPoint)
            {
                My_SetCursorPosition(item);
                Write(Symbol); Thread.Sleep(size_sleep);
            }

            if (full == true)
            { Fill(); }

            ForegroundColor = ConsoleColor.Black;
            BackgroundColor = ConsoleColor.White;
        }
    }   
}
