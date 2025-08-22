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
    /// Repositorio: SentinelSueldoIndividualRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 16/06/2022
    /// <summary>
    /// Gestión general de T_SentinelSueldoIndividual
    /// </summary>
    public class SentinelSueldoIndividualRepository : GenericRepository<TSentinelSueldoIndividual>, ISentinelSueldoIndividualRepository
    {
        private Mapper _mapper;

        public SentinelSueldoIndividualRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSentinelSueldoIndividual, SentinelSueldoIndividual>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSentinelSueldoIndividual MapeoEntidad(SentinelSueldoIndividual entidad)
        {
            try
            {
                //crea la entidad padre
                TSentinelSueldoIndividual modelo = _mapper.Map<TSentinelSueldoIndividual>(entidad);

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

        public TSentinelSueldoIndividual Add(SentinelSueldoIndividual entidad)
        {
            try
            {
                var SentinelSueldoIndividual = MapeoEntidad(entidad);
                base.Insert(SentinelSueldoIndividual);
                return SentinelSueldoIndividual;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSentinelSueldoIndividual Update(SentinelSueldoIndividual entidad)
        {
            try
            {
                var SentinelSueldoIndividual = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SentinelSueldoIndividual.RowVersion = entidadExistente.RowVersion;

                base.Update(SentinelSueldoIndividual);
                return SentinelSueldoIndividual;
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


        public IEnumerable<TSentinelSueldoIndividual> Add(IEnumerable<SentinelSueldoIndividual> listadoEntidad)
        {
            try
            {
                List<TSentinelSueldoIndividual> listado = new List<TSentinelSueldoIndividual>();
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

        public IEnumerable<TSentinelSueldoIndividual> Update(IEnumerable<SentinelSueldoIndividual> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSentinelSueldoIndividual> listado = new List<TSentinelSueldoIndividual>();
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
        /// Obtiene todos los registros de T_SentinelSueldoIndividual.
        /// </summary>
        /// <returns> List<SentinelSueldoIndividualDTO> </returns>
        public IEnumerable<SentinelSueldoIndividualDTO> ObtenerSentinelSueldoIndividual()
        {
            try
            {
                List<SentinelSueldoIndividualDTO> rpta = new List<SentinelSueldoIndividualDTO>();
                var query = @"
                    SELECT
	                    Id,Dni,Nombres,ApellidoPaterno,ApellidoMaterno,Industria,IdIndustria,TamanioEmpresa,IdTamanioEmpresa,EmpresaNombre,IdEmpresa,Cargo,IdCargo,AreaTrabajo,
	                    IdAreaTrabajo,AreaFormacion,IdAreaFormacion,Ciudad,IdCodigoCiudad,Pais,IdCodigoPais,SeLimiteInferior,SeLimiteSuperior,SePromedio,OrigenInformacion,Incluir,
	                    UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion
                    FROM com.T_SentinelSueldoIndividual
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSueldoIndividualDTO>>(resultado);
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
        /// Obtiene registros de T_SentinelSueldoIndividual para mostrarse en combo.
        /// </summary>
        /// <returns> List<SentinelSueldoIndividualComboDTO> </returns>
        public IEnumerable<SentinelSueldoIndividualComboDTO> ObtenerCombo()
        {
            try
            {
                List<SentinelSueldoIndividualComboDTO> rpta = new List<SentinelSueldoIndividualComboDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Dni,
	                    CONCAT(ApellidoPaterno,ApellidoMaterno,',',Nombres) AS Nombre,
	                    EmpresaNombre
                    FROM com.T_SentinelSueldoIndividual
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SentinelSueldoIndividualComboDTO>>(resultado);
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
        /// Obtiene Sueldo Promedio asociado a un DNI
        /// </summary>
        /// <param name="dni">Documento de Identificacion</param>
        /// <returns> ValorFloatDTO </returns>
        public FloatDTO ObtenerSueldoPromedioPorDni(string dni)
        {
            try
            {
                FloatDTO rpta = new FloatDTO();
                var query = @"SELECT SePromedio AS Valor FROM com.T_SentinelSueldoIndividual WHERE Estado = 1 AND Dni = @dni";
                var resultado = _dapperRepository.FirstOrDefault(query, new { dni });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<FloatDTO>(resultado)!;
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
