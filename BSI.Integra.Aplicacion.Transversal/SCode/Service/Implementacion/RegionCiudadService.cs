using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: RegionCiudadService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_RegionCiudad
    /// </summary>
    public class RegionCiudadService : IRegionCiudadService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public RegionCiudadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TRegionCiudad, RegionCiudad>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }



        public RegionCiudad Add(RegionCiudad data)
        {
            try
            {
                var repRegionCiudad = _unitOfWork.RegionCiudadRepository;
                RegionCiudad entidad = new RegionCiudad();
                entidad.Id = 0;
                entidad.Nombre = data.Nombre;
                entidad.CodigoBS = data.CodigoBS;
                entidad.DenominacionBS = data.DenominacionBS;
                entidad.NombreCorto = data.NombreCorto;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.UsuarioCreacion;
                entidad.UsuarioModificacion = data.UsuarioModificacion;
                entidad.Estado = true;


                var modelo = _unitOfWork.RegionCiudadRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RegionCiudad>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RegionCiudad Update(RegionCiudad data)
        {
            try
            {
                var repRegionCiudad = _unitOfWork.RegionCiudadRepository;
                RegionCiudad entidad = new RegionCiudad();
                entidad.Id = 0;
                entidad.Nombre = data.Nombre;
                entidad.CodigoBS = data.CodigoBS;
                entidad.DenominacionBS = data.DenominacionBS;
                entidad.NombreCorto = data.NombreCorto;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = data.UsuarioCreacion;
                entidad.UsuarioModificacion = data.UsuarioModificacion;
                entidad.Estado = true;

                var modelo = _unitOfWork.RegionCiudadRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<RegionCiudad>(modelo);
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
                _unitOfWork.RegionCiudadRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RegionCiudad> Add(List<RegionCiudad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RegionCiudadRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RegionCiudad>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RegionCiudad> Update(List<RegionCiudad> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.RegionCiudadRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<RegionCiudad>>(modelo);
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
                _unitOfWork.RegionCiudadRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_RegionCiudad para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboRegionCiudadDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.RegionCiudadRepository.ObtenerComboCiudad();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: MargORY.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_RegionCiudad
        /// </summary>
        /// <returns> List<RegionCiudadDTO> </returns>
        public IEnumerable<RegionCiudadPanelDTO> ObtenerRegionCiudad()
        {
            try
            {
                return _unitOfWork.RegionCiudadRepository.ObtenerRegionCiudad();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<RegionCiudadPanelDTO2> filtroPaisCiudad(int idPais, int idCiudad)
        {
            try
            {
                return _unitOfWork.RegionCiudadRepository.filtroPaisCiudad(idPais, idCiudad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Tipo Función: GET
        /// Autor: GIlmer Quispe
        /// Fecha: 07/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de Region ciudad filtrado por el Estado=1
        /// </summary> 
        /// <returns> List<RegionCiudadComboDTO> </returns>
        public List<RegionCiudadComboDTO> ObtenerPorEstado()
        {
            try
            {
                return _unitOfWork.RegionCiudadRepository.ObtenerPorEstado().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
