using System;

namespace GolfTaskManager;

public abstract class Tache
{
    private string titre;
    private string description;
    private int priorite;
    private string statut;
    private DateTime datePrevue;

    public Tache(string titre, string description, int priorite, DateTime datePrevue)
    {
        this.titre = titre;
        this.description = description;
        this.priorite = priorite;
        this.statut = "En attente";
        this.datePrevue = datePrevue;
    }

    public string Titre { get => titre; }
    public string Description { get => description; }
    public int Priorite { get => priorite; }
    public string Statut { get => statut; set => statut = value; }
    public DateTime DatePrevue { get => datePrevue; }

    public abstract void Executer();
    public abstract string AfficherFrequence();
    public virtual void MarquerCommeFaite()
    {
        throw new NotImplementedException();
    }
}