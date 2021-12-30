//Worker.cs
using System;
using static System.Console;
using System.Collections;

namespace House_Construction
{    
    public class Human 
    {
        public Human (string f_name, string l_name, DateTime date)
        {
            FirstName = f_name;
            LastName = l_name;
            BirthDate = date;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public override string ToString()
        {   return $"{LastName} {FirstName}";    }
    }

    public class Employee : Human
    {
        public string Position { get; set; } //должность       
        public Employee(string f_name, string l_name, DateTime date, 
            string position) : base(f_name, l_name, date)
        {
            Position = position;            
        }
        public override string ToString() 
        {
            return base.ToString() + $", { Position}";
        }             
    }
    interface IWorker
    {
        bool IsWorking { get; set; }       
    }

    interface IManager
    {        
        void Organize(IWorker worker, IPart part);
        void MakeReport(int x, int y, HouseProject house);
        void Control(IPart part);
    }

    class Teamleader : Employee, IManager, IWorker
    {
        public Teamleader(string f_name, string l_name, DateTime date,
            string position):
            base(f_name, l_name, date, position) { }

        bool isWorking = true;

        public bool IsWorking
        {
            get { return isWorking; }
            set { isWorking = value; }
        }

        public void Control(IPart part)
        {   Write("The TeamLeader upervises the work on the manufacture of the "
            + part.Name);  
        }

        public void MakeReport(int x, int y, HouseProject house)
        {
            SetCursorPosition(x, y);
            Write("To date, the following work has been done:");

            foreach (IPart part in house.House)
            {
                y++;
                SetCursorPosition(x, y);
                if (part.IsBuilt == true)
                    Write(part.Name);
                if(part == house.House[house.House.Count - 1] && part.IsBuilt == true)
                {
                    y++;
                    SetCursorPosition(x, y);
                    Write("====================");
                    y++;
                    SetCursorPosition(x, y);
                    Write("The construction of the house is over!");
                }
            }            
        }

        public void Organize(IWorker worker, IPart part)
        {  
            Write("The TeamLeader instructs " + worker.ToString() +
            " to make the " + part.Name);  
        }             
    }

    class Builder : Employee, IWorker
    {         
        public Builder(string f_name, string l_name, DateTime date,
           string position) :
           base(f_name, l_name, date, position) { }

        bool isWorking = false;
        public bool IsWorking
        {
            get { return isWorking; }
            set { isWorking = value; }
        }       
    }
    
    class Team
    {
        public ArrayList brigade = new ArrayList();
        public Teamleader Leader = new Teamleader("Ivanchenko", "Anton",
                new DateTime(1988, 3, 31), "teamleader");

        public void MakeTeam()
        {            
            brigade.Add(new Builder("Semenov", "Nikolay",
                new DateTime(1976, 11, 25), "plasterer")); //штукатур
            brigade.Add(new Builder("Nikonenko", "Stepan",
                new DateTime(1985, 12, 3), "fitter"));  //монтажник
            brigade.Add(new Builder("Antonenko", "Ruslan",
                new DateTime(1965, 7, 12), "fitter"));  //монтажник
        }

        public void Show()
        {
            int i = 1;
            SetCursorPosition(20, 55);
            Write("Construction brigade:");
            SetCursorPosition(20, 55+i);
            Write("=====================");
            i++;
            SetCursorPosition(20, 55 + i);
            WriteLine(Leader.ToString());
            foreach (IWorker el in brigade)
            { 
                i++;
                SetCursorPosition(20, 55 + i);
                Write(el.ToString()); 
            }
        }        
    }
}
