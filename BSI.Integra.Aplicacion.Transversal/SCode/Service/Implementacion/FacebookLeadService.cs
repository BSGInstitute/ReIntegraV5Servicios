using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.Office2010.Excel;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Transactions;
using Twilio.Jwt.AccessToken;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: TipoDatoService
    /// Autor: Margiory Ramirez Neyra
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class FacebookLeadService : IFacebookLeadService
    {
        private IUnitOfWork _unitOfWork;
        private string token = string.Empty;
        private Mapper _mapper;
        public FacebookLeadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var objToken = _unitOfWork.AutenticacionServicioExternoRepository.ObtenerTokenpoId(1);//ValorEstatico.IdAutenticacionFacebookLeadsReportes
            token = objToken != null ? objToken.Valor : string.Empty;


        }




        #region Metodos Base

        #endregion




        /// Tipo Función: POST
        /// Autor: Margiory Ramirez
        /// Fecha: 31/01/2024
        /// Versión: 1.0
        /// <summary>
        /// Procesa los leads enviados desde Webhook en objetos
        /// </summary>
        /// <param name="LeadgenInformacionDTO">Objeto de clase LeadgenInformacionDTO</param>
        /// <returns>Response 200 con el Id de AsignacionAutomaticaTemp, caso contrario response 400 con el mensaje de error</returns>

        public int ProcesarFacebookLead(LeadgenInformacionDTO LeadgenInformacionDTO)
        {
            try
            {
                string formato = "MM/dd/yyyy HH:mm:ss";
                var asignacionAutomaticaTem = new AsignacionAutomaticaTempService(_unitOfWork);

                FacebookFormularioLeadgen facebookFormularioLeadgen = new FacebookFormularioLeadgen();
                TAsignacionAutomaticaTemp rep = new TAsignacionAutomaticaTemp();
                TAnuncioFacebook anuncioFacebook = new TAnuncioFacebook();

                facebookFormularioLeadgen.IdLeadgenFacebook = LeadgenInformacionDTO.Id;
                facebookFormularioLeadgen.IdCampanhaFacebook = LeadgenInformacionDTO.AdsetId;
                facebookFormularioLeadgen.NombreCampaniaFacebook = LeadgenInformacionDTO.NombreCampania;
                facebookFormularioLeadgen.FacebookAnuncioId = LeadgenInformacionDTO.AdId;
                facebookFormularioLeadgen.FacebookAnuncioNombre = LeadgenInformacionDTO.AdName;
                facebookFormularioLeadgen.AreaFormacion = LeadgenInformacionDTO.AreaFormacion;
                facebookFormularioLeadgen.AreaTrabajo = LeadgenInformacionDTO.AreaTrabajo;
                facebookFormularioLeadgen.Cargo = LeadgenInformacionDTO.Cargo;
                facebookFormularioLeadgen.Ciudad = LeadgenInformacionDTO.Ciudad;
                facebookFormularioLeadgen.FechaCreacionFacebook = DateTime.ParseExact(LeadgenInformacionDTO.created_time, formato, CultureInfo.InvariantCulture);
                facebookFormularioLeadgen.Email = LeadgenInformacionDTO.Email;
                facebookFormularioLeadgen.NombreCompleto = LeadgenInformacionDTO.NombreCompleto;
                facebookFormularioLeadgen.Telefono = LeadgenInformacionDTO.Telefono;
                facebookFormularioLeadgen.Industria = LeadgenInformacionDTO.Industria;
                facebookFormularioLeadgen.InicioCapacitacion = LeadgenInformacionDTO.InicioCapacitacion;
                facebookFormularioLeadgen.EsProcesado = true;
                facebookFormularioLeadgen.Estado = true;
                facebookFormularioLeadgen.UsuarioCreacion = "WebhookFacebook";
                facebookFormularioLeadgen.UsuarioModificacion = "WebhookFacebook";
                facebookFormularioLeadgen.FechaCreacion = DateTime.Now;
                facebookFormularioLeadgen.FechaModificacion = DateTime.Now;

                try
                {
                    ConjuntoAnuncioFacebook conjuntoAnuncioFacebook = new ConjuntoAnuncioFacebook();

                    conjuntoAnuncioFacebook.IdAnuncioFacebook = LeadgenInformacionDTO.AdsetId;
                    conjuntoAnuncioFacebook.BudgetRemaining = Convert.ToDouble(LeadgenInformacionDTO.BudgetRemaining);
                    conjuntoAnuncioFacebook.CampaignId = LeadgenInformacionDTO.CampaignId;
                    conjuntoAnuncioFacebook.CreatedTime = DateTime.ParseExact(LeadgenInformacionDTO.created_time, formato, CultureInfo.InvariantCulture);
                    conjuntoAnuncioFacebook.DailyBudget = Convert.ToInt32(LeadgenInformacionDTO.DailyBudget);
                    conjuntoAnuncioFacebook.EffectiveStatus = LeadgenInformacionDTO.EffectiveStatus;
                    conjuntoAnuncioFacebook.Name = LeadgenInformacionDTO.AdsetName;
                    conjuntoAnuncioFacebook.OptimizationGoal = LeadgenInformacionDTO.OptimizationGoal;
                    conjuntoAnuncioFacebook.StartTime = DateTime.ParseExact(LeadgenInformacionDTO.created_time, formato, CultureInfo.InvariantCulture);
                    conjuntoAnuncioFacebook.Status = LeadgenInformacionDTO.AdsetStatus;
                    conjuntoAnuncioFacebook.UpdatedTime = DateTime.ParseExact(LeadgenInformacionDTO.created_time, formato, CultureInfo.InvariantCulture);
                    conjuntoAnuncioFacebook.IdConjuntoAnuncio = 0;
                    conjuntoAnuncioFacebook.CuentaPublicitaria = LeadgenInformacionDTO.Account;
                    conjuntoAnuncioFacebook.NombreCampania = LeadgenInformacionDTO.NombreCampania;
                    conjuntoAnuncioFacebook.Estado = true;
                    conjuntoAnuncioFacebook.FechaCreacion = DateTime.Now;
                    conjuntoAnuncioFacebook.FechaModificacion = DateTime.Now;
                    conjuntoAnuncioFacebook.UsuarioCreacion = "CARGAMASIVA";
                    conjuntoAnuncioFacebook.UsuarioModificacion = "CARGAMASIVA";




                    var respuesta = asignacionAutomaticaTem.ProcesarAsignacionAutomaticaLeadgen(LeadgenInformacionDTO);


                    string idCampania = LeadgenInformacionDTO.AdsetId?.Replace("as:", string.Empty);

                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (!string.IsNullOrEmpty(idCampania))
                        {
                            if (conjuntoAnuncioFacebook.DailyBudget == null)
                                conjuntoAnuncioFacebook.DailyBudget = 0;

                            var objetoConjuntoAnuncio = _unitOfWork.ConjuntoAnuncioRepository.FirstBy(x => x.IdConjuntoAnuncioFacebook == conjuntoAnuncioFacebook.IdAnuncioFacebook, s => new { s.Id });
                            if (objetoConjuntoAnuncio == null)
                            {
                                ConjuntoAnuncio conjuntoAnuncio = new ConjuntoAnuncio();
                                conjuntoAnuncio.IdCategoriaOrigen = ValorEstatico.IdFacebookFormulario5Campos;
                                conjuntoAnuncio.Origen = "LEADS_FACEBOOK";
                                conjuntoAnuncio.IdConjuntoAnuncioFacebook = LeadgenInformacionDTO.AdsetId;
                                conjuntoAnuncio.Estado = true;
                                conjuntoAnuncio.FechaCreacion = DateTime.Now;
                                conjuntoAnuncio.FechaModificacion = DateTime.Now;
                                conjuntoAnuncio.UsuarioCreacion = "CARGAMASIVA";
                                conjuntoAnuncio.UsuarioModificacion = "CARGAMASIVA";

                                var objetoConjuntoAnuncioFacebook = _unitOfWork.ConjuntoAnuncioFacebookRepository.FirstBy(x => x.IdAnuncioFacebook == conjuntoAnuncioFacebook.IdAnuncioFacebook, s => new { s.Name });
                                if (objetoConjuntoAnuncioFacebook != null)
                                {
                                    conjuntoAnuncio.Nombre = objetoConjuntoAnuncioFacebook.Name;
                                }
                                else
                                {
                                    conjuntoAnuncio.Nombre = LeadgenInformacionDTO.AdsetName;

                                    _unitOfWork.ConjuntoAnuncioFacebookRepository.Add(conjuntoAnuncioFacebook);
                                    _unitOfWork.Commit();


                                }

                                var resul = _unitOfWork.ConjuntoAnuncioRepository.Add(conjuntoAnuncio);
                                _unitOfWork.Commit();
                                respuesta.IdConjuntoAnuncio = resul.Id;
                            }
                            else
                                respuesta.IdConjuntoAnuncio = objetoConjuntoAnuncio.Id;
                        }

                        anuncioFacebook = _unitOfWork.AnuncioFacebookRepository.FirstBy(x => x.FacebookIdAnuncio == LeadgenInformacionDTO.AdId);

                        if (anuncioFacebook == null)
                        {


                            anuncioFacebook = new TAnuncioFacebook();
                            var objetoConjuntoAnuncioFacebook = _unitOfWork.ConjuntoAnuncioFacebookRepository.FirstBy(x => x.IdAnuncioFacebook == conjuntoAnuncioFacebook.IdAnuncioFacebook, s => new { s.Id });

                            anuncioFacebook.FacebookIdAnuncio = LeadgenInformacionDTO.AdId;
                            anuncioFacebook.FacebookNombreAnuncio = LeadgenInformacionDTO.AdName;
                            anuncioFacebook.FacebookIdConjuntoAnuncio = LeadgenInformacionDTO.AdsetId;
                            anuncioFacebook.Estado = true;
                            anuncioFacebook.FechaCreacion = DateTime.Now;
                            anuncioFacebook.FechaModificacion = DateTime.Now;
                            anuncioFacebook.UsuarioCreacion = "CARGAMASIVA";
                            anuncioFacebook.UsuarioModificacion = "CARGAMASIVA";
                            anuncioFacebook.IdConjuntoAnuncioFacebook = objetoConjuntoAnuncioFacebook.Id;

                            //_unitOfWork.AnuncioFacebookRepository.Insert(anuncioFacebook);
                            _unitOfWork.AnuncioFacebookRepository.Insert(anuncioFacebook);

                            _unitOfWork.Commit();
                        }
                        else
                        {
                            if (LeadgenInformacionDTO.AdName != anuncioFacebook.FacebookNombreAnuncio)
                            {
                                anuncioFacebook.FacebookNombreAnuncio = LeadgenInformacionDTO.AdName;

                                _unitOfWork.AnuncioFacebookRepository.Update(anuncioFacebook);
                                _unitOfWork.Commit();
                            }
                        }

                        respuesta.Procesado = false;
                        respuesta.IdAnuncioFacebook = anuncioFacebook.Id;
                        respuesta.Estado = true;
                        respuesta.UsuarioCreacion = "WebHookFacebookLeads";
                        respuesta.UsuarioModificacion = "WebHookFacebookLeads";
                        respuesta.FechaCreacion = DateTime.Now;
                        respuesta.FechaModificacion = DateTime.Now;


                        var re = _unitOfWork.FacebookFormularioLeadgenRepository.Add(facebookFormularioLeadgen);
                        _unitOfWork.Commit();

                        respuesta.IdFacebookFormularioLeadgen = re.Id;
                        rep = _unitOfWork.AsignacionAutomaticaTempRepository.Add(respuesta);
                        _unitOfWork.Commit();

                        scope.Complete();

                    }
                    return (rep.Id);
                }
                catch (Exception ex)
                {
                    facebookFormularioLeadgen.EsProcesado = false;
                    facebookFormularioLeadgen.Excepcion = !string.IsNullOrEmpty(ex.ToString()) ? ex.ToString() : "";
                    ///  _repFacebookFormularioLeadgen.Add(facebookFormularioLeadgen);

                    throw ex;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }





    }
}

