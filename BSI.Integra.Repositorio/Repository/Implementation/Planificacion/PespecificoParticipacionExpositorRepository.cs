using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;


namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PespecificoParticipacionExpositorRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 30/05/2023
    /// <summary>
    /// Gestión general de T_PespecificoParticipacionExpositor
    /// </summary>
    public class PespecificoParticipacionExpositorRepository : GenericRepository<TPespecificoParticipacionExpositor>, IPespecificoParticipacionExpositorRepository
    {
        private Mapper _mapper;

        public PespecificoParticipacionExpositorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPespecificoParticipacionExpositor, PespecificoParticipacionExpositor>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPespecificoParticipacionExpositor MapeoEntidad(PespecificoParticipacionExpositor entidad)
        {
            try
            {
                return _mapper.Map<TPespecificoParticipacionExpositor>(entidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPespecificoParticipacionExpositor Add(PespecificoParticipacionExpositor entidad)
        {
            try
            {
                var PespecificoParticipacionExpositor = MapeoEntidad(entidad);
                base.Insert(PespecificoParticipacionExpositor);
                return PespecificoParticipacionExpositor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPespecificoParticipacionExpositor Update(PespecificoParticipacionExpositor entidad)
        {
            try
            {
                var PespecificoParticipacionExpositor = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PespecificoParticipacionExpositor.RowVersion = entidadExistente.RowVersion;

                base.Update(PespecificoParticipacionExpositor);
                return PespecificoParticipacionExpositor;
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
        public IEnumerable<TPespecificoParticipacionExpositor> Add(IEnumerable<PespecificoParticipacionExpositor> listadoEntidad)
        {
            try
            {
                List<TPespecificoParticipacionExpositor> listado = new List<TPespecificoParticipacionExpositor>();
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
        public IEnumerable<TPespecificoParticipacionExpositor> Update(IEnumerable<PespecificoParticipacionExpositor> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPespecificoParticipacionExpositor> listado = new List<TPespecificoParticipacionExpositor>();
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
        /// Fecha: 30/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_PespecificoParticipacionExpositor por id
        /// </summary>
        /// <returns> List<PespecificoParticipacionExpositorDTO> </returns>
        public PespecificoParticipacionExpositor? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT 
                        Id,
		                IdPEspecifico AS IdPespecifico,
		                Orden,
		                Grupo,
		                IdExpositor_Curso AS IdExpositorCurso,
		                ExpositorCurso,
		                IdExpositor_Grupo AS IdExpositorGrupo,
		                ExpositorGrupo,
		                IdExpositorV3,
		                ExpositorV3,
		                IdExpositor_GrupoConfirmado AS IdExpositorGrupoConfirmado,
		                IdProveedor_PlanificacionGrupo AS IdProveedorPlanificacionGrupo,
		                IdProveedor_OperacionesGrupoConfirmado AS IdProveedorOperacionesGrupoConfirmado,
		                IdProveedor_FurHonorario AS IdProveedorFurHonorario,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                EsSilaboAprobado
	                FROM pla.T_PEspecificoParticipacionExpositor
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PespecificoParticipacionExpositor>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PePER-OPI001@Error en ObtenerPorId: {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/05/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_PespecificoParticipacionExpositor por id
        /// </summary>
        /// <returns> List<PespecificoParticipacionExpositorDTO> </returns>
        public IEnumerable<PespecificoParticipacionExpositor>? ObtenerPorIds(IEnumerable<int> id)
        {
            try
            {
                List<PespecificoParticipacionExpositor> rpta = new();
                var query = @"
                    SELECT 
                        Id,
		                IdPEspecifico AS IdPespecifico,
		                Orden,
		                Grupo,
		                IdExpositor_Curso AS IdExpositorCurso,
		                ExpositorCurso,
		                IdExpositor_Grupo AS IdExpositorGrupo,
		                ExpositorGrupo,
		                IdExpositorV3,
		                ExpositorV3,
		                IdExpositor_GrupoConfirmado AS IdExpositorGrupoConfirmado,
		                IdProveedor_PlanificacionGrupo AS IdProveedorPlanificacionGrupo,
		                IdProveedor_OperacionesGrupoConfirmado AS IdProveedorOperacionesGrupoConfirmado,
		                IdProveedor_FurHonorario AS IdProveedorFurHonorario,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                EsSilaboAprobado
	                FROM pla.T_PEspecificoParticipacionExpositor
                    WHERE Estado = 1 AND Id IN @id";
                var resultado = _dapperRepository.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PespecificoParticipacionExpositor>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PePER-OPIs001@Error en ObtenerPorIds: {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de T_PespecificoParticipacionExpositor por id
        /// </summary>
        /// <returns> Entidad - PespecificoParticipacionExpositor </returns>
        public PespecificoParticipacionExpositor? ObtenerPorIdPespecificoYGrupo(int idPespecifico, int grupo)
        {
            try
            {
                var query = @"
                    SELECT 
                        Id,
		                IdPEspecifico AS IdPespecifico,
		                Orden,
		                Grupo,
		                IdExpositor_Curso AS IdExpositorCurso,
		                ExpositorCurso,
		                IdExpositor_Grupo AS IdExpositorGrupo,
		                ExpositorGrupo,
		                IdExpositorV3,
		                ExpositorV3,
		                IdExpositor_GrupoConfirmado AS IdExpositorGrupoConfirmado,
		                IdProveedor_PlanificacionGrupo AS IdProveedorPlanificacionGrupo,
		                IdProveedor_OperacionesGrupoConfirmado AS IdProveedorOperacionesGrupoConfirmado,
		                IdProveedor_FurHonorario AS IdProveedorFurHonorario,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion,
		                EsSilaboAprobado
	                FROM pla.T_PEspecificoParticipacionExpositor
                    WHERE Estado = 1 AND IdPEspecifico = @idPespecifico AND Grupo = @grupo";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPespecifico, grupo });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<PespecificoParticipacionExpositor>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PePER-OPIPEYG-001@Error en ObtenerPorId: {ex.Message}", ex);
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 15/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene grupos para el filtro de PEspecifico
        /// </summary>
        /// <returns> Lista DTO - List<FiltroDTO> </returns>
        public IEnumerable<PEspecificoHistorialParticipacionDocenteDTO> ObtenerHistorialParticipacion(ParticipacionExpositorFiltroDTO dto)
        {
            try
            {
                var query = "pla.SP_ObtenerHistorialParticipacionV3_Filtrado";
                var resultado = _dapperRepository.QuerySPDapper(query,
                new
                {
                    dto.IdExpositor,
                    dto.IdProgramaEspecifico,
                    dto.IdCentroCosto,
                    dto.IdCodigoBSCiudad,
                    dto.IdEstadoPEspecifico,
                    dto.IdModalidadCurso,
                    dto.IdPGeneral,
                    dto.IdArea,
                    dto.IdSubArea,
                    dto.IdCentroCostoD,
                    dto.IdProveedorPlanificacion,
                    dto.IdProveedorOperaciones,
                    dto.IdProveedorFur
                });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PEspecificoHistorialParticipacionDocenteDTO>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PePER-OPIPEYG-001@Error en ObtenerHistorialParticipacion: {ex.Message}", ex);
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 15/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene grupos para el filtro de PEspecifico
        /// </summary>
        /// <returns> Lista DTO - List<FiltroDTO> </returns>
        public bool ActualizarRegistroAsistencias(int idCursoActual, string nombreUsuario)
        {
            try
            {
                var query = "ope.SP_ActulizarEstadoAsistenciaCompletaDocente";
                var resultado = _dapperRepository.QuerySPDapper(query, new { idCursoActual, nombreUsuario });
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PePER-OPIPEYG-001@Error en ActualizarRegistroAsistencias: {ex.Message}", ex);
            }
        }
        /// Autor: Christian A. Quispe Mamani
        /// Fecha: 15/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene grupos para el filtro de PEspecifico
        /// </summary>
        /// <returns> Lista DTO - List<FiltroDTO> </returns>
        public bool ActualizarRegistroNotas(int idCursoActual, string nombreUsuario)
        {
            try
            {
                var query = "ope.SP_ActulizarEstadoNotaCompletaDocente";
                var resultado = _dapperRepository.QuerySPDapper(query, new { idCursoActual, nombreUsuario });
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"#PePER-OPIPEYG-001@Error en ActualizarRegistroNotas: {ex.Message}", ex);
            }
        }
    }
}



