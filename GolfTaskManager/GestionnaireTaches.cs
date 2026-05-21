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
            AnsiConsole.MarkupLine("[red]Aucune tâche à filtrer.[/]");
            return;
        }

        var frequence = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choisis une [green]fréquence[/] :")
                .AddChoices("Journalière", "Hebdomadaire", "Mensuelle", "Annuelle")
        );

        var tachesFiltrees = taches
            .Where(t => t.AfficherFrequence() == frequence)
            .ToList();

        if (tachesFiltrees.Count == 0)
        {
            AnsiConsole.MarkupLine($"[yellow]Aucune tâche trouvée pour la fréquence {frequence}.[/]");
            return;
        }

        AnsiConsole.MarkupLine($"[bold green]Tâches {frequence}[/]");
        var table = new Table().RoundedBorder().BorderColor(Color.Green);

        table.AddColumn("[yellow]Titre[/]");
        table.AddColumn("[yellow]Description[/]");
        table.AddColumn("[yellow]Priorité[/]");
        table.AddColumn("[yellow]Statut[/]");

        foreach (Tache t in tachesFiltrees)
        {
            string statutCouleur = t.Statut == "Terminée"
                ? "[green]Terminée[/]"
                : "[orange1]En attente[/]";

            table.AddRow(
                t.Titre,
                t.Description,
                t.Priorite.ToString(),
                statutCouleur
            );
        }

        AnsiConsole.Write(table);
    }
}
