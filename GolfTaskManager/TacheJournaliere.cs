using System;

namespace GolfTaskManager;

public class TacheJournaliere : Tache
{
    private DateTime date;

    public TacheJournaliere(string titre, string description, int priorite, DateTime date) 
        : base(titre, description, priorite, date)
    {
        this.date = date;
    }

    public override void Executer()
    {
        throw new NotImplementedException();
    }

    public override string AfficherFrequence()
    {
        throw new NotImplementedException();
        return "Journalière";
    }
}