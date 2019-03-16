﻿Feature: Calcul de balance pour une période
	En tant qu'utilisateur
    Je veux que la période me calcul la balance en fonction de mes opérations
    Afin de connaitre quelle somme est dûe lors d'une période

Scenario: Calcul de la somme dûe en fonction des dépenses
	Given Une période est créée	
	When J'ajoute à la période les dépenses suivantes
    | Montant | Libelle | Binome   | Type   |
    | 100     | Courses | Aurelien | Commun |
    | 200     | Pret    | Aurelien | Avance |
    | 50      | Cadeaux | Marie    | Avance |
    | 300     | Leclerc | Marie    | Commun |
    | 700     | Nounou  | Marie    | Commun |
	Then le binome Aurelien doit la somme de 300 euros

Scenario: Calcul de la somme dûe en fonction des recettes
	Given Une période est créée	
	When J'ajoute à la période les recettes suivantes
    | Montant | Libelle | Binome   | Type      |
    | 100     | Courses | Aurelien | Partielle |
    | 200     | Pret    | Aurelien | Totale    |
    | 50      | Cadeaux | Marie    | Totale    |
    | 300     | Leclerc | Marie    | Partielle |
    | 700     | Nounou  | Marie    | Partielle |
	Then le binome Marie doit la somme de 300 euros


#Scenario: Calcul de la somme dûe en fonction des dépenses et recettes
