using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS1_06_LE_02_05
{
    internal class Program
    {
       private static List<(string Task, string Description)> taskData = new List<(string Task, string Description)> ();
        static void Main(string[] args)
        {
            PrintMenu();
        }

        static void PrintMenu() 
        {
            int userInput;
            bool exit = false;
            while (!exit)
            {
                Console.Write("\n*** ToDo-Listen-Applikation ***\n[1] Aufgabe hinzufügen\n[2] Aufgabenliste anzeigen\n[3] Aufgabe bearbeiten \n[4] Aufgabe löschen\n[0] Programm beenden\nGeben Sie den Index Ihrer Wahl ein: ");
                userInput = InputNumber();
                switch (userInput)
                {
                    case 1:AddTask();break;
                    case 2:PrintTask();break;
                    case 3:EditTask();break;
                    case 4:DeleteTask();break;
                    case 0:exit = true;break;
                    default: Console.WriteLine("\n--- Geben Sie nur den Index von 0-4 ein ---\n");break;
                }
            }         
        }

        static void AddTask()
        { 
            string taskName, descriptionTask;
            Console.Write("\n*** Aufgabe hinzufügen ***\nAufgabe name: "); 
            taskName = InputString();
            Console.Write("Beschreibung: ");
            descriptionTask = Console.ReadLine();
            taskData.Add((taskName, descriptionTask));
        }

        static void PrintTask()
        {
            if (IsTaskInList())
            {
                int countTask=1;
                Console.WriteLine("\n*** Aufgabenliste anzeigen ***\n");
                foreach (var task in taskData)
                {
                    Console.WriteLine($"[{countTask}] Aufgabe: {task.Task} , Beschreibung: {task.Description}");
                    countTask++;
                }
            }        
        }

        static void EditTask()
        {
            if (IsTaskInList())
            {
                int index, indexOfTask;
                string newTaskName, newTaskDescription;
                PrintTask();
                Console.Write("\n*** Aufgabe bearbeiten ***\nGeben Sie den Index Ihrer Wahl ein: ");
                indexOfTask = InputNumber()-1;
                if (indexOfTask < 0 || indexOfTask >=taskData.Count)
                {
                    Console.WriteLine("\n--- Ungültiger Index ---\n");
                    return;
                }
                var taskFromIndex = taskData.ElementAt(indexOfTask);
                Console.WriteLine($"\n[1] Wechseln Aufgabe name({taskFromIndex.Task})\n[2] Wechseln Beschreibung({taskFromIndex.Description})\n[0] Zurück zum Menü\nGeben Sie den Index Ihrer Wahl ein: ");
                index = InputNumber();
                switch(index)
                {
                    case 0: break;
                    case 1:
                        Console.Write("Neu Aufgabe name: ");
                        newTaskName = InputString();
                        taskData[indexOfTask] = (newTaskName, taskFromIndex.Description);
                        break;
                    case 2:
                        Console.Write("Neu Beschreibung: ");
                        newTaskDescription = InputString();
                        taskData[indexOfTask] = (taskFromIndex.Task, newTaskDescription);
                        break;
                    default: Console.WriteLine("\n--- Ungültiger Index ---\n");break;
                }
            }
        }

        static void DeleteTask()
        {
            if(IsTaskInList())
            {
                int index, indexOfTask;
                PrintTask();
                Console.Write("\n*** Aufgabe löschen ***\nGeben Sie den Index Ihrer Wahl ein: ");
                indexOfTask = InputNumber()-1;
                if(indexOfTask < 0 || indexOfTask >= taskData.Count)
                {
                    Console.WriteLine("\n--- Ungültiger Index ---\n");
                    return;
                }
                taskData.RemoveAt(indexOfTask);
                Console.WriteLine("\n° Löschung erfolgreich °\n"); 
            }
        }

        static bool IsTaskInList()
        {
            if (taskData.Count == 0)
            {
                Console.WriteLine("\n--- Keine Daten verfügbar ---\n");
                return false;
            }
            return true;
        }


        static string InputString()
        {
            bool exit = false;
            string input = "";
            while (!exit)
            {
                input = Console.ReadLine();
                if (input.Any(char.IsDigit))
                {
                    Console.WriteLine("\n--- Ungültige Eingabe. Schreiben Sie nur Alphabete ---\n");
                }
                else if (input.Length < 1)
                {
                    Console.WriteLine("\n--- Die Eingabe darf nicht leer sein ---\n");
                }
                else
                {
                    exit = true;
                }
            }
            return input;
        }

        static int InputNumber()
        {
            int number;
            while (true)
            {
                try
                {
                    number = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n--- Geben Sie nur eine Ganzzahl ein ---\n");
                }
            }
            return number;
        }
    }
}
