using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SentinelSdtRepSbsitemRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/06/2022
    /// <summary>
    /// Gestión general de T_SentinelSdtRepSbsitem
    /// </summary>
    public class SentinelSdtRepSbsitemRepository : GenericRepository<TSentinelSdtRepSbsitem>, ISentinelSdtRepSbsitemRepository
    {
        private Mapper _mapper;

        public SentinelSdtRepSbsitemRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSentinelSdtRepSbsitem, SentinelSdtRepSbsitem>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSentinelSdtRepSbsitem MapeoEntidad(SentinelSdtRepSbsitem entidad)
        {
            try
            {
                //crea la entidad padre
                TSentinelSdtRepSbsitem modelo = _mapper.Map<TSentinelSdtRepSbsitem>(entidad);

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

        public TSentinelSdtRepSbsitem Add(SentinelSdtRepSbsitem entidad)
        {
            try
            {
                var SentinelSdtRepSbsitem = MapeoEntidad(entidad);
                base.Insert(SentinelSdtRepSbsitem);
                return SentinelSdtRepSbsitem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSentinelSdtRepSbsitem Update(SentinelSdtRepSbsitem entidad)
        {
            try
            {
                var SentinelSdtRepSbsitem = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SentinelSdtRepSbsitem.RowVersion = entidadExistente.RowVersion;

                base.Update(SentinelSdtRepSbsitem);
                return SentinelSdtRepSbsitem;
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


        public IEnumerable<TSentinelSdtRepSbsitem> Add(IEnumerable<SentinelSdtRepSbsitem> listadoEntidad)
        {
            try
            {
                List<TSentinelSdtRepSbsitem> listado = new List<TSentinelSdtRepSbsitem>();
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

        public IEnumerable<TSentinelSdtRepSbsitem> Update(IEnumerable<SentinelSdtRepSbsitem> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSentinelSdtRepSbsitem> listado = new List<TSentinelSdtRepSbsitem>();
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
        /// Fecha: 15/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_SentinelSdtRepSbsitem.
        /// </summary>
        /// <returns> List<SentinelSdtRepSbsitemDTO> </returns>
        public IEnumerable<SentinelSdtRepSbsitemDTO> ObtenerSentinelSdtRepSbsitem()
        {
            try
            {
                List<SentinelSdtRepSbsitemDTO> rpta = new List<SentinelSdtRepSbsitemDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdSentinel,
	                    TipoDoc,
	                    NroDoc,
	                    NombreRazonSocial,
	                    Calificacion,
	                    MontoDeuda,
	                    DiasVencidos,
	                    FechaReporte,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_SentinelSdtRepSBSItem
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSdtRepSbsitemDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_SentinelSdtRepSbsitem para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelSdtRepSbsitemComboDTO> </returns>
        public IEnumerable<SentinelSdtRepSbsitemComboDTO> ObtenerCombo()
        {
            try
            {
                List<SentinelSdtRepSbsitemComboDTO> rpta = new List<SentinelSdtRepSbsitemComboDTO>();
                var query = @"SELECT Id,NroDoc,NombreRazonSocial FROM com.T_SentinelSdtRepSBSItem WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSdtRepSbsitemComboDTO>>(resultado);
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
        /// Obtiene registros de T_SentinelSdtRepSbsitem asociados a un IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id de Sentinel</param>
        /// <returns> List<SentinelLineaDeudaDatosAlumnoDTO> </returns>
        public IEnumerable<SentinelLineaDeudaDatosAlumnoDTO> ObtenerLineaDeudaPorIdSentinel(int idSentinel)
        {
            try
            {
                List<SentinelLineaDeudaDatosAlumnoDTO> lineaDeuda = new List<SentinelLineaDeudaDatosAlumnoDTO>();
                var resultadoQuery = _dapperRepository.QuerySPDapper("com.SP_SentinelLineasDeudasByAlumno", new { IdSentinel = idSentinel });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    lineaDeuda = JsonConvert.DeserializeObject<List<SentinelLineaDeudaDatosAlumnoDTO>>(resultadoQuery);
                }
                return lineaDeuda;
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
        /// Obtiene los registros de T_SentinelSdtRepSbsitem asociados a un IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id se Sentinel</param>
        /// <returns> List<SentinelSdtRepSbsitemDTO> </returns>
        public IEnumerable<SentinelSdtRepSbsitemDTO> ObtenerPorIdSentinel(int idSentinel)
        {
            try
            {
                List<SentinelSdtRepSbsitemDTO> rpta = new List<SentinelSdtRepSbsitemDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdSentinel,
	                    TipoDoc,
	                    NroDoc,
	                    NombreRazonSocial,
	                    Calificacion,
	                    MontoDeuda,
	                    DiasVencidos,
	                    FechaReporte,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_SentinelSdtRepSBSItem
                    WHERE Estado = 1 AND IdSentinel = @idSentinel";
                var resultado = _dapperRepository.QueryDapper(query, new { idSentinel });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSdtRepSbsitemDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 20/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de com.SP_SentinelLineasDeudasByAlumno asociados a un IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id de Sentinel</param>
        /// <returns> List<AlumnosSentinelLineasDeudaDTO> </returns>
        public List<SentinelLineaDeudaDatosAlumnoDTO> ObtenerLineaDeuda(int idSentinel)
        {
            try
            {
                var queryLineaDeuda = _dapperRepository.QuerySPDapper("com.SP_SentinelLineasDeudasByAlumno", new { IdSentinel = idSentinel });
                return JsonConvert.DeserializeObject<List<SentinelLineaDeudaDatosAlumnoDTO>>(queryLineaDeuda);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// obtiene la linea de deuda de un Contacto Por IdSentinel
        /// </summary>
        /// <param name="idSentinel"></param>
        /// <returns></returns>
        public List<SentinelLineaDeudaDatosAlumnoDTO> ObtenerLineaDeudaVigente(int idSentinel)
        {
            try
            {
                var queryLineaDeuda = _dapperRepository.QuerySPDapper("com.SP_SentinelLineasDeudasVigenteByAlumno", new { IdSentinel = idSentinel });
                return JsonConvert.DeserializeObject<List<SentinelLineaDeudaDatosAlumnoDTO>>(queryLineaDeuda);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// obtiene la linea de deuda de un Contacto Por IdSentinel
        /// </summary>
        /// <param name="idSentinel"></param>
        /// <returns></returns>
        public List<SentinelLineaDeudaDatosAlumnoDTO> ObtenerLineaDeudaVencida(int idSentinel)
        {
            try
            {
                var queryLineaDeuda = _dapperRepository.QuerySPDapper("com.SP_SentinelLineasDeudasVencidaByAlumno", new { IdSentinel = idSentinel });
                return JsonConvert.DeserializeObject<List<SentinelLineaDeudaDatosAlumnoDTO>>(queryLineaDeuda);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 02/11/2022
        /// <summary>
        /// Obtiene La Linea de Deuda Para Detalle de Sentinel
        /// </summary>
        /// <param name="idSentinel"></param>
        /// <returns></returns>
        public List<SentinelSdtRepSbsitemLineaDeudaDTO> ObtenerLineaDeudaSentinel(int idSentinel)
        {
            try
            {
                string _queryDeuda = "Select TipoDoc,NombreRazonSocial,Calificacion,MontoDeuda,DiasVencidos,FechaReporte from com.T_SentinelSdtRepSbsitem Where IdSentinel=@IdSentinel AND Estado = 1 ORDER BY FechaCreacion desc";
                var queryDeuda = _dapperRepository.QueryDapper(_queryDeuda, new { IdSentinel = idSentinel });
                return JsonConvert.DeserializeObject<List<SentinelSdtRepSbsitemLineaDeudaDTO>>(queryDeuda);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
