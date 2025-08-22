using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class TokenPostulanteProcesoSeleccionRepository : GenericRepository<TTokenPostulanteProcesoSeleccion>, ITokenPostulanteProcesoSeleccionRepository
    {
        private Mapper _mapper;
        public TokenPostulanteProcesoSeleccionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTokenPostulanteProcesoSeleccion, TokenPostulanteProcesoSeleccion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TokenPostulanteProcesoSeleccion, TokenPostulanteProcesoSeleccionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TokenPostulanteProcesoSeleccion, TTokenPostulanteProcesoSeleccion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTokenPostulanteProcesoSeleccion MapeoEntidad(TokenPostulanteProcesoSeleccion entidad)
        {
            try
            {
                TTokenPostulanteProcesoSeleccion modelo = _mapper.Map<TTokenPostulanteProcesoSeleccion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTokenPostulanteProcesoSeleccion Add(TokenPostulanteProcesoSeleccion entidad)
        {
            try
            {
                var TokenPostulanteProcesoSeleccion = MapeoEntidad(entidad);
                base.Insert(TokenPostulanteProcesoSeleccion);
                return TokenPostulanteProcesoSeleccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTokenPostulanteProcesoSeleccion Update(TokenPostulanteProcesoSeleccion entidad)
        {
            try
            {
                var TokenPostulanteProcesoSeleccion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TokenPostulanteProcesoSeleccion.RowVersion = entidadExistente.RowVersion;

                base.Update(TokenPostulanteProcesoSeleccion);
                return TokenPostulanteProcesoSeleccion;
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
        public IEnumerable<TTokenPostulanteProcesoSeleccion> Add(IEnumerable<TokenPostulanteProcesoSeleccion> listadoEntidad)
        {
            try
            {
                List<TTokenPostulanteProcesoSeleccion> listado = new List<TTokenPostulanteProcesoSeleccion>();
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
        public IEnumerable<TTokenPostulanteProcesoSeleccion> Update(IEnumerable<TokenPostulanteProcesoSeleccion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTokenPostulanteProcesoSeleccion> listado = new List<TTokenPostulanteProcesoSeleccion>();
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
        /// Obtiene un registro de T_TokenPostulanteProcesoSeleccion por el Primary Key
        /// </summary>
        /// <returns>TokenPostulanteProcesoSeleccion o Nulo</returns>
        public TokenPostulanteProcesoSeleccion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id,
		                IdPostulanteProcesoSeleccion,
		                Token,
		                TokenHash,
		                GuidAccess,
		                Activo,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                FechaEnvioAccesos
                    FROM gp.T_TokenPostulanteProcesoSeleccion
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<TokenPostulanteProcesoSeleccion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }

        /// Autor: Eliot Ariasf F.
        /// Fecha: 11/11/2024
        /// <summary>
		/// Obtiene el ultimo token registrado del postulante proceso seleccion
		/// </summary>
		/// <param name="idPostulanteProcesoSeleccion"></param>
		/// <returns></returns>
		public TokenPostulanteProcesoSeleccionDTO ObtenerUltimoTokenPorPostulanteProcesoSeleccion(int idPostulanteProcesoSeleccion)
        {
            try
            {
                var query = @"SELECT *
                            FROM [gp].[V_TTokenPostulanteProcesoSeleccion_ObtenerTokenGenerados]
                            WHERE IdPostulanteProcesoSeleccion = @IdPostulanteProcesoSeleccion
                                    AND Estado = 1
                            ORDER BY FechaCreacion DESC;";
                var res = _dapperRepository.FirstOrDefault(query, new { IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion });
                return JsonConvert.DeserializeObject<TokenPostulanteProcesoSeleccionDTO>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
