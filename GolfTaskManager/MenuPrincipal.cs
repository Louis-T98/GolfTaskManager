using System;

namespace GolfTaskManager;

public class MenuPrincipal
{
    private GestionnaireTaches gestionnaire;

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
            Console.WriteLine("=== GolfTaskManager ===");
            Console.WriteLine("1. Ajouter une tâche");
            Console.WriteLine("2. Afficher les tâches");
            Console.WriteLine("3. Marquer une tâche comme terminée");
            Console.WriteLine("4. Quitter");
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
