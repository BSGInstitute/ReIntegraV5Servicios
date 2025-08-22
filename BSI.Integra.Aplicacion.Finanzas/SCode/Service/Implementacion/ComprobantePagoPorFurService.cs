using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ComprobantePagoPorFurService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_ComprobantePagoPorFur
    /// </summary>
    public class ComprobantePagoPorFurService : IComprobantePagoPorFurService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public Mapper mapperComprobantePagoFur;

        public ComprobantePagoPorFurService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TComprobantePagoPorFur, ComprobantePagoPorFur>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
            mapperComprobantePagoFur = new Mapper(config);
        }

        #region Metodos Base
        public ComprobantePagoPorFur Add(ComprobantePagoPorFur entidad)
        {
            try
            {
                var modelo = _unitOfWork.ComprobantePagoPorFurRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ComprobantePagoPorFur>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ComprobantePagoPorFur Update(ComprobantePagoPorFur entidad)
        {
            try
            {
                var modelo = _unitOfWork.ComprobantePagoPorFurRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ComprobantePagoPorFur>(modelo);
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
                _unitOfWork.ComprobantePagoPorFurRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ComprobantePagoPorFur> Add(List<ComprobantePagoPorFur> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ComprobantePagoPorFurRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ComprobantePagoPorFur>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ComprobantePagoPorFur> Update(List<ComprobantePagoPorFur> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ComprobantePagoPorFurRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ComprobantePagoPorFur>>(modelo);
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
                _unitOfWork.ComprobantePagoPorFurRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Griselberto Huaman
        /// Fecha: 20/09/2022
        /// Versión: 1.0
        /// <summary>
        /// asocia un comprobante a un FUR
        /// </summary>
        /// <returns> true </returns>
        public bool AsociarComprobante(AsociarComprobateDTO Comprobante)
        {
            try
            {
                ComprobantePagoPorFur comprobantePagoPorFur = new ComprobantePagoPorFur();

                comprobantePagoPorFur.IdComprobantePago = Comprobante.IdComprobantePago;
                comprobantePagoPorFur.IdFur = Comprobante.IdFur;
                comprobantePagoPorFur.Monto = Comprobante.Monto;
                comprobantePagoPorFur.Estado = true;
                comprobantePagoPorFur.UsuarioCreacion = Comprobante.Usuario;
                comprobantePagoPorFur.UsuarioModificacion = Comprobante.Usuario;
                comprobantePagoPorFur.FechaCreacion = DateTime.Now;
                comprobantePagoPorFur.FechaModificacion = DateTime.Now;
                comprobantePagoPorFur = this.Add(comprobantePagoPorFur);
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///  Obtiene el Id del Comprobante asociado al Comprobante Pago por Fur.
        /// </summary>
        /// <returns></returns>
        public int ObtenerIdComprobante(int? IdComprobantePagoPorFur)
        {
            try
            {
                var rep = _unitOfWork.ComprobantePagoPorFurRepository;
                int IdComprobante = rep.GetBy(x => x.Estado == true && x.Id == IdComprobantePagoPorFur).Select(x => x.IdComprobantePago).FirstOrDefault();
                return IdComprobante;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        /// <summary>
        /// Genera el desgloce de ReporteEgresoPorRubro
        /// </summary>
        /// <param name="Sedes"></param>
        /// <param name="Anio"></param>
        /// <param name="IdRubro"></param>
        /// <returns></returns>
        public List<DesgloseReporteEgresoPorRubroDTO> ObtenerDesgloceReporteEgresosPorRubro(string IdEmpresa, DateTime FechaInicio, DateTime FechaFin, int idRubro)
        {
            try
            {
                return _unitOfWork.ComprobantePagoPorFurRepository.ObtenerDesgloceReporteEgresosPorRubro(IdEmpresa, FechaInicio, FechaFin,idRubro);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ReporteEgresoPorRubroDTO> ObtenerDatosReporteEgresosPorRubro(string IdEmpresa, DateTime FechaInicio, DateTime FechaFin)
        {
            try
            {
                return _unitOfWork.ComprobantePagoPorFurRepository.ObtenerDatosReporteEgresosPorRubro(IdEmpresa, FechaInicio, FechaFin);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
