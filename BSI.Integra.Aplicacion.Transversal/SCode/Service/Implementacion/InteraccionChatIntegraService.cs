using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: InteraccionChatIntegraService
    /// Autor: Margiory Ramirez Neyra.
    /// Version: 1.0
    /// Fecha: 30/09/2022
    /// <summary>
    /// Gestión general de T_InteraccionChatIntegra
    /// </summary>
    public class InteraccionChatIntegraService : IInteraccionChatIntegraService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public InteraccionChatIntegraService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TInteraccionChatIntegra, InteraccionChatIntegra>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public InteraccionChatIntegra Add(InteraccionChatIntegra entidad)
        {
            try
            {
                var modelo = _unitOfWork.InteraccionChatIntegraRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<InteraccionChatIntegra>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public InteraccionChatIntegra Update(InteraccionChatIntegra entidad)
        {
            try
            {
                var modelo = _unitOfWork.InteraccionChatIntegraRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<InteraccionChatIntegra>(modelo);
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
                _unitOfWork.InteraccionChatIntegraRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<InteraccionChatIntegra> Add(List<InteraccionChatIntegra> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.InteraccionChatIntegraRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<InteraccionChatIntegra>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<InteraccionChatIntegra> Update(List<InteraccionChatIntegra> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.InteraccionChatIntegraRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<InteraccionChatIntegra>>(modelo);
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
                _unitOfWork.InteraccionChatIntegraRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 30/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene  el Reporte para  ChaLog, con los filtros realizados
        /// </summary>
        /// <param name="chat">/param>
        /// <returns> List<ReporteChatLogDTO>> </returns>

        public IEnumerable<ReporteChatLogDTO> GenerarReporteChatLog(ChatReporteDTO chat)
        {
            try
            {
                return _unitOfWork.InteraccionChatIntegraRepository.GenerarReporteChatLog(chat);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 30/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene  el Reporte para  Chat, con los filtros realizados
        /// </summary>
        /// /// <param name="data">/param>
        /// <returns> List<ChatAgrupadoDTO>> </returns

        public IEnumerable<ChatAgrupadoDTO> GenerarReporteChat(ChatReporteDTO data)
        {

            try
            {
                var chat = _unitOfWork.InteraccionChatIntegraRepository.GenerarReporteChat(data).OrderByDescending(x => x.Fecha).ToList();
                //var sinChat = _repInteraccionChatIntegra.GenerarReporteChatSinChat(data);

                //var agrupado = (from p in chat
                //				group p by new { p.Fecha, p.Pais } into grupo
                //				select new { g = grupo.Key, l = grupo }).ToList();

                IEnumerable<ChatAgrupadoDTO> agrupado = null;
                if (data.Desglose == 1)
                {
                    agrupado = chat.GroupBy(x => x.Pais)
                    .Select(g => new ChatAgrupadoDTO
                    {
                        Pais = g.Key,
                        Detalle = g.GroupBy(x => x.Fecha).Select(x => new ChatIntegraDetalleDTO
                        {
                            Fecha = x.Key.ToString("yyyyMMdd"),
                            Detalle = x.Select(y => new ChatIntegraSubDetalleDTO
                            {
                                Area = y.Area,
                                Asesor = y.Asesor,
                                Chats = y.Chats,
                                ClickEmpezar = y.ClickEmpezar,
                                Logueados = y.Logueados,
                                PalabrasVisitante = y.PalabrasVisitante,
                                Oportunidades = y.Oportunidades,
                                Promedio = y.Promedio,
                                Atendidos = y.Atendidos,
                                ClienteTiempoEspera = y.ClienteTiempoEspera
                                //NoAtendidos = y.NoAtendidos

                            }).ToList()
                        }).ToList()
                    });
                }

                if (data.Desglose == 2)
                {
                    agrupado = chat.GroupBy(x => x.Pais)
                    .Select(g => new ChatAgrupadoDTO
                    {

                        Pais = g.Key,
                        Detalle = g.GroupBy(x => _unitOfWork.InteraccionChatIntegraRepository.ObtenerNumeroSemana(x.Fecha)).Select(x => new ChatIntegraDetalleDTO
                        {
                            Fecha = "Semana_" + x.Key,
                            Detalle = x.Select(y => new ChatIntegraSubDetalleDTO
                            {
                                Area = y.Area,
                                Asesor = y.Asesor,
                                Chats = y.Chats,
                                ClickEmpezar = y.ClickEmpezar,
                                Logueados = y.Logueados,
                                PalabrasVisitante = y.PalabrasVisitante,
                                Oportunidades = y.Oportunidades,
                                Promedio = y.Promedio,
                                Atendidos = y.Atendidos,
                                ClienteTiempoEspera = y.ClienteTiempoEspera
                                //NoAtendidos = y.NoAtendidos

                            }).ToList()
                        }).ToList()
                    });
                }

                if (data.Desglose == 3)
                {
                    agrupado = chat.GroupBy(x => x.Pais)
                    .Select(g => new ChatAgrupadoDTO
                    {
                        Pais = g.Key,
                        Detalle = g.GroupBy(x => x.Fecha.Month).Select(x => new ChatIntegraDetalleDTO
                        {
                            Fecha = _unitOfWork.InteraccionChatIntegraRepository.ObtenerNombreMes(x.Key),
                            Detalle = x.Select(y => new ChatIntegraSubDetalleDTO
                            {
                                Area = y.Area,
                                Asesor = y.Asesor,
                                Chats = y.Chats,
                                ClickEmpezar = y.ClickEmpezar,
                                Logueados = y.Logueados,
                                PalabrasVisitante = y.PalabrasVisitante,
                                Oportunidades = y.Oportunidades,
                                Promedio = y.Promedio,
                                Atendidos = y.Atendidos,
                                ClienteTiempoEspera = y.ClienteTiempoEspera
                                //NoAtendidos = y.NoAtendidos

                            }).ToList()
                        }).ToList()
                    });
                }


                //var agrupadosinChat = (from p in sinChat
                //					   group p by p.Fecha into grupo
                //					   select new { g = grupo.Key, l = grupo }).ToList();

                return agrupado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 30/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene  Mes de T_TInteraccionChatIntegra para el llenado de datos.
        /// </summary>

        /// <returns> List<ReporteChatIntegraDTO>> </returns
        public string ObtenerNombreMes(int mes)
        {
            try
            {
                return _unitOfWork.InteraccionChatIntegraRepository.ObtenerNombreMes(mes);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 30/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene umero de  Semana de T_TInteraccionChatIntegra para el llenado de datos.
        /// </summary>
        /// /// <param name="date">/param>
        /// <returns> List<ReporteChatIntegraDTO>> </returns
        public int ObtenerNumeroSemana(DateTime date)
        {
            try
            {
                return _unitOfWork.InteraccionChatIntegraRepository.ObtenerNumeroSemana(date);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 30/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene  registro de Reporte chat  de T_TInteraccionChatIntegra.
        /// </summary>
        /// <param name="chat">/param>
        /// <returns> List<ReporteChatIntegraDTO>> </returns

        IEnumerable<ReporteChatIntegraDTO> IInteraccionChatIntegraService.GenerarReporteChat(ChatReporteDTO chat)
        {
            try
            {
                return _unitOfWork.InteraccionChatIntegraRepository.GenerarReporteChat(chat);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 05/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los atributos de T_InteraccionChatIntegra asociados a un Id.
        /// </summary>
        /// <param name="id">Id de la Actividad Detalle</param>
        /// <returns> InteraccionChatIntegra </returns>
        public InteraccionChatIntegra ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.InteraccionChatIntegraRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    } 
}
