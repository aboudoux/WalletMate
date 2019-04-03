Feature: Period
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Background: 
	Given Je suis connecté à l'application avec l'utilisateur aurelien et le mot de passe 0f46f2fb6f5a91c79e86acc5da7df95176b4e4c7

Scenario: Création d'une nouvelle période
	When Je demande la création d'une période pour le mois 1 et l'année 2001
	Then La liste des périodes contient "Janvier 2001"

Scenario: Intérdire la création de deux mêmes périodes
	And J'ai demandé la création d'une période pour le mois 1 et l'année 2001
	When Je demande la création d'une période pour le mois 1 et l'année 2001
	Then Le serveur me retourne une erreur 400 avec le message "La période Janvier 2001 existe déjà."

Scenario: Ajouter une dépense à une période
	And J'ai demandé la création d'une période pour le mois 1 et l'année 2001
	When J'ajoute des dépenses dans l'application
	| Type    | Periode | Montant | Libelle | Binome   | TypeOperation |
	| Dépense | 2001-01 | 100     | Test    | Aurélien | Commun        |
	Then La liste des opérations pour la période 2001-01 contient les elements suivants
	| Type    | Periode | Montant | Libelle | Binome   | TypeOperation |
	| Dépense | 2001-01 | 100     | Test    | Aurélien | Commun        |

Scenario: Ajouter une recette à une période
	And J'ai demandé la création d'une période pour le mois 1 et l'année 2001
	When J'ajoute des recettes dans l'application
	| Type    | Periode | Montant | Libelle         | Binome | TypeOperation |
	| Recette | 2001-01 | 200     | Test de recette | Marie  | Commun        |
	Then La liste des opérations pour la période 2001-01 contient les elements suivants
	| Type    | Periode | Montant | Libelle         | Binome | TypeOperation |
	| Recette | 2001-01 | 200     | Test de recette | Marie  | Commun        |

Scenario: Ajouter plusieurs recettes et dépenses 
