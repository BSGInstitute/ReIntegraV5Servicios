using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ConfiguracionDatoRemarketingService
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 06/10/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionDatoRemarketing
    /// </summary>
    public class ConfiguracionDatoRemarketingService : IConfiguracionDatoRemarketingService

    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ConfiguracionDatoRemarketingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TConfiguracionDatoRemarketing, ConfiguracionDatoRemarketing>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ConfiguracionDatoRemarketing Add(ConfiguracionDatoRemarketing entidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionDatoRemarketingRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionDatoRemarketing>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConfiguracionDatoRemarketing Update(ConfiguracionDatoRemarketing entidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionDatoRemarketingRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionDatoRemarketing>(modelo);
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
                _unitOfWork.ConfiguracionDatoRemarketingRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionDatoRemarketing> Add(List<ConfiguracionDatoRemarketing> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionDatoRemarketingRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionDatoRemarketing>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionDatoRemarketing> Update(List<ConfiguracionDatoRemarketing> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionDatoRemarketingRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionDatoRemarketing>>(modelo);
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
                _unitOfWork.ConfiguracionDatoRemarketingRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 06/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ConfiguracionDatoRemarketing para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ConfiguracionDatoRemarketingRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionDatoRemarketingAgrupadoGrillaDTO> ObtenerConfiguracionesDatoRemarketing()
        {
            try

            {
                var _repConfiguracionDatoRemarketing = _unitOfWork.ConfiguracionDatoRemarketingRepository;
                List<ConfiguracionDatoRemarketingAgrupadoGrillaDTO> resultadoAgrupado = new List<ConfiguracionDatoRemarketingAgrupadoGrillaDTO>();

                List<ConfiguracionDatoRemarketingGrillaDTO> listaAFormatear = _repConfiguracionDatoRemarketing.ObtenerConfiguracionesDatoRemarketing();

                resultadoAgrupado = listaAFormatear.GroupBy(g => new
                {
                    g.Id,
                    g.IdAgendaTab,
                    g.NombreAgendaTab,
                    g.FechaInicio,
                    g.FechaFin,
                    g.Vigente
                }).Select(s => new ConfiguracionDatoRemarketingAgrupadoGrillaDTO()
                {
                    Id = s.Key.Id,
                    IdAgendaTab = s.Key.IdAgendaTab,
                    NombreAgendaTab = s.Key.NombreAgendaTab,
                    FechaInicio = s.Key.FechaInicio,
                    FechaFin = s.Key.FechaFin,
                    Vigente = s.Key.Vigente,
                    ListaTipoDato = listaAFormatear.Select(ss => new { ss.Id, ss.IdTipoDato, ss.NombreTipoDato })
                                                    .Where(x => x.Id == s.Key.Id && x.IdTipoDato != null).Distinct()
                                                    .Select(sss => new ConfiguracionDatoRemarketingTipoDatoGrillaDTO() { IdTipoDato = sss.IdTipoDato.Value, NombreTipoDato = sss.NombreTipoDato })
                                                    .ToList(),
                    ListaTipoCategoriaOrigen = listaAFormatear
                                                    .Select(ss => new { ss.Id, ss.IdTipoCategoriaOrigen, ss.NombreTipoCategoriaOrigen })
                                                    .Where(x => x.Id == s.Key.Id && x.IdTipoCategoriaOrigen != null).Distinct()
                                                    .Select(sss => new ConfiguracionDatoRemarketingTipoCategoriaOrigenGrillaDTO() { IdTipoCategoriaOrigen = sss.IdTipoCategoriaOrigen.Value, NombreTipoCategoriaOrigen = sss.NombreTipoCategoriaOrigen })
                                                    .ToList(),
                    ListaCategoriaOrigen = listaAFormatear
                                                    .Select(ss => new { ss.Id, ss.IdCategoriaOrigen, ss.NombreCategoriaOrigen })
                                                    .Where(x => x.Id == s.Key.Id && x.IdCategoriaOrigen != null).Distinct()
                                                    .Select(sss => new ConfiguracionDatoRemarketingCategoriaOrigenGrillaDTO() { IdCategoriaOrigen = sss.IdCategoriaOrigen.Value, NombreCategoriaOrigen = sss.NombreCategoriaOrigen })
                                                    .ToList(),
                    ListaProbabilidadRegistroPw = listaAFormatear
                                                    .Select(ss => new { ss.Id, ss.IdProbabilidadRegistroPw, ss.NombreProbabilidadRegistroPw })
                                                    .Where(x => x.Id == s.Key.Id && x.IdProbabilidadRegistroPw != null).Distinct()
                                                    .Select(sss => new ConfiguracionDatoRemarketingProbabilidadRegistroPwGrillaDTO() { IdProbabilidadRegistroPw = sss.IdProbabilidadRegistroPw.Value, NombreProbabilidadRegistroPw = sss.NombreProbabilidadRegistroPw })
                                                    .ToList()
                }).ToList();

                return resultadoAgrupado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 06/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los combos para configuracion de dato Remarketing
        /// </summary>
        /// <returns>Objeto de clase ComboConfiguracionDatoRemarketingDTO</returns>
        public ComboConfiguracionDatoRemarketingDTO ObtenerCombosParaConfiguracionDatoRemarketing()
        {
            try

            {
                var _repConfiguracionDatoRemarketing = _unitOfWork.ConfiguracionDatoRemarketingRepository;
                var _repTipoDato = _unitOfWork.TipoDatoRepository;
                var _repCategoriaOrigen = _unitOfWork.CategoriaOrigenRepository;
                var _repTipoCategoriaOrigen = _unitOfWork.TipoCategoriaOrigenRepository;
                var _repProbabilidadRegistroPw = _unitOfWork.ProbabilidadRegistroPwRepository;

                var comboAgendaTab = _repConfiguracionDatoRemarketing.ObtenerAgendaTabVentasParaConfiguracion();
                var comboTipoDato = _repTipoDato.GetBy(x => x.Estado == true).Select(s => new ConfiguracionDatoRemarketingTipoDatoGrillaDTO { IdTipoDato = s.Id, NombreTipoDato = s.Nombre }).ToList();
                var comboCategoriaOrigen = _repCategoriaOrigen
                        .GetBy(x => x.Nombre.ToLower().Contains("remarketing")
                                 || x.Nombre.ToLower().Contains("mailing")
                                 || x.Nombre.ToLower().Contains("whatsapp"))
                        .Select(s => new ConfiguracionDatoRemarketingCategoriaOrigenGrillaDTO
                        {
                            IdCategoriaOrigen = s.Id,
                            NombreCategoriaOrigen = s.Nombre,
                            IdTipoCategoriaOrigen = s.IdTipoCategoriaOrigen
                        }).ToList();

                var comboTipoCategoriaOrigen = _repTipoCategoriaOrigen.GetBy(x => comboCategoriaOrigen.Select(s => s.IdTipoCategoriaOrigen).Contains(x.Id)).Select(s => new ConfiguracionDatoRemarketingTipoCategoriaOrigenGrillaDTO { IdTipoCategoriaOrigen = s.Id, NombreTipoCategoriaOrigen = s.Nombre }).ToList();
                var comboProbabilidadRegistroPw = _repProbabilidadRegistroPw.GetBy(x => x.Estado == true).ToList().Select(s => new ConfiguracionDatoRemarketingProbabilidadRegistroPwGrillaDTO { IdProbabilidadRegistroPw = s.Id, NombreProbabilidadRegistroPw = s.Nombre }).ToList();

                return new ComboConfiguracionDatoRemarketingDTO
                {
                    ListaComboConfiguracionDatoRemarketingAgendaTab = comboAgendaTab,
                    ListaComboConfiguracionDatoRemarketingTipoDato = comboTipoDato,
                    ListaComboConfiguracionDatoRemarketingTipoCategoriaOrigen = comboTipoCategoriaOrigen,
                    ListaComboConfiguracionDatoRemarketingCategoriaOrigen = comboCategoriaOrigen,
                    ListaComboConfiguracionDatoRemarketingProbabilidadRegistroPw = comboProbabilidadRegistroPw
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionDatoRemarketingAgendaTabVentasDTO> ObtenerAgendaTabVentasParaConfiguracion()
        {
            throw new NotImplementedException();
        }
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 06/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza lista de configuracion de remarketing Tipo de Dato
        /// </summary>
        /// <param name="configuracionDatoRemarketingAActualizar">Objeto de clase ConfiguracionDatoRemarketingDTO</param>
        /// <returns>bool</returns>
        public bool ActualizarListaConfiguracionDatoRemarketingGeneral(ConfiguracionDatoRemarketingDTO configuracionDatoRemarketingAActualizar)
                                         {
            try
            {
           
                bool resultadoActualizacionConfiguracion = false;


                var _repConfiguracionDatoRemarketing = _unitOfWork.ConfiguracionDatoRemarketingRepository;




                using (TransactionScope scope = new TransactionScope())
                {
                    var ConfiguracionDatoRemarketingTipoDato = new ConfiguracionDatoRemarketingTipoDatoService(_unitOfWork);
                    var ConfiguracionDatoRemarketingTipoCategoriaOrigen = new ConfiguracionDatoRemarketingTipoCategoriaOrigenService(_unitOfWork);
                    var ConfiguracionDatoRemarketingCategoriaOrigen = new ConfiguracionDatoRemarketingCategoriaOrigenService(_unitOfWork);
                    var ConfiguracionDatoRemarketingProbabilidadRegistro = new ConfiguracionDatoRemarketingProbabilidadRegistroService(_unitOfWork);

                    configuracionDatoRemarketingAActualizar.Id = ActualizarListaConfiguracionDatoRemarketing(configuracionDatoRemarketingAActualizar);

                    resultadoActualizacionConfiguracion = ConfiguracionDatoRemarketingTipoDato.ActualizarListaConfiguracionDatoRemarketingTipoDato(configuracionDatoRemarketingAActualizar.Id, configuracionDatoRemarketingAActualizar.ListaIdTipoDato, configuracionDatoRemarketingAActualizar.Usuario);


                    resultadoActualizacionConfiguracion = ConfiguracionDatoRemarketingTipoCategoriaOrigen.ActualizarListaConfiguracionDatoRemarketingTipoCategoriaOrigen(configuracionDatoRemarketingAActualizar.Id, configuracionDatoRemarketingAActualizar.ListaIdTipoCategoriaOrigen, configuracionDatoRemarketingAActualizar.Usuario);
                    resultadoActualizacionConfiguracion = ConfiguracionDatoRemarketingCategoriaOrigen.ActualizarListaConfiguracionDatoRemarketingCategoriaOrigen(configuracionDatoRemarketingAActualizar.Id, configuracionDatoRemarketingAActualizar.ListaCategoriaOrigen, configuracionDatoRemarketingAActualizar.Usuario);
                    resultadoActualizacionConfiguracion = ConfiguracionDatoRemarketingProbabilidadRegistro.ActualizarListaConfiguracionDatoRemarketingProbabilidadRegistro(configuracionDatoRemarketingAActualizar.Id, configuracionDatoRemarketingAActualizar.ListaProbabilidadRegistro, configuracionDatoRemarketingAActualizar.Usuario);



                    scope.Complete();
                }

                return resultadoActualizacionConfiguracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 06/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza lista de configuracion de remarketing Tipo de Dato
        /// </summary>
        /// <param name="configuracionDatoRemarketingAActualizar">Objeto de clase ConfiguracionDatoRemarketingDTO</param>
        /// <returns>bool</returns>
        public int ActualizarListaConfiguracionDatoRemarketing(ConfiguracionDatoRemarketingDTO configuracionDatoRemarketingAActualizar)
        {
            try
            {
                var _repConfiguracionDatoRemarketing = _unitOfWork.ConfiguracionDatoRemarketingRepository;
                var configuracionAActualizar = _repConfiguracionDatoRemarketing.FirstBy(x => x.Id == configuracionDatoRemarketingAActualizar.Id);

                if (configuracionAActualizar == null)
                {
                     configuracionAActualizar = new TConfiguracionDatoRemarketing()
                    {
                        IdAgendaTab = configuracionDatoRemarketingAActualizar.IdAgendaTab,
                        FechaInicio = configuracionDatoRemarketingAActualizar.FechaInicio,
                        FechaFin = configuracionDatoRemarketingAActualizar.FechaFin,
                        Estado = true,
                        UsuarioCreacion = configuracionDatoRemarketingAActualizar.Usuario,
                        UsuarioModificacion = configuracionDatoRemarketingAActualizar.Usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                }
                else
                {
                    configuracionAActualizar.IdAgendaTab = configuracionDatoRemarketingAActualizar.IdAgendaTab;
                    configuracionAActualizar.FechaInicio = configuracionDatoRemarketingAActualizar.FechaInicio;
                    configuracionAActualizar.FechaFin = configuracionDatoRemarketingAActualizar.FechaFin;
                    configuracionAActualizar.Estado = true;
                    configuracionAActualizar.UsuarioModificacion = configuracionDatoRemarketingAActualizar.Usuario;
                    configuracionAActualizar.FechaModificacion = DateTime.Now;
                }

                bool resultadoActualizacion = _repConfiguracionDatoRemarketing.Update(configuracionAActualizar);
                _unitOfWork.Commit();
            

                if (!resultadoActualizacion)
                    throw new Exception("Fallo en la actualizacion de la configuracion base de remarketing");

                return configuracionAActualizar.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 06/10/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina una configuracion de dato de remarketing
        /// </summary>
        /// <param name="configuracionDatoRemarketingAEliminar">Objeto de clase ConfiguracionDatoRemarketingAEliminarDTO</param>
        /// <returns>bool</returns>
        public bool EliminarConfiguracionDatoRemarketingGeneral(ConfiguracionDatoRemarketingAEliminarDTO configuracionDatoRemarketingAEliminar)
        {
            try
            {
                bool resultadoEliminadoConfiguracion = false;

                using (TransactionScope scope = new TransactionScope())
                {
                    var ConfiguracionDatoRemarketingTipoDato = new ConfiguracionDatoRemarketingTipoDatoService(_unitOfWork);
                    var ConfiguracionDatoRemarketingTipoCategoriaOrigen = new ConfiguracionDatoRemarketingTipoCategoriaOrigenService(_unitOfWork);
                    var ConfiguracionDatoRemarketingCategoriaOrigen = new ConfiguracionDatoRemarketingCategoriaOrigenService(_unitOfWork);
                    var ConfiguracionDatoRemarketingProbabilidadRegistro = new ConfiguracionDatoRemarketingProbabilidadRegistroService(_unitOfWork);

                  

                    resultadoEliminadoConfiguracion = ConfiguracionDatoRemarketingTipoDato.EliminarListaConfiguracionDatoRemarketingTipoDato(configuracionDatoRemarketingAEliminar.Id, configuracionDatoRemarketingAEliminar.Usuario);
                    resultadoEliminadoConfiguracion = ConfiguracionDatoRemarketingTipoCategoriaOrigen.EliminarListaConfiguracionDatoRemarketingTipoCategoriaOrigen(configuracionDatoRemarketingAEliminar.Id, configuracionDatoRemarketingAEliminar.Usuario);
                    resultadoEliminadoConfiguracion = ConfiguracionDatoRemarketingCategoriaOrigen.EliminarListaConfiguracionDatoRemarketingCategoriaOrigen(configuracionDatoRemarketingAEliminar.Id, configuracionDatoRemarketingAEliminar.Usuario);
                    resultadoEliminadoConfiguracion = ConfiguracionDatoRemarketingProbabilidadRegistro.EliminarListaConfiguracionDatoRemarketingProbabilidadRegistro(configuracionDatoRemarketingAEliminar.Id, configuracionDatoRemarketingAEliminar.Usuario);
                    resultadoEliminadoConfiguracion = EliminarListaConfiguracionDatoRemarketing(configuracionDatoRemarketingAEliminar.Id, configuracionDatoRemarketingAEliminar.Usuario);

                    scope.Complete();
                }

                return resultadoEliminadoConfiguracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 06/10/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina la lista de configuracion de datos de remarketing de probabilidad de registro
        /// </summary>
        /// <param name="idConfiguracionDatoRemarketing">Id de la configuracion de dato de remarketing (PK de la tabla mkt.T_ConfiguracionDatoRemarketing)</param>
        /// <param name="usuarioResponsable">Usuario que realiza la accion</param>
        /// <returns>bool</returns>
        public bool EliminarListaConfiguracionDatoRemarketing(int idConfiguracionDatoRemarketing, string usuarioResponsable)
        {
            try
            {
                var _repConfiguracionDatoRemarketing = _unitOfWork.ConfiguracionDatoRemarketingRepository;
                var listaIdAEliminar = _repConfiguracionDatoRemarketing.GetBy(x => x.Id == idConfiguracionDatoRemarketing).Select(s => s.Id).ToList();
                bool resultadoEliminado = true;

                if (listaIdAEliminar.Any())
                    resultadoEliminado = _repConfiguracionDatoRemarketing.Delete(listaIdAEliminar, usuarioResponsable);
                _unitOfWork.Commit();

                if (!resultadoEliminado)
                    throw new Exception("Fallo en el eliminado de la base de configuracion de dato remarketing");

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }




}

