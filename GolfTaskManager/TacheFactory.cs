using System;

namespace GolfTaskManager;

public static class TacheFactory
{
    public static Tache CreerTache(string type, string titre, string description, int priorite)
    {
        switch (type)
        {
            case "1":
                return new TacheJournaliere(titre, description, priorite, DateTime.Now);
            case "2":
                return new TacheHebdomadaire(titre, description, priorite, 1);
            case "3":
                return new TacheMensuelle(titre, description, priorite, 1);
            case "4":
                return new TacheAnnuelle(titre, description, priorite, "Année en cours");
            default:
                throw new ArgumentException("Type de tâche invalide");
        }
    }
}
