using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using DocumentFormat.OpenXml.Office2010.Excel;
using Mandrill.Utilities;
using Nancy.Json;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Transactions;
using System.Web;
using DocumentFormat.OpenXml.Wordprocessing;
//using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroTipoCampaniaGeneralWhatsAppDTO;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CampaniaGeneralWhatsAppService
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 12/09/2022
    /// <summary>
    /// Gestión general de T_CampaniaGeneralWhatsApp
    /// </summary>
    public class CampaniaGeneralWhatsAppService : ICampaniaGeneralWhatsAppService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CampaniaGeneralWhatsAppService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCampaniaGeneralWhatsApp, CampaniaGeneralWhatsApp>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public CampaniaGeneralWhatsApp Add(CampaniaGeneralWhatsApp entidad)
        {
            try
            {
                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CampaniaGeneralWhatsApp>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CampaniaGeneralWhatsApp Update(CampaniaGeneralWhatsApp entidad)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CampaniaGeneralWhatsApp>(modelo);
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
                _unitOfWork.CampaniaGeneralWhatsAppRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CampaniaGeneralWhatsApp> Add(List<CampaniaGeneralWhatsApp> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CampaniaGeneralWhatsApp>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CampaniaGeneralWhatsApp> Update(List<CampaniaGeneralWhatsApp> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CampaniaGeneralWhatsApp>>(modelo);
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
                _unitOfWork.CampaniaGeneralWhatsAppRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        public List<ObtenerCampaniaGeneralDetalleWhatsAppDTO>? ObtenerCampaniaGeneralDetalleWhatsApp(IdDTO id)
        {
            try
            {

                List<ObtenerCampaniaGeneralDetalleWhatsAppGrupoDTO> modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerCampaniaGeneralDetalleWhatsApp(id);
                List<ObtenerCampaniaGeneralDetalleWhatsAppDTO>? resultadoAgrupado = new List<ObtenerCampaniaGeneralDetalleWhatsAppDTO>();

                resultadoAgrupado = modelo.GroupBy(x => new { x.Id, x.NombreCampaniaGeneralWhatsApp, x.FechaInicioEnvioWhatsapp, x.HoraEnvio }).Select(x => new ObtenerCampaniaGeneralDetalleWhatsAppDTO
                {
                    Id = x.Key.Id,
                    NombreCampaniaGeneralWhatsApp = x.Key.NombreCampaniaGeneralWhatsApp,
                    FechaInicioEnvioWhatsapp = x.Key.FechaInicioEnvioWhatsapp,
                    HoraEnvio = x.Key.HoraEnvio,
                    ObtenerCampaniaGeneralDetallePrioridadWhatsApp = x.GroupBy(y => new { y.IdCampaniaGeneralDetalleWhatsApp, y.NombreCampaniaOrigen, y.Prioridad, y.Nombre, y.ActivarMasivo, y.Programados, y.CantidadBase, y.Enviados }).Select(y => new ObtenerCampaniaGeneralDetallePrioridadWhatsAppDTO
                    {
                        IdCampaniaGeneralDetalleWhatsApp = y.Key.IdCampaniaGeneralDetalleWhatsApp,
                        NombreCampaniaOrigen = y.Key.NombreCampaniaOrigen,
                        Prioridad = y.Key.Prioridad,
                        Nombre = y.Key.Nombre,
                        ActivarMasivo = y.Key.ActivarMasivo,
                        Programados = y.Key.Programados,
                        CantidadBase = y.Key.CantidadBase,
                        Enviados = y.Key.Enviados,


                    }).ToList(),
                }).ToList();
                return resultadoAgrupado;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertarCampaniaGeneralWhatsApp(StringDTO nombre, string Usuario)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.InsertarCampaniaGeneralWhatsApp(nombre, Usuario);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CampaniaGeneralWhatsAppDTO ObtenerCampaniaGeneralWhatsApp(IdDTO id)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerCampaniaGeneralWhatsApp(id);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActualizarCampaniaGeneralWhatsApp(ActualizarCampaniaGeneralWhatsAppDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.ActualizarCampaniaGeneralWhatsApp(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ObtenerCampaniaGeneralGrillaWhatsAppDTO> ObtenerCampaniaGeneralGrillaWhatsApp()
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerCampaniaGeneralGrillaWhatsApp();
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarCampaniaGeneralWhatsApp(EliminarCampaniaGeneralWhatsAppDTO json)
        {
            try
            {
                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.EliminarCampaniaGeneralWhatsApp(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ActualizarActivarMasivoPorCampania(ActualizarActivarMasivoPorCampaniaDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.ActualizarActivarMasivoPorCampania(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool EliminarCampaniaGeneralDetalleWhatsApp(EliminarCampaniaGeneralDetalleWhatsAppDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.EliminarCampaniaGeneralDetalleWhatsApp(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool InsertarCampaniaGeneralDetalleWhatsApp(InsertarCampaniaGeneralDetalleWhatsAppDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.InsertarCampaniaGeneralDetalleWhatsApp(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool InsertarCampaniaGeneralDetalleExcelWhatsApp(InsertarCampaniaGeneralDetalleWhatsAppDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.InsertarCampaniaGeneralDetalleExcelWhatsApp(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ActualizarCamposCampaniaGeneralDetalleWhatsApp(ActualizarCamposCampaniaGeneralDetalleWhatsAppDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.ActualizarCamposCampaniaGeneralDetalleWhatsApp(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ProcesarDataPorPrioridadExcel(ProcesarDataPorPrioridadExcelAlumnoDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.ProcesarDataPorPrioridadExcel(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ObtenerConfiguracionCampaniaGeneralDetalleWhatsAppDTO ObtenerConfiguracionCampaniaGeneralDetalleWhatsApp(IdDTO id)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerConfiguracionCampaniaGeneralDetalleWhatsApp(id);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAgrupadoDTO ObtenerCampaniaGeneralDetalleResponsablePorPrioridad(IdDTO id)
        {
            try
            {
                List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadDTO> modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerCampaniaGeneralDetalleResponsablePorPrioridad(id);
                ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAgrupadoDTO resultadoAgrupado = new ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAgrupadoDTO();

                resultadoAgrupado = modelo.GroupBy(x => new { x.Id, x.CantidadBase, x.CantidadDisponible }).Select(x => new ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAgrupadoDTO
                {
                    Id = x.Key.Id,
                    CantidadBase = x.Key.CantidadBase,
                    CantidadDisponible = x.Key.CantidadDisponible,
                    ObtenerCampaniaGeneralDetalleResponsablePorPrioridadLista = x.GroupBy(y => new { y.IdCampaniaGeneralDetalleResponsableWhatsApp, y.Asesor, y.Plantilla, y.CentroCosto, y.Cantidad, y.Enviados, y.AlumnoConfigurado }).Select(y => new ObtenerCampaniaGeneralDetalleResponsablePorPrioridadListaDTO
                    {
                        IdCampaniaGeneralDetalleResponsableWhatsApp = y.Key.IdCampaniaGeneralDetalleResponsableWhatsApp,
                        Asesor = y.Key.Asesor,
                        Plantilla = y.Key.Plantilla,
                        CentroCosto = y.Key.CentroCosto,
                        Cantidad = y.Key.Cantidad,
                        Enviados = y.Key.Enviados,
                        AlumnoConfigurado = y.Key.AlumnoConfigurado
                    }).ToList(),
                }).FirstOrDefault();

                return resultadoAgrupado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarCampaniaGeneralDetalleResponsableWhatsApp(EliminarCampaniaGeneralDetalleResponsableWhatsAppDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.EliminarCampaniaGeneralDetalleResponsableWhatsApp(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertarCampaniaGeneralDetalleResponsableWhatsApp(InsertarCampaniaGeneralDetalleResponsableWhatsAppDTO json, string usuario)
        {
            try
            {
                var serializerProceso = new JavaScriptSerializer();

                List<AlumnoWhatsAppMasivo> ListaAlumnos = new List<AlumnoWhatsAppMasivo>();
                List<AlumnoWhatsAppMasivoJSON> ListaAlumnosReemplazo = new List<AlumnoWhatsAppMasivoJSON>();
                PrioridadDatosDTO obj = new PrioridadDatosDTO();
                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.InsertarCampaniaGeneralDetalleResponsableWhatsApp(json);
                var modeloDatos = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerDatosPorPrioridadAsignada(modelo.Valor);

                obj.Cantidad = json.Cantidad;
                obj.IdCampaniaGeneralDetalleWhatsApp = json.IdCampaniaGeneralDetalleWhatsApp;
                obj.Dias = json.Dias;
                List<AlumnoWhatsAppMasivoBaseDTO> Alumnos = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerAlumnoConfiguradoPorPrioridad(obj);
                foreach (AlumnoWhatsAppMasivoBaseDTO Alu in Alumnos)
                {
                    AlumnoWhatsAppMasivo Alumno = new AlumnoWhatsAppMasivo();
                    Alumno.IdAlumno = Alu.IdAlumno;
                    Alumno.Celular = Alu.CelularWhatsApp;
                    Alumno.WhatsAppEmpresaIdPais = Alu.WhatsAppEmpresaIdPais;
                    Alumno.ListaObjetoPlantilla = new List<DatoPlantillaWhatsAppDTO>();
                    ListaAlumnos.Add(Alumno);
                }
                ListaAlumnos = ReemplazarEtiquetaMasivosWhatsApp(ListaAlumnos, modeloDatos.IdPlantilla, modeloDatos.IdProgramaGeneral);
                InsertarCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO dto = new InsertarCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO();
                foreach (var AluRemplazo in ListaAlumnos)
                {
                    if (AluRemplazo.Plantilla != "")
                    {
                        AlumnoWhatsAppMasivoJSON Alumno = new AlumnoWhatsAppMasivoJSON();
                        Alumno.IdAlumno = AluRemplazo.IdAlumno;
                        Alumno.Celular = AluRemplazo.Celular;
                        Alumno.Plantilla = AluRemplazo.Plantilla;
                        Alumno.WhatsAppEmpresaIdPais = AluRemplazo.WhatsAppEmpresaIdPais;
                        Alumno.ObjetoPlantilla = JsonConvert.SerializeObject(AluRemplazo.ListaObjetoPlantilla);
                        Alumno.Dias = json.Dias;
                        ListaAlumnosReemplazo.Add(Alumno);
                    }
                }
                dto.IdCampaniaGeneralDetalleResponsableWhatsApp = modelo.Valor;
                dto.Usuario = usuario;
                dto.Json = serializerProceso.Serialize(ListaAlumnosReemplazo);
                _unitOfWork.CampaniaGeneralWhatsAppRepository.InsertarCampaniaGeneralDetalleResponsableAlumnoWhatsApp(dto);

                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EnvioMensajePorPlantilla(WhatsAppPlantillaDTO json, string numeroIdentificador = null)
        {
            try
            {

                var Serializer = new JavaScriptSerializer();
                RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();

                WhatsAppEnviarMensajeDTO objetoWhatsAppHook = new WhatsAppEnviarMensajeDTO();
                List<AlumnoWhatsAppMasivo> ListaAlumnos = new List<AlumnoWhatsAppMasivo>();
                int IdProgramaGeneral = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerProgramaGeneral(json.IdCentroCosto);
                string? Plantilla = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerPlantillaWhatsApp(json.IdPlantilla);

                AlumnoWhatsAppMasivo Alumno = new AlumnoWhatsAppMasivo();
                Alumno.IdAlumno = json.IdAlumno;
                Alumno.Celular = json.CelularWhatsApp;
                Alumno.WhatsAppEmpresaIdPais = json.IdPais;
                Alumno.ObjetoPlantilla = "";
                Alumno.ListaObjetoPlantilla = new List<DatoPlantillaWhatsAppDTO>();
                ListaAlumnos.Add(Alumno);

                ListaAlumnos = ReemplazarEtiquetaMasivosWhatsApp(ListaAlumnos, json.IdPlantilla, IdProgramaGeneral);



                objetoWhatsAppHook.Id = 0;
                objetoWhatsAppHook.WaTo = json.CelularWhatsApp;
                objetoWhatsAppHook.WaId = null;
                objetoWhatsAppHook.WaType = "hsm";
                objetoWhatsAppHook.WaTypeMensaje = 8;
                objetoWhatsAppHook.WaRecipientType = "hsm";
                objetoWhatsAppHook.WaBody = Plantilla;
                objetoWhatsAppHook.WaFile = null;
                objetoWhatsAppHook.WaFileName = null;
                objetoWhatsAppHook.WaMimeType = null;
                objetoWhatsAppHook.WaSha256 = null;
                objetoWhatsAppHook.WaLink = null;
                objetoWhatsAppHook.WaCaption = ListaAlumnos[0].Plantilla;
                objetoWhatsAppHook.IdPais = json.IdPais;
                objetoWhatsAppHook.EsMigracion = true;
                objetoWhatsAppHook.IdMigracion = 0;
                objetoWhatsAppHook.IdPersonal = json.IdPersonal;
                objetoWhatsAppHook.IdAlumno = json.IdAlumno;
                objetoWhatsAppHook.usuario = json.usuario;
                objetoWhatsAppHook.DatosPlantillaWhatsApp = JsonConvert.DeserializeObject<List<DatosPlantillaWhatsAppDTO>>(ListaAlumnos[0].ObjetoPlantilla);
                var serializedResult = Serializer.Serialize(objetoWhatsAppHook);
                string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketing";
                //string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketing";

                if (!string.IsNullOrEmpty(numeroIdentificador))
                    url = $"{url}V2?numeroIdentificador={numeroIdentificador}";

                try
                {

                    datoRespuesta = UrlPost(url, serializedResult);

                    if (datoRespuesta.EstadoMensaje == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch { }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool EnvioListaMensajePorPlantilla(WhatsAppPlantillaListaDTO json)
        {
            try
            {
                var Serializer = new JavaScriptSerializer();
                RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();

                List<AlumnoWhatsAppMasivo> ListaAlumnos = new List<AlumnoWhatsAppMasivo>();
                int IdProgramaGeneral = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerProgramaGeneral(json.IdCentroCosto);
                string? Plantilla = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerPlantillaWhatsApp(json.IdPlantilla);

                foreach (var alumnoData in json.Alumnos)
                {
                    AlumnoWhatsAppMasivo Alumno = new AlumnoWhatsAppMasivo
                    {
                        IdAlumno = alumnoData.IdAlumno,
                        Celular = alumnoData.CelularWhatsApp,
                        WhatsAppEmpresaIdPais = alumnoData.IdPais,
                        ObjetoPlantilla = "",
                        ListaObjetoPlantilla = new List<DatoPlantillaWhatsAppDTO>()
                    };

                    ListaAlumnos.Add(Alumno);
                }

                ListaAlumnos = ReemplazarEtiquetaMasivosWhatsApp(ListaAlumnos, json.IdPlantilla, IdProgramaGeneral);
                bool alMenosUnEnvioExitoso = false;

                string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketing";
                //string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketing";

                foreach (var alumnoData in ListaAlumnos)
                {
                    try
                    {
                        var objetoWhatsAppHook = new WhatsAppEnviarMensajeDTO
                        {
                            Id = 0,
                            WaTo = alumnoData.Celular,
                            WaId = null,
                            WaType = "hsm",
                            WaTypeMensaje = 8,
                            WaRecipientType = "hsm",
                            WaBody = Plantilla,
                            WaFile = null,
                            WaFileName = null,
                            WaMimeType = null,
                            WaSha256 = null,
                            WaLink = null,
                            WaCaption = alumnoData.Plantilla,
                            IdPais = alumnoData.WhatsAppEmpresaIdPais,
                            EsMigracion = true,
                            IdMigracion = 0,
                            IdPersonal = json.IdPersonal,
                            IdAlumno = alumnoData.IdAlumno,
                            usuario = json.usuario,
                            DatosPlantillaWhatsApp = JsonConvert.DeserializeObject<List<DatosPlantillaWhatsAppDTO>>(alumnoData.ObjetoPlantilla),
                        };
                        var serializedResult = Serializer.Serialize(objetoWhatsAppHook);

                        datoRespuesta = UrlPost(url, serializedResult);
                        if (datoRespuesta.EstadoMensaje == true)
                            alMenosUnEnvioExitoso = true;
                    }
                    catch (Exception exEnvio)
                    {
                        continue;
                    }
                }
                return alMenosUnEnvioExitoso;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EnvioMensajePorTexto(WhatsAppMensajeTextoDTO json, string numeroIdentificador = null)
        {
            try
            {
                var Serializer = new JavaScriptSerializer();
                RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();

                WhatsAppEnviarMensajeDTO objetoWhatsAppHook = new WhatsAppEnviarMensajeDTO();
                objetoWhatsAppHook.Id = 0;
                objetoWhatsAppHook.WaTo = json.CelularWhatsApp;
                objetoWhatsAppHook.WaId = null;
                objetoWhatsAppHook.WaType = "text";
                objetoWhatsAppHook.WaTypeMensaje = 8;
                objetoWhatsAppHook.WaRecipientType = "text";
                objetoWhatsAppHook.WaBody = json.Mensaje;
                objetoWhatsAppHook.WaFile = null;
                objetoWhatsAppHook.WaFileName = null;
                objetoWhatsAppHook.WaMimeType = null;
                objetoWhatsAppHook.WaSha256 = null;
                objetoWhatsAppHook.WaLink = null;
                objetoWhatsAppHook.WaCaption = null;
                objetoWhatsAppHook.IdPais = json.IdPais;
                objetoWhatsAppHook.EsMigracion = true;
                objetoWhatsAppHook.IdMigracion = 0;
                objetoWhatsAppHook.IdPersonal = json.IdPersonal;
                objetoWhatsAppHook.IdAlumno = json.IdAlumno;
                objetoWhatsAppHook.usuario = json.usuario;
                objetoWhatsAppHook.DatosPlantillaWhatsApp = null;
                var serializedResult = Serializer.Serialize(objetoWhatsAppHook);

                string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketing";
                //string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphMarketing";

                if (!string.IsNullOrEmpty(numeroIdentificador))
                    url = $"{url}V2?numeroIdentificador={numeroIdentificador}";

                try
                {

                    datoRespuesta = Task.Run(() => UrlPostAsync(url, serializedResult)).Result;
                    //datoRespuesta = UrlPost(url, serializedResult);

                    if (datoRespuesta.EstadoMensaje == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch { }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public bool EnvioMensajePorTextoFacebook(WhatsAppMensajeTextoFacebookDTO json)
        {
            try
            {
                var Serializer = new JavaScriptSerializer();
                RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();

                WhatsAppEnviarMensajeDTO objetoWhatsAppHook = new WhatsAppEnviarMensajeDTO();
                objetoWhatsAppHook.Id = 0;
                objetoWhatsAppHook.WaTo = json.CelularWhatsApp;
                objetoWhatsAppHook.WaId = null;
                objetoWhatsAppHook.WaType = "text";
                objetoWhatsAppHook.WaTypeMensaje = 8;
                objetoWhatsAppHook.WaRecipientType = "text";
                objetoWhatsAppHook.WaBody = json.Mensaje;
                objetoWhatsAppHook.WaFile = null;
                objetoWhatsAppHook.WaFileName = null;
                objetoWhatsAppHook.WaMimeType = null;
                objetoWhatsAppHook.WaSha256 = null;
                objetoWhatsAppHook.WaLink = null;
                objetoWhatsAppHook.WaCaption = null;
                objetoWhatsAppHook.IdPais = json.IdPais;
                objetoWhatsAppHook.EsMigracion = true;
                objetoWhatsAppHook.IdMigracion = 0;
                objetoWhatsAppHook.IdPersonal = json.IdPersonal;

                objetoWhatsAppHook.IdAlumno = json.IdAlumno;
                objetoWhatsAppHook.usuario = json.usuario;
                objetoWhatsAppHook.DatosPlantillaWhatsApp = null;
                var serializedResult = Serializer.Serialize(objetoWhatsAppHook);


                string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphFacebookMarketing";
                //string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphFacebookMarketing";
                try
                {
                    //datoRespuesta = UrlPost(url, serializedResult);
                    datoRespuesta = Task.Run(() => UrlPostAsync(url, serializedResult)).Result;
                    PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO item = new PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO();
                    //Json.CelularWhatsApp = objetoWhatsAppHook.WaTo;
                    //Json.IdAlumno = objetoWhatsAppHook.IdAlumno;
                    //Json.WhatsAppEmpresaIdPais = objetoWhatsAppHook.IdPais;
                    //Json.MensajePlantillaHtml = objetoWhatsAppHook.WaCaption;
                    //Json.ObjetoPlantilla = item.ObjetoPlantilla;
                    objetoWhatsAppHook.WaId = datoRespuesta.WaId;
                    //Json.IdPersonal = objetoWhatsAppHook.IdPersonal;
                    var serializedResultInsertEnviado = Serializer.Serialize(json);
                    bool ResultadoInserccion = _unitOfWork.WhatsAppConfiguracionPreEnvioRepository.InsertarCampaniaGeneralDetalleResponsableAlumnoEnviadoWhatsApp(serializedResultInsertEnviado, item.WaId, item.IdCampaniaGeneralDetalleResponsableWhatsApp);
                    return (datoRespuesta.EstadoMensaje == true) ? true : false;
                }
                catch { }
                return false;



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool EnvioMensajeArchivoFacebook(WhatsAppMensajeArchivoFacebookDTO json, int idPersonal, string usuario)
        {
            try
            {
                var Serializer = new JavaScriptSerializer();
                RespuestaMensajeHookDTO datoRespuesta = new RespuestaMensajeHookDTO();

                WhatsAppEnviarMensajeDTO objetoWhatsAppHook = new WhatsAppEnviarMensajeDTO();
                objetoWhatsAppHook.Id = 0;
                objetoWhatsAppHook.WaTo = json.WaTo;
                objetoWhatsAppHook.WaId = null;
                objetoWhatsAppHook.WaType = json.WaType;
                objetoWhatsAppHook.WaTypeMensaje = 2;
                objetoWhatsAppHook.WaRecipientType = "individual";
                objetoWhatsAppHook.WaBody = null;
                objetoWhatsAppHook.WaFile = null;
                objetoWhatsAppHook.WaFileName = json.WaFileName;
                objetoWhatsAppHook.WaMimeType = null;
                objetoWhatsAppHook.WaSha256 = null;
                objetoWhatsAppHook.WaLink = json.WaLink;
                objetoWhatsAppHook.WaCaption = null;
                objetoWhatsAppHook.IdPais = json.IdPais;
                objetoWhatsAppHook.EsMigracion = true;
                objetoWhatsAppHook.IdMigracion = 0;
                objetoWhatsAppHook.IdPersonal = idPersonal;
                objetoWhatsAppHook.IdAlumno = json.IdAlumno;
                objetoWhatsAppHook.usuario = usuario;
                objetoWhatsAppHook.DatosPlantillaWhatsApp = null;
                var serializedResult = Serializer.Serialize(objetoWhatsAppHook);


                string url = $"https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphFacebookMarketing";
                // string url = $"https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphFacebookMarketing";
                try
                {
                    datoRespuesta = UrlPost(url, serializedResult);
                    return (datoRespuesta.EstadoMensaje) ? true : false;
                }
                catch { }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Edson Daniel Mayta Escobedo
        /// Fecha: 19/09/2022
        /// Version: 1.0
        /// <summary>
        /// método que funciona el de asignación, manda los datos a V4 para que el dato se asigne
        /// </summary>
        /// <returns>bool</returns>
        public RespuestaMensajeHookDTO UrlPost(string UrlBase, string jsonStringResult)
        {
            RespuestaMensajeHookDTO respuestaMensajeHook = new RespuestaMensajeHookDTO();
            try
            {
                var http = (HttpWebRequest)WebRequest.Create(new Uri(UrlBase));
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "POST";

                string parsedContent = jsonStringResult;
                ASCIIEncoding encoding = new ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(parsedContent);

                Stream newStream = http.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();

                var response = http.GetResponse();

                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                var content = sr.ReadToEnd();
                respuestaMensajeHook = JsonConvert.DeserializeObject<RespuestaMensajeHookDTO>(content);

                return respuestaMensajeHook;
            }
            catch (Exception ex)
            {
                return respuestaMensajeHook;
            }

        }
        public bool ProcesarDataPorPrioridadSendinblue(ProcesarDataPorPrioridadSendinblueDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.ProcesarDataPorPrioridadSendinblue(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ComboCampaniaGeneralDetalleResponsableWhatsAppDTO ObtenerComboCampaniaGeneralDetalleResponsableWhatsApp()
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerComboCampaniaGeneralDetalleResponsableWhatsApp();
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ObtenerComboRespuestaWhatsAppp ObtenerComboRespuestaWhatsAppp()
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerComboRespuestaWhatsAppp();
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ObtenerComboCampaniasSendinBlueDTO ObtenerComboCampaniasSendinBlue()
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerComboCampaniasSendinBlue();
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ObtenerComboCentroCostoCampaniasSendinBlueDTO> ObtenerComboCentroCostoCampaniasSendinBlue()
        {
            try
            {

                List<ObtenerComboCentroCostoCampaniasSendinBlueDTO> dto = new List<ObtenerComboCentroCostoCampaniasSendinBlueDTO>();
                dto = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerComboCentroCostoCampaniasSendinBlue();
                _unitOfWork.Commit();
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Autor: edson daniel mayta escobedo
        /// Fecha: 21-03-2023
        /// Descipcion:realzia el reemplazo de las etiquetas necesarias para el envio de campanias
        /// </summary>
        /// <param name="NumeroAlumno"></param>
        /// <param name="IdPlantilla"></param>
        /// <param name="IdPGeneral"></param>
        /// <param name="IdCampaniaGeneralDetalle"></param>
        /// <returns></returns>
        public List<AlumnoWhatsAppMasivo> ReemplazarEtiquetaMasivosWhatsApp(List<AlumnoWhatsAppMasivo> NumeroAlumno, int IdPlantilla, int IdPGeneral)
        {
            string valor = string.Empty;
            string plantillaBaseGeneral = string.Empty;
            var serializerProceso = new JavaScriptSerializer();
            bool alumnoProcesadoConExito = true;


            try
            {
                var rpta = _unitOfWork.CentroCostoRepository.ObtenerRemplazoPlantilla(IdPGeneral);
                plantillaBaseGeneral = _unitOfWork.PlantillaClaveValorRepository.GetBy(x => x.IdPlantilla == IdPlantilla && x.Clave == "Texto", x => new { x.Valor }).FirstOrDefault().Valor;
                List<int> IdAlumno = NumeroAlumno.Select(x => x.IdAlumno).ToList();

                List<AlumnoInformacionBasicaDTO> listaAlumno;

                if (IdAlumno[0] == 0)
                {
                    listaAlumno = new List<AlumnoInformacionBasicaDTO>
                    {
                        new AlumnoInformacionBasicaDTO
                        {
                            Id = 0,
                            Nombre1 = "",
                            Nombre2 = "",
                            ApellidoMaterno = "",
                            ApellidoPaterno = ""
                        }
                    };
                }
                else
                {
                    listaAlumno = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerDatosAlumno(IdAlumno);
                }



                foreach (var alumnoEtiqueta in NumeroAlumno)
                {
                    string plantillaBase = plantillaBaseGeneral;
                    try
                    {
                        alumnoEtiqueta.ListaObjetoPlantilla = new List<DatoPlantillaWhatsAppDTO>();
                        var alumno = listaAlumno.FirstOrDefault(x => x.Id == alumnoEtiqueta.IdAlumno);
                        var primerProgramaInt = _unitOfWork.CampaniaGeneralWhatsAppRepository.OntenerUltimoProgramaInteresado(alumnoEtiqueta.IdAlumno);
                        var programaPorb = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerProgramaProbabilidadAlumno(alumnoEtiqueta.IdAlumno, 3);

                        if (alumno != null)
                        {
                            foreach (string word in plantillaBase.Split('{'))
                            {
                                DatoPlantillaWhatsAppDTO PlantillaEtiqueValor = new DatoPlantillaWhatsAppDTO();
                                if (word.Contains('}'))
                                {
                                    string Etiqueta = word.Split('}')[0];
                                    //Separamos solo los Id's
                                    if (Etiqueta.Contains("tPartner.nombre"))
                                        valor = rpta.NombrePartner;
                                    else if (Etiqueta.Contains("tPEspecifico.nombre"))
                                        valor = rpta.NombrePEspecifico;
                                    else if (Etiqueta.Contains("tPLA_PGeneral.Nombre"))
                                        valor = rpta.NombrePGeneral;
                                    else if (Etiqueta.Contains("tAlumnos.nombre1"))
                                        valor = alumno.Nombre1;
                                    else if (Etiqueta.Contains("tAlumnos.nombre2"))
                                        valor = alumno.Nombre2;
                                    else if (Etiqueta.Contains("tAlumnos.apepaterno"))
                                        valor = alumno.ApellidoPaterno;
                                    else if (Etiqueta.Contains("tAlumnos.apematerno"))
                                        valor = alumno.ApellidoMaterno;
                                    else if (Etiqueta.Contains("T_Alumno.NombrePGeneralUltimaSolicitudInformacion"))
                                        valor = _unitOfWork.AlumnoRepository.ObtenerNombreProgramaGeneralUltimaSolicitudInformacion(alumno.Id);
                                    else if (Etiqueta.Contains("Ultimo_Programa_Int"))
                                        valor = primerProgramaInt.Nombre;
                                    else if (Etiqueta.Contains("Top1_Programa_Mayor_Prob"))
                                        valor = programaPorb[0].Nombre;
                                    else if (Etiqueta.Contains("Top2_Programa_Mayor_Prob"))
                                        valor = programaPorb[1].Nombre;
                                    else if (Etiqueta.Contains("Top3_Programa_Mayor_Prob"))
                                        valor = programaPorb[2].Nombre;
                                    else if (Etiqueta.Contains("tRemarketingMensajeGenerado.Contenido"))
                                        valor = Task.Run(() => ObtenerContenidoMensajeRemarketingPorAlumno(alumnoEtiqueta.IdAlumno)).GetAwaiter().GetResult();
                                    if (valor != null)
                                    {
                                        valor = valor.Replace("#$%", "<br>");
                                        plantillaBase = plantillaBase.Replace("{" + Etiqueta + "}", valor);
                                        PlantillaEtiqueValor.Codigo = "{ " + Etiqueta + "}";
                                        PlantillaEtiqueValor.Texto = valor;
                                        alumnoEtiqueta.ListaObjetoPlantilla.Add(PlantillaEtiqueValor);

                                    }
                                    else
                                    {
                                        alumnoProcesadoConExito = false;
                                        break;
                                    }

                                }
                            }


                            alumnoEtiqueta.Plantilla = plantillaBase;
                        }
                    }
                    catch (Exception ex)
                    {
                        alumnoProcesadoConExito = false;
                    }

                    if (alumnoProcesadoConExito)
                    {
                        alumnoEtiqueta.ObjetoPlantilla = JsonConvert.SerializeObject(alumnoEtiqueta.ListaObjetoPlantilla);
                    }

                }

                return NumeroAlumno;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<ReporteInteraccionCampaniaGeneralDetalleDTO> ReporteInteraccionCampaniaGeneralDetalle(IdDTO id)
        {
            try
            {

                List<ReporteInteraccionCampaniaGeneralDetalleDTO> dto = new List<ReporteInteraccionCampaniaGeneralDetalleDTO>();
                dto = _unitOfWork.CampaniaGeneralWhatsAppRepository.ReporteInteraccionCampaniaGeneralDetalle(id);
                _unitOfWork.Commit();
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool SumaChatValidoWhatsApp(SumaValidadorChatDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.SumaChatValidoWhatsApp(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool RestaChatValidoWhatsApp(SumaValidadorChatDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.RestaChatValidoWhatsApp(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool SumaChatInValidoWhatsApp(SumaValidadorChatDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.SumaChatInValidoWhatsApp(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool RestaChatInValidoWhatsApp(SumaValidadorChatDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.RestaChatInValidoWhatsApp(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool SumaOportunidadWhatsApp(SumaOportunidadWhatsAootDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.SumaOportunidadWhatsApp(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool RestaOportunidadWhatsApp(SumaOportunidadWhatsAootDTO json)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.RestaOportunidadWhatsApp(json);
                _unitOfWork.Commit();
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DiasWhatsappDTO ObtenerDiasPorPrioridadWhatsapp(IdDTO id)
        {
            try
            {

                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerDiasPorPrioridadWhatsapp(id);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAgrupadoDTO ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAlterno(IdDiasWhatsappDTO datos)
        {
            try
            {

                List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadDTO> modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAlterno(datos);
                ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAgrupadoDTO resultadoAgrupado = new ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAgrupadoDTO();

                resultadoAgrupado = modelo.GroupBy(x => new { x.Id, x.CantidadBase, x.CantidadDisponible }).Select(x => new ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAgrupadoDTO
                {
                    Id = x.Key.Id,
                    CantidadBase = x.Key.CantidadBase,
                    CantidadDisponible = x.Key.CantidadDisponible,
                    ObtenerCampaniaGeneralDetalleResponsablePorPrioridadLista = x.GroupBy(y => new { y.IdCampaniaGeneralDetalleResponsableWhatsApp, y.Asesor, y.Plantilla, y.CentroCosto, y.Cantidad, y.Enviados, y.AlumnoConfigurado }).Select(y => new ObtenerCampaniaGeneralDetalleResponsablePorPrioridadListaDTO
                    {
                        IdCampaniaGeneralDetalleResponsableWhatsApp = y.Key.IdCampaniaGeneralDetalleResponsableWhatsApp,
                        Asesor = y.Key.Asesor,
                        Plantilla = y.Key.Plantilla,
                        CentroCosto = y.Key.CentroCosto,
                        Cantidad = y.Key.Cantidad,
                        Enviados = y.Key.Enviados,
                        AlumnoConfigurado = y.Key.AlumnoConfigurado
                    }).ToList(),
                }).FirstOrDefault();

                return resultadoAgrupado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Joseph Llanque
        /// Fecha: 12/07/2024
        /// Version: 1.0
        /// <summary>
        /// metodo url post utf8
        /// </summary>
        /// <returns>bool</returns>
        private async Task<RespuestaMensajeHookDTO> UrlPostAsync(string UrlBase, string jsonStringResult)
        {
            RespuestaMensajeHookDTO respuestaMensajeHook = new RespuestaMensajeHookDTO();
            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                using (HttpClient client = new HttpClient(handler))
                {

                    var content = new StringContent(jsonStringResult, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(UrlBase, content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    respuestaMensajeHook = JsonConvert.DeserializeObject<RespuestaMensajeHookDTO>(responseBody)!;
                }
                return respuestaMensajeHook;
            }
            catch (Exception ex)
            {
                return respuestaMensajeHook;
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 29/08/2025
        /// Version: 1.0
        /// <summary>
        /// Devuelve el ultimo mensaje de campania que se envio a un alumno
        /// </summary>
        /// <param name="celularAlumno">Celular del alumno</param>
        /// <returns>Ultimo mensaje de campania enviado</returns>
        public string ObtenerUltimoMensajeCampaniaEnviado(string celularAlumno)
        {
            try
            {
                var response = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerUltimoMensajeCampaniaEnviado(celularAlumno);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string ObtenerNumeroIdentificadorWhatsAppPorIdPersonal(int IdPersonal)
        {
            try
            {
                var modelo = _unitOfWork.CampaniaGeneralWhatsAppRepository.ObtenerNumeroIdentificadorWhatsAppPorIdPersonal(IdPersonal);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Humberto Oscata
        /// Fecha: 07/03/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el contenido del mensaje más reciente generado por la IA para un alumno específico,
        /// sin necesidad de un identificador de llamada. Se usa en el reemplazo de la etiqueta {tRemarketingMensajeGenerado.Contenido}.
        /// </summary>
        /// <param name="idAlumno">ID del alumno</param>
        /// <returns>Contenido del mensaje más reciente o cadena vacía si no existe</returns>
        private async Task<string> ObtenerContenidoMensajeRemarketingPorAlumno(int idAlumno)
        {
            try
            {
                //string url = $"http://ia-remarketing-api.bsginstitute.com/testing/api/generacion_mensaje/consulta_mensaje_alumno?id_alumno={idAlumno}&con_argumentos=false";
                string url = $"http://ia-remarketing-api.bsginstitute.com/api/generacion_mensaje/consulta_mensaje_alumno?id_alumno={idAlumno}&con_argumentos=false";


                using (var client = new HttpClient())
                {
                    var response = await client.PostAsync(url, null);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                        return string.Empty;

                    var resultado = JsonConvert.DeserializeObject<List<MensajeGeneradoIA>>(responseContent);

                    if (resultado == null || !resultado.Any())
                        return string.Empty;

                    return resultado[0].contenido ?? string.Empty;
                }
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}





















