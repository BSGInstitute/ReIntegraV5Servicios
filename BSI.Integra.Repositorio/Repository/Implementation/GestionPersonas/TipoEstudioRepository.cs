using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class TipoEstudioRepository : GenericRepository<TTipoEstudio>, ITipoEstudioRepository
    {
        private Mapper _mapper;
        public TipoEstudioRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoEstudio, TipoEstudio>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoEstudio, TipoEstudioDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TipoEstudio, TTipoEstudio>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTipoEstudio MapeoEntidad(TipoEstudio entidad)
        {
            try
            {
                TTipoEstudio modelo = _mapper.Map<TTipoEstudio>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTipoEstudio Add(TipoEstudio entidad)
        {
            try
            {
                var TipoEstudio = MapeoEntidad(entidad);
                base.Insert(TipoEstudio);
                return TipoEstudio;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTipoEstudio Update(TipoEstudio entidad)
        {
            try
            {
                var TipoEstudio = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoEstudio.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoEstudio);
                return TipoEstudio;
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
        public IEnumerable<TTipoEstudio> Add(IEnumerable<TipoEstudio> listadoEntidad)
        {
            try
            {
                List<TTipoEstudio> listado = new List<TTipoEstudio>();
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
        public IEnumerable<TTipoEstudio> Update(IEnumerable<TipoEstudio> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoEstudio> listado = new List<TTipoEstudio>();
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

        /// Autor: Flavio R.M.F.
        /// Fecha: 04/06/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene un registro de T_TipoEstudio por el Primary Key
        /// </summary>
        /// <returns>TipoEstudio o Nulo</returns>
        public TipoEstudio? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
                        Id,
		                Nombre,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
                    FROM gp.T_TipoEstudio
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<TipoEstudio>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }


        /// Autor: Eliot Arias F.
        /// Fecha: 28/10/2024
        /// <summary>
        /// Obtiene los registros de T_TipoEstudio para Combo
        /// </summary>
        /// <returns>IEnumerable<TipoEstudioDTO></returns>
        public IEnumerable<TipoEstudioDTO> ObtenerListaTipoEstudioCombo()
        {
            try
            {
                var rpta = new List<TipoEstudioDTO>();
                string queryDapper = @"SELECT
                                        Id,
		                                Nombre
                                      FROM gp.T_TipoEstudio
                                      WHERE Estado = 1;";
                var resultado = _dapperRepository.QueryDapper(queryDapper, null);
                if (!String.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoEstudioDTO>>(resultado);
                    return rpta;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IEnumerable<TipoEstudioDTO> Obtener()
        {
            try
            {
                List<TipoEstudioDTO> rpta = new List<TipoEstudioDTO>();
                var query = @"
                    SELECT
	                    Id,Nombre
                    FROM gp.T_TipoEstudio
                    WHERE Estado = 1 ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoEstudioDTO>>(resultado);

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
