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
    /// Repositorio: SentinelSueldoPorIndustriaDataTotalRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 16/06/2022
    /// <summary>
    /// Gestión general de T_SentinelSueldoPorIndustriaDataTotal
    /// </summary>
    public class SentinelSueldoPorIndustriaDataTotalRepository : GenericRepository<TSentinelSueldoPorIndustriaDataTotal>, ISentinelSueldoPorIndustriaDataTotalRepository
    {
        private Mapper _mapper;

        public SentinelSueldoPorIndustriaDataTotalRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSentinelSueldoPorIndustriaDataTotal, SentinelSueldoPorIndustriaDataTotal>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSentinelSueldoPorIndustriaDataTotal MapeoEntidad(SentinelSueldoPorIndustriaDataTotal entidad)
        {
            try
            {
                //crea la entidad padre
                TSentinelSueldoPorIndustriaDataTotal modelo = _mapper.Map<TSentinelSueldoPorIndustriaDataTotal>(entidad);

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

        public TSentinelSueldoPorIndustriaDataTotal Add(SentinelSueldoPorIndustriaDataTotal entidad)
        {
            try
            {
                var SentinelSueldoPorIndustriaDataTotal = MapeoEntidad(entidad);
                base.Insert(SentinelSueldoPorIndustriaDataTotal);
                return SentinelSueldoPorIndustriaDataTotal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSentinelSueldoPorIndustriaDataTotal Update(SentinelSueldoPorIndustriaDataTotal entidad)
        {
            try
            {
                var SentinelSueldoPorIndustriaDataTotal = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SentinelSueldoPorIndustriaDataTotal.RowVersion = entidadExistente.RowVersion;

                base.Update(SentinelSueldoPorIndustriaDataTotal);
                return SentinelSueldoPorIndustriaDataTotal;
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


        public IEnumerable<TSentinelSueldoPorIndustriaDataTotal> Add(IEnumerable<SentinelSueldoPorIndustriaDataTotal> listadoEntidad)
        {
            try
            {
                List<TSentinelSueldoPorIndustriaDataTotal> listado = new List<TSentinelSueldoPorIndustriaDataTotal>();
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

        public IEnumerable<TSentinelSueldoPorIndustriaDataTotal> Update(IEnumerable<SentinelSueldoPorIndustriaDataTotal> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSentinelSueldoPorIndustriaDataTotal> listado = new List<TSentinelSueldoPorIndustriaDataTotal>();
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
        /// Obtiene todos los registros de T_SentinelSueldoPorIndustriaDataTotal.
        /// </summary>
        /// <returns> List<SentinelSueldoPorIndustriaDataTotalDTO> </returns>
        public IEnumerable<SentinelSueldoPorIndustriaDataTotalDTO> ObtenerSentinelSueldoPorIndustriaDataTotal()
        {
            try
            {
                List<SentinelSueldoPorIndustriaDataTotalDTO> rpta = new List<SentinelSueldoPorIndustriaDataTotalDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdCargo,
	                    IdIndustria,
	                    Nombre,
	                    Tipo,
	                    Valor,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_SentinelSueldoPorIndustriaDataTotal
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSueldoPorIndustriaDataTotalDTO>>(resultado);
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
        /// Obtiene registros de T_SentinelSueldoPorIndustriaDataTotal para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelSueldoPorIndustriaDataTotalComboDTO> </returns>
        public IEnumerable<SentinelSueldoPorIndustriaDataTotalComboDTO> ObtenerCombo()
        {
            try
            {
                List<SentinelSueldoPorIndustriaDataTotalComboDTO> rpta = new List<SentinelSueldoPorIndustriaDataTotalComboDTO>();
                var query = @"
                    SELECT
	                    SIDT.Id,
	                    I.Nombre AS Industria,
	                    C.Nombre AS Cargo,
	                    SIDT.Nombre
                    FROM com.T_SentinelSueldoPorIndustriaDataTotal AS SIDT
                    INNER JOIN pla.T_Industria AS I
	                    ON SIDT.IdIndustria = I.Id
	                    AND I.Estado = 1
                    INNER JOIN pla.T_Cargo AS C
	                    ON SIDT.IdCargo = C.Id
	                    AND C.Estado = 1
                    WHERE SIDT.Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSueldoPorIndustriaDataTotalComboDTO>>(resultado);
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
        /// Obtiene el Sueldo asociado al Cargo e Industria
        /// </summary>
        /// <param name="idCargo">Id del Cargo</param>
        /// <param name="idIndustria">Id de la Industria</param>
        /// <returns> ValorIntDTO </returns>
        public ValorIntDTO ObtenerSueldoPorCargoIndustria(int idCargo, int idIndustria)
        {
            try
            {
                ValorIntDTO sueldo = new ValorIntDTO();
                var query = @"
                    SELECT Valor
                    FROM com.T_SentinelSueldoPorIndustriaDataTotal
                    WHERE Estado = 1
	                    AND IdCargo = @idCargo
	                    AND IdIndustria = @idIndustria";
                var resultadoQuery = _dapperRepository.FirstOrDefault(query, new { idCargo, idIndustria });
                if (!string.IsNullOrEmpty(resultadoQuery) && resultadoQuery != "null")
                {
                    sueldo = JsonConvert.DeserializeObject<ValorIntDTO>(resultadoQuery)!;
                }
                return sueldo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
