using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: MontoPagoCronogramaDetalleService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 09/07/2022
    /// <summary>
    /// Gestión general de T_MontoPagoCronogramaDetalle
    /// </summary>
    public class MontoPagoCronogramaDetalleService : IMontoPagoCronogramaDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public MontoPagoCronogramaDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMontoPagoCronogramaDetalle, MontoPagoCronogramaDetalle>(MemberList.None).ReverseMap();
                cfg.CreateMap<MontoPagoCronogramaDetalleDTO, MontoPagoCronogramaDetalle>(MemberList.None);
                cfg.CreateMap<MontoPagoCronogramaDetalleDTO, MontoPagoCronogramaDetalleInterfazDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public MontoPagoCronogramaDetalle Add(MontoPagoCronogramaDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.MontoPagoCronogramaDetalleRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MontoPagoCronogramaDetalle>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MontoPagoCronogramaDetalle Update(MontoPagoCronogramaDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.MontoPagoCronogramaDetalleRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<MontoPagoCronogramaDetalle>(modelo);
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
                _unitOfWork.MontoPagoCronogramaDetalleRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MontoPagoCronogramaDetalle> Add(List<MontoPagoCronogramaDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MontoPagoCronogramaDetalleRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MontoPagoCronogramaDetalle>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MontoPagoCronogramaDetalle> Update(List<MontoPagoCronogramaDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.MontoPagoCronogramaDetalleRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<MontoPagoCronogramaDetalle>>(modelo);
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
                _unitOfWork.MontoPagoCronogramaDetalleRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MontoPagoCronogramaDetalle
        /// </summary>
        /// <returns> List<MontoPagoCronogramaDetalleDTO> </returns>
        public IEnumerable<MontoPagoCronogramaDetalleDTO> ObtenerMontoPagoCronogramaDetalle()
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaDetalleRepository.ObtenerMontoPagoCronogramaDetalle();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_MontoPagoCronogramaDetalle para mostrarse en combo.
        /// </summary>
        /// <returns> List<MontoPagoCronogramaDetalleComboDTO> </returns>
        public IEnumerable<MontoPagoCronogramaDetalleComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaDetalleRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 27/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_MontoPagoCronogramaDetalle relacionados a un Cronograma.
        /// </summary>
        /// <param name="idCronograma">Id de MontoPagoCronograma</param>
        /// <returns> List<MontoPagoCronogramaDetalleDTO> </returns>
        public IEnumerable<MontoPagoCronogramaDetalleDTO> ObtenerMontoPagoCronogramaDetallePorIdCronograma(int idCronograma)
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaDetalleRepository.ObtenerMontoPagoCronogramaDetallePorIdCronograma(idCronograma);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 27/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la Vista para Portal Web de una Lista de MontoPagoCronogramaDetalle.
        /// </summary>
        /// <param name="cronogramaDetalles">Lista de  MontoPagoCronogramaDetalle</param>
        /// /// <param name="pluralMoneda">Nombre en Plural de la Moneda</param>
        /// <returns> List<MontoPagoCronogramaDetalleDTO> </returns>
        public string ObtenerVistaPortalWeb(List<MontoPagoCronogramaDetalleDTO> cronogramaDetalles, string pluralMoneda)
        {
            try
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append("<div class='card'><div class='card-header cabecera_tab'> <span class='panel-title'>Cronograma de Asesor: </span></div><br> <div class='row'> <div class='col-sm-1'></div><div class='col-sm-9'> <nav class='jumbotron' id='header' style='background-color: #094d82 !important;height: 150px;background: #094d82;margin-bottom: 0;' h_cabecera> <div class='container'> <div> <a href='https://bsgrupo.com/' style='width: 163px;height: 192px;background: #afca0a url(https://bsginstitute.com/repositorioweb/img/logo.png) no-repeat center center;position: absolute;top: 0;text-indent: -9999px;z-index: 100;'></a> </div> </div> </nav> <div style=' margin-top: 80px;margin-bottom: 80px;'></div> <div class='bloque-blanco'> <div style='background: #EEEEEE;'> <div class='container' style='padding: 0px;'> <div class='row'> <div class='col-sm-1'></div><div class='col-sm-9' style='text-align:center;'> <div style='background-color: #094D82;padding: 2px;'> <p st_texto style='color:white;'>Cronograma de pagos</p> </div> <br> <br> <div> #tablacuotas </div><br> <br>  </div> <br> <br>");

                string tabla = "<table class='table table-striped table table-hover'><thead><tr><th> Nro.Cuota </th><th> Moneda </th><th> Monto </th><th> Fecha de vencimiento</th></tr></thead><tbody>";
                foreach (var item in cronogramaDetalles.OrderBy(w => w.FechaPago))
                {
                    tabla = tabla + "<tr>";
                    tabla = tabla + "<td>" + item.NumeroCuota + "</td>";
                    tabla = tabla + "<td>" + pluralMoneda + "</td>";
                    tabla = tabla + "<td>" + item.MontoCuotaDescuento + "</td>";
                    tabla = tabla + "<td>" + item.FechaPago.ToShortDateString() + "</td>";
                    tabla = tabla + "</tr>";
                }
                tabla = tabla + "</tbody></table>";

                stringBuilder.Replace("#tablacuotas", tabla);
                return stringBuilder.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 19/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de entidades MontoPagoCronogramaDetalle a partir de una lista de MontoPagoCronogramaDetalleDTO
        /// </summary>
        /// <param name="listaDto">Lista de MontoPagoCronogramaDetalleDTO</param>
        /// <returns> List<MontoPagoCronogramaDetalleDTO> </returns>
        public IEnumerable<MontoPagoCronogramaDetalle> MapeoEntidadesDesdeListaDTO(List<MontoPagoCronogramaDetalleDTO> listaDto)
        {
            try
            {
                var entidades = _mapper.Map<List<MontoPagoCronogramaDetalle>>(listaDto);
                if (entidades != null) entidades.ForEach(p => p.Estado = true);
                return entidades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jashin Salazar
        /// Fecha: 28/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de entidades MontoPagoCronogramaDetalle a partir de una lista de MontoPagoCronogramaDetalleDTO
        /// </summary>
        /// <param name="listaDto">Lista de MontoPagoCronogramaDetalleDTO</param>
        /// <returns> List<MontoPagoCronogramaDetalleDTO> </returns>
        public IEnumerable<MontoPagoCronogramaDetalleDTO> MapeoEntidadesEntreDTOsDesdeListaDTO(List<MontoPagoCronogramaDetalleInterfazDTO> listaDto)
        {
            try
            {
                var entidades = _mapper.Map<List<MontoPagoCronogramaDetalleDTO>>(listaDto);
                return entidades;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 28/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_MontoPagoCronograma asociado al identificador.
        /// </summary>
        /// <param name="idCronograma">Id de MontoPagoCronograma</param>
        /// <returns> Listt<MontoPagoCronogramaDetalleDTO> </returns>
        public IEnumerable<MontoPagoCronogramaDetalleDTO> ObtenerPorIdCronograma(int idCronograma)
        {
            try
            {
                return _unitOfWork.MontoPagoCronogramaDetalleRepository.ObtenerPorIdCronograma(idCronograma);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
