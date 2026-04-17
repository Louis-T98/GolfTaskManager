using System;

namespace GolfTaskManager;

public class TacheMensuelle : Tache
{
    private int mois;

    public TacheMensuelle(string titre, string description, int priorite, int mois) 
        : base(titre, description, priorite, DateTime.Now)
    {
        this.mois = mois;
    }

    public override void Executer() { throw new NotImplementedException(); }
    public override string AfficherFrequence() { throw new NotImplementedException(); return "Mensuelle"; }
}
