using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
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

namespace GestionaireEtudiant
{
    public partial class Stats : UserControl
    {
        DataClasses1DataContext _db = new DataClasses1DataContext();

        private ObservableCollection<StatsDTO> _data;

        public ObservableCollection<StatsDTO> Data
        {
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged("Data");
            }
        }


        public Stats()
        {
            InitializeComponent();
            DataContext = this;
            LoadData();
        }


        private void LoadData()
        {

            try
            {
                var statsQuery = from f in _db.Filiere
                                 join e in _db.Etudiant on f.id equals e.ID_Filiere into grouped
                                 select new StatsDTO
                                 {
                                     Field = f.nom_filiere,
                                     RegistrationCount = grouped.Count()
                                 };

                Data = new ObservableCollection<StatsDTO>(statsQuery);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
