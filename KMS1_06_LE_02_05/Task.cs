namespace KMS1_06_LE_02_05
{
    /// <summary>
    /// Die Klasse für Aufgaben. Repräsentiert eine Aufgabe mit einem Namen, einer Beschreibung und einer Priorität.
    /// </summary>
    internal class Task
    {
        /// <summary>
        /// Ruft den Namen der Aufgabe ab oder legt diesen fest.
        /// </summary>
        public string TaskName {get; set;}

        /// <summary>
        /// Ruft die Beschreibung der Aufgabe ab oder legt diese fest.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ruft die Priorität der Aufgabe ab oder legt diese fest.
        /// </summary>
        public string Prioritet { get; set; }    
    }
}
