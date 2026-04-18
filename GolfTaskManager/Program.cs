// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Golf Task Manager - TEST SQUELETTE ===");
        
        // Test TOUS les constructeurs (classe par classe)
        TestTacheJournaliere();
        TestTacheHebdomadaire();
        TestTacheMensuelle();
        TestTacheAnnuelle();
        TestOuvrier();
        TestChefEquipe();
        TestEquipe();
        TestPlanning();
        TestZoneTerrain();
        
        Console.WriteLine("\n🎯 TOUT FONCTIONNE PARFAITEMENT !");
        Console.WriteLine("Appuyez sur Entrée...");
        Console.ReadLine();
    }
    
    static void TestTacheJournaliere()
    {
        var tache = new TacheJournaliere("Tonte greens", "3mm", 1, DateTime.Now);
        Console.WriteLine($"✅ TacheJournaliere: {tache.Titre}");
    }
    
    static void TestTacheHebdomadaire()
    {
        var tache = new TacheHebdomadaire("Fertilisation", "NPK", 2, 1);
        Console.WriteLine($"✅ TacheHebdomadaire: {tache.Titre}");
    }
    
    static void TestTacheMensuelle()
    {
        var tache = new TacheMensuelle("Arrosage", "Vérif", 3, 5);
        Console.WriteLine($"✅ TacheMensuelle: {tache.Titre}");
    }
    
    static void TestTacheAnnuelle()
    {
        var tache = new TacheAnnuelle("Bunkers", "Sable", 1, "Printemps");
        Console.WriteLine($"✅ TacheAnnuelle: {tache.Titre}");
    }
    
    static void TestOuvrier()
    {
        var ouvrier = new Ouvrier("Dupont", "Jean", "Greenkeeper");
        Console.WriteLine($"✅ Ouvrier: {ouvrier.Nom} {ouvrier.Prenom}");
    }
    
    static void TestChefEquipe()
    {
        var chef = new ChefEquipe("Martin", "Paul", "Niveau 3");
        Console.WriteLine($"✅ ChefEquipe: {chef.Nom}");
    }
    
    static void TestEquipe()
    {
        var equipe = new Equipe("Greens Team");
        Console.WriteLine($"✅ Equipe: {equipe.Nom}");
    }
    
    static void TestPlanning()
    {
        var planning = new Planning(DateTime.Now, DateTime.Now.AddDays(7));
        Console.WriteLine($"✅ Planning OK");
    }
    
    static void TestZoneTerrain()
    {
        var zone = new ZoneTerrain("Green 1", "Green", 500.0);
        Console.WriteLine($"✅ ZoneTerrain: {zone.Nom}");
    }
}