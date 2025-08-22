using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: EsquemaEvaluacionDetalleRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 08/05/2023
    /// Version: 1.0
    /// <summary>
    /// Gestión general de T_EsquemaEvaluacionDetalle
    /// </summary>
    public class EsquemaEvaluacionDetalleRepository : GenericRepository<TEsquemaEvaluacionDetalle>, IEsquemaEvaluacionDetalleRepository
    {
        private Mapper _mapper;

        public EsquemaEvaluacionDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEsquemaEvaluacionDetalle, EsquemaEvaluacionDetalle>(MemberList.None).ReverseMap();

            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TEsquemaEvaluacionDetalle MapeoEntidad(EsquemaEvaluacionDetalle entidad)
        {
            try
            {
                TEsquemaEvaluacionDetalle modelo = _mapper.Map<TEsquemaEvaluacionDetalle>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TEsquemaEvaluacionDetalle Add(EsquemaEvaluacionDetalle entidad)
        {
            try
            {
                var perfilAtrabajoCoeficiente = MapeoEntidad(entidad);
                base.Insert(perfilAtrabajoCoeficiente);
                return perfilAtrabajoCoeficiente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TEsquemaEvaluacionDetalle Update(EsquemaEvaluacionDetalle entidad)
        {
            try
            {
                var perfilAtrabajoCoeficiente = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                perfilAtrabajoCoeficiente.RowVersion = entidadExistente.RowVersion;

                base.Update(perfilAtrabajoCoeficiente);
                return perfilAtrabajoCoeficiente;
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
        public IEnumerable<TEsquemaEvaluacionDetalle> Add(IEnumerable<EsquemaEvaluacionDetalle> listadoEntidad)
        {
            try
            {
                List<TEsquemaEvaluacionDetalle> listado = new List<TEsquemaEvaluacionDetalle>();
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
        public IEnumerable<TEsquemaEvaluacionDetalle> Update(IEnumerable<EsquemaEvaluacionDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEsquemaEvaluacionDetalle> listado = new List<TEsquemaEvaluacionDetalle>();
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/08/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EsquemaEvaluacionDetalle? ObtenerPorId(int id)
        {
            try
            {
                var query = $@"
                    SELECT 
                        Id,
		                IdEsquemaEvaluacion,
		                IdCriterioEvaluacion,
		                Ponderacion,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
                    FROM 
                        pla.T_EsquemaEvaluacionDetalle
                    WHERE
                        Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<EsquemaEvaluacionDetalle>(resultado)!;
                }
                 return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#EEPGR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }


        public IEnumerable<EsquemaEvaluacionDetalle> ObtenerPorIdEsquemaEvaluacion(int idEsquemaEvaluacion)
        {
            try
            {
                var query = @"
                         SELECT 
                        Id,
                        IdEsquemaEvaluacion,
                        Ponderacion,
                        IdCriterioEvaluacion,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion
                    FROM 
                        pla.T_EsquemaEvaluacionDetalle
                        WHERE Estado = 1 AND IdEsquemaEvaluacion=@idEsquemaEvaluacion";
                var resultado = _dapperRepository.QueryDapper(query, new { idEsquemaEvaluacion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<EsquemaEvaluacionDetalle>>(resultado)!;
                }
                return new List<EsquemaEvaluacionDetalle>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#PCPwR-OPIP-001@Error en ObtenerPorIdEsquemaEvaluacion(), {ex.Message}");
            }
        }


    }
}
