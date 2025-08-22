using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SentinelSdtInfGenRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/06/2022
    /// <summary>
    /// Gestión general de T_SentinelSdtInfGen
    /// </summary>
    public class SentinelSdtInfGenRepository : GenericRepository<TSentinelSdtInfGen>, ISentinelSdtInfGenRepository
    {
        private Mapper _mapper;

        public SentinelSdtInfGenRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSentinelSdtInfGen, SentinelSdtInfGen>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSentinelSdtInfGen MapeoEntidad(SentinelSdtInfGen entidad)
        {
            try
            {
                //crea la entidad padre
                TSentinelSdtInfGen modelo = _mapper.Map<TSentinelSdtInfGen>(entidad);

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

        public TSentinelSdtInfGen Add(SentinelSdtInfGen entidad)
        {
            try
            {
                var SentinelSdtInfGen = MapeoEntidad(entidad);
                base.Insert(SentinelSdtInfGen);
                return SentinelSdtInfGen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSentinelSdtInfGen Update(SentinelSdtInfGen entidad)
        {
            try
            {
                var SentinelSdtInfGen = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SentinelSdtInfGen.RowVersion = entidadExistente.RowVersion;

                base.Update(SentinelSdtInfGen);
                return SentinelSdtInfGen;
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


        public IEnumerable<TSentinelSdtInfGen> Add(IEnumerable<SentinelSdtInfGen> listadoEntidad)
        {
            try
            {
                List<TSentinelSdtInfGen> listado = new List<TSentinelSdtInfGen>();
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

        public IEnumerable<TSentinelSdtInfGen> Update(IEnumerable<SentinelSdtInfGen> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSentinelSdtInfGen> listado = new List<TSentinelSdtInfGen>();
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
        /// Obtiene todos los registros de T_SentinelSdtInfGen.
        /// </summary>
        /// <returns> List<SentinelSdtInfGenDTO> </returns>
        public IEnumerable<SentinelSdtInfGenDTO> ObtenerSentinelSdtInfGen()
        {
            try
            {
                List<SentinelSdtInfGenDTO> rpta = new List<SentinelSdtInfGenDTO>();
                var query = @"
                    SELECT
	                    Id,IdSentinel,DNI,FechaNacimiento,Sexo,Digito,DigitoAnterior,RUC,RazonSocial,NombreComercial,FechaBaja,TipoContribuyente,CodigoTipoContribuyente,
	                    EstadoContribuyente,CodigoEstadoContribuyente,CondicionContribuyente,CodigoCondicionContribuyente,ActividadEconomica,CIIU,ActividadEconomica2,CIIU2,
	                    ActividadEconomica3,CIIU3,FechaActividad,Direccion,Referencia,Departamento,Provincia,Distrito,Ubigeo,FechaConstitucion,ActvidadComercioExterior,
	                    CodigoActividadComerExt,CodigoDependencia,Dependencia,Folio,Asiento,Tomo,PartidaReg,Patron,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion
                    FROM com.T_SentinelSdtInfGen
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSdtInfGenDTO>>(resultado);
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
        /// Obtiene registros de T_SentinelSdtInfGen para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelSdtInfGenComboDTO> </returns>
        public IEnumerable<SentinelSdtInfGenComboDTO> ObtenerCombo()
        {
            try
            {
                List<SentinelSdtInfGenComboDTO> rpta = new List<SentinelSdtInfGenComboDTO>();
                var query = @"SELECT Id,DNI,RUC,RazonSocial FROM com.T_SentinelSdtInfGen WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSdtInfGenComboDTO>>(resultado);
                }
                return rpta;
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
        /// Obtiene los registros de T_SentinelSdtInfGen asociados al IdSentinel.
        /// </summary>
        /// <param name="idSentinel">Id de Sentinel</param>
        /// <returns> List<SentinelSdtInfGenDTO> </returns>
        public IEnumerable<SentinelSdtInfGenDTO> ObtenerPorIdSentinel(int idSentinel)
        {
            try
            {
                List<SentinelSdtInfGenDTO> rpta = new List<SentinelSdtInfGenDTO>();
                var query = @"
                    SELECT
	                    Id,IdSentinel,DNI,FechaNacimiento,Sexo,Digito,DigitoAnterior,RUC,RazonSocial,NombreComercial,FechaBaja,TipoContribuyente,CodigoTipoContribuyente,
	                    EstadoContribuyente,CodigoEstadoContribuyente,CondicionContribuyente,CodigoCondicionContribuyente,ActividadEconomica,CIIU,ActividadEconomica2,CIIU2,
	                    ActividadEconomica3,CIIU3,FechaActividad,Direccion,Referencia,Departamento,Provincia,Distrito,Ubigeo,FechaConstitucion,ActvidadComercioExterior,
	                    CodigoActividadComerExt,CodigoDependencia,Dependencia,Folio,Asiento,Tomo,PartidaReg,Patron,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion
                    FROM com.T_SentinelSdtInfGen
                    WHERE Estado = 1 AND IdSentinel = @idSentinel";
                var resultado = _dapperRepository.QueryDapper(query, new { idSentinel });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSdtInfGenDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 02/11/2022
        /// <summary>
        /// Obtiene datos generales relacionados al idSentinel
        /// </summary>
        /// <param name="idSentinel"></param>
        /// <returns></returns>
        public List<SentinelSdtInfGenDatosGeneralesDTO> ObtenerDatosGenerales(int idSentinel)
        {
            try
            {
                string _queryDatosGenerales = "select  Id,FechaNacimiento,Sexo,Digito,DigitoAnterior,RUC,RazonSocial,NombreComercial,TipoContribuyente,EstadoContribuyente,CondicionContribuyente,Dependencia,CIIU,FechaActividad,Patron"
                                           + ", Folio, Asiento from com.V_TSentinelSdtInfGen_ObtenerDatosGenerales Where IdSentinel=@IdSentinel AND Estado = 1 ORDER BY FechaCreacion desc";
                var queryDatosGenerales = _dapperRepository.QueryDapper(_queryDatosGenerales, new { IdSentinel = idSentinel });
                return JsonConvert.DeserializeObject<List<SentinelSdtInfGenDatosGeneralesDTO>>(queryDatosGenerales);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
