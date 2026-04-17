using System;

namespace GolfTaskManager;

public class Equipe
{
    private string nom;
    private List<Ouvrier> listeOuvriers = new List<Ouvrier>();

    public Equipe(string nom)
    {
        this.nom = nom;
    }

    public void AjouterOuvrier(Ouvrier ouvrier) { throw new NotImplementedException(); }
    public void RetirerOuvrier(Ouvrier ouvrier) { throw new NotImplementedException(); }
}