Feature: Gestion des périodes
	Une période représente un mois dans lequel nous allons pouvoir ajouter des dépenses et des recettes.
	Toutes ces opérations pemettent de calculer qui sera redevable à l'autre binôme	


Background: 
	Given Je suis connecté à l'application avec l'utilisateur Aurélien et le mot de passe 1234

Scenario: Création d'une nouvelle période
	When Je demande la création d'une période pour le mois 1 et l'année 2001
	Then La liste des périodes contient "Janvier 2001" avec l'identifiant 2001-01

Scenario: Intérdire la création de deux mêmes périodes
	And J'ai demandé la création d'une période pour le mois 1 et l'année 2001
	When Je demande la création d'une période pour le mois 1 et l'année 2001
	Then Le serveur me retourne une erreur 400 avec le message "La période Janvier 2001 existe déjà."

Scenario: Ajouter une dépense à une période
	And J'ai demandé la création d'une période pour le mois 1 et l'année 2001
	When J'ajoute des dépenses dans l'application
	| Periode | Montant | Libelle | Binome   | Categorie |
	| 2001-01 | 100     | Test    | Aurélien | Commun        |
	Then La liste des opérations pour la période 2001-01 contient les elements suivants
	| Type    | operationId | Periode | Montant | Libelle | Binome   | Categorie |
	| Dépense | 1           | 2001-01 | 100     | Test    | Aurélien | Commun    |

Scenario: Ajouter une recette à une période
	And J'ai demandé la création d'une période pour le mois 1 et l'année 2001
	When J'ajoute des recettes dans l'application
	| Periode | Montant | Libelle         | Binome | Categorie |
	| 2001-01 | 200     | Test de recette | Marie  | Commun    |
	Then La liste des opérations pour la période 2001-01 contient les elements suivants
	| Type    | OperationId | Periode | Montant | Libelle         | Binome | Categorie |
	| Recette | 1           | 2001-01 | 200     | Test de recette | Marie  | Commun    |

Scenario: Supprimer une dépense d'une période
And J'ai demandé la création d'une période pour le mois 1 et l'année 2001
	And J'ai ajouté des dépenses dans l'application
	| Periode | Montant | Libelle | Binome   | Categorie |
	| 2001-01 | 100     | Test    | Aurélien | Commun        |
	And La liste des opérations pour la période 2001-01 contient les elements suivants
	| Type    | OperationId | Periode | Montant | Libelle | Binome   | Categorie |
	| Dépense | 1           | 2001-01 | 100     | Test    | Aurélien | Commun        |
	When Je demande à supprimer l'opération 1 de la période 2001-01
	Then La liste des opérations pour la période 2001-01 est vide

	Scenario: Supprimer une recette d'une période
And J'ai demandé la création d'une période pour le mois 1 et l'année 2001
	And J'ai ajouté des recettes dans l'application
	| Periode | Montant | Libelle | Binome   | Categorie |
	| 2001-01 | 100     | Test    | Aurélien | Commun    |
	And La liste des opérations pour la période 2001-01 contient les elements suivants
	| Type    | OperationId | Periode | Montant | Libelle | Binome   | Categorie |
	| Recette | 1           | 2001-01 | 100     | Test    | Aurélien | Commun        |
	When Je demande à supprimer l'opération 1 de la période 2001-01
	Then La liste des opérations pour la période 2001-01 est vide

Scenario: Modifier le montant d'une opération de recette
And J'ai demandé la création d'une période pour le mois 1 et l'année 2001
	And J'ai ajouté des recettes dans l'application
	| Periode | Montant | Libelle | Binome   | Categorie |
	| 2001-01 | 100     | Test    | Marie | Commun        |
	And La liste des opérations pour la période 2001-01 contient les elements suivants
	| Type    | OperationId | Periode | Montant | Libelle | Binome   | Categorie |
	| Recette | 1           | 2001-01 | 100     | Test    | Marie | Commun        |
	When je demande à modifier le montant de la recette numéro 1 en 200 euros pour la période 2001-01
	Then L'opération 1 de la période 2001-01 à un montant de 200 euros
	And Marie doit la somme de 100 euros pour la période 2001-01

Scenario: Modifier le libellé d'une opération de recette
And J'ai demandé la création d'une période pour le mois 1 et l'année 2001
	And J'ai ajouté des recettes dans l'application
	| Periode | Montant | Libelle | Binome   | Categorie |
	| 2001-01 | 100     | Test    | Marie | Commun        |
	And La liste des opérations pour la période 2001-01 contient les elements suivants
	| Type    | OperationId | Periode | Montant | Libelle | Binome   | Categorie |
	| Recette | 1           | 2001-01 | 100     | Test    | Marie | Commun        |
	When je demande à modifier le libellé de la recette numéro 1 en "essai" pour la période 2001-01
	Then L'opération 1 de la période 2001-01 à pour libellé "essai"
	And Marie doit la somme de 50 euros pour la période 2001-01

Scenario: Modifier le binôme d'une opération de recette
And J'ai demandé la création d'une période pour le mois 1 et l'année 2001
	And J'ai ajouté des recettes dans l'application
	| Periode | Montant | Libelle | Binome   | Categorie |
	| 2001-01 | 100     | Test    | Marie | Commun        |
	And La liste des opérations pour la période 2001-01 contient les elements suivants
	| Type    | OperationId | Periode | Montant | Libelle | Binome   | Categorie |
	| Recette | 1           | 2001-01 | 100     | Test    | Marie | Commun        |
	When je demande à modifier le binôme de la recette numéro 1 par Aurélien pour la période 2001-01
	Then L'opération 1 de la période 2001-01 à pour binôme Aurélien
	And Aurélien doit la somme de 50 euros pour la période 2001-01

Scenario: Modifier le type d'une opération de recette
And J'ai demandé la création d'une période pour le mois 1 et l'année 2001
	And J'ai ajouté des recettes dans l'application
	| Periode | Montant | Libelle | Binome | Categorie |
	| 2001-01 | 100     | Test    | Marie  | Commun        |
	And La liste des opérations pour la période 2001-01 contient les elements suivants
	| Type    | OperationId | Periode | Montant | Libelle | Binome | Categorie |
	| Recette | 1           | 2001-01 | 100     | Test    | Marie  | Commun        |
	When je demande à modifier le type de la recette numéro 1 en "Individuelle" pour la période 2001-01
	Then L'opération 1 de la période 2001-01 à pour type "Individuelle"
	And Marie doit la somme de 100 euros pour la période 2001-01

Scenario: Modifier le montant d'une opération de dépense
And J'ai demandé la création d'une période pour le mois 1 et l'année 2001
	And J'ai ajouté des dépenses dans l'application
	| Periode | Montant | Libelle | Binome | Categorie |
	| 2001-01 | 100     | Test    | Marie  | Commun        |
	And La liste des opérations pour la période 2001-01 contient les elements suivants
	| Type    | OperationId | Periode | Montant | Libelle | Binome | Categorie |
	| Dépense | 1           | 2001-01 | 100     | Test    | Marie  | Commun        |
	When je demande à modifier le montant de la dépense numéro 1 en 200 euros pour la période 2001-01
	Then L'opération 1 de la période 2001-01 à un montant de 200 euros
	And Aurélien doit la somme de 100 euros pour la période 2001-01

Scenario: Modifier le libellé d'une opération de dépense
And J'ai demandé la création d'une période pour le mois 1 et l'année 2001
	And J'ai ajouté des dépenses dans l'application
	| Periode | Montant | Libelle | Binome | Categorie |
	| 2001-01 | 100     | Test    | Marie  | Commun        |
	And La liste des opérations pour la période 2001-01 contient les elements suivants
	| Type    | OperationId | Periode | Montant | Libelle | Binome | Categorie |
	| Dépense | 1           | 2001-01 | 100     | Test    | Marie  | Commun        |
	When je demande à modifier le libellé de la dépense numéro 1 en "essai" pour la période 2001-01
	Then L'opération 1 de la période 2001-01 à pour libellé "essai"
	And Aurélien doit la somme de 50 euros pour la période 2001-01

Scenario: Modifier le binôme d'une opération de dépense
And J'ai demandé la création d'une période pour le mois 1 et l'année 2001
	And J'ai ajouté des dépenses dans l'application
	| Periode | Montant | Libelle | Binome | Categorie |
	| 2001-01 | 100     | Test    | Marie  | Commun        |
	And La liste des opérations pour la période 2001-01 contient les elements suivants
	| Type    | OperationId | Periode | Montant | Libelle | Binome | Categorie |
	| Dépense | 1           | 2001-01 | 100     | Test    | Marie  | Commun    |
	When je demande à modifier le binôme de la dépense numéro 1 par Aurélien pour la période 2001-01
	Then L'opération 1 de la période 2001-01 à pour binôme Aurélien
	And Marie doit la somme de 50 euros pour la période 2001-01

Scenario: Modifier le type d'une opération de dépense
And J'ai demandé la création d'une période pour le mois 1 et l'année 2001
	And J'ai ajouté des dépenses dans l'application
	| Periode | Montant | Libelle | Binome | Categorie |
	| 2001-01 | 100     | Test    | Marie  | Commun        |
	And La liste des opérations pour la période 2001-01 contient les elements suivants
	| Type    | OperationId | Periode | Montant | Libelle | Binome | Categorie |
	| Dépense | 1           | 2001-01 | 100     | Test    | Marie  | Commun        |
	When je demande à modifier le type de la dépense numéro 1 en "Avance" pour la période 2001-01
	Then L'opération 1 de la période 2001-01 à pour type "Avance"
	And Aurélien doit la somme de 100 euros pour la période 2001-01

Scenario: Obtenir le solde d'une période après de multiples opérations
	And J'ai demandé la création d'une période pour le mois 1 et l'année 2001
	When J'ajoute des dépenses dans l'application
	| Periode | Montant | Libelle | Binome   | Categorie |
	| 2001-01 | 150     | leclerc | Marie    | Commun        |
	| 2001-01 | 200     | cadeau  | Aurelien | Commun        |
	| 2001-01 | 55      | edf     | Aurelien | Commun        |
	| 2001-01 | 30      | docteur | Marie    | Avance        |		
	And J'ajoute des recettes dans l'application
	| Periode | Montant | Libelle | Binome | Categorie |
	| 2001-01 | 200     | CAF     | Marie  | Commun        |
	Then Marie doit la somme de 122.5 euros pour la période 2001-01

Scenario: La liste des périodes doit être dans un ordre déscendant
	And J'ai demandé la création d'une période pour le mois 1 et l'année 2001
	And J'ai demandé la création d'une période pour le mois 2 et l'année 2001
	And J'ai demandé la création d'une période pour le mois 3 et l'année 2001
	And J'ai demandé la création d'une période pour le mois 4 et l'année 2001
	And J'ai demandé la création d'une période pour le mois 1 et l'année 2002
	And J'ai demandé la création d'une période pour le mois 1 et l'année 2003
	Then la liste des périodes est présentée dans l'ordre décroissant
	
