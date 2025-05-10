using System;
using System.Collections.Generic;

namespace GestionEtudiants
{
    class Etudiant
    {
        public int NO { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public double NoteCC { get; set; }
        public double NoteDevoir { get; set; }

        public double Moyenne => (NoteCC * 0.33) + (NoteDevoir * 0.67);
    }

    class Program
    {
        static void Main()
        {
            SortedList<string, Etudiant> listeEtudiants = new SortedList<string, Etudiant>();

            Console.Write("Combien d'étudiants voulez-vous saisir ? ");
            int n;
            while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
            {
                Console.Write("Saisir un nombre valide : ");
            }

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"\nÉtudiant {i + 1}:");

                Console.Write("Numéro d’ordre (NO) : ");
                int no;
                while (!int.TryParse(Console.ReadLine(), out no))
                {
                    Console.Write("Numéro invalide. Réessayer : ");
                }

                Console.Write("Prénom : ");
                string prenom = Console.ReadLine();

                Console.Write("Nom : ");
                string nom = Console.ReadLine();

                while (listeEtudiants.ContainsKey(nom))
                {
                    Console.Write("Nom déjà utilisé comme clé. Entrez un autre nom : ");
                    nom = Console.ReadLine();
                }

                Console.Write("Note CC : ");
                double noteCC;
                while (!double.TryParse(Console.ReadLine(), out noteCC) || noteCC < 0 || noteCC > 20)
                {
                    Console.Write("Note CC invalide (0 à 20) : ");
                }

                Console.Write("Note Devoir : ");
                double noteDevoir;
                while (!double.TryParse(Console.ReadLine(), out noteDevoir) || noteDevoir < 0 || noteDevoir > 20)
                {
                    Console.Write("Note Devoir invalide (0 à 20) : ");
                }

                Etudiant etu = new Etudiant
                {
                    NO = no,
                    Prenom = prenom,
                    Nom = nom,
                    NoteCC = noteCC,
                    NoteDevoir = noteDevoir
                };

                listeEtudiants.Add(nom, etu);
            }

            // Initialisation de la file avec un initialiseur de collection
            Queue<Etudiant> fileEtudiants = new Queue<Etudiant>(listeEtudiants.Values);

            // Affichage direct
            Console.WriteLine("\n--- Liste des étudiants ---");
            double somme = 0;

            foreach (var pair in listeEtudiants)
            {
                Etudiant e = pair.Value;
                Console.WriteLine($"NO: {e.NO}, Nom: {e.Nom}, Prénom: {e.Prenom}, CC: {e.NoteCC}, Devoir: {e.NoteDevoir}, Moyenne: {e.Moyenne:F2}");
                somme += e.Moyenne;
            }

            Console.WriteLine($"\nMoyenne de la classe : {(somme / listeEtudiants.Count):F2}");

            // Recherche dans la file
            Console.Write("\nEntrez un prénom pour rechercher dans la file : ");
            string recherche = Console.ReadLine();

            bool trouve = false;
            foreach (Etudiant e in fileEtudiants)
            {
                if (e.Prenom.Equals(recherche, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"→ {e.Prenom} {e.Nom}, Moyenne: {e.Moyenne:F2}");
                    trouve = true;
                    break;
                }
            }

            if (!trouve)
            {
                Console.WriteLine("Étudiant non trouvé dans la file.");
            }

            // Option pour quitter
            Console.WriteLine("\nTapez 'q' puis Entrée pour quitter.");
            while (Console.ReadLine() != "q") { }
        }
    }
}
