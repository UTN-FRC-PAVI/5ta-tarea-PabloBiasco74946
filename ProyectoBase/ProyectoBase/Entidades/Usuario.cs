using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoBase.Entidades
{
    public class Usuario
    {
        private string NombreUsu;
        private string Pass;
        public Usuario(string nombreUsuario, string password)
        {
            NombreUsu = nombreUsuario;
            Pass = password;

        }

        public string NombreDeUsuario
        {
            get => NombreUsu;
            set => NombreUsu = value;
        }

        public string Password
        {
            get => Pass;
            set => Pass = value;
        }

    }
}
