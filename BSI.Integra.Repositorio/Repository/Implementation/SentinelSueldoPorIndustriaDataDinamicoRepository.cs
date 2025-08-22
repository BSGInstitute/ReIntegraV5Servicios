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
    /// Repositorio: SentinelSueldoPorIndustriaDataDinamicoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 16/06/2022
    /// <summary>
    /// Gestión general de T_SentinelSueldoPorIndustriaDataDinamico
    /// </summary>
    public class SentinelSueldoPorIndustriaDataDinamicoRepository : GenericRepository<TSentinelSueldoPorIndustriaDataDinamico>, ISentinelSueldoPorIndustriaDataDinamicoRepository
    {
        private Mapper _mapper;

        public SentinelSueldoPorIndustriaDataDinamicoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSentinelSueldoPorIndustriaDataDinamico, SentinelSueldoPorIndustriaDataDinamico>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSentinelSueldoPorIndustriaDataDinamico MapeoEntidad(SentinelSueldoPorIndustriaDataDinamico entidad)
        {
            try
            {
                //crea la entidad padre
                TSentinelSueldoPorIndustriaDataDinamico modelo = _mapper.Map<TSentinelSueldoPorIndustriaDataDinamico>(entidad);

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

        public TSentinelSueldoPorIndustriaDataDinamico Add(SentinelSueldoPorIndustriaDataDinamico entidad)
        {
            try
            {
                var SentinelSueldoPorIndustriaDataDinamico = MapeoEntidad(entidad);
                base.Insert(SentinelSueldoPorIndustriaDataDinamico);
                return SentinelSueldoPorIndustriaDataDinamico;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSentinelSueldoPorIndustriaDataDinamico Update(SentinelSueldoPorIndustriaDataDinamico entidad)
        {
            try
            {
                var SentinelSueldoPorIndustriaDataDinamico = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SentinelSueldoPorIndustriaDataDinamico.RowVersion = entidadExistente.RowVersion;

                base.Update(SentinelSueldoPorIndustriaDataDinamico);
                return SentinelSueldoPorIndustriaDataDinamico;
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


        public IEnumerable<TSentinelSueldoPorIndustriaDataDinamico> Add(IEnumerable<SentinelSueldoPorIndustriaDataDinamico> listadoEntidad)
        {
            try
            {
                List<TSentinelSueldoPorIndustriaDataDinamico> listado = new List<TSentinelSueldoPorIndustriaDataDinamico>();
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

        public IEnumerable<TSentinelSueldoPorIndustriaDataDinamico> Update(IEnumerable<SentinelSueldoPorIndustriaDataDinamico> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSentinelSueldoPorIndustriaDataDinamico> listado = new List<TSentinelSueldoPorIndustriaDataDinamico>();
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
        /// Obtiene todos los registros de T_SentinelSueldoPorIndustriaDataDinamico.
        /// </summary>
        /// <returns> List<SentinelSueldoPorIndustriaDataDinamicoDTO> </returns>
        public IEnumerable<SentinelSueldoPorIndustriaDataDinamicoDTO> ObtenerSentinelSueldoPorIndustriaDataDinamico()
        {
            try
            {
                List<SentinelSueldoPorIndustriaDataDinamicoDTO> rpta = new List<SentinelSueldoPorIndustriaDataDinamicoDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdCargo,
	                    IdIndustria,
	                    IdTamanioEmpresa,
	                    Nombre,
	                    Tipo,
	                    Valor,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM com.T_SentinelSueldoPorIndustriaDataDinamico
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSueldoPorIndustriaDataDinamicoDTO>>(resultado);
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
        /// Obtiene registros de T_SentinelSueldoPorIndustriaDataDinamico para mostrarse en combo.
        /// </summary>
        /// <returns> ValorIntDTO </returns>
        public IEnumerable<SentinelSueldoPorIndustriaDataDinamicoComboDTO> ObtenerCombo()
        {
            try
            {
                List<SentinelSueldoPorIndustriaDataDinamicoComboDTO> rpta = new List<SentinelSueldoPorIndustriaDataDinamicoComboDTO>();
                var query = @"SELECT Id,Nombre,IdTamanioEmpresa FROM com.T_SentinelSueldoPorIndustriaDataDinamico WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSueldoPorIndustriaDataDinamicoComboDTO>>(resultado);
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
        /// Fecha: 19/12/2022
        /// Version: 1.1
        /// Se corrige la devolucion de datos de Objeto a Entero.
        /// <summary>
        /// Obtiene el Valor de Sueldo segun los parametros enviados.
        /// </summary>
        /// <param name="idCargo">Argumentos relacionados a la Empresa e Industria</param>
        /// <param name="idIndustria">Argumentos relacionados a la Empresa e Industria</param>
        /// <param name="idTamanio">Argumentos relacionados a la Empresa e Industria</param>
        /// <returns> ValorIntDTO </returns>
        public ValorIntDTO ObtenerValorSueldoIndustria(int idCargo, int idIndustria, int idTamanio)
        {
            try
            {
                ValorIntDTO rpta = new ValorIntDTO();
                var query = @"
                    SELECT Valor
                    FROM com.T_SentinelSueldoPorIndustriaDataDinamico
                    WHERE Estado = 1
	                    AND IdCargo = @idCargo
	                    AND IdIndustria = @idIndustria
	                    AND IdTamanioEmpresa= @idTamanio";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idCargo, idIndustria, idTamanio });
                if (!string.IsNullOrEmpty(resultado) && resultado !="null")
                {
                    rpta = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
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
