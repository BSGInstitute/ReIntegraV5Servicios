using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web.WebPages.Html;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion
{
    public class MaestroEvaluacionService : IMaestroEvaluacionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public MaestroEvaluacionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            //var config = new MapperConfiguration(cfg => cfg.CreateMap<TPuestoTrabajoRemuneracion, PuestoTrabajoRemuneracion>(MemberList.None).ReverseMap());
            //_mapper = new Mapper(config);
        }
        /// Autor: Sergio Yepez Pillco.
        /// Fecha: 02/01/2025
        /// <summary>
        /// Evaluacion Proceso
        /// </summary>
        /// <returns> Lista GestionRemuneracionPuestoTrabajoDTO </returns>
        public IEnumerable<ExamenTestDTO> Obtener()
        {
            List<ExamenTestDTO> listaCompleta = _unitOfWork.MaestroEvaluacionRepository.ObtenerExamenTest();
            
            return listaCompleta;
        }

        /// 
        /// Autor: Sergio Yepez Pillco
        /// Fecha: 07/01/2025
        /// <summary>
        /// Función que trae data para llenar los combos Formula, Sexo, Categoría
        /// </summary>
        /// <returns>Retorma una lista</returns>
        public ObtenerDataComboMaestroEvaluacionDTO ObtenerCombosModulo()
        {

            try
            {
                var comboDataCombo = new ObtenerDataComboMaestroEvaluacionDTO();
                comboDataCombo.ObtenerFormula = _unitOfWork.FormulaPuntajeRepository.ObtenerCombo().ToList();
                comboDataCombo.ObtenerSexo = _unitOfWork.SexoRepository.ObtenerCombo().ToList();
                comboDataCombo.ObtenerCategoria = _unitOfWork.EvaluacionCategoriumRepository.ObtenerCombo().ToList();
                return comboDataCombo;
            }
            catch
            {
                throw;
            }

        }

        /// 
        /// Autor: Sergio Yepez Pillco
        /// Fecha: 07/01/2025
        /// <summary>
        /// Función que trae data para llenar los combos Formula, Sexo, Categoría
        /// </summary>
        /// <returns>Retorma una lista</returns>
        public List<EvaluacionAgrupadaComponenteDTO> ObtenerEvaluacionAgrupado(int IdEvaluacion)
        {

            try
            {
                //implementar el resto de listas y retornar UN solo cuerpo o UNA serie de cuerpos DTO.
                List<EvaluacionAgrupadaComponenteDTO> listaExamenes = new List<EvaluacionAgrupadaComponenteDTO>();
                listaExamenes = _unitOfWork.MaestroEvaluacionRepository.ObtenerEvaluacionAgrupado(IdEvaluacion).ToList();
                return listaExamenes;
            }
            catch
            {
                throw;
            }

        }

        public List<ComboDTO> ObtenerExamenesAsignados(int idEvaluacion)
        {
            try
            {
                var examenesAsignados = _unitOfWork.ExamenRepository.GetBy(
                    x => x.Estado == true && x.IdExamenTest == idEvaluacion,
                    x => new ComboDTO
                    {
                        Id = x.Id,
                        Nombre = x.Nombre + "(" + x.Titulo + ")"
                    }
                ).ToList();

                return examenesAsignados;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<ComboDTO> ObtenerExamenesNoAsignados(int idEvaluacion)
        {
            try
            {
                var examenesAsignados = _unitOfWork.ExamenRepository.GetBy(
                    x => x.Estado == true && x.IdExamenTest == null,
                    x => new ComboDTO
                    {
                        Id = x.Id,
                        Nombre = x.Nombre + "(" + x.Titulo + ")"
                    }
                ).ToList();

                return examenesAsignados;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<CentilDTO> ObtenerCentilGrupoComponente(int idGrupoComponenteEvaluacion)
        {
            try
            {
                var centiles = _unitOfWork.CentilRepository.ObtenerGrupoEvaluacionDesglosadoPorComponente(idGrupoComponenteEvaluacion).ToList();
                return centiles;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public List<ComboDTO> ObtenerGrupos(int idEvaluacion)
        {
            try
            {
                var comboGrupos = _unitOfWork.GrupoComponenteEvaluacionRepository.ObtenerGrupoPorIdEvaluacion(idEvaluacion).ToList();
                return comboGrupos;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public List<GrupoComponenteDTO> ObtenerGruposComponenteDesglosado(int IdEvaluacion)
        {
            try
            {
                var listaGruposComponente = _unitOfWork.GrupoComponenteEvaluacionRepository.ObtenerGrupoEvaluacionDesglosadoPorComponente(IdEvaluacion).ToList();
                return listaGruposComponente;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public bool InsertarCentilGrupoComponente(CentilDTO CentilFormulario, string usuario)
        {
            try
            {
                TCentil centil = new TCentil()
                {
                    IdExamenTest = CentilFormulario.IdExamenTest == 0 ? null : CentilFormulario.IdExamenTest,
                    IdGrupoComponenteEvaluacion = CentilFormulario.IdGrupoComponenteEvaluacion == 0 ? null : CentilFormulario.IdGrupoComponenteEvaluacion,
                    IdExamen = CentilFormulario.IdExamen == 0 ? null : CentilFormulario.IdExamen,
                    IdSexo = CentilFormulario.IdSexo == 0 ? null : CentilFormulario.IdSexo,
                    ValorMinimo = CentilFormulario.ValorMinimo,
                    ValorMaximo = CentilFormulario.ValorMaximo,
                    Centil = CentilFormulario.Centil,
                    CentilLetra = CentilFormulario.CentilLetra,
                    Estado = true,
                    EsVigente = true,
                    UsuarioCreacion = usuario,
                    UsuarioModificacion = usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                _unitOfWork.CentilRepository.Insert(centil);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public bool ActualizarCentilGrupoComponente(CentilDTO CentilFormulario, string usuario)
        {
            try
            {
                var centil = _unitOfWork.CentilRepository.FirstById(CentilFormulario.Id);
                if (centil != null)
                {
                    //centil.IdExamenTest = CentilFormulario.IdExamenTest;
                    centil.IdGrupoComponenteEvaluacion = CentilFormulario.IdGrupoComponenteEvaluacion;
                    centil.IdExamen = CentilFormulario.IdExamen;
                    centil.IdSexo = CentilFormulario.IdSexo;
                    centil.ValorMinimo = CentilFormulario.ValorMinimo;
                    centil.ValorMaximo = CentilFormulario.ValorMaximo;
                    centil.Centil = CentilFormulario.Centil;
                    centil.CentilLetra = CentilFormulario.CentilLetra;
                    centil.Estado = true;
                    centil.UsuarioModificacion = usuario;
                    centil.FechaModificacion = DateTime.Now;
                    _unitOfWork.CentilRepository.Update(centil);
                    _unitOfWork.Commit();
                }
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public List<CentilDTO> ObtenerCentilesPorEvaluacion(int idEvaluacion)
        {
            try
            {
                var listaCentiles = _unitOfWork.CentilRepository.ObtenerCentilesPorEvaluacion(idEvaluacion);
                return listaCentiles;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public List<GrupoComponenteEvaluacionDTO> ObtenerEvaluacionEditarGrupoGrilla(int IdEvaluacion)
        {
            try
            {
                List<GrupoComponenteEvaluacionDTO> ListaGrupoComponenteCompleta;
                if (IdEvaluacion > 0)
                {
                    //GrupoComponenteEvaluacionRepositorio repGrupoComponenteEvaluacionRep = new GrupoComponenteEvaluacionRepositorio();

                    //var listaGruposComponente = repGrupoComponenteEvaluacionRep.ObtenerGrupoEvaluacion(IdEvaluacion); // GrupoComponenteEvaluacionDTO
                    var listaGruposComponente = _unitOfWork.GrupoComponenteEvaluacionRepository.ObtenerGrupoEvaluacion(IdEvaluacion).ToList();

                    ListaGrupoComponenteCompleta = listaGruposComponente.GroupBy(u => (u.Id, u.Nombre)).Select(group =>
                                      new GrupoComponenteEvaluacionDTO
                                      {
                                          Id = group.Key.Id
                                          ,
                                          Nombre = group.Key.Nombre
                                          ,
                                          IdFormulaPuntaje = group.Select(x => x.IdFormula).FirstOrDefault()
                                          ,
                                          RequiereCentil = group.Select(x => x.RequiereCentil).FirstOrDefault()
                                          ,
                                          Factor = group.Select(x => x.Factor).FirstOrDefault()
                                          ,
                                          ListaComponentes = group.Select(x => new GrupoComponentesDTO { Id = x.IdExamen, Nombre = x.NombreExamen }).ToArray()
                                      }).ToList();
                }
                else
                {
                    ListaGrupoComponenteCompleta = new List<GrupoComponenteEvaluacionDTO>();
                }
                return ListaGrupoComponenteCompleta;
            }
            catch
            {
                throw;
            }
        }

        /// Metodo Eliminar.
        /// Autor: Sergio Yepez Pillco.
        /// Fecha: 17/12/2024
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica por el Primary Key
        /// </summary>   
        /// <param name="id"> (PK) </param>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                if (id == 0)
                {
                    throw new BadRequestException($"Id 0 no valido");
                }
                var entidad = _unitOfWork.ExamenTestRepository.ObtenerPorId(id);
                if (entidad != null && entidad.Id != 0)
                {
                    var respuesta = _unitOfWork.ExamenTestRepository.Delete(id, usuario);

                    _unitOfWork.Commit();
                    return respuesta;
                }
                else
                {
                    throw new BadRequestException($"No se encontro la entidad con el id {id}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ExamenTestDTO Actualizar(ExamenTestDTO dto)
        {
            try {
                //CentilRepositorio repCentilRep = new CentilRepositorio(_integraDBContext);
                //AsignacionPreguntaExamenRepositorio repAsignacionPreguntaExamenRep = new AsignacionPreguntaExamenRepositorio(_integraDBContext);
                //ExamenTestRepositorio repExamenTestRep = new ExamenTestRepositorio(_integraDBContext);
                TExamenTest examenTest = new TExamenTest();
                examenTest = _unitOfWork.ExamenTestRepository.FirstById(dto.Id);

                using (TransactionScope scope = new TransactionScope())
                {
                    examenTest.Nombre = dto.Nombre;
                    examenTest.NombreAbreviado = dto.NombreAbreviado;
                    examenTest.EsCalificadoPorPostulante = dto.EsCalificadoPorPostulante;
                    examenTest.MostrarEvaluacionAgrupado = dto.MostrarEvaluacionAgrupado;
                    examenTest.MostrarEvaluacionPorGrupo = dto.MostrarEvaluacionPorGrupo;
                    examenTest.MostrarEvaluacionPorComponente = dto.MostrarEvaluacionPorComponente;
                    examenTest.RequiereCentil = dto.RequiereCentil;
                    examenTest.IdFormulaPuntaje = dto.IdFormulaPuntaje;
                    examenTest.CalificarEvaluacion = dto.CalificarEvaluacion;
                    examenTest.EsCalificacionAgrupada = dto.EsCalificacionAgrupada;
                    examenTest.Factor = dto.Factor;
                    //examenTest.UsuarioModificacion = dto.Usuario;
                    examenTest.FechaModificacion = DateTime.Now;
                    examenTest.IdEvaluacionCategoria = dto.IdEvaluacionCategoria;
                    bool rpta = _unitOfWork.ExamenTestRepository.Update(examenTest);
                    _unitOfWork.Commit();
                    scope.Complete();
                }

                //foreach (var examenV in dto.ListaExamenVisualizacion)
                //{
                //    AsignacionPreguntaExamenBO asignado = repAsignacionPreguntaExamenRep.FirstById(examenV.IdAsignacionPreguntaExamen);
                //    if (asignado != null && asignado.Id != 0 && examenV.NroOrden != asignado.NroOrden)
                //    {
                //        asignado.NroOrden = examenV.NroOrden;
                //        asignado.UsuarioModificacion = dto.Usuario;
                //        asignado.FechaModificacion = DateTime.Now;
                //        repAsignacionPreguntaExamenRep.Update(asignado);
                //    }
                //}

                //var listaCentiles = repCentilRep.ObtenerCentilesEvaluacion(dto.Id);
                //foreach (var item in listaCentiles)
                //{
                //    if (!dto.ListaCentilEvaluacion.Any(x => x.Id == item.Id))
                //    {
                //        repCentilRep.Delete(item.Id, dto.Usuario);
                //    }
                //}
                //foreach (var item in dto.ListaCentilEvaluacion)
                //{
                //    if (item.Id > 0)
                //    {
                //        var centil = repCentilRep.FirstById(item.Id);
                //        centil.IdExamenTest = dto.Id;
                //        centil.Centil = item.Centil;
                //        centil.ValorMinimo = item.ValorMinimo;
                //        centil.ValorMaximo = item.ValorMaximo;
                //        centil.CentilLetra = item.CentilLetra;
                //        centil.IdSexo = item.IdSexo;
                //        centil.UsuarioModificacion = dto.Usuario;
                //        centil.FechaModificacion = DateTime.Now;
                //        repCentilRep.Update(centil);
                //    }
                //    else
                //    {
                //        CentilBO centil = new CentilBO();
                //        centil.IdExamenTest = dto.Id;
                //        centil.Centil = item.Centil;
                //        centil.ValorMinimo = item.ValorMinimo;
                //        centil.ValorMaximo = item.ValorMaximo;
                //        centil.CentilLetra = item.CentilLetra;
                //        centil.IdSexo = item.IdSexo;
                //        centil.UsuarioCreacion = dto.Usuario;
                //        centil.UsuarioModificacion = dto.Usuario;
                //        centil.Estado = true;
                //        centil.FechaCreacion = DateTime.Now;
                //        centil.FechaModificacion = DateTime.Now;
                //        repCentilRep.Insert(centil);
                //    }
                //}
                return dto;
            } catch (Exception ex){
                throw;
            }
        }
        public ExamenTestDTO Insertar(ExamenTestDTO dto)
        {
            try
            {
                TExamenTest examenTest = new TExamenTest();
                using (TransactionScope scope = new TransactionScope())
                {
                    examenTest.Nombre = dto.Nombre;
                    examenTest.NombreAbreviado = dto.NombreAbreviado;
                    examenTest.EsCalificadoPorPostulante = dto.EsCalificadoPorPostulante;
                    examenTest.MostrarEvaluacionAgrupado = dto.MostrarEvaluacionAgrupado;
                    examenTest.MostrarEvaluacionPorGrupo = dto.MostrarEvaluacionPorGrupo;
                    examenTest.MostrarEvaluacionPorComponente = dto.MostrarEvaluacionPorComponente;
                    examenTest.RequiereCentil = dto.RequiereCentil;
                    examenTest.IdFormulaPuntaje = dto.IdFormulaPuntaje;
                    examenTest.CalificarEvaluacion = dto.CalificarEvaluacion;
                    examenTest.EsCalificacionAgrupada = dto.EsCalificacionAgrupada;
                    examenTest.Factor = dto.Factor;

                    examenTest.Estado = true;
                    //examenTest.UsuarioCreacion = dto.Usuario;
                    examenTest.UsuarioCreacion = "System";
                    examenTest.FechaCreacion = DateTime.Now;
                    //examenTest.UsuarioModificacion = dto.Usuario;
                    examenTest.UsuarioModificacion = "System";
                    examenTest.FechaModificacion = DateTime.Now;
                    examenTest.IdEvaluacionCategoria = dto.IdEvaluacionCategoria;

                    _unitOfWork.ExamenTestRepository.Insert(examenTest);
                    _unitOfWork.Commit();
                    //dto.Id = examenTest.Id;
                    //foreach (var item in dto.)
                    //{
                    //    TCentil centil = new TCentil();
                    //    centil.IdExamenTest = dto.Id;
                    //    centil.Centil = item.Centil;
                    //    centil.ValorMinimo = item.ValorMinimo;
                    //    centil.ValorMaximo = item.ValorMaximo;
                    //    centil.CentilLetra = item.CentilLetra;
                    //    centil.IdSexo = item.IdSexo;
                    //    centil.UsuarioCreacion = dto.Usuario;
                    //    centil.UsuarioModificacion = dto.Usuario;
                    //    centil.Estado = true;
                    //    centil.FechaCreacion = DateTime.Now;
                    //    centil.FechaModificacion = DateTime.Now;
                    //    _unitOfWork.CentilRepository.Insert(centil);
                    //}

                    scope.Complete();
                }
                return dto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
