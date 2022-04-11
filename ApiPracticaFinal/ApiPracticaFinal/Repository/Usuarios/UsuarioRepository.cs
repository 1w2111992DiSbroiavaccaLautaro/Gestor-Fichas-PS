﻿using ApiPracticaFinal.Models;
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

        //metodo accesible solo para usuarios con rol 1
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

        //metodo accesible para todos los usuarios
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

        //metodo accesible solo para usuarios con rol 1
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

        //metodo accesible solo para usuarios con rol 1
        public async Task<Usuario> UpdateRol(UsuarioRolDTO usu)
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Idusuario == usu.Id);
            if (usuario != null)
            {
                usuario.Rol = usu.Rol;
                context.Usuarios.Update(usuario);
                await context.SaveChangesAsync();
                return usuario;
            }
            else
            {
                throw new Exception("Usuario no encontrado");
            }
        }

        //validar desde el fron que se envie un mail correcto
        //metodo accesible para todos los usuarios
        public async Task<Usuario> UpdateCredenciales(UsuarioCredencialDTO usu)
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Idusuario == usu.Id);
            if(usuario != null)
            {
                if(usu.Email == "" || usu.Email == "string")
                {
                    usuario.Email = usuario.Email;
                }
                else
                {
                    usuario.Email = usu.Email ?? usuario.Email;
                }
                if(usu.Nombre == "" || usu.Nombre == "string")
                {
                    usuario.Nombre = usuario.Nombre;
                }
                else
                {
                    usuario.Nombre = usu.Nombre ?? usuario.Nombre;
                }
                context.Usuarios.Update(usuario);
                await context.SaveChangesAsync();
                return usuario;
            }
            else
            {
                throw new Exception("No se encontro el usuario");
            }
        }
    }
}
