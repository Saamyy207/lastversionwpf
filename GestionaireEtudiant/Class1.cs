using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionaireEtudiant
{
    public class EtudiantDTO
    {
        public string ID_Etudiant { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime Date_Naissance { get; set; }
        public string Genre { get; set; }
        public int ID_Filiere { get; set; }
        public string Nom_Filiere { get; set; }
        public int Annee_Inscription { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Adresse { get; set; }
        public int? Heures_Absence { get; set; }
        public int? Absences_Justifiees { get; set; }
    }

}
