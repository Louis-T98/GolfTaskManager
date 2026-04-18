using System;

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

    public void AjouterTache(Tache tache) { throw new NotImplementedException(); }
    public void AfficherPlanning() { throw new NotImplementedException(); }
}