using System;

namespace GolfTaskManager;

public class Ouvrier
{
    private string nom;
    private string prenom;
    private string role;

    public Ouvrier(string nom, string prenom, string role)
    {
        this.nom = nom;
        this.prenom = prenom;
        this.role = role;
    }

    public string Nom { get => nom; }
    public string Prenom { get => prenom; }
    public string Role { get => role; }

    public void Travailler() { throw new NotImplementedException(); }
    public void TerminerTache() { throw new NotImplementedException(); }
}