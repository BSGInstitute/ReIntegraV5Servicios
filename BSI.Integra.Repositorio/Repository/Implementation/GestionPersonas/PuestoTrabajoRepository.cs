using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class PuestoTrabajoRepository : GenericRepository<TPuestoTrabajo>, IPuestoTrabajoRepository
    {
        private Mapper _mapper;
        public PuestoTrabajoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPuestoTrabajo, PuestoTrabajo>(MemberList.None).ReverseMap();
                cfg.CreateMap<PuestoTrabajo, PuestoTrabajoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<PuestoTrabajo, PuestoTrabajoInsertDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPuestoTrabajo MapeoEntidad(PuestoTrabajo entidad)
        {
            try
            {
                TPuestoTrabajo modelo = _mapper.Map<TPuestoTrabajo>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPuestoTrabajo Add(PuestoTrabajo entidad)
        {
            try
            {
                var PuestoTrabajo = MapeoEntidad(entidad);
                base.Insert(PuestoTrabajo);
                return PuestoTrabajo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPuestoTrabajo Update(PuestoTrabajo entidad)
        {
            try
            {
                var PuestoTrabajo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PuestoTrabajo.RowVersion = entidadExistente.RowVersion;

                base.Update(PuestoTrabajo);
                return PuestoTrabajo;
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
        public IEnumerable<TPuestoTrabajo> Add(IEnumerable<PuestoTrabajo> listadoEntidad)
        {
            try
            {
                List<TPuestoTrabajo> listado = new List<TPuestoTrabajo>();
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
        public IEnumerable<TPuestoTrabajo> Update(IEnumerable<PuestoTrabajo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPuestoTrabajo> listado = new List<TPuestoTrabajo>();
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
        /// Obtiene un registro de T_PuestoTrabajo por el Primary Key
        /// </summary>
        /// <returns>PuestoTrabajo o Nulo</returns>
        public PuestoTrabajo? ObtenerPorId(int id) 
        {
            try
            {
                var query = @"
                    SELECT Id, Nombre, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, RowVersion, IdMigracion, IdPersonalAreaTrabajo
                    FROM gp.T_PuestoTrabajo
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PuestoTrabajo>(resultado)!;
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
        /// <summary>
        /// Obtiene los registro de T_PuestoTrabajo para combo
        /// </summary>
        /// <returns>Lista de ComboDTO</returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var query = @"
                    SELECT Id, Nombre
                    FROM gp.T_PuestoTrabajo
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(), {ex.Message}");
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 04/06/2024
        /// <summary>
        /// Obtiene los registro de T_PuestoTrabajo para combo
        /// </summary>
        /// <returns>Lista de ComboDTO</returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var query = @"
                    SELECT Id, Nombre
                    FROM gp.T_PuestoTrabajo
                    WHERE Estado = 1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(), {ex.Message}");
            }
        }


        /// <summary>
		/// Obtiene la lista de todos los elementos en la tabla PuestoTrabajoRemuneracion
		/// </summary>
		/// <returns></returns>
		public List<PuestoTrabajoRemuneracionDTO> ObtenerPuestoTrabajoRemuneracionRegistrado()
        {
            try
            {
                List<PuestoTrabajoRemuneracionDTO> listaPuestoTrabajoRemuneracion = new List<PuestoTrabajoRemuneracionDTO>();
                var query = "SELECT * FROM [gp].[V_TPuestoTrabajoRemuneracion_ObtenerRegistro] WHERE Estado = 1";
                var res = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    listaPuestoTrabajoRemuneracion = JsonConvert.DeserializeObject<List<PuestoTrabajoRemuneracionDTO>>(res);
                }
                return listaPuestoTrabajoRemuneracion;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Obtiene la lista de todos los elementos en la tabla PuestoTrabajoRemuneracion
		/// </summary>
		/// <returns></returns>
        public PuestoTrabajoRemuneracionDTO ObtenerComboRemuneracion(int IdPuestoTrabajo)
        {
            try
            {
                PuestoTrabajoRemuneracionDTO PuestoTrabajoRemuneracion = new PuestoTrabajoRemuneracionDTO();
                var query = "SELECT * FROM [gp].[T_PuestoTrabajoRemuneracion] WHERE IdPuestoTrabajo = @IdPuestoTrabajo";
                var res = _dapperRepository.FirstOrDefault(query, new { IdPuestoTrabajo = IdPuestoTrabajo });

                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    PuestoTrabajoRemuneracion = JsonConvert.DeserializeObject<PuestoTrabajoRemuneracionDTO>(res);
                }
                return PuestoTrabajoRemuneracion;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Obtiene la lista de todos los elementos en la tabla PuestoTrabajoRemuneracion
		/// </summary>
		/// <returns></returns>
        public List<PuestoTrabajoGestionContratoDTO> ObtenerPuestoTrabajoRemuneracionDet(int IdPuestoTrabajoRemuneracion)
        {
            try
            {
                List<PuestoTrabajoGestionContratoDTO> listaPuestoTrabajoRemuneracion = new List<PuestoTrabajoGestionContratoDTO>();
                var query = "SELECT * FROM [gp].[T_PuestoTrabajoRemuneracionDetalle] WHERE IdPuestoTrabajoRemuneracion = @IdPuestoTrabajoRemuneracion";
                var res = _dapperRepository.QueryDapper(query, new { IdPuestoTrabajoRemuneracion = IdPuestoTrabajoRemuneracion} );

                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    listaPuestoTrabajoRemuneracion = JsonConvert.DeserializeObject<List<PuestoTrabajoGestionContratoDTO>>(res);
                }
                return listaPuestoTrabajoRemuneracion;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Eliot Ariasf F.
        /// Fecha: 21/01/2025
        /// <summary>
        /// Obtiene los registro de funcion de puesto trabajo
        /// </summary>
        /// <returns>Lista de ComboDTO</returns>
        public List<FuncionPuestoTrabajoDTO> ObtenerFuncionPuestoTrabajo()
        {
            try
            {
                List<FuncionPuestoTrabajoDTO> listaFuncionPuesto = new List<FuncionPuestoTrabajoDTO>();
                var query = "SELECT * FROM gp.V_TPuestoTrabajoFuncion_ObtenerFuncionPorPuesto";
                var res = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
                {
                    listaFuncionPuesto= JsonConvert.DeserializeObject<List<FuncionPuestoTrabajoDTO>>(res);
                }
                return listaFuncionPuesto;

            }
            catch (Exception e)
            {
                throw new Exception($"Error al obtener las funciones por puesto de Trabajo: {e.Message}");
            }
        }
        public List<PuestoTrabajoPorFechaDTO> ObtenerPuestoTrabajoRegistradoFechaModificacion()
        {
            try
            {
                List<PuestoTrabajoPorFechaDTO> listaPuestoTrabajo = new List<PuestoTrabajoPorFechaDTO>();
                var query = "SELECT Id, Nombre, IdPersonalAreaTrabajo, PersonalAreaTrabajo, IdPerfilPuestoTrabajo, Objetivo, Descripcion," +
                    "PuestoTrabajoFechaModificacion,PuestoTrabajoUsuarioModificacion," +
                    "PerfilPuestoTrabajoFechaModificacion,PerfilPuestoTrabajoUsuarioModificacion," +
                    "PersonalAreaFechaModificacion,PersonalAreaUsuarioModificacion," +
                    "PuestoTrabajoCaracteristicaPersonalFechaModificacion,PuestoTrabajoCaracteristicaPersonalUsuarioModificacion," +
                    "PuestoTrabajoCursoComplementarioFechaModificacion,PuestoTrabajoCursoComplementarioUsuarioModificacion," +
                    "PuestoTrabajoExperienciaFechaModificacion,PuestoTrabajoExperienciaUsuarioModificacion," +
                    "PuestoTrabajoFormacionAcademicaFechaModificacion,PuestoTrabajoFormacionAcademicaUsuarioModificacion," +
                    "PuestoTrabajoFuncionFechaModificacion,PuestoTrabajoFuncionUsuarioModificacion," +
                    "PuestoTrabajoRelacionFechaModificacion,PuestoTrabajoRelacionUsuarioModificacion," +
                    "PuestoTrabajoRelacionDetalleFechaModificacion,PuestoTrabajoRelacionDetalleUsuarioModificacion," +
                    "PuestoTrabajoReporteFechaModificacion,PuestoTrabajoReporteUsuarioModificacion," +
                    "PuestoTrabajoPuntajeCalificacionFechaModificacion,PuestoTrabajoPuntajeCalificacionUsuarioModificacion, " +
                    "ModuloSistemaPuestoTrabajoFechaModificacion,ModuloSistemaPuestoTrabajoUsuarioModificacion " +
                    "FROM [gp].[V_TPuestoTrabajo_ObtenerPuestoTrabajoRegistradoFechaModificacion] WHERE Estado = 1";
                var res = _dapperRepository.QueryDapper(query, null);
                if (!res.Contains("[]") && !string.IsNullOrEmpty(res))
                {
                    listaPuestoTrabajo = JsonConvert.DeserializeObject<List<PuestoTrabajoPorFechaDTO>>(res);
                }
                return listaPuestoTrabajo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
