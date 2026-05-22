using System;
using System.Collections.Generic;
using System.Linq;
using Spectre.Console;

namespace GolfTaskManager;

public class GestionnaireTaches
{
    private List<Tache> taches;
    
    private List<Ouvrier> ouvriers;

    private List<ZoneTerrain> zones;

    public GestionnaireTaches()
    {
        taches = new List<Tache>();

        ouvriers = new List<Ouvrier>
        {
            new Ouvrier("Bob", "Bob", "Ouvrier"),
            new Ouvrier("John", "John", "Ouvrier"),
            new Ouvrier("Michel", "Michel", "Ouvrier")
        };

        zones = new List<ZoneTerrain>
        {
            new ZoneTerrain("Parc", "Zone verte", 1.0),
            new ZoneTerrain("Grand Terrain", "Terrain principal", 5.0),
            new ZoneTerrain("Petit Terrain", "Terrain secondaire", 2.0),
            new ZoneTerrain("Practice", "Zone d'entraînement", 3.0)
        };
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
        table.AddColumn("[yellow]Attribuée à[/]");
        table.AddColumn("[yellow]Zone[/]");

        foreach (Tache t in taches)
        {
            string ouvrier = t.AssigneA != null ? t.AssigneA.Prenom : "[grey]Non attribuée[/]";
            string zone = t.Zone != null ? t.Zone.Nom : "[grey]Non attribuée[/]";

            table.AddRow(
                t.Titre,
                t.Description,
                t.Priorite.ToString(),
                t.AfficherFrequence(),
                t.Statut,
                ouvrier,
                zone
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

    public void AttribuerTache()
    {
        if (taches.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Aucune tâche disponible.[/]");
            return;
        }

        var tableTaches = new Table().RoundedBorder();
        tableTaches.AddColumn("Numéro");
        tableTaches.AddColumn("Titre");

        for (int i = 0; i < taches.Count; i++)
        {
            tableTaches.AddRow((i + 1).ToString(), taches[i].Titre);
        }

        AnsiConsole.Write(tableTaches);

        int indexTache = AnsiConsole.Ask<int>("Choisis le [green]numéro de la tâche[/] :") - 1;

        if (indexTache < 0 || indexTache >= taches.Count)
        {
            AnsiConsole.MarkupLine("[red]Numéro de tâche invalide.[/]");
            return;
        }

        var tableOuvriers = new Table().RoundedBorder();
        tableOuvriers.AddColumn("Numéro");
        tableOuvriers.AddColumn("Ouvrier");

        for (int i = 0; i < ouvriers.Count; i++)
        {
            tableOuvriers.AddRow((i + 1).ToString(), ouvriers[i].Prenom);
        }

        AnsiConsole.Write(tableOuvriers);

        int indexOuvrier = AnsiConsole.Ask<int>("Choisis le [green]numéro de l'ouvrier[/] :") - 1;

        if (indexOuvrier < 0 || indexOuvrier >= ouvriers.Count)
        {
            AnsiConsole.MarkupLine("[red]Numéro d'ouvrier invalide.[/]");
            return;
        }

        var tableZones = new Table().RoundedBorder();
        tableZones.AddColumn("Numéro");
        tableZones.AddColumn("Zone");

        for (int i = 0; i < zones.Count; i++)
        {
            tableZones.AddRow((i + 1).ToString(), zones[i].Nom);
        }

        AnsiConsole.Write(tableZones);

        int indexZone = AnsiConsole.Ask<int>("Choisis le [green]numéro de la zone[/] :") - 1;

        if (indexZone < 0 || indexZone >= zones.Count)
        {
            AnsiConsole.MarkupLine("[red]Numéro de zone invalide.[/]");
            return;
        }

        taches[indexTache].AssigneA = ouvriers[indexOuvrier];
        taches[indexTache].Zone = zones[indexZone];

        string heure = AnsiConsole.Ask<string>("À quelle [green]heure[/] la tâche doit-elle être faite ? (ex: 09:00)");
        taches[indexTache].HeurePrevue = heure;

        AnsiConsole.MarkupLine($"[green]La tâche '{taches[indexTache].Titre}' a été attribuée à {ouvriers[indexOuvrier].Prenom} dans la zone {zones[indexZone].Nom}.[/]");
    }
    public void AfficherTachesDunOuvrier()
    {
        if (ouvriers.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Aucun ouvrier disponible.[/]");
            return;
        }

        var ouvrierChoisi = AnsiConsole.Prompt(
            new SelectionPrompt<Ouvrier>()
                .Title("Choisis un [green]ouvrier[/] :")
                .UseConverter(o => o.Prenom)
                .AddChoices(ouvriers)
        );

        var tachesOuvrier = taches
            .Where(t => t.AssigneA == ouvrierChoisi)
            .ToList();

        if (tachesOuvrier.Count == 0)
        {
            AnsiConsole.MarkupLine($"[yellow]Aucune tâche attribuée à {ouvrierChoisi.Prenom}.[/]");
            return;
        }

        AnsiConsole.MarkupLine($"[bold green]Tâches de {ouvrierChoisi.Prenom}[/]");

        var table = new Table().RoundedBorder().BorderColor(Color.Green);
        table.AddColumn("[yellow]Titre[/]");
        table.AddColumn("[yellow]Description[/]");
        table.AddColumn("[yellow]Priorité[/]");
        table.AddColumn("[yellow]Fréquence[/]");
        table.AddColumn("[yellow]Statut[/]");
        table.AddColumn("[yellow]Zone[/]");
        table.AddColumn("[yellow]Heure[/]");

        foreach (Tache t in tachesOuvrier)
        {
            string zone = t.Zone != null ? t.Zone.Nom : "[grey]Non attribuée[/]";
            string heure = string.IsNullOrWhiteSpace(t.HeurePrevue) ? "[grey]Non définie[/]" : t.HeurePrevue;

            table.AddRow(
                t.Titre,
                t.Description,
                t.Priorite.ToString(),
                t.AfficherFrequence(),
                t.Statut, 
                zone,
                heure
            );
        }

        AnsiConsole.Write(table);
    }
}
