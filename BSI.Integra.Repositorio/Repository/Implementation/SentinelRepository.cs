using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SentinelRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/06/2022
    /// <summary>
    /// Gestión general de T_Sentinel
    /// </summary>
    public class SentinelRepository : GenericRepository<TSentinel>, ISentinelRepository
    {
        private Mapper _mapper;

        public SentinelRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSentinel, Sentinel>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSentinelRepLegItem, SentinelRepLegItem>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSentinelSdtEstandarItem, SentinelSdtEstandarItem>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSentinelSdtInfGen, SentinelSdtInfGen>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSentinelSdtLincreItem, SentinelSdtLincreItem>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSentinelSdtPoshisItem, SentinelSdtPoshisItem>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSentinelSdtRepSbsitem, SentinelSdtRepSbsitem>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSentinelSdtResVenItem, SentinelSdtResVenItem>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSentinel MapeoEntidad(Sentinel entidad)
        {
            try
            {
                //crea la entidad padre
                TSentinel modelo = _mapper.Map<TSentinel>(entidad);

                //mapea los hijos
                if (entidad.SentinelRepLegItems != null && entidad.SentinelRepLegItems.Count() > 0)
                {
                    var modelosHijo = _mapper.Map<List<TSentinelRepLegItem>>(entidad.SentinelRepLegItems);
                    foreach (var modeloHijo in modelosHijo)
                    {
                        modelo.TSentinelRepLegItems.Add(modeloHijo);
                    }
                }

                if (entidad.SentinelSdtEstandarItems != null && entidad.SentinelSdtEstandarItems.Count() > 0)
                {
                    var modelosHijo = _mapper.Map<List<TSentinelSdtEstandarItem>>(entidad.SentinelSdtEstandarItems);
                    foreach (var modeloHijo in modelosHijo)
                    {
                        modelo.TSentinelSdtEstandarItems.Add(modeloHijo);
                    }
                }

                if (entidad.SentinelSdtInfGens != null && entidad.SentinelSdtInfGens.Count() > 0)
                {
                    var modelosHijo = _mapper.Map<List<TSentinelSdtInfGen>>(entidad.SentinelSdtInfGens);
                    foreach (var modeloHijo in modelosHijo)
                    {
                        modelo.TSentinelSdtInfGens.Add(modeloHijo);
                    }
                }

                if (entidad.SentinelSdtLincreItems != null && entidad.SentinelSdtLincreItems.Count() > 0)
                {
                    var modelosHijo = _mapper.Map<List<TSentinelSdtLincreItem>>(entidad.SentinelSdtLincreItems);
                    foreach (var modeloHijo in modelosHijo)
                    {
                        modelo.TSentinelSdtLincreItems.Add(modeloHijo);
                    }
                }

                if (entidad.SentinelSdtPoshisItems != null && entidad.SentinelSdtPoshisItems.Count() > 0)
                {
                    var modelosHijo = _mapper.Map<List<TSentinelSdtPoshisItem>>(entidad.SentinelSdtPoshisItems);
                    foreach (var modeloHijo in modelosHijo)
                    {
                        modelo.TSentinelSdtPoshisItems.Add(modeloHijo);
                    }
                }

                if (entidad.SentinelSdtRepSbsitems != null && entidad.SentinelSdtRepSbsitems.Count() > 0)
                {
                    var modelosHijo = _mapper.Map<List<TSentinelSdtRepSbsitem>>(entidad.SentinelSdtRepSbsitems);
                    foreach (var modeloHijo in modelosHijo)
                    {
                        modelo.TSentinelSdtRepSbsitems.Add(modeloHijo);
                    }
                }

                if (entidad.SentinelSdtResVenItems != null && entidad.SentinelSdtResVenItems.Count() > 0)
                {
                    var modelosHijo = _mapper.Map<List<TSentinelSdtResVenItem>>(entidad.SentinelSdtResVenItems);
                    foreach (var modeloHijo in modelosHijo)
                    {
                        modelo.TSentinelSdtResVenItems.Add(modeloHijo);
                    }
                }

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSentinel Add(Sentinel entidad)
        {
            try
            {
                var Sentinel = MapeoEntidad(entidad);
                base.Insert(Sentinel);
                return Sentinel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSentinel Update(Sentinel entidad)
        {
            try
            {
                var Sentinel = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Sentinel.RowVersion = entidadExistente.RowVersion;

                base.Update(Sentinel);
                return Sentinel;
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


        public IEnumerable<TSentinel> Add(IEnumerable<Sentinel> listadoEntidad)
        {
            try
            {
                List<TSentinel> listado = new List<TSentinel>();
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

        public IEnumerable<TSentinel> Update(IEnumerable<Sentinel> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSentinel> listado = new List<TSentinel>();
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
        /// Fecha: 14/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Sentinel.
        /// </summary>
        /// <returns> List<SentinelDTO> </returns>
        public IEnumerable<SentinelDTO> ObtenerSentinel()
        {
            try
            {
                List<SentinelDTO> rpta = new List<SentinelDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Dni,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_Sentinel
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 14/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Sentinel para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelComboDTO> </returns>
        public IEnumerable<SentinelComboDTO> ObtenerCombo()
        {
            try
            {
                List<SentinelComboDTO> rpta = new List<SentinelComboDTO>();
                var query = @"SELECT Id,Dni FROM com.T_Sentinel WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Sentinel con tipo de documento DNI asociados a un IdAlumno.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> SentinelDatosAlumnoAgendaDTO </returns>
        public SentinelDatosAlumnoAgendaDTO ObtenerSentinelTipoDocumentoDNIPorIdAlumno(int idAlumno)
        {
            try
            {
                SentinelDatosAlumnoAgendaDTO datosSentinel = new SentinelDatosAlumnoAgendaDTO();
                var query = @"
                    SELECT DNI,IdSentinel,IdAlumno,TipoDocumento,Nombre,Sexo,FechaNacimiento,Ubigeo,Distrito,Provincia,Departamento,CIIU,ActividadEconomica,
	                    Direccion,SemaforoActual,SemaforoPrevio,FechaUltimaActualizacion,NombreAlterno
                    FROM com.V_DatosSentinelPorIdAlumno
                    WHERE IdAlumno = @idAlumno AND TipoDocumento = 'D' AND EstadoSentinel = 1 AND EstadoInf = 1 AND EstadoEs = 1";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idAlumno });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    datosSentinel = JsonConvert.DeserializeObject<SentinelDatosAlumnoAgendaDTO>(resultadoQuery);
                }
                return datosSentinel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Sentinel asociados a un IdAlumno.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> List<SentinelDatosAlumnoAgendaDTO> </returns>
        public SentinelDatosAlumnoAgendaDTO ObtenerSentinelPorIdAlumno(int idAlumno)
        {
            try
            {
                SentinelDatosAlumnoAgendaDTO datosSentinel = new SentinelDatosAlumnoAgendaDTO();
                var query = @"
                    SELECT DNI,IdSentinel,IdAlumno,TipoDocumento,Nombre,Sexo,FechaNacimiento,Ubigeo,Distrito,Provincia,Departamento,CIIU,ActividadEconomica,
	                    Direccion,SemaforoActual,SemaforoPrevio,FechaUltimaActualizacion,NombreAlterno
                    FROM com.V_DatosSentinelPorIdAlumno
                    WHERE IdAlumno = @idAlumno AND EstadoSentinel = 1 AND EstadoInf = 1 AND EstadoEs = 1";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idAlumno });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    datosSentinel = JsonConvert.DeserializeObject<SentinelDatosAlumnoAgendaDTO>(resultadoQuery);
                }
                return datosSentinel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el identificador de sentinel relacionado al DNI recibido.
        /// </summary>
        /// <param name="dni">DNI asociado a registro Sentinel</param>
        /// <returns> ValorIntDTO </returns>
        public ValorIntDTO ObtenerIdSentinelPorDni(string dni)
        {
            try
            {
                ValorIntDTO id = new ValorIntDTO();
                var query = @"SELECT Id AS Valor FROM com.V_TSentinel_ObtenerId WHERE Dni = @dni AND Estado = 1";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { dni });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    id = JsonConvert.DeserializeObject<ValorIntDTO>(resultadoQuery);
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_Sentinel relacionado al DNI recibido.
        /// </summary>
        /// <param name="dni">DNI asociado a registro Sentinel</param>
        /// <returns> SentinelDTO </returns>
        public SentinelDTO ObtenerSentinelPorDni(string dni)
        {
            try
            {
                SentinelDTO id = new SentinelDTO();
                var query = @"
                    SELECT
	                    Id, Dni, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion,IdMigracion
                    FROM 
                        com.T_Sentinel
                    WHERE 
                        Estado = 1 AND Dni = @Dni";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { Dni = dni });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    id = JsonConvert.DeserializeObject<SentinelDTO>(resultadoQuery)!;
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los Datos Sentinel asociado a un Alumno.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> SentinelDatosContactoDTO </returns>
        public SentinelDatosContactoDTO ObtenerDatosAlumnoSentinel(int idAlumno)
        {
            try
            {
                string _queryDatoSentinel = @"
                    SELECT
	                    DNI,IdSentinel,IdAlumno,TipoDocumento,Nombre,Sexo,FechaNacimiento,Ubigeo,Distrito,Provincia,Departamento,CIIU,ActividadEconomica,
	                    Direccion,SemaforoActual,SemaforoPrevio,FechaUltimaActualizacion, NombreAlterno
                    FROM com.V_DatosSentinelPorIdAlumno
                    WHERE IdAlumno = @idAlumno AND TipoDocumento = 'D' AND EstadoSentinel = 1 AND EstadoInf = 1 AND EstadoEs = 1";
                var queryDatoSentinel = _dapperRepository.FirstOrDefault(_queryDatoSentinel, new { idAlumno });
                if (queryDatoSentinel == "null" || queryDatoSentinel == "")
                {
                    string _queryDatoSentinel2 = @"
                    SELECT
	                    DNI,IdSentinel,IdAlumno,TipoDocumento,Nombre,Sexo,FechaNacimiento,Ubigeo,Distrito,Provincia,Departamento,CIIU,ActividadEconomica,
	                    Direccion,SemaforoActual,SemaforoPrevio,FechaUltimaActualizacion, NombreAlterno
                    FROM com.V_DatosSentinelPorIdAlumno
                    WHERE IdAlumno = @idAlumno AND EstadoSentinel = 1 AND EstadoInf = 1 AND EstadoEs = 1";
                    var queryDatoSentinel2 = _dapperRepository.FirstOrDefault(_queryDatoSentinel2, new { idAlumno });
                    return JsonConvert.DeserializeObject<SentinelDatosContactoDTO>(queryDatoSentinel2);

                }
                else
                {
                    return JsonConvert.DeserializeObject<SentinelDatosContactoDTO>(queryDatoSentinel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la cabecera para la agenda.
        /// </summary>
        /// <param name="idSentinel">Id de sentinel</param>
        /// <returns>SentinelDatosCabeceraDTO</returns>
        public SentinelDatosCabeceraDTO ObtenerCabeceraSentinel(int idSentinel)
        {
            try
            {
                var cabecera = new SentinelDatosCabeceraDTO();
                var resultado = _dapperRepository.QuerySPFirstOrDefault("com.SP_ObtenerCabeceraSemaforo", new { idSentinel });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    cabecera = JsonConvert.DeserializeObject<SentinelDatosCabeceraDTO>(resultado);
                }
                return cabecera;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 10/09/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene credenciales de Sentinel
        /// </summary> 
        /// <returns>SentinelCredencialDTO</returns>
        public SentinelCredencialDTO ObtenerCredencial()
        {
            try
            {
                var sentinelCredencial = new SentinelCredencialDTO();
                var resultado = _dapperRepository.FirstOrDefault("SELECT DNI,Clave,Servicio,TipoDocumento FROM fin.V_ObtenerSentinelCredencial", null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    sentinelCredencial = JsonConvert.DeserializeObject<SentinelCredencialDTO>(resultado);
                }
                return sentinelCredencial;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
