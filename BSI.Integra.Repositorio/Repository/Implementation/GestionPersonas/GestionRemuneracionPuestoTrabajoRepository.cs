using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class GestionRemuneracionPuestoTrabajoRepository : GenericRepository<TPuestoTrabajoRemuneracion>, IGestionRemuneracionPuestoTrabajoRepository
    {
        private Mapper _mapper;
        public GestionRemuneracionPuestoTrabajoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPuestoTrabajoRemuneracion, PuestoTrabajoRemuneracion>(MemberList.None).ReverseMap();
                cfg.CreateMap<PuestoTrabajoRemuneracion, GestionRemuneracionPuestoTrabajoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PuestoTrabajoRemuneracion, TPuestoTrabajoRemuneracion>(MemberList.None).ReverseMap();
                cfg.CreateMap<PuestoTrabajoRemuneracionDetalle, TPuestoTrabajoRemuneracionDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<PuestoTrabajo, TPuestoTrabajo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TPuestoTrabajoRemuneracion MapeoEntidad(PuestoTrabajoRemuneracion entidad)
        {
            try
            {
                TPuestoTrabajoRemuneracion modelo = _mapper.Map<TPuestoTrabajoRemuneracion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void AsignacionId(TPuestoTrabajoRemuneracion entidad, PuestoTrabajoRemuneracion objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PuestoTrabajoRemuneracionDetalleDTO> ObtenerPuestoTrabajoRemuneracionVariableRegistrado(int IdPuestoTrabajoRemuneracion)
        {
            try
			{
				List <PuestoTrabajoRemuneracionDetalleDTO> listaRemuneracionVariable = new List<PuestoTrabajoRemuneracionDetalleDTO>();
				var query = "SELECT * FROM [gp].[V_TPuestoTrabajoRemuneracionVariable_ObtenerRegistro] WHERE Estado = 1 AND IdPuestoTrabajoRemuneracion = @IdPuestoTrabajoRemuneracion";
				var res = _dapperRepository.QueryDapper(query, new { IdPuestoTrabajoRemuneracion });

				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					listaRemuneracionVariable = JsonConvert.DeserializeObject<List<PuestoTrabajoRemuneracionDetalleDTO>>(res);
				}
				return listaRemuneracionVariable;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
        }

        public List<PuestoTrabajoRemuneracionDetalle> ObtenerDetallePuestoTrabajoRemuneracionPorId(int IdPuestoTrabajoRemuneracion)
        {
            try
            {
                List<PuestoTrabajoRemuneracionDetalle> listaRemuneracionVariable = new List<PuestoTrabajoRemuneracionDetalle>();
                var query = "SELECT * FROM [gp].[V_TPuestoTrabajoRemuneracionVariable_ObtenerRegistro] WHERE Estado = 1 AND IdPuestoTrabajoRemuneracion = @IdPuestoTrabajoRemuneracion";
                var res = _dapperRepository.QueryDapper(query, new { IdPuestoTrabajoRemuneracion });

                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    listaRemuneracionVariable = JsonConvert.DeserializeObject<List<PuestoTrabajoRemuneracionDetalle>>(res);
                }
                return listaRemuneracionVariable;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PuestoTrabajoRemuneracionDetalle ObtenerDetallePorId(int id)
        {
            try
            {
                PuestoTrabajoRemuneracionDetalle remuneracionPuestoDetalle = new PuestoTrabajoRemuneracionDetalle();
                var query = "SELECT * FROM [gp].[t_puestotrabajoremuneraciondetalle] WHERE Estado = 1 AND Id = @id";
                var res = _dapperRepository.QueryDapper(query, new { id });

                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    remuneracionPuestoDetalle = JsonConvert.DeserializeObject<List<PuestoTrabajoRemuneracionDetalle>>(res).FirstOrDefault();
                }
                return remuneracionPuestoDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
		/// Obtiene la lista de todos los elementos en la tabla PuestoTrabajoRemuneracionVariable
		/// </summary>
		/// <returns></returns>
		public List<ComboDTO> ObtenerRemuneracion()
        {
            try
            {
                List<ComboDTO> listaRemuneracion = new List<ComboDTO>();
                var query = "SELECT Id, Nombre FROM gp.T_RemuneracionTipo WHERE Estado = 1 ";
                var res = _dapperRepository.QueryDapper(query, new { });

                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    listaRemuneracion = JsonConvert.DeserializeObject<List<ComboDTO>>(res);
                }
                return listaRemuneracion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de todos los elementos en la tabla PuestoTrabajoRemuneracionVariable
        /// </summary>
        /// <returns></returns>
        public List<ComboDTO> ObtenerTipoRemuneracion()
        {
            try
            {
                List<ComboDTO> listaTipoRemuneracion = new List<ComboDTO>();
                var query = "SELECT Id, Nombre FROM gp.T_RemuneracionTipoCobro WHERE Estado = 1 ";
                var res = _dapperRepository.QueryDapper(query, new { });

                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    listaTipoRemuneracion = JsonConvert.DeserializeObject<List<ComboDTO>>(res);
                }
                return listaTipoRemuneracion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de todos los elementos en la tabla PuestoTrabajoRemuneracionVariable
        /// </summary>
        /// <returns></returns>
        public List<ComboDTO> ObtenerClaseRemuneracion()
        {
            try
            {
                List<ComboDTO> listaClaseRemuneracion = new List<ComboDTO>();
                var query = "SELECT Id, Nombre FROM gp.T_RemuneracionFormaCobro WHERE Estado = 1 ";
                var res = _dapperRepository.QueryDapper(query, new { });

                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    listaClaseRemuneracion = JsonConvert.DeserializeObject<List<ComboDTO>>(res);
                }
                return listaClaseRemuneracion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de todos los elementos en la tabla PuestoTrabajoRemuneracionVariable
        /// </summary>
        /// <returns></returns>
        public List<ComboDTO> ObtenerPeriodoRemuneracion()
        {
            try
            {
                List<ComboDTO> listaPeriodoRemuneracion = new List<ComboDTO>();
                var query = "SELECT Id, Nombre FROM gp.T_RemuneracionPeriodoCobro WHERE Estado = 1 ";
                var res = _dapperRepository.QueryDapper(query, new { });

                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    listaPeriodoRemuneracion = JsonConvert.DeserializeObject<List<ComboDTO>>(res);
                }
                return listaPeriodoRemuneracion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de todos los elementos en la tabla PuestoTrabajoRemuneracionVariable
        /// </summary>
        /// <returns></returns>
        public List<ComboDTO> ObtenerMonedaParaTableroComercial()
        {
            try
            {
                List<ComboDTO> listaMoneda = new List<ComboDTO>();
                var query = "SELECT Id, Codigo AS Nombre FROM pla.V_TMoneda_FiltroCodigoMoneda ";
                var res = _dapperRepository.QueryDapper(query, new { });

                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    listaMoneda = JsonConvert.DeserializeObject<List<ComboDTO>>(res);
                }
                return listaMoneda;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de todos los elementos en la tabla PuestoTrabajoRemuneracionVariable
        /// </summary>
        /// <returns></returns>
        public List<ComboDTO> ObtenerDescripcionMonetaria()
        {
            try
            {
                List<ComboDTO> listaDescripcionMonetaria = new List<ComboDTO>();
                var query = "SELECT Id, Nombre FROM gp.T_RemuneracionDescripcionMonetaria WHERE Estado=1 ";
                var res = _dapperRepository.QueryDapper(query, new { });

                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    listaDescripcionMonetaria = JsonConvert.DeserializeObject<List<ComboDTO>>(res);
                }
                return listaDescripcionMonetaria;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public TPuestoTrabajoRemuneracion Add(PuestoTrabajoRemuneracion entidad)
        {
            try
            {
                var MaterialAccion = MapeoEntidad(entidad);
                base.Insert(MaterialAccion);
                return MaterialAccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPuestoTrabajoRemuneracion Update(PuestoTrabajoRemuneracion entidad)
        {
            try
            {
                var PuestoTrabajoRemuneracion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PuestoTrabajoRemuneracion.RowVersion = entidadExistente.RowVersion;

                base.Update(PuestoTrabajoRemuneracion);
                return PuestoTrabajoRemuneracion;
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

        public bool ValidarPuestoTrabajoRemuneracionDetalle(List<PuestoTrabajoRemuneracionDetalleDTO> Detalle)
        {
            try
            {
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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

        /// Autor:  Sergio Yepez Pillco.
        /// Fecha: 17/12/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PreguntaCategoria.
        /// </summary>
        /// <returns> List<CategoriaPregunta> </returns>
        public IEnumerable<GestionRemuneracionPuestoTrabajoDTO> Obtener()
        {
            try
            {
                List<GestionRemuneracionPuestoTrabajoDTO> rpta = new List<GestionRemuneracionPuestoTrabajoDTO>();
                var query = @"SELECT * FROM [gp].[V_TPuestoTrabajoRemuneracion_ObtenerRegistro] WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<GestionRemuneracionPuestoTrabajoDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Sergio Yepez Pillco.
        /// Fecha: 24/12/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PuestoTrabajoRemuneracionDetalle.
        /// </summary>
        /// <returns> List<PuestoTrabajoRemuneracionDetalleDTO> </returns>
        public IEnumerable<PuestoTrabajoRemuneracionDetalle> ObtenerPorIdPuestoTrabajo(int idPuestoTrabajo)
        {
            try
            {
                List<PuestoTrabajoRemuneracionDetalle> rpta = new();
                var query = @"
                        SELECT 
                        *
                        FROM [gp].[V_TPuestoTrabajoRemuneracionVariable_ObtenerRegistro_V5]
                        WHERE estado = 1 AND IdPuestoTrabajoRemuneracion = @idPuestoTrabajo;";
                var resultado = _dapperRepository.QueryDapper(query, new { idPuestoTrabajo });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<List<PuestoTrabajoRemuneracionDetalle>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:  Sergio Yepez Pillco.
        /// Fecha: 17/12/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns > || null</returns>
        public PuestoTrabajoRemuneracion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT * FROM [gp].[V_TPuestoTrabajoRemuneracion_ObtenerRegistro] WHERE Estado = 1 AND id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PuestoTrabajoRemuneracion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }


    }
}
