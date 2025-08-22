using AutoMapper;

using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;

using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: TipoDocumentoAlumnoRepository
    /// Autor: Christian Quispe Mamani.
    /// Fecha: 16/05/2023
    /// <summary>
    /// Gestión general de T_TipoDocumentoAlumno
    /// </summary>
    public class TipoDocumentoAlumnoRepository : GenericRepository<TTipoDocumentoAlumno>, ITipoDocumentoAlumnoRepository
    {
        private Mapper _mapper;

        public TipoDocumentoAlumnoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoDocumentoAlumno, TipoDocumentoAlumno>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TTipoDocumentoAlumno MapeoEntidad(TipoDocumentoAlumno entidad)
        {
            try
            {
                TTipoDocumentoAlumno modelo = _mapper.Map<TTipoDocumentoAlumno>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTipoDocumentoAlumno Add(TipoDocumentoAlumno entidad)
        {
            try
            {
                var TipoDocumentoAlumno = MapeoEntidad(entidad);
                base.Insert(TipoDocumentoAlumno);
                return TipoDocumentoAlumno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTipoDocumentoAlumno Update(TipoDocumentoAlumno entidad)
        {
            try
            {
                var TipoDocumentoAlumno = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoDocumentoAlumno.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoDocumentoAlumno);
                return TipoDocumentoAlumno;
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
        public IEnumerable<TTipoDocumentoAlumno> Add(IEnumerable<TipoDocumentoAlumno> listadoEntidad)
        {
            try
            {
                List<TTipoDocumentoAlumno> listado = new List<TTipoDocumentoAlumno>();
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
        public IEnumerable<TTipoDocumentoAlumno> Update(IEnumerable<TipoDocumentoAlumno> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoDocumentoAlumno> listado = new List<TTipoDocumentoAlumno>();
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
        /// Autor: Christian Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoDocumentoAlumno.
        /// </summary>
        /// <returns> TipoDocumentoAlumnoDTO </returns>
        public TipoDocumentoAlumnoDTO ObtenerNombrePlantillaPorId(int id)
        {
            try
            {
                TipoDocumentoAlumnoDTO rpta = new TipoDocumentoAlumnoDTO();
                var query = string.Empty;
                query = @"SELECT
	                          TDA.Id                      AS Id,
	                          TDA.Nombre                  AS Nombre,
	                          TDA.IdPlantillaFrontal      AS IdPlantillaFrontal,
	                          P1.Nombre                   AS NombrePlantillaFrontal,
	                          TDA.IdPlantillaPosterior    AS IdPlantillaPosterior,
	                          P2.Nombre                   AS NombrePlantillaPosterior,
	                          TDA.IdOperadorComparacion   AS IdOperadorComparacion,
	                          TDA.TieneDeuda              AS TieneDeuda,
	                          TDA.Estado                  AS Estado
                          FROM ope.T_TipoDocumentoAlumno AS TDA
                          LEFT JOIN mkt.T_Plantilla AS P1
	                          ON P1.Id = TDA.IdPlantillaFrontal
                          LEFT JOIN mkt.T_Plantilla AS P2
	                          ON P2.Id = TDA.IdPlantillaPosterior
                          WHERE TDA.Estado = 1 AND TDA.Id = @id
                          ORDER BY TDA.FechaModificacion DESC";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<TipoDocumentoAlumnoDTO>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Christian Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoDocumentoAlumno.
        /// </summary>
        /// <returns> TipoDocumentoAlumnoDTO </returns>
        public TipoDocumentoAlumno ObtenerPorId(int id)
        {
            try
            {
                TipoDocumentoAlumno rpta = new TipoDocumentoAlumno();
                var query = string.Empty;
                query = @"SELECT
	                          Id,
	                          Nombre,
	                          IdPlantillaFrontal,
	                          IdPlantillaPosterior,
	                          IdOperadorComparacion,
	                          TieneDeuda,
                              Estado,
                              UsuarioCreacion,
                              UsuarioModificacion,
                              FechaCreacion,
                              FechaModificacion,
                              RowVersion,
                              IdMigracion
                          FROM ope.T_TipoDocumentoAlumno
                          WHERE Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<TipoDocumentoAlumno>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Christian Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoDocumentoAlumno.
        /// </summary>
        /// <returns> List<TipoDocumentoAlumnoDTO> </returns>
        public IEnumerable<TipoDocumentoAlumnoDTO> Obtener()
        {
            try
            {
                IEnumerable<TipoDocumentoAlumnoDTO> rpta = new List<TipoDocumentoAlumnoDTO>();
                var query = string.Empty;
                query = @"SELECT
	                          TDA.Id                      AS Id,
	                          TDA.Nombre                  AS Nombre,
	                          TDA.IdPlantillaFrontal      AS IdPlantillaFrontal,
	                          P1.Nombre                   AS NombrePlantillaFrontal,
	                          TDA.IdPlantillaPosterior    AS IdPlantillaPosterior,
	                          P2.Nombre                   AS NombrePlantillaPosterior,
	                          TDA.IdOperadorComparacion   AS IdOperadorComparacion,
	                          TDA.TieneDeuda              AS TieneDeuda,
	                          TDA.Estado                  AS Estado
                          FROM ope.T_TipoDocumentoAlumno AS TDA
                          LEFT JOIN mkt.T_Plantilla AS P1
	                          ON P1.Id = TDA.IdPlantillaFrontal
                          LEFT JOIN mkt.T_Plantilla AS P2
	                          ON P2.Id = TDA.IdPlantillaPosterior
                          WHERE TDA.Estado = 1
                          ORDER BY TDA.FechaModificacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoDocumentoAlumnoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Christian Quispe Mamani
        /// Fecha: 18/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de V_TPlantilla_CertificadoConstancia.
        /// </summary>
        /// <returns> List<PlantilaCertificadoConstanciaDTO> </returns>
        public IEnumerable<TipoDocumentoAlumnoDetalleConfiguracionDTO> ObtenerDetalleConfiguracionCerficicado(int idTipoDocumentoAlumno)
        {
            try
            {
                IEnumerable<TipoDocumentoAlumnoDetalleConfiguracionDTO> rpta = new List<TipoDocumentoAlumnoDetalleConfiguracionDTO>();
                var query = string.Empty;
                query = @"SELECT
                            Id,
	                        ModalidadCurso      AS IdModalidadCurso,
	                        EstadoMatricula     AS IdEstadoMatricula,
	                        SubEstadoMatricula  AS IdSubEstadoMatricula,
	                        OperadorComparacion AS IdOperadorComparacion,
	                        TieneDeuda
                        FROM  [ope].[V_CondicionTipoDocumentoAlumno] 
                        WHERE Id = @idTipoDocumentoAlumno";
                var resultado = _dapperRepository.QueryDapper(query, new { idTipoDocumentoAlumno });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoDocumentoAlumnoDetalleConfiguracionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Christian Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de V_TPlantilla_CertificadoConstancia.
        /// </summary>
        /// <returns> List<PlantilaCertificadoConstanciaDTO> </returns>
        public IEnumerable<PlantilaCertificadoConstanciaDTO> ObtenerPlantillaCertificadoConstancia()
        {
            try
            {
                IEnumerable<PlantilaCertificadoConstanciaDTO> rpta = new List<PlantilaCertificadoConstanciaDTO>();
                var query = string.Empty;
                query = @"SELECT Id,IdPlantillaBase,Nombre FROM [mkt].[V_TPlantilla_CertificadoConstancia] WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<PlantilaCertificadoConstanciaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
