using System;
using System.Collections.Generic;
using System.Linq;
using Spectre.Console;

namespace GolfTaskManager;

public class GestionnaireTaches
{
    private readonly List<Tache> _taches;
    private readonly List<Ouvrier> _ouvriers;
    private readonly List<ZoneTerrain> _zones;

    // Encapsulation propre pour que le menu puisse lire les choix possibles sans modifier les listes
    public List<Ouvrier> Ouvriers => _ouvriers;
    public List<ZoneTerrain> Zones => _zones;

    public GestionnaireTaches()
    {
        _taches = new List<Tache>();

        _ouvriers = new List<Ouvrier>
        {
            new Ouvrier("Bob", "Bob", "Ouvrier"),
            new Ouvrier("John", "John", "Ouvrier"),
            new Ouvrier("Michel", "Michel", "Ouvrier")
        };

        _zones = new List<ZoneTerrain>
        {
            new ZoneTerrain("Parc", "Zone verte", 1.0),
            new ZoneTerrain("Grand Terrain", "Terrain principal", 5.0),
            new ZoneTerrain("Petit Terrain", "Terrain secondaire", 2.0),
            new ZoneTerrain("Practice", "Zone d'entraînement", 3.0)
        };
    }

    public void CreerEtAjouterTache(string type, string titre, string description, int priorite)
    {
        Tache nouvelleTache = TacheFactory.CreerTache(type, titre, description, priorite);
        _taches.Add(nouvelleTache);
    }

    public List<Tache> ObtenirTachesEnCours() => _taches.Where(t => t.Statut != "Terminée").ToList();
    public List<Tache> ObtenirToutesLesTaches() => _taches;

    public void SupprimerTache(Tache tache) => _taches.Remove(tache);

    public void AfficherTaches()
    {
        if (_taches.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]Aucune tâche enregistrée.[/]");
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

        foreach (Tache t in _taches)
        {
            string ouvrier = t.AssigneA != null ? t.AssigneA.Prenom : "[grey]Non attribuée[/]";
            string zone = t.Zone != null ? t.Zone.Nom : "[grey]Non attribuée[/]";
            string statutCouleur = t.Statut == "Terminée" ? "[green]Terminée[/]" : "[orange1]En attente[/]";

            table.AddRow(t.Titre, t.Description, t.Priorite.ToString(), t.AfficherFrequence(), statutCouleur, ouvrier, zone);
        }

        AnsiConsole.Write(table);
    }

    public void AfficherTachesFiltrees(string frequence)
    {
        var tachesFiltrees = _taches.Where(t => t.AfficherFrequence() == frequence).ToList();

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
            string statutCouleur = t.Statut == "Terminée" ? "[green]Terminée[/]" : "[orange1]En attente[/]";
            table.AddRow(t.Titre, t.Description, t.Priorite.ToString(), statutCouleur);
        }

        AnsiConsole.Write(table);
    }

    public void AfficherTachesParFrequence()
    {
        if (_taches.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]Aucune tâche enregistrée.[/]");
            return;
        }

        string[] ordre = { "Journalière", "Hebdomadaire", "Mensuelle", "Annuelle" };

        foreach (string frequence in ordre)
        {
            var tachesFrequence = _taches.Where(t => t.AfficherFrequence() == frequence).ToList();
            if (tachesFrequence.Count == 0) continue;

            AnsiConsole.MarkupLine($"[bold blue]===== TACHES {frequence.ToUpper()} =====[/]");
            foreach (var tache in tachesFrequence)
            {
                AnsiConsole.MarkupLine($"- {tache.Titre} ([grey]{tache.Statut}[/])");
            }
            AnsiConsole.WriteLine();
        }
    }
}