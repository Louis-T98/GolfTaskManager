using System;
using System.Collections.Generic;

namespace GolfTaskManager;

public class Equipe
{
    private string nom;
    private List<Ouvrier> listeOuvriers = new List<Ouvrier>();

     public string Nom { get
        {
            return nom;
        } }

    public Equipe(string nom)
    {
        this.nom = nom;
    }

    public void AjouterOuvrier(Ouvrier ouvrier)
    {
        if (ouvrier != null && !listeOuvriers.Contains(ouvrier))
        {
            listeOuvriers.Add(ouvrier);
            Console.WriteLine($"{ouvrier.Prenom} a été ajouté à l'équipe {nom}.");
        }
    }
    public void RetirerOuvrier(Ouvrier ouvrier)
    {
        if (ouvrier != null && listeOuvriers.Remove(ouvrier))
        {
            Console.WriteLine($"{ouvrier.Prenom} a été retiré de l'équipe {nom}.");
        }
    }
}