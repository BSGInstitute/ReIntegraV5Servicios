using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PEspecificoConsumoRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 09/06/2023
    /// <summary>
    /// Gestión general de T_PespecificoConsumo
    /// </summary>
    public class PEspecificoConsumoRepository : GenericRepository<TPespecificoConsumo>, IPEspecificoConsumoRepository
    {
        private Mapper _mapper;
        public PEspecificoConsumoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecificoConsumo, PEspecificoConsumo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPespecificoConsumo MapeoEntidad(PEspecificoConsumo entidad)
        {
            try
            {
                //crea la entidad padre
                TPespecificoConsumo modelo = _mapper.Map<TPespecificoConsumo>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoConsumo Add(PEspecificoConsumo entidad)
        {
            try
            {
                var pEspecificoConsumo = MapeoEntidad(entidad);
                base.Insert(pEspecificoConsumo);
                return pEspecificoConsumo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoConsumo Update(PEspecificoConsumo entidad)
        {
            try
            {
                var pEspecificoConsumo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                pEspecificoConsumo.RowVersion = entidadExistente.RowVersion;

                base.Update(pEspecificoConsumo);
                return pEspecificoConsumo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TPespecificoConsumo> Add(IEnumerable<PEspecificoConsumo> listadoEntidad)
        {
            try
            {
                List<TPespecificoConsumo> listado = new List<TPespecificoConsumo>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TPespecificoConsumo> Update(IEnumerable<PEspecificoConsumo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPespecificoConsumo> listado = new List<TPespecificoConsumo>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
