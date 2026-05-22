using System;
using Spectre.Console;

namespace GolfTaskManager;

public class MenuPrincipal
{
    private GestionnaireTaches gestionnaire;

    private ChefEquipe chefEquipe;

    public MenuPrincipal()
    {
        gestionnaire = new GestionnaireTaches();
    }

    public void Afficher()
    {
        bool quitter = false;

        while (!quitter)
        {
            Console.Clear();

            AnsiConsole.MarkupLine("[green bold]=== GolfTaskManager ===[/]");
            AnsiConsole.WriteLine();

            Console.WriteLine("=== GolfTaskManager ===");
            Console.WriteLine("1. Ajouter une tâche");
            Console.WriteLine("2. Afficher les tâches");
            Console.WriteLine("3. Marquer une tâche comme terminée");
            Console.WriteLine("4. Supprimer une tâche");
            Console.WriteLine("5. Filtrer les tâches par fréquence");
            Console.WriteLine("6. Attribuer une tache");
            Console.WriteLine("7. Les taches d'un ouvrier");
            Console.WriteLine("8. Quitter");
            Console.Write("Votre choix : ");

            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    gestionnaire.AjouterTache();
                    break;
                case "2":
                    gestionnaire.AfficherTaches();
                    break;
                case "3":
                    gestionnaire.MarquerTacheTerminee();
                    break;
                case "4":
                    gestionnaire.SupprimerTache();
                    break;
                case "5":
                    gestionnaire.FiltrerTachesParFrequence();
                    break;
                case "6":
                    gestionnaire.AttribuerTache();
                    break;
                case "7":
                    gestionnaire.AfficherTachesDunOuvrier();
                    break;
                case "8":
                    quitter = true;
                    break;
                default:
                    Console.WriteLine("Choix invalide.");
                    break;
            }

            if (!quitter)
            {
                Console.WriteLine("Appuie sur une touche pour continuer...");
                Console.ReadKey();
            }
        }
    }
}
