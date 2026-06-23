# GolfTaskManager

**GolfTaskManager** est une application console robuste développée en C# pour la gestion, la planification et le suivi des tâches de maintenance sur un terrain de golf. [cite_start]L'application intègre une interface utilisateur terminal fluide, colorée et sécurisée contre les erreurs de saisie grâce au framework **Spectre.Console**.

---

##  Fonctionnalités Clés

L'application intègre les fonctionnalités métier suivantes :
* **Gestion des Tâches :** Création de tâches avec titre, description, priorité et typologie de fréquence (journalière, hebdomadaire, mensuelle, annuelle).
* **Attribution Dynamique :** Assignation d'une tâche à un ouvrier spécifique, dans une zone du terrain définie (Practice, Grand Terrain, etc.) à une heure précise.
* **Suivi et Validation :** Visualisation globale de l'état des tâches et validation des travaux terminés par le Chef d'équipe.
* **Filtrage Avancé :** Tri et affichage des tâches par fréquence ou par ouvrier pour un suivi ciblé.

---

##  Architecture & Principes Orientés Objet

[cite_start]Le projet applique les piliers de la programmation orientée objet (POO) demandés[cite: 35]:
1. [cite_start]**Encapsulation :** Protection stricte des données avec des attributs privés accessibles uniquement via des propriétés ou getters dédiés[cite: 37].
2. [cite_start]**Héritage & Polymorphisme :** La classe abstraite `Tache` sert de parent commun à 4 classes spécialisées (`TacheJournaliere`, `TacheHebdomadaire`, etc.)[cite: 37]. [cite_start]La méthode abstraite `AfficherFrequence()` est redéfinie (`override`) par chaque enfant de manière polymorphique[cite: 37]. [cite_start]Une seconde hiérarchie lie `Ouvrier` à sa spécialisation `ChefEquipe`[cite: 31, 37].
3. [cite_start]**Séparation des Responsabilités (Découplage) :** L'interface utilisateur graphique est centralisée dans `MenuPrincipal`, tandis que les structures de données et la logique pure sont encapsulées dans `GestionnaireTaches` et `ChefEquipe`[cite: 46].

### Design Pattern Implémenté : Factory 

[cite_start]Pour répondre aux contraintes d'architecture, le projet intègre le **Design Pattern Factory** à travers la classe statique `TacheFactory`[cite: 31, 38]. 
* [cite_start]**Problème résolu :** Centralise l'instanciation des différents types dérivés de `Tache` sans encombrer le code de l'interface utilisateur[cite: 43].
* **Avantage :** Rend l'application extensible (principe Ouvert/Fermé du SOLID). Ajouter une nouvelle fréquence de tâche n'exige aucune modification du menu d'ajout.

---

##  Diagramme de Classes

[cite_start]L'architecture complète du logiciel est modélisée selon les conventions UML réglementaires ci-dessous:

```text
       ┌──────────────────────────────────────────┐
       │                  Tache                   │ ← Classe Abstraite
       ├──────────────────────────────────────────┤
       │ - titre: string                          │
       │ - description: string                    │
       │ - priorite: int                          │       ┌───────────────┐
       │ - statut: string                         │       │  ZoneTerrain  │
       │ - datePrevue: DateTime                   │       ├───────────────┤
       │ + AssigneA: Ouvrier?   ──────────────────┼─────> │ - nom: string │
       │ + Zone: ZoneTerrain?   ──────────────────┼─────> │ - type: string│
       │ + HeurePrevue: string?                   │       │ - superf: dbl │
       ├──────────────────────────────────────────┤       └───────────────┘
       │ + Executer(): void       {abstract}      │
       │ + AfficherFrequence(): string {abstract} │
       └────────────────────┬─────────────────────┘
                            ▲
 ┌──────────────────┬───────┴──────────┬──────────────────┐
 │                  │                  │                  │
┌┴─────────────────┐┌┴────────────────┐┌┴────────────────┐┌┴────────────────┐
│ TacheJournaliere ││TacheHebdomadaire││ TacheMensuelle  ││  TacheAnnuelle  │
├──────────────────┤├─────────────────┤├─────────────────┤├─────────────────┤
│ - date: DateTime ││- jourSemaine:int││ - mois: int     ││- periode:string │
├──────────────────┤├─────────────────┤├─────────────────┤├─────────────────┤
│ + Executer()     ││ + Executer()    ││ + Executer()    ││ + Executer()    │
│ + AfficherFreq() ││ + AfficherFreq()││ + AfficherFreq()││ + AfficherFreq()│
└──────────────────┘└─────────────────┘└─────────────────┘└─────────────────┘

       ┌──────────────────────────┐               ┌──────────────────────────┐
       │         Ouvrier          │ <──────────── │          Equipe          │
       ├──────────────────────────┤               ├──────────────────────────┤
       │ - nom: string            │               │ - nom: string            │
       │ - prenom: string         │               │ - listeOuvriers: List    │
       │ - role: string           │               └──────────────────────────┘
       ├──────────────────────────┤
       │ + Travailler(): void     │               ┌──────────────────────────┐
       │ + TerminerTache(): void  │               │         Planning         │
       └────────────▲─────────────┘               ├──────────────────────────┤
                    │                             │ - dateDebut: DateTime    │
       ┌────────────┴─────────────┐               │ - dateFin: DateTime      │
       │       ChefEquipe         │               │ - listeTaches: List   ───┼─┐
       ├──────────────────────────┤               └──────────────────────────┘ │
       │ - niveauAcces: string    │                                            │
       ├──────────────────────────┤                                            │
       │ + AssignerTache()        │ <──────────────────────────────────────────┘
       │ + ValiderTravail()       │
       └──────────────────────────┘

 ┌──────────────────────────────────────────────────────────────────────────┐
 │                            GestionnaireTaches                            │
 ├──────────────────────────────────────────────────────────────────────────┤
 │ - _taches: List<Tache>                                                   │
 │ - _ouvriers: List<Ouvrier>                                               │
 │ - _zones: List<ZoneTerrain>                                              │
 ├──────────────────────────────────────────────────────────────────────────┤
 │ + CreerEtAjouterTache(type: string, titre: string, ...) void             │
 │ + ObtenirTachesEnCours(): List<Tache>                                    │
 │ + AfficherTaches(): void                                                 │
 └──────────────────────────────────────────────────────────────────────────┘
