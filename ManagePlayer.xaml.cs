using Punto.Database;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

       
        private async void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            if (this.player != null)
            {
                this.player.Name = inputName.Text;
                this.player.Color = ((ListBoxItem)inputColor.SelectedItem).Content.ToString();
                await database.ModifyPlayerAsync(this.player);
                await database.LoadDatasAsync();
                ChoosePlayers choosePlayers = new ChoosePlayers(database);
                this.Content = choosePlayers;

            }
            else
            {
                if (inputName.Text != "" && inputColor.SelectedItem != null)
                {
                    Player newPlayer = new Player();
                    newPlayer.Name = inputName.Text;
                    newPlayer.Color = ((ListBoxItem)inputColor.SelectedItem).Content.ToString();
                    newPlayer.Wins = 0;
                    newPlayer.LastWin = "Pas encore gagné (looser !)";

                    await database.AddPlayerAsync(newPlayer);
                    await database.LoadDatasAsync();
                    ChoosePlayers choosePlayers = new ChoosePlayers(database);
                    this.Content = choosePlayers;

                }
                else
                {
                    MessageBox.Show("Merci de remplir tous les champs");
                }

            }
        }

        private async void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            await database.DeletePlayerAsync(this.player);
            await database.LoadDatasAsync();
            ChoosePlayers choosePlayers = new ChoosePlayers(database);
            this.Content = choosePlayers;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            ChoosePlayers choosePlayers = new ChoosePlayers(database);
            this.Content = choosePlayers;
        }




    }
}
