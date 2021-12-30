//Parts.cs
using System;
using static System.Console;
using System.Collections;

namespace House_Construction
{
    interface IPart
    {
        bool IsBuilt { get; set; }       
        int Time { get; set; }   
        bool Full { get; set; }
        string Name { get; set; }
        void Show();
    }

    public class Basement : IPart  //фундамент
    {
        bool isBuilt = false;        
        int time; 
        Rectangle foundation;    
        bool full = false;
        string name;
        

        public Basement(int x, int y, int h, int w, ConsoleColor col, int t)
        {           
            foundation = new Rectangle(x, y, h, w, col);
            time = t;
            Name = "Basement";
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool IsBuilt
        { 
            get { return isBuilt; }
            set { isBuilt = value; }
        }

        public bool Full
        {
            get { return full; }
            set { full = value; }
        }

        public int Time
        { 
            get { return time; }
            set { time = value; }
        }

        public void Show() 
        {
            foundation.Show(time, full);
        }
    }

    public class Wall : IPart  //стена
    {
        bool isBuilt = false;        
        int time;
        Rectangle wall;
        bool full = false;
        string name;
        static int n = 0;

        public Wall(int x, int y, int h, int w, ConsoleColor col, int t)
        {
            wall = new Rectangle(x, y, h, w, col);
            time = t;
            n++;
            Name = "Wall - " + n;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool Full
        {
            get { return full; }
            set { full = value; }
        }

        public bool IsBuilt
        {
            get { return isBuilt; }
            set { isBuilt = value; }
        }

        public int Time
        {
            get { return time; }
            set { time = value; }
        }

        public void Show()
        {
            wall.Show(time, full);
        }
    }

    public class Door : IPart  //двери
    {
        bool isBuilt = false;
        int time;
        Rectangle door;
        bool full = false;
        string name;

        public Door(int x, int y, int h, int w, ConsoleColor col, int t)
        {
            door = new Rectangle(x, y, h, w, col);
            time = t;
            Name = "Door";
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool Full
        {
            get { return full; }
            set { full = value; }
        }

        public bool IsBuilt
        {
            get { return isBuilt; }
            set { isBuilt = value; }
        }

        public int Time
        {
            get { return time; }
            set { time = value; }
        }

        public void Show()
        {
            door.Show(time, full);
        }
    }

    public class Roof : IPart  //крыша
    {
        bool isBuilt = false;
        int time;
        Trapezoid roof;
        bool full = false;   
        string name;

        public Roof(int x, int y, int h, int w, int i, ConsoleColor col, int t)
        {
            roof = new Trapezoid(x, y, h, w, i, col);
            time = t;
            Name = "Roof";
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool Full
        {
            get { return full; }
            set { full = value; }
        }

        public bool IsBuilt
        {
            get { return isBuilt; }
            set { isBuilt = value; }
        }

        public int Time
        {
            get { return time; }
            set { time = value; }
        }

        public void Show()
        {
            roof.Show(time, full);
        }
    }

    public class Window : IPart  //окно
    {
        bool isBuilt = false;        
        int time; //work time
        Rectangle window;
        Rectangle[] part_window;
        bool full;
        string name;
        static int n = 0;

        public Window(int x, int y, int h, int w, ConsoleColor col1, ConsoleColor col2, int t)
        {
            window = new Rectangle(x, y, h, w, col1);
            part_window = new Rectangle[4];
            int h_part = (h - 3) / 2;
            int w_part = (w - 3) / 2;
            part_window[0] = new Rectangle(x + 1, y + 1, h_part, w_part, col2);
            part_window[1] = new Rectangle(x + 3 + w_part, y + 1, h_part, w_part, col2);
            part_window[2] = new Rectangle(x + 1, y + 3 + h_part, h_part, w_part, col2);
            part_window[3] = new Rectangle(x + 3 + w_part, y + 3 + h_part, h_part, w_part, col2);
            time = t;
            n++;
            Name = "Window - " + n;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool Full
        {
            get { return full; }
            set { full = value; }
        }

        public bool IsBuilt
        {
            get { return isBuilt; }
            set { isBuilt = value; }
        }

        public int Time
        {
            get { return time; }
            set { time = value; }
        }

        public void Show()
        {
            window.Show(time/2, full);

            for (int i = 0; i < 4; i++)
            {
                part_window[i].Show(time/2, full);                
            }
        }        
    }

    public class HouseProject
    {
        //create an array House
        public ArrayList House = new ArrayList();
        public void SetHouse()
        {
            House.Add(new Basement(20, 0, 2, 200, ConsoleColor.DarkGray, 10));
            House.Add(new Wall(20, 3, 25, 50, ConsoleColor.Yellow, 10));
            House.Add(new Wall(70, 3, 25, 50, ConsoleColor.Yellow, 5));
            House.Add(new Wall(120, 3, 25, 50, ConsoleColor.Yellow, 15));
            House.Add(new Wall(170, 3, 25, 50, ConsoleColor.Yellow, 8));
            House.Add(new Door(110, 3, 16, 20, ConsoleColor.DarkGreen, 10));
            House.Add(new Window(40, 10, 12, 20, ConsoleColor.DarkGreen,
                ConsoleColor.DarkYellow, 10));
            House.Add(new Window(80, 10, 12, 20, ConsoleColor.DarkGreen,
                ConsoleColor.DarkYellow, 10));
            House.Add(new Window(140, 10, 12, 20, ConsoleColor.DarkGreen,
               ConsoleColor.DarkYellow, 8));
            House.Add(new Window(180, 10, 12, 20, ConsoleColor.DarkGreen,
               ConsoleColor.DarkYellow, 10));
            House.Add(new Roof(15, 28, 5, 210, 5, ConsoleColor.DarkRed, 20));
        }       

        public void ShowProject()
        {
            foreach (IPart el in House)
            {
                el.Time = 1;
                el.Full = false;
                el.Show();                
            }
        }      
    }
}
