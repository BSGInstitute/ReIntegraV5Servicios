using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: SubNivelCcRepository
    /// Autor: Gilmer Qm.
    /// Fecha: 09/05/2023
    /// <summary>
    /// Gestión general de T_SubNivelCc
    /// </summary>
    public class SubNivelCcRepository : GenericRepository<TSubNivelCc>, ISubNivelCcRepository
    {
        private Mapper _mapper;

        public SubNivelCcRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSubNivelCc, SubNivelCc>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TSubNivelCc MapeoEntidad(SubNivelCc entidad)
        {
            try
            {
                TSubNivelCc modelo = _mapper.Map<TSubNivelCc>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSubNivelCc Add(SubNivelCc entidad)
        {
            try
            {
                var SubNivelCc = MapeoEntidad(entidad);
                base.Insert(SubNivelCc);
                return SubNivelCc;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSubNivelCc Update(SubNivelCc entidad)
        {
            try
            {
                var SubNivelCc = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SubNivelCc.RowVersion = entidadExistente.RowVersion;

                base.Update(SubNivelCc);
                return SubNivelCc;
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


        public IEnumerable<TSubNivelCc> Add(IEnumerable<SubNivelCc> listadoEntidad)
        {
            try
            {
                List<TSubNivelCc> listado = new List<TSubNivelCc>();
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

        public IEnumerable<TSubNivelCc> Update(IEnumerable<SubNivelCc> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSubNivelCc> listado = new List<TSubNivelCc>();
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
        /// Autor: Gilmer Quispe.
        /// Fecha: 10/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene registros por el filtro
        /// </summary>
        /// <param name="filtroSubNivelCcDTO"> filtro para obtener datos </param> 
        /// <returns> List<SubNivelCcListaDTO> </returns>
        public List<SubNivelCcListaDTO> ObtenerPorFiltro(int skip, int take, string subNivel)
        {
            try
            {
                string _querySubNivelCentroCosto = "pla.SP_GetAllSubNivel";
                var SubAreaCentroCosto = _dapperRepository.QuerySPDapper(_querySubNivelCentroCosto, new
                {
                    Skip = skip, Take = take, SubNivel = subNivel
                });
                return JsonConvert.DeserializeObject<List<SubNivelCcListaDTO>>(SubAreaCentroCosto);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Gretel Canasa
        /// Obtiene lista de subniveles de centro de costos por id area centro de costo
        /// </summary>
        /// <param name="idAreaCC"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SubNivelCcDTO>> ObtenerSubNivelCCAsync()
        {
            try
            {
                var retorno = new List<SubNivelCcDTO>();

                string _querySubNivel = "Select Id,Nombre, IdAreaCC, Codigo from pla.V_TSubNivelCC_ObtenerDatos where Estado=1 order by Id";
                var SubNivelCC = await _dapperRepository.QueryDapperAsync(_querySubNivel, null);

                if (!string.IsNullOrEmpty(SubNivelCC) && SubNivelCC != "[]")
                {
                    retorno = JsonConvert.DeserializeObject<List<SubNivelCcDTO>>(SubNivelCC);
                }
                return retorno;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Gretel Canasa
        /// Obtiene lista de subniveles de centro de costos por id area centro de costo
        /// </summary>
        /// <param name="idAreaCC"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerSubNivelCCPorAreaCCAsync(int idAreaCC)
        {
            try
            {
                var retorno = new List<ComboDTO>();

                string _querySubNivel = "Select Id,Nombre from pla.V_TSubNivelCC_ObtenerDatos where Estado=1 and IdAreaCC=@idAreaCC";
                var SubNivelCC = await _dapperRepository.QueryDapperAsync(_querySubNivel, new { IdAreaCC = idAreaCC });

                if (!string.IsNullOrEmpty(SubNivelCC) && SubNivelCC !="[]")
                {
                    retorno = JsonConvert.DeserializeObject<List<ComboDTO>> (SubNivelCC);
                }
                return retorno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 10/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el registro por el Id
        /// </summary>
        /// <param name="id"> Primary Key de la entidad </param> 
        /// <returns> SubNivelCc </returns>
        public SubNivelCc ObtenerPorId(int id)
        {
            try
            {
                var subNivelCc = new SubNivelCc();
                string _query = @"SELECT Id,
                                           Nombre,
                                           Codigo,
                                           IdAreaCC,
                                           Estado,
                                           UsuarioCreacion,
                                           UsuarioModificacion,
                                           FechaCreacion,
                                           FechaModificacion,
                                           RowVersion,
                                           IdMigracion
                                    FROM pla.T_SubNivelCC
                                    WHERE Estado = 1 AND Id=@Id";
                var retorno = _dapperRepository.FirstOrDefault(_query, new { Id = id });
                if (!string.IsNullOrEmpty(retorno) && retorno != ("null"))
                {
                    subNivelCc = JsonConvert.DeserializeObject<SubNivelCc>(retorno);
                }
                return subNivelCc;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
