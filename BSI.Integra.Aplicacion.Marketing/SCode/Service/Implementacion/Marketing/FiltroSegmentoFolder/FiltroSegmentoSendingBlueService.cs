using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB; 
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.FiltroSegmentoTipoContacto;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Sendingblue; 
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.FiltroSegmentoFolder;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueCarpetaDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendingblueListaDTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroSegmentoFolder;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.FiltroSegmentoFolder;
using System.Text.RegularExpressions;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.FiltroSegmentoFolder
{
    public class FiltroSegmentoSendingBlueService : IFiltroSegmentoSendingBlueService
    {
        private readonly IUnitOfWork unitOfWork;
        string apikey = "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";
        string urlSendinBlue = "https://api.sendinblue.com/v3/";
        public FiltroSegmentoSendingBlueService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para obtener listas de filtro segmento 
        /// </summary>
        /// <param name="id">Identificador unico</param>
        /// <param name="idFiltroSegmentoTipoContacto">identificador de filtro segmento tipo contacto</param>
        /// <returns>Lista de filtro segmento compuesto</returns>
        public List<FiltroSegmentoCompuestoDTO> ObtenerResultadoFiltroSegmento(int id, int idFiltroSegmentoTipoContacto)
        {
            var alumnoService = new AlumnoService(unitOfWork);
            var resultado = unitOfWork.filtroSegmentoSendinBlueRepository.ObtenerResultadoFiltroSegmento(id, idFiltroSegmentoTipoContacto);
            foreach (var item in resultado)
            {
                if (!string.IsNullOrWhiteSpace(item.Email1))
                    item.Email1Encriptado = alumnoService.EncriptarCorreoHash(item.Email1);

                if (!string.IsNullOrWhiteSpace(item.Celular))
                    item.CelularEncriptado = alumnoService.EncriptarNumeroHash(Regex.Replace(item.Celular, @"[^\d]", ""));
            }

            return resultado;
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Realiza una peticion para obtener la lista de filtro segmento
        /// </summary>
        /// <returns>Retorna lista de filtros</returns>
        public List<FiltroSegmentoPanelDTO> ObtenerFiltroSegmentoPanel()
        {
            return unitOfWork.filtroSegmentoSendinBlueRepository.ObtenerFiltroSegmentoPanel();
        }


        public void Ejecutarrecursivo()
        {
            var action = true;
            var carpetas = unitOfWork.sendinblueListaRepository.ObtenerTodaslasLista();

            do
            {
                var limit = 50;
                var offset = 0;

                var res = ServicioCompletoDeConsultaGetURL("contacts/lists?limit=" + limit + "&offset=" + offset + "&sort=desc");
                GetLists folder = JsonConvert.DeserializeObject<GetLists>(res.SendingblueRespuesta);
                var folders = JsonConvert.DeserializeObject<List<GetExtendedList>>(JsonConvert.SerializeObject(folder.Lists));
                List<CrearSendingblueListaDTO> cpl = new List<CrearSendingblueListaDTO>();
                foreach (var i in folders)
                {
                    var exist = carpetas.FirstOrDefault(x => x.IdSendinblueLista == i.Id);
                    if (exist == null)
                    {
                        CrearSendingblueListaDTO cp = new CrearSendingblueListaDTO()
                        {
                            IdSendinblueLista = Convert.ToInt32(i.Id),
                            Estado = true,
                            EstadoGuardado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            TotalExcluido = Convert.ToInt32(i.TotalBlacklisted),
                            Nombre = i.Name,
                            Respuesta = JsonConvert.SerializeObject(i),
                            UnicoSuscrito = Convert.ToInt32(i.UniqueSubscribers),
                            UsuarioCreacion = "ReplicaService",
                            UsuarioModificacion = "ReplicaService",
                            TotalSuscrito = Convert.ToInt32(i.TotalSubscribers),
                            IdSendinblueCarpeta = Convert.ToInt32(i.FolderId),
                        };
                        cpl.Add(cp);
                    }
                }
                if (cpl != null)
                {
                    if (cpl.Count() > 0)
                    {
                        unitOfWork.sendinblueListaRepository.Add(cpl);
                        unitOfWork.Commit();
                    }
                }
                offset += limit;
                limit += limit;
                if (folder.Count < limit)
                {
                    action = false;
                }
            } while (action);

        }

        public RespuestaGenerica ServicioCompletoDeConsultaGetURL(string url)
        {
            RespuestaGenerica respuesta = new RespuestaGenerica();
            respuesta.error = new ErrorGenerico();
            try
            {
                WebClient comunicacion = new WebClient();
                comunicacion.Headers[HttpRequestHeader.ContentType] = "application/json";
                comunicacion.Headers["api-key"] = apikey;
                String result = "";
                if (url != string.Empty)
                {
                    result = comunicacion.DownloadString(urlSendinBlue + url);
                }
                respuesta.SendingblueRespuesta = result;
                respuesta.error.Response = false;
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.error.Response = true;
                respuesta.error.Detalle = new DetailError { Codigo = "SB-SCURL-Ex00001-N1", Descripcion = "Este error fue generado en la consulta por url," + ex.Message, Mensaje = "Error en consulta http" };
                return respuesta;
            }
        }


       
    }
}
