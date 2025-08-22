using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.CampaniaMailingWhatsapp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Marketing.CampaniaMailingWhatsapp
{
    public class CampaniaMailing : ICampaniaMailing
    {
        //public CampaniaMailing(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        //{
        //}
        //public CampaniaMailingBO(int IdCampaniaMailing)
        //{
        //    _repCampaniaMailing = new CampaniaMailingRepositorio();
        //    var campaniaMailing = _repCampaniaMailing.FirstById(IdCampaniaMailing);
        //    this.Id = campaniaMailing.Id;
        //    this.Nombre = campaniaMailing.Nombre;
        //    this.PrincipalValor = campaniaMailing.PrincipalValor;
        //    this.PrincipalValorTiempo = campaniaMailing.PrincipalValorTiempo;
        //    this.SecundarioValor = campaniaMailing.SecundarioValor;
        //    this.SecundarioValorTiempo = campaniaMailing.SecundarioValorTiempo;
        //    this.ActivaValor = campaniaMailing.ActivaValor;
        //    this.ActivaValorTiempo = campaniaMailing.ActivaValorTiempo;
        //    this.IdParaConjuntoAnuncios = campaniaMailing.IdParaConjuntoAnuncios;
        //    this.IdCategoriaOrigen = campaniaMailing.IdCategoriaOrigen;
        //    this.Estado = campaniaMailing.Estado;
        //    this.UsuarioCreacion = campaniaMailing.UsuarioCreacion;
        //    this.UsuarioModificacion = campaniaMailing.UsuarioModificacion;
        //    this.FechaCreacion = campaniaMailing.FechaCreacion;
        //    this.FechaModificacion = campaniaMailing.FechaModificacion;
        //    this.RowVersion = campaniaMailing.RowVersion;

        //}

        ///// <summary>
        ///// Llena los hijos de campania mailing para insertar
        ///// </summary>
        ///// <param name="campaniaMailing"></param>
        //public void LlenarHijosParaInsertar(CampaniaMailingDTO campaniaMailing)
        //{
        //    try
        //    {
        //        foreach (var item in campaniaMailing.ListaExcluirPorCampaniaMailing)
        //        {
        //            var _new = new CampaniaMailingValorTipoBO
        //            {
        //                IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroCampaniaMailing,
        //                Valor = item.Valor,
        //                Estado = true,
        //                UsuarioCreacion = campaniaMailing.UsuarioModificacion,
        //                UsuarioModificacion = campaniaMailing.UsuarioModificacion,
        //                FechaCreacion = DateTime.Now,
        //                FechaModificacion = DateTime.Now
        //            };
        //            this.ListaCampaniaMailingValorTipo.Add(_new);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        ///// <summary>
        ///// Llena los hijos para actualizar
        ///// </summary>
        ///// <param name="campaniaMailing"></param>
        //public void LlenarHijosParaActualizar(CampaniaMailingDTO campaniaMailing)
        //{
        //    try
        //    {
        //        _repCampaniaMailingValorTipo.EliminacionLogicaPorCampaniaMailing(campaniaMailing);
        //        CampaniaMailingValorTipoBO _new;
        //        foreach (var item in campaniaMailing.ListaExcluirPorCampaniaMailing)
        //        {
        //            if (_repCampaniaMailingValorTipo.Exist(x => x.Valor == item.Valor && x.IdCampaniaMailing == campaniaMailing.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCampaniaMailing))
        //            {
        //                _new = _repCampaniaMailingValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdCampaniaMailing == campaniaMailing.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCampaniaMailing);
        //                _new.UsuarioModificacion = campaniaMailing.UsuarioModificacion;
        //                _new.FechaModificacion = DateTime.Now;
        //            }
        //            else
        //            {
        //                _new = new CampaniaMailingValorTipoBO
        //                {
        //                    IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroCampaniaMailing,
        //                    Valor = item.Valor,
        //                    Estado = true,
        //                    UsuarioCreacion = campaniaMailing.UsuarioModificacion,
        //                    UsuarioModificacion = campaniaMailing.UsuarioModificacion,
        //                    FechaCreacion = DateTime.Now,
        //                    FechaModificacion = DateTime.Now
        //                };
        //            }
        //            this.ListaCampaniaMailingValorTipo.Add(_new);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        ///// <summary>
        ///// Inserta o Actualiza un registro a la tabla T_CampaniaMailing, tambien procesa CampaniaMailingDetalle con sus respectivos programas.
        ///// </summary>
        ///// <param name="campaniaMailingDTO"></param>
        ///// <returns></returns>
        //public void InsertarOActualizar(CampaniaMailingDTO campaniaMailingDTO)
        //{
        //    if (campaniaMailingDTO.Id != 0)
        //    {
        //        var campaniaMailing = _repCampaniaMailing.FirstById(campaniaMailingDTO.Id);

        //        if (campaniaMailing != null)
        //        {
        //            this.Id = campaniaMailing.Id;
        //            this.Nombre = campaniaMailingDTO.Nombre;
        //            this.PrincipalValor = campaniaMailingDTO.PrincipalValor;
        //            this.PrincipalValorTiempo = campaniaMailingDTO.PrincipalValorTiempo;
        //            this.SecundarioValor = campaniaMailingDTO.SecundarioValor;
        //            this.SecundarioValorTiempo = campaniaMailingDTO.SecundarioValorTiempo;
        //            this.ActivaValor = campaniaMailingDTO.ActivaValor;
        //            this.ActivaValorTiempo = campaniaMailingDTO.ActivaValorTiempo;
        //            this.IdParaConjuntoAnuncios = campaniaMailing.IdParaConjuntoAnuncios;
        //            this.IdCategoriaOrigen = campaniaMailingDTO.IdCategoriaOrigen;
        //            this.Estado = campaniaMailing.Estado;
        //            this.FechaCreacion = campaniaMailing.FechaCreacion;
        //            this.FechaModificacion = DateTime.Now;
        //            this.UsuarioModificacion = campaniaMailingDTO.UsuarioModificacion;
        //            this.UsuarioCreacion = campaniaMailing.UsuarioCreacion;
        //            this.IdMigracion = campaniaMailing.IdMigracion;
        //            this.RowVersion = campaniaMailing.RowVersion;
        //            this.FechaInicioExcluirPorEnviadoMismoProgramaGeneralPrincipal = campaniaMailingDTO.FechaInicioExcluirPorEnviadoMismoProgramaGeneralPrincipal;
        //            this.FechaFinExcluirPorEnviadoMismoProgramaGeneralPrincipal = campaniaMailingDTO.FechaFinExcluirPorEnviadoMismoProgramaGeneralPrincipal;
        //        }

        //        List<CampaniaMailingDetalleBO> campaniaMailingDetalleBO = _repCampaniaMailingDetalle.GetBy(x => x.IdCampaniaMailing == campaniaMailingDTO.Id).ToList();
        //        campaniaMailingDetalleBO.RemoveAll(x => campaniaMailingDTO.ListaPrioridades.Any(y => y.Id == x.Id));
        //        if (campaniaMailingDetalleBO != null)
        //        {
        //            _repCampaniaMailingDetalle.Delete(campaniaMailingDetalleBO.Select(x => x.Id), campaniaMailingDTO.UsuarioModificacion);
        //        }

        //        this.LlenarHijosParaActualizar(campaniaMailingDTO);
        //    }
        //    else
        //    {
        //        this.Nombre = campaniaMailingDTO.Nombre;
        //        this.PrincipalValor = campaniaMailingDTO.PrincipalValor;
        //        this.PrincipalValorTiempo = campaniaMailingDTO.PrincipalValorTiempo;
        //        this.SecundarioValor = campaniaMailingDTO.SecundarioValor;
        //        this.SecundarioValorTiempo = campaniaMailingDTO.SecundarioValorTiempo;
        //        this.ActivaValor = campaniaMailingDTO.ActivaValor;
        //        this.ActivaValorTiempo = campaniaMailingDTO.ActivaValorTiempo;
        //        this.IdParaConjuntoAnuncios = 0; //cambiar
        //        this.IdCategoriaOrigen = campaniaMailingDTO.IdCategoriaOrigen;
        //        this.Estado = true;
        //        this.FechaCreacion = DateTime.Now;
        //        this.FechaModificacion = DateTime.Now;
        //        this.UsuarioModificacion = campaniaMailingDTO.UsuarioModificacion;
        //        this.UsuarioCreacion = campaniaMailingDTO.UsuarioModificacion;
        //        this.FechaInicioExcluirPorEnviadoMismoProgramaGeneralPrincipal = campaniaMailingDTO.FechaInicioExcluirPorEnviadoMismoProgramaGeneralPrincipal;
        //        this.FechaFinExcluirPorEnviadoMismoProgramaGeneralPrincipal = campaniaMailingDTO.FechaFinExcluirPorEnviadoMismoProgramaGeneralPrincipal;

        //        this.LlenarHijosParaInsertar(campaniaMailingDTO);
        //    }

        //    if (campaniaMailingDTO.ListaPrioridades != null)
        //    {
        //        this.listaCampaniaMailingDetalleBO = new List<CampaniaMailingDetalleBO>();

        //        foreach (var detalle in campaniaMailingDTO.ListaPrioridades)
        //        {
        //            //detalle.IdCampaniaMailing = this.Id;
        //            if (detalle.Id == 0)
        //            {
        //                ConjuntoAnuncioBO conjuntoAnuncioBO = new ConjuntoAnuncioBO();
        //                conjuntoAnuncioBO.Nombre = detalle.Campania;
        //                conjuntoAnuncioBO.Estado = true;
        //                conjuntoAnuncioBO.FechaCreacion = DateTime.Now;
        //                conjuntoAnuncioBO.FechaModificacion = DateTime.Now;
        //                conjuntoAnuncioBO.FechaCreacionCampania = conjuntoAnuncioBO.FechaCreacion;
        //                conjuntoAnuncioBO.IdCategoriaOrigen = campaniaMailingDTO.IdCategoriaOrigen;
        //                conjuntoAnuncioBO.Origen = "CAMPANIA_MAILING";
        //                conjuntoAnuncioBO.UsuarioCreacion = campaniaMailingDTO.UsuarioModificacion;
        //                conjuntoAnuncioBO.UsuarioModificacion = campaniaMailingDTO.UsuarioModificacion;

        //                _repConjuntoAnuncio.Insert(conjuntoAnuncioBO);

        //                _repConjuntoAnuncio.InsertarConjuntoAnuncioV3(conjuntoAnuncioBO.Id);

        //                detalle.IdConjuntoAnuncio = conjuntoAnuncioBO.Id;

        //                CampaniaMailingDetalleBO campaniaMailingDetalleBO = new CampaniaMailingDetalleBO
        //                {
        //                    IdCampaniaMailing = detalle.IdCampaniaMailing ?? 0,
        //                    Prioridad = detalle.Prioridad,
        //                    Tipo = detalle.Tipo,
        //                    IdRemitenteMailing = detalle.IdRemitenteMailing,
        //                    IdPersonal = detalle.IdPersonal,
        //                    Subject = detalle.Subject,
        //                    FechaEnvio = detalle.FechaEnvio,
        //                    IdHoraEnvio = detalle.IdHoraEnvio,
        //                    Proveedor = detalle.Proveedor,
        //                    EstadoEnvio = detalle.EstadoEnvio,
        //                    IdFiltroSegmento = detalle.IdFiltroSegmento,
        //                    IdPlantilla = detalle.IdPlantilla,
        //                    IdConjuntoAnuncio = detalle.IdConjuntoAnuncio,
        //                    Campania = detalle.Campania,
        //                    CodMailing = detalle.CodMailing,
        //                    CantidadContactos = detalle.CantidadContactos,
        //                    IdConjuntoListaDetalle = detalle.IdConjuntoListaDetalle,
        //                    IdCentroCosto = detalle.IdCentroCosto,
        //                    EsSubidaManual = detalle.EsSubidaManual,
        //                    Estado = true,
        //                    FechaCreacion = DateTime.Now,
        //                    FechaModificacion = DateTime.Now,
        //                    UsuarioCreacion = campaniaMailingDTO.UsuarioModificacion,
        //                    UsuarioModificacion = campaniaMailingDTO.UsuarioModificacion,

        //                    listaCampaniaMailingDetalleProgramaBO = new List<CampaniaMailingDetalleProgramaBO>()
        //                };
        //                int i = 0;
        //                if (detalle.ProgramasPrincipales != null)
        //                {
        //                    foreach (var programa in detalle.ProgramasPrincipales)
        //                    {
        //                        i += 1;
        //                        campaniaMailingDetalleBO.listaCampaniaMailingDetalleProgramaBO.Add(CrearCampaniaMailingDetallePrograma(programa, i, detalle.Id));
        //                    }
        //                }

        //                if (detalle.ProgramasSecundarios != null)
        //                {
        //                    i = 0;
        //                    foreach (var programa in detalle.ProgramasSecundarios)
        //                    {
        //                        i += 1;
        //                        campaniaMailingDetalleBO.listaCampaniaMailingDetalleProgramaBO.Add(CrearCampaniaMailingDetallePrograma(programa, i));
        //                    }
        //                }

        //                if (detalle.ProgramasFiltro != null)
        //                {
        //                    i = 0;
        //                    foreach (var programa in detalle.ProgramasFiltro)
        //                    {
        //                        i += 1;
        //                        campaniaMailingDetalleBO.listaCampaniaMailingDetalleProgramaBO.Add(CrearCampaniaMailingDetallePrograma(programa, i));
        //                    }
        //                }

        //                campaniaMailingDetalleBO.AreaCampaniaMailingDetalle = new List<AreaCampaniaMailingDetalleBO>();
        //                if (detalle.Areas.Count() > 0)
        //                {
        //                    foreach (var item in detalle.Areas)
        //                    {
        //                        AreaCampaniaMailingDetalleBO area = new AreaCampaniaMailingDetalleBO
        //                        {
        //                            IdAreaCapacitacion = item,
        //                            Estado = true,
        //                            FechaCreacion = DateTime.Now,
        //                            FechaModificacion = DateTime.Now,
        //                            UsuarioCreacion = this.UsuarioModificacion,
        //                            UsuarioModificacion = this.UsuarioModificacion
        //                        };
        //                        campaniaMailingDetalleBO.AreaCampaniaMailingDetalle.Add(area);
        //                    }
        //                }

        //                campaniaMailingDetalleBO.SubAreaCampaniaMailingDetalle = new List<SubAreaCampaniaMailingDetalleBO>();
        //                if (detalle.SubAreas.Count() > 0)
        //                {
        //                    foreach (var item in detalle.SubAreas)
        //                    {
        //                        SubAreaCampaniaMailingDetalleBO subArea = new SubAreaCampaniaMailingDetalleBO
        //                        {
        //                            IdSubAreaCapacitacion = item,
        //                            Estado = true,
        //                            FechaCreacion = DateTime.Now,
        //                            FechaModificacion = DateTime.Now,
        //                            UsuarioCreacion = this.UsuarioModificacion,
        //                            UsuarioModificacion = this.UsuarioModificacion
        //                        };
        //                        campaniaMailingDetalleBO.SubAreaCampaniaMailingDetalle.Add(subArea);
        //                    }
        //                }

        //                this.listaCampaniaMailingDetalleBO.Add(campaniaMailingDetalleBO);
        //            }
        //            else
        //            {
        //                //actualizamos la campaña
        //                if (!string.IsNullOrEmpty(detalle.Campania))
        //                {
        //                    ConjuntoAnuncioBO conjuntoAnuncioBO = new ConjuntoAnuncioBO();

        //                    if (detalle.IdConjuntoAnuncio != null)
        //                    {
        //                        var conjuntoAnuncio = _repConjuntoAnuncio.FirstBy(x => x.Id == detalle.IdConjuntoAnuncio);

        //                        conjuntoAnuncio.Nombre = detalle.Campania;
        //                        conjuntoAnuncio.Estado = true;
        //                        conjuntoAnuncio.FechaModificacion = DateTime.Now;
        //                        conjuntoAnuncio.UsuarioModificacion = campaniaMailingDTO.UsuarioModificacion;
        //                        conjuntoAnuncio.IdCategoriaOrigen = campaniaMailingDTO.IdCategoriaOrigen;
        //                        conjuntoAnuncio.Origen = "CAMPANIA_MAILING";
        //                        _repConjuntoAnuncio.Update(conjuntoAnuncio);

        //                        _repConjuntoAnuncio.ActualizarConjuntoAnuncioV3(conjuntoAnuncio.Id);
        //                    }
        //                    else
        //                    {

        //                        conjuntoAnuncioBO.Nombre = detalle.Campania;
        //                        conjuntoAnuncioBO.Estado = true;
        //                        conjuntoAnuncioBO.FechaCreacion = DateTime.Now;
        //                        conjuntoAnuncioBO.FechaModificacion = DateTime.Now;
        //                        conjuntoAnuncioBO.UsuarioCreacion = campaniaMailingDTO.UsuarioModificacion;
        //                        conjuntoAnuncioBO.FechaCreacionCampania = conjuntoAnuncioBO.FechaCreacion;
        //                        conjuntoAnuncioBO.IdCategoriaOrigen = campaniaMailingDTO.IdCategoriaOrigen;
        //                        conjuntoAnuncioBO.Origen = "CAMPANIA_MAILING";
        //                        conjuntoAnuncioBO.UsuarioModificacion = campaniaMailingDTO.UsuarioModificacion;

        //                        _repConjuntoAnuncio.Insert(conjuntoAnuncioBO);
        //                        detalle.IdConjuntoAnuncio = conjuntoAnuncioBO.Id;

        //                        _repConjuntoAnuncio.InsertarConjuntoAnuncioV3(conjuntoAnuncioBO.Id);
        //                    }
        //                }
        //                var campaniaMailingDetalle = _repCampaniaMailingDetalle.FirstById(detalle.Id);

        //                CampaniaMailingDetalleBO campaniaMailingDetalleBO = new CampaniaMailingDetalleBO
        //                {
        //                    Id = campaniaMailingDetalle.Id,
        //                    IdCampaniaMailing = detalle.IdCampaniaMailing ?? 0,
        //                    Prioridad = detalle.Prioridad,
        //                    Tipo = detalle.Tipo,
        //                    IdRemitenteMailing = detalle.IdRemitenteMailing,
        //                    IdPersonal = detalle.IdPersonal,
        //                    Subject = detalle.Subject,
        //                    FechaEnvio = detalle.FechaEnvio,
        //                    IdHoraEnvio = detalle.IdHoraEnvio,
        //                    Proveedor = detalle.Proveedor,
        //                    EstadoEnvio = detalle.EstadoEnvio,
        //                    IdFiltroSegmento = detalle.IdFiltroSegmento,
        //                    IdPlantilla = detalle.IdPlantilla,
        //                    IdConjuntoAnuncio = detalle.IdConjuntoAnuncio,
        //                    Campania = detalle.Campania,
        //                    CodMailing = detalle.CodMailing,
        //                    CantidadContactos = detalle.CantidadContactos,
        //                    IdCentroCosto = detalle.IdCentroCosto,
        //                    EsSubidaManual = detalle.EsSubidaManual,
        //                    Estado = campaniaMailingDetalle.Estado,
        //                    FechaCreacion = campaniaMailingDetalle.FechaCreacion,
        //                    FechaModificacion = DateTime.Now,
        //                    UsuarioCreacion = campaniaMailingDetalle.UsuarioCreacion,
        //                    UsuarioModificacion = campaniaMailingDTO.UsuarioModificacion,
        //                    IdMigracion = campaniaMailingDetalle.IdMigracion,
        //                    RowVersion = campaniaMailingDetalle.RowVersion
        //                };

        //                List<CampaniaMailingDetalleProgramaBO> listaCampaniaMailingDetallePrograma = _repCampaniaMailingDetallePrograma.GetBy(x => x.IdCampaniaMailingDetalle == detalle.Id).ToList();
        //                foreach (var campaniaMailingDetallePrograma in listaCampaniaMailingDetallePrograma)
        //                {
        //                    _repCampaniaMailingDetallePrograma.Delete(campaniaMailingDetallePrograma.Id, campaniaMailingDTO.UsuarioModificacion);
        //                }
        //                _repAreaCampaniaMailingDetalle.EliminacionLogicoPorCampaniaMailing(detalle.Id, campaniaMailingDTO.UsuarioModificacion, detalle.Areas);
        //                _repSubAreaCampaniaMailingDetalle.EliminacionLogicoPorCampaniaMailing(detalle.Id, campaniaMailingDTO.UsuarioModificacion, detalle.SubAreas);

        //                campaniaMailingDetalleBO.listaCampaniaMailingDetalleProgramaBO = new List<CampaniaMailingDetalleProgramaBO>();

        //                int i = 0;
        //                if (detalle.ProgramasPrincipales != null)
        //                {
        //                    foreach (var programa in detalle.ProgramasPrincipales)
        //                    {
        //                        i += 1;
        //                        campaniaMailingDetalleBO.listaCampaniaMailingDetalleProgramaBO.Add(CrearCampaniaMailingDetallePrograma(programa, i, detalle.Id));
        //                    }
        //                }
        //                if (detalle.ProgramasSecundarios != null)
        //                {
        //                    i = 0;
        //                    foreach (var programa in detalle.ProgramasSecundarios)
        //                    {
        //                        i += 1;
        //                        campaniaMailingDetalleBO.listaCampaniaMailingDetalleProgramaBO.Add(CrearCampaniaMailingDetallePrograma(programa, i, detalle.Id));
        //                    }
        //                }
        //                if (detalle.ProgramasFiltro != null)
        //                {
        //                    i = 0;
        //                    foreach (var programa in detalle.ProgramasFiltro)
        //                    {
        //                        i += 1;
        //                        campaniaMailingDetalleBO.listaCampaniaMailingDetalleProgramaBO.Add(CrearCampaniaMailingDetallePrograma(programa, i));
        //                    }
        //                }
        //                campaniaMailingDetalleBO.AreaCampaniaMailingDetalle = new List<AreaCampaniaMailingDetalleBO>();
        //                foreach (var item in detalle.Areas)
        //                {
        //                    AreaCampaniaMailingDetalleBO area;
        //                    if (_repAreaCampaniaMailingDetalle.Exist(x => x.IdAreaCapacitacion == item && x.IdCampaniaMailingDetalle == detalle.Id))
        //                    {
        //                        area = _repAreaCampaniaMailingDetalle.FirstBy(x => x.IdAreaCapacitacion == item && x.IdCampaniaMailingDetalle == detalle.Id);
        //                        area.IdAreaCapacitacion = item;
        //                        area.UsuarioModificacion = campaniaMailingDTO.UsuarioModificacion;
        //                        area.FechaModificacion = DateTime.Now;

        //                    }
        //                    else
        //                    {
        //                        area = new AreaCampaniaMailingDetalleBO
        //                        {
        //                            IdAreaCapacitacion = item,
        //                            UsuarioCreacion = campaniaMailingDTO.UsuarioModificacion,
        //                            UsuarioModificacion = campaniaMailingDTO.UsuarioModificacion,
        //                            FechaCreacion = DateTime.Now,
        //                            FechaModificacion = DateTime.Now,
        //                            Estado = true
        //                        };
        //                    }

        //                    campaniaMailingDetalleBO.AreaCampaniaMailingDetalle.Add(area);
        //                }
        //                campaniaMailingDetalleBO.SubAreaCampaniaMailingDetalle = new List<SubAreaCampaniaMailingDetalleBO>();
        //                foreach (var item in detalle.SubAreas)
        //                {
        //                    SubAreaCampaniaMailingDetalleBO subArea;
        //                    if (_repSubAreaCampaniaMailingDetalle.Exist(x => x.IdSubAreaCapacitacion == item && x.IdCampaniaMailingDetalle == detalle.Id))
        //                    {
        //                        subArea = _repSubAreaCampaniaMailingDetalle.FirstBy(x => x.IdSubAreaCapacitacion == item && x.IdCampaniaMailingDetalle == detalle.Id);
        //                        subArea.IdSubAreaCapacitacion = item;
        //                        subArea.UsuarioModificacion = campaniaMailingDTO.UsuarioModificacion;
        //                        subArea.FechaModificacion = DateTime.Now;

        //                    }
        //                    else
        //                    {
        //                        subArea = new SubAreaCampaniaMailingDetalleBO
        //                        {
        //                            IdSubAreaCapacitacion = item,
        //                            UsuarioCreacion = campaniaMailingDTO.UsuarioModificacion,
        //                            UsuarioModificacion = campaniaMailingDTO.UsuarioModificacion,
        //                            FechaCreacion = DateTime.Now,
        //                            FechaModificacion = DateTime.Now,
        //                            Estado = true
        //                        };
        //                    }

        //                    campaniaMailingDetalleBO.SubAreaCampaniaMailingDetalle.Add(subArea);
        //                }
        //                this.listaCampaniaMailingDetalleBO.Add(campaniaMailingDetalleBO);

        //                //Nuevo filtro

        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// Genera lista de CampaniaMailingDetallePrograma
        ///// </summary>
        ///// <param name="listaCampaniaMailingDetallePrograma"></param>
        ///// <param name="IdCampaniaMailingDetalle"></param>
        //public void InsertarListaCampaniaMailingDetallePrograma(CampaniaMailingDetalleBO campaniaMailingDetalleBO, List<CampaniaMailingDetalleProgramaDTO> listaCampaniaMailingDetallePrograma, int idCampaniaMailingDetalle = 0)
        //{
        //    campaniaMailingDetalleBO.listaCampaniaMailingDetalleProgramaBO = new List<CampaniaMailingDetalleProgramaBO>();
        //    int i = 0;
        //    foreach (var programa in listaCampaniaMailingDetallePrograma)
        //    {
        //        i = i + 1;

        //        CampaniaMailingDetalleProgramaBO campaniaMailingDetallePrograma = new CampaniaMailingDetalleProgramaBO();
        //        if (idCampaniaMailingDetalle != 0) campaniaMailingDetallePrograma.IdCampaniaMailingDetalle = idCampaniaMailingDetalle;
        //        campaniaMailingDetallePrograma.IdPgeneral = programa.IdPgeneral;
        //        campaniaMailingDetallePrograma.Nombre = programa.Nombre;
        //        campaniaMailingDetallePrograma.Tipo = programa.Tipo;
        //        campaniaMailingDetallePrograma.Orden = i;
        //        campaniaMailingDetallePrograma.FechaCreacion = DateTime.Now;
        //        campaniaMailingDetallePrograma.FechaModificacion = DateTime.Now;
        //        campaniaMailingDetallePrograma.UsuarioCreacion = this.UsuarioModificacion;
        //        campaniaMailingDetallePrograma.UsuarioModificacion = this.UsuarioModificacion;

        //        campaniaMailingDetalleBO.listaCampaniaMailingDetalleProgramaBO.Add(campaniaMailingDetallePrograma);
        //    }
        //}

        ///// <summary>
        ///// Genera lista de CampaniaMailingDetallePrograma
        ///// </summary>
        ///// <param name="listaCampaniaMailingDetallePrograma"></param>
        ///// <param name="IdCampaniaMailingDetalle"></param>
        //public CampaniaMailingDetalleProgramaBO CrearCampaniaMailingDetallePrograma(CampaniaMailingDetalleProgramaDTO listaCampaniaMailingDetallePrograma, int i, int idCampaniaMailingDetalle = 0)
        //{
        //    CampaniaMailingDetalleProgramaBO campaniaMailingDetallePrograma = new CampaniaMailingDetalleProgramaBO();
        //    if (idCampaniaMailingDetalle != 0) campaniaMailingDetallePrograma.IdCampaniaMailingDetalle = idCampaniaMailingDetalle;
        //    campaniaMailingDetallePrograma.IdPgeneral = listaCampaniaMailingDetallePrograma.IdPgeneral;
        //    campaniaMailingDetallePrograma.Nombre = listaCampaniaMailingDetallePrograma.Nombre;
        //    campaniaMailingDetallePrograma.Tipo = listaCampaniaMailingDetallePrograma.Tipo;
        //    campaniaMailingDetallePrograma.Orden = i;
        //    campaniaMailingDetallePrograma.Estado = true;
        //    campaniaMailingDetallePrograma.FechaCreacion = DateTime.Now;
        //    campaniaMailingDetallePrograma.FechaModificacion = DateTime.Now;
        //    campaniaMailingDetallePrograma.UsuarioCreacion = this.UsuarioModificacion;
        //    campaniaMailingDetallePrograma.UsuarioModificacion = this.UsuarioModificacion;
        //    return campaniaMailingDetallePrograma;
        //}

        ///// <summary>
        ///// Obtiene los detalle de una campaña
        ///// </summary>
        ///// <returns>Objeto de clase CampaniaMailingDTO</returns>
        //public CampaniaMailingDTO ObtenerDetalle()
        //{
        //    try
        //    {
        //        var campaniaMailing = _repCampaniaMailing.Obtener(this.Id);
        //        campaniaMailing.ListaPrioridades = _repCampaniaMailingDetalle.ObtenerListaCampaniaMailingDetalleConProgramas(this.Id);
        //        var lista = _repCampaniaMailingValorTipo.ObtenerPorIdCampaniaMailing(this.Id);
        //        campaniaMailing.ListaExcluirPorCampaniaMailing = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCampaniaMailing).ToList();
        //        return campaniaMailing;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}

        ///// <summary>
        ///// Estructura el reporte de campania Mailchimp general
        ///// </summary>
        ///// <param name="fechaInicio">Fecha de inicio para la busqueda</param>
        ///// <param name="fechaFin">Fecha de fin para la busqueda</param>
        ///// <returns>Lista de objetos de clase ReporteCampaniaMailchimpMetricaDTO</returns>
        //public List<ReporteCampaniaMailchimpMetricaDTO> EstructurarReporteCampaniaMailChimpGeneral(DateTime fechaInicio, DateTime fechaFin)
        //{
        //    try
        //    {
        //        List<ReporteCampaniaMailchimpMetricaDTO> listaReporteAnuncioMailChimpGeneral = _repCampaniaMailing.ObtenerReporteCampaniaMailing(fechaInicio, fechaFin);

        //        var listaReporteAgrupado = listaReporteAnuncioMailChimpGeneral
        //            .GroupBy(g => new
        //            {
        //                g.IdCampaniaMailing,
        //                g.NombreCampaniaMailing,
        //                g.VersionMailing,
        //                g.FechaEnvioCampaniaMailing,
        //                g.CantidadEnviadosMailChimp,
        //                g.CantidadEntregaExitosaMailChimp,
        //                g.CantidadAperturaUnica,
        //                g.CantidadReboteDuro,
        //                g.CantidadReboteSuave,
        //                g.CantidadReboteSintaxis,
        //                g.CantidadTotalRebotes,
        //                g.CantidadClicUnico,
        //                g.CantidadTotalClic,
        //                g.CantidadRegistros,
        //                g.CantidadOportunidades,
        //                g.CantidadMailingAsesores,
        //                g.CantidadTotalOportunidades
        //            }).Select(s => new ReporteCampaniaMailchimpMetricaDTO
        //            {
        //                IdCampaniaMailing = s.Key.IdCampaniaMailing,
        //                NombreCampaniaMailing = s.Key.NombreCampaniaMailing,
        //                VersionMailing = s.Key.VersionMailing,
        //                FechaEnvioCampaniaMailing = s.Key.FechaEnvioCampaniaMailing,
        //                CantidadEnviadosMailChimp = s.Key.CantidadEnviadosMailChimp,
        //                CantidadEntregaExitosaMailChimp = s.Key.CantidadEntregaExitosaMailChimp,
        //                CantidadAperturaUnica = s.Key.CantidadAperturaUnica,
        //                CantidadReboteDuro = s.Key.CantidadReboteDuro,
        //                CantidadReboteSuave = s.Key.CantidadReboteSuave,
        //                CantidadReboteSintaxis = s.Key.CantidadReboteSintaxis,
        //                CantidadTotalRebotes = s.Key.CantidadTotalRebotes,
        //                CantidadClicUnico = s.Key.CantidadClicUnico,
        //                CantidadTotalClic = s.Key.CantidadTotalClic,
        //                CantidadCorreoAbiertoMismoDia = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date == w.FechaEnvioCampaniaMailing.Date).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadCorreoAbiertoDosTresDias = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date > w.FechaEnvioCampaniaMailing.Date && w.FechaConsulta.Value.Date <= w.FechaEnvioCampaniaMailing.Date.AddDays(2)).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadCorreoAbiertoCuatroSieteDias = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date >= w.FechaEnvioCampaniaMailing.Date.AddDays(3) && w.FechaConsulta.Value.Date <= w.FechaEnvioCampaniaMailing.Date.AddDays(6)).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadCorreoAbiertoOchoCatorceDias = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date >= w.FechaEnvioCampaniaMailing.Date.AddDays(7) && w.FechaConsulta.Value.Date <= w.FechaEnvioCampaniaMailing.Date.AddDays(13)).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadCorreoAbiertoQuinceTreintaDias = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date >= w.FechaEnvioCampaniaMailing.Date.AddDays(14) && w.FechaConsulta.Value.Date <= w.FechaEnvioCampaniaMailing.Date.AddDays(29)).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadCorreoAbiertoTreintaUnoNoventaDias = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date >= w.FechaEnvioCampaniaMailing.Date.AddDays(30) && w.FechaConsulta.Value.Date <= w.FechaEnvioCampaniaMailing.Date.AddDays(89)).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadCorreoAbiertoNoventaMedioAnioDias = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date >= w.FechaEnvioCampaniaMailing.Date.AddDays(90) && w.FechaConsulta.Value.Date <= w.FechaEnvioCampaniaMailing.Date.AddDays(179)).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadRegistros = s.Key.CantidadRegistros,
        //                CantidadOportunidades = s.Key.CantidadOportunidades,
        //                CantidadMailingAsesores = s.Key.CantidadMailingAsesores,
        //                CantidadTotalOportunidades = s.Key.CantidadTotalOportunidades
        //            }).ToList();

        //        listaReporteAgrupado = listaReporteAgrupado
        //            .GroupBy(g => new
        //            {
        //                g.IdCampaniaMailing,
        //                g.NombreCampaniaMailing,
        //                g.VersionMailing
        //            }).Select(s => new ReporteCampaniaMailchimpMetricaDTO
        //            {
        //                IdCampaniaMailing = s.Key.IdCampaniaMailing,
        //                NombreCampaniaMailing = s.Key.NombreCampaniaMailing,
        //                VersionMailing = s.Key.VersionMailing,
        //                FechaEnvioCampaniaMailing = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Min(op => op.FechaEnvioCampaniaMailing),
        //                CantidadEnviadosMailChimp = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadEnviadosMailChimp),
        //                CantidadEntregaExitosaMailChimp = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadEntregaExitosaMailChimp),
        //                CantidadAperturaUnica = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadAperturaUnica),
        //                TasaAperturaUnica = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadEntregaExitosaMailChimp) == 0 ? s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadAperturaUnica) : s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadAperturaUnica) * 1.0 / s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadEntregaExitosaMailChimp) * 1.0,
        //                TasaRebote = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadEntregaExitosaMailChimp) == 0 ? s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadTotalRebotes) : s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadTotalRebotes) * 1.0 / s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadEntregaExitosaMailChimp) * 1.0,
        //                TasaClic = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadEntregaExitosaMailChimp) == 0 ? s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadClicUnico) : s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadClicUnico) * 1.0 / s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadEntregaExitosaMailChimp) * 1.0,
        //                CantidadReboteDuro = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadReboteDuro),
        //                CantidadReboteSuave = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadReboteSuave),
        //                CantidadReboteSintaxis = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadReboteSintaxis),
        //                CantidadTotalRebotes = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadTotalRebotes),
        //                CantidadClicUnico = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadClicUnico),
        //                CantidadTotalClic = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadTotalClic),
        //                CantidadCorreoAbiertoMismoDia = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCorreoAbiertoMismoDia),
        //                CantidadCorreoAbiertoDosTresDias = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCorreoAbiertoDosTresDias),
        //                CantidadCorreoAbiertoCuatroSieteDias = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCorreoAbiertoCuatroSieteDias),
        //                CantidadCorreoAbiertoOchoCatorceDias = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCorreoAbiertoOchoCatorceDias),
        //                CantidadCorreoAbiertoQuinceTreintaDias = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCorreoAbiertoQuinceTreintaDias),
        //                CantidadCorreoAbiertoTreintaUnoNoventaDias = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCorreoAbiertoTreintaUnoNoventaDias),
        //                CantidadCorreoAbiertoNoventaMedioAnioDias = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCorreoAbiertoNoventaMedioAnioDias),
        //                CantidadRegistros = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadRegistros),
        //                CantidadOportunidades = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadOportunidades),
        //                CantidadMailingAsesores = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadMailingAsesores),
        //                CantidadTotalOportunidades = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadTotalOportunidades)
        //            }).ToList();

        //        return listaReporteAgrupado;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Estructura el reporte de campania Mailchimp general para exportacion
        ///// </summary>
        ///// <param name="fechaInicio">Fecha de inicio para la busqueda</param>
        ///// <param name="fechaFin">Fecha de fin para la busqueda</param>
        ///// <returns>Lista de objetos de clase ReporteCampaniaMailchimpMetricaDTO</returns>
        //public List<ReporteCampaniaMailchimpMetricaDTO> EstructurarReporteCampaniaMailChimpGeneralExportacion(DateTime fechaInicio, DateTime fechaFin)
        //{
        //    try
        //    {
        //        List<ReporteCampaniaMailchimpMetricaDTO> listaReporteAnuncioMailChimpGeneral = _repCampaniaMailing.ObtenerReporteCampaniaMailing(fechaInicio, fechaFin);

        //        var listaReporteAgrupado = listaReporteAnuncioMailChimpGeneral
        //            .GroupBy(g => new
        //            {
        //                g.IdCampaniaMailing,
        //                g.NombreCampaniaMailing,
        //                g.IdCampaniaMailingDetalle,
        //                g.NombreCampaniaMailingDetalle,
        //                g.VersionMailing,
        //                g.FechaEnvioCampaniaMailing,
        //                g.CantidadEnviadosMailChimp,
        //                g.CantidadEntregaExitosaMailChimp,
        //                g.CantidadAperturaUnica,
        //                g.CantidadReboteDuro,
        //                g.CantidadReboteSuave,
        //                g.CantidadReboteSintaxis,
        //                g.CantidadTotalRebotes,
        //                g.CantidadClicUnico,
        //                g.CantidadTotalClic,
        //                g.CantidadRegistros,
        //                g.CantidadOportunidades,
        //                g.CantidadMailingAsesores,
        //                g.CantidadTotalOportunidades
        //            }).Select(s => new ReporteCampaniaMailchimpMetricaDTO
        //            {
        //                IdCampaniaMailing = s.Key.IdCampaniaMailing,
        //                NombreCampaniaMailing = s.Key.NombreCampaniaMailing,
        //                IdCampaniaMailingDetalle = s.Key.IdCampaniaMailingDetalle,
        //                NombreCampaniaMailingDetalle = s.Key.NombreCampaniaMailingDetalle,
        //                VersionMailing = s.Key.VersionMailing,
        //                FechaEnvioCampaniaMailing = s.Key.FechaEnvioCampaniaMailing,
        //                CantidadEnviadosMailChimp = s.Key.CantidadEnviadosMailChimp,
        //                CantidadEntregaExitosaMailChimp = s.Key.CantidadEntregaExitosaMailChimp,
        //                CantidadAperturaUnica = s.Key.CantidadAperturaUnica,
        //                CantidadReboteDuro = s.Key.CantidadReboteDuro,
        //                CantidadReboteSuave = s.Key.CantidadReboteSuave,
        //                CantidadReboteSintaxis = s.Key.CantidadReboteSintaxis,
        //                CantidadTotalRebotes = s.Key.CantidadTotalRebotes,
        //                CantidadClicUnico = s.Key.CantidadClicUnico,
        //                CantidadTotalClic = s.Key.CantidadTotalClic,
        //                CantidadCorreoAbiertoMismoDia = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date == w.FechaEnvioCampaniaMailing.Date).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadCorreoAbiertoDosTresDias = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date > w.FechaEnvioCampaniaMailing.Date && w.FechaConsulta.Value.Date <= w.FechaEnvioCampaniaMailing.Date.AddDays(2)).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadCorreoAbiertoCuatroSieteDias = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date >= w.FechaEnvioCampaniaMailing.Date.AddDays(3) && w.FechaConsulta.Value.Date <= w.FechaEnvioCampaniaMailing.Date.AddDays(6)).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadCorreoAbiertoOchoCatorceDias = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date >= w.FechaEnvioCampaniaMailing.Date.AddDays(7) && w.FechaConsulta.Value.Date <= w.FechaEnvioCampaniaMailing.Date.AddDays(13)).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadCorreoAbiertoQuinceTreintaDias = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date >= w.FechaEnvioCampaniaMailing.Date.AddDays(14) && w.FechaConsulta.Value.Date <= w.FechaEnvioCampaniaMailing.Date.AddDays(29)).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadCorreoAbiertoTreintaUnoNoventaDias = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date >= w.FechaEnvioCampaniaMailing.Date.AddDays(30) && w.FechaConsulta.Value.Date <= w.FechaEnvioCampaniaMailing.Date.AddDays(89)).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadCorreoAbiertoNoventaMedioAnioDias = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date >= w.FechaEnvioCampaniaMailing.Date.AddDays(90) && w.FechaConsulta.Value.Date <= w.FechaEnvioCampaniaMailing.Date.AddDays(179)).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadRegistros = s.Key.CantidadRegistros,
        //                CantidadOportunidades = s.Key.CantidadOportunidades,
        //                CantidadMailingAsesores = s.Key.CantidadMailingAsesores,
        //                CantidadTotalOportunidades = s.Key.CantidadTotalOportunidades
        //            }).ToList();

        //        return listaReporteAgrupado;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Estructura el reporte del detalle de campania Mailchimp registros
        ///// </summary>
        ///// <param name="idCampaniaMailing">Id de la campania general o campania mailing</param>
        ///// <param name="versionMailing">Flag para determinar si es del nuevo modulo de mailing</param>
        ///// <param name="fechaInicio">Fecha de inicio para la busqueda</param>
        ///// <param name="fechaFin">Fecha de fin para la busqueda</param>
        ///// <returns>Lista de objetos de clase ReporteCampaniaMailchimpMetricaRegistrosDTO</returns>
        //public List<ReporteCampaniaMailchimpMetricaDTO> EstructurarReporteDetalleCampaniaMailChimpGeneral(int idCampaniaMailing, bool versionMailing, DateTime fechaInicio, DateTime fechaFin)
        //{
        //    try
        //    {
        //        List<ReporteCampaniaMailchimpMetricaDTO> listaReporteAnuncioMailChimpGeneralDetalle = _repCampaniaMailing.ObtenerReporteCampaniaMailingDetalle(idCampaniaMailing, versionMailing, fechaInicio, fechaFin);

        //        var listaReporteAgrupado = listaReporteAnuncioMailChimpGeneralDetalle
        //            .GroupBy(g => new
        //            {
        //                g.IdCampaniaMailingDetalle,
        //                g.NombreCampaniaMailingDetalle,
        //                g.VersionMailing,
        //                g.FechaEnvioCampaniaMailing,
        //                g.CantidadEnviadosMailChimp,
        //                g.CantidadEntregaExitosaMailChimp,
        //                g.CantidadAperturaUnica,
        //                g.CantidadReboteDuro,
        //                g.CantidadReboteSuave,
        //                g.CantidadReboteSintaxis,
        //                g.CantidadTotalRebotes,
        //                g.CantidadClicUnico,
        //                g.CantidadTotalClic,
        //                g.CantidadRegistros,
        //                g.CantidadOportunidades,
        //                g.CantidadMailingAsesores,
        //                g.CantidadTotalOportunidades
        //            }).Select(s => new ReporteCampaniaMailchimpMetricaDTO
        //            {
        //                IdCampaniaMailingDetalle = s.Key.IdCampaniaMailingDetalle,
        //                NombreCampaniaMailingDetalle = s.Key.NombreCampaniaMailingDetalle,
        //                VersionMailing = s.Key.VersionMailing,
        //                FechaEnvioCampaniaMailing = s.Key.FechaEnvioCampaniaMailing,
        //                CantidadEnviadosMailChimp = s.Key.CantidadEnviadosMailChimp,
        //                CantidadEntregaExitosaMailChimp = s.Key.CantidadEntregaExitosaMailChimp,
        //                CantidadAperturaUnica = s.Key.CantidadAperturaUnica,
        //                CantidadReboteDuro = s.Key.CantidadReboteDuro,
        //                CantidadReboteSuave = s.Key.CantidadReboteSuave,
        //                CantidadReboteSintaxis = s.Key.CantidadReboteSintaxis,
        //                CantidadTotalRebotes = s.Key.CantidadTotalRebotes,
        //                CantidadClicUnico = s.Key.CantidadClicUnico,
        //                CantidadTotalClic = s.Key.CantidadTotalClic,
        //                CantidadCorreoAbiertoMismoDia = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date == w.FechaEnvioCampaniaMailing.Date).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadCorreoAbiertoDosTresDias = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date > w.FechaEnvioCampaniaMailing.Date && w.FechaConsulta.Value.Date <= w.FechaEnvioCampaniaMailing.Date.AddDays(2)).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadCorreoAbiertoCuatroSieteDias = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date >= w.FechaEnvioCampaniaMailing.Date.AddDays(3) && w.FechaConsulta.Value.Date <= w.FechaEnvioCampaniaMailing.Date.AddDays(6)).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadCorreoAbiertoOchoCatorceDias = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date >= w.FechaEnvioCampaniaMailing.Date.AddDays(7) && w.FechaConsulta.Value.Date <= w.FechaEnvioCampaniaMailing.Date.AddDays(13)).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadCorreoAbiertoQuinceTreintaDias = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date >= w.FechaEnvioCampaniaMailing.Date.AddDays(14) && w.FechaConsulta.Value.Date <= w.FechaEnvioCampaniaMailing.Date.AddDays(29)).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadCorreoAbiertoTreintaUnoNoventaDias = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date >= w.FechaEnvioCampaniaMailing.Date.AddDays(30) && w.FechaConsulta.Value.Date <= w.FechaEnvioCampaniaMailing.Date.AddDays(89)).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadCorreoAbiertoNoventaMedioAnioDias = s.Where(w => w.FechaConsulta.HasValue && w.FechaConsulta.Value.Date >= w.FechaEnvioCampaniaMailing.Date.AddDays(90) && w.FechaConsulta.Value.Date <= w.FechaEnvioCampaniaMailing.Date.AddDays(179)).Sum(op => op.CantidadCorreoAbierto),
        //                CantidadRegistros = s.Key.CantidadRegistros,
        //                CantidadOportunidades = s.Key.CantidadOportunidades,
        //                CantidadMailingAsesores = s.Key.CantidadMailingAsesores,
        //                CantidadTotalOportunidades = s.Key.CantidadTotalOportunidades
        //            }).OrderByDescending(o => o.FechaEnvioCampaniaMailing).ToList();

        //        return listaReporteAgrupado;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Estructura el reporte de campania Mailchimp registros
        ///// </summary>
        ///// <param name="fechaInicio">Fecha de inicio para la busqueda</param>
        ///// <param name="fechaFin">Fecha de fin para la busqueda</param>
        ///// <returns>Lista de objetos de clase ReporteCampaniaMailchimpMetricaRegistrosDTO</returns>
        //public List<ReporteCampaniaMailchimpMetricaRegistrosDTO> EstructurarReporteCampaniaMailChimpRegistros(DateTime fechaInicio, DateTime fechaFin)
        //{
        //    try
        //    {
        //        List<ReporteCampaniaMailchimpMetricaRegistrosDTO> listaReporteAnuncioMailChimpRegistros = _repCampaniaMailing.ObtenerReporteCampaniaMailingRegistros(fechaInicio, fechaFin);

        //        var listaReporteAgrupado = listaReporteAnuncioMailChimpRegistros
        //            .GroupBy(g => new
        //            {
        //                g.IdCampaniaMailing,
        //                g.NombreCampaniaMailing,
        //                g.VersionMailing,
        //                g.FechaEnvioCampaniaMailing
        //            }).Select(s => new ReporteCampaniaMailchimpMetricaRegistrosDTO
        //            {
        //                IdCampaniaMailing = s.Key.IdCampaniaMailing,
        //                NombreCampaniaMailing = s.Key.NombreCampaniaMailing,
        //                FechaEnvioCampaniaMailing = s.Key.FechaEnvioCampaniaMailing,
        //                VersionMailing = s.Key.VersionMailing,
        //                CantidadRegistrosMailing = s.Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaOrigenChat = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasChat).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaOrigenChatOffline = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasChatOffline).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaAccesoPrueba = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioAccesoPrueba).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioCarrera = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioCarrera).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioContactenos = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioContactenos).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioPago = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioPago).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioPrograma = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioPrograma).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioPropio = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioPropio).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioRegistrarse = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioRegistrarse).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaOrigenInteligenteChat = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteChat).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaOrigenInteligenteChatOffline = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteChatOffline).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaInteligenteAccesoPrueba = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasIntFormularioAccesoPrueba).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioInteligenteCarrera = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioCarrera).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioInteligenteContactenos = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasIntFormularioContactenos).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioInteligentePago = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioPago).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioInteligentePrograma = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioPrograma).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioInteligentePropio = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioPropio).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioInteligenteRegistrarse = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasIntFormularioRegistrarse).Sum(op => op.CantidadRegistrosMailing),
        //            }).ToList();

        //        listaReporteAgrupado = listaReporteAgrupado
        //            .GroupBy(g => new
        //            {
        //                g.IdCampaniaMailing,
        //                g.NombreCampaniaMailing,
        //                g.VersionMailing
        //            }).Select(s => new ReporteCampaniaMailchimpMetricaRegistrosDTO
        //            {
        //                IdCampaniaMailing = s.Key.IdCampaniaMailing,
        //                NombreCampaniaMailing = s.Key.NombreCampaniaMailing,
        //                FechaEnvioCampaniaMailing = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Min(op => op.FechaEnvioCampaniaMailing),
        //                VersionMailing = s.Key.VersionMailing,
        //                CantidadRegistrosMailing = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaOrigenChat = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaOrigenChat),
        //                CantidadCategoriaOrigenChatOffline = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaOrigenChatOffline),
        //                CantidadCategoriaAccesoPrueba = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaAccesoPrueba),
        //                CantidadCategoriaFormularioCarrera = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioCarrera),
        //                CantidadCategoriaFormularioContactenos = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioContactenos),
        //                CantidadCategoriaFormularioPago = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioPago),
        //                CantidadCategoriaFormularioPrograma = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioPrograma),
        //                CantidadCategoriaFormularioPropio = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioPropio),
        //                CantidadCategoriaFormularioRegistrarse = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioRegistrarse),
        //                CantidadCategoriaOrigenInteligenteChat = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaOrigenInteligenteChat),
        //                CantidadCategoriaOrigenInteligenteChatOffline = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaOrigenInteligenteChatOffline),
        //                CantidadCategoriaInteligenteAccesoPrueba = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaInteligenteAccesoPrueba),
        //                CantidadCategoriaFormularioInteligenteCarrera = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioInteligenteCarrera),
        //                CantidadCategoriaFormularioInteligenteContactenos = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioInteligenteContactenos),
        //                CantidadCategoriaFormularioInteligentePago = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioInteligentePago),
        //                CantidadCategoriaFormularioInteligentePrograma = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioInteligentePrograma),
        //                CantidadCategoriaFormularioInteligentePropio = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioInteligentePropio),
        //                CantidadCategoriaFormularioInteligenteRegistrarse = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioInteligenteRegistrarse),
        //            }).ToList();

        //        return listaReporteAgrupado;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Estructura el reporte de campania Mailchimp registros para exportacion
        ///// </summary>
        ///// <param name="fechaInicio">Fecha de inicio para la busqueda</param>
        ///// <param name="fechaFin">Fecha de fin para la busqueda</param>
        ///// <returns>Lista de objetos de clase ReporteCampaniaMailchimpMetricaRegistrosDTO</returns>
        //public List<ReporteCampaniaMailchimpMetricaRegistrosDTO> EstructurarReporteCampaniaMailChimpRegistrosExportacion(DateTime fechaInicio, DateTime fechaFin)
        //{
        //    try
        //    {
        //        List<ReporteCampaniaMailchimpMetricaRegistrosDTO> listaReporteAnuncioMailChimpRegistros = _repCampaniaMailing.ObtenerReporteCampaniaMailingRegistros(fechaInicio, fechaFin);

        //        var listaReporteAgrupado = listaReporteAnuncioMailChimpRegistros
        //            .GroupBy(g => new
        //            {
        //                g.IdCampaniaMailing,
        //                g.NombreCampaniaMailing,
        //                g.IdCampaniaMailingDetalle,
        //                g.NombreCampaniaMailingDetalle,
        //                g.VersionMailing,
        //                g.FechaEnvioCampaniaMailing,
        //            }).Select(s => new ReporteCampaniaMailchimpMetricaRegistrosDTO
        //            {
        //                IdCampaniaMailing = s.Key.IdCampaniaMailing,
        //                NombreCampaniaMailing = s.Key.NombreCampaniaMailing,
        //                IdCampaniaMailingDetalle = s.Key.IdCampaniaMailingDetalle,
        //                NombreCampaniaMailingDetalle = s.Key.NombreCampaniaMailingDetalle,
        //                FechaEnvioCampaniaMailing = s.Key.FechaEnvioCampaniaMailing,
        //                CantidadRegistrosMailing = s.Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaOrigenChat = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasChat).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaOrigenChatOffline = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasChatOffline).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaAccesoPrueba = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioAccesoPrueba).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioCarrera = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioCarrera).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioContactenos = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioContactenos).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioPago = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioPago).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioPrograma = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioPrograma).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioPropio = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioPropio).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioRegistrarse = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioRegistrarse).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaOrigenInteligenteChat = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteChat).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaOrigenInteligenteChatOffline = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteChatOffline).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaInteligenteAccesoPrueba = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasIntFormularioAccesoPrueba).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioInteligenteCarrera = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioCarrera).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioInteligenteContactenos = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasIntFormularioContactenos).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioInteligentePago = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioPago).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioInteligentePrograma = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioPrograma).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioInteligentePropio = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioPropio).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioInteligenteRegistrarse = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasIntFormularioRegistrarse).Sum(op => op.CantidadRegistrosMailing),
        //            }).ToList();

        //        return listaReporteAgrupado;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Estructura el reporte del detalle de campania Mailchimp registros
        ///// </summary>
        ///// <param name="idCampaniaMailing">Id de la campania general o campania mailing</param>
        ///// <param name="versionMailing">Flag para determinar si es del nuevo modulo de mailing</param>
        ///// <param name="fechaInicio">Fecha de inicio para la busqueda</param>
        ///// <param name="fechaFin">Fecha de fin para la busqueda</param>
        ///// <returns>Lista de objetos de clase ReporteCampaniaMailchimpMetricaRegistrosDTO</returns>
        //public List<ReporteCampaniaMailchimpMetricaRegistrosDTO> EstructurarReporteDetalleCampaniaMailChimpRegistros(int idCampaniaMailing, bool versionMailing, DateTime fechaInicio, DateTime fechaFin)
        //{
        //    try
        //    {
        //        List<ReporteCampaniaMailchimpMetricaRegistrosDTO> listaReporteAnuncioMailChimpRegistros = _repCampaniaMailing.ObtenerReporteCampaniaMailingRegistrosDetalle(idCampaniaMailing, versionMailing, fechaInicio, fechaFin);

        //        var listaReporteAgrupado = listaReporteAnuncioMailChimpRegistros
        //            .GroupBy(g => new
        //            {
        //                g.IdCampaniaMailingDetalle,
        //                g.NombreCampaniaMailingDetalle,
        //                g.VersionMailing,
        //                g.FechaEnvioCampaniaMailing,
        //            }).Select(s => new ReporteCampaniaMailchimpMetricaRegistrosDTO
        //            {
        //                IdCampaniaMailingDetalle = s.Key.IdCampaniaMailingDetalle,
        //                NombreCampaniaMailingDetalle = s.Key.NombreCampaniaMailingDetalle,
        //                FechaEnvioCampaniaMailing = s.Key.FechaEnvioCampaniaMailing,
        //                CantidadRegistrosMailing = s.Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaOrigenChat = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasChat).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaOrigenChatOffline = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasChatOffline).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaAccesoPrueba = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioAccesoPrueba).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioCarrera = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioCarrera).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioContactenos = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioContactenos).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioPago = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioPago).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioPrograma = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioPrograma).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioPropio = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioPropio).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioRegistrarse = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioRegistrarse).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaOrigenInteligenteChat = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteChat).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaOrigenInteligenteChatOffline = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteChatOffline).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaInteligenteAccesoPrueba = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasIntFormularioAccesoPrueba).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioInteligenteCarrera = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioCarrera).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioInteligenteContactenos = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasIntFormularioContactenos).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioInteligentePago = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioPago).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioInteligentePrograma = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioPrograma).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioInteligentePropio = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioPropio).Sum(op => op.CantidadRegistrosMailing),
        //                CantidadCategoriaFormularioInteligenteRegistrarse = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasIntFormularioRegistrarse).Sum(op => op.CantidadRegistrosMailing),
        //            }).OrderByDescending(o => o.FechaEnvioCampaniaMailing).ToList();

        //        return listaReporteAgrupado;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Estructura el reporte de campania Mailchimp oportunidades
        ///// </summary>
        ///// <param name="fechaInicio">Fecha de inicio para la busqueda</param>
        ///// <param name="fechaFin">Fecha de fin para la busqueda</param>
        ///// <returns>Lista de objetos de clase ReporteCampaniaMailchimpMetricaOportunidadesDTO</returns>
        //public List<ReporteCampaniaMailchimpMetricaOportunidadesDTO> EstructurarReporteCampaniaMailChimpOportunidades(DateTime fechaInicio, DateTime fechaFin)
        //{
        //    try
        //    {
        //        List<ReporteCampaniaMailchimpMetricaOportunidadesDTO> listaReporteAnuncioMailChimpRegistros = _repCampaniaMailing.ObtenerReporteCampaniaMailingOportunidades(fechaInicio, fechaFin);

        //        var listaReporteAgrupado = listaReporteAnuncioMailChimpRegistros
        //            .GroupBy(g => new
        //            {
        //                g.IdCampaniaMailing,
        //                g.NombreCampaniaMailing,
        //                g.VersionMailing,
        //                g.FechaEnvioCampaniaMailing,
        //            }).Select(s => new ReporteCampaniaMailchimpMetricaOportunidadesDTO
        //            {
        //                IdCampaniaMailing = s.Key.IdCampaniaMailing,
        //                NombreCampaniaMailing = s.Key.NombreCampaniaMailing,
        //                FechaEnvioCampaniaMailing = s.Key.FechaEnvioCampaniaMailing,
        //                VersionMailing = s.Key.VersionMailing,
        //                CantidadOportunidadesMailing = s.Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaOrigenChat = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasChat).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaOrigenChatOffline = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasChatOffline).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaAccesoPrueba = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioAccesoPrueba).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioCarrera = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioCarrera).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioContactenos = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioContactenos).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioPago = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioPago).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioPrograma = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioPrograma).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioPropio = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioPropio).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioRegistrarse = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioRegistrarse).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaOrigenInteligenteChat = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteChat).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaOrigenInteligenteChatOffline = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteChatOffline).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaInteligenteAccesoPrueba = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasIntFormularioAccesoPrueba).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioInteligenteCarrera = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioCarrera).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioInteligenteContactenos = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasIntFormularioContactenos).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioInteligentePago = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioPago).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioInteligentePrograma = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioPrograma).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioInteligentePropio = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioPropio).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioInteligenteRegistrarse = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasIntFormularioRegistrarse).Sum(op => op.CantidadOportunidadesMailing),
        //            }).ToList();

        //        listaReporteAgrupado = listaReporteAgrupado
        //            .GroupBy(g => new
        //            {
        //                g.IdCampaniaMailing,
        //                g.NombreCampaniaMailing,
        //                g.VersionMailing
        //            }).Select(s => new ReporteCampaniaMailchimpMetricaOportunidadesDTO
        //            {
        //                IdCampaniaMailing = s.Key.IdCampaniaMailing,
        //                NombreCampaniaMailing = s.Key.NombreCampaniaMailing,
        //                FechaEnvioCampaniaMailing = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Min(op => op.FechaEnvioCampaniaMailing),
        //                VersionMailing = s.Key.VersionMailing,
        //                CantidadOportunidadesMailing = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaOrigenChat = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaOrigenChat),
        //                CantidadCategoriaOrigenChatOffline = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaOrigenChatOffline),
        //                CantidadCategoriaAccesoPrueba = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaAccesoPrueba),
        //                CantidadCategoriaFormularioCarrera = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioCarrera),
        //                CantidadCategoriaFormularioContactenos = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioContactenos),
        //                CantidadCategoriaFormularioPago = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioPago),
        //                CantidadCategoriaFormularioPrograma = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioPrograma),
        //                CantidadCategoriaFormularioPropio = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioPropio),
        //                CantidadCategoriaFormularioRegistrarse = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioRegistrarse),
        //                CantidadCategoriaOrigenInteligenteChat = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaOrigenInteligenteChat),
        //                CantidadCategoriaOrigenInteligenteChatOffline = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaOrigenInteligenteChatOffline),
        //                CantidadCategoriaInteligenteAccesoPrueba = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaInteligenteAccesoPrueba),
        //                CantidadCategoriaFormularioInteligenteCarrera = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioInteligenteCarrera),
        //                CantidadCategoriaFormularioInteligenteContactenos = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioInteligenteContactenos),
        //                CantidadCategoriaFormularioInteligentePago = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioInteligentePago),
        //                CantidadCategoriaFormularioInteligentePrograma = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioInteligentePrograma),
        //                CantidadCategoriaFormularioInteligentePropio = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioInteligentePropio),
        //                CantidadCategoriaFormularioInteligenteRegistrarse = s.Where(w => w.IdCampaniaMailing == s.Key.IdCampaniaMailing).Sum(op => op.CantidadCategoriaFormularioInteligenteRegistrarse),
        //            }).ToList();

        //        return listaReporteAgrupado;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Estructura el reporte de campania Mailchimp oportunidades para exportacion
        ///// </summary>
        ///// <param name="fechaInicio">Fecha de inicio para la busqueda</param>
        ///// <param name="fechaFin">Fecha de fin para la busqueda</param>
        ///// <returns>Lista de objetos de clase ReporteCampaniaMailchimpMetricaRegistrosDTO</returns>
        //public List<ReporteCampaniaMailchimpMetricaOportunidadesDTO> EstructurarReporteCampaniaMailChimpOportunidadesExportacion(DateTime fechaInicio, DateTime fechaFin)
        //{
        //    try
        //    {
        //        List<ReporteCampaniaMailchimpMetricaOportunidadesDTO> listaReporteAnuncioMailChimpOportunidades = _repCampaniaMailing.ObtenerReporteCampaniaMailingOportunidades(fechaInicio, fechaFin);

        //        var listaReporteAgrupado = listaReporteAnuncioMailChimpOportunidades
        //            .GroupBy(g => new
        //            {
        //                g.IdCampaniaMailing,
        //                g.NombreCampaniaMailing,
        //                g.IdCampaniaMailingDetalle,
        //                g.NombreCampaniaMailingDetalle,
        //                g.VersionMailing,
        //                g.FechaEnvioCampaniaMailing,
        //            }).Select(s => new ReporteCampaniaMailchimpMetricaOportunidadesDTO
        //            {
        //                IdCampaniaMailing = s.Key.IdCampaniaMailing,
        //                NombreCampaniaMailing = s.Key.NombreCampaniaMailing,
        //                IdCampaniaMailingDetalle = s.Key.IdCampaniaMailingDetalle,
        //                NombreCampaniaMailingDetalle = s.Key.NombreCampaniaMailingDetalle,
        //                FechaEnvioCampaniaMailing = s.Key.FechaEnvioCampaniaMailing,
        //                CantidadOportunidadesMailing = s.Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaOrigenChat = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasChat).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaOrigenChatOffline = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasChatOffline).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaAccesoPrueba = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioAccesoPrueba).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioCarrera = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioCarrera).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioContactenos = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioContactenos).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioPago = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioPago).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioPrograma = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioPrograma).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioPropio = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioPropio).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioRegistrarse = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioRegistrarse).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaOrigenInteligenteChat = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteChat).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaOrigenInteligenteChatOffline = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteChatOffline).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaInteligenteAccesoPrueba = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasIntFormularioAccesoPrueba).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioInteligenteCarrera = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioCarrera).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioInteligenteContactenos = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasIntFormularioContactenos).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioInteligentePago = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioPago).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioInteligentePrograma = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioPrograma).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioInteligentePropio = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioPropio).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioInteligenteRegistrarse = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasIntFormularioRegistrarse).Sum(op => op.CantidadOportunidadesMailing),
        //            }).ToList();

        //        return listaReporteAgrupado;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Estructura el reporte del detalle de campania Mailchimp oportnidades
        ///// </summary>
        ///// <param name="idCampaniaMailing">Id de la campania general o campania mailing</param>
        ///// <param name="versionMailing">Flag para determinar si es del nuevo modulo de mailing</param>
        ///// <param name="fechaInicio">Fecha de inicio para la busqueda</param>
        ///// <param name="fechaFin">Fecha de fin para la busqueda</param>
        ///// <returns>Lista de objetos de clase ReporteCampaniaMailchimpMetricaOportunidadesDTO</returns>
        //public List<ReporteCampaniaMailchimpMetricaOportunidadesDTO> EstructurarReporteDetalleCampaniaMailChimpOportunidades(int idCampaniaMailing, bool versionMailing, DateTime fechaInicio, DateTime fechaFin)
        //{
        //    try
        //    {
        //        List<ReporteCampaniaMailchimpMetricaOportunidadesDTO> listaReporteAnuncioMailChimpRegistros = _repCampaniaMailing.ObtenerReporteCampaniaMailingOportunidadesDetalle(idCampaniaMailing, versionMailing, fechaInicio, fechaFin);

        //        var listaReporteAgrupado = listaReporteAnuncioMailChimpRegistros
        //            .GroupBy(g => new
        //            {
        //                g.IdCampaniaMailingDetalle,
        //                g.NombreCampaniaMailingDetalle,
        //                g.VersionMailing,
        //                g.FechaEnvioCampaniaMailing,
        //            }).Select(s => new ReporteCampaniaMailchimpMetricaOportunidadesDTO
        //            {
        //                IdCampaniaMailingDetalle = s.Key.IdCampaniaMailingDetalle,
        //                NombreCampaniaMailingDetalle = s.Key.NombreCampaniaMailingDetalle,
        //                FechaEnvioCampaniaMailing = s.Key.FechaEnvioCampaniaMailing,
        //                CantidadOportunidadesMailing = s.Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaOrigenChat = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasChat).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaOrigenChatOffline = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasChatOffline).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaAccesoPrueba = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioAccesoPrueba).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioCarrera = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioCarrera).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioContactenos = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioContactenos).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioPago = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioPago).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioPrograma = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioPrograma).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioPropio = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioPropio).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioRegistrarse = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasFormularioRegistrarse).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaOrigenInteligenteChat = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteChat).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaOrigenInteligenteChatOffline = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteChatOffline).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaInteligenteAccesoPrueba = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasIntFormularioAccesoPrueba).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioInteligenteCarrera = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioCarrera).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioInteligenteContactenos = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasIntFormularioContactenos).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioInteligentePago = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioPago).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioInteligentePrograma = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioPrograma).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioInteligentePropio = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasInteligenteFormularioPropio).Sum(op => op.CantidadOportunidadesMailing),
        //                CantidadCategoriaFormularioInteligenteRegistrarse = s.Where(w => w.IdCategoriaOrigen == ValorEstatico.IdMailingBasesPropiasIntFormularioRegistrarse).Sum(op => op.CantidadOportunidadesMailing),
        //            }).OrderByDescending(o => o.FechaEnvioCampaniaMailing).ToList();

        //        return listaReporteAgrupado;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        ///// <summary>
        ///// Actualiza la metrica en Integra, segun la DB de Facebook
        ///// </summary>
        ///// <param name="fechaInicio">Fecha de inicio del proceso</param>
        ///// <param name="fechaFin">Fecha de fin del proceso</param>
        ///// <param name="cadenaFechaConsultada">Cadena con la fecha consultada</param>
        ///// <returns>bool</returns>
        //public string EstructurarMensajeRegularizacionMailChimp(string cadenaFechaConsultadaInicio, string cadenaFechaConsultadaFin, DateTime fechaInicio, DateTime fechaFin)
        //{
        //    try
        //    {
        //        string texto = $@"<table style='font-family:'Helvetica Neue',Helvetica,Arial,'Lucida Grande',sans-serif; border-collapse: collapse;'>
        //        <tr style='padding: 5px;'>
        //            <td>
        //                <div style = 'background-color: #224060; padding: 25px 100px;'>
        //                    <img src='https://integrav4.bsginstitute.com/Images/Sistema/bsginstitute.png' alt='' />
        //                </div>
        //            </td>
        //        </tr>
        //        <tr><td><br /></td></tr>
        //        <tr style='text-align: center;'>
        //               <td>Ha finalizado correctamente la actualización de los datos de mailing:</td>
        //        </tr>
        //        <tr style='text-align: center;'>
        //            <td>
        //                <h3 style='color: #4A92DB;'>FECHA ACTUALIZADA:</h3>
        //                <h3>{cadenaFechaConsultadaInicio} - {cadenaFechaConsultadaFin}</h3>
        //            </td>
        //        </tr>
        //        <tr style='text-align: center;'>
        //            <td>
        //                <h3 style='color: #CA341D;'>HORA DE INICIO:</h3>
        //                <h3>{fechaInicio}</h3>
        //            </td>
        //        </tr>
        //        <tr style='text-align: center;'>
        //            <td>
        //                <h3 style='color: #CA341D;'>HORA DE FIN:</h3>
        //                <h3>{fechaFin}</h3>
        //            </td>
        //        </tr>
        //    </table>";

        //        return texto;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}