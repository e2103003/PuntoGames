# PuntoGames
## Installation et utilisation
Pour ce faire vous pouvez utiliser directement le fichier /release/Punto-Punto.exe ou bien lancer le projet avec Visual Studio (Fichier Punto.sln) et cliquer sur le bouton "Punto" (ou F5).
Concernant les bases de données, elles fonctionnent dès le premier lancement du programme, il se peut que MySQL recontre des problèmes pour liéer le bon serveur, si besoin voici les informations de connexions :

	- Serveur : 127.0.0.1
	- Nom de la base de données : PuntoDatabase
	- Uid : username
	- Pwd : password
**Attention :** Il faut que l'utilisateur ait les droits de création de base de données et de tables.
*Ce sont les paramètres par défaut, vous pouvez les modifier dans le fichier MySQLUse.cs (ligne 19)*

## Fonctionnalités

### Choix de la base de données
Vous pouvez choisir la base de données que vous souhaitez utiliser, pour ce faire il suffit de cliquer sur le bouton correspondant au lancement du jeu. Vous pouvez la changer quand vous souhaitez.

### Transfert de données
Il est possible, depuis le menu principal, de transférer les données d'une base de données à une autre, pour ce faire il suffit de cliquer sur le bouton "Transférer" et de choisir la base de données source et la base de données cible. Les joueurs avec les mêmes noms ne seront pas ajoutés pour éviter les doublons.

### Choix des joueurs
Vous pouvez, pour les 3 base de données, créer modifier ou supprimer un joueur, pour ce faire il suffit de cliquer sur le bouton correspondant et de remplir les champs.


### Gameplay
Le jeu se joue de 2 à 4 joueurs, chaque joueurs dispose de 18 cartes (de 1 à 9) et doit avoir une couleur différente, le but étant d'aligner 5 cartes de la même couleur (horizontalement, verticalement ou en diagonale). <br>
Durant la partie, vous pouvez :
		
	- placer votre carte sur le plateau en cliquant sur la case correspondante, les cases jouables sont avec les bords noirs
	- Passer votre tour en cliquant sur le bouton "Passer mon tour" 
	- Recommencer la partie en cliquant sur le bouton avec le logo de la flèche circulaire
Vous avez également le nom du joueur qui doit jouer et le nombre de cartes restantes pour chaque joueur.<br>
A la fin de la partie, le gagnant vous est annoncé et sa victoire est mise à jour dans la base de données. Une partie se relance automatiquement après la fin de la précédente.



