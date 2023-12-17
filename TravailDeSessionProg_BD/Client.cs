using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravailDeSessionProg_BD
{
    class Client
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string NumTel { get; set; }
        public string Email { get; set; }
        public string PetiteInfo { get => Id.ToString() + " " + Nom.ToString(); }
    }
}
