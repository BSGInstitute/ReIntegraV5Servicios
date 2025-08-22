using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: OcurrenciaAlternoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 16/08/2022
    /// <summary>
    /// Gestión general de T_OcurrenciaAlterno
    /// </summary>
    public class OcurrenciaAlternoRepository : GenericRepository<TOcurrenciaAlterno>, IOcurrenciaAlternoRepository
    {
        private Mapper _mapper;

        public OcurrenciaAlternoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOcurrenciaAlterno, OcurrenciaAlterno>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TOcurrenciaAlterno MapeoEntidad(OcurrenciaAlterno entidad)
        {
            try
            {
                //crea la entidad padre
                TOcurrenciaAlterno modelo = _mapper.Map<TOcurrenciaAlterno>(entidad);

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

        public TOcurrenciaAlterno Add(OcurrenciaAlterno entidad)
        {
            try
            {
                var OcurrenciaAlterno = MapeoEntidad(entidad);
                base.Insert(OcurrenciaAlterno);
                return OcurrenciaAlterno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOcurrenciaAlterno Update(OcurrenciaAlterno entidad)
        {
            try
            {
                var OcurrenciaAlterno = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                OcurrenciaAlterno.RowVersion = entidadExistente.RowVersion;

                base.Update(OcurrenciaAlterno);
                return OcurrenciaAlterno;
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


        public IEnumerable<TOcurrenciaAlterno> Add(IEnumerable<OcurrenciaAlterno> listadoEntidad)
        {
            try
            {
                List<TOcurrenciaAlterno> listado = new List<TOcurrenciaAlterno>();
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

        public IEnumerable<TOcurrenciaAlterno> Update(IEnumerable<OcurrenciaAlterno> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TOcurrenciaAlterno> listado = new List<TOcurrenciaAlterno>();
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_OcurrenciaAlterno.
        /// </summary>
        /// <returns> List<OcurrenciaAlternoDTO> </returns>
        public IEnumerable<OcurrenciaAlternoDTO> ObtenerOcurrenciaAlterno()
        {
            try
            {
                List<OcurrenciaAlternoDTO> rpta = new List<OcurrenciaAlternoDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre,NombreM,NombreCS,IdFaseOportunidad,IdActividadCabecera,IdPlantilla_Speech,IdEstadoOcurrencia,Oportunidad,RequiereLlamada,
	                    Roles,Color,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion,IdPersonalAreaTrabajo,IdTipoOcurrencia
                    FROM com.T_OcurrenciaAlterno
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OcurrenciaAlternoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_OcurrenciaAlterno para mostrarse en combo.
        /// </summary>
        /// <returns> List<OcurrenciaAlternoComboDTO> </returns>
        public IEnumerable<OcurrenciaAlternoComboDTO> ObtenerCombo()
        {
            try
            {
                List<OcurrenciaAlternoComboDTO> rpta = new List<OcurrenciaAlternoComboDTO>();
                var query = @"SELECT Id,Nombre,IdFaseOportunidad FROM com.T_OcurrenciaAlterno WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OcurrenciaAlternoComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtener Información de Ocurrencia por Id
        /// </summary>
        /// <param name="idOcurrencia">Id de la Ocurrencia</param>
        /// <returns> OcurrenciaAlternoDTO </returns>
        public OcurrenciaAlternoDTO ObtenerOcurrenciaPorActividad(int idOcurrencia)
        {
            try
            {
                string queryOcurrencia = "Select Id,Nombre,IdFaseOportunidad,IdActividadCabecera,IdPlantilla_Speech  AS IdPlantillaSpeech, IdEstadoOcurrencia,Oportunidad,RequiereLlamada,Roles,Color From com.T_OcurrenciaReporteAlterno Where Id=@IdOcurrencia";
                var resultado = _dapperRepository.FirstOrDefault(queryOcurrencia, new { IdOcurrencia = idOcurrencia });
                return JsonConvert.DeserializeObject<OcurrenciaAlternoDTO>(resultado);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_OcurrenciaAlterno.
        /// </summary>
        /// <returns> List<OcurrenciaAlternoDTO> </returns>
        public OcurrenciaAlterno ObtenerPorId(int idOcurrencia)
        {
            try
            {
                OcurrenciaAlterno rpta = new OcurrenciaAlterno();
                var query = @"
                    SELECT
                        Id,
	                    Nombre,
	                    IdFaseOportunidad,
	                    IdActividadCabecera,
	                    IdPlantilla_Speech AS IdPlantillaSpeech,
	                    IdEstadoOcurrencia,
	                    Oportunidad,
	                    RequiereLlamada,
	                    Roles,
	                    Color,
	                    Estado,
	                    FechaCreacion,
	                    FechaModificacion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM com.T_OcurrenciaAlterno
                    WHERE Estado = 1 AND Id = @idOcurrencia";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idOcurrencia });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<OcurrenciaAlterno>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_OcurrenciaAlterno.
        /// </summary>
        /// <returns> List<OcurrenciaAlternoDTO> </returns>
        public async Task<OcurrenciaAlterno> ObtenerPorIdAsync(int idOcurrencia)
        {
            try
            {
                OcurrenciaAlterno rpta = new OcurrenciaAlterno();
                var query = @"
                    SELECT
                        Id,
	                    Nombre,
	                    IdFaseOportunidad,
	                    IdActividadCabecera,
	                    IdPlantilla_Speech AS IdPlantillaSpeech,
	                    IdEstadoOcurrencia,
	                    Oportunidad,
	                    RequiereLlamada,
	                    Roles,
	                    Color,
	                    Estado,
	                    FechaCreacion,
	                    FechaModificacion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM com.T_OcurrenciaAlterno
                    WHERE Estado = 1 AND Id = @idOcurrencia";
                var resultado = await _dapperRepository.FirstOrDefaultAsync(query, new { idOcurrencia });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<OcurrenciaAlterno>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
