using System;

namespace GolfTaskManager;

public class ChefEquipe : Ouvrier
{
    private string niveauAcces;

    public ChefEquipe(string nom, string prenom, string niveauAcces) 
        : base(nom, prenom, "Chef d'équipe")
    {
        this.niveauAcces = niveauAcces;
    }

    public void AssignerTache() { throw new NotImplementedException(); }
    public void ValiderTravail() { throw new NotImplementedException(); }
}