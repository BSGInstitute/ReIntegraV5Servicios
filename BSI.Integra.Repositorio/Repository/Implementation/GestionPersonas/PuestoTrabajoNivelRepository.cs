using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionDePersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;


namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PuestoTrabajoNivelRepository : GenericRepository<TPuestoTrabajoNivel>, IPuestoTrabajoNivelRepository
    {
        private Mapper _mapper;

        public PuestoTrabajoNivelRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPuestoTrabajoNivel, PuestoTrabajoNivel>(MemberList.None).ReverseMap();

                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPuestoTrabajoNivel MapeoEntidad(PuestoTrabajoNivel entidad)
        {
            try
            {
                TPuestoTrabajoNivel modelo = _mapper.Map<TPuestoTrabajoNivel>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPuestoTrabajoNivel Add(PuestoTrabajoNivel entidad)
        {
            try
            {
                var puestoTrabajoNivel = MapeoEntidad(entidad);
                Insert(puestoTrabajoNivel);
                return puestoTrabajoNivel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPuestoTrabajoNivel Update(PuestoTrabajoNivel entidad)
        {
            try
            {
                var puestoTrabajoNivel = MapeoEntidad(entidad);
                var entidadExistente = FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                puestoTrabajoNivel.RowVersion = entidadExistente.RowVersion;

                Update(puestoTrabajoNivel);
                return puestoTrabajoNivel;
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


        public IEnumerable<TPuestoTrabajoNivel> Add(IEnumerable<PuestoTrabajoNivel> listadoEntidad)
        {
            try
            {
                List<TPuestoTrabajoNivel> listado = new List<TPuestoTrabajoNivel>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TPuestoTrabajoNivel> Update(IEnumerable<PuestoTrabajoNivel> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPuestoTrabajoNivel> listado = new List<TPuestoTrabajoNivel>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                Update(listado);
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

        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 20/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PuestoTrabajoNivel
        /// </summary>
        /// <returns> List<PuestoTrabajoNivelDTO> </returns>
        public IEnumerable<PuestoTrabajoNivelDTO> Obtener()
        {
            try
            {
                List<PuestoTrabajoNivelDTO> rpta = new List<PuestoTrabajoNivelDTO>();
                var query = @"
                    SELECT
                            Id,Nombre,Descripcion,Estado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion,NivelVisualizacionAgenda
                    FROM gp.T_PuestoTrabajoNivel
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PuestoTrabajoNivelDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Marco Jose Villanueva Torres
        /// Fecha: 20/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PuestoTrabajoNivel
        /// </summary>
        /// <param name="idPartner">Id de Partner</param>
        /// <returns> PartnerPwDTO </returns>
        public PuestoTrabajoNivel? ObtenerPorId(int idPuestoNivelTrabajo)
        {
            try
            {
                PuestoTrabajoNivel rpta = new();
                var query = @"
                    SELECT
	                    Id,
						Nombre,
						Descripcion,
						Estado,
						UsuarioCreacion,
						UsuarioModificacion,
						FechaCreacion,
						FechaModificacion,
						RowVersion,
						IdMigracion,
                        NivelVisualizacionAgenda
                    FROM gp.T_PuestoTrabajoNivel
                    WHERE Estado = 1 AND Id = @idPuestoNivelTrabajo";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPuestoNivelTrabajo });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PuestoTrabajoNivel>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorId()", ex);
            }


            
        }
        public IEnumerable<ComboDTO> ObtenerListaParaFiltro()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre
                    FROM gp.T_PuestoTrabajoNivel
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);

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
