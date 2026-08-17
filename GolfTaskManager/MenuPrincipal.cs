using System;
using System.Linq;
using Spectre.Console;

namespace GolfTaskManager;

public class MenuPrincipal
{
    private readonly GestionnaireTaches _gestionnaire;
    private readonly ChefEquipe _chefParDefaut;

    public MenuPrincipal()
    {
        _gestionnaire = new GestionnaireTaches();
        // On crée un chef d'équipe par défaut pour l'utiliser dans l'application
        _chefParDefaut = new ChefEquipe("Dupont", "Jean", "Admin");
    }

    public void Afficher()
    {
        bool quitter = false;

        while (!quitter)
        {
            Console.Clear();
            AnsiConsole.Write(new Rule("[green bold]=== GolfTaskManager ===[/]") { Justification = Justify.Left });
            AnsiConsole.WriteLine();

            var choix = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Sélectionnez une action :")
                    .PageSize(12)
                    .AddChoices(new[] {
                        "1. Ajouter une tâche",
                        "2. Afficher toutes les tâches",
                        "3. Marquer une tâche comme terminée",
                        "4. Supprimer une tâche",
                        "5. Filtrer les tâches par fréquence",
                        "6. Attribuer une tâche (via le Chef d'équipe)",
                        "7. Voir le planning d'un ouvrier",
                        "8. Afficher le récapitulatif par fréquence",
                        "9. Quitter"
                    }));

            // En isolant le numéro avant le point, on évite les conflits entre 1 et 11
            string numeroOption = choix.Split('.')[0].Trim();

            switch (numeroOption)
            {
                case "1":
                    SaisieAjouterTache();
                    break;
                case "2":
                    _gestionnaire.AfficherTaches();
                    break;
                case "3":
                    SaisieMarquerTerminee();
                    break;
                case "4":
                    SaisieSupprimerTache();
                    break;
                case "5":
                    SaisieFiltrerFrequence();
                    break;
                case "6":
                    SaisieAttribuerTache();
                    break;
                case "7":
                    SaisiePlanningOuvrier();
                    break;
                case "8":
                    _gestionnaire.AfficherTachesParFrequence();
                    break;
                case "9":
                    quitter = true;
                    break;
            }

            if (!quitter)
            {
                AnsiConsole.WriteLine();
                AnsiConsole.MarkupLine("[grey]Appuyez sur une touche pour continuer...[/]");
                Console.ReadKey(true);
            }
        }
    }

    private void SaisieAjouterTache()
    {
        AnsiConsole.MarkupLine("[bold green]=== Ajouter une nouvelle tâche ===[/]");
        string titre = AnsiConsole.Ask<string>("Nom de la tâche : ");
        string description = AnsiConsole.Ask<string>("Description : ");
        int priorite = AnsiConsole.Ask<int>("Priorité (Nombre entier) : ");

        string typeSaisie = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Fréquence de la tâche :")
                .AddChoices("1 (Jour)", "2 (Semaine)", "3 (Mois)", "4 (Année)")
        ).Substring(0, 1);

        _gestionnaire.CreerEtAjouterTache(typeSaisie, titre, description, priorite);
        AnsiConsole.MarkupLine("[green]✓ Tâche ajoutée avec succès.[/]");
    }

    private void SaisieMarquerTerminee()
    {
        var enCours = _gestionnaire.ObtenirTachesEnCours();
        if (!enCours.Any())
        {
            AnsiConsole.MarkupLine("[yellow]Aucune tâche en cours à terminer.[/]");
            return;
        }

        var tacheChoisie = AnsiConsole.Prompt(
            new SelectionPrompt<Tache>()
                .Title("Sélectionnez la tâche à [green]terminer[/] :")
                .UseConverter(t => $"{t.Titre} (Priorité: {t.Priorite})")
                .AddChoices(enCours)
        );

        // On utilise la méthode de ChefEquipe pour valider l'action métier !
        _chefParDefaut.ValiderTravail(tacheChoisie);
        AnsiConsole.MarkupLine($"[green]✓ La tâche '{tacheChoisie.Titre}' est validée comme terminée par le chef d'équipe.[/]");
    }

    private void SaisieSupprimerTache()
    {
        var toutes = _gestionnaire.ObtenirToutesLesTaches();
        if (!toutes.Any())
        {
            AnsiConsole.MarkupLine("[yellow]Aucune tâche à supprimer.[/]");
            return;
        }

        var tacheChoisie = AnsiConsole.Prompt(
            new SelectionPrompt<Tache>()
                .Title("[red]Sélectionnez la tâche à supprimer :[/]")
                .UseConverter(t => $"{t.Titre} [{t.Statut}]".EscapeMarkup())
                .AddChoices(toutes)
        );

        _gestionnaire.SupprimerTache(tacheChoisie);
        AnsiConsole.MarkupLine($"[red]✓ La tâche '{tacheChoisie.Titre.EscapeMarkup()}' a été supprimée.[/]");
    }

    private void SaisieFiltrerFrequence()
    {
        var freq = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choisis une [green]fréquence[/] :")
                .AddChoices("Journalière", "Hebdomadaire", "Mensuelle", "Annuelle")
        );

        _gestionnaire.AfficherTachesFiltrees(freq);
    }

    private void SaisieAttribuerTache()
    {
        var toutes = _gestionnaire.ObtenirToutesLesTaches();
        if (!toutes.Any())
        {
            AnsiConsole.MarkupLine("[red]Aucune tâche disponible pour attribution.[/]");
            return;
        }

        var tache = AnsiConsole.Prompt(
            new SelectionPrompt<Tache>().Title("Choisis la [green]tâche[/] :").UseConverter(t => t.Titre).AddChoices(toutes)
        );
        var ouvrier = AnsiConsole.Prompt(
            new SelectionPrompt<Ouvrier>().Title("Choisis l'[green]ouvrier[/] :").UseConverter(o => o.Prenom).AddChoices(_gestionnaire.Ouvriers)
        );
        var zone = AnsiConsole.Prompt(
            new SelectionPrompt<ZoneTerrain>().Title("Choisis la [green]zone[/] :").UseConverter(z => z.Nom).AddChoices(_gestionnaire.Zones)
        );
        string heure = AnsiConsole.Ask<string>("À quelle [green]heure[/] ? (ex: 09:00) :");

        // Utilisation de la logique métier de la classe ChefEquipe !
        _chefParDefaut.AssignerTache(tache, ouvrier, zone, heure);
        AnsiConsole.MarkupLine($"[green]✓ Tâche '{tache.Titre}' attribuée par {_chefParDefaut.Prenom} à {ouvrier.Prenom}.[/]");
    }

    private void SaisiePlanningOuvrier()
    {
        var ouvrier = AnsiConsole.Prompt(
            new SelectionPrompt<Ouvrier>().Title("Sélectionnez un [green]ouvrier[/] :").UseConverter(o => o.Prenom).AddChoices(_gestionnaire.Ouvriers)
        );

        // Appel direct à la fonctionnalité de ChefEquipe pour générer le visuel du planning
        _chefParDefaut.VoirPlanningJournee(_gestionnaire.ObtenirToutesLesTaches(), ouvrier);
    }
}
