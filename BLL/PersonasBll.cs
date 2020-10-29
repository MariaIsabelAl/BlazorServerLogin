using Microsoft.EntityFrameworkCore;
using BlazorServerLogin.Data;
using BlazorServerLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlazorServerLogin.BLL
{
    public class PersonasBLL
    {
        private ApplicationDbContext _contexto;
        public PersonasBLL(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }
        public bool Guardar(Personas personas)
        {
            if (!Existe(personas.PersonaId))//si no existe insertamos

                return Insertar(personas);
            else
                return Modificar(personas);

        }

        private  bool Insertar(Personas personas)
        {
            bool paso = false;
           

            try
            {
                _contexto.Personas.Add(personas);
                paso = _contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;

            }
     
            return paso;
        }


        private  bool Modificar(Personas personas)
        {
            bool paso = false;
            

            try
            {

                _contexto.Entry(personas).State = EntityState.Modified;
                paso = _contexto.SaveChanges() > 0;

            }
            catch (Exception)
            {
                throw;
            }
            
            return paso;
        }

        public bool Eliminar(int id)
        {
            bool paso = false;
            

            try
            {

                var auxPersona = _contexto.Personas.Find(id);
                    _contexto.Personas.Remove(auxPersona);//remueve la entidad
                        paso = _contexto.SaveChanges() > 0;

                    
                
            }
            catch (Exception)
            {
                throw;
            }
            
            return paso;
        }

        public Personas Buscar(int id)
        {
           
            Personas personas;

            try
            {
                personas = _contexto.Personas.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
            
            return personas;

        }

        public List<Personas> GetList(Expression<Func<Personas, bool>> persona)
        {
            List<Personas> lista = new List<Personas>();
            

            try
            {
                lista = _contexto.Personas.Where(persona).ToList();
            }
            catch (Exception)
            {

                throw;
            }
          

            return lista;
        }

        private bool Existe(int id)
        {
            
            bool encontrado = false;

            try
            {
                encontrado = _contexto.Personas.Any(p => p.PersonaId == id);

            }
            catch (Exception)
            {
                throw;
            }
            

            return encontrado;

        }

       

    }
}