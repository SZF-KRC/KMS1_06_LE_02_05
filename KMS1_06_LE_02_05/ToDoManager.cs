using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMS1_06_LE_02_05
{
    internal class ToDoManager
    {
        private static List<Task> _tasks = new List<Task>();

        public void PrintMenu()
        {
            int userInput;
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n*** ToDo-Listen-Applikation ***\n[1] Aufgabe hinzufügen\n[2] Aufgabenliste anzeigen\n[3] Aufgabe bearbeiten \n[4] Aufgabe löschen\n[5] Aufgaben nach Priorität\n[6] Aufgaben nach Schlüsselwörtern\n[0] Programm beenden");
                userInput = InputNumber("Geben Sie den Index Ihrer Wahl ein: ");
                switch (userInput)
                {
                    case 1: AddTask(); break;
                    case 2: PrintTask("Aufgabenliste anzeigen"); break;
                    case 3: EditTask(); break;
                    case 4: DeleteTask(); break;
                    case 5: PrintTaskByPriority();break;
                    case 6: PrintTaskByKeywordsInDescription(); break;
                    case 0: exit = true; break;
                    default: Console.WriteLine("\n--- Geben Sie nur den Index von 0-6 ein ---\n"); break;
                }
            }
        }

        private static void AddTask()
        {
            Task newTask = new Task();
            Console.Write("\n*** Aufgabe hinzufügen ***\n");
            newTask.TaskName = InputString("Aufgabe name: ");
            newTask.Description = InputString("Beschreibung: ");
            newTask.Prioritet = InputPriority("Priorität (hoch,mittel,niedrig): ");
            _tasks.Add(newTask);
        }

        private static void PrintTask(string promt)
        {
            if (IsTaskInList())
            {
                int countTask = 1;
                Console.WriteLine($"\n*** {promt} ***\n");
                foreach (Task task in _tasks)
                {
                    Console.WriteLine($"[{countTask}] Aufgabe: {task.TaskName}\nPriorität: {task.Prioritet}\nBeschreibung: {task.Description}\n");
                    countTask++;
                }
            }
        }

        private static void EditTask()
        {
            if (IsTaskInList())
            {
                PrintTask("Aufgabe bearbeiten");
                Task task = _tasks.ElementAt(InputIndex());
                Console.WriteLine($"\n[1] Wechseln Aufgabe name({task.TaskName})\n[2] Wechseln Beschreibung({task.Description})\n[3] Wecheln Priorität({task.Prioritet})\n[0] Zurück zum Menü");
                switch (InputNumber("Geben Sie den Index Ihrer Wahl ein: "))
                {
                    case 0: break;
                    case 1: task.TaskName = InputString("Neu Aufgabe name: "); break;
                    case 2: task.Description = InputString("Neu Beschreibung: "); break;
                    case 3: task.Prioritet = InputPriority("Neu Priorität: ");break;
                    default: Console.WriteLine("\n--- Ungültiger Index ---\n"); break;
                }
            }            
        }

        private static void DeleteTask()
        {
            if (IsTaskInList())
            {
                PrintTask("Aufgabe löschen");
                Task task = _tasks.ElementAt(InputIndex());
                _tasks.Remove(task);
                Console.WriteLine("\n° Löschung erfolgreich °\n");
            }
        }

        private static void PrintTaskByPriority()
        {
            if (IsTaskInList())
            {
                string priority = InputPriority("Aufgaben nach Priorität (hoch, mittel, niedrig) zu sortieren: ");
                List<Task> result = _tasks.FindAll(task => task.Prioritet.Equals(priority));
                if (result.Count > 0)
                {
                    Console.WriteLine($"\n*** Aufgaben mit Priorität '{priority}' ***\n");
                    foreach (Task task in result)
                    {
                        Console.WriteLine($"Aufgabe: {task.TaskName}\nPriorität: {task.Prioritet}\nBeschreibung: {task.Description}\n");
                    }
                }
                else
                {
                    Console.WriteLine($"\n--- Keine Aufgaben mit Priorität '{priority}' gefunden ---\n");
                }
            }
        }

        private static void PrintTaskByKeywordsInDescription()
        {
            if (IsTaskInList())
            {
                string keyword = InputString("Aufgaben nach Schlüsselwörtern: ");
                List<Task> result = _tasks.FindAll(task => task.Description.IndexOf(keyword) >= 0);
                if (result.Count > 0)
                {
                    Console.WriteLine($"\n*** Aufgaben nach Schlüsselwörtern '{keyword}' ***\n");
                    foreach(Task task in result)
                    {
                        Console.WriteLine($"Aufgabe: {task.TaskName}\nPriorität: {task.Prioritet}\nBeschreibung: {task.Description}\n");
                    }
                }
                else
                {
                    Console.WriteLine($"\n--- Keine Aufgaben nach Schlüsselwörtern '{keyword}' gefunden ---\n");
                }
            }
        }

        private static bool IsTaskInList()
        {
            if (_tasks.Count == 0)
            {
                Console.WriteLine("\n--- Keine Daten verfügbar ---\n");
                return false;
            }
            return true;
        }

        private static string InputString(string promt)
        {
            bool exit = false;
            string input = "";
            while (!exit)
            {
                Console.Write(promt);
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

        private static int InputNumber(string promt)
        {
            int number;
            while (true)
            {
                Console.Write($"\n{promt}");
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

        private static int InputIndex()
        {
            while (true)
            {
                int indexOfTask = InputNumber("Geben Sie den Index Ihrer Wahl ein: ") - 1;
                if (indexOfTask >= 0 && indexOfTask < _tasks.Count)
                {
                    return indexOfTask;
                }
                Console.WriteLine("\n--- Ungültiger Index ---\n");
            }
        }

        private static string InputPriority(string promt)
        {
            while (true)
            {
                string prioritet = InputString(promt).ToLower().Trim();
                if (prioritet.Equals("hoch") || prioritet.Equals("mittel") || prioritet.Equals("niedrig"))
                {
                    return prioritet;
                }
                else
                {
                    Console.WriteLine("Geben Sie Bitte nur [hoch] oder [mittel] oder [niedrig]");
                }
            }
        }
    }
}
