using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using TheArtOfDev.HtmlRenderer.Core.Entities;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Servicio: AdwordsService
    /// Autor: Adriana Chipana.
    /// Fecha: 14/02/2022
    /// <summary>
    /// </summary>
    public class AdwordsService : IAdwordsService
    {
        private IUnitOfWork _unitOfWork;

        public AdwordsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IdDTO ProcesarGoogleLeads(GoogleFormularioLeadgenDTO leads)
        {
            try
            {
                InsertarAsignacionAutomaticaTempDTO asignacionAutomaticaTemp = new InsertarAsignacionAutomaticaTempDTO();

                // Asignación de valores básicos
                asignacionAutomaticaTemp.Nombres = leads.Nombre;
                asignacionAutomaticaTemp.Apellidos = leads.Apellidos;
                asignacionAutomaticaTemp.Movil = leads.Celular;
                asignacionAutomaticaTemp.Correo = leads.Email;

                var campania = _unitOfWork.AdwordsRepository.ObtenerDatosCampaniaAdwords(leads.CampaniaGoogle);
                var centroc = _unitOfWork.CentroCostoRepository.ObtenerPorId(campania.IdCentroCosto);

                asignacionAutomaticaTemp.IdCentroCosto = campania.IdCentroCosto;
                asignacionAutomaticaTemp.IdTipoDato = 8;

                // Diccionario para mapear países y ciudades
                var paisCiudadMap = new Dictionary<string, Tuple<int, int>>
        {
            { "perú", Tuple.Create(51, 14) },
            { "peru", Tuple.Create(51, 14) },
            { "arequipa", Tuple.Create(51, 4) },
            { "colombia", Tuple.Create(57, 1956) },
            { "chile", Tuple.Create(56, 1942) },
            { "mexico", Tuple.Create(52, 25) },
            { "bolivia", Tuple.Create(591, 2061) },
            { "estados unidos", Tuple.Create(0, 2370) },
            { "ecuador", Tuple.Create(593, 2093) },
            { "venezuela", Tuple.Create(58, 2371) },
        };

                string paisLimpiado = leads.Pais.Trim().ToLower();
                if (paisCiudadMap.ContainsKey(paisLimpiado))
                {
                    var ciudadTuple = paisCiudadMap[paisLimpiado];
                    asignacionAutomaticaTemp.IdPais = ciudadTuple.Item1;
                    asignacionAutomaticaTemp.IdCiudad = ciudadTuple.Item2;
                }

                if (campania.EsRemarketing == true)
                {
                    asignacionAutomaticaTemp.Origen = "Adwords Busqueda Remarketing Formulario Propio";
                    asignacionAutomaticaTemp.IdCategoriaDato = 554;
                }
                else
                {
                    asignacionAutomaticaTemp.Origen = "Adwords Busqueda Formulario Propio";
                    asignacionAutomaticaTemp.IdCategoriaDato = 104;


                }

                if (leads.Industria != null)
                {
                    string industriaLimpio = leads.Industria.Trim().ToLower();

                    if (industriaLimpio == "otro" || industriaLimpio == "otros")
                    {
                        leads.Industria = "Otra";
                    }
                }

                var cargo = _unitOfWork.CargoRepository.FirstBy(x => x.Nombre.Contains(leads.Cargo), s => new { s.Id });
                var areatrabajo = _unitOfWork.AreaTrabajoRepository.FirstBy(x => x.Nombre.Contains(leads.AreaTrabajo), s => new { s.Id });
                var industria = _unitOfWork.IndustriaRepository.FirstBy(x => x.Nombre.Contains(leads.Industria), s => new { s.Id });
                var areaFormacion = _unitOfWork.AreaFormacionRepository.FirstBy(x => x.Nombre.Contains(leads.AreaFormacion), s => new { s.Id });

                if (cargo != null)
                {
                    asignacionAutomaticaTemp.IdCargo = cargo.Id;
                }
                if (areatrabajo != null)
                {
                    asignacionAutomaticaTemp.IdAreaTrabajo = areatrabajo.Id;
                }
                if (industria != null)
                {
                    asignacionAutomaticaTemp.IdIndustria = industria.Id;
                }

                if (areaFormacion != null)
                {
                    asignacionAutomaticaTemp.IdAreaFormacion = areaFormacion.Id;
                }

                asignacionAutomaticaTemp.IdFaseOportunidad = 2;
                asignacionAutomaticaTemp.Procesado = false;
                asignacionAutomaticaTemp.FechaRegistroCampania = DateTime.Now;
                asignacionAutomaticaTemp.IdTipoInteraccion = 15;
                asignacionAutomaticaTemp.AptoProcesamiento = true;
                asignacionAutomaticaTemp.Estado = true;
                asignacionAutomaticaTemp.UsuarioCreacion = "WebHookGoogleLeads";
                asignacionAutomaticaTemp.UsuarioModificacion = "WebHookGoogleLeads";
                asignacionAutomaticaTemp.FechaCreacion = DateTime.Now;
                asignacionAutomaticaTemp.FechaModificacion = DateTime.Now;

                var respuesta = _unitOfWork.AsignacionAutomaticaTempRepository.InsertarAsignacionAutomatica(asignacionAutomaticaTemp);

                return respuesta;
            }
            catch (Exception ex)
            {
                // Manejar la excepción adecuadamente (registrar, lanzar otra excepción, etc.)
                throw new Exception("Error al procesar los Google Leads", ex);
            }
        }


        public bool ValidarFormulario(int idAsignacionAutomaticaTemp)
        {

            try
            {
                return true;
            }
            catch
            {
                return false;

            }
        }


        public bool CrearOportunidadWebhookAdwords(string idAsignacionAutomatica)
        {
            try
            {

                var id = int.Parse(idAsignacionAutomatica);
                OportunidadService oportunidad = new OportunidadService(_unitOfWork);
                var repAsignacionA = _unitOfWork.AsignacionAutomaticaRepository;
                var repLog = _unitOfWork.LogRepository;
                var rpta = oportunidad.CrearOportunidadPortalWeb(id);
                var IdOportunidad = repAsignacionA.FirstById(id).IdOportunidad;

                if (IdOportunidad != null)
                {
                    int nroIntentos = 0;
                    bool flagValidado = false;  

                    while (!flagValidado && nroIntentos < 10)
                    {
                        try
                        {
                            var respuesta = oportunidad.ValidarCasosOportunidad(IdOportunidad ?? 0, id, true);

                            flagValidado = true;
                        }
                        catch (Exception ex)
                        {
                            nroIntentos++;

                            Thread.Sleep(3000);
                        }
                    }

                    if (nroIntentos == 10)
                    {
                        repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "CrearOportunidadWebHookAdwords", Parametros = $"IdAsignacionAutomatica={idAsignacionAutomatica}", Mensaje = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Excepcion = $"Excedio el numero de intentos de validacion ({nroIntentos} intentos) posterior a la creacion", Tipo = "VALIDATE", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });
                    }

                    return true;
                }

                return true;
            }
            catch
            {
                return false;

            }
        }


        public List<CampaniaAdwordsTodoDTO> ObtenerTodoCampaniaAdwords()
        {
            try
            {
                var respuesta = _unitOfWork.AdwordsRepository.ObtenerTodoCampaniaAdwords();
                return respuesta;
            }
            catch
            {
                return null;

            }
        }

        

        public CampaniaAdwordsTodoDTO ObtenerCampaniaAdwords(int id)
        {
            try
            {
                var respuesta = _unitOfWork.AdwordsRepository.ObtenerCampaniaAdwordsPorId(id);
                return respuesta;
            }
            catch
            {
                return null;

            }
        }

        public bool InsertarCampaniaAdwords(CampaniaAdwordsDTO datos)
        {
            try
            {
                var respuesta = _unitOfWork.AdwordsRepository.InsertarCampaniaAdwords(datos);
                return respuesta;
            }
            catch
            {
                return false;

            }
        }

        public bool ActualizarCampaniaAdwords(ActualzarCampaniaAdwordsDTO datos)
        {
            try
            {
                var respuesta = _unitOfWork.AdwordsRepository.ActualizarCampaniaAdwords(datos);
                return respuesta;
            }
            catch
            {
                return false;

            }
        }

        public bool EliminarCampaniaAdwords(int id)
        {
            try
            {
                var respuesta = _unitOfWork.AdwordsRepository.EliminarCampaniaAdwords(id);
                return respuesta;
            }
            catch
            {
                return false;

            }
        }
    }
}
