using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaMailingWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using iTextSharp.text;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingBlueCampaniasDTO;
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue.SendingblueContactosDTO;
using CampaniaGeneralDetalleProgramaDTO = BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp.CampaniaGeneralDetalleProgramaDTO;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.CampaniasMailingWhatsapp
{
    public class CampaniaGeneralService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CampaniaGeneralService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TCampaniaGeneral, CampaniaGeneral>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        /// Autor: Carlos Crispin
        /// Fecha: 16/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los detalle de una campaña
        /// </summary>
        /// <param name="idCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <returns>Objeto de clase CampaniaGeneralDTO</returns>
        public CampaniaGeneralDTO ObtenerDetalle(int idCampaniaGeneral)
        {
            try
            {
                var campaniaGeneral = _unitOfWork.campaniaGeneralRepositorio.Obtener(idCampaniaGeneral);
                campaniaGeneral.ListaPrioridades = _unitOfWork.campaniaGeneralRepositorio.ObtenerListaCampaniaGeneralDetalleConProgramas(idCampaniaGeneral);
                return campaniaGeneral;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 27/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los detalle de una campaña
        /// </summary>
        /// <param name="idCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <returns>Objeto de clase CampaniaGeneralDetalleEstadoEnEjecucionDTO</returns>
        public List<CampaniaGeneralDetalleEstadoEnEjecucionDTO> ObtenerEstadoEjecucion(int idCampaniaGeneral)
        {
            try
            {
                var campaniaGeneralEstadoEnEjecucion = _unitOfWork.campaniaGeneralRepositorio.ObtenerEstadoEjecucionCampaniaGeneralDetalle(idCampaniaGeneral);

                return campaniaGeneralEstadoEnEjecucion;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los detalle de una campaña detalle responsable
        /// </summary>
        /// <returns>Lsita de responsables</returns>
        public List<ResponsablesDTO> ObtenerDetalleResponsables(int IdCampaniaGeneralDetalle)
        {
            try
            {
                var ListaResponsables = _unitOfWork.campaniaGeneralRepositorio.ObtenerListaCampaniaGeneralDetalleResponsables(IdCampaniaGeneralDetalle);
                return ListaResponsables;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera lista de CampaniaGeneralDetallePrograma
        /// </summary>
        /// <param name="listaCampaniaGeneralDetallePrograma">Objeto de clase CampaniaGeneralDetalleProgramaDTO</param>
        /// <param name="i">Indice</param>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania general detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Objeto de clase CampaniaGeneralDetalleProgramaBO</returns>
        public TCampaniaGeneralDetallePrograma CrearCampaniaGeneralDetallePrograma(CampaniaGeneralDetalleProgramaDTO listaCampaniaGeneralDetallePrograma, int i, string usuario, int idCampaniaGeneralDetalle = 0)
        {
            TCampaniaGeneralDetallePrograma campaniaGeneralDetallePrograma = new TCampaniaGeneralDetallePrograma();

            if (idCampaniaGeneralDetalle != 0) campaniaGeneralDetallePrograma.IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle;
            if (listaCampaniaGeneralDetallePrograma.IdPgeneral != null)
            {
                campaniaGeneralDetallePrograma.IdPgeneral = Convert.ToInt32(listaCampaniaGeneralDetallePrograma.IdPgeneral);
            }
            campaniaGeneralDetallePrograma.NombreProgramaGeneral = listaCampaniaGeneralDetallePrograma.NombreProgramaGeneral;
            campaniaGeneralDetallePrograma.Orden = i;
            campaniaGeneralDetallePrograma.Estado = true;
            campaniaGeneralDetallePrograma.FechaCreacion = DateTime.Now;
            campaniaGeneralDetallePrograma.FechaModificacion = DateTime.Now;
            campaniaGeneralDetallePrograma.UsuarioCreacion = usuario;
            campaniaGeneralDetallePrograma.UsuarioModificacion = usuario;
            if (listaCampaniaGeneralDetallePrograma.IdCampaniaGeneralDetalle!=null)
            {
                campaniaGeneralDetallePrograma.IdCampaniaGeneralDetalle = Convert.ToInt32(listaCampaniaGeneralDetallePrograma.IdCampaniaGeneralDetalle);
            }
            if (listaCampaniaGeneralDetallePrograma.IdPgeneral != null)
            {
                campaniaGeneralDetallePrograma.IdPgeneral = Convert.ToInt32(listaCampaniaGeneralDetallePrograma.IdPgeneral);
            }
            return campaniaGeneralDetallePrograma;
        }
        /// <summary>
        /// Obtiene Las Campanias generales a ejecutar
        /// </summary>
        /// <returns>Lista de objetos de clase ActividadCampaniaGeneralParaEjecutarDTO</returns>
        public List<ActividadCampaniaGeneralWhatsappParaEjecutarDTO> ObtenerActividadCampaniaGeneralParaEjecutar()
        {
            try
            {
                return _unitOfWork.campaniaGeneralRepositorio.ObtenerActividadCampaniaGeneralParaEjecutar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Ingresa o actualiza la campania general
        /// </summary>
        /// <param name="campaniaGeneralDTO">entidad</param>
        /// <param name="usuario">usuario que realizo la inseccion</param>
        /// <returns>vacio</returns>
        public void InsertarOActualizarCampaniaGeneral(CampaniaGeneralDTO campaniaGeneralDTO, string usuario)
        {
            try
            {
                if (campaniaGeneralDTO.Id != 0)

                {
                    var campaniaGeneral = _unitOfWork.campaniaGeneralRepositorio.Obtener(campaniaGeneralDTO.Id);
                    campaniaGeneralDTO.IdTiempoFrecuencia=campaniaGeneral.IdTiempoFrecuencia;
                    campaniaGeneralDTO.IdRemitenteMailing = campaniaGeneral.IdRemitenteMailing;
                    campaniaGeneralDTO.IdPlantilla_Mailing = campaniaGeneral.IdPlantilla_Mailing;
                    campaniaGeneralDTO.IdPlantilla_Whatsapp = campaniaGeneral.IdPlantilla_Whatsapp;
                    campaniaGeneralDTO.IdHoraEnvio_Mailing =campaniaGeneral.IdHoraEnvio_Mailing;
                    campaniaGeneralDTO.IdHoraEnvio_Whatsapp = campaniaGeneral.IdHoraEnvio_Whatsapp;
                    _unitOfWork.campaniaGeneralRepositorio.Update(campaniaGeneralDTO);
                    _unitOfWork.Commit();

                    var campaniaGeneralDetalleBO = _unitOfWork.CampaniaGeneralDetalleRepository.ObtenerCampaniaGeneralDetallePorIdDeCampaniaGenerla(campaniaGeneralDTO.Id).ToList();

                    campaniaGeneralDetalleBO.RemoveAll(x => campaniaGeneralDTO.ListaPrioridades.Any(y => y.Id == x.Id));
                    if (campaniaGeneralDetalleBO != null)
                    {
                        _unitOfWork.CampaniaGeneralDetalleRepository.Delete(campaniaGeneralDetalleBO.Select(x => x.Id), usuario);
                        _unitOfWork.Commit();
                    }
                }
                if (campaniaGeneralDTO.ListaPrioridades != null)
                {
                    List<CampaniaGeneralDetalle> listaCampaniaGeneral = new List<CampaniaGeneralDetalle>();


                    foreach (var detalle in campaniaGeneralDTO.ListaPrioridades)
                    {
                        var IdCampaniaGeneralDetallesql = 0;
                        if (detalle.Id == 0)
                        {
                            var conjuntoAnuncioBO = new ConjuntoAnuncio
                            {
                                Nombre = detalle.Nombre,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                FechaCreacionCampania = DateTime.Now,
                                IdCategoriaOrigen = campaniaGeneralDTO.IdCategoriaOrigen,
                                Origen = "CAMPANIA_MAILING",
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario
                            };

                            var ConjuntoAuncioDespuesDeInseccion = _unitOfWork.ConjuntoAnuncioRepository.Add(conjuntoAnuncioBO);
                            _unitOfWork.Commit();
                            detalle.IdConjuntoAnuncio = ConjuntoAuncioDespuesDeInseccion.Id;

                            var campaniaGeneralDetalleBO = new CampaniaGeneralDetalle
                            {
                                IdCampaniaGeneral = detalle.IdCampaniaGeneral ?? 0,
                                Nombre = detalle.Nombre,
                                Prioridad = detalle.Prioridad,
                                Asunto = detalle.Asunto,
                                IdPersonal = detalle.IdPersonal,
                                IdCentroCosto = detalle.IdCentroCosto,
                                IdConjuntoAnuncio = detalle.IdConjuntoAnuncio,
                                CantidadContactosMailing = detalle.CantidadContactosMailing,
                                CantidadContactosWhatsapp = detalle.CantidadContactosWhatsapp,
                                NoIncluyeWhatsaap = detalle.NoIncluyeWhatsaap,
                                UrlFormulario = detalle.UrlFormulario,
                                Estado = true,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now,
                                UsuarioCreacion = usuario,
                                UsuarioModificacion = usuario,
                            };
                            IdCampaniaGeneralDetallesql = _unitOfWork.CampaniaGeneralDetalleRepository.AddSqlString(campaniaGeneralDetalleBO);

                            int i = 0;

                            if (detalle.ProgramasFiltro != null)
                            {
                                i = 0;
                                foreach (var programa in detalle.ProgramasFiltro)
                                {
                                    i += 1;
                                    programa.IdCampaniaGeneralDetalle = IdCampaniaGeneralDetallesql;
                                    _unitOfWork.CampaniaGeneralDetalleProgramaRepositorio.AddEntity(CrearCampaniaGeneralDetallePrograma(programa, i, usuario));
                                }
                                _unitOfWork.Commit();
                            }

                            var AreaCampaniaGeneralDetalle = new List<TCampaniaGeneralDetalleArea>();
                            if (detalle.Areas.Count() > 0)
                            {
                                foreach (var item in detalle.Areas)
                                {
                                    TCampaniaGeneralDetalleArea area = new TCampaniaGeneralDetalleArea
                                    {
                                        IdAreaCapacitacion = item,
                                        IdCampaniaGeneralDetalle = IdCampaniaGeneralDetallesql,
                                        Estado = true,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = usuario,
                                        UsuarioModificacion = usuario
                                    };
                                    AreaCampaniaGeneralDetalle.Add(area);
                                }
                                _unitOfWork.areaCampaniaGeneralDetalleRepository.AddByEntity(AreaCampaniaGeneralDetalle);
                                _unitOfWork.Commit();
                            }

                            var SubAreaCampaniaGeneralDetalle = new List<CampaniaGeneralDetalleSubAreaDTO>();
                            if (detalle.SubAreas.Count() > 0)
                            {
                                foreach (var item in detalle.SubAreas)
                                {
                                    CampaniaGeneralDetalleSubAreaDTO subArea = new CampaniaGeneralDetalleSubAreaDTO
                                    {
                                        IdSubAreaCapacitacion = item,
                                        IdCampaniaGeneralDetalle = IdCampaniaGeneralDetallesql,
                                        Estado = true,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = usuario,
                                        UsuarioModificacion = usuario
                                    };
                                    SubAreaCampaniaGeneralDetalle.Add(subArea);
                                }
                                _unitOfWork.CampaniaGeneralDetalleSubAreaRepositorio.Add(SubAreaCampaniaGeneralDetalle);
                                _unitOfWork.Commit();
                            }

                            var ResponsableCampaniaGeneralDetalle = new List<TCampaniaGeneralDetalleResponsable>();
                            if (detalle.Responsables.Count() > 0)
                            {
                                foreach (var item in detalle.Responsables)
                                {
                                    TCampaniaGeneralDetalleResponsable responsable = new TCampaniaGeneralDetalleResponsable
                                    {
                                        IdPersonal = item.IdResponsable == null ? 0 : item.IdResponsable.Value,
                                        Dia1 = item.Dia1 == null ? 0 : item.Dia1.Value,
                                        Dia2 = item.Dia2 == null ? 0 : item.Dia2.Value,
                                        Dia3 = item.Dia3 == null ? 0 : item.Dia3.Value,
                                        Dia4 = item.Dia4 == null ? 0 : item.Dia4.Value,
                                        Dia5 = item.Dia5 == null ? 0 : item.Dia5.Value,
                                        Total = item.Total == null ? 0 : item.Total.Value,
                                        Estado = true,
                                        FechaCreacion = DateTime.Now,
                                        IdCampaniaGeneralDetalle = IdCampaniaGeneralDetallesql,
                                        FechaModificacion = DateTime.Now,
                                        UsuarioCreacion = usuario,
                                        UsuarioModificacion = usuario
                                    };
                                    ResponsableCampaniaGeneralDetalle.Add(responsable);
                                }
                                _unitOfWork.campaniaGeneralDetalleResponsableRepositorio.Add(ResponsableCampaniaGeneralDetalle);
                                _unitOfWork.Commit();
                            }
                            listaCampaniaGeneral.Add(campaniaGeneralDetalleBO);
                        }
                        else
                        {
                            //actualizamos la campaña
                            if (!string.IsNullOrEmpty(detalle.Nombre))
                            {
                                var conjuntoAnuncioBO = new ConjuntoAnuncio();

                                if (detalle.IdConjuntoAnuncio != null)
                                {
                                    var conjuntoAnuncio = _unitOfWork.ConjuntoAnuncioRepository.FirstBy(x => x.Id == detalle.IdConjuntoAnuncio);

                                    conjuntoAnuncio.Nombre = detalle.Nombre;
                                    conjuntoAnuncio.Estado = true;
                                    conjuntoAnuncio.FechaModificacion = DateTime.Now;
                                    conjuntoAnuncio.UsuarioModificacion = usuario;
                                    conjuntoAnuncio.IdCentroCosto = detalle.IdCentroCosto;//esto revisa
                                    conjuntoAnuncio.IdCategoriaOrigen = campaniaGeneralDTO.IdCategoriaOrigen;
                                    conjuntoAnuncio.Origen = "CAMPANIA_MAILING";
                                    _unitOfWork.ConjuntoAnuncioRepository.Update(conjuntoAnuncio);
                                    _unitOfWork.Commit();
                                }
                                else
                                {
                                    conjuntoAnuncioBO.Nombre = detalle.Nombre;
                                    conjuntoAnuncioBO.Estado = true;
                                    conjuntoAnuncioBO.FechaCreacion = DateTime.Now;
                                    conjuntoAnuncioBO.FechaModificacion = DateTime.Now;
                                    conjuntoAnuncioBO.UsuarioCreacion = usuario;
                                    conjuntoAnuncioBO.IdCentroCosto = detalle.IdCentroCosto; //esto revisa
                                    conjuntoAnuncioBO.FechaCreacionCampania = conjuntoAnuncioBO.FechaCreacion;
                                    conjuntoAnuncioBO.IdCategoriaOrigen = campaniaGeneralDTO.IdCategoriaOrigen;
                                    conjuntoAnuncioBO.Origen = "CAMPANIA_MAILING";
                                    conjuntoAnuncioBO.UsuarioModificacion = usuario;

                                    _unitOfWork.ConjuntoAnuncioRepository.Add(conjuntoAnuncioBO);
                                    detalle.IdConjuntoAnuncio = conjuntoAnuncioBO.Id;
                                    _unitOfWork.Commit();
                                }
                            }
                            var campaniaGeneralDetalle = _unitOfWork.CampaniaGeneralDetalleRepository.FirstById(detalle.Id);

                            List<CampaniaGeneralDetalleDTO> campaniaGeneralDetalleDTO = new List<CampaniaGeneralDetalleDTO>();

                            var listaCampaniaGeneralDetallePrograma = _unitOfWork.CampaniaGeneralDetalleRepository.GetBy(x => x.IdCampaniaGeneral == detalle.Id).ToList();
                            foreach (var campaniaGeneralDetallePrograma in listaCampaniaGeneralDetallePrograma)
                            {
                                _unitOfWork.CampaniaGeneralDetalleRepository.Delete(campaniaGeneralDetallePrograma.Id, usuario);
                            }
                            _unitOfWork.Commit();
                            _unitOfWork.areaCampaniaGeneralDetalleRepository.EliminacionLogicoPorCampaniaGeneral(detalle.Id, usuario, detalle.Areas);
                            _unitOfWork.Commit();
                            _unitOfWork.CampaniaGeneralDetalleSubAreaRepositorio.EliminacionLogicoPorCampaniaGeneral(detalle.Id, usuario, detalle.SubAreas);
                            _unitOfWork.Commit();

                            var listaCampaniaGeneralDetalleProgramaBO = new List<CampaniaGeneralDetalleProgramaDTO>();

                            int i = 0;
                            if (detalle.ProgramasFiltro != null)
                            {
                                i = 0;
                                foreach (var programa in detalle.ProgramasFiltro)
                                {
                                    i += 1;
                                    var modelo = (CrearCampaniaGeneralDetallePrograma(programa, i, usuario));
                                    _unitOfWork.CampaniaGeneralDetalleProgramaRepositorio.Add(modelo);
                                    _unitOfWork.Commit();
                                }
                            }
                            List<CampaniaGeneralDetalleArea> var = new List<CampaniaGeneralDetalleArea>();
                            foreach (var item in detalle.Areas)
                            {
                                TCampaniaGeneralDetalleArea area;
                                if (_unitOfWork.areaCampaniaGeneralDetalleRepository.ExistFunction(item, detalle.Id))
                                {
                                    area = _unitOfWork.areaCampaniaGeneralDetalleRepository.FirstBy(item, detalle.Id);
                                    area.IdAreaCapacitacion = item;
                                    area.UsuarioModificacion = usuario;
                                    area.FechaModificacion = DateTime.Now;
                                    _unitOfWork.areaCampaniaGeneralDetalleRepository.Update(area);
                                    _unitOfWork.Commit();
                                }
                                else
                                {
                                    area = new TCampaniaGeneralDetalleArea
                                    {
                                        IdAreaCapacitacion = item,
                                        IdCampaniaGeneralDetalle = campaniaGeneralDetalle.Id,
                                        UsuarioCreacion = usuario,
                                        UsuarioModificacion = usuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        Estado = true
                                    };
                                    _unitOfWork.areaCampaniaGeneralDetalleRepository.AddByEntity(area);
                                    _unitOfWork.Commit();
                                }

                            }
                            List<CampaniaGeneralDetalleSubArea> variable = new List<CampaniaGeneralDetalleSubArea>();
                            foreach (var item in detalle.SubAreas)
                            {
                                TCampaniaGeneralDetalleSubArea subArea;
                                if (_unitOfWork.CampaniaGeneralDetalleSubAreaRepositorio.ExistFunction(item, detalle.Id))
                                {
                                    subArea = _unitOfWork.CampaniaGeneralDetalleSubAreaRepositorio.FirstBy(item, detalle.Id);
                                    subArea.IdSubAreaCapacitacion = item;
                                    subArea.UsuarioModificacion = usuario;
                                    subArea.FechaModificacion = DateTime.Now;
                                    _unitOfWork.CampaniaGeneralDetalleSubAreaRepositorio.UpdateByEntity(subArea);
                                    _unitOfWork.Commit();
                                }
                                else
                                {
                                    subArea = new TCampaniaGeneralDetalleSubArea
                                    {
                                        IdSubAreaCapacitacion = item,
                                        UsuarioCreacion = usuario,
                                        IdCampaniaGeneralDetalle = campaniaGeneralDetalle.Id,
                                        UsuarioModificacion = usuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        Estado = true
                                    };
                                    _unitOfWork.CampaniaGeneralDetalleSubAreaRepositorio.Add(subArea);
                                    _unitOfWork.Commit();
                                }

                            }

                            var ResponsableCampaniaGeneralDetalle = new List<CampaniaGeneralDetalleResponsableDTO>();
                            foreach (var item in detalle.Responsables)
                            {
                                TCampaniaGeneralDetalleResponsable responsable;
                                if (_unitOfWork.campaniaGeneralDetalleResponsableRepositorio.ExistFunction(item.Id, detalle.Id))
                                {
                                    responsable = _unitOfWork.campaniaGeneralDetalleResponsableRepositorio.FirstBy(Convert.ToInt32(item.Id), detalle.Id);
                                    responsable.IdPersonal = item.IdResponsable == null ? 0 : item.IdResponsable.Value;
                                    responsable.Dia1 = item.Dia1 == null ? 0 : item.Dia1.Value;
                                    responsable.Dia2 = item.Dia2 == null ? 0 : item.Dia2.Value;
                                    responsable.Dia3 = item.Dia3 == null ? 0 : item.Dia3.Value;
                                    responsable.Dia4 = item.Dia4 == null ? 0 : item.Dia4.Value;
                                    responsable.Dia5 = item.Dia5 == null ? 0 : item.Dia5.Value;
                                    responsable.Total = item.Total == null ? 0 : item.Total.Value;
                                    responsable.UsuarioModificacion = usuario;
                                    responsable.FechaModificacion = DateTime.Now;
                                    _unitOfWork.campaniaGeneralDetalleResponsableRepositorio.UpdateByEntity(responsable);
                                    _unitOfWork.Commit();
                                }
                                else
                                {
                                    responsable = new TCampaniaGeneralDetalleResponsable
                                    {
                                        IdPersonal = item.IdResponsable == null ? 0 : item.IdResponsable.Value,
                                        Dia1 = item.Dia1 == null ? 0 : item.Dia1.Value,
                                        Dia2 = item.Dia2 == null ? 0 : item.Dia2.Value,
                                        Dia3 = item.Dia3 == null ? 0 : item.Dia3.Value,
                                        Dia4 = item.Dia4 == null ? 0 : item.Dia4.Value,
                                        Dia5 = item.Dia5 == null ? 0 : item.Dia5.Value,
                                        Total = item.Total == null ? 0 : item.Total.Value,
                                        UsuarioCreacion = usuario,
                                        IdCampaniaGeneralDetalle = campaniaGeneralDetalle.Id,
                                        UsuarioModificacion = usuario,
                                        FechaCreacion = DateTime.Now,
                                        FechaModificacion = DateTime.Now,
                                        Estado = true
                                    };
                                    _unitOfWork.campaniaGeneralDetalleResponsableRepositorio.Add(responsable);
                                    _unitOfWork.Commit();
                                }

                            }
                        }
                    }
                }
          }catch(Exception e)
            {
                throw e;
            }
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Agrega una nueva campania general
        /// </summary>
        /// <returns>Campania general</returns>
        public CampaniaGeneral Add(CampaniaGeneralEnvioDTO data, string User)
        {
            try
            {
                var repCampaniaGeneral = _unitOfWork.CampaniaGeneralRepository;
                CampaniaGeneral entidad = new CampaniaGeneral();
                entidad.Id = 0;
                entidad.Nombre = data.Nombre;
                entidad.IdCategoriaOrigen = data.IdCategoriaOrigen;
                entidad.IdCategoriaObjetoFiltro = data.IdCategoriaObjetoFiltro;
                entidad.NroMaximoSegmentos = data.NroMaximoSegmentos;
                entidad.CantidadPeriodoSinCorreo = data.CantidadPeriodoSinCorreo;
                entidad.IdProbabilidadRegistroPw = data.IdProbabilidadRegistroPw;
                if (data.IncluyeWhatsapp != null)
                {
                    entidad.IncluyeWhatsapp = data.IncluyeWhatsapp;
                }
                else
                {
                    entidad.IncluyeWhatsapp = false;
                }
                entidad.IdFiltroSegmento = data.IdFiltroSegmento;
                entidad.IdTipoAsociacion = data.IdTipoAsociacion;
                entidad.Estado = true;
                entidad.UsuarioCreacion = User;
                entidad.UsuarioModificacion = User;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;


                var modelo = _unitOfWork.CampaniaGeneralRepository.AddSqlInsert(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualia los datos de una campania general
        /// </summary>
        /// <returns>Retorna una campania general</returns>
        public CampaniaGeneral Update(CampaniaGeneralEnvioDTO data, string User)
        {
            try
            {
                var repCampaniaGeneral = _unitOfWork.CampaniaGeneralRepository;
                CampaniaGeneral entidad = new CampaniaGeneral();
                entidad.Id = data.Id;
                entidad.Nombre = data.Nombre;
                entidad.IdCategoriaOrigen = data.IdCategoriaOrigen;
                entidad.NroMaximoSegmentos = data.NroMaximoSegmentos;
                entidad.CantidadPeriodoSinCorreo = data.CantidadPeriodoSinCorreo;
                entidad.IdProbabilidadRegistroPw = data.IdProbabilidadRegistroPw;
                entidad.IdFiltroSegmento = data.IdFiltroSegmento;
                entidad.IdTipoAsociacion = data.IdTipoAsociacion;
                entidad.UsuarioModificacion = User;
                entidad.FechaModificacion = DateTime.Now;

                var modelo = _unitOfWork.CampaniaGeneralRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<CampaniaGeneral>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// Realiza la eliminacion logica de una campania general
        /// </summary>
        /// <returns>true si es correcto o false si hubo algun problema</returns>
        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.CampaniaGeneralRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Agrega una lista de entidades
        /// </summary>
        /// <returns>retorna una lista de entidades</returns>
        public List<CampaniaGeneral> Add(List<CampaniaGeneral> listadoEntidad,string usuario)
        {
            try
            {
                listadoEntidad = listadoEntidad.Select(x => new CampaniaGeneral
                {
                    CantidadPeriodoSinCorreo = x.CantidadPeriodoSinCorreo,
                    DiasSinWhatsapp=x.DiasSinWhatsapp,
                    Estado=x.Estado,
                    FechaCreacion=x.FechaCreacion,
                    FechaEnvio=x.FechaEnvio,
                    FechaFinEnvioWhatsapp=x.FechaFinEnvioWhatsapp,
                    FechaInicioEnvioWhatsapp=x.FechaInicioEnvioWhatsapp,
                    FechaModificacion=x.FechaModificacion,
                    Id=x.Id,
                    IdCategoriaObjetoFiltro=x.IdCategoriaObjetoFiltro,
                    IdCategoriaOrigen=x.IdCategoriaOrigen,
                    IdEstadoEnvioMailing=x.IdEstadoEnvioMailing,
                    IdEstadoEnvioWhatsapp=x.IdEstadoEnvioWhatsapp,
                    IdFiltroSegmento=x.IdFiltroSegmento,
                    IdHoraEnvioMailing=x.IdHoraEnvioMailing,
                    IdHoraEnvioWhatsapp=x.IdHoraEnvioWhatsapp,
                    IdMigracion=x.IdMigracion,
                    IdPlantillaMailing=x.IdPlantillaMailing,
                    IdPlantillaWhatsapp=x.IdPlantillaWhatsapp,
                    IdProbabilidadRegistroPw = x.IdProbabilidadRegistroPw,
                    IdRemitenteMailing =x.IdPlantillaMailing,
                    IdTiempoFrecuencia =x.IdTiempoFrecuencia,
                    IdTipoAsociacion = x.IdTipoAsociacion,
                    IncluirRebotes=x.IncluirRebotes,
                    IncluyeWhatsapp=x.IncluyeWhatsapp,
                    Nombre = x.Nombre,
                    NroMaximoSegmentos=x.NroMaximoSegmentos,
                    NumeroMinutosPrimerEnvio = x.NumeroMinutosPrimerEnvio,
                    RowVersion=x.RowVersion,
                    UsuarioCreacion=usuario,
                    UsuarioModificacion=usuario,
                }).ToList();
                var modelo = _unitOfWork.CampaniaGeneralRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CampaniaGeneral>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza una lista de entidades
        /// </summary>
        /// <returns>una lista de entidades</returns>
        public List<CampaniaGeneral> Update(List<CampaniaGeneral> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.CampaniaGeneralRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<CampaniaGeneral>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Rodrigo Montesinos
        /// Fecha: 05/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtener la campania general pro el id
        /// </summary>
        /// <returns>campania general</returns>
        public CampaniaGeneralDTO ObtenerPorId(int id)
        {
            return _unitOfWork.campaniaGeneralRepositorio.Obtener(id)
;
        }
        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.CampaniaGeneralRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 25/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CampaniaGeneral para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<CampaniaGeneralEnvio> ObtenerCampaniaGeneral()
        {
            try
            {
                return _unitOfWork.CampaniaGeneralRepository.ObtenerCampaniaGeneral();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 27/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CampaniaGeneral solo para whatsapp para mostrarse en tabla.
        /// </summary>
        /// <returns> List<CampaniaGeneral> </returns>
        public IEnumerable<ConfiguracionDeEnvioParaWhatsAppMasPlantilla> ObtenerConfiguracionDeEnvioParaWhatsAppMasPlantilla()
        {
            try
            {
                return _unitOfWork.CampaniaGeneralRepository.ObtenerConfiguracionDeEnvioParaWhatsAppMasPlantilla();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 27/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CampaniaGeneral solo para whatsapp para mostrarse en tabla.
        /// </summary>
        /// <returns> List<CampaniaGeneral> </returns>
        public IEnumerable<CampaniaGeneralEnvio> ObtenerCampaniaGeneralSoloWhatsApp()
        {
            try
            {
                return _unitOfWork.CampaniaGeneralRepository.ObtenerCampaniaGeneralWhatsApp();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene alumnos para subir lsita por excel
        /// </summary>
        /// <returns> List<AlumnoDTO> </returns>
        public List<AlumnoDTO> ObtenerAlumnosParaSubirALista(ListaIdsDtos lista)
        {
            try
            {
                return _unitOfWork.sendinblueListaRepository.ObtenerAlumnosParaSubirALista(lista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene alumnos para subir lsita por excel
        /// </summary>
        /// <returns> List<AlumnoDTO> </returns>
        public bool agregarListaAlumnos(agregarListaContactosDTO lista)
        {
            try
            {
                return _unitOfWork.sendinblueListaRepository.agregarListaAlumnos(lista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
