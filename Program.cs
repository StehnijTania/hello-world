//Реализовать программу “Строительство дома”
//Реализовать:
//Классы:
//=======
//House(Дом), Basement(Фундамент), Walls(Стены), Door(Дверь),
//Window(Окно), Roof(Крыша); 
//Team(Бригада строителей), Worker(Строитель), TeamLeader(Бригадир).
//Интерфейсы
//==========
//IWorker, IPart.
//Все части дома должны реализовать интерфейс Ipart (Часть дома), для
//рабочих и бригадира предоставляется базовый интерфейс IWorker (Рабочий).
//Бригада строителей(Team) строит дом(House). 
//Дом состоит из фундамента (Basement), стен(Wall), окон(Window),
//дверей(Door), крыши(Part).
//Согласно проекту, в доме должно быть:
//=====================================
//1 фундамент, 4 стены, 1 дверь, 4 окна и 1 крыша.
//Бригада начинает работу, и строители последовательно “строят” дом,
//начиная с фундамента. Каждый строитель не знает заранее, на чём
//завершился предыдущий этап строительства, поэтому он “проверяет”,
//что уже построено и продолжает работу. Если в игру вступает бригадир
//(TeamLeader), он не строит, а формирует отчёт, что уже построено и
//какая часть работы выполнена.
//В конечном итоге на консоль выводится сообщение, что строительство
//дома завершено и отображается “рисунок дома” (вариант отображения
//выбрать самостоятельно).
//sweqwrwerw

//Program.cs

using System;
using static System.Console;
using System.Threading;

namespace House_Construction
{
    class Program
    {      
        static void FOR(int n, char sym)
        {
            for (int i = 0; i < n; i++) Write(sym);
        }
        static void Main(string[] args)
        {
            SetWindowSize(250, 100);
            ForegroundColor = ConsoleColor.Black;
            BackgroundColor = ConsoleColor.White;
            var rnd = new Random();

            //create an array House
            HouseProject Project = new HouseProject();
            Project.SetHouse();

            //displaying the project at house
            Project.ShowProject();

            //create an array Теам
            Team team = new Team();
            team.MakeTeam();            
            team.Show();


            SetCursorPosition(95, 11);
            FOR(55, '=');
            SetCursorPosition(95, 12);
            Write("**   H  O  U  S  E             P  R  O  J  E  C  T   **");
            SetCursorPosition(95, 13);
            FOR(55, '=');

            SetCursorPosition(80, 53);
            Write("**   HOUSE CONSTRUCTION WORKS:   **");
            SetCursorPosition(80, 54);
            FOR(35, '=');

            SetCursorPosition(170, 53);
            Write("**   REPORTS ON PERFORMED WORKS:  **");
            SetCursorPosition(170, 54);
            FOR(36, '=');    
            
            foreach (IPart part in Project.House)
            {
                int y1 = 0;
                if (part.IsBuilt == false) //if part of the house is unbuilt
                    {
                    part.Time = rnd.Next(2, 5);  //randomly set the time of work

                    //looking for free workers
                                        foreach (IWorker worker in team.brigade)
                    {
                        if (worker.IsWorking == false)//if the worker is not working
                            {
                            SetCursorPosition(80, 57 + y1);
                            team.Leader.Organize(worker, part); //the foreman gives out the task
                            FOR(10, ' ');
                            Thread.Sleep(100);
                            worker.IsWorking = true; //the worker's status changes to "working"                            
                            y1++;                          
                        }
                    }
                    y1++;
                    //displaying messages about who does what work
                    foreach (IWorker worker in team.brigade)
                    {
                        SetCursorPosition(80, 57 + y1);
                        string s = worker.ToString();
                        
                        Write(worker.ToString() + " is engaged in the manufacture of the " + part.Name);
                        FOR(10, ' ');
                        Thread.Sleep(100);
                        y1++;
                    }
                    y1++;
                    SetCursorPosition(80, 57 + y1);
                    team.Leader.Control(part);
                    FOR(10, ' ');

                    part.Full = true;
                    part.Show();     //display of the image of the part of the house under construction
                    part.IsBuilt = true;   //change the status to "built"
                    foreach (IWorker worker in team.brigade)
                        if(worker is IWorker) worker.IsWorking = false;  //change the worker status to "not working"

                }

                //foreman makes a report                          
                team.Leader.MakeReport(170, 56, Project);               
            }
           

            SetCursorPosition(95, 11);
            FOR(55, '=');
            SetCursorPosition(95, 12);
            Write("**        H  O  U  S  E     I S    B U I L T  !      **");
            SetCursorPosition(95, 13);
            FOR(55, '=');

            for (int i = 0; i < 15; i++)
            {
                SetCursorPosition(80, 53 + i);
                FOR(80, ' ');
            }                       

            ReadKey();        
        } 
    }
}
