using System;

namespace GolfTaskManager;

public class ZoneTerrain
{
    private string nom;
    private string typeZone;
    private double superficie;

    public string Nom 
    { 
        get { return nom; } 
    }

    public string TypeZone
    {
        get { return typeZone; }
    }

    public double Superficie
    {
        get { return superficie; }
    }

    public ZoneTerrain(string nom, string typeZone, double superficie)
    {
        this.nom = nom;
        this.typeZone = typeZone;
        this.superficie = superficie;
    }

    public void AffecterTache() { Console.WriteLine($"La tâche a été affectée à la zone {nom}."); }
}