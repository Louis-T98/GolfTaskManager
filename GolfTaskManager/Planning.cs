using System;
using System.Collections.Generic;

namespace GolfTaskManager;

public class Planning
{
    private DateTime dateDebut;
    private DateTime dateFin;
    private List<Tache> listeTaches = new List<Tache>();

    public Planning(DateTime debut, DateTime fin)
    {
        dateDebut = debut;
        dateFin = fin;
    }

    public void AjouterTache(Tache tache)
    {
        if (tache != null)
        {
            listeTaches.Add(tache);
            Console.WriteLine($"La tâche '{tache.Titre}' a été ajoutée au planning.");
        }
    }
    public void AfficherPlanning()
    {
        Console.WriteLine($"Planning du {dateDebut:dd/MM/yyyy} au {dateFin:dd/MM/yyyy}");

        if (listeTaches.Count == 0)
        {
            Console.WriteLine("Aucune tâche dans le planning.");
            return;
        }

        foreach (var tache in listeTaches)
        {
            Console.WriteLine($"- {tache.Titre} | {tache.AfficherFrequence()} | {tache.Statut}");
        }
    }
}