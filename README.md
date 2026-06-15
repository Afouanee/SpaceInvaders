# Space Invaders 👾

> **Clone de Space Invaders en C# / .NET WinForms — projet de Programmation Orientée Objet (POO) à l'ESIEE.**

![C#](https://img.shields.io/badge/C%23-14b8a6?style=flat-square)
![Type](https://img.shields.io/badge/ESIEE-555?style=flat-square)
[![Portfolio](https://img.shields.io/badge/Portfolio-afouanee.dev-14b8a6?style=flat-square)](https://afouanee.dev/projects/space-invaders)

## ✨ Aperçu
Projet ESIEE de Programmation Orientée Objet : un clone du célèbre Space Invaders développé en C# avec Windows Forms. Le projet met en avant une conception orientée objet soignée — hiérarchie de classes, patron singleton, gestion d'état de jeu — appliquée à un jeu vidéo classique avec rendu, collisions, sons et ressources embarquées.

## 🚀 Fonctionnalités
- **Boucle de jeu complète** avec machine à états (`enum GameState { Play, Pause, Win, Lost }`).
- **Vaisseau joueur** (`PlayerSpaceShip`), bloc d'ennemis (`EnemyBlock`), missiles et bunkers destructibles.
- **Bonus** récupérables et gestion du son (`SoundManager`).
- **Conception POO** : hiérarchie `GameObject → SimpleObject / SpaceShip`, classe utilitaire `Vecteur2D`, gestionnaire de jeu en **singleton** (`Game`).
- **Ressources embarquées** : sprites et sons intégrés à l'exécutable.

## 🛠️ Stack technique
- **Langage** : C# (.NET Framework 4.8)
- **Bibliothèques / frameworks** : Windows Forms
- **Outils** : Visual Studio (2017+), cible x86

## ▶️ Lancer le projet
```text
Ouvrir SpaceInvaders.sln dans Visual Studio (2017+, .NET 4+) puis lancer avec F5.
Alternative : compiler avec msbuild. Windows uniquement.
```

## 📂 Structure
```
SpaceInvaders.sln   # solution Visual Studio
Program.cs          # point d'entrée : Main → Application.Run(new GameForm())
Game.cs             # singleton de jeu + machine à états
PlayerSpaceShip / EnemyBlock / Bunkers / Missiles / Bonus / SoundManager
GameObject → SimpleObject / SpaceShip   # hiérarchie d'objets
Vecteur2D           # utilitaire vectoriel
```

---
🔗 **Fiche projet** : [afouanee.dev/projects/space-invaders](https://afouanee.dev/projects/space-invaders)
👤 **Auteur** : Afouane MOUHAMAD — [Portfolio](https://afouanee.dev) · [LinkedIn](https://linkedin.com/in/afouane-mouhamad)
