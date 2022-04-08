using ApiPracticaFinal.Models;
using ApiPracticaFinal.Models.DTO.UsuariosDTO;
using ApiPracticaFinal.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPracticaFinal.Repository.Usuarios
{
    public interface IUsuarioRepository
    {
        string Authenticate(string email, string password);
        Task<List<Usuario>> GetUsuariosAsync();
        Task<bool> UpdatePassword(UsuarioUpdatePass usu);
        Task<Usuario> Signup(UsuarioSignUp oUser);
        Task<bool> Delete(int id);
        ResultadosApi Login(UsuarioLogin usu);
    }
}
