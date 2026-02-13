# Tic-tac-toe-repositery


**Auteur : Philippe Beaulieu**

------------------------------------------------------------------------

##  Description brève

Tic-tac-toe-repositery est une application de Tic-Tac-Toe en réalité
augmentée (AR) développée avec Unity et AR Foundation.

Le projet permet à l'utilisateur de :

-   Détecter des surfaces planes dans l'environnement réel
-   Placer un plateau de Tic-Tac-Toe en AR
-   Jouer une partie complète à deux joueurs (X et O)
-   Détecter automatiquement une victoire ou un match nul
-   Afficher dynamiquement le gagnant via une interface utilisateur

Le projet combine : Raycasting AR pour le placement du plateau -
Raycasting 3D classique pour l'interaction avec les cellules - Gestion
complète de la logique du jeu en C#

------------------------------------------------------------------------

##  Versions Unity et packages utilisés

###  Unity

-   Unity 6.3 LTS (6000.3.5f1)

###  Packages principaux

-   AR Foundation 5.0+
-   Google ARCore XR Plugin 5.0+
-   XR Plug-in Management
-   Input System (New Input System)
-   TextMeshPro

------------------------------------------------------------------------

## Fonctionnement du projet

###  Placement du plateau en AR

Le placement du plateau est effectué grâce à un raycast AR :

    raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon);

Ce raycast permet de détecter une surface plane valide dans
l'environnement réel et d'instancier le plateau à cette position.

------------------------------------------------------------------------

###  Interaction avec les cellules

Les interactions avec les cases utilisent un raycast 3D classique :

    Ray ray = cam.ScreenPointToRay(screenPos);
    Physics.Raycast(ray, out RaycastHit hit);

Cela permet de détecter quelle cellule est sélectionnée par
l'utilisateur.

------------------------------------------------------------------------

###  Logique du jeu

-   Tableau logique : int\[8\]
-   Gestion des tours X / O
-   Vérification des 8 combinaisons gagnantes
-   Détection automatique du match nul
-   Interface utilisateur dynamique avec TextMeshPro

------------------------------------------------------------------------

##  Défis rencontrés et solutions

### 1️ Le plateau ne s'affichait pas

Problème : Le boardPrefab n'était pas assigné dans l'Inspector.

Solution : - Création d'un prefab valide dans le dossier Assets -
Assignation du prefab dans ARPlacementManager

------------------------------------------------------------------------

### 2️ Le raycast AR ne détectait aucun plan

Problème : Raycast retournait toujours false.

Solution : - Vérification de la présence de ARPlaneManager -
Vérification que ARPlacementManager est sur le même GameObject que
ARRaycastManager - Activation de XR Simulation pour les tests en éditeur

------------------------------------------------------------------------

### 3️ Conflit entre placement AR et interaction de jeu

Problème : Le raycast AR pouvait interférer avec le raycast des
cellules.

Solution : - Autoriser le placement uniquement avant instanciation -
Bloquer le déplacement après le premier placement

------------------------------------------------------------------------

##  Améliorations prévues

-   Ajouter un environnement immersif (ex : chambre en XR Simulation)
-   Permettre le déplacement libre du plateau
-   Ajouter une animation de victoire avec une ligne sur les trois
    cellules gagnantes

------------------------------------------------------------------------

## Annexe : Requêtes utilisées pour écrire du code : IA

-   Comment faire un raycast AR avec AR Foundation
-   ScreenPointToRay Unity explication
-   Physics.Raycast exemple Unity
-   Comment détecter une victoire Tic Tac Toe en C#
-   Comment utiliser TrackableType.PlaneWithinPolygon
-   Comment empêcher NullReferenceException Unity
-   Comment configurer XR Simulation Unity
-   Comment utiliser Input System Touchscreen.current
-   Comment instancier un prefab en Unity
-   la logique du jeux tic-tac-toe en c#

------------------------------------------------------------------------

## Plateformes ciblées

-   Android (ARCore)
-   Simulation XR dans Unity Editor

------------------------------------------------------------------------

## Conclusion

Ce projet démontre l'intégration : - De la logique d'un jeu classique -
Du raycasting 3D - Du raycasting AR - De la gestion d'état - De
l'interface utilisateur dynamique

Il met en pratique les concepts fondamentaux de la réalité augmentée
avec Unity et AR Foundation.

