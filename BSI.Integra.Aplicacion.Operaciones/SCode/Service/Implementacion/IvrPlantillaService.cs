using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: IvrPlantillaService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_IvrPlantilla
    /// </summary>
    public class IvrPlantillaService : IIvrPlantillaService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public IvrPlantillaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TIvrPlantilla, IvrPlantilla>(MemberList.None).ReverseMap();
                cfg.CreateMap<IvrPlantillaDTO, IvrPlantilla>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        

        #region Metodos Base
        public IvrPlantilla Add(IvrPlantillaDTO data,string Usuario)
        {
            try
            {
                IvrPlantilla entidad = _mapper.Map<IvrPlantilla>(data);
                entidad.Id = 0;
                entidad.UsuarioCreacion = Usuario;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;

                var modelo = _unitOfWork.IvrPlantillaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<IvrPlantilla>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IvrPlantilla Update(IvrPlantillaDTO data, string Usuario)
        {
            try
            {
                var repositorioIvrPlantilla = _unitOfWork.IvrPlantillaRepository;
                var entidad = _mapper.Map<IvrPlantilla>(repositorioIvrPlantilla.FirstById(data.Id));
                entidad.Nombre = data.Nombre;
                entidad.Texto = data.Texto;
                entidad.MenuOpcion = data.MenuOpcion;
                entidad.TextoMenu = data.TextoMenu;
                entidad.Activo = data.Activo;
                entidad.UsuarioModificacion = Usuario;
                entidad.FechaModificacion = DateTime.Now;

                var modelo = _unitOfWork.IvrPlantillaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<IvrPlantilla>(modelo);
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
                _unitOfWork.IvrPlantillaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<IvrPlantilla> Add(List<IvrPlantilla> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.IvrPlantillaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<IvrPlantilla>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<IvrPlantilla> Update(List<IvrPlantilla> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.IvrPlantillaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<IvrPlantilla>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.IvrPlantillaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_IvrPlantilla
        /// </summary>
        /// <returns> List<IvrPlantillaDTO> </returns>
        public IEnumerable<IvrPlantillaDTO> ObtenerIvrPlantilla()
        {
            try
            {
                return _unitOfWork.IvrPlantillaRepository.ObtenerIvrPlantilla();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_IvrPlantilla para mostrarse en combo.
        /// </summary>
        /// <returns> List<IvrPlantillaComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.IvrPlantillaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
