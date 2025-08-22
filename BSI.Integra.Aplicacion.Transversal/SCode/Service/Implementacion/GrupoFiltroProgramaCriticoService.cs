using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: GrupoFiltroProgramaCriticoService
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 07/10/2022
    /// <summary>
    /// Gestión general de T_GrupoFiltroProgramaCritico
    /// </summary>
    public class GrupoFiltroProgramaCriticoService : IGrupoFiltroProgramaCriticoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public GrupoFiltroProgramaCriticoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TGrupoFiltroProgramaCritico, GrupoFiltroProgramaCritico>(MemberList.None).ReverseMap();
                cfg.CreateMap<TGrupoFiltroProgramaCriticoPorAsesor, GrupoFiltroProgramaCriticoPorAsesor>(MemberList.None).ReverseMap();
            });

            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public GrupoFiltroProgramaCritico Add(GrupoFiltroProgramaCritico entidad)
        {
            try
            {
                var modelo = _unitOfWork.GrupoFiltroProgramaCriticoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<GrupoFiltroProgramaCritico>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GrupoFiltroProgramaCritico Update(GrupoFiltroProgramaCritico entidad)
        {
            try
            {
                var modelo = _unitOfWork.GrupoFiltroProgramaCriticoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<GrupoFiltroProgramaCritico>(modelo);
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
                _unitOfWork.GrupoFiltroProgramaCriticoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GrupoFiltroProgramaCritico> Add(List<GrupoFiltroProgramaCritico> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.GrupoFiltroProgramaCriticoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<GrupoFiltroProgramaCritico>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GrupoFiltroProgramaCritico> Update(List<GrupoFiltroProgramaCritico> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.GrupoFiltroProgramaCriticoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<GrupoFiltroProgramaCritico>>(modelo);
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
                _unitOfWork.GrupoFiltroProgramaCriticoRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 07/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_GrupoFiltroProgramaCritico para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.GrupoFiltroProgramaCriticoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 07/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros d dee Asesores para  T_GrupoFiltroProgramaCritico.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>

        public List<DatosPersonalAsesorDTO> ObtenerTodoPersonalAsesoresFiltro()
        {
            try
            {
                return _unitOfWork.GrupoFiltroProgramaCriticoRepository.ObtenerTodoPersonalAsesoresFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 07/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda la data para T_GrupoFiltroProgramaCritico.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>

        public List<GrupoFiltroProgramaCriticoDTO> ObtenerTodoGrid()
        {

            try
            {
                return _unitOfWork.GrupoFiltroProgramaCriticoRepository.ObtenerTodoGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 09/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda la data de Sub Area.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public List<PGeneralSubAreaDTO> ObtenerPorIdGrupo(int idGrupo)
        {

            try
            {
                return _unitOfWork.GrupoFiltroProgramaCriticoRepository.ObtenerPorIdGrupo(idGrupo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public GrupoFiltroProgramaCritico Insertar(CompuestoGrupoFiltroProgramaCriticoDTO Json)

        {

            try
            {
                var _repGrupoFiltroProgramaCritico = new GrupoFiltroProgramaCriticoService(_unitOfWork);
                GrupoFiltroProgramaCritico grupoFiltro = new GrupoFiltroProgramaCritico();
                var serGrupoFiltroProgramaCriticoPorAsesorService = new GrupoFiltroProgramaCriticoPorAsesorService(_unitOfWork);
                using (TransactionScope scope = new TransactionScope())
                {
                    grupoFiltro.Nombre = Json.GrupoFiltroProgramaCritico.Nombre;
                    grupoFiltro.Descripcion = Json.GrupoFiltroProgramaCritico.Descripcion;

                    grupoFiltro.Estado = true;
                    grupoFiltro.UsuarioCreacion = Json.Usuario;
                    grupoFiltro.UsuarioModificacion = Json.Usuario;
                    grupoFiltro.FechaCreacion = DateTime.Now;
                    grupoFiltro.FechaModificacion = DateTime.Now;

                    grupoFiltro.GrupoFiltroProgramaCriticoPorAsesor = new List<GrupoFiltroProgramaCriticoPorAsesor>();

                    grupoFiltro = this.Add(grupoFiltro);
                    _unitOfWork.Commit();

                    foreach (var item in Json.GrupoFiltroProgramaCriticoPorAsesor)
                    {
                        GrupoFiltroProgramaCriticoPorAsesor grupoFiltroProgramaCriticoPorAsesor = new GrupoFiltroProgramaCriticoPorAsesor();
                        grupoFiltroProgramaCriticoPorAsesor.IdPersonal = item;
                        grupoFiltroProgramaCriticoPorAsesor.UsuarioCreacion = Json.Usuario;
                        grupoFiltroProgramaCriticoPorAsesor.UsuarioModificacion = Json.Usuario;
                        grupoFiltroProgramaCriticoPorAsesor.FechaCreacion = DateTime.Now;
                        grupoFiltroProgramaCriticoPorAsesor.FechaModificacion = DateTime.Now;
                        grupoFiltroProgramaCriticoPorAsesor.IdGrupoFiltroProgramaCritico = grupoFiltro.Id;
                        grupoFiltroProgramaCriticoPorAsesor.Estado = true;


                        // grupoFiltro.GrupoFiltroProgramaCriticoPorAsesor.Add(grupoFiltroProgramaCriticoPorAsesor);
                        var respuesta = serGrupoFiltroProgramaCriticoPorAsesorService.Add(grupoFiltroProgramaCriticoPorAsesor);
                        _unitOfWork.Commit();
                    }


                    scope.Complete();
                }

                return grupoFiltro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GrupoFiltroProgramaCritico Actualizar(CompuestoGrupoFiltroProgramaCriticoDTO Json)
        {

            try
            {

                var _repGrupoFiltroProgramaCritico = _unitOfWork.GrupoFiltroProgramaCriticoRepository;
                var _repGrupoFiltroProgramaCriticoPorAsesor = _unitOfWork.GrupoFiltroProgramaCriticoPorAsesorRepository;
                //var serGrupoFiltroProgramaCritico = new GrupoFiltroProgramaCriticoService(_unitOfWork);
                var serGrupoFiltroProgramaCriticoPorAsesorService = new GrupoFiltroProgramaCriticoPorAsesorService(_unitOfWork);

                GrupoFiltroProgramaCritico grupoFiltro = new GrupoFiltroProgramaCritico();

                using (TransactionScope scope = new TransactionScope())
                {
                    if (_repGrupoFiltroProgramaCritico.Exist(Json.GrupoFiltroProgramaCritico.Id))
                    {
                        this.EliminacionLogicoPorGrupoFiltro(Json.GrupoFiltroProgramaCritico.Id, Json.Usuario, Json.GrupoFiltroProgramaCriticoPorAsesor);

                        grupoFiltro = _mapper.Map<GrupoFiltroProgramaCritico>(_repGrupoFiltroProgramaCritico.FirstById(Json.GrupoFiltroProgramaCritico.Id));
                        grupoFiltro.Nombre = Json.GrupoFiltroProgramaCritico.Nombre;
                        grupoFiltro.Descripcion = Json.GrupoFiltroProgramaCritico.Descripcion;
                        grupoFiltro.UsuarioModificacion = Json.Usuario;
                        grupoFiltro.FechaModificacion = DateTime.Now;

                        grupoFiltro.GrupoFiltroProgramaCriticoPorAsesor = new List<GrupoFiltroProgramaCriticoPorAsesor>();

                        foreach (var item in Json.GrupoFiltroProgramaCriticoPorAsesor)
                        {
                            GrupoFiltroProgramaCriticoPorAsesor grupoFiltroProgramaCriticoPorAsesor;
                            if (_repGrupoFiltroProgramaCriticoPorAsesor.Exist(x => x.IdGrupoFiltroProgramaCritico == Json.IdGrupo && x.IdPersonal == item))
                            {
                                grupoFiltroProgramaCriticoPorAsesor = _mapper.Map<GrupoFiltroProgramaCriticoPorAsesor>(_repGrupoFiltroProgramaCriticoPorAsesor.FirstBy(x => x.IdPersonal == item && x.IdGrupoFiltroProgramaCritico == Json.IdGrupo));

                                grupoFiltroProgramaCriticoPorAsesor.IdPersonal = item;
                                grupoFiltroProgramaCriticoPorAsesor.UsuarioModificacion = Json.Usuario;
                                grupoFiltroProgramaCriticoPorAsesor.FechaModificacion = DateTime.Now;
                                grupoFiltroProgramaCriticoPorAsesor.IdGrupoFiltroProgramaCritico = Json.IdGrupo;

                                var respuesta = serGrupoFiltroProgramaCriticoPorAsesorService.Update(grupoFiltroProgramaCriticoPorAsesor);
                                _unitOfWork.Commit();
                            }
                            else
                            {
                                grupoFiltroProgramaCriticoPorAsesor = new GrupoFiltroProgramaCriticoPorAsesor();
                                grupoFiltroProgramaCriticoPorAsesor.IdPersonal = item;
                                grupoFiltroProgramaCriticoPorAsesor.UsuarioCreacion = Json.Usuario;
                                grupoFiltroProgramaCriticoPorAsesor.UsuarioModificacion = Json.Usuario;
                                grupoFiltroProgramaCriticoPorAsesor.FechaCreacion = DateTime.Now;
                                grupoFiltroProgramaCriticoPorAsesor.FechaModificacion = DateTime.Now;
                                grupoFiltroProgramaCriticoPorAsesor.IdGrupoFiltroProgramaCritico = Json.IdGrupo;
                                grupoFiltroProgramaCriticoPorAsesor.Estado = true;

                                var respuesta = serGrupoFiltroProgramaCriticoPorAsesorService.Add(grupoFiltroProgramaCriticoPorAsesor);
                                _unitOfWork.Commit();
                            }

                        }
                        grupoFiltro = this.Update(grupoFiltro);
                        _unitOfWork.Commit();
                        scope.Complete();
                    }
                }
                return grupoFiltro;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public void EliminacionLogicoPorGrupoFiltro(int idGrupo, string usuario, List<int> nuevos)
        {
            try
            {
                var GrupoFiltroProgramaCriticoPorAsesorService = new GrupoFiltroProgramaCriticoPorAsesorService(_unitOfWork);
                var _rep = _unitOfWork.GrupoFiltroProgramaCriticoPorAsesorRepository;
                var listaBorrar = _rep.GetBy(x => x.IdGrupoFiltroProgramaCritico == idGrupo && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdPersonal));
                foreach (var item in listaBorrar)
                {
                    GrupoFiltroProgramaCriticoPorAsesorService.Delete(item.Id, usuario);
                    _unitOfWork.Commit();
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Eliminar(CompuestoGrupoFiltroProgramaCriticoDTO Json)
        {

            try
            {
                var _repGrupoFiltroProgramaCriticoPorAsesor = _unitOfWork.GrupoFiltroProgramaCriticoPorAsesorRepository;
                var _repGrupoFiltroProgramaCriticoPgeneral = _unitOfWork.GrupoFiltroProgramaCriticoPgeneralRepository;

                var _repGrupoFiltroProgramaCritico = _unitOfWork.GrupoFiltroProgramaCriticoRepository;




                if (_repGrupoFiltroProgramaCritico.Exist(Json.GrupoFiltroProgramaCritico.Id))
                {
                    var hijosGrupoAsesores = _repGrupoFiltroProgramaCriticoPorAsesor.GetBy(x => x.IdGrupoFiltroProgramaCritico == Json.GrupoFiltroProgramaCritico.Id);
                    var hijosGrupoPgeneral = _repGrupoFiltroProgramaCriticoPgeneral.GetBy(x => x.IdGrupoFiltroProgramaCritico == Json.GrupoFiltroProgramaCritico.Id);

                    foreach (var hijo in hijosGrupoAsesores)
                    {
                        _repGrupoFiltroProgramaCriticoPorAsesor.Delete(hijo.Id, Json.Usuario);
                        _unitOfWork.Commit();
                    }

                    foreach (var hijo in hijosGrupoPgeneral)
                    {
                        _repGrupoFiltroProgramaCriticoPgeneral.Delete(hijo.Id, Json.Usuario);
                        _unitOfWork.Commit();
                    }

                    this.Delete(Json.GrupoFiltroProgramaCritico.Id, Json.Usuario);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        /// Autor: Margiory Ramirez.
        /// Fecha: 08/01/2024
        /// Version: 1.0
        /// <summary>
        /// <summary>
        /// Obtiene el reporte de programas criticos
        /// </summary>
        /// <param name="filtros">Objeto de clase ReporteProgramasCriticosFiltroDTO pasando los parametros necesarios para generar el reporte</param>
        /// <returns>Lista de objetos de clase ReporteEstructuradoAsignacionProgramasCriticosDTO</returns>




        public List<ReporteEstructuradoAsignacionProgramasCriticosDTO> ObtenerReporteProgramasCriticosAsignacion(ReporteProgramasCriticosFiltroDTO filtros)
        {
            try
            {

                var _repProgramaCritico = _unitOfWork.GrupoFiltroProgramaCriticoRepository;
                List<ReporteProgramasCriticosAsignacionDiariaSimplificadoDTO> resultadoCrudo = _repProgramaCritico.ObtenerReporteProgramasCriticosAsignacion(filtros);

                var resultado = resultadoCrudo.GroupBy(g => new
                {
                    g.IdGrupoFiltroProgramaCritico,
                    g.NombreGrupoFiltroProgramaCritico,
                    g.OrdenAsesorGrupo,
                    g.IdPersonal,
                    g.NombrePersonal,
                    g.NombrePaisPersonal,
                    g.AsignacionPais
                }).Select(s => new ReporteEstructuradoAsignacionProgramasCriticosDTO
                {
                    IdGrupoFiltroProgramaCritico = s.Key.IdGrupoFiltroProgramaCritico,
                    NombreGrupoFiltroProgramaCritico = s.Key.NombreGrupoFiltroProgramaCritico,
                    OrdenAsesorGrupo = s.Key.OrdenAsesorGrupo,
                    IdPersonal = s.Key.IdPersonal,
                    NombrePersonal = s.Key.NombrePersonal,
                    NombrePaisPersonal = s.Key.NombrePaisPersonal,
                    AsignacionPais = s.Key.AsignacionPais,
                    Paises = new PaisesReporteProgramasCriticosDTO
                    {
                        CantidadPeru = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal && w.IdCodigoPais == ValorEstatico.IdPaisPeru).Sum(op => op.TotalDatos),
                        CantidadColombia = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal && w.IdCodigoPais == ValorEstatico.IdPaisColombia).Sum(op => op.TotalDatos),
                        CantidadBolivia = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal && w.IdCodigoPais == ValorEstatico.IdPaisBolivia).Sum(op => op.TotalDatos),
                        CantidadChile = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal && w.IdCodigoPais == ValorEstatico.IdPaisChile).Sum(op => op.TotalDatos),
                        CantidadMexico = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal && w.IdCodigoPais == ValorEstatico.IdPaisMexico).Sum(op => op.TotalDatos),
                        CantidadOtros = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal && w.IdCodigoPais != ValorEstatico.IdPaisPeru && w.IdCodigoPais != ValorEstatico.IdPaisColombia && w.IdCodigoPais != ValorEstatico.IdPaisBolivia && w.IdCodigoPais != ValorEstatico.IdPaisMexico && w.IdCodigoPais != ValorEstatico.IdPaisChile).Sum(op => op.TotalDatos)
                    },
                    BNC_MuyAlta = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.BNC_MuyAlta),
                    BNC_Historico = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.BNC_Historico),
                    BNC_AltaMediaRemarketing = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.BNC_AltaMediaRemarketing),
                    BNC_TotalDatos = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.BNC_TotalDatos),
                    RN = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.RN),
                    IT = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.IT),
                    IP = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.IP),
                    PF = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.PF),
                    IC = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.IC),
                    Seguimiento = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.Seguimiento),
                    TotalDatos = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.TotalDatos),
                    IS_M = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.IS_M),
                    IS_M_Acumulado = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal).Sum(op => op.IS_M_Acumulado),
                    CantidadGrupoActual = resultadoCrudo.Where(w => w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal && w.IdGrupoFiltroProgramaCriticoExterno == s.Key.IdGrupoFiltroProgramaCritico).Sum(op => op.TotalDatos),
                    CantidadOtrosGrupos = resultadoCrudo.Where(w => (w.IdPersonal != ValorEstatico.IdPersonalAsignacionAutomatica || w.IdGrupoFiltroProgramaCritico == s.Key.IdGrupoFiltroProgramaCritico) && w.IdPersonal == s.Key.IdPersonal && w.NombrePersonal == s.Key.NombrePersonal && w.IdGrupoFiltroProgramaCriticoExterno != s.Key.IdGrupoFiltroProgramaCritico).Sum(op => op.TotalDatos)
                }).Where(w => w.IdGrupoFiltroProgramaCritico != 0).OrderBy(o => o.IdGrupoFiltroProgramaCritico).ThenBy(tb => tb.OrdenAsesorGrupo).ToList();

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor:08/01/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda la data de Sub Area.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public List<FiltroIdNombreDTO> ObtenerFiltro()
        {

            try
            {
                return _unitOfWork.GrupoFiltroProgramaCriticoRepository.ObtenerFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor:08/01/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda la data de Sub Area.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public List<ReporteProgramasCriticosDTO> ObtenerReporteProgramasCriticos(ReporteProgramasCriticosFiltroDTO filtros)
        {

            try
            {
                return _unitOfWork.GrupoFiltroProgramaCriticoRepository.ObtenerReporteProgramasCriticos(filtros);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }
}


