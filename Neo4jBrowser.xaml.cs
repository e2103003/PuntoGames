using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Web.WebView2.Core;


namespace Punto
{
    /// <summary>
    /// Logique d'interaction pour Neo4jBrowser.xaml
    /// </summary>
    public partial class Neo4jBrowser : UserControl
    {
        private DatabaseUse database;
        public Neo4jBrowser(DatabaseUse database)
        {
            InitializeComponent();
            this.database = database;
            neo4jWebView.EnsureCoreWebView2Async(null);
            neo4jWebView.CoreWebView2InitializationCompleted += WebView2InitializationCompleted;
            //neo4jWebView.CoreWebView2DOMContentLoaded += WebView2DOMContentLoaded;

        }


        private void WebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            // Une fois que WebView2 est initialisé, chargez le Neo4j Browser


            neo4jWebView.Source = new Uri("http://localhost:7474/browser/");
        }


        private void WebView2DOMContentLoaded(object sender, CoreWebView2DOMContentLoadedEventArgs e)
        {
            // Une fois que la page a fini de charger, exécutez la requête Cypher
            string cypherQuery = "MATCH (p:Player) RETURN p";
            string encodedQuery = Uri.EscapeDataString(cypherQuery);
            string neo4jUrl = $"http://localhost:7474/browser/?cmd=play&arg={encodedQuery}";

            neo4jWebView.Source = new Uri(neo4jUrl);
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            ChoosePlayers choosePlayers = new ChoosePlayers(database);
            this.Content = choosePlayers;

        }
    }
}
