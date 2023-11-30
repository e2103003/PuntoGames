using Punto.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Punto
{
    /// <summary>
    /// Logique d'interaction pour ManagePlayer.xaml
    /// </summary>
    public partial class ManagePlayer : UserControl
    {
        private Player player;
        private DatabaseUse database;
        public ManagePlayer(Player player, DatabaseUse database)
        {
            InitializeComponent();

            this.database = database;

            if (player != null)
            {
                this.player = player;                

                inputId.Text = player.Id.ToString();
                inputName.Text = player.Name;
                foreach (ListBoxItem item in inputColor.Items)
                {
                    if (item.Content.ToString() == player.Color)
                    {
                        item.IsSelected = true;
                        break;
                    }
                }

                titre.Text = "Modifier un joueur : ";
                btnSupprimer.IsEnabled = true;  
                btnSupprimer.Foreground = Brushes.Red;

            }
        }

       
        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            
            if (this.player != null)
            {
                this.player.Name = inputName.Text;
                this.player.Color = ((ListBoxItem)inputColor.SelectedItem).Content.ToString();
                database.ModifyPlayer(this.player);
                ChoosePlayers choosePlayers = new ChoosePlayers(database);
                this.Content = choosePlayers;

            }
            else
            {
                if(inputName.Text != "" && inputColor.SelectedItem != null )    
                {
                    Player newPlayer = new Player();
                    newPlayer.Name = inputName.Text;
                    newPlayer.Color = ((ListBoxItem)inputColor.SelectedItem).Content.ToString();
                    newPlayer.Wins = 0;
                    newPlayer.LastWin = "Pas encore gagné (looser !)";

                    database.AddPlayer(newPlayer);
                    ChoosePlayers choosePlayers = new ChoosePlayers(database);
                    this.Content = choosePlayers;

                }
                else
                {
                    MessageBox.Show("Merci de remplir tous les champs");
                }
                
            }
            
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            database.DeletePlayer(this.player);
            ChoosePlayers choosePlayers = new ChoosePlayers(database);
            this.Content = choosePlayers;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            ChoosePlayers choosePlayers = new ChoosePlayers(database);
            this.Content = choosePlayers;
        }

        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            // test d'une nouvelle victoire du joueur
            database.AddVictory(this.player);

        }



    }
}
