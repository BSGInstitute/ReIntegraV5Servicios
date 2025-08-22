using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PEspecificoCriterioEvaluacionRepository
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 22/07/2023
    /// <summary>
    /// Gestión general de pla.T_PEespecificoCriterioEvaluacion
    /// </summary>
    public class PEspecificoCriterioEvaluacionRepository : GenericRepository<TPespecificoCriterioEvaluacion>, IPEspecificoCriterioEvaluacionRepository
    {
        private Mapper _mapper;

        public PEspecificoCriterioEvaluacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecificoCriterioEvaluacion, PEspecificoCriterioEvaluacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPespecificoCriterioEvaluacion MapeoEntidad(PEspecificoCriterioEvaluacion entidad)
        {
            try
            {
                //crea la entidad padre
                TPespecificoCriterioEvaluacion modelo = _mapper.Map<TPespecificoCriterioEvaluacion>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoCriterioEvaluacion Add(PEspecificoCriterioEvaluacion entidad)
        {
            try
            {
                var PEspecificoCriterioEvaluacion = MapeoEntidad(entidad);
                base.Insert(PEspecificoCriterioEvaluacion);
                return PEspecificoCriterioEvaluacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPespecificoCriterioEvaluacion Update(PEspecificoCriterioEvaluacion entidad)
        {
            try
            {
                var PEspecificoCriterioEvaluacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PEspecificoCriterioEvaluacion.RowVersion = entidadExistente.RowVersion;

                base.Update(PEspecificoCriterioEvaluacion);
                return PEspecificoCriterioEvaluacion;
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


        public IEnumerable<TPespecificoCriterioEvaluacion> Add(IEnumerable<PEspecificoCriterioEvaluacion> listadoEntidad)
        {
            try
            {
                List<TPespecificoCriterioEvaluacion> listado = new List<TPespecificoCriterioEvaluacion>();
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

        public IEnumerable<TPespecificoCriterioEvaluacion> Update(IEnumerable<PEspecificoCriterioEvaluacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPespecificoCriterioEvaluacion> listado = new List<TPespecificoCriterioEvaluacion>();
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
