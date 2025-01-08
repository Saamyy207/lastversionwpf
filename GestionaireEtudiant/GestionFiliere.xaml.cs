using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
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
//using CrystalDecisions.CrystalReports.Engine;
//using CrystalDecisions.Shared;
//using iTextSharp.text.pdf;
//using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;


namespace GestionaireEtudiant
{
    /// <summary>
    /// Logique d'interaction pour GestionFiliere.xaml
    /// </summary>
    public partial class GestionFiliere : UserControl
    {
        DataClasses1DataContext db = new DataClasses1DataContext();
        private Filiere selectedFiliere;

        private void ChargerFilières()
        {
            var fil = from x in db.Filiere select x;
            mydata.ItemsSource = fil.ToList();
        }

        private void Ajout(object sender, RoutedEventArgs e)
        {
            PopupAjouter.IsOpen = true;
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            string filiereNom = FiliereTextBox.Text.Trim();
            string RespoNom = RespoTextBox.Text.Trim();
            string EtudNom = EtudiantTextBox.Text.Trim();

            int nombreetds;
            if (int.TryParse(EtudNom, out nombreetds))
            {
                if (!string.IsNullOrEmpty(filiereNom))
                {
                    // Créer une nouvelle filière
                    Filiere nouvelleFiliere = new Filiere
                    {
                        nom_filiere = filiereNom,
                        responsable_filiere = RespoNom,
                        nombre_etudiants = nombreetds
                    };

                    // Insertion dans la base de données
                    db.Filiere.InsertOnSubmit(nouvelleFiliere);
                    db.SubmitChanges();

                    // Recharger les données
                    ChargerFilières();
                }
                else
                {
                    MessageBox.Show("Le nom de la filière ne peut pas être vide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Gérer le cas où EtudNom n'est pas un nombre valide
                MessageBox.Show("Le nombre d'étudiants doit être un nombre valide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Annuler l'ajout de la filière
        private void Annuler_Click(object sender, RoutedEventArgs e)
        {
            PopupAjouter.IsOpen = false;

        }

        // Sélectionner un élément dans le DataGrid
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedFiliere = mydata.SelectedItem as Filiere;

            if (selectedFiliere != null)
            {
                ModifierFiliereTextBox.Text = selectedFiliere.nom_filiere;
                MRespoTextBox.Text = selectedFiliere.responsable_filiere;
                MEtudiantTextBox.Text = selectedFiliere.nombre_etudiants.ToString();
            }
        }

        // Afficher le Popup pour modifier la filière
        private void Modifier_Click(object sender, RoutedEventArgs e)
        {
            if (selectedFiliere != null)
            {
                PopupModifier.IsOpen = true;
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une filière à modifier.");
            }
        }

        // Valider la modification
        private void ModifierValider_Click(object sender, RoutedEventArgs e)
        {
            if (selectedFiliere != null)
            {
                string newNomFiliere = ModifierFiliereTextBox.Text.Trim();
                string newRespo = MRespoTextBox.Text.Trim();
                string newNbEtds = MEtudiantTextBox.Text.Trim();
                int nvnombreetds;
                if (int.TryParse(newNbEtds, out nvnombreetds))
                    if (!string.IsNullOrEmpty(newNomFiliere))
                    {
                        selectedFiliere.nom_filiere = newNomFiliere;
                        selectedFiliere.responsable_filiere = newRespo;
                        selectedFiliere.nombre_etudiants = nvnombreetds;
                        db.SubmitChanges();

                        ChargerFilières();
                        PopupModifier.IsOpen = false;
                    }
            }
            else
            {
                MessageBox.Show("Le nom de la filière ne peut pas être vide.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            SoundPlayer p = new SoundPlayer(@"C:\Users\user\Downloads\great.wav");
            p.Load();
            p.Play();
        }

        // Annuler la modification
        private void AnnulerModifier_Click(object sender, RoutedEventArgs e)
        {
            PopupModifier.IsOpen = false;
        }

        // Supprimer une filière
        private void Supp(object sender, RoutedEventArgs e)
        {
            if (selectedFiliere != null)
            {
                var result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette filière ?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    db.Filiere.DeleteOnSubmit(selectedFiliere);
                    db.SubmitChanges();

                    ChargerFilières();
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une filière à supprimer.");
            }
        }


        private void TriComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TriComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string triOption = selectedItem.Content.ToString();

                ObservableCollection<Filiere> filieresTriées = new ObservableCollection<Filiere>(db.Filiere);

                if (triOption == "Ordre Alphabétique")
                {
                    var sorted = filieresTriées.OrderBy(f => f.nom_filiere).ToList();
                    filieresTriées.Clear();
                    foreach (var filiere in sorted)
                    {
                        filieresTriées.Add(filiere);
                    }
                }
                else if (triOption == "Nombre d'Élèves (Croissant)")
                {
                    var sorted = filieresTriées.OrderBy(f => f.nombre_etudiants ?? 0).ToList();
                    filieresTriées.Clear();
                    foreach (var filiere in sorted)
                    {
                        filieresTriées.Add(filiere);
                    }
                }
                else if (triOption == "Nombre d'Élèves (Décroissant)")
                {
                    var sorted = filieresTriées.OrderByDescending(f => f.nombre_etudiants ?? 0).ToList();
                    filieresTriées.Clear();
                    foreach (var filiere in sorted)
                    {
                        filieresTriées.Add(filiere);
                    }
                }

                mydata.ItemsSource = filieresTriées;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = SearchBox.Text.Trim().ToLower();

            // Check if the search query is empty
            if (string.IsNullOrEmpty(searchQuery))
            {
                MessageBox.Show("Veuillez entrer une filière.");
                return;
            }

            bool foundMatch = false;

            foreach (var item in mydata.Items)
            {
                if (item is Filiere filiere)
                {
                    var row = mydata.ItemContainerGenerator.ContainerFromItem(filiere) as DataGridRow;
                    if (row != null)
                    {
                        // Check if the "nom_filiere" contains the search query
                        if (filiere.nom_filiere.ToLower().Contains(searchQuery))
                        {
                            row.Background = Brushes.Yellow; // Highlight the matching row
                            foundMatch = true;
                        }
                        else
                        {
                            row.Background = Brushes.White; // Reset the background for non-matching rows
                        }
                    }
                }
            }

            if (!foundMatch)
            {
                MessageBox.Show("Aucune filière correspondante trouvée.");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*var filieres = from f in db.Filiere select f;
            try
            {
                if (filieres.Any())
                {
                    // Définir le chemin où le fichier PDF sera créé (exemple : Bureau de l'utilisateur)
                    string cheminPDF = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Liste_Filieres.pdf";

                    // Création du document PDF
                    Document document = new Document(PageSize.A4);
                    PdfWriter.GetInstance(document, new FileStream(cheminPDF, FileMode.Create));

                    // Ouvrir le document pour y ajouter du contenu
                    document.Open();

                    // Ajouter un titre au PDF, centré et en bleu
                    iTextSharp.text.Paragraph titre = new iTextSharp.text.Paragraph(
                        "Liste des Filières",
                        FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, iTextSharp.text.BaseColor.BLUE)
                    );
                    titre.Alignment = Element.ALIGN_CENTER;
                    document.Add(titre);

                    // Ajouter un espace après le titre
                    document.Add(new iTextSharp.text.Paragraph("\n"));

                    // Ajouter deux images
                    string basePath = AppDomain.CurrentDomain.BaseDirectory; // Récupère le chemin d'exécution
                    string imagePath1 = System.IO.Path.Combine(basePath, "Images", "caddyayad.jpg");
                    string imagePath2 = System.IO.Path.Combine(basePath, "Images", "ENSASafi_logo.png");

                    if (File.Exists(imagePath1))
                    {
                        iTextSharp.text.Image img1 = iTextSharp.text.Image.GetInstance(imagePath1);
                        img1.ScaleToFit(100f, 100f); // Ajuster la taille de l'image
                        img1.Alignment = Element.ALIGN_LEFT;
                        document.Add(img1);
                    }
                    else
                    {
                        MessageBox.Show("Image introuvable : " + imagePath1, "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    if (File.Exists(imagePath2))
                    {
                        iTextSharp.text.Image img2 = iTextSharp.text.Image.GetInstance(imagePath2);
                        img2.ScaleToFit(100f, 100f); // Ajuster la taille de l'image
                        img2.Alignment = Element.ALIGN_RIGHT;
                        document.Add(img2);
                    }
                    else
                    {
                        MessageBox.Show("Image introuvable : " + imagePath2, "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                    // Ajouter un espace après les images
                    document.Add(new iTextSharp.text.Paragraph("\n"));

                    // Créer une table pour afficher les données
                    PdfPTable table = new PdfPTable(3); // Trois colonnes : Nom, Responsable, Nombre d'Etudiants
                    table.WidthPercentage = 100; // Ajuster la table à la largeur de la page

                    // Ajouter les en-têtes de colonnes avec un fond bleu clair
                    PdfPCell cell = new PdfPCell(new Phrase("Nom de la Filière", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(173, 216, 230); // Bleu clair
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Responsable", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(173, 216, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Nombre d'Étudiants", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(173, 216, 230);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    // Ajouter les données des filières dans la table
                    foreach (var filiere in filieres)
                    {
                        table.AddCell(new Phrase(filiere.nom_filiere, FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                        table.AddCell(new Phrase(filiere.responsable_filiere, FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                        table.AddCell(new Phrase(filiere.nombre_etudiants.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                    }

                    // Ajouter la table au document PDF
                    document.Add(table);

                    // Fermer le document
                    document.Close();

                    // Message de confirmation
                    MessageBox.Show("PDF généré avec succès à l'emplacement : " + cheminPDF, "Succès", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Ouvrir le fichier PDF après génération (facultatif)
                    System.Diagnostics.Process.Start(cheminPDF);
                }
                else
                {
                    MessageBox.Show("Aucune donnée trouvée dans la base de données.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la génération du PDF : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }*/
        }
        
    }
}
