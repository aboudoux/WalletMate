Feature: Rechercher une opération
	En tant qu'utilisateur
	Je veux pouvoir rechercher un opération dans l'application
	Pour savoir si celle-ci a déjà été enregistrée dans l'application

Background: 
	Given Je suis connecté à l'application avec l'utilisateur Aurélien et le mot de passe 1234
	And J'ai demandé la création d'une période pour le mois 1 et l'année 2001
	And J'ai demandé la création d'une période pour le mois 3 et l'année 2001
	And J'ai ajouté des dépenses dans l'application
	| Periode | Montant | Libelle      | Binome   | Categorie |
	| 2001-01 | 100     | Leclerc      | Marie    | Commun    |
	| 2001-01 | 18      | leroy merlin | Aurélien | Commun    |
	| 2001-01 | 25      | restaurant   | Aurélien | Commun    |
	| 2001-01 | 50      | spectacle    | Marie    | Commun    |
	| 2001-01 | 100     | liquide      | Aurélien | Avance    |
	| 2001-03 | 30      | cadeaux      | Aurélien | Commun    |
	| 2001-01 | 1234.56 | bricolage    | Marie    | Commun    |
	And J'ai ajouté des recettes dans l'application
	| Periode | Montant | Libelle | Binome   | Categorie |
	| 2001-01 | 10      | CAF     | Marie    | Commun    |
	| 2001-01 | 10      | Cadeau  | Aurélien | Commun    |
	| 2001-01 | 20      | Noel    | Marie    | Commun    |

Scenario: Recherche par la période
	When Je lance une recherche d'opérations avec le filtre "mars"
	Then La liste des opérations trouvées est
	| Type    | OperationId | Periode | Montant | Libelle | Binome   | Categorie |
	| Dépense | 1           | 2001-03 | 30      | cadeaux | Aurélien | Commun    |

Scenario: Recherche par le montant
	When Je lance une recherche d'opérations avec le filtre "100"
	Then La liste des opérations trouvées est
	| Type    | OperationId | Periode | Montant | Libelle | Binome   | Categorie |
	| Dépense | 1           | 2001-01 | 100     | Leclerc | Marie    | Commun    |
	| Dépense | 5           | 2001-01 | 100     | liquide | Aurélien | Avance    |

Scenario Outline: Recherche par une parite du montant
	When Je lance une recherche d'opérations avec le filtre "<filtre>"
	Then La liste des opérations trouvées est
	| Type    | OperationId | Periode | Montant | Libelle   | Binome | Categorie |
	| Dépense | 6           | 2001-01 | 1234.56 | bricolage | Marie  | Commun    |
	Examples: 
	| filtre  |
	| 12      |
	| 123     |
	| 1234    |
	| 1234,5  |
	| 1234,56 |

Scenario: Recherche par le label		
	When Je lance une recherche d'opérations avec le filtre "leclerc"
	Then La liste des opérations trouvées est
	| Type    | OperationId | Periode | Montant | Libelle | Binome | Categorie |
	| Dépense | 1           | 2001-01 | 100     | Leclerc | Marie  | Commun    |


Scenario: Recherche par le binôme
	When Je lance une recherche d'opérations avec le filtre "marie"
	Then La liste des opérations trouvées est
	| Type    | OperationId | Periode | Montant | Libelle   | Binome | Categorie |
	| Dépense | 1           | 2001-01 | 100     | Leclerc   | Marie  | Commun    |	
	| Dépense | 4           | 2001-01 | 50      | spectacle | Marie  | Commun    |
	| Dépense | 6           | 2001-01 | 1234.56 | bricolage | Marie  | Commun    |
	| Recette | 7           | 2001-01 | 10      | CAF       | Marie  | Commun    |
	| Recette | 9           | 2001-01 | 20      | Noel      | Marie  | Commun    |

Scenario: Recherche par la catégorie
	When Je lance une recherche d'opérations avec le filtre "Avance"
	Then La liste des opérations trouvées est
	| Type    | OperationId | Periode | Montant | Libelle | Binome   | Categorie |
	| Dépense | 5           | 2001-01 | 100     | liquide | Aurélien | Avance    |

Scenario: Recherche par le type
	When Je lance une recherche d'opérations avec le filtre "recette"
	Then La liste des opérations trouvées est
	| Type    | OperationId | Periode | Montant | Libelle | Binome   | Categorie |
	| Recette | 7           | 2001-01 | 10      | CAF     | Marie    | Commun    |
	| Recette | 8           | 2001-01 | 10      | Cadeau  | Aurélien | Commun    |
	| Recette | 9           | 2001-01 | 20      | Noel    | Marie    | Commun    |

Scenario: Recherche combinée par type et montant
	When Je lance une recherche d'opérations avec le filtre "dépense 25"
	Then La liste des opérations trouvées est
	| Type    | OperationId | Periode | Montant | Libelle    | Binome   | Categorie |
	| Dépense | 3           | 2001-01 | 25      | restaurant | Aurélien | Commun    |
	
Scenario: Recherche combinée par période et libellé
When Je lance une recherche d'opérations avec le filtre "mars cadeaux"
	Then La liste des opérations trouvées est
	| Type    | OperationId | Periode | Montant | Libelle | Binome   | Categorie |
	| Dépense | 1           | 2001-03 | 30      | cadeaux | Aurélien | Commun    |

Scenario: Recherche combinée avec tous les critères
When Je lance une recherche d'opérations avec le filtre "100 Marie leclerc commun janvier"
	Then La liste des opérations trouvées est
	| Type    | OperationId | Periode | Montant | Libelle | Binome | Categorie |
	| Dépense | 1           | 2001-01 | 100     | Leclerc | Marie  | Commun    |

Scenario: La recherche ne tiens pas compte de l'année de la période
	When Je lance une recherche d'opérations avec le filtre "20"
	Then La liste des opérations trouvées est
	| Type    | OperationId | Periode | Montant | Libelle | Binome | Categorie |
	| Recette | 9           | 2001-01 | 20      | Noel    | Marie  | Commun    |

Scenario: Une recherche vide ne retourne rien
	When Je lance une recherche d'opérations avec le filtre ""
	Then La liste des opérations trouvées est vide