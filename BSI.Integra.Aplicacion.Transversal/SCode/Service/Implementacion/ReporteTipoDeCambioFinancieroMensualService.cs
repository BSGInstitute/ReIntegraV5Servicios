using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Google.Api.Ads.AdWords.v201809;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ReporteTipoDeCambioFinancieroMensualService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 12/08/2022
    /// <summary>
    /// Gestión general de T_ReporteTipoDeCambioFinancieroMensual
    /// </summary>
    public class ReporteTipoDeCambioFinancieroMensualService : IReporteTipoDeCambioFinancieroMensualService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public Mapper _mapperActividad;

        public ReporteTipoDeCambioFinancieroMensualService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TReporteTipoDeCambioFinancieroMensual, ReporteTipoDeCambioFinancieroMensual>(MemberList.None).ReverseMap();
                cfg.CreateMap<ReporteTipoDeCambioFinancieroMensualDTO, ReporteTipoDeCambioFinancieroMensual>(MemberList.None);
            });
            _mapper = new Mapper(config);
            _mapperActividad = new Mapper(config);
        }

        #region Metodos Base
        public ReporteTipoDeCambioFinancieroMensual Add(ReporteTipoDeCambioFinancieroMensualDTO data, string usuario)
        {
            try
            {

                var _repTipoCambioMoneda = _unitOfWork.ReporteTipoDeCambioFinancieroMensualRepository;
                var tempDiasExistente = _repTipoCambioMoneda.GetBy(x => x.IdMoneda == data.IdMoneda && x.Mes == data.Mes && x.Anio == data.Anio).ToList();
                if (tempDiasExistente.Count() == 0 || tempDiasExistente == null) { 
                    var repReporteTipoDeCambioFinancieroMensual = _unitOfWork.ReporteTipoDeCambioFinancieroMensualRepository;
                    ReporteTipoDeCambioFinancieroMensual entidad = new ReporteTipoDeCambioFinancieroMensual();
                    entidad.Id = 0;
                    entidad.MonedaAdolar = data.MonedaAdolar;
                    entidad.DolarAmoneda = data.DolarAmoneda;
                    entidad.Mes = data.Mes;
                    entidad.Anio = data.Anio;
                    entidad.IdMoneda = data.IdMoneda;
                    entidad.FechaCreacion = DateTime.Now;
                    entidad.FechaModificacion = DateTime.Now;
                    entidad.UsuarioCreacion = usuario;
                    entidad.UsuarioModificacion = usuario;
                    entidad.Estado = true;

                    var modelo = _unitOfWork.ReporteTipoDeCambioFinancieroMensualRepository.Add(entidad);
                    _unitOfWork.Commit();
                    return _mapper.Map<ReporteTipoDeCambioFinancieroMensual>(modelo);

                }
                else
                {
                    throw new Exception("Existe un tipo de cambio para esa fecha!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReporteTipoDeCambioFinancieroMensual Update(ReporteTipoDeCambioFinancieroMensualDTO data, string usuario)
        {
            try
            {


                 var repReporteTipoDeCambioFinancieroMensual = _unitOfWork.ReporteTipoDeCambioFinancieroMensualRepository;
                ReporteTipoDeCambioFinancieroMensual entidad = new ReporteTipoDeCambioFinancieroMensual();
                entidad.Id = data.Id;
                entidad.MonedaAdolar = data.MonedaAdolar;
                entidad.DolarAmoneda = data.DolarAmoneda;
                entidad.Mes = data.Mes;
                entidad.Anio = data.Anio;
                entidad.IdMoneda = data.IdMoneda;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.UsuarioCreacion = usuario;
                entidad.UsuarioModificacion = usuario;
                entidad.Estado = true;

                var modelo = _unitOfWork.ReporteTipoDeCambioFinancieroMensualRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ReporteTipoDeCambioFinancieroMensual>(modelo);
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
                _unitOfWork.ReporteTipoDeCambioFinancieroMensualRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReporteTipoDeCambioFinancieroMensual> Add(List<ReporteTipoDeCambioFinancieroMensual> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ReporteTipoDeCambioFinancieroMensualRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ReporteTipoDeCambioFinancieroMensual>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReporteTipoDeCambioFinancieroMensual> Update(List<ReporteTipoDeCambioFinancieroMensual> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ReporteTipoDeCambioFinancieroMensualRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ReporteTipoDeCambioFinancieroMensual>>(modelo);
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
                _unitOfWork.ReporteTipoDeCambioFinancieroMensualRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        public List<ReporteTipoDeCambioFinancieroMensualEnvioDTO> ObtenerTipoPagoPorAnioYMes(ReporteTipoDeCambioFinanzcieroMensualGrillaDTO entidad)
        {
            try
            {
                var modelo = _unitOfWork.ReporteTipoDeCambioFinancieroMensualRepository.ObtenerTipoPagoPorAnioYMes(entidad).OrderByDescending(w => w.Id).ToList();
                _unitOfWork.Commit();
                return _mapper.Map<List<ReporteTipoDeCambioFinancieroMensualEnvioDTO>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ReporteTipoDeCambioFinanzcieroMensualGrillaDTO> ObtenerReporteEgresoPorRubroTotal()
        {
            try
            {
                var modelo = _unitOfWork.ReporteTipoDeCambioFinancieroMensualRepository.ObtenerTipoCambioTotal();
                _unitOfWork.Commit();
                return _mapper.Map<List<ReporteTipoDeCambioFinanzcieroMensualGrillaDTO>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
