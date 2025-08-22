using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: OportunidadRemarketingAgendaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_OportunidadRemarketingAgenda
    /// </summary>
    public class OportunidadRemarketingAgendaRepository : GenericRepository<TOportunidadRemarketingAgendum>, IOportunidadRemarketingAgendaRepository
    {
        private Mapper _mapper;

        public OportunidadRemarketingAgendaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TOportunidadRemarketingAgendum, OportunidadRemarketingAgenda>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TOportunidadRemarketingAgendum MapeoEntidad(OportunidadRemarketingAgenda entidad)
        {
            try
            {
                //crea la entidad padre
                TOportunidadRemarketingAgendum modelo = _mapper.Map<TOportunidadRemarketingAgendum>(entidad);

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

        public TOportunidadRemarketingAgendum Add(OportunidadRemarketingAgenda entidad)
        {
            try
            {
                var OportunidadRemarketingAgenda = MapeoEntidad(entidad);
                base.Insert(OportunidadRemarketingAgenda);
                return OportunidadRemarketingAgenda;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TOportunidadRemarketingAgendum Update(OportunidadRemarketingAgenda entidad)
        {
            try
            {
                var OportunidadRemarketingAgenda = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                OportunidadRemarketingAgenda.RowVersion = entidadExistente.RowVersion;

                base.Update(OportunidadRemarketingAgenda);
                return OportunidadRemarketingAgenda;
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


        public IEnumerable<TOportunidadRemarketingAgendum> Add(IEnumerable<OportunidadRemarketingAgenda> listadoEntidad)
        {
            try
            {
                List<TOportunidadRemarketingAgendum> listado = new List<TOportunidadRemarketingAgendum>();
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

        public IEnumerable<TOportunidadRemarketingAgendum> Update(IEnumerable<OportunidadRemarketingAgenda> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TOportunidadRemarketingAgendum> listado = new List<TOportunidadRemarketingAgendum>();
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
        /// Fecha: 12/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_OportunidadRemarketingAgenda.
        /// </summary>
        /// <returns> List<OportunidadRemarketingAgendaDTO> </returns>
        public IEnumerable<OportunidadRemarketingAgendaDTO> ObtenerOportunidadRemarketingAgenda()
        {
            try
            {
                List<OportunidadRemarketingAgendaDTO> rpta = new List<OportunidadRemarketingAgendaDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdOportunidad,
	                    IdAgendaTab,
	                    AplicaRedireccion,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM mkt.T_OportunidadRemarketingAgenda
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OportunidadRemarketingAgendaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 13/08/2022
        /// Version: 1.0
        /// <summary>
        /// Desactivar la redireccion de remarketing
        /// </summary>
        /// <returns>bool</returns>
        public bool DesactivarRedireccionRemarketingAnterior(int idOportunidad)
        {
            try
            {
                List<OportunidadRemarketingAgendaDTO> rpta = new List<OportunidadRemarketingAgendaDTO>();
                var resultado = _dapperRepository.QuerySPFirstOrDefault("com.SP_DesactivarRedireccionRemarketingAnterior", new { IdOportunidad = idOportunidad });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/03/2023
        /// Version: 1.0
        /// <summary>
        /// Desactivar la redireccion de remarketing
        /// </summary>
        /// <returns>bool</returns>
        public async Task<bool> DesactivarRedireccionRemarketingAnteriorAsync(int idOportunidad)
        {
            try
            {
                List<OportunidadRemarketingAgendaDTO> rpta = new List<OportunidadRemarketingAgendaDTO>();
                var resultado = await _dapperRepository.QuerySPFirstOrDefaultAsync("com.SP_DesactivarRedireccionRemarketingAnterior", new { IdOportunidad = idOportunidad });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Margiory Ramirez
        /// Fecha: 28/11/2022
        /// Version: 1.0
        /// <summary>
        /// Eliminar la redireccion de remarketing
        /// </summary>
        /// <returns>Bool</returns>
        public bool EliminarRedireccionRemarketingAnterior(int idOportunidad)
        {
            try
            {
                string spPeticion = "[com].[SP_EliminarRedireccionRemarketingAnterior]";
                string resultadoPeticion = _dapperRepository.QuerySPFirstOrDefault(spPeticion, new { IdOportunidad = idOportunidad });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
