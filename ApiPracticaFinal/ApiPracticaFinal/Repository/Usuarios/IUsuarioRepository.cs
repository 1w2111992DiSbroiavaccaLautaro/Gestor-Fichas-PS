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
        Task<List<UsuarioListaDTO>> GetUsuariosAsync();
        Task<bool> UpdatePassword(UsuarioUpdatePass usu);
        Task<UsuarioDTO> Signup(UsuarioSignUp oUser);
        Task<bool> Delete(int id);
        ResultadosApi Login(UsuarioLogin usu);
        Task<UsuarioDTO> UpdateRol(UsuarioRolDTO usu);
        Task<UsuarioDTO> UpdateCredenciales(UsuarioCredencialDTO usu);
        
    }
}
