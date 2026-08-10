# SpaceInvaders

Clone de Space Invaders en C# / .NET Framework 4.8 (WinForms), projet scolaire POO (ESIEE). Pas de dotnet SDK requis pour lire le code ; compiler nécessite Visual Studio avec le workload ".NET desktop development" (le `.csproj` est en ancien format MSBuild, ToolsVersion 12.0).

## Structure

- `Program.cs` — point d'entrée (`Main` → `Application.Run(new GameForm())`).
- `Form1.cs` / `Form1.Designer.cs` — `GameForm`, la fenêtre WinForms. Boucle de rendu (double buffering), timer `WorldClock` (tick toutes les 30 ms) qui appelle `Game.Update`, gestion clavier (KeyDown/KeyUp remplissent `Game.keyPressed`).
- `Game.cs` — singleton `Game` (accès via `Game.game`, création via `Game.CreateGame`). Contient la machine à états (`GameState`: Play/Pause/Win/Lost), la liste des `GameObject` actifs, et la logique d'update/draw/reset.
- `GameObject.cs` — classe abstraite racine (`Update`, `Draw`, `IsAlive`, `Collision`).
- `SimpleObject.cs` — implémente la collision pixel-perfect entre un `SimpleObject` et un `Missile`, et le rendu par sprite. Classe mère de `SpaceShip`, `Bunker`, `Missile`, `Bonus`.
- `SpaceShip.cs` → `PlayerSpaceShip.cs` — vaisseau ennemi générique et vaisseau joueur (déplacement clavier, tir).
- `EnemyBlock.cs` — bloc d'ennemis (mouvement latéral + descente, tir aléatoire, apparition de bonus, suppression des vaisseaux détruits).
- `Bunker.cs`, `Missile.cs`, `Bonus.cs` — objets de jeu spécifiques.
- `Vecteur2D.cs` — utilitaire vectoriel (opérateurs +, -, *, /).
- `SoundManager.cs` — singleton pour la musique de fond.
- `Properties/` — ressources embarquées (sprites, sons) générées par Visual Studio, ne pas éditer à la main.

## Points connus (non corrigés, hors périmètre sans compilateur disponible)

- `PlayerSpaceShip.handlekeyinput` est une méthode morte : définie mais jamais appelée (le déplacement réel passe par `PlayerSpaceShip.Update` qui lit `Game.keyPressed` directement).
- `EnemyBlock.UpdateSize` / `UpdateBlockMouvement` appellent `.Max()` sur `enemyShips.Where(s => s.IsAlive())` : lèverait une exception si appelé alors qu'aucun ennemi n'est vivant (séquence vide). En pratique semble protégé par la transition d'état vers `Win` dans `Game.CheckObjectStatus`, mais l'ordre d'appel n'est pas garanti à toute épreuve — à vérifier si un crash de fin de partie est observé.

Ne pas retenter d'installer le SDK dotnet ou Visual Studio Build Tools dans cet environnement sans demande explicite : c'est un gros téléchargement hors scope pour une simple lecture de code.
