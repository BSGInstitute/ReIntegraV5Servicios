using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PespecificoCursoAdicionalRepository
    /// Autor Modificacion: Gretel Canasa.
    /// Fecha: 31/08/2023
    /// <summary>
    /// Gestión general de T_PespecificoCursoAdicional
    /// </summary>
    public class PespecificoCursoAdicionalRepository : GenericRepository<TPespecificoCursoAdicional>, IPespecificoCursoAdicionalRepository
    {
        private Mapper _mapper;

        public PespecificoCursoAdicionalRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PespecificoCursoAdicional, TPespecificoCursoAdicional>(MemberList.None).ReverseMap();
            }
            );
            _mapper = new Mapper(config);

        }

        #region Metodos Base
        private TPespecificoCursoAdicional MapeoEntidad(PespecificoCursoAdicional entidad)
        {
            try
            {
                //crea la entidad padre
                TPespecificoCursoAdicional modelo = _mapper.Map<TPespecificoCursoAdicional>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoCursoAdicional Add(PespecificoCursoAdicional entidad)
        {
            try
            {
                var PespecificoCursoAdicional = MapeoEntidad(entidad);
                base.Insert(PespecificoCursoAdicional);
                return PespecificoCursoAdicional;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoCursoAdicional Update(PespecificoCursoAdicional entidad)
        {
            try
            {
                var PespecificoCursoAdicional = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PespecificoCursoAdicional.RowVersion = entidadExistente.RowVersion;

                base.Update(PespecificoCursoAdicional);
                return PespecificoCursoAdicional;
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


        public IEnumerable<TPespecificoCursoAdicional> Add(IEnumerable<PespecificoCursoAdicional> listadoEntidad)
        {
            try
            {
                List<TPespecificoCursoAdicional> listado = new List<TPespecificoCursoAdicional>();
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

        public IEnumerable<TPespecificoCursoAdicional> Update(IEnumerable<PespecificoCursoAdicional> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPespecificoCursoAdicional> listado = new List<TPespecificoCursoAdicional>();
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
        public bool Exist(int id)
        {
            try
            {
                base.Exist(id);
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
