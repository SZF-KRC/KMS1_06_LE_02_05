namespace KMS1_06_LE_02_05
{
    internal class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt des Programms, der die Aufgabe hat, das Menü zu starten
        /// </summary>
        /// <param name="args">Befehlszeilenargumente.</param>
        static void Main(string[] args)
        {
            //Rufen Sie die Klasse auf, damit wir das Hauptmenü starten können
            ToDoManager toDoManager = new ToDoManager();

            // Starten Sie das Hauptmenü
            toDoManager.PrintMenu();

        }   
    }
}
