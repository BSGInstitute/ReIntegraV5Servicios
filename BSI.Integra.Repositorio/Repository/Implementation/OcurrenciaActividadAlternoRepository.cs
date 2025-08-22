using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: OcurrenciaActividadAlternoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_OcurrenciaActividadAlterno
    /// </summary>
    public class OcurrenciaActividadAlternoRepository : GenericRepository<TOcurrenciaActividadAlterno>, IOcurrenciaActividadAlternoRepository
    {
        private Mapper _mapper;

        public OcurrenciaActividadAlternoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOcurrenciaActividadAlterno, OcurrenciaActividadAlterno>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TOcurrenciaActividadAlterno MapeoEntidad(OcurrenciaActividadAlterno entidad)
        {
            try
            {
                //crea la entidad padre
                TOcurrenciaActividadAlterno modelo = _mapper.Map<TOcurrenciaActividadAlterno>(entidad);

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

        public TOcurrenciaActividadAlterno Add(OcurrenciaActividadAlterno entidad)
        {
            try
            {
                var OcurrenciaActividadAlterno = MapeoEntidad(entidad);
                base.Insert(OcurrenciaActividadAlterno);
                return OcurrenciaActividadAlterno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOcurrenciaActividadAlterno Update(OcurrenciaActividadAlterno entidad)
        {
            try
            {
                var OcurrenciaActividadAlterno = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                OcurrenciaActividadAlterno.RowVersion = entidadExistente.RowVersion;

                base.Update(OcurrenciaActividadAlterno);
                return OcurrenciaActividadAlterno;
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


        public IEnumerable<TOcurrenciaActividadAlterno> Add(IEnumerable<OcurrenciaActividadAlterno> listadoEntidad)
        {
            try
            {
                List<TOcurrenciaActividadAlterno> listado = new List<TOcurrenciaActividadAlterno>();
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

        public IEnumerable<TOcurrenciaActividadAlterno> Update(IEnumerable<OcurrenciaActividadAlterno> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TOcurrenciaActividadAlterno> listado = new List<TOcurrenciaActividadAlterno>();
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
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_OcurrenciaActividadAlterno.
        /// </summary>
        /// <returns> List<OcurrenciaActividadAlternoDTO> </returns>
        public IEnumerable<OcurrenciaActividadAlternoDTO> ObtenerOcurrenciaActividadAlterno()
        {
            try
            {
                List<OcurrenciaActividadAlternoDTO> rpta = new List<OcurrenciaActividadAlternoDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdOcurrencia,
	                    IdActividadCabecera,
	                    PreProgramada,
	                    IdOcurrenciaActividad_Padre,
	                    NodoPadre,
	                    FechaCreacion,
	                    FechaModificacion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    IdPlantilla_Speech,
	                    IdFaseOportunidad,
	                    IdActividadCabecera_Programada,
	                    Roles
                    FROM com.T_OcurrenciaActividadAlterno
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OcurrenciaActividadAlternoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_OcurrenciaActividadAlterno para mostrarse en combo.
        /// </summary>
        /// <returns> List<OcurrenciaActividadAlternoComboDTO> </returns>
        public IEnumerable<OcurrenciaActividadAlternoComboDTO> ObtenerCombo()
        {
            try
            {
                List<OcurrenciaActividadAlternoComboDTO> rpta = new List<OcurrenciaActividadAlternoComboDTO>();
                var query = @"
                    SELECT
	                    OAA.Id,
	                    AC.Nombre AS ActividadCabecera,
	                    OA.Nombre AS OcurrenciaAlterno
                    FROM com.T_OcurrenciaActividadAlterno AS OAA
                    INNER JOIN com.T_OcurrenciaAlterno AS OA
	                    ON OAA.IdOcurrencia = OA.Id
	                    AND OA.Estado = 1
                    INNER JOIN com.T_ActividadCabecera AS AC
	                    ON OAA.IdActividadCabecera = AC.Id
	                    AND AC.Estado = 1
                    WHERE OAA.Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OcurrenciaActividadAlternoComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Arbol de Ocurrencias Alterno.
        /// </summary>
        /// <param name="idActividadCabecera">Id de la Actividad Cabecera</param>
        /// <param name="idOcurrenciaPadre">Id de la Ocurrencia Padre</param>
        /// <returns> List<ArbolOcurenciaAlternoDTO> </returns>
        public IEnumerable<ArbolOcurenciaAlternoDTO> ObtenerArbolOcurrenciaAlterno(int idActividadCabecera, int idOcurrenciaPadre)
        {
            try
            {
                List<ArbolOcurenciaAlternoDTO> arbolOcurrencia = new List<ArbolOcurenciaAlternoDTO>();
                var query = @"
                    SELECT IdOcurrenciaActividad,IdOcurrenciaReporte,RequiereLlamada,EstadoOcurrencia,NombreOcurrencia,Color,Roles,Nivel,
	                    TieneOcurrencias,TieneActividades,IdFaseOportunidad,IdOcurrenciaActividad_Padre AS IdOcurrenciaActividadPadre,
	                    FechaCreacion,IdPlantilla_Speech AS IdPlantillaSpeech,NombreEstadoOcurrencia,CrearOportunidad,FaseSiguiente,
	                    IdPlantillaWP,IdPlantillaCE
                    FROM com.V_HojaGetArbolDeOcurrenciasAlterno
                    WHERE IdActividadCabecera = @idActividadCabecera
	                    AND IdOcurrenciaActividad_Padre = @idOcurrenciaPadre
	                    AND EstadoOa = 1
	                    AND EstadoOc = 1";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idActividadCabecera, idOcurrenciaPadre });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    arbolOcurrencia = JsonConvert.DeserializeObject<List<ArbolOcurenciaAlternoDTO>>(resultadoQuery);
                }
                return arbolOcurrencia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 01/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Arbol de Ocurrencias Alterno.
        /// </summary>
        /// <param name="idActividadCabecera">Id de la Actividad Cabecera</param>
        /// <param name="idOcurrenciaPadre">Id de la Ocurrencia Padre</param>
        /// <returns> List<ArbolOcurenciaAlternoDTO> </returns>
        public ArbolOcurenciaAlternoDTO? ObtenerArbolOcurrenciaAlternoV2(int idActividadCabecera, int idOcurrenciaPadre, int idOcurrenciaReporte)
        {
            try
            {
                var query = @"
                    SELECT IdOcurrenciaActividad,IdOcurrenciaReporte,RequiereLlamada,EstadoOcurrencia,NombreOcurrencia,Color,Roles,Nivel,
	                    TieneOcurrencias,TieneActividades,IdFaseOportunidad,IdOcurrenciaActividad_Padre AS IdOcurrenciaActividadPadre,
	                    FechaCreacion,IdPlantilla_Speech AS IdPlantillaSpeech,NombreEstadoOcurrencia,CrearOportunidad,FaseSiguiente,
	                    IdPlantillaWP,IdPlantillaCE
                    FROM com.V_HojaGetArbolDeOcurrenciasAlterno
                    WHERE IdActividadCabecera = @idActividadCabecera
	                    AND IdOcurrenciaActividad_Padre = @idOcurrenciaPadre
                        AND IdOcurrenciaReporte=@idOcurrenciaReporte
	                    AND EstadoOa = 1
	                    AND EstadoOc = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idActividadCabecera, idOcurrenciaPadre, idOcurrenciaReporte });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ArbolOcurenciaAlternoDTO>(resultado)!;
                }
                return null;
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
        /// Obtiene una lista de OcurrenciaActividades por el Id de OcurrenciaActividad.
        /// </summary>
        /// <param name="idOcurrenciaActividad">Id de la Ocurrencia Actividad</param>
        /// <returns> OcurenciaActividadCompletoDTO </returns>
        public OcurenciaActividadCompletoDTO ObtenerOcurrenciaActividadPorId(int? idOcurrenciaActividad)
        {
            try
            {
                OcurenciaActividadCompletoDTO arbolOcurrencia = new OcurenciaActividadCompletoDTO();
                var query = @"
                    SELECT
	                    Id,
	                    IdOcurrencia,
	                    Nombre,
	                    IdFaseOportunidad,
	                    IdActividadCabecera,
	                    IdPlantilla_Speech,
	                    IdActividadCabeceraProgramada,
	                    Roles
                    FROM com.V_ObtenerOcurrenciasActividadesPorId
                    WHERE Id = @idOcurrenciaActividad";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idOcurrenciaActividad });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    arbolOcurrencia = JsonConvert.DeserializeObject<OcurenciaActividadCompletoDTO>(resultadoQuery);
                }
                return arbolOcurrencia;
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
        /// Obtiene una lista de OcurrenciaActividades por el Id de OcurrenciaActividad.
        /// </summary>
        /// <param name="idOcurrenciaActividad">Id de la Ocurrencia Actividad</param>
        /// <returns> OcurenciaActividadCompletoDTO </returns>
        public async Task<OcurenciaActividadCompletoDTO> ObtenerOcurrenciaActividadPorIdAsync(int? idOcurrenciaActividad)
        {
            try
            {
                OcurenciaActividadCompletoDTO arbolOcurrencia = new OcurenciaActividadCompletoDTO();
                var query = @"
                    SELECT
	                    Id,
	                    IdOcurrencia,
	                    Nombre,
	                    IdFaseOportunidad,
	                    IdActividadCabecera,
	                    IdPlantilla_Speech,
	                    IdActividadCabeceraProgramada,
	                    Roles
                    FROM com.V_ObtenerOcurrenciasActividadesPorId
                    WHERE Id = @idOcurrenciaActividad";
                var resultadoQuery = await _dapperRepository.FirstOrDefaultAsync(query, new { idOcurrenciaActividad });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    arbolOcurrencia = JsonConvert.DeserializeObject<OcurenciaActividadCompletoDTO>(resultadoQuery);
                }
                return arbolOcurrencia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
