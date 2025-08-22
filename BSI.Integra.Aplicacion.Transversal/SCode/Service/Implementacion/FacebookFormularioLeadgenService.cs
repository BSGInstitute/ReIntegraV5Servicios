using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System.Net;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: FacebookFormularioLeadgenService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class FacebookFormularioLeadgenService : IFacebookFormularioLeadgenService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FacebookFormularioLeadgenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFacebookFormularioLeadgen, FacebookFormularioLeadgen>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public FacebookFormularioLeadgen Add(FacebookFormularioLeadgen entidad)
        {
            try
            {
                var modelo = _unitOfWork.FacebookFormularioLeadgenRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FacebookFormularioLeadgen>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FacebookFormularioLeadgen Update(FacebookFormularioLeadgen entidad)
        {
            try
            {
                var modelo = _unitOfWork.FacebookFormularioLeadgenRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FacebookFormularioLeadgen>(modelo);
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
                _unitOfWork.FacebookFormularioLeadgenRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FacebookFormularioLeadgen> Add(List<FacebookFormularioLeadgen> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FacebookFormularioLeadgenRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FacebookFormularioLeadgen>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FacebookFormularioLeadgen> Update(List<FacebookFormularioLeadgen> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FacebookFormularioLeadgenRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FacebookFormularioLeadgen>>(modelo);
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
                _unitOfWork.FacebookFormularioLeadgenRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Margiory Ramirez Neyra
        /// Fecha: 22/11/2022
        /// Version: 1.0
        /// <summary>
        /// Procesa los leads erroneos
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio del procesamiento de Leads</param>
        /// <param name="fechaFin">Fecha de fin del procesamiento de Leads</param>
        /// <returns>bool</returns>

        public bool ProcesarDatosLeadErroneos(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                string cadenaFechaInicio = fechaInicio.ToString("yyyyMMddHHmmss");
                string cadenaFechaFinal = fechaFin.ToString("yyyyMMddHHmmss");
                string url = $"https://integrav4-webhook.bsginstitute.com/api/ErrorFabookLeads/ProcesarLeads?FechaInicio={cadenaFechaInicio}&FechaFin={cadenaFechaFinal}";
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                    wc.DownloadString(url);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<InteraccionesPorPasoProgramasDTO> Reportebot(FiltroBotDTO filtro)
        {
            try
            {
                //InteraccionesPorPasoProgramasDTO resultado = new InteraccionesPorPasoProgramasDTO();
                List<InteraccionesPorPasoProgramasDTO> resultado = new List<InteraccionesPorPasoProgramasDTO>();
                string url = $"https://localhost:7177/api/ChatBot/ReporteInteracciones?estaRegistrado={filtro.EstaRegistrado}&fechaInicio={filtro.FechaInicio.ToString()}&fechaFin={filtro.FechaFin.ToString()}";

                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                    //wc.DownloadString(url);System.Net.WebException: 'The remote server returned an error: (404) Not Found.'

                    var resp = wc.DownloadString(url);

                        resultado = JsonConvert.DeserializeObject<List<InteraccionesPorPasoProgramasDTO>>(resp);
                 


                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}

