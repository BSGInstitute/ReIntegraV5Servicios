using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Comercial;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Comercial
{
    /// Repositorio: TransicionFaseCriterioOportunidadRepository
    /// Autor: Jose Vega
    /// Fecha: 15/09/2025
    /// <summary>
    /// Gestión general de T_TransicionFaseCriterioOportunidad
    /// </summary>
    public class TransicionFaseCriterioOportunidadRepository : GenericRepository<TTransicionFaseCriterioOportunidad>, ITransicionFaseCriterioOportunidadRepository
    {
        private Mapper _mapper;
        public TransicionFaseCriterioOportunidadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTransicionFaseCriterioOportunidad, TransicionFaseCriterioOportunidad>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTransicionFaseCriterioOportunidad MapeoEntidad(TransicionFaseCriterioOportunidad entidad)
        {
            try
            {
                TTransicionFaseCriterioOportunidad transicionFaseCriterioOportunidad = _mapper.Map<TTransicionFaseCriterioOportunidad>(entidad);

                return transicionFaseCriterioOportunidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTransicionFaseCriterioOportunidad Add(TransicionFaseCriterioOportunidad entidad)
        {
            try
            {
                var TransicionFaseCriterioOportunidad = MapeoEntidad(entidad);
                base.Insert(TransicionFaseCriterioOportunidad);
                return TransicionFaseCriterioOportunidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTransicionFaseCriterioOportunidad Update(TransicionFaseCriterioOportunidad entidad)
        {
            try
            {
                var TransicionFaseCriterioOportunidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id);
                base.Update(TransicionFaseCriterioOportunidad);
                return TransicionFaseCriterioOportunidad;
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
        #endregion

        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> List<TransicionFaseCriterioOportunidadDTO> </returns>
        public List<TransicionFaseCriterioOportunidadDTO> Obtener()
        {
            try
            {
                List<TransicionFaseCriterioOportunidadDTO> transicionesFiltro = new();
                var query = @"SELECT Id, 
							IdTransicionFaseOportunidad,
							IdCriterioCalificacionFaseOportunidad,
							Estado,
							UsuarioCreacion,
							UsuarioModificacion,
							FechaCreacion,
							FechaModificacion,
							RowVersion,
							IdMigracion
							FROM com.T_TransicionFaseCriterioOportunidad WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    transicionesFiltro = JsonConvert.DeserializeObject<List<TransicionFaseCriterioOportunidadDTO>>(resultado)!;
                }
                return transicionesFiltro;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 15/09/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_TransicionFaseCriterioOportunidad por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> TransicionFaseCriterioOportunidad </returns>
        public TransicionFaseCriterioOportunidad ObtenerPorId(int id)
        {
            try
            {
                string query = @"SELECT Id, 
							IdTransicionFaseOportunidad,
							IdCriterioCalificacionFaseOportunidad,
							Estado,
							UsuarioCreacion,
							UsuarioModificacion,
							FechaCreacion,
							FechaModificacion,
							RowVersion,
							IdMigracion
							FROM com.T_TransicionFaseCriterioOportunidad WHERE Estado = 1 AND Id =  @Id;";
                string resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<TransicionFaseCriterioOportunidad>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#TCF-OPI-001@Error en ObtenerPorId: {ex.Message}", ex);
            }
        }
    }
}
