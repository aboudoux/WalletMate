Feature: Calcul de balance pour une période
	En tant qu'utilisateur
    Je veux que la période me calcul la balance en fonction de mes opérations
    Afin de connaitre quelle somme est dûe lors d'une période

Scenario: Calcul de la somme due en fonction des dépenses
	Given Une période est créée	
	When J'ajoute à la période les dépenses suivantes
    | Montant | Libelle | Binome   | Type   |
    | 100     | Courses | Aurelien | Commun |
    | 200     | Pret    | Aurelien | Avance |
    | 50      | Cadeaux | Marie    | Avance |
    | 300     | Leclerc | Marie    | Commun |
    | 700     | Nounou  | Marie    | Commun |
	Then le binome Aurelien doit la somme de 300 euros

Scenario: Calcul de la somme due en fonction des recettes
	Given Une période est créée	
	When J'ajoute à la période les recettes suivantes
    | Montant | Libelle | Binome   | Type      |
    | 100     | Courses | Aurelien | Partielle |
    | 200     | Pret    | Aurelien | Totale    |
    | 50      | Cadeaux | Marie    | Totale    |
    | 300     | Leclerc | Marie    | Partielle |
    | 700     | Nounou  | Marie    | Partielle |
	Then le binome Marie doit la somme de 300 euros

Scenario: Calcul de la somme due en fonction des dépenses et recettes
	Given Une période est créée	
	When J'ajoute à la période les dépenses suivantes
    | Montant | Libelle | Binome   | Type   |
    | 200     | Pret    | Aurelien | Avance |
    | 50      | Cadeaux | Marie    | Avance |
    | 300     | Leclerc | Marie    | Commun |
	And J'ajoute à la période les recettes suivantes
    | Montant | Libelle | Binome   | Type      |
    | 100     | Courses | Aurelien | Partielle |
    | 200     | Pret    | Aurelien | Totale    |
    | 50      | Cadeaux | Marie    | Totale    |
	Then le binome Aurelien doit la somme de 200 euros

Scenario: Recalcul de la somme due lors d'une modification de montant
	Given Une période est créée	
	And j'y ai ajouté les dépenses suivantes
	| Montant | Libelle | Binome   | Type   |
    | 200     | Pret    | Aurelien | Avance |
    | 50      | Cadeaux | Marie    | Avance |
    | 300     | Leclerc | Marie    | Commun |
	And j'y ai ajouté les recettes suivantes
	| Montant | Libelle | Binome   | Type      |
    | 100     | Courses | Aurelien | Partielle |
    | 200     | Pret    | Aurelien | Totale    |
    | 50      | Cadeaux | Marie    | Totale    |
	When je modifie le montant de l'operation 1 en 100 euros
	Then le binome Aurelien doit la somme de 300 euros

Scenario: Recalcul de la somme due lors d'une modification de binome
	Given Une période est créée	
	And j'y ai ajouté les dépenses suivantes
	| Montant | Libelle | Binome   | Type   |
    | 200     | Pret    | Aurelien | Avance |
    | 50      | Cadeaux | Marie    | Avance |
    | 300     | Leclerc | Marie    | Commun |
	And j'y ai ajouté les recettes suivantes
	| Montant | Libelle | Binome   | Type      |
    | 100     | Courses | Aurelien | Partielle |
    | 200     | Pret    | Aurelien | Totale    |
    | 50      | Cadeaux | Marie    | Totale    |
	When je modifie le binome de l'operation 1 en Marie
	Then le binome Aurelien doit la somme de 600 euros

Scenario: Recalcul de la somme due lors d'une modification de type
	Given Une période est créée	
	And j'y ai ajouté les dépenses suivantes
	| Montant | Libelle | Binome   | Type   |
    | 200     | Pret    | Aurelien | Avance |
    | 50      | Cadeaux | Marie    | Avance |
    | 300     | Leclerc | Marie    | Commun |
	And j'y ai ajouté les recettes suivantes
	| Montant | Libelle | Binome   | Type      |
    | 100     | Courses | Aurelien | Partielle |
    | 200     | Pret    | Aurelien | Totale    |
    | 50      | Cadeaux | Marie    | Totale    |
	When je modifie le type de l'operation 1 en Commun
	Then le binome Aurelien doit la somme de 300 euros

Scenario: Recalcul de la somme due lors de la suppression d'une dépense
	Given Une période est créée	
	And j'y ai ajouté les dépenses suivantes
	| Montant | Libelle | Binome   | Type   |
    | 200     | Pret    | Aurelien | Avance |
    | 50      | Cadeaux | Marie    | Avance |
    | 300     | Leclerc | Marie    | Commun |
	And j'y ai ajouté les recettes suivantes
	| Montant | Libelle | Binome   | Type      |
    | 100     | Courses | Aurelien | Partielle |
    | 200     | Pret    | Aurelien | Totale    |
    | 50      | Cadeaux | Marie    | Totale    |
	When je supprime l'opération 2
	Then le binome Aurelien doit la somme de 150 euros

Scenario: Recalcul de la somme due lors de la suppression d'une recette
Given Une période est créée	
	And j'y ai ajouté les dépenses suivantes
	| Montant | Libelle | Binome   | Type   |
    | 200     | Pret    | Aurelien | Avance |
    | 50      | Cadeaux | Marie    | Avance |
    | 300     | Leclerc | Marie    | Commun |
	And j'y ai ajouté les recettes suivantes
	| Montant | Libelle | Binome   | Type      |
    | 100     | Courses | Aurelien | Partielle |
    | 200     | Pret    | Aurelien | Totale    |
    | 50      | Cadeaux | Marie    | Totale    |
	When je supprime l'opération 4
	Then le binome Aurelien doit la somme de 150 euros
