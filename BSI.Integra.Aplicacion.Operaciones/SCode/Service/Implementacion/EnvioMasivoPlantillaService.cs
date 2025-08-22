using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Operaciones.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Mandrill.Models;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Implementacion
{
    /// Service: EnvioMasivoPlantillaService
    /// Autor: Jonathan Caipo
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión general de Envío de masivo de plantillas
    /// </summary>
    public class EnvioMasivoPlantillaService : IEnvioMasivoPlantillaService
    {
        private IUnitOfWork _unitOfWork;

        public EnvioMasivoPlantillaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los archivos adjuntos por plantilla
        /// </summary>
        /// <param name="plantilla">Plantilla a la cual se va a analizar</param>
        /// <returns>Lista de objetos (EmailAttachment)</returns>
        public List<EmailAttachment> ObtenerArchivosAdjuntos(string plantilla)
        {
            try
            {
                var listaArchivosAdjunto = new List<string>()
                {
                    "{ArchivoAdjunto.ManualIngresoAulaVirtual}",
                    "{ArchivoAdjunto.ManualBSPlay}",
                    "{ArchivoAdjunto.ManualConectarseSesionWebinar}",
                    "{ArchivoAdjunto.ManualConectarseSesionVirtual}"
                };

                var listaArchivosAdjuntos = new List<EmailAttachment>();

                if (listaArchivosAdjunto.Any(plantilla.Contains))
                {
                    if (plantilla.Contains("{ArchivoAdjunto.ManualIngresoAulaVirtual}"))
                    {
                        listaArchivosAdjuntos.Add(new EmailAttachment()
                        {
                            Base64 = true,
                            Content = Convert.ToBase64String(ExtendedWebClient.GetFile(ValorEstaticoUtil.Get("{ArchivoAdjunto.ManualIngresoAulaVirtual}"))),
                            Name = "Manual para ingreso al Aula Virtual.pdf",
                            Type = "application/pdf"
                        });
                        plantilla = plantilla.Replace("{ArchivoAdjunto.ManualIngresoAulaVirtual}", "");
                    }

                    if (plantilla.Contains("{ArchivoAdjunto.ManualBSPlay}"))
                    {
                        listaArchivosAdjuntos.Add(new EmailAttachment()
                        {
                            Base64 = true,
                            Content = Convert.ToBase64String(ExtendedWebClient.GetFile(ValorEstaticoUtil.Get("{ArchivoAdjunto.ManualBSPlay}"))),
                            Name = "Manual BS Play.pdf",
                            Type = "application/pdf"
                        });
                        plantilla = plantilla.Replace("{ArchivoAdjunto.ManualBSPlay}", "");
                    }

                    if (plantilla.Contains("{ArchivoAdjunto.ManualConectarseSesionWebinar}"))
                    {
                        listaArchivosAdjuntos.Add(new EmailAttachment()
                        {
                            Base64 = true,
                            Content = Convert.ToBase64String(ExtendedWebClient.GetFile(ValorEstaticoUtil.Get("{ArchivoAdjunto.ManualConectarseSesionWebinar}"))),
                            Name = "Manual para conectarse a la sesión webinar.pdf",
                            Type = "application/pdf"
                        });
                        plantilla = plantilla.Replace("{ArchivoAdjunto.ManualConectarseSesionWebinar}", "");
                    }

                    if (plantilla.Contains("{ArchivoAdjunto.ManualConectarseSesionVirtual}"))
                    {
                        listaArchivosAdjuntos.Add(new EmailAttachment()
                        {
                            Base64 = true,
                            Content = Convert.ToBase64String(ExtendedWebClient.GetFile(ValorEstaticoUtil.Get("{ArchivoAdjunto.ManualConectarseSesionVirtual}"))),
                            Name = "Manual para conectarse a la sesión virtual.pdf",
                            Type = "application/pdf"
                        });
                        plantilla = plantilla.Replace("{ArchivoAdjunto.ManualConectarseSesionVirtual}", "");
                    }
                }
                return listaArchivosAdjuntos;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los archivos adjuntos por plantilla
        /// </summary>
        /// <param name="plantilla"></param>
        /// <returns></returns>
        public string QuitarEtiquetasArchivosAdjuntos(string plantilla)
        {
            try
            {
                var listaArchivosAdjunto = new List<string>()
                {
                    "{ArchivoAdjunto.ManualIngresoAulaVirtual}",
                    "{ArchivoAdjunto.ManualBSPlay}",
                    "{ArchivoAdjunto.ManualConectarseSesionWebinar}",
                    "{ArchivoAdjunto.ManualConectarseSesionVirtual}"
                };

                var listaArchivosAdjuntos = new List<EmailAttachment>();

                if (listaArchivosAdjunto.Any(plantilla.Contains))
                {
                    if (plantilla.Contains("{ArchivoAdjunto.ManualIngresoAulaVirtual}"))
                    {
                        plantilla = plantilla.Replace("{ArchivoAdjunto.ManualIngresoAulaVirtual}", "");
                    }

                    if (plantilla.Contains("{ArchivoAdjunto.ManualBSPlay}"))
                    {
                        plantilla = plantilla.Replace("{ArchivoAdjunto.ManualBSPlay}", "");
                    }

                    if (plantilla.Contains("{ArchivoAdjunto.ManualConectarseSesionWebinar}"))
                    {
                        plantilla = plantilla.Replace("{ArchivoAdjunto.ManualConectarseSesionWebinar}", "");
                    }

                    if (plantilla.Contains("{ArchivoAdjunto.ManualConectarseSesionVirtual}"))
                    {
                        plantilla = plantilla.Replace("{ArchivoAdjunto.ManualConectarseSesionVirtual}", "");
                    }
                }
                return plantilla;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
