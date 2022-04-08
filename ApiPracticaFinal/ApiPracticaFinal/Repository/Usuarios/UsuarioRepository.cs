using ApiPracticaFinal.Models;
using ApiPracticaFinal.Models.DTO.UsuariosDTO;
using ApiPracticaFinal.Resultados;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiPracticaFinal.Repository.Usuarios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        dd4snj9pkf64vpContext context = new dd4snj9pkf64vpContext();
        private readonly string key;

        public UsuarioRepository(string key)
        {
            this.key = key;
        }
        public string Authenticate(string email, string password)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, email)
                    }),
                Expires = DateTime.UtcNow.AddMinutes(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            return await context.Usuarios.Where(x => x.Activo == true).ToListAsync();
        }

        public async Task<Usuario> Signup(UsuarioSignUp oUser)
        {
            oUser.Password = BCrypt.Net.BCrypt.HashPassword(oUser.Password);

            Usuario u = new Usuario
            {
                Nombre = oUser.Nombre,
                Email = oUser.Email,
                Password = oUser.Password,
                Rol = 2,
                Activo = true
            };

            if(u != null)
            {
                context.Usuarios.Add(u);
                await context.SaveChangesAsync();
                return u;
            }
            else
            {
                throw new Exception("No se pudo registrar el usuario");
            }
            
        }

        public ResultadosApi Login(UsuarioLogin usu)
        {
            var token = Authenticate(usu.Email, usu.Password);

            //var result = context.Usuarios.FirstOrDefault(x => x.Email == usu.Email && x.Password == usu.Password);

            ResultadosApi resultado = new ResultadosApi();

            var u = context.Usuarios.SingleOrDefault(x => x.Email == usu.Email);
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(usu.Password, u.Password);

            if (isValidPassword)
            {
                if (token == null)
                {
                    resultado.Error = "No autorizado";
                    resultado.Ok = false;
                    return resultado;
                }
                else
                {
                    resultado.Ok = true;
                    resultado.InfoAdicional = "Login exitoso";
                    Login lg = new Login(u.Email, token, u.Rol);
                    resultado.Respuesta = lg;
                    return resultado;
                }
            }
            else
            {
                resultado.Error = "Usuario y/o contraseña incorrectos";
                return resultado;
            }
        }

        public async Task<bool> UpdatePassword(UsuarioUpdatePass usu)
        {
            UsuarioUpdatePass u = new UsuarioUpdatePass
            {
                Email = usu.Email,
                PasswordNueva = usu.PasswordNueva,
                PasswordVieja = usu.PasswordVieja
            };

            var usuario = context.Usuarios.SingleOrDefault(x => x.Email == usu.Email);
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(usu.PasswordVieja, usuario.Password);
            if (isValidPassword)
            {
                usuario.Password = BCrypt.Net.BCrypt.HashPassword(usu.PasswordNueva);
                context.Usuarios.Update(usuario);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new Exception("Usuario y/o password incorrectos");
            }
        }

        public async Task<bool> Delete(int id)
        {
            var usuario = await context.Usuarios.FindAsync(id);
            if(usuario != null)
            {
                usuario.Activo = false;
                context.Usuarios.Update(usuario);
                await context.SaveChangesAsync();
                return true;
            }
            else
            {
                throw new Exception("No se encontro el usuario");
            }
        }
    }
}
