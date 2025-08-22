using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: FurConfiguracionAutomaticaService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_FurConfiguracionAutomatica
    /// </summary>
    public class FurConfiguracionAutomaticaService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FurConfiguracionAutomaticaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFurConfiguracionAutomatica, FurConfiguracionAutomatica>(MemberList.None).ReverseMap();
                cfg.CreateMap<FurConfiguracionAutomaticaFrontDTO, FurConfiguracionAutomatica>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }



        #region Metodos Base
        public FurConfiguracionAutomatica Add(FurConfiguracionAutomaticaFrontDTO data)
        {
            try
            {
                FurConfiguracionAutomatica entidad = _mapper.Map<FurConfiguracionAutomatica>(data);
                entidad.Id = 0;
                entidad.IdFurTipoSolicitud = 3;//Siempre sera un costo Fijo
                entidad.IdMonedaPagoReal = data.IdMoneda;
                entidad.FechaGeneracionFur = data.FechaSemilla;
                entidad.IdMonedaPagoReal = data.IdMoneda;
                entidad.UsuarioCreacion = data.Usuario;
                entidad.UsuarioModificacion = data.Usuario;
                entidad.FechaModificacion = DateTime.Now;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Activo = false;
                entidad.Estado = true;

                var modelo = _unitOfWork.FurConfiguracionAutomaticaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FurConfiguracionAutomatica>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FurConfiguracionAutomatica Update(FurConfiguracionAutomaticaFrontDTO data)
        {
            try
            {
                var repositorioFurConfiguracionAutomatica = _unitOfWork.FurConfiguracionAutomaticaRepository;
                var entidadNueva = _mapper.Map<FurConfiguracionAutomatica>(data);
                var entidadActual = repositorioFurConfiguracionAutomatica.ObtenerFurConfiguracionAutomaticaById(data.Id);

                entidadNueva.IdFurTipoSolicitud = entidadActual.IdFurTipoSolicitud;
                entidadNueva.FechaGeneracionFur = data.FechaSemilla;
                entidadNueva.IdMonedaPagoReal = data.IdMoneda;

                entidadNueva.FechaCreacion = entidadActual.FechaCreacion;
                entidadNueva.FechaModificacion = DateTime.Now;
                entidadNueva.UsuarioCreacion = entidadActual.UsuarioCreacion;
                entidadNueva.UsuarioModificacion = data.Usuario;
                entidadNueva.Estado = entidadActual.Estado;
                entidadNueva.RowVersion = entidadActual.RowVersion;

                var modelo = _unitOfWork.FurConfiguracionAutomaticaRepository.Update(entidadNueva);
                _unitOfWork.Commit();
                return _mapper.Map<FurConfiguracionAutomatica>(modelo);
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
                _unitOfWork.FurConfiguracionAutomaticaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FurConfiguracionAutomatica> Add(List<FurConfiguracionAutomatica> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FurConfiguracionAutomaticaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FurConfiguracionAutomatica>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FurConfiguracionAutomatica> Update(List<FurConfiguracionAutomatica> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FurConfiguracionAutomaticaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FurConfiguracionAutomatica>>(modelo);
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
                _unitOfWork.FurConfiguracionAutomaticaRepository.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_FurConfiguracionAutomatica por Area
        /// </summary>
        /// <returns> List<FurConfiguracionAutomaticaVersionDetalleDTO> </returns>
        public List<FurConfiguracionAutomaticaVersionDetalleDTO> ObtenerFurConfiguracionAutomaticaByIdArea(int IdArea)
        {
            try
            {
                var stringId = IdArea.ToString();
                return _unitOfWork.FurConfiguracionAutomaticaRepository.ObtenerFurConfiguracionAutomaticaByIdArea(stringId);
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
        /// Obtiene todos los registros activos de T_FurConfiguracionAutomatica por Area
        /// </summary>
        /// <returns> List<FurConfiguracionAutomaticaVersionDetalleDTO> </returns>
        public List<FurConfiguracionAutomaticaVersionDetalleDTO> ObtenerFurConfiguracionAutomaticaByIdAreaActivo(int IdArea)
        {
            try
            {
                var stringId = IdArea.ToString();
                return _unitOfWork.FurConfiguracionAutomaticaRepository.ObtenerFurConfiguracionAutomaticaByIdAreaActivo(stringId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los registros de FurConfiguracionAutomatica por IdArea que no sean validos
        /// </summary>
        /// <returns> List<FurConfiguracionAutomaticaVersionDetalleDTO> </returns>
        public List<FurConfiguracionAutomaticaVersionDetalleDTO> ObtenerFurConfiguracionAutomaticaNoValida(ParametrosEnvioDTO data)
        {
            try
            {
                return _unitOfWork.FurConfiguracionAutomaticaRepository.ObtenerFurConfiguracionAutomaticaNoValida(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }

}
