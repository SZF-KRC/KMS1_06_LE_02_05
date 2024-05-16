using System;
using System.Collections.Generic;
using System.Linq;

namespace KMS1_06_LE_02_05
{
    /// <summary>
    /// Die Klasse zum Verwalten von Aufgaben in List
    /// </summary>
    internal class ToDoManager
    {
        private static List<Task> _tasks = new List<Task>();

        /// <summary>
        /// Zeigt das Menü an und verarbeitet die Benutzereingaben.
        /// </summary>
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
                    case 1: AddTask(); break;//Aufrufen der Task-Speichermethode
                    case 2: PrintTask("Aufgabenliste anzeigen"); break;//Aufrufen auf Aufgabe anzeigen
                    case 3: EditTask(); break;//Aufrufe zum Bearbeiten von Aufgaben
                    case 4: DeleteTask(); break;//Aufrufen auf Löschaufgaben
                    case 5: PrintTaskByPriority();break; //Aufrufe auf Aufgabe entsprechend der angegebenen Priorität anzeigen
                    case 6: PrintTaskByKeywordsInDescription(); break;// Aufrufe auf Aufgabe entsprechend dem Schlüsselwort in der Aufgabenbeschreibung
                    case 0: exit = true; break;// Beenden Sie das Programm, indem Sie den Exit-Wert auf „true“ setzen, um die Schleife zu stoppen
                    default: Console.WriteLine("\n--- Geben Sie nur den Index von 0-6 ein ---\n"); break; //Wenn der Benutzer keine Zahl im Bereich von 0 bis 6 eingibt, wird immer eine Warnung angezeigt und die Schleife kehrt zum Anfang zurück
                }
            }
        }

        /// <summary>
        /// Fügt eine neue Aufgabe zur Liste hinzu.
        /// </summary>
        private static void AddTask()
        {
            Task newTask = new Task();
            Console.Write("\n*** Aufgabe hinzufügen ***\n");
            newTask.TaskName = InputString("Aufgabe name: ");// Der Benutzer wird aufgefordert, den Namen der Aufgabe einzugeben.
            newTask.Description = InputString("Beschreibung: ");// Der Benutzer wird aufgefordert, die Beschreibung der Aufgabe einzugeben.
            newTask.Prioritet = InputPriority("Priorität (hoch,mittel,niedrig): ");// Der Benutzer wird aufgefordert, die Priorität der Aufgabe anzugeben.
            _tasks.Add(newTask);// Die neue Aufgabe wird der Liste hinzugefügt.
        }

        /// <summary>
        /// Zeigt die Liste der Aufgaben an.
        /// </summary>
        /// <param name="promt">Die Anzeigeaufforderung.</param>
        private static void PrintTask(string promt)
        {
            if (IsTaskInList())// Überprüft, ob Aufgaben in der Liste vorhanden sind.
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

        /// <summary>
        /// Bearbeitet eine bestehende Aufgabe.
        /// </summary>
        private static void EditTask()
        {
            if (IsTaskInList())// Überprüft, ob Aufgaben in der Liste vorhanden sind.
            {
                PrintTask("Aufgabe bearbeiten");//Dem Benutzer wird die gesamte Aufgabenliste angezeigt
                Task task = _tasks.ElementAt(InputIndex());// Wählt die Aufgabe basierend auf dem vom Benutzer eingegebenen Index aus.
                Console.WriteLine($"\n[1] Wechseln Aufgabe name({task.TaskName})\n[2] Wechseln Beschreibung({task.Description})\n[3] Wecheln Priorität({task.Prioritet})\n[0] Zurück zum Menü");
                switch (InputNumber("Geben Sie den Index Ihrer Wahl ein: "))
                {
                    case 0: break;// Zurück zum Menü.
                    case 1: task.TaskName = InputString("Neu Aufgabe name: "); break;// Ändert den Namen der Aufgabe.
                    case 2: task.Description = InputString("Neu Beschreibung: "); break;// Ändert die Beschreibung der Aufgabe.
                    case 3: task.Prioritet = InputPriority("Neu Priorität: ");break;// Ändert die Priorität der Aufgabe.
                    default: Console.WriteLine("\n--- Ungültiger Index ---\n"); break;// Gibt eine Fehlermeldung aus, wenn der Index ungültig ist.
                }
            }            
        }

        /// <summary>
        /// Löscht eine Aufgabe aus der Liste.
        /// </summary>
        private static void DeleteTask()
        {
            if (IsTaskInList())// Überprüft, ob Aufgaben in der Liste vorhanden sind.
            {
                PrintTask("Aufgabe löschen");//Dem Benutzer wird die gesamte Aufgabenliste angezeigt
                Task task = _tasks.ElementAt(InputIndex());// Wählt die Aufgabe basierend auf dem vom Benutzer eingegebenen Index aus.
                _tasks.Remove(task);// Entfernt die ausgewählte Aufgabe aus der Liste.
                Console.WriteLine("\n° Löschung erfolgreich °\n");
            }
        }

        /// <summary>
        /// Zeigt Aufgaben nach Priorität sortiert an.
        /// </summary>
        private static void PrintTaskByPriority()
        {
            if (IsTaskInList())// Überprüft, ob Aufgaben in der Liste vorhanden sind.
            {
                string priority = InputPriority("Aufgaben nach Priorität (hoch, mittel, niedrig) zu sortieren: ");// Fordert den Benutzer auf, die Priorität anzugeben.
                List<Task> result = _tasks.FindAll(task => task.Prioritet.Equals(priority));// Findet alle Aufgaben mit der angegebenen Priorität.
                if (result.Count > 0)
                {
                    Console.WriteLine($"\n*** Aufgaben mit Priorität '{priority}' ***\n");
                    foreach (Task task in result)
                    {
                        // Zeigt die Details jeder gefundenen Aufgabe an.
                        Console.WriteLine($"Aufgabe: {task.TaskName}\nPriorität: {task.Prioritet}\nBeschreibung: {task.Description}\n");
                    }
                }
                else
                {
                    // Gibt eine Fehlermeldung aus, wenn keine Aufgaben mit der angegebenen Priorität gefunden wurden.
                    Console.WriteLine($"\n--- Keine Aufgaben mit Priorität '{priority}' gefunden ---\n");
                }
            }
        }

        /// <summary>
        /// Zeigt Aufgaben an, die ein bestimmtes Schlüsselwort in ihrer Beschreibung enthalten.
        /// </summary>
        private static void PrintTaskByKeywordsInDescription()
        {
            if (IsTaskInList())// Überprüft, ob Aufgaben in der Liste vorhanden sind.
            {
                string keyword = InputString("Aufgaben nach Schlüsselwörtern: ");// Fordert den Benutzer auf, ein Schlüsselwort einzugeben.
                List<Task> result = _tasks.FindAll(task => task.Description.IndexOf(keyword) >= 0);// Findet alle Aufgaben, die das Schlüsselwort in der Beschreibung enthalten.
                if (result.Count > 0)
                {
                    Console.WriteLine($"\n*** Aufgaben nach Schlüsselwörtern '{keyword}' ***\n");
                    foreach(Task task in result)
                    {
                        // Zeigt die Details jeder gefundenen Aufgabe an.
                        Console.WriteLine($"Aufgabe: {task.TaskName}\nPriorität: {task.Prioritet}\nBeschreibung: {task.Description}\n");
                    }
                }
                else
                {
                    // Gibt eine Fehlermeldung aus, wenn keine Aufgaben mit dem Schlüsselwort gefunden wurden.
                    Console.WriteLine($"\n--- Keine Aufgaben nach Schlüsselwörtern '{keyword}' gefunden ---\n");
                }
            }
        }

        /// <summary>
        /// Überprüft, ob Aufgaben in der Liste vorhanden sind.
        /// </summary>
        /// <returns>True, wenn Aufgaben vorhanden sind, ansonsten false.</returns>
        private static bool IsTaskInList()
        {
            if (_tasks.Count == 0)//Wenn keine Aufgabe in List ist, geben wir den Wert false zurück
            {
                Console.WriteLine("\n--- Keine Daten verfügbar ---\n");// Gibt eine Meldung aus, wenn keine Aufgaben in der Liste vorhanden sind.
                return false;
            }
            return true;//Wenn die Liste mindestens eine Aufgabe enthält, geben wir true zurück
        }

        /// <summary>
        /// Liest eine Zeichenfolge vom Benutzer ein, die nur alphabetisch und gleichzeitig nicht leer sein kann.
        /// </summary>
        /// <param name="promt">Die Anzeigeaufforderung.</param>
        /// <returns>Die Benutzereingabe als Zeichenfolge mit der korrekten Ausgabe</returns>
        private static string InputString(string promt)
        {
            bool exit = false;
            string input = "";
            while (!exit)
            {
                Console.Write(promt);
                input = Console.ReadLine();
                if (input.Any(char.IsDigit))//wenn eines aller Zeichen in der Zeichenfolge eine Zahl enthält
                {
                    Console.WriteLine("\n--- Ungültige Eingabe. Schreiben Sie nur Alphabete ---\n");
                }
                else if (input.Length < 1) //wenn die Eingabe leer ist
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

        /// <summary>
        /// Lesen Sie eine Zeichenfolge vom Benutzer ein, die nur eine Ganzzahl.
        /// </summary>
        /// <param name="promt">Die Anzeigeaufforderung.</param>
        /// <returns>Die Benutzereingabe als Ganzzahl</returns>
        private static int InputNumber(string promt)
        {
            int number;
            while (true)
            {
                Console.Write($"\n{promt}");
                try//Wir werden versuchen, jede Eingabe in eine Ganzzahl umzuwandeln. Wenn dies fehlschlägt, ist die Eingabe keine Zahl und die Schleife wiederholt sich
                {
                    number = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("\n--- Geben Sie nur eine Ganzzahl ein ---\n");
                }
            }
            return number;
        }

        /// <summary>
        /// List einen Index vom Benutzer ein, der nur innerhalb der Anzahl der Aufgaben in der Liste liegen kann
        /// </summary>
        /// <returns>Die Benutzereingabe als Index, der den Indizes im Brief entspricht</returns>
        private static int InputIndex()
        {
            while (true)
            {
                //Wir erhalten eine ganze Zahl und subtrahieren 1 davon, da dem Benutzer die Liste ausgegeben wird, die bei 1 und nicht bei Null beginnt
                int indexOfTask = InputNumber("Geben Sie den Index Ihrer Wahl ein: ") - 1;
                if (indexOfTask >= 0 && indexOfTask < _tasks.Count)// Wir prüfen, ob die Anzahl im Bereich von 0 bis zur Anzahl der Aufgaben in der Liste liegt
                {
                    return indexOfTask;
                }
                Console.WriteLine("\n--- Ungültiger Index ---\n");
            }
        }

        /// <summary>
        /// Liest eine Priorität  vom Benutzer ein, sodass der Benutzer nur eine von drei Optionen (hoch, mittel, niedrig) eingeben kann.
        /// </summary>
        /// <param name="promt">Die Anzeigeaufforderung.</param>
        /// <returns>Die Benutzereingabe als Priorität, reduziert auf Kleinbuchstaben ohne führende und nachgestellte Leerzeichen und nur als (hoch, mittel, niedrig)</returns>
        private static string InputPriority(string promt)
        {
            while (true)
            {
                //Außerdem haben wir die String-Eingabe mit Leerzeichen vor und nach dem eingegebenen Wort bereinigt und auch auf Kleinbuchstaben gesetzt, um die nächste Bedingung besser prüfen zu können
                string prioritet = InputString(promt).ToLower().Trim();
                //Wenn die Eingabe hoch, mittel oder niedrig enthält, wird der Wert der Eingabe zurückgegeben, andernfalls wird die Schleife wiederholt
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
