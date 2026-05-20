using System;
using System.Collections.Generic;
using System.Linq;
using Spectre.Console;

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
            AnsiConsole.MarkupLine("[red]Aucune tâche.[/]");
            return;
        }

        var table = new Table().RoundedBorder().BorderColor(Color.Green);
        table.AddColumn("[yellow]Titre[/]");
        table.AddColumn("[yellow]Description[/]");
        table.AddColumn("[yellow]Priorité[/]");
        table.AddColumn("[yellow]Fréquence[/]");
        table.AddColumn("[yellow]Statut[/]");

        foreach (Tache t in taches)
        {
            table.AddRow(
                t.Titre,
                t.Description,
                t.Priorite.ToString(),
                t.AfficherFrequence(),
                t.Statut
            );
        }

        AnsiConsole.Write(table);
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

    public void SupprimerTache()
    {
        if (taches.Count == 0)
        {
            Console.WriteLine("Aucune tâche à supprimer.");
            return;
        }

        Console.WriteLine("Liste des tâches :");
        for (int i = 0; i < taches.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {taches[i].Titre} - {taches[i].Statut}");
        }

        Console.Write("Choisis le numéro de la tâche à supprimer : ");
        string saisie = Console.ReadLine();

        if (int.TryParse(saisie, out int numero) && numero >= 1 && numero <= taches.Count)
        {
            Tache tacheASupprimer = taches[numero - 1];
            taches.RemoveAt(numero - 1);
            Console.WriteLine($"La tâche '{tacheASupprimer.Titre}' a été supprimée.");
        }
        else
        {
            Console.WriteLine("Numéro invalide.");
        }
    }

    public void FiltrerTachesParFrequence()
    {
        if (taches.Count == 0)
        {
            Console.WriteLine("Aucune tâche à filtrer.");
            return;
        }

        Console.WriteLine("Choisis une fréquence :");
        Console.WriteLine("1. Journalière");
        Console.WriteLine("2. Hebdomadaire");
        Console.WriteLine("3. Mensuelle");
        Console.WriteLine("4. Annuelle");
        Console.Write("Votre choix : ");

        string choix = Console.ReadLine();
        string frequenceRecherchee = "";

        switch (choix)
        {
            case "1":
                frequenceRecherchee = "Journalière";
                break;
            case "2":
                frequenceRecherchee = "Hebdomadaire";
                break;
            case "3":
                frequenceRecherchee = "Mensuelle";
                break;
            case "4":
                frequenceRecherchee = "Annuelle";
                break;
            default:
                Console.WriteLine("Choix invalide.");
                return;
        }

        var tachesFiltrees = taches.Where(t => t.AfficherFrequence() == frequenceRecherchee).ToList();

        if (tachesFiltrees.Count == 0)
        {
            Console.WriteLine("Aucune tâche trouvée pour cette fréquence.");
            return;
        }

        Console.WriteLine($"Tâches {frequenceRecherchee} :");
        foreach (Tache t in tachesFiltrees)
        {
            Console.WriteLine("-------------------");
            Console.WriteLine($"Titre      : {t.Titre}");
            Console.WriteLine($"Description: {t.Description}");
            Console.WriteLine($"Priorité   : {t.Priorite}");
            Console.WriteLine($"Statut     : {t.Statut}");
        }
    }
}
