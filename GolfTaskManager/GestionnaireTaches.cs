using System;
using System.Collections.Generic;

namespace GolfTaskManager;

public class GestionnaireTaches
{
    private List<Tache> taches;

    public GestionnaireTaches()
    {
        taches = new List<Tache>();
    }

    public void AjouterTache()
    {
        Console.WriteLine("Nom de la tâche : ");
        string titre = Console.ReadLine();

        Console.WriteLine("Description : ");
        string description = Console.ReadLine();

        Console.WriteLine("Priorité : ");
        int priorite = int.Parse(Console.ReadLine());

        Console.WriteLine("Type de tâche (1=jour, 2=semaine, 3=mois, 4=année) : ");
        string type = Console.ReadLine();

        Tache nouvelleTache = TacheFactory.CreerTache(type, titre, description, priorite);
        taches.Add(nouvelleTache);

        Console.WriteLine("Tâche ajoutée.");
    }

    public void AfficherTaches()
    {
        if (taches.Count == 0)
        {
            Console.WriteLine("Aucune tâche.");
            return;
        }

        foreach (Tache t in taches)
        {
            Console.WriteLine($"{t.Titre} - {t.Description} - {t.Priorite} - {t.AfficherFrequence()} - {t.Statut}");
        }
    }

    
    public void MarquerTacheTerminee()
    {
        if (taches.Count == 0)
        {
            Console.WriteLine("Aucune tâche à modifier.");
            return;
        }

        Console.WriteLine("Liste des tâches :");
        for (int i = 0; i < taches.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {taches[i].Titre} - {taches[i].Statut}");
        }

        Console.Write("Choisis le numéro de la tâche à terminer : ");
        string saisie = Console.ReadLine();

        if (int.TryParse(saisie, out int numero) && numero >= 1 && numero <= taches.Count)
        {
            Tache tacheChoisie = taches[numero - 1];
            tacheChoisie.Statut = "Terminée";
            Console.WriteLine($"La tâche '{tacheChoisie.Titre}' est maintenant terminée.");
        }
        else
        {
            Console.WriteLine("Numéro invalide.");
        }
    }
}
