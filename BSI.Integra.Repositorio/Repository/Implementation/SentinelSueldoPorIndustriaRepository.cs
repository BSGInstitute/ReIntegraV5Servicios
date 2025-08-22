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
    /// Repositorio: SentinelSueldoPorIndustriaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 16/06/2022
    /// <summary>
    /// Gestión general de T_SentinelSueldoPorIndustria
    /// </summary>
    public class SentinelSueldoPorIndustriaRepository : GenericRepository<TSentinelSueldoPorIndustrium>, ISentinelSueldoPorIndustriaRepository
    {
        private Mapper _mapper;

        public SentinelSueldoPorIndustriaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSentinelSueldoPorIndustrium, SentinelSueldoPorIndustria>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSentinelSueldoPorIndustrium MapeoEntidad(SentinelSueldoPorIndustria entidad)
        {
            try
            {
                //crea la entidad padre
                TSentinelSueldoPorIndustrium modelo = _mapper.Map<TSentinelSueldoPorIndustrium>(entidad);

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

        public TSentinelSueldoPorIndustrium Add(SentinelSueldoPorIndustria entidad)
        {
            try
            {
                var SentinelSueldoPorIndustria = MapeoEntidad(entidad);
                base.Insert(SentinelSueldoPorIndustria);
                return SentinelSueldoPorIndustria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSentinelSueldoPorIndustrium Update(SentinelSueldoPorIndustria entidad)
        {
            try
            {
                var SentinelSueldoPorIndustria = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SentinelSueldoPorIndustria.RowVersion = entidadExistente.RowVersion;

                base.Update(SentinelSueldoPorIndustria);
                return SentinelSueldoPorIndustria;
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


        public IEnumerable<TSentinelSueldoPorIndustrium> Add(IEnumerable<SentinelSueldoPorIndustria> listadoEntidad)
        {
            try
            {
                List<TSentinelSueldoPorIndustrium> listado = new List<TSentinelSueldoPorIndustrium>();
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

        public IEnumerable<TSentinelSueldoPorIndustrium> Update(IEnumerable<SentinelSueldoPorIndustria> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSentinelSueldoPorIndustrium> listado = new List<TSentinelSueldoPorIndustrium>();
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
        /// Fecha: 16/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_SentinelSueldoPorIndustria.
        /// </summary>
        /// <returns> List<SentinelSueldoPorIndustriaDTO> </returns>
        public IEnumerable<SentinelSueldoPorIndustriaDTO> ObtenerSentinelSueldoPorIndustria()
        {
            try
            {
                List<SentinelSueldoPorIndustriaDTO> rpta = new List<SentinelSueldoPorIndustriaDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdCargo,
	                    IdIndustria,
	                    Tipo,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_SentinelSueldoPorIndustria
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSueldoPorIndustriaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 16/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_SentinelSueldoPorIndustria para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelSueldoPorIndustriaComboDTO> </returns>
        public IEnumerable<SentinelSueldoPorIndustriaComboDTO> ObtenerCombo()
        {
            try
            {
                List<SentinelSueldoPorIndustriaComboDTO> rpta = new List<SentinelSueldoPorIndustriaComboDTO>();
                var query = @"
                    SELECT
	                    SSI.Id,
	                    I.Nombre AS Industria,
	                    C.Nombre AS Cargo
                    FROM com.T_SentinelSueldoPorIndustria AS SSI
                    INNER JOIN pla.T_Industria AS I
	                    ON SSI.IdIndustria = I.Id
	                    AND I.Estado = 1
                    INNER JOIN pla.T_Cargo AS C
	                    ON SSI.IdCargo = C.Id
	                    AND C.Estado = 1
                    WHERE SSI.Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSueldoPorIndustriaComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Tipo de SentinelSueldoPorIndustria asociado a un Cargo e Industria.
        /// </summary>
        /// <param name="idCargo">Id del Cargo</param>
        /// <param name="idIndustria">Id de la Industria</param>
        /// <returns> ValorIntDTO </returns>
        public ValorIntDTO ObtenerTipoSueldoIndustria(int idCargo, int idIndustria)
        {
            try
            {
                ValorIntDTO tipoSueldo = new ValorIntDTO();
                var query = @"
                    SELECT Tipo AS Valor
                    FROM com.T_SentinelSueldoPorIndustria
                    WHERE IdCargo = @IdCargo
	                    AND IdIndustria = @IdIndustria
	                    AND Estado = 1"; 
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { IdCargo = idCargo, IdIndustria= idIndustria }); 
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    tipoSueldo = JsonConvert.DeserializeObject<ValorIntDTO>(resultadoQuery)!;
                }
                return tipoSueldo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
