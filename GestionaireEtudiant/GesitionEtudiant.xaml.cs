using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml.Linq;

namespace GestionaireEtudiant
{
    /// <summary>
    /// Logique d'interaction pour GesitionEtudiant.xaml
    /// </summary>
    public partial class GesitionEtudiant : UserControl, INotifyPropertyChanged
    {
        DataClasses1DataContext d;

        public ObservableCollection<EtudiantDTO> etudiants;
        public ObservableCollection<EtudiantDTO> Etudiants
        {
            get { 
                return etudiants;
            }
            set
            {
                etudiants = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Etudiants)));
            }
        }
       

        public event PropertyChangedEventHandler PropertyChanged;
        private string addFromVisibility = "Collapsed";
        public string AddFormVisibility
        {
            get
            {
                return this.addFromVisibility;
            }
            set
            {
                if (addFromVisibility != value)
                {
                    addFromVisibility = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddFormVisibility)));
                }
            }
        }
        private string modifyFromVisibility = "Collapsed";
        public string ModifyFormVisibility
        {
            get
            {
                return this.modifyFromVisibility;
            }
            set
            {
                if (modifyFromVisibility != value)
                {
                    modifyFromVisibility = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ModifyFormVisibility)));
                }
            }
        }


        private string nomErr= "#0077B6";
        public string NomErr
        {
            get { return this.nomErr; }
            set
            {
                    nomErr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NomErr)));
                

            }
        }
        private string prenomErr = "#0077B6";
        public string PrenomErr
        {
            get { return this.prenomErr; }
            set
            {
                prenomErr = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PrenomErr)));
            }
        }

        private string dateNaissanceErr = "#0077B6";
        public string DateNaissanceErr
        {
            get { return this.dateNaissanceErr; }
            set
            {
                dateNaissanceErr = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DateNaissanceErr)));
            }
        }

        private string anneeInsErr = "#0077B6";
        public string AnneeInsErr
        {
            get { return this.anneeInsErr; }
            set
            {
                anneeInsErr = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnneeInsErr)));
            }
        }
        private string _emailErr= "#0077B6";

        private string _heureAbsErr = "#0077B6";

        private string _heureJustErr = "#0077B6";
        private string _telephoneErr = "#0077B6";

        public string EmailErr
        {
            get { return _emailErr; }
            set
            {
                
                    _emailErr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EmailErr)));
                
            }
        }

        public string HeureAbsErr
        {
            get { return _heureAbsErr; }
            set
            {
                if (_heureAbsErr != value)
                {
                    _heureAbsErr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HeureAbsErr))); 
                }
            }
        }

        public string HeureJustErr
        {
            get { return _heureJustErr; }
            set
            {
                    _heureJustErr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HeureJustErr))); 
                
            }
        }

        public string TelephoneErr
        {
            get { return _telephoneErr; }
            set
            {
                
                    _telephoneErr = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TelephoneErr))); ;
                
            }
        }

        private string searchedNom;
        public string SearchedNom
        {
            get
            {
                return searchedNom;
            }
            set
            {
                searchedNom = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchedNom)));

            }
        }
        private string searchedPrenom;
        public string SearchedPrenom
        {
            get
            {
                return searchedPrenom;

            }
            set
            {
                searchedPrenom = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchedPrenom)));
            }
                }
        private string searchedCne;
        public string SearchedCne
        {
            get
            {
                return searchedCne;

            }
            set
            {
                searchedCne = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchedCne)));
            }
        }

        public string selectedF { get; set; }

        List<EtudiantDTO>  EtudiantsOriginal;


        public GesitionEtudiant()
        {
            InitializeComponent();
            d = new DataClasses1DataContext();
            selectedF = "Toutes les fillières";

             EtudiantsOriginal = (from e in d.Etudiant
                                     join f in d.Filiere on e.ID_Filiere equals f.id
                                     select new EtudiantDTO
                                     {
                                         ID_Etudiant = e.Cne,
                                         Nom = e.Nom,
                                         Prenom = e.Prenom,
                                         Date_Naissance = e.Date_Naissance,
                                         Genre = e.Genre,
                                         ID_Filiere = e.ID_Filiere,
                                         Nom_Filiere = f.nom_filiere,
                                         Annee_Inscription = e.Annee_Inscription,
                                         Email = e.Email,
                                         Telephone = e.Telephone,
                                         Adresse = e.Adresse,
                                         Heures_Absence = e.Heures_Absence,
                                         Absences_Justifiees = e.Absences_Justifiees
                                     }).ToList();
            Etudiants = new ObservableCollection<EtudiantDTO>(EtudiantsOriginal);

            DataContext = this;

            

            LoadCombo();

        }

        private void LoadCombo()
        {

            ComboFiliere.Items.Add("Toutes les fillières");
            var filiere = (from f in d.Filiere
                           select f.nom_filiere).ToList();
            foreach (var f in filiere) {
                ComboFiliere.Items.Add(f);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EmailErr = "#0077B6";
            NomErr="#0077B6";
            PrenomErr="#0077B6";
            TelephoneErr = "#0077B6";

            AddFormVisibility = "Visible";
            ComboAdd.ItemsSource = (from f in d.Filiere
                                    select f.nom_filiere).ToList();


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddFormVisibility = "Collapsed";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            AddFormVisibility = "Collapsed";
        }

       
        private void ClearForm()
        {
            Nom.Text = string.Empty;
            Prenom.Text = string.Empty;
            Cne.Text = string.Empty;
            dateNaissance.Text = string.Empty;
            male.IsChecked = false;
            female.IsChecked = false;
            ComboAdd.SelectedIndex = -1;
            email.Text = string.Empty;
            telephone.Text = string.Empty;
            adresse.Text = string.Empty;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {

                string pattern = @"^[a-zA-Z]+$";
                // Create an instance of the Etudiant object
                Etudiant etudiant = new Etudiant();
                EtudiantDTO et = new EtudiantDTO();
                if (Nom.Text != string.Empty) {

                    if (Regex.IsMatch(Nom.Text, pattern)){
                        etudiant.Nom = Nom.Text;
                        et.Nom = Nom.Text;
                    }
                    else
                    {
                        throw new Exception("Entrez un nom valide contenat que des lettres svp");

                    }

                }
                else
                {
                    throw new Exception("Entrez un nom valide contenat que des lettres svp");
                }
                if (Prenom.Text != string.Empty)
                {

                    if (Regex.IsMatch(Prenom.Text, pattern))
                    {
                        etudiant.Prenom = Prenom.Text;
                        et.Prenom = Prenom.Text;
                    }
                    else
                    {
                        throw new Exception("Entrez un Prenom valide contenat que des lettres svp");

                    }

                }
                else
                {
                    throw new Exception("Entrez un Prenom valide contenat que des lettres svp");
                }

                if (Cne.Text != string.Empty)
                {

                    etudiant.Cne =Cne.Text;
                    et.ID_Etudiant =Cne.Text;

                }
                else
                {
                    throw new Exception("Entrez un Cne valide");
                }
                et.Absences_Justifiees=0;
                et.Heures_Absence= 0;
                etudiant.Absences_Justifiees =0;
                etudiant.Heures_Absence = 0;


                // Parse and assign Date_Naissance
                if (DateTime.TryParse(dateNaissance.Text, out var parsedDate))
                {
                    etudiant.Date_Naissance = parsedDate;
                    et.Date_Naissance = parsedDate;
                }
                else
                {
                    throw new Exception("Format de date non valide. Veuillez entrer une date valide.");
                }

                if (int.Parse(dateInscription.Text) <= 2024)
                {
                    etudiant.Annee_Inscription = int.Parse(dateInscription.Text);
                    et.Annee_Inscription = int.Parse(dateInscription.Text);
                }
                else
                {
                    throw new Exception("Format d'année non valide. Veuillez entrer une année valide.");
                }
                // Assign Genre based on selected RadioButton
                if (male.IsChecked == true)
                {
                    etudiant.Genre = "Masculin";
                    et.Genre = "Masculin";
                }
                else if (female.IsChecked == true)
                {
                    etudiant.Genre = "Féminin";
                    et.Genre = "Féminin";
                }
                else
                {
                    throw new Exception("Veuillez sélectionner un genre.");
                }

                // Assign Filière (ID_Filiere)
                if (ComboAdd.SelectedValue != null)
                {
                    et.Nom_Filiere = ComboAdd.SelectedValue.ToString();
                    var selectedfilere = ComboAdd.SelectedValue;
                    var id_fil = (from f in d.Filiere
                                  where f.nom_filiere == selectedfilere
                                  select f.id).FirstOrDefault();
                    etudiant.ID_Filiere = id_fil;
                }
                else
                {
                    throw new Exception("Veuillez sélectionner une filière.");
                }


                if (email.Text != string.Empty)
                {
                    if (Regex.IsMatch(email.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                    {
                        et.Email = email.Text;

                        etudiant.Email = email.Text;
                    }
                    else
                        throw new Exception("Format email invalid");
                }
                if (telephone.Text != string.Empty)
                {
                    if (Regex.IsMatch(telephone.Text, @"^0[1-9]\d{8}$"))
                    {
                        et.Telephone = telephone.Text;

                        etudiant.Telephone = telephone.Text;
                    }
                    else
                        throw new Exception("Format telephone invalid");
                }
                if (adresse.Text != string.Empty)
                {
                  
                        et.Adresse = adresse.Text;

                        etudiant.Adresse = adresse.Text;

                }
                else
                {
                    et.Adresse = "Non Specifié";
                    etudiant.Adresse = "Non Specifié";
                }


                d.Etudiant.InsertOnSubmit(etudiant);
                d.SubmitChanges();

                Etudiants.Add(et);


                ClearForm();
                MessageBox.Show("Etudiant Ajouté avec succées");
                EtudiantsOriginal = (from ee in d.Etudiant
                                     join f in d.Filiere on ee.ID_Filiere equals f.id
                                     select new EtudiantDTO
                                     {
                                         ID_Etudiant = ee.Cne,
                                         Nom = ee.Nom,
                                         Prenom = ee.Prenom,
                                         Date_Naissance = ee.Date_Naissance,
                                         Genre = ee.Genre,
                                         ID_Filiere = ee.ID_Filiere,
                                         Nom_Filiere = f.nom_filiere,
                                         Annee_Inscription = ee.Annee_Inscription,
                                         Email = ee.Email,
                                         Telephone = ee.Telephone,
                                         Adresse = ee.Adresse,
                                         Heures_Absence = ee.Heures_Absence,
                                         Absences_Justifiees = ee.Absences_Justifiees
                                     }).ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void ValidateInput(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            string tag = textBox.Name as string;
            string input = textBox.Text.Trim();
            bool isValid = false;

            switch (tag)
            {
                case "Nom":
                    isValid = Regex.IsMatch(input, @"^[a-zA-Z\u00C0-\u017F\s'-]{2,}$");
                    if (!isValid)
                    {
                        NomErr ="Red";

                    }
                    else
                    {
                        NomErr="#0077B6";

                    }
                    break;

                case "dateNaissance":
                    isValid = DateTime.TryParseExact(input, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out _);
                    if (!isValid)
                    {
                        DateNaissanceErr ="Red";

                    }
                    else
                    {
                        DateNaissanceErr="#0077B6";

                    }
                    break;
                    
                case "Prenom":
                    isValid = Regex.IsMatch(input, @"^[a-zA-Z\u00C0-\u017F\s'-]{2,}$");
                    if (!isValid)
                    {
                        PrenomErr ="Red";
                       
                    }
                    else
                    {
                        PrenomErr="#0077B6";

                    }

                    break;

                case "dateInscription":
                    isValid = Regex.IsMatch(input, @"^\d{4}$") && int.Parse(input) >= 1900 && int.Parse(input) <= DateTime.Now.Year;
                    if (!isValid)
                    {
                        AnneeInsErr ="Red";

                    }
                    else
                    {
                        AnneeInsErr="#0077B6";

                    }
                    break;
                case "NomModify": // Validate Name
                    isValid = Regex.IsMatch(input, @"^[a-zA-Z\u00C0-\u017F\s'-]{2,}$");
                    NomErr = isValid ? "#0077B6" : "Red";
                    break;

                case "PrenomModify": // Validate First Name
                    isValid = Regex.IsMatch(input, @"^[a-zA-Z\u00C0-\u017F\s'-]{2,}$");
                    PrenomErr = isValid ? "#0077B6" : "Red";
                    break;

                case "dateNaissanceModify": // Validate Date of Birth
                    isValid = DateTime.TryParseExact(input, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out _);
                    DateNaissanceErr = isValid ? "#0077B6" : "Red";
                    break;

                case "dateInscriptionModify": // Validate Year of Registration
                    isValid = Regex.IsMatch(input, @"^\d{4}$") && int.TryParse(input, out int year) && year >= 1900 && year <= DateTime.Now.Year;
                    AnneeInsErr = isValid ? "#0077B6" : "Red";
                    break;

               

                case "emailModify": // Validate Email
                    isValid = Regex.IsMatch(input, @"^[^\s@]+@[^\s@]+\.[^\s@]+$");
                    EmailErr = isValid ? "#0077B6" : "Red";
                    break;

                case "telephoneModify": // Validate Phone Number
                    isValid = Regex.IsMatch(input, @"^\+?[0-9]{10,15}$");
                    TelephoneErr = isValid ? "#0077B6" : "Red";
                    break;

            

                case "HeureJust": // Validate Justified Absence Hours
                case "HeureAbs":  // Validate Non-Justified Absence Hours
                    isValid = Regex.IsMatch(input, @"^\d+$");
                    if (tag == "HeureJust")
                        HeureJustErr = isValid ? "#0077B6" : "Red";
                    else
                        HeureAbsErr = isValid ? "#0077B6" : "Red";
                    break;

                default:
                    break;
            }
           
        }



        public void ComboFilierChange(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected filière
             selectedF = ComboFiliere.SelectedItem as string;
            var filteredList = EtudiantsOriginal.Where(etudiant =>
           (string.IsNullOrEmpty(SearchedNom) || etudiant.Nom.ToLower().Contains(SearchedNom)) &&
           (string.IsNullOrEmpty(SearchedPrenom) || etudiant.Prenom.ToLower().Contains(SearchedPrenom))&&
           (etudiant.Nom_Filiere == selectedF || selectedF == "Toutes les fillières")&&
           (string.IsNullOrEmpty(SearchedCne) || etudiant.ID_Etudiant.Contains(SearchedCne))).ToList();

            Etudiants.Clear();
            foreach (var item in filteredList)
            {

                Etudiants.Add(item);
            }



        }

        public void SeachByName(object sender, TextChangedEventArgs e)
        {

            TextBox n = sender as TextBox;
                SearchedNom = searchNom.Text;
                
                SearchedPrenom = searchPrenom.Text;
                SearchedCne = searchCne.Text;

            var filteredList = EtudiantsOriginal.Where(etudiant =>
         (string.IsNullOrEmpty(SearchedNom) || etudiant.Nom.ToLower().Contains(SearchedNom)) &&
         (string.IsNullOrEmpty(SearchedPrenom) || etudiant.Prenom.ToLower().Contains(SearchedPrenom))&&
         (etudiant.Nom_Filiere == selectedF || selectedF == "Toutes les fillières")&&
         (string.IsNullOrEmpty(SearchedCne) || etudiant.ID_Etudiant.Contains(SearchedCne))).ToList();

            Etudiants.Clear();
            foreach (var item in filteredList) {
            
                    Etudiants.Add(item);
                }




        }
        public void openmodify(object sender, RoutedEventArgs e)
        {
            // Ensure an item is selected
            if (grid.SelectedItem == null)
            {
                MessageBox.Show("Sélectionnez un étudiant à modifier.");
                return;
            }

            // Cast the selected item to EtudiantDTO
            EtudiantDTO selectedEtudiant = (EtudiantDTO)grid.SelectedItem;

            // Set visibility of the modify form
            ModifyFormVisibility = "Visible";
            ComboModify.ItemsSource = (from f in d.Filiere
                                    select f.nom_filiere).ToList();

            // Populate the fields
            NomModify.Text = selectedEtudiant.Nom;
            PrenomModify.Text = selectedEtudiant.Prenom;
            CneModify.Text = selectedEtudiant.ID_Etudiant;
            dateNaissanceModify.Text = selectedEtudiant.Date_Naissance.ToString("dd/MM/yyyy");
            dateInscriptionModify.Text = selectedEtudiant.Annee_Inscription.ToString();
            HeureAbs.Text = selectedEtudiant.Heures_Absence.ToString();
            HeureJust.Text = selectedEtudiant.Absences_Justifiees.ToString();


            // Handle gender selection
            if (selectedEtudiant.Genre == "Masculin")
            {
                maleModify.IsChecked = true;
            }
            else if (selectedEtudiant.Genre == "Féminin")
            {
                femaleModif.IsChecked = true;
            }

            // Populate the filiere combo box (if applicable)
            if (!string.IsNullOrEmpty(selectedEtudiant.Nom_Filiere))
            {
                ComboModify.SelectedItem = selectedEtudiant.Nom_Filiere;
            }

            // Populate additional fields
            emailModify.Text = selectedEtudiant.Email;
            telephoneModify.Text = selectedEtudiant.Telephone;
            adresseModify.Text = selectedEtudiant.Adresse;
        }

        public void collpaseModify(Object Sender, RoutedEventArgs e)
        {
            ModifyFormVisibility="Collapsed";
        }
        public void modify(object sender, RoutedEventArgs e)
        {
            try
            {
                // Cast the selected item to EtudiantDTO
                EtudiantDTO et = (EtudiantDTO)grid.SelectedItem;

                // Query the database to find the matching student
                var student = (from etu in d.Etudiant
                               where etu.Cne == et.ID_Etudiant
                               select etu).FirstOrDefault();

                // Check if the student exists
                if (student == null)
                {
                    MessageBox.Show("Étudiant introuvable dans la base de données.");
                    return;
                }

                // Retrieve the ID of the selected Filiere
                var idfil = (from f in d.Filiere
                             where f.nom_filiere == ComboModify.SelectedItem.ToString()
                             select f.id).FirstOrDefault();

                // Update the student properties
                student.Nom = NomModify.Text;
                student.Prenom = PrenomModify.Text;
                student.Date_Naissance = DateTime.Parse(dateNaissanceModify.Text); // Ensure date format is valid
                student.Annee_Inscription = int.Parse(dateInscriptionModify.Text); // Ensure numeric input
                student.Genre = maleModify.IsChecked == true ? "Masculin" : "Féminin";
                student.ID_Filiere = idfil;
                student.Email = emailModify.Text;
                student.Telephone = telephoneModify.Text;
                student.Adresse = adresseModify.Text;
                student.Heures_Absence = string.IsNullOrEmpty(HeureAbs.Text) ? 0 : int.Parse(HeureAbs.Text);
                student.Absences_Justifiees = string.IsNullOrEmpty(HeureJust.Text) ? 0 : int.Parse(HeureJust.Text);
                // Save changes to the database
                d.SubmitChanges();

                // Update the ObservableCollection<EtudiantDTO>
                var etudiantInCollection = etudiants.FirstOrDefault(ete => ete.ID_Etudiant == et.ID_Etudiant);

                if (etudiantInCollection != null)
                {
                    etudiantInCollection.Nom = NomModify.Text;
                    etudiantInCollection.Prenom = PrenomModify.Text;
                    etudiantInCollection.Date_Naissance = DateTime.Parse(dateNaissanceModify.Text);
                    etudiantInCollection.Annee_Inscription = int.Parse(dateInscriptionModify.Text);
                    etudiantInCollection.Genre = maleModify.IsChecked == true ? "Masculin" : "Féminin";
                    etudiantInCollection.ID_Filiere = idfil;
                    etudiantInCollection.Email = emailModify.Text;
                    etudiantInCollection.Telephone = telephoneModify.Text;
                    etudiantInCollection.Adresse = adresseModify.Text;
                    etudiantInCollection.Nom_Filiere = (string)ComboModify.SelectedItem;
                    etudiantInCollection.Heures_Absence = string.IsNullOrEmpty(HeureAbs.Text) ? 0 : int.Parse(HeureAbs.Text);
                    etudiantInCollection.Absences_Justifiees = string.IsNullOrEmpty(HeureJust.Text) ? 0 : int.Parse(HeureJust.Text);
                    grid.ItemsSource = null;
                    grid.ItemsSource = etudiants;

                }

                // Notify the user
                MessageBox.Show("Modification réussie.");

                // Hide the modify form
                ModifyFormVisibility = "Collapsed";
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show($"Erreur lors de la modification : {ex.Message}");
            }
        }


    }
}
    