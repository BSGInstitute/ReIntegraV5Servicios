using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: CajaService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 27/06/2022
    /// <summary>
    /// Gestión general de T_Caja
    /// </summary>
    public class CajaService : ICajaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CajaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCaja, Caja>(MemberList.None).ReverseMap();
                cfg.CreateMap<CajaDatosDTO, Caja>(MemberList.None).ReverseMap();
            }


            );
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public Caja Add(CajaDatosDTO entidad, string Usuario)
        {

            try
            {
                Caja data = _mapper.Map<Caja>(entidad);
                data.Id = 0;
                data.UsuarioModificacion = Usuario;
                data.UsuarioCreacion = Usuario;
                data.FechaCreacion = DateTime.Now;
                data.FechaModificacion = DateTime.Now;
                data.Estado = true;

                var modelo = _unitOfWork.CajaRepository.Add(data);
                _unitOfWork.Commit();
                return _mapper.Map<Caja>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Caja Update(CajaDatosDTO entidad,string Usuario)
        {
            try
            {

                var rep = _unitOfWork.CajaRepository;
                var entidadActual = _mapper.Map<Caja>(rep.FirstById(entidad.Id));
                entidadActual.UsuarioModificacion = Usuario;
                entidadActual.FechaModificacion = DateTime.Now;
                entidadActual.CodigoCaja = entidad.CodigoCaja;
                entidadActual.IdEmpresaAutorizada = entidad.IdEmpresaAutorizada;
                entidadActual.IdEntidadFinanciera = entidad.IdEntidadFinanciera;
                entidadActual.IdMoneda = entidad.IdMoneda;
                entidadActual.IdCiudad = entidad.IdCiudad;
                entidadActual.IdPersonalResponsable = entidad.IdPersonalResponsable;
                entidadActual.Activo = entidad.Activo;

               
                var modelo = _unitOfWork.CajaRepository.Update(entidadActual);
                _unitOfWork.Commit();
                return _mapper.Map<Caja>(modelo);
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
                _unitOfWork.CajaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Caja> Add(List<Caja> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CajaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Caja>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Caja> Update(List<Caja> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CajaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Caja>>(modelo);
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
                _unitOfWork.CajaRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Caja
        /// </summary>
        /// <returns> List<CajaDTO> </returns>
        public IEnumerable<CajaDTO> ObtenerCaja()
        {
            try
            {
                return _unitOfWork.CajaRepository.ObtenerCaja();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene al Personal responsable de cada Caja.
        /// </summary>
        /// <returns> List<CajaResponsableDTO> </returns>
        public IEnumerable<CajaComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.CajaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la Lista de Responsables de Caja
        /// </summary>
        /// <returns> List<CajaResponsableDTO> </returns>
        public IEnumerable<CajaResponsableComboDTO> ObtenerListaCajaResponsable()
        {
            try
            {
                return _unitOfWork.CajaRepository.ObtenerListaCajaResponsable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el primer registro que conincida con IdCaja,
        /// </summary>
        /// <returns> IdCuentaCorriente </returns>
        /// <param name="IdCaja">identificador de caja</param>
        public int obtenerIdCuentaCorriente(int IdCaja)
        {
            try
            {
                return _unitOfWork.CajaRepository.obtenerIdCuentaCorriente(IdCaja);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los resgistro de FIN.V_ObtenerResumenCaja
        /// </summary>
        /// <returns> ResumenCajaDTO </returns>
        public IEnumerable<ResumenCajaDTO> ObtenerResumenCaja()
        {
            try
            {
                return _unitOfWork.CajaRepository.ObtenerResumenCaja();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }

}
