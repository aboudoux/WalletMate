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
	Given Je suis connecté à l'application avec l'utilisateur aurelien et le mot de passe 1234
	And J'ai demandé la création d'une période pour le mois 1 et l'année 2001
	When Je demande la création d'une période pour le mois 1 et l'année 2001
	Then Le serveur me retourne une erreur 400 avec le message "La période Janvier 2001 existe déjà."

Scenario: Ajouter une dépense à une période
	And J'ai demandé la création de la période pour le mois 1 et l'année 2001
	When J'ajoute une dépense à la période
	| Montant | Libelle | Binome   | Type   |
	| 100     | Test    | Aurelien | Commun |
	Then La liste des opérations pour la période 
