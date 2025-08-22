using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.Sendingblue;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueContactosDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueRespuestaGenericaDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendinblueCamapaniaDTO;
using static BSI.Integra.Persistencia.Entidades.IntegraDB.Sendingblue.IntegracionConIntegraDB.T_SendingblueListaDTO;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.Sendingblue
{
    public class SendinblueReporteService : ISendinblueReporteService
    {
        private readonly IUnitOfWork unitOfWork;
        private Mapper _mapper;
        private List<SendinBlueEventoWebHookDTO> DatadeEvento = new List<SendinBlueEventoWebHookDTO>();

        public SendinblueReporteService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSendinBlueDataDeEvento, SendinBlueDataDeEvento>(MemberList.None).ReverseMap();
                cfg.CreateMap<SendinBlueDataDeEvento, SendinBlueDataDeEventoDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<TSendinBlueEventoWebHook, SendinBlueEventoWebHook>(MemberList.None).ReverseMap();
                cfg.CreateMap<SendinBlueEventoWebHook, SendinBlueEventoWebHookDTO>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// <summary>
        /// Autor: Rodrigo Montesinos
        /// Fecha de creacion; 03/31/2023
        /// Descripcion: etsa funcion obtiene todos los tipos de evento que ingresan por web hook
        /// </summary>
        /// <returns>Una lista de eventos</returns>
        public List<SendinBlueEventoWebHookDTO> ObtenerTipoDeDatos()
        {
            try
            {
                if (DatadeEvento.Count == 0)
                {
                    DatadeEvento = unitOfWork.SendinBlueEventoWebHookRepository.ObtenerTodaLaData();
                    return DatadeEvento;
                }
                return DatadeEvento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Autor: Rodrigo Montesinos
        /// Fecha de creacion; 03/31/2023
        /// Descripcion: Agrega los datos obtenidos por emdio del webhook
        /// </summary>
        /// <param name="sendinblueObject">diccionario de elmentos que comprenden el dto</param>
        public void AgregarDatosDeWebhook(Dictionary<string, object> sendinblueObject)
        {
            try
            {
                var TipoDeDatos = ObtenerTipoDeDatos();
                var tipoaConsultar = GetPropValue(sendinblueObject, "event") == null ? "" : Convert.ToString(GetPropValue(sendinblueObject, "event"));

                var datoalamacenar = TipoDeDatos.Where(x => x.Tipo == tipoaConsultar.ToLower()).FirstOrDefault();
                SendinBlueDataDeEvento snd = new SendinBlueDataDeEvento()
                {
                    EmailContacto = GetPropValue(sendinblueObject, "email") == null ? "" : GetPropValue(sendinblueObject, "email").ToString(),
                    FechaDeEvento = GetPropValue(sendinblueObject, "date_event") == null ? DateTime.Now : Convert.ToDateTime(GetPropValue(sendinblueObject, "date_event")),
                    FechaEnvio = GetPropValue(sendinblueObject, "date_sent") == null ? DateTime.Now : Convert.ToDateTime(GetPropValue(sendinblueObject, "date_sent")),
                    IdSendinBlueCampania = GetPropValue(sendinblueObject, "camp_id") == null ? 0 : Convert.ToInt32(GetPropValue(sendinblueObject, "camp_id")),
                    JsonResponse = JsonConvert.SerializeObject(sendinblueObject),
                    UrlEvento = GetPropValue(sendinblueObject, "URL") == null ? "" : Convert.ToString(GetPropValue(sendinblueObject, "URL")),
                    IdSendinBlueEventoWebHook = datoalamacenar == null ? 1 : Convert.ToInt32(datoalamacenar.Id),
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Estado = true,
                    UsuarioCreacion = "webhook",
                    UsuarioModificacion = "webhook",
                };
                unitOfWork.SendinBlueDataDeEventoRepository.Add(snd);
                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Autor: Rodrigo Montesinos
        /// Fecha de creacion; 03/31/2023
        /// Descripcion: esta funcion se encarga de obtener valores de un objeto no casteado como dto esto con la funncion de pder recibir cualquier tipo de informacion de sendinblue
        /// </summary>
        /// <returns>Un objeto o un null</returns>
        public static object? GetPropValue(Dictionary<string, object> src, string propName)
        {
            object value;
            bool hasValue = src.TryGetValue(propName, out value);
            if (hasValue)
            {
                return value;
            }
            return null;
        }

        /// <summary>
        /// Autor: Rodrigo Montesinos
        /// Fecha de creacion; 03/31/2023
        /// Descripcion: esta funcion realiza la busqeda de datos pro un intervalo de tiempo la cual es necesaria para ls sreportes
        /// </summary>
        /// <returns>Una lista de datos necesarios apra el reporte </returns>

        public List<SendinblueReporteMarketingDTO> ObtenerReporteDeSendinBlue(SendinblueReporteParametrosDTO ParametrosBusqueda)
        {
            try
            {
                List<SendinblueReporteMarketingDTO> resultado = new List<SendinblueReporteMarketingDTO>();
                var datosRecuperados = unitOfWork.SendinBlueDataDeEventoRepository.ObtenerReproteSendinBlue(ParametrosBusqueda);

                resultado = datosRecuperados.GroupBy(x => new { x.Correo })
                .Select(x => new SendinblueReporteMarketingDTO
                {
                    IdAlumno = x.GroupBy(x => new { x.IdAlumno }).Select(x => x.Key.IdAlumno).First() == null ? 0 : x.GroupBy(x => new { x.IdAlumno }).Select(x => x.Key.IdAlumno).First(),
                    CantidadDeClick = x.GroupBy(x => new { x.IdAlumno, x.CantidadDeClick }).Sum(x => x.Key.CantidadDeClick),
                    Correo = x.GroupBy(x => new { x.IdAlumno }).Select(g => g.Select(x => x.Correo).FirstOrDefault()).FirstOrDefault(),
                    FechaApertura = x.Select(x => x.FechaApertura).FirstOrDefault(),
                    PrimeraApertura = x.GroupBy(x => new { x.IdAlumno, x.PrimeraApertura }).Sum(x => x.Key.PrimeraApertura) 
                }).ToList();
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}
