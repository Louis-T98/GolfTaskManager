using System;
using System.Linq;
using Spectre.Console;

namespace GolfTaskManager;

public class ChefEquipe : Ouvrier
{
    private string niveauAcces;

    public ChefEquipe(string nom, string prenom, string niveauAcces) 
        : base(nom, prenom, "Chef d'équipe")
    {
        this.niveauAcces = niveauAcces;
    }

    public void VoirTachesEquipe( List<Tache> taches )
    {
        var tachesAttribuees = taches
            .Where(t => t.AssigneA != null)
            .ToList();

        if (tachesAttribuees.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]Aucune tâche attribuée à l'équipe.[/]");
            return;
        }

        var table = new Table().RoundedBorder().BorderColor(Color.Green);
        table.AddColumn("[yellow]Titre[/]");
        table.AddColumn("[yellow]Ouvrier[/]");
        table.AddColumn("[yellow]Zone[/]");
        table.AddColumn("[yellow]Heure[/]");
        table.AddColumn("[yellow]Statut[/]");

        foreach (var t in tachesAttribuees)
        {
            string ouvrier = t.AssigneA.Prenom;
            string zone = t.Zone != null ? t.Zone.Nom : "Non attribuée";
            string heure = string.IsNullOrWhiteSpace(t.HeurePrevue) ? "Non définie" : t.HeurePrevue;

            table.AddRow(t.Titre, ouvrier, zone, heure, t.Statut);
        }

        AnsiConsole.Write(table);
    }    
    

    public void AssignerTache( Tache tache, Ouvrier ouvrier, ZoneTerrain zone, string heure ) 
    { 
        tache.AssigneA = ouvrier;
        tache.Zone = zone;
        tache.HeurePrevue = heure; 
    }
    public void ValiderTravail( Tache tache ) 
    {
        tache.Statut = "Terminée"; 
    }

    public void VoirPlanningJournee(List<Tache> taches, Ouvrier ouvrier)
    {
        var planning = taches
        .Where(t => t.AssigneA == ouvrier)
        .OrderBy(t => t.HeurePrevue)
        .ToList();

        if (planning.Count == 0)
        {
            AnsiConsole.MarkupLine($"[yellow]Aucune tâche pour {ouvrier.Prenom}.[/]");
            return;
        }

        var table = new Table().RoundedBorder().BorderColor(Color.Blue);
        table.AddColumn("[yellow]Heure[/]");
        table.AddColumn("[yellow]Titre[/]");
        table.AddColumn("[yellow]Zone[/]");
        table.AddColumn("[yellow]Statut[/]");

        foreach (var t in planning)
        {
            string zone = t.Zone != null ? t.Zone.Nom : "Non attribuée";
            string heure = string.IsNullOrWhiteSpace(t.HeurePrevue) ? "Non définie" : t.HeurePrevue;

            table.AddRow(heure, t.Titre, zone, t.Statut);
        }

        AnsiConsole.Write(table);
    }
}