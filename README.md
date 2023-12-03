# PuntoGames
## Installation et utilisation
Pour ce faire vous pouvez utiliser directement le fichier /release/Punto-Punto.exe ou bien lancer le projet avec Visual Studio (Fichier Punto.sln) et cliquer sur le bouton "Punto" (ou F5).
Concernant les bases de donn�es, elles fonctionnent d�s le premier lancement du programme, il se peut que MySQL recontre des probl�mes pour li�er le bon serveur, si besoin voici les informations de connexions :

	- Serveur : 127.0.0.1
	- Nom de la base de donn�es : PuntoDatabase
	- Uid : username
	- Pwd : password
**Attention :** Il faut que l'utilisateur ait les droits de cr�ation de base de donn�es et de tables.
*Ce sont les param�tres par d�faut, vous pouvez les modifier dans le fichier MySQLUse.cs (ligne 19)*

## Fonctionnalit�s

### Choix de la base de donn�es
Vous pouvez choisir la base de donn�es que vous souhaitez utiliser, pour ce faire il suffit de cliquer sur le bouton correspondant au lancement du jeu. Vous pouvez la changer quand vous souhaitez.

### Transfert de donn�es
Il est possible, depuis le menu principal, de transf�rer les donn�es d'une base de donn�es � une autre, pour ce faire il suffit de cliquer sur le bouton "Transf�rer" et de choisir la base de donn�es source et la base de donn�es cible. Les joueurs avec les m�mes noms ne seront pas ajout�s pour �viter les doublons.

### Choix des joueurs
Vous pouvez, pour les 3 base de donn�es, cr�er modifier ou supprimer un joueur, pour ce faire il suffit de cliquer sur le bouton correspondant et de remplir les champs.


### Gameplay
Le jeu se joue de 2 � 4 joueurs, chaque joueurs dispose de 18 cartes (de 1 � 9) et doit avoir une couleur diff�rente, le but �tant d'aligner 5 cartes de la m�me couleur (horizontalement, verticalement ou en diagonale). <br>
Durant la partie, vous pouvez :
		
	- placer votre carte sur le plateau en cliquant sur la case correspondante, les cases jouables sont avec les bords noirs
	- Passer votre tour en cliquant sur le bouton "Passer mon tour" 
	- Recommencer la partie en cliquant sur le bouton avec le logo de la fl�che circulaire
Vous avez �galement le nom du joueur qui doit jouer et le nombre de cartes restantes pour chaque joueur.<br>
A la fin de la partie, le gagnant vous est annonc� et sa victoire est mise � jour dans la base de donn�es. Une partie se relance automatiquement apr�s la fin de la pr�c�dente.



