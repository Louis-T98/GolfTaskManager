using System;

namespace GolfTaskManager;

public class TacheAnnuelle : Tache
{
    private string periode;

    public TacheAnnuelle(string titre, string description, int priorite, string periode) 
        : base(titre, description, priorite, DateTime.Now)
    {
        this.periode = periode;
    }

    public override void Executer() { throw new NotImplementedException(); }
    public override string AfficherFrequence() { throw new NotImplementedException(); return "Annuelle"; }
}