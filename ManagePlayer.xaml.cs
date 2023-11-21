﻿using Punto.Database;
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
                inputColor.ItemsSource = player.Color;
                inputColor.SelectedIndex = 0;

                titre.Text = "Modifier un joueur : ";
                btnSupprimer.IsEnabled = true;  
                btnSupprimer.Foreground = Brushes.Red;

            }
        }

        private void BtnValider_Click(object sender, RoutedEventArgs e)
        {
            if(this.player != null)
            {
                database.ModifyPlayer(this.player);
            }
            else
            {
                if(inputName.Text != "" && inputColor.SelectedItem != null )
                {
                    Player newPlayer = new Player();
                    newPlayer.Name = inputName.Text;
                    newPlayer.Color = ((ListBoxItem)inputColor.SelectedItem).Content.ToString();
                    database.AddPlayer(newPlayer);

                }
                else
                {
                    MessageBox.Show("Merci de remplir tous les champs");
                }
                
            }
            ChoosePlayers choosePlayers = new ChoosePlayers(database);
            this.Content = choosePlayers;
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            database.DeletePlayer(this.player);
        }
    }
}