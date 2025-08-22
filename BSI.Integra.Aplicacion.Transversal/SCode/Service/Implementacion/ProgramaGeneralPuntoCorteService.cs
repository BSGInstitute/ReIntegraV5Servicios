using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ProgramaGeneralPuntoCorteService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_ProgramaGeneralPuntoCorte
    /// </summary>
    public class ProgramaGeneralPuntoCorteService : IProgramaGeneralPuntoCorteService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProgramaGeneralPuntoCorteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TProgramaGeneralPuntoCorte, ProgramaGeneralPuntoCorte>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 04/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene los combos para el modulo de puntos de corte
        /// </summary>
        /// <returns> PGeneralPuntoCorteComboDTO </returns>
        public PGeneralPuntoCorteComboDTO ObtenerComboModulo()
        {
            try
            {
                PGeneralPuntoCorteComboDTO combo = new PGeneralPuntoCorteComboDTO
                {
                    ListaAreaCapacitacion = _unitOfWork.AreaCapacitacionRepository.ObtenerCombo(),
                    ListaSubAreaCapacitacion = _unitOfWork.SubAreaCapacitacionRepository.ObtenerFiltro(),
                    ListaProgramaGeneral = _unitOfWork.PGeneralRepository.ObtenerProgramaSubAreaFiltroTodo(),
                    ListaPuntoCorte = _unitOfWork.PuntoCorteRepository.ObtenerCombo()
                };
                return combo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 04/07/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene la configiruacion de puntos de corte
        /// </summary>
        /// <returns> Lista ProgramaGeneralPuntoCorteConfiguracionDTO </returns>
        public List<ProgramaGeneralPuntoCorteConfiguracionDTO> ObtenerConfiguracionPuntoCorte()
        {
            try
            {
                return _unitOfWork.ProgramaGeneralPuntoCorteConfiguracionRepository.Obtener();
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
        /// Obtiene registros de T_ConfiguracionDatoRemarketing para mostrarse en lista.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public List<ProgramaGeneralPuntoCorteAreaSubAreaDTO> ObtenerListaProgramaGeneralPuntoCorte(ProgramaGeneralPuntoCorteFiltroDTO filtroProgramaGeneralPuntoCorte)
        {
            try
            {
                var resultado = _unitOfWork.ProgramaGeneralPuntoCorteRepository.ObtenerListaProgramaGeneralPuntoCorte(filtroProgramaGeneralPuntoCorte)
                    .OrderBy(x => x.IdProgramaGeneralPuntoCorte)
                    .ThenBy(y => y.PuntoCorteMedia)
                    .ThenBy(z => z.PuntoCorteAlta)
                    .ThenBy(zz => zz.PuntoCorteMuyAlta)
                    .ToList();
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Actualiza el programa general con el punto de corte masivo
        /// </summary>
        /// <param name="listaActualizar">Objeto de clase ProgramaGeneralPuntoCorteMasivoDTO</param>
        /// <returns>Bool</returns>
        public bool ActualizarProgramaGeneralPuntoCorteMasivo(ProgramaGeneralPuntoCorteMasivoDTO dto, string usuario)
        {
            try
            {
                bool resultadoPrevalidacion = PrevalidarLista(dto);
                using (TransactionScope scope = new TransactionScope())
                {
                    if (dto.ProgramaGeneralPuntoCorte.Count() > 0)
                    {
                        dto.ProgramaGeneralPuntoCorte.ForEach(item =>
                        {
                            EliminarRegistrosAntiguos(dto.ListaIdPgeneral, item.IdPais, usuario);
                            InsertarPuntoCorteMasivo(dto.ListaIdPgeneral, item, usuario);
                        });
                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Prevalida la lista enviada
        /// </summary>
        /// <param name="listaActualizar">Objeto de clase ProgramaGeneralPuntoCorteMasivoDTO</param>
        /// <returns>Bool</returns>
        private bool PrevalidarLista(ProgramaGeneralPuntoCorteMasivoDTO listaActualizar)
        {
            try
            {
                if (listaActualizar.ListaIdPgeneral.Where(x => x >= 0).Count() == listaActualizar.ListaIdPgeneral.Count())
                    return true;
                else
                    throw new Exception("Falta el programa general de un elemento de la lista");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Elimina configuraciones de la lista de PGeneral enviados como parametros
        /// </summary>
        /// <param name="idsPgeneral">Lista de los Ids de PGeneral a eliminar la configuracion</param>
        /// <param name="usuario">Usuario responsable del eliminado</param>
        /// <returns>Bool</returns>
        private void EliminarRegistrosAntiguos(List<int> idsPgeneral, int? idPais, string usuario)
        {
            try
            {
                List<int> pgeneralPuntosDeCorte = _unitOfWork.ProgramaGeneralPuntoCorteRepository.GetBy(x => idsPgeneral.Contains(x.IdProgramaGeneral!.Value) && x.IdPais == idPais).Select(s => s.Id).ToList();

                if (pgeneralPuntosDeCorte.Any())
                {
                    pgeneralPuntosDeCorte.ForEach(idPgeneralPuntoCorte =>
                    {
                        var listaDetalle = _unitOfWork.ProgramaGeneralPuntoCorteDetalleRepository.GetBy(x => x.IdProgramaGeneralPuntoCorte == idPgeneralPuntoCorte).Select(s => s.Id).ToList();
                        if (listaDetalle.Any())
                        {
                            _unitOfWork.ProgramaGeneralPuntoCorteDetalleRepository.Delete(listaDetalle, usuario);
                            _unitOfWork.Commit();
                        }
                    });
                }
                if (pgeneralPuntosDeCorte.Any())
                {
                    _unitOfWork.ProgramaGeneralPuntoCorteRepository.Delete(pgeneralPuntosDeCorte, usuario);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Inserta el punto de corte masivo
        /// </summary>
        /// <param name="dto">Objeto de clase ProgramaGeneralPuntoCorteMasivoDTO</param>
        /// <returns>Bool</returns>
        private void InsertarPuntoCorteMasivo(List<int> idsPgeneral, ProgramaGeneralPuntoCorteDTO dto, string usuario)
        {
            try
            {
                foreach (var idPgeneral in idsPgeneral)
                {
                    ProgramaGeneralPuntoCorte entidad = new ProgramaGeneralPuntoCorte()
                    {
                        IdProgramaGeneral = idPgeneral,
                        PuntoCorteMedia = dto.PuntoCorteMedia,
                        PuntoCorteAlta = dto.PuntoCorteAlta,
                        PuntoCorteMuyAlta = dto.PuntoCorteMuyAlta,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        IdPais = dto.IdPais,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    };

                    var resultado = _unitOfWork.ProgramaGeneralPuntoCorteRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = resultado.Id;

                    var detallePuntoCorte = new List<ProgramaGeneralPuntoCorteDetalle>();
                    foreach (var item in dto.ListaPuntoCorteMedia)
                    {
                        ProgramaGeneralPuntoCorteDetalle entidadDetalle = new();
                        entidadDetalle.IdProgramaGeneralPuntoCorte = entidad.Id;
                        entidadDetalle.IdPuntoCorte = 1;
                        entidadDetalle.Tipo = item.Tipo;
                        entidadDetalle.Descripcion = item.Descripcion;
                        entidadDetalle.ValorMinimo = item.ValorMinimo;
                        entidadDetalle.ValorMaximo = item.ValorMaximo;
                        entidadDetalle.Estado = true;
                        entidadDetalle.UsuarioCreacion = usuario;
                        entidadDetalle.UsuarioModificacion = usuario;
                        entidadDetalle.FechaCreacion = DateTime.Now;
                        entidadDetalle.FechaModificacion = DateTime.Now;
                        detallePuntoCorte.Add(entidadDetalle);
                    }
                    foreach (var item in dto.ListaPuntoCorteAlta)
                    {
                        ProgramaGeneralPuntoCorteDetalle entidadDetalle = new();
                        entidadDetalle.IdProgramaGeneralPuntoCorte = entidad.Id;
                        entidadDetalle.IdPuntoCorte = 2;
                        entidadDetalle.Tipo = item.Tipo;
                        entidadDetalle.Descripcion = item.Descripcion;
                        entidadDetalle.ValorMinimo = item.ValorMinimo;
                        entidadDetalle.ValorMaximo = item.ValorMaximo;
                        entidadDetalle.Estado = true;
                        entidadDetalle.UsuarioCreacion = usuario;
                        entidadDetalle.UsuarioModificacion = usuario;
                        entidadDetalle.FechaCreacion = DateTime.Now;
                        entidadDetalle.FechaModificacion = DateTime.Now;
                        detallePuntoCorte.Add(entidadDetalle);
                    }
                    foreach (var item in dto.ListaPuntoCorteMuyAlta)
                    {
                        ProgramaGeneralPuntoCorteDetalle entidadDetalle = new();
                        entidadDetalle.IdProgramaGeneralPuntoCorte = entidad.Id;
                        entidadDetalle.IdPuntoCorte = 3;
                        entidadDetalle.Tipo = item.Tipo;
                        entidadDetalle.Descripcion = item.Descripcion;
                        entidadDetalle.ValorMinimo = item.ValorMinimo;
                        entidadDetalle.ValorMaximo = item.ValorMaximo;
                        entidadDetalle.Estado = true;
                        entidadDetalle.UsuarioCreacion = usuario;
                        entidadDetalle.UsuarioModificacion = usuario;
                        entidadDetalle.FechaCreacion = DateTime.Now;
                        entidadDetalle.FechaModificacion = DateTime.Now;
                        detallePuntoCorte.Add(entidadDetalle);
                    }
                    _unitOfWork.ProgramaGeneralPuntoCorteDetalleRepository.Add(detallePuntoCorte);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ActualizarProgramaGeneralPuntoCortePaises(List<ProgramaGeneralPuntoCorteDTO> listaDto, string usuario)
        {
            try
            {
                if (listaDto.Count() == 0)
                {
                    return false;
                }
                if (listaDto.FirstOrDefault()!.IdProgramaGeneral == 0)
                {
                    throw new Exception("Ingrese un programa general");
                }
                var idProgramaGeneral = listaDto.FirstOrDefault()!.IdProgramaGeneral;
                var programaGeneralPuntoCorte = _unitOfWork.ProgramaGeneralPuntoCorteRepository.ObtenerPorIdPgeneral(idProgramaGeneral);
                using (TransactionScope scope = new TransactionScope())
                {
                    listaDto.ForEach(dto =>
                    {
                        var existe = programaGeneralPuntoCorte.Where(x => x.IdPais == dto.IdPais).ToList();
                        if (existe.Count > 0)
                        {
                            _unitOfWork.ProgramaGeneralPuntoCorteRepository.Delete(existe.Select(s => s.Id), usuario);
                            _unitOfWork.Commit();
                        }
                        ProgramaGeneralPuntoCorte entidad = new ProgramaGeneralPuntoCorte();
                        entidad.IdProgramaGeneral = dto.IdProgramaGeneral;
                        entidad.PuntoCorteMedia = dto.PuntoCorteMedia;
                        entidad.PuntoCorteAlta = dto.PuntoCorteAlta;
                        entidad.PuntoCorteMuyAlta = dto.PuntoCorteMuyAlta;
                        entidad.IdPais = dto.IdPais;
                        entidad.Estado = true;
                        entidad.UsuarioCreacion = usuario;
                        entidad.UsuarioModificacion = usuario;
                        entidad.FechaCreacion = DateTime.Now;
                        entidad.FechaModificacion = DateTime.Now;
                        var res = _unitOfWork.ProgramaGeneralPuntoCorteRepository.Add(entidad);
                        _unitOfWork.Commit();
                        entidad.Id = res.Id;

                        var detallePuntoCorte = new List<ProgramaGeneralPuntoCorteDetalle>();
                        foreach (var item in dto.ListaPuntoCorteMedia)
                        {
                            var entidadDetalle = new ProgramaGeneralPuntoCorteDetalle();
                            entidadDetalle.IdProgramaGeneralPuntoCorte = entidad.Id;
                            entidadDetalle.IdPuntoCorte = 1;
                            entidadDetalle.Tipo = item.Tipo;
                            entidadDetalle.Descripcion = item.Descripcion;
                            entidadDetalle.ValorMinimo = item.ValorMinimo;
                            entidadDetalle.ValorMaximo = item.ValorMaximo;
                            entidadDetalle.Estado = true;
                            entidadDetalle.UsuarioCreacion = usuario;
                            entidadDetalle.UsuarioModificacion = usuario;
                            entidadDetalle.FechaCreacion = DateTime.Now;
                            entidadDetalle.FechaModificacion = DateTime.Now;
                            detallePuntoCorte.Add(entidadDetalle);
                        }

                        foreach (var item in dto.ListaPuntoCorteAlta)
                        {
                            var entidadDetalle = new ProgramaGeneralPuntoCorteDetalle();
                            entidadDetalle.IdProgramaGeneralPuntoCorte = entidad.Id;
                            entidadDetalle.IdPuntoCorte = 2;
                            entidadDetalle.Tipo = item.Tipo;
                            entidadDetalle.Descripcion = item.Descripcion;
                            entidadDetalle.ValorMinimo = item.ValorMinimo;
                            entidadDetalle.ValorMaximo = item.ValorMaximo;
                            entidadDetalle.Estado = true;
                            entidadDetalle.UsuarioCreacion = usuario;
                            entidadDetalle.UsuarioModificacion = usuario;
                            entidadDetalle.FechaCreacion = DateTime.Now;
                            entidadDetalle.FechaModificacion = DateTime.Now;
                            entidadDetalle.Id = 0;
                            detallePuntoCorte.Add(entidadDetalle);
                        }
                        foreach (var item in dto.ListaPuntoCorteMuyAlta)
                        {
                            var entidadDetalle = new ProgramaGeneralPuntoCorteDetalle();
                            entidadDetalle.IdProgramaGeneralPuntoCorte = entidad.Id;
                            entidadDetalle.IdPuntoCorte = 3;
                            entidadDetalle.Tipo = item.Tipo;
                            entidadDetalle.Descripcion = item.Descripcion;
                            entidadDetalle.ValorMinimo = item.ValorMinimo;
                            entidadDetalle.ValorMaximo = item.ValorMaximo;
                            entidadDetalle.Estado = true;
                            entidadDetalle.UsuarioCreacion = usuario;
                            entidadDetalle.UsuarioModificacion = usuario;
                            entidadDetalle.FechaCreacion = DateTime.Now;
                            entidadDetalle.FechaModificacion = DateTime.Now;
                            detallePuntoCorte.Add(entidadDetalle);
                        }
                        _unitOfWork.ProgramaGeneralPuntoCorteDetalleRepository.Add(detallePuntoCorte);
                        _unitOfWork.Commit();
                    });
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ActualizarProgramaGeneralPuntoCorte(ProgramaGeneralPuntoCorteDTO dto, string usuario)
        {
            try
            {
                if (dto.IdProgramaGeneral == 0)
                {
                    throw new Exception("Ingrese un programa general");
                }

                var programaGeneralPuntoCorte = _unitOfWork.ProgramaGeneralPuntoCorteRepository.ObtenerPorIdPgeneral(dto.IdProgramaGeneral);
                using (TransactionScope scope = new TransactionScope())
                {
                    var existe = programaGeneralPuntoCorte.Where(x => x.IdPais == dto.IdPais).ToList();
                    if (existe.Count > 0)
                    {
                        _unitOfWork.ProgramaGeneralPuntoCorteRepository.Delete(existe.Select(s => s.Id), usuario);
                        _unitOfWork.Commit();
                    }
                    ProgramaGeneralPuntoCorte entidad = new ProgramaGeneralPuntoCorte();
                    entidad.IdProgramaGeneral = dto.IdProgramaGeneral;
                    entidad.PuntoCorteMedia = dto.PuntoCorteMedia;
                    entidad.PuntoCorteAlta = dto.PuntoCorteAlta;
                    entidad.PuntoCorteMuyAlta = dto.PuntoCorteMuyAlta;
                    entidad.IdPais = dto.IdPais;
                    entidad.Estado = true;
                    entidad.UsuarioCreacion = usuario;
                    entidad.UsuarioModificacion = usuario;
                    entidad.FechaCreacion = DateTime.Now;
                    entidad.FechaModificacion = DateTime.Now;
                    var res = _unitOfWork.ProgramaGeneralPuntoCorteRepository.Add(entidad);
                    _unitOfWork.Commit();
                    entidad.Id = res.Id;

                    var detallePuntoCorte = new List<ProgramaGeneralPuntoCorteDetalle>();
                    foreach (var item in dto.ListaPuntoCorteMedia)
                    {
                        var entidadDetalle = new ProgramaGeneralPuntoCorteDetalle();
                        entidadDetalle.IdProgramaGeneralPuntoCorte = entidad.Id;
                        entidadDetalle.IdPuntoCorte = 1;
                        entidadDetalle.Tipo = item.Tipo;
                        entidadDetalle.Descripcion = item.Descripcion;
                        entidadDetalle.ValorMinimo = item.ValorMinimo;
                        entidadDetalle.ValorMaximo = item.ValorMaximo;
                        entidadDetalle.Estado = true;
                        entidadDetalle.UsuarioCreacion = usuario;
                        entidadDetalle.UsuarioModificacion = usuario;
                        entidadDetalle.FechaCreacion = DateTime.Now;
                        entidadDetalle.FechaModificacion = DateTime.Now;
                        detallePuntoCorte.Add(entidadDetalle);
                    }

                    foreach (var item in dto.ListaPuntoCorteAlta)
                    {
                        var entidadDetalle = new ProgramaGeneralPuntoCorteDetalle();
                        entidadDetalle.IdProgramaGeneralPuntoCorte = entidad.Id;
                        entidadDetalle.IdPuntoCorte = 2;
                        entidadDetalle.Tipo = item.Tipo;
                        entidadDetalle.Descripcion = item.Descripcion;
                        entidadDetalle.ValorMinimo = item.ValorMinimo;
                        entidadDetalle.ValorMaximo = item.ValorMaximo;
                        entidadDetalle.Estado = true;
                        entidadDetalle.UsuarioCreacion = usuario;
                        entidadDetalle.UsuarioModificacion = usuario;
                        entidadDetalle.FechaCreacion = DateTime.Now;
                        entidadDetalle.FechaModificacion = DateTime.Now;
                        entidadDetalle.Id = 0;
                        detallePuntoCorte.Add(entidadDetalle);
                    }
                    foreach (var item in dto.ListaPuntoCorteMuyAlta)
                    {
                        var entidadDetalle = new ProgramaGeneralPuntoCorteDetalle();
                        entidadDetalle.IdProgramaGeneralPuntoCorte = entidad.Id;
                        entidadDetalle.IdPuntoCorte = 3;
                        entidadDetalle.Tipo = item.Tipo;
                        entidadDetalle.Descripcion = item.Descripcion;
                        entidadDetalle.ValorMinimo = item.ValorMinimo;
                        entidadDetalle.ValorMaximo = item.ValorMaximo;
                        entidadDetalle.Estado = true;
                        entidadDetalle.UsuarioCreacion = usuario;
                        entidadDetalle.UsuarioModificacion = usuario;
                        entidadDetalle.FechaCreacion = DateTime.Now;
                        entidadDetalle.FechaModificacion = DateTime.Now;
                        detallePuntoCorte.Add(entidadDetalle);
                    }
                    _unitOfWork.ProgramaGeneralPuntoCorteDetalleRepository.Add(detallePuntoCorte);
                    _unitOfWork.Commit();

                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ActualizarProgramaGeneralPuntoCorteConfiguracion(List<ProgramaGeneralPuntoCorteConfiguracionDTO> dtoConfiguracion, string usuario)
        {
            try
            {
                List<int> listaIds = _unitOfWork.ProgramaGeneralPuntoCorteConfiguracionRepository.GetBy(e => e.Estado == true).Select(s => s.Id).ToList();
                using (TransactionScope scope = new TransactionScope())
                {
                    if (listaIds.Any())
                    {
                        _unitOfWork.ProgramaGeneralPuntoCorteConfiguracionRepository.Delete(listaIds, usuario);
                        _unitOfWork.Commit();
                    }
                    List<ProgramaGeneralPuntoCorteConfiguracion> listaConfiguracion = new();
                    foreach (var item in dtoConfiguracion)
                    {
                        ProgramaGeneralPuntoCorteConfiguracion configuracion = new();
                        configuracion.IdTipoCorte = item.IdTipoCorte;
                        configuracion.Tipo = item.Tipo;
                        configuracion.IdAreaCapacitacion = item.IdAreaCapacitacion;
                        configuracion.IdSubAreaCapacitacion = item.IdSubAreaCapacitacion;
                        configuracion.IdPgeneral = item.IdPgeneral;
                        configuracion.Color = item.Color;
                        configuracion.Texto = item.Texto;
                        configuracion.Estado = true;
                        configuracion.UsuarioCreacion = usuario;
                        configuracion.UsuarioModificacion = usuario;
                        configuracion.FechaCreacion = DateTime.Now;
                        configuracion.FechaModificacion = DateTime.Now;
                        listaConfiguracion.Add(configuracion);
                    }
                    _unitOfWork.ProgramaGeneralPuntoCorteConfiguracionRepository.Add(listaConfiguracion);
                    _unitOfWork.Commit();
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ProgramaGeneralPuntoCorteDTO> ObtenerPuntoCortePorPrograma(int idProgramaGeneral)
        {
            try
            {
                var puntosCortePrograma = _unitOfWork.ProgramaGeneralPuntoCorteRepository.ObtenerPorIdPgeneral(idProgramaGeneral);
                puntosCortePrograma.ForEach(x =>
                {
                    var detalle = _unitOfWork.ProgramaGeneralPuntoCorteDetalleRepository.ObtenerPorIdProgramaGeneralPuntoCorte(x.Id);
                    x.ListaPuntoCorteMedia = detalle.Where(d => d.IdPuntoCorte == 1).ToList();
                    x.ListaPuntoCorteAlta = detalle.Where(d => d.IdPuntoCorte == 2).ToList();
                    x.ListaPuntoCorteMuyAlta = detalle.Where(d => d.IdPuntoCorte == 3).ToList();
                });
                return puntosCortePrograma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProgramaGeneralPuntoCorteDTO? ObtenerPuntoCortePorProgramaPais(int idProgramaGeneral, int idPais)
        {
            try
            {
                var puntoCortePrograma = _unitOfWork.ProgramaGeneralPuntoCorteRepository.ObtenerPorIdPgeneralIdPais(idProgramaGeneral, idPais);
                if (puntoCortePrograma != null)
                {
                    var detalle = _unitOfWork.ProgramaGeneralPuntoCorteDetalleRepository.ObtenerPorIdProgramaGeneralPuntoCorte(puntoCortePrograma.Id);
                    puntoCortePrograma.ListaPuntoCorteMedia = detalle.Where(d => d.IdPuntoCorte == 1).OrderBy(x => x.Id).ToList();
                    puntoCortePrograma.ListaPuntoCorteAlta = detalle.Where(d => d.IdPuntoCorte == 2).OrderBy(x => x.Id).ToList();
                    puntoCortePrograma.ListaPuntoCorteMuyAlta = detalle.Where(d => d.IdPuntoCorte == 3).OrderBy(x => x.Id).ToList();
                }
                return puntoCortePrograma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ProgramaGeneralPuntoCorteDetalleDTO> ObtenerDetallePuntoCortePorIdPuntoCorte(PuntoCorteDetalleFiltroDTO filtro)
        {
            try
            {
                var puntoCortePrograma = _unitOfWork.ProgramaGeneralPuntoCorteRepository.ObtenerPorIdPgeneralIdPais(filtro.IdProgramaGeneral, filtro.IdPais);
                if (puntoCortePrograma != null)
                {
                    var detalle = _unitOfWork.ProgramaGeneralPuntoCorteDetalleRepository.ObtenerPorIdProgramaGeneralPuntoCorteIdPuntoCorte(puntoCortePrograma.Id, filtro.IdPuntoCorte);
                    return detalle;
                }
                return new List<ProgramaGeneralPuntoCorteDetalleDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Eliminar(List<int> idPaises, int idProgramaGeneral, string usuario)
        {
            try
            {
                if (idPaises.Count() == 0)
                {
                    return false;
                }
                else
                {
                    idPaises.ForEach(pais =>
                    {
                        var registro = _unitOfWork.ProgramaGeneralPuntoCorteRepository.ObtenerPorIdPgeneralIdPais(idProgramaGeneral, pais);
                        if (registro != null)
                        {
                            _unitOfWork.ProgramaGeneralPuntoCorteRepository.Delete(registro.Id, usuario);
                            _unitOfWork.Commit();
                            var detalles = _unitOfWork.ProgramaGeneralPuntoCorteDetalleRepository.ObtenerPorIdProgramaGeneralPuntoCorte(registro.Id);
                            if (detalles.Count() > 0)
                            {
                                _unitOfWork.ProgramaGeneralPuntoCorteDetalleRepository.Delete(detalles.Select(s => s.Id), usuario);
                                _unitOfWork.Commit();
                            }
                        }
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}





