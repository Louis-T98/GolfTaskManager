using System;

namespace GolfTaskManager;

public class ZoneTerrain
{
    private string nom;
    private string typeZone;
    private double superficie;

    public ZoneTerrain(string nom, string typeZone, double superficie)
    {
        this.nom = nom;
        this.typeZone = typeZone;
        this.superficie = superficie;
    }

    public void AffecterTache() { throw new NotImplementedException(); }
}