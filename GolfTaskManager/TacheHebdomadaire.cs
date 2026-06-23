using System;

namespace GolfTaskManager;

public class TacheHebdomadaire : Tache
{
    private int jourSemaine;

    public TacheHebdomadaire(string titre, string description, int priorite, int jourSemaine) 
        : base(titre, description, priorite, DateTime.Now)
    {
        this.jourSemaine = jourSemaine;
    }

    public override void Executer()
    {
        MarquerCommeFaite();
    }

    public override string AfficherFrequence()
    {
        return "Hebdomadaire";
    }
}