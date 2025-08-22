using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class Error
    {
        public int Id { get; }
        public string Nombre { get; }
        public string Descripcion { get; }

        public Error()
        {

        }
        public Error(int id, string nombre, string descripcion)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
        }
    }

    /// <summary>
    /// Genera una lista de errores
    /// </summary>
    public class ListError
    {
        private List<Error> ListaErrores { get; }

        public ListError()
        {
            ListaErrores = new List<Error>();
        }

        /// <summary>
        /// Define si la lista tiene errores o no
        /// </summary>
        public bool TieneErrores
        {
            get { return this.ExistenErrores(); }
        }

        public void AgregarError(Error error)
        {
            try
            {
                ListaErrores.Add(error);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<Error> ObtenerErrores()
        {
            try
            {
                return ListaErrores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public Error ObtenerErrorPorId(int idError)
        {
            try
            {
                return ListaErrores.Where(x => x.Id == idError).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private bool ExistenErrores()
        {
            try
            {
                if (ListaErrores != null && ListaErrores.Count() > 0)
                {
                    return true;
                }
                return false;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
