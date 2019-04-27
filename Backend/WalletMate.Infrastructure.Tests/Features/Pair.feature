Feature: Obtention du binôme configuré
	Le binôme peut dorénavant être configuré sous la forme d'utilisateurs dans un fichier de configuration
	Cela donne la possibilité d'installer et configurer le logiciel pour d'autres personnes et ainsi éviter que des informations soient stockées en dur
	
Scenario: Obtenir les binômes configurés
When Je demande à obtenir les binômes configurés dans le système
Then Le premier binôme est "Aurélien" et le second est "Marie"