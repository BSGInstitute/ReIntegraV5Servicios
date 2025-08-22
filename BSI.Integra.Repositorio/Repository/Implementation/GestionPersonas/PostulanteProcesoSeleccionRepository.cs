using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PostulanteProcesoSeleccionRepository : GenericRepository<TPostulanteProcesoSeleccion>, IPostulanteProcesoSeleccionRepository
    {
        private Mapper _mapper;
        public PostulanteProcesoSeleccionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPostulanteProcesoSeleccion, PostulanteProcesoSeleccion>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteProcesoSeleccion, PostulanteProcesoSeleccionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PostulanteProcesoSeleccion, TPostulanteProcesoSeleccion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPostulanteProcesoSeleccion MapeoEntidad(PostulanteProcesoSeleccion entidad)
        {
            try
            {
                TPostulanteProcesoSeleccion modelo = _mapper.Map<TPostulanteProcesoSeleccion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPostulanteProcesoSeleccion Add(PostulanteProcesoSeleccion entidad)
        {
            try
            {
                var PostulanteProcesoSeleccion = MapeoEntidad(entidad);
                base.Insert(PostulanteProcesoSeleccion);
                return PostulanteProcesoSeleccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPostulanteProcesoSeleccion Update(PostulanteProcesoSeleccion entidad)
        {
            try
            {
                var PostulanteProcesoSeleccion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PostulanteProcesoSeleccion.RowVersion = entidadExistente.RowVersion;

                base.Update(PostulanteProcesoSeleccion);
                return PostulanteProcesoSeleccion;
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
        public IEnumerable<TPostulanteProcesoSeleccion> Add(IEnumerable<PostulanteProcesoSeleccion> listadoEntidad)
        {
            try
            {
                List<TPostulanteProcesoSeleccion> listado = new List<TPostulanteProcesoSeleccion>();
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
        public IEnumerable<TPostulanteProcesoSeleccion> Update(IEnumerable<PostulanteProcesoSeleccion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPostulanteProcesoSeleccion> listado = new List<TPostulanteProcesoSeleccion>();
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
        /// Obtiene un registro de T_PostulanteProcesoSeleccion por el Primary Key
        /// </summary>
        /// <returns>PostulanteProcesoSeleccion o Nulo</returns>
        public PostulanteProcesoSeleccion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id,
	                    Id,
		                IdPostulante,
		                IdProcesoSeleccion,
		                FechaRegistro,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                IdEstadoProcesoSeleccion,
		                IdPostulanteNivelPotencial,
		                IdProveedor,
		                IdPersonal_OperadorProceso AS IdPersonalOperadorProceso,
		                IdConvocatoriaPersonal
                    FROM gp.T_PostulanteProcesoSeleccion
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PostulanteProcesoSeleccion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 04/06/2024
        /// <param name="idPostulante"> (PK) </param> 
        /// <summary>
        /// Obtiene un registro de T_PostulanteProcesoSeleccion por el Primary Key
        /// </summary>
        /// <returns>PostulanteProcesoSeleccion o Nulo</returns>
        public PostulanteProcesoSeleccion? ObtenerPorIdPostulante(int idPostulante)
        {
            try
            {
                var query = @"
                    SELECT Id,
	                    IdPostulante,
		                IdProcesoSeleccion,
		                FechaRegistro,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                IdEstadoProcesoSeleccion,
		                IdPostulanteNivelPotencial,
		                IdProveedor,
		                IdPersonal_OperadorProceso AS IdPersonalOperadorProceso,
		                IdConvocatoriaPersonal
                    FROM gp.T_PostulanteProcesoSeleccion
                    WHERE IdPostulante = @IdPostulante AND Estado = 1
                    ORDER BY Id DESC";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPostulante = idPostulante });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PostulanteProcesoSeleccion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorIdPostulante(), {ex.Message}");
            }
        }
        /// <summary>
		/// Obtiene lista de procesos de seleccion a lso que el postulante se inscribio mediante el idPostulante
		/// </summary>
		/// <param name="idPostulanteProcesoSeleccion"></param>
		/// <returns></returns>
		public PostulanteAccesoProcesoSeleccionDTO? VerificacionTokenPresenteInactivo(int idPostulanteProcesoSeleccion)
        {
            try
            {
                var query = "gp.SP_VerificacionExistenteTokenPresenteInactivo";
                //var query = "gp.SP_VerificacionExistenteTokenPresenteInactivo";
                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion });
                if (!string.IsNullOrEmpty(resultado)/* && !resultado.Contains("[]")*/)
                {
                    return JsonConvert.DeserializeObject<PostulanteAccesoProcesoSeleccionDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
		/// Obtiene datos del postulante junto a su token
		/// </summary>
		/// <param name="idPostulanteProcesoSeleccion"></param>
		/// <returns></returns>
		public PostulanteAccesoProcesoSeleccionDTO? ObtenerPostulanteProcesoSeleccion(int idPostulanteProcesoSeleccion)
        {
            try
            {
                var query = "SELECT Id, IdPostulante, Postulante, Dni, Email, ProcesoSeleccion, Token, GuidAccess FROM [gp].[V_TPostulanteProcesoSeleccion_ObtenerPostulanteProceso] WHERE Id = @IdPostulanteProcesoSeleccion AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<PostulanteAccesoProcesoSeleccionDTO>(resultado);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
		/// Obtiene lista de procesos de seleccion a lso que el postulante se inscribio mediante el idPostulante
		/// </summary>
		/// <param name="idPostulante"></param>
		/// <returns></returns>
        public List<ProcesoSeleccionInscritoDTO> ObtenerProcesoSeleccionInscrito(int idPostulante)
        {
            try
            {
                var query = "SELECT Id, IdPostulante, Postulante, IdProcesoSeleccion, ProcesoSeleccion, IdPuestoTrabajo, PuestoTrabajo, IdSede, Sede, FechaRegistro FROM [gp].[V_TPostulanteProcesoSeleccion_ObtenerProcesoSeleccionados] WHERE IdPostulante = @IdPostulante AND Estado = 1 AND Activo = 1 ORDER BY FechaRegistro DESC";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPostulante = idPostulante });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<List<ProcesoSeleccionInscritoDTO>>(resultado);
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Error en ObtenerProcesoSeleccionInscrito: " + e.Message);
            }
        }


        /// <summary>
		/// Elimina los procesos de seleccion asociados
		/// </summary>
		/// <param name="idPostulante"></param>
		/// <returns></returns>
		public bool EliminarProcesoSeleccionAsociado(int idPostulante, int idProcesoSeleccion)
        {
            try
            {
                var resultado = new Dictionary<string, bool>();

                string query = _dapperRepository.QuerySPFirstOrDefault("gp.SP_ProcesoSeleccion_EliminarAsociados", new { IdPostulante = idPostulante, IdProcesoSeleccion = idProcesoSeleccion });
                if (!string.IsNullOrEmpty(query))
                {
                    resultado = JsonConvert.DeserializeObject<Dictionary<string, bool>>(query);
                }
                return resultado.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
		/// Obtiene lista de procesos de seleccion a lso que el postulante se inscribio mediante el idPostulante
		/// </summary>
		/// <param name="idPostulanteProcesoSeleccion"></param>
		/// <returns></returns>
		public PostulanteAccesoProcesoSeleccionDTO? VerificacionTokenPresente(int idPostulanteProcesoSeleccion)
        {
            try
            {
                string query = "gp.SP_VerificacionExistenteTokenPresenteInactivo";
                string resultado = _dapperRepository.QuerySPFirstOrDefault(query, new { IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<PostulanteAccesoProcesoSeleccionDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
