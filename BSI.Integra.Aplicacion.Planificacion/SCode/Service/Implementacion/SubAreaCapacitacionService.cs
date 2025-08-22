using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: SubAreaCapacitacionService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 15/07/2022
    /// <summary>
    /// Gestión general de T_SubAreaCapacitacion
    /// </summary>
    public class SubAreaCapacitacionService : ISubAreaCapacitacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public SubAreaCapacitacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSubAreaCapacitacion, SubAreaCapacitacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSubAreaCapacitacion, SubAreaCapacitacionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSubAreaCapacitacion, SubAreaCapacitacionAlternoDTO>(MemberList.None).ReverseMap();
                // cfg.CreateMap<TSubAreaCapacitacion, SubAreaCapacitacion>(MemberList.None).ReverseMap();
            }
            );
            _mapper = new Mapper(config);
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_SubAreaCapacitacion
        /// </summary>
        /// <returns> List<SubAreaCapacitacionDTO> </returns>
        public IEnumerable<SubAreaCapacitacionAlternoDTO> Obtener()
        {
            try
            {
                return _unitOfWork.SubAreaCapacitacionRepository.ObtenerAlterno();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 15/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_SubAreaCapacitacion para mostrarse en combo.
        /// </summary>
        /// <returns> List<SubAreaCapacitacionFiltroDTO> </returns>
        public IEnumerable<SubAreaCapacitacionFiltroDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.SubAreaCapacitacionRepository.ObtenerFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 08/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una insercion a la tabla T_SubAreaParametroSeo
        /// </summary>
        /// <param name="subAreaCapacitacionDTO"> datos para insercion capturados del subAreaCapacitacionDTO </param>
        /// <param name="usuario"> usuario de integra </param>
        /// <returns> bool </returns>
        public SubAreaCapacitacionAlternoDTO Insertar(SubAreaCapacitacionDTO subAreaCapacitacionDTO, string usuario)
        {
            try
            {

                SubAreaCapacitacion subAreaCapacitacion = new SubAreaCapacitacion();
                subAreaCapacitacion.Nombre = subAreaCapacitacionDTO.Nombre;
                subAreaCapacitacion.Descripcion = subAreaCapacitacionDTO.Descripcion;
                subAreaCapacitacion.IdAreaCapacitacion = subAreaCapacitacionDTO.IdAreaCapacitacion;
                subAreaCapacitacion.EsVisibleWeb = subAreaCapacitacionDTO.EsVisibleWeb;
                subAreaCapacitacion.IdSubArea = subAreaCapacitacionDTO.IdSubArea;
                subAreaCapacitacion.DescripcionHtml = subAreaCapacitacionDTO.DescripcionHtml;
                subAreaCapacitacion.AliasFacebook = "";
                subAreaCapacitacion.Estado = true;
                subAreaCapacitacion.UsuarioCreacion = usuario;
                subAreaCapacitacion.UsuarioModificacion = usuario;
                subAreaCapacitacion.FechaCreacion = DateTime.Now;
                subAreaCapacitacion.FechaModificacion = DateTime.Now;
                subAreaCapacitacion.SubAreaParametroSeoPws = subAreaCapacitacionDTO.ListaParametro.Select(x => new SubAreaParametroSeoPw
                {
                    Descripcion = string.IsNullOrEmpty(x.Descripcion) ? "<!--vacio-->" : x.Descripcion,
                    IdParametroSeoPw = x.Id,
                    Estado = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                }).ToList();

                var nuevoDato = _unitOfWork.SubAreaCapacitacionRepository.Add(subAreaCapacitacion);
                _unitOfWork.Commit();
                var subAreaCapacitacionAlterno = _mapper.Map<SubAreaCapacitacionAlternoDTO>(nuevoDato);
                subAreaCapacitacionAlterno.NombreAreaCapacitacion = _unitOfWork.AreaCapacitacionRepository.ObtenerPorId(nuevoDato.IdAreaCapacitacion).Nombre;
                return subAreaCapacitacionAlterno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 08/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una actualizacion a la tabla T_SubAreaParametroSeo
        /// </summary>
        /// <param name="subAreaCapacitacionDTO"> datos para insercion capturados del Json </param>
        /// <param name="usuario"> usuario de integra </param>
        /// <returns> bool </returns>
        public SubAreaCapacitacionAlternoDTO Actualizar(SubAreaCapacitacionDTO subAreaCapacitacionDTO, string usuario)
        {
            try
            {
                if (subAreaCapacitacionDTO.Id == null || subAreaCapacitacionDTO.Id == 0)
                {
                    throw new BadRequestException("Id 0 no valido");
                }
                SubAreaCapacitacion subAreaCapacitacion = _unitOfWork.SubAreaCapacitacionRepository.ObtenerPorId(subAreaCapacitacionDTO.Id.Value);
                if (subAreaCapacitacion == null || subAreaCapacitacion.Id == 0)
                {
                    throw new BadRequestException($"No se encontro la entidad con Id {subAreaCapacitacionDTO.Id}");
                }
                subAreaCapacitacion.Nombre = subAreaCapacitacionDTO.Nombre;
                subAreaCapacitacion.Descripcion = subAreaCapacitacionDTO.Descripcion;
                subAreaCapacitacion.IdAreaCapacitacion = subAreaCapacitacionDTO.IdAreaCapacitacion;
                subAreaCapacitacion.EsVisibleWeb = subAreaCapacitacionDTO.EsVisibleWeb;
                subAreaCapacitacion.IdSubArea = subAreaCapacitacionDTO.IdSubArea;
                subAreaCapacitacion.DescripcionHtml = subAreaCapacitacionDTO.DescripcionHtml;
                subAreaCapacitacion.AliasFacebook = "";
                subAreaCapacitacion.UsuarioModificacion = usuario;
                subAreaCapacitacion.FechaModificacion = DateTime.Now;

                subAreaCapacitacion.SubAreaParametroSeoPws = new List<SubAreaParametroSeoPw>();

                ISubAreaParametroSeoPwService subAreaParametroSeoPwService = new SubAreaParametroSeoPwService(_unitOfWork);
                subAreaParametroSeoPwService.EliminarPorIdSubAreaCapacitacion(subAreaCapacitacionDTO.Id.Value, usuario, subAreaCapacitacionDTO.ListaParametro);

                foreach (var item in subAreaCapacitacionDTO.ListaParametro)
                {
                    SubAreaParametroSeoPw subAreaParametroSeoPw;
                    if (_unitOfWork.SubAreaParametroSeoPwRepository.ExistePorIdParametroSeoPwIdSubAreaCapacitacion(item.Id, subAreaCapacitacionDTO.Id.Value))
                    {
                        subAreaParametroSeoPw = _unitOfWork.SubAreaParametroSeoPwRepository.ObtenerPorIdParametroSeoPwIdSubAreaCapacitacion(item.Id, subAreaCapacitacionDTO.Id.Value);
                        subAreaParametroSeoPw.Descripcion = string.IsNullOrEmpty(item.Descripcion) ? "<!--vacio-->" : item.Descripcion;
                        subAreaParametroSeoPw.UsuarioModificacion = usuario;
                        subAreaParametroSeoPw.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        subAreaParametroSeoPw = new();
                        subAreaParametroSeoPw.IdParametroSeoPw = item.Id;
                        subAreaParametroSeoPw.Descripcion = string.IsNullOrEmpty(item.Descripcion) ? "<!--vacio-->" : item.Descripcion;
                        subAreaParametroSeoPw.UsuarioCreacion = usuario;
                        subAreaParametroSeoPw.UsuarioModificacion = usuario;
                        subAreaParametroSeoPw.FechaCreacion = DateTime.Now;
                        subAreaParametroSeoPw.FechaModificacion = DateTime.Now;
                        subAreaParametroSeoPw.Estado = true;
                    }
                    subAreaCapacitacion.SubAreaParametroSeoPws.Add(subAreaParametroSeoPw);
                }
                var resultado = _unitOfWork.SubAreaCapacitacionRepository.Update(subAreaCapacitacion);
                _unitOfWork.Commit();
                var subAreaCapacitacionAlterno = _mapper.Map<SubAreaCapacitacionAlternoDTO>(resultado);
                subAreaCapacitacionAlterno.NombreAreaCapacitacion = _unitOfWork.AreaCapacitacionRepository.ObtenerPorId(resultado.IdAreaCapacitacion).Nombre;
                return subAreaCapacitacionAlterno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 09/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Realiza una eliminacion lógica en la tabla T_SubAreaParametroSeo
        /// </summary>
        /// <param name="idSubAreaCapacitacion"> id de la tabla </param> 
        /// <param name="usuario"> usuario del sistema integra </param> 
        /// <returns> bool </returns>
        public bool Eliminar(int idSubAreaCapacitacion, string usuario)
        {
            try
            {
                if (_unitOfWork.SubAreaCapacitacionRepository.ExistePorId(idSubAreaCapacitacion))
                {
                    _unitOfWork.SubAreaCapacitacionRepository.Delete(idSubAreaCapacitacion, usuario);
                    var hijoSubAreaParametroSeo = _unitOfWork.SubAreaParametroSeoPwRepository.ObtenerPorIdSubAreaCapacitacion(idSubAreaCapacitacion);
                    _unitOfWork.SubAreaParametroSeoPwRepository.Delete(hijoSubAreaParametroSeo.Select(x => x.Id).ToList(), usuario);
                    _unitOfWork.Commit();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 02/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el informacion contenido de ParametroSeoPw por el IdSubAreaCapacitacion
        /// </summary> 
        /// <returns> List<ParametroContenidoDTO> </returns>
        public IEnumerable<ParametroContenidoDTO> ObtenerParametroContenidoPorIdSubAreaCapacitacion(int idSubAreaCapacitacion)
        {
            try
            {
                return _unitOfWork.SubAreaParametroSeoPwRepository.ObtenerParametroContenidoPorIdSubAreaCapacitacion(idSubAreaCapacitacion);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
