using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.EsquemaRespuestas;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.EsquemaRespuestas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.EsquemaRespuestas
{
    /// Autor: Miguel Valdivia
    /// Fecha: 18/03/2026
    /// Version: 1.0
    /// <summary>
    /// Repositorio para la gestion de actividades Bot IA y sus numeros asociados.
    /// </summary>
    public class ChatbotActividadBotIARepository : IChatbotActividadBotIARepository
    {
        private readonly IDapperRepository _dapperRepository;

        public ChatbotActividadBotIARepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        public List<ChatbotActividadBotIAListadoDTO> ObtenerListadoChatbotActividadBotIA()
        {
            try
            {
                var sp         = "[ia].[SP_ChatbotActividadObtener]";
                var jsonResult = _dapperRepository.QuerySPDapper(sp, null);

                if (string.IsNullOrEmpty(jsonResult) || jsonResult == "null")
                    return new List<ChatbotActividadBotIAListadoDTO>();

                return JsonConvert.DeserializeObject<List<ChatbotActividadBotIAListadoDTO>>(jsonResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ChatbotActividadBotIANumeroDTO> ObtenerNumerosPorActividades()
        {
            try
            {
                var query =
                    @"SELECT
                          CABN.IdChatbotActividad,
                          AMWA.NumeroWhatsApp
                      FROM
                          [ia].[T_ChatbotActividadAsignacion]                      CABN
                          INNER JOIN [ia].[T_AsistenteMarketingWhatsAppAsignacion] AMWA
                              ON AMWA.Id = CABN.IdAsistenteMarketingWhatsAppAsignacion
                      WHERE
                          CABN.Estado = 1";

                var jsonResult = _dapperRepository.QueryDapper(query, null);

                if (string.IsNullOrEmpty(jsonResult) || jsonResult == "null")
                    return new List<ChatbotActividadBotIANumeroDTO>();

                return JsonConvert.DeserializeObject<List<ChatbotActividadBotIANumeroDTO>>(jsonResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int InsertarChatbotActividadBotIA(InsertarChatbotActividadBotIADTO request, string usuario)
        {
            try
            {
                var sp         = "[ia].[SP_TChatbotActividad_Insertar]";
                var jsonResult = _dapperRepository.QuerySPDapper(sp, new
                {
                    Nombre              = request.Nombre.Trim(),
                    IdChatbotEsquema    = request.IdChatbotEsquema,
                    IdMedioComunicacion = request.IdMedioComunicacion,
                    Estado              = request.Estado,
                    UsuarioCreacion     = usuario
                });

                var resultado = JsonConvert.DeserializeObject<dynamic>(jsonResult);
                return (int)resultado[0].IdChatbotActividad;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InsertarChatbotActividadBotIANumero(int idChatbotActividadBotIA, int idAsistenteMarketingWhatsAppAsignacion, bool estado, string usuario)
        {
            try
            {
                var sp = "[ia].[SP_TChatbotActividadAsignacion_Insertar]";
                _dapperRepository.QuerySPDapper(sp, new
                {
                    IdChatbotActividad                     = idChatbotActividadBotIA,
                    IdAsistenteMarketingWhatsAppAsignacion = idAsistenteMarketingWhatsAppAsignacion,
                    Estado                                 = estado,
                    UsuarioCreacion                        = usuario
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EliminarChatbotActividadBotIANumeros(int idChatbotActividadBotIA, string usuario)
        {
            try
            {
                var sp = "[ia].[SP_TChatbotActividadAsignacion_Eliminar]";
                _dapperRepository.QuerySPDapper(sp, new
                {
                    IdChatbotActividad  = idChatbotActividadBotIA,
                    UsuarioModificacion = usuario
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DesactivarNumerosWhatsAppDeActividad(int idChatbotActividadBotIA, string usuario)
        {
            try
            {
                var query =
                    @"UPDATE [ia].[T_AsistenteMarketingWhatsAppAsignacion]
                      SET
                          Estado              = 0,
                          UsuarioModificacion = @UsuarioModificacion,
                          FechaModificacion   = GETDATE()
                      WHERE Id IN (
                          SELECT IdAsistenteMarketingWhatsAppAsignacion
                          FROM   [ia].[T_ChatbotActividadAsignacion]
                          WHERE  IdChatbotActividad = @IdChatbotActividad
                            AND  Estado = 1
                      )";

                _dapperRepository.QueryDapper(query, new
                {
                    IdChatbotActividad  = idChatbotActividadBotIA,
                    UsuarioModificacion = usuario
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ActualizarChatbotActividadBotIA(ActualizarChatbotActividadBotIADTO request, string usuario)
        {
            try
            {
                var sp = "[ia].[SP_TChatbotActividad_Actualizar]";
                _dapperRepository.QuerySPDapper(sp, new
                {
                    IdChatbotActividad  = request.Id,
                    Nombre              = request.Nombre.Trim(),
                    IdChatbotEsquema    = request.IdChatbotEsquema,
                    IdMedioComunicacion = request.IdMedioComunicacion,
                    Estado              = request.Estado,
                    UsuarioModificacion = usuario
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void EliminarChatbotActividadBotIA(int idChatbotActividadBotIA, string usuario)
        {
            try
            {
                var sp = "[ia].[SP_TChatbotActividad_Eliminar]";
                _dapperRepository.QuerySPDapper(sp, new
                {
                    IdChatbotActividad  = idChatbotActividadBotIA,
                    UsuarioModificacion = usuario
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<MedioComunicacionDTO> ObtenerListadoMedioComunicacion()
        {
            try
            {
                var sp         = "[ia].[SP_MedioComunicacionObtenerListado]";
                var jsonResult = _dapperRepository.QuerySPDapper(sp, null);

                if (string.IsNullOrEmpty(jsonResult) || jsonResult == "null")
                    return new List<MedioComunicacionDTO>();

                return JsonConvert.DeserializeObject<List<MedioComunicacionDTO>>(jsonResult);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
