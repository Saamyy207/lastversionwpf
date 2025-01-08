using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionaireEtudiant
{
    public class StatsDTO
    {
        public string Field { get; set; }
        public int RegistrationCount { get; set; }

        public StatsDTO() { }

        public StatsDTO(string field, int registrationCount)
        {
            Field = field;
            RegistrationCount = registrationCount;
        }
    }
}