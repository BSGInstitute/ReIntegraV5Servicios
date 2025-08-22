using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Operacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: CertificadoPartnerComplementoRepository
    /// Autor: Marco Jose Villanueva Torres
    /// Fecha: 15/09/2023
    /// <summary>
    /// Gestión general de T_CertificadoPartnerComplemento
    /// </summary>
    public class CertificadoPartnerComplementoRepository : GenericRepository<TCertificadoPartnerComplemento>, ICertificadoPartnerComplementoRepository
    {
        private Mapper _mapper;

        public CertificadoPartnerComplementoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCertificadoPartnerComplemento, CertificadoPartnerComplemento>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TCertificadoPartnerComplemento MapeoEntidad(CertificadoPartnerComplemento entidad)
        {
            try
            {
                TCertificadoPartnerComplemento modelo = _mapper.Map<TCertificadoPartnerComplemento>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCertificadoPartnerComplemento Add(CertificadoPartnerComplemento entidad)
        {
            try
            {
                var CertificadoPartnerComplemento = MapeoEntidad(entidad);
                base.Insert(CertificadoPartnerComplemento);
                return CertificadoPartnerComplemento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TCertificadoPartnerComplemento Update(CertificadoPartnerComplemento entidad)
        {
            try
            {
                var CertificadoPartnerComplemento = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CertificadoPartnerComplemento.RowVersion = entidadExistente.RowVersion;

                base.Update(CertificadoPartnerComplemento);
                return CertificadoPartnerComplemento;
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
        public IEnumerable<TCertificadoPartnerComplemento> Add(IEnumerable<CertificadoPartnerComplemento> listadoEntidad)
        {
            try
            {
                List<TCertificadoPartnerComplemento> listado = new List<TCertificadoPartnerComplemento>();
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
        public IEnumerable<TCertificadoPartnerComplemento> Update(IEnumerable<CertificadoPartnerComplemento> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCertificadoPartnerComplemento> listado = new List<TCertificadoPartnerComplemento>();
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



        /// Autor: Marco Jose Villanueva Torres.
        /// Fecha: 15/09/2023
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns >CertificadoPartnerComplemento || null</returns>
        public CertificadoPartnerComplemento? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Codigo,
	                    Descripcion,
	                    FrontalCentral,
	                    FrontalInferiorIzquierda,
	                    PosteriorCentral,
	                    PosteriorInferiorIzquierda,
	                    MencionEnCertificado,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM ope.T_CertificadoPartnerComplemento
                    WHERE Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<CertificadoPartnerComplemento>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor: Marco Jose Villanueva Torres.
        /// Fecha: 15/09/2023
        /// <summary>
        /// Obtiene el registro completo de CertificadoPartnerComplemento
        /// </summary>

        public IEnumerable<CertificadoPartnerComplementoDTO> ObtenerTodo()
        {
            try
            {
                var query = @"
                    SELECT
                    Id,
                    Nombre,
                    Codigo,
                    Descripcion,
                    Estado,
                    FrontalCentral,
                    FrontalInferiorIzquierda,
                    PosteriorCentral,
                    PosteriorInferiorIzquierda,
                    MencionEnCertificado
                    FROM ope.T_CertificadoPartnerComplemento
                    ORDER BY Id ASC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<CertificadoPartnerComplementoDTO>>(resultado)!;
                }
                return new List<CertificadoPartnerComplementoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"#CPCR-OT-002@Error en ObtenerTodo() {ex.Message}", ex);
            }
        }
        /// Autor: Marco Jose Villanueva Torres.
        /// Fecha: 15/09/2023
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene El centro de Costo Asociado por id
        /// </summary>

        public List<CentroCostoAsignadoCertificadoPartnerComplementoDTO> ObtenerCentroCostoAsociadoPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT 
                        IdCertificadoPartnerComplemento,
                        IdCentroCosto, 
                        NombreCentroCosto 
                    FROM ope.V_ObtenerCentroCostoPorCertificadoPartnerComplemento 
                    WHERE IdCertificadoPartnerComplemento = @id;";
                var centrosCostoDB = _dapperRepository.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(centrosCostoDB) && !centrosCostoDB.Contains("[]"))
                {
                    return  JsonConvert.DeserializeObject<List<CentroCostoAsignadoCertificadoPartnerComplementoDTO>>(centrosCostoDB);
                }
                return new List<CentroCostoAsignadoCertificadoPartnerComplementoDTO>(); ;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Marco Jose Villanueva Torres.
        /// Fecha: 15/09/2023
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene por el centro de costo Asociado por idCentro de Costo
        /// </summary>

        public CertificadoPartnerComplementoDTO ObtenerPorCentroCosto(int IdCentroCosto)
        {
            try
            {
                CertificadoPartnerComplementoDTO centrosCosto = new CertificadoPartnerComplementoDTO();
                var query = @"SELECT    Id,MencionEnCertificado,
                                        FrontalCentral,
                                        FrontalInferiorIzquierda,
                                        PosteriorCentral,
                                        PosteriorInferiorIzquierda " +
                            " FROM ope.V_ObtenerCertificadoPartnerComplementoPorCentroCosto WHERE IdCentroCosto = @IdCentroCosto;";
                var centrosCostoDB = _dapperRepository.FirstOrDefault(query, new { IdCentroCosto });
                if (!string.IsNullOrEmpty(centrosCostoDB) && !centrosCostoDB.Contains("[]"))
                {
                    centrosCosto = JsonConvert.DeserializeObject<CertificadoPartnerComplementoDTO>(centrosCostoDB);
                }
                return centrosCosto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}



