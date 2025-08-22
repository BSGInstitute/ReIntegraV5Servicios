using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class CriterioEvaluacionProcesoRepository : GenericRepository<TCriterioEvaluacionProceso>, ICriterioEvaluacionProcesoRepository
    {
        private Mapper _mapper;
        public CriterioEvaluacionProcesoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioEvaluacionProceso, CriterioEvaluacionProceso>(MemberList.None).ReverseMap();
                cfg.CreateMap<CriterioEvaluacionProceso, CriterioEvaluacionProcesoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<CriterioEvaluacionProceso, TCriterioEvaluacionProceso>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TCriterioEvaluacionProceso MapeoEntidad(CriterioEvaluacionProceso entidad)
        {
            try
            {
                TCriterioEvaluacionProceso modelo = _mapper.Map<TCriterioEvaluacionProceso>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCriterioEvaluacionProceso Add(CriterioEvaluacionProceso entidad)
        {
            try
            {
                var CriterioEvaluacionProceso = MapeoEntidad(entidad);
                base.Insert(CriterioEvaluacionProceso);
                return CriterioEvaluacionProceso;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCriterioEvaluacionProceso Update(CriterioEvaluacionProceso entidad)
        {
            try
            {
                var CriterioEvaluacionProceso = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CriterioEvaluacionProceso.RowVersion = entidadExistente.RowVersion;

                base.Update(CriterioEvaluacionProceso);
                return CriterioEvaluacionProceso;
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


        public IEnumerable<TCriterioEvaluacionProceso> Add(IEnumerable<CriterioEvaluacionProceso> listadoEntidad)
        {
            try
            {
                List<TCriterioEvaluacionProceso> listado = new List<TCriterioEvaluacionProceso>();
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

        public IEnumerable<TCriterioEvaluacionProceso> Update(IEnumerable<CriterioEvaluacionProceso> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCriterioEvaluacionProceso> listado = new List<TCriterioEvaluacionProceso>();
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

        /// Autor:  Marco Jose Villanueva Torres.
        /// Fecha: 04/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PreguntaCategoria.
        /// </summary>
        /// <returns> List<CategoriaPregunta> </returns>
        public IEnumerable<CriterioEvaluacionProcesoDTO> Obtener()
        {
            try
            {
                List<CriterioEvaluacionProcesoDTO> rpta = new List<CriterioEvaluacionProcesoDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre
                    FROM gp.T_CriterioEvaluacionProceso
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CriterioEvaluacionProcesoDTO>>(resultado);

                }



                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Marco Jose Villanueva Torres.
        /// Fecha: 04/04/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns >CategoriaPregunta || null</returns>
        public CriterioEvaluacionProceso? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_CriterioEvaluacionProceso
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<CriterioEvaluacionProceso>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }


    }
}
