using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.CampaniaMailingWhatsapp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.CampaniaMailingWhatsapp
{
    public class CategoriaObjetoFiltroRepositorio : GenericRepository<TCategoriaObjetoFiltro>, ICategoriaObjetoFiltroRepositorio
    {
        public CategoriaObjetoFiltroRepositorio(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
        }
        public IEnumerable<CategoriaObjetoFiltroDTO> GetBy(Expression<Func<TCategoriaObjetoFiltro, bool>> filter)
        {
            IEnumerable<TCategoriaObjetoFiltro> listado = base.GetBy(filter);
            List<CategoriaObjetoFiltroDTO> listadoBO = new List<CategoriaObjetoFiltroDTO>();
            //foreach (var itemEntidad in listado)
            //{
            //    CategoriaObjetoFiltroDTO objetoBO = Mapper.Map<TCategoriaObjetoFiltro, CategoriaObjetoFiltroDTO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
            //    listadoBO.Add(objetoBO);
            //}

            return listadoBO;
        }
        public CategoriaObjetoFiltroDTO FirstById(int id)
        {
            try
            {
                TCategoriaObjetoFiltro entidad = base.FirstById(id);
                CategoriaObjetoFiltroDTO objetoBO = new CategoriaObjetoFiltroDTO();
                //Mapper.Map<TCategoriaObjetoFiltro, CategoriaObjetoFiltroDTO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CategoriaObjetoFiltroDTO FirstBy(Expression<Func<TCategoriaObjetoFiltro, bool>> filter)
        {
            try
            {
                TCategoriaObjetoFiltro entidad = base.FirstBy(filter);
                //CategoriaObjetoFiltroDTO objetoBO = Mapper.Map<TCategoriaObjetoFiltro, CategoriaObjetoFiltroDTO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CategoriaObjetoFiltroDTO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCategoriaObjetoFiltro entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Insert(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(IEnumerable<CategoriaObjetoFiltroDTO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Insert(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(CategoriaObjetoFiltroDTO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCategoriaObjetoFiltro entidad = MapeoEntidad(objetoBO);

                bool resultado = base.Update(entidad);
                if (resultado)
                    AsignacionId(entidad, objetoBO);

                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool Update(IEnumerable<CategoriaObjetoFiltroDTO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado = Update(objetoBO);
                    if (resultado == false)
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void AsignacionId(TCategoriaObjetoFiltro entidad, CategoriaObjetoFiltroDTO objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                   // objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private TCategoriaObjetoFiltro MapeoEntidad(CategoriaObjetoFiltroDTO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCategoriaObjetoFiltro entidad = new TCategoriaObjetoFiltro();
                //entidad = Mapper.Map<CategoriaObjetoFiltroDTO, TCategoriaObjetoFiltro>(objetoBO,
                //    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los filtros disponibles para conjunto lista
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerParaConjuntoAnuncio()
        {
            return this.GetBy(x => x.AplicaConjuntoLista, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
        }
    }
}
