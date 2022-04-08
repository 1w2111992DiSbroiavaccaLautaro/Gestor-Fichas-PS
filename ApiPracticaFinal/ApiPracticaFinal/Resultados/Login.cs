using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPracticaFinal.Resultados
{
    public class Login
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public int Rol { get; set; }

        public Login(string Email, string Token, int Rol)
        {
            this.Token = Token;
            this.Email = Email;
            this.Rol = Rol;
        }
    }
}
