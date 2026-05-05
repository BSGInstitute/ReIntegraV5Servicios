using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion
{
    /// Servicio: GlassdoorResenaService
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Lógica de negocio para la gestión de reseñas de empleador en Glassdoor.
    /// Módulo 100% manual (sin API externa). CRUD para carga desde modales,
    /// consultas de administración vía SP y gestión de visibilidad.
    /// API pública de Glassdoor descontinuada en 2023 — captura manual periódica.
    /// </summary>
    public class GlassdoorResenaService : IGlassdoorResenaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _mapper;

        private const string USUARIO_FALLBACK = "SISTEMA";

        public GlassdoorResenaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TGlassdoorResena, GlassdoorResena>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region CRUD

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta una reseña y persiste los cambios.</summary>
        /// <param name="entidad">Entidad de reseña a insertar.</param>
        /// <returns>GlassdoorResena</returns>
        public GlassdoorResena Add(GlassdoorResena entidad)
        {
            var modelo = _unitOfWork.GlassdoorResenaRepository.Add(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<GlassdoorResena>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Actualiza una reseña y persiste los cambios.</summary>
        /// <param name="entidad">Entidad de reseña con los datos actualizados.</param>
        /// <returns>GlassdoorResena</returns>
        public GlassdoorResena Update(GlassdoorResena entidad)
        {
            var modelo = _unitOfWork.GlassdoorResenaRepository.Update(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<GlassdoorResena>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Elimina lógicamente una reseña por su Id.</summary>
        /// <param name="id">Id de la reseña a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>bool</returns>
        public bool Delete(int id, string usuario)
        {
            _unitOfWork.GlassdoorResenaRepository.Delete(id, usuario);
            _unitOfWork.Commit();
            return true;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Inserta un listado de reseñas en bloque.</summary>
        /// <param name="listadoEntidad">Listado de reseñas a insertar.</param>
        /// <returns>List de GlassdoorResena</returns>
        public List<GlassdoorResena> Add(List<GlassdoorResena> listadoEntidad)
        {
            var modelo = _unitOfWork.GlassdoorResenaRepository.Add(listadoEntidad);
            _unitOfWork.Commit();
            return _mapper.Map<List<GlassdoorResena>>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Actualiza un listado de reseñas en bloque.</summary>
        /// <param name="listadoEntidad">Listado de reseñas con los datos actualizados.</param>
        /// <returns>List de GlassdoorResena</returns>
        public List<GlassdoorResena> Update(List<GlassdoorResena> listadoEntidad)
        {
            var modelo = _unitOfWork.GlassdoorResenaRepository.Update(listadoEntidad);
            _unitOfWork.Commit();
            return _mapper.Map<List<GlassdoorResena>>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Elimina lógicamente un listado de reseñas por sus Ids.</summary>
        /// <param name="listadoIds">Listado de Ids de las reseñas a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>bool</returns>
        public bool Delete(List<int> listadoIds, string usuario)
        {
            _unitOfWork.GlassdoorResenaRepository.Delete(listadoIds, usuario);
            _unitOfWork.Commit();
            return true;
        }

        #endregion

        #region Consultas de Administración

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Retorna la grilla paginada ejecutando mkt.SP_GlassdoorResenaObtenerDatos.</summary>
        /// <param name="filtro">Filtros de la grilla (visibilidad, país, tipo empleado, fechas, paginación).</param>
        /// <returns>GlassdoorResenaGrillaPaginadaDTO</returns>
        public GlassdoorResenaGrillaPaginadaDTO ObtenerGrilla(GlassdoorResenaGrillaFiltroDTO filtro)
        {
            return _unitOfWork.GlassdoorResenaRepository.ObtenerGrilla(filtro);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Retorna los países con reseñas activas para el combo de filtros.</summary>
        /// <returns>List de GlassdoorResenaPaisComboDTO</returns>
        public List<GlassdoorResenaPaisComboDTO> ObtenerPaisesCombo()
        {
            return _unitOfWork.GlassdoorResenaRepository.ObtenerPaisesCombo();
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Retorna las ciudades de un país para el combo de filtros.</summary>
        /// <param name="idPais">Id del país a filtrar.</param>
        /// <returns>List de GlassdoorResenaCiudadComboDTO</returns>
        public List<GlassdoorResenaCiudadComboDTO> ObtenerCiudadesCombo(int idPais)
        {
            return _unitOfWork.GlassdoorResenaRepository.ObtenerCiudadesCombo(idPais);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Marca las reseñas indicadas como visibles (Mostrar=true).</summary>
        /// <param name="dto">Ids de reseñas y usuario que realiza la acción.</param>
        /// <returns>bool</returns>
        public bool MarcarResenaVisible(GlassdoorResenaMarcarMostrarDTO dto)
        {
            _unitOfWork.GlassdoorResenaRepository.MarcarResenaVisible(
                dto.IdsGlassdoorResena, dto.Usuario ?? USUARIO_FALLBACK);
            _unitOfWork.Commit();
            return true;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>Marca las reseñas indicadas como ocultas (Mostrar=false).</summary>
        /// <param name="dto">Ids de reseñas y usuario que realiza la acción.</param>
        /// <returns>bool</returns>
        public bool MarcarResenaOculta(GlassdoorResenaMarcarMostrarDTO dto)
        {
            _unitOfWork.GlassdoorResenaRepository.MarcarResenaOculta(
                dto.IdsGlassdoorResena, dto.Usuario ?? USUARIO_FALLBACK);
            _unitOfWork.Commit();
            return true;
        }

        #endregion
    }
}
