using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: DocumentoSeccionPwService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/07/2022
    /// <summary>
    /// Gestión general de T_DocumentoSeccionPw
    /// </summary>
    public class DocumentoSeccionPwService : IDocumentoSeccionPwService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DocumentoSeccionPwService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDocumentoSeccionPw, DocumentoSeccionPw>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public DocumentoSeccionPw Add(DocumentoSeccionPw entidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoSeccionPwRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DocumentoSeccionPw>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentoSeccionPw Update(DocumentoSeccionPw entidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoSeccionPwRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DocumentoSeccionPw>(modelo);
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
                _unitOfWork.DocumentoSeccionPwRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentoSeccionPw> Add(List<DocumentoSeccionPw> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoSeccionPwRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DocumentoSeccionPw>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentoSeccionPw> Update(List<DocumentoSeccionPw> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoSeccionPwRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DocumentoSeccionPw>>(modelo);
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
                _unitOfWork.DocumentoSeccionPwRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_DocumentoSeccionPw
        /// </summary>
        /// <returns> List<DocumentoSeccionPwDTO> </returns>
        public IEnumerable<DocumentoSeccionPwDTO> ObtenerDocumentoSeccionPw()
        {
            try
            {
                return _unitOfWork.DocumentoSeccionPwRepository.ObtenerDocumentoSeccionPw();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_DocumentoSeccionPw para mostrarse en combo.
        /// </summary>
        /// <returns> List<DocumentoSeccionPwComboDTO> </returns>
        public IEnumerable<DocumentoSeccionPwComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.DocumentoSeccionPwRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos complementarios de un Programa General
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General </param>
        /// <returns> List<RegistroListaSeccionesDocumentoDTO> </returns>
        public List<RegistroListaSeccionesDocumentoDTO> ObtenerDatosComplementariosProgramaGeneralV2(int idPGeneral)
        {
            try
            {
                return _unitOfWork.DocumentoSeccionPwRepository.ObtenerDatosComplementariosProgramaGeneralV2(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Genera HTML a partir de la lista de secciones de documento del programa general
        /// </summary>
        /// <param name="listaProgramaGeneralDocumentoSeccion"> Lista de programas por documento sección </param>
        /// <param name="conTitulo"> Validación de Título </param>
        /// <returns> List<RegistroListaSeccionesDocumentoDTO> </returns>
        public List<ProgramaGeneralSeccionAnexosHTMLDTO> GenerarHTMLProgramaGeneralDocumentoSeccion(List<ProgramaGeneralSeccionDocumentoDTO> listaProgramaGeneralDocumentoSeccion, bool conTitulo)
        {
            try
            {
                var lista = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();

                foreach (var item in listaProgramaGeneralDocumentoSeccion)
                {
                    string contenido = string.Empty;
                    conTitulo = item.Seccion == "Estructura Curricular";

                    foreach (var detalleSeccion in item.DetalleSeccion)
                    {
                        contenido += conTitulo ? $"<p><strong>{detalleSeccion.Titulo}</strong></p>" : string.Empty;

                        contenido += (detalleSeccion.Cabecera != string.Empty) ? $"<p>{detalleSeccion.Cabecera}</p><ul>" : "<ul>";
                        contenido = detalleSeccion.DetalleContenido.Aggregate(contenido, (current, contenidoSeccion) => current + "<li>" + contenidoSeccion + "</li>");
                        contenido += (detalleSeccion.PiePagina != string.Empty) ? $"</ul><p>{detalleSeccion.PiePagina}</p>" : "</ul>";
                    }

                    lista.Add(new ProgramaGeneralSeccionAnexosHTMLDTO
                    {
                        Seccion = item.Seccion,
                        Contenido = contenido
                    });
                }

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 09/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las secciones disponibles por el programa general
        /// </summary>
        /// <param name="idPGeneral">Id del programa general del cual se desae obtener las secciones disponibles (PK de la tabla pla.T_PGeneral)</param>
        /// <returns> List<SeccionDocumentoDTO> </returns>
        public List<SeccionDocumentoDTO> ObtenerSecciones(int idPGeneral)
        {
            try
            {
                return _unitOfWork.DocumentoSeccionPwRepository.ObtenerSecciones(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Datos Complementarios asociados a un Programa General.
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> List<RegistroListaSeccionesDocumentoDTO> </returns>
        public List<RegistroListaSeccionesDocumentoDTO> ObtenerDatosComplementariosProgramaGeneralV1(int idPGeneral)
        {
            try
            {
                return _unitOfWork.DocumentoSeccionPwRepository.ObtenerDatosComplementariosProgramaGeneralV1(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los Expositores relacionados a un Programa General.
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> List<ProgramaExpositoresDTO> </returns>
        public List<ProgramaExpositoresDTO> ObtenerExpositoresPorIdGeneral(int idPGeneral)
        {
            try
            {
                return _unitOfWork.DocumentoSeccionPwRepository.ObtenerExpositoresPorIdGeneral(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 07/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene secciones incluidos cabecera y pie de pagina asociados a un Programa General
        /// </summary>
        /// <param name="idPgeneral">Id de Programa General</param>
        /// <returns> List<SeccionDocumentoDTO> </returns>
        public List<RegistroListaSeccionesDocumentoDTO> ObtenerSeccionDocumento(int idPgeneral)
        {
            try
            {
                return _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionDocumento(idPgeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene secciones incluidos cabecera y pie de pagina de Estructura Curricular asociados a un Programa General
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns> List<SeccionDocumentoDTO> </returns>
        public List<RegistroListaSeccionesDocumentoDTO> ObtenerSeccionDocumentoEstructuraCurricular(int idPGeneral)
        {
            try
            {
                return _unitOfWork.DocumentoSeccionPwRepository.ObtenerSeccionDocumentoEstructuraCurricular(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la estructura curricular para un curso
        /// </summary>
        /// <param name="idPGeneral">Id del programa general del cual se desea obtener estructura (PK de la tabla pla.T_PGeneral)</param>
        /// <returns> Lista de contenido para un programa general: List<RegistroListaSeccionesDocumentoDTO> </returns>
        public List<EstructuraCursoDTO> ObtenerEstructuraCurso(int idPGeneral)
        {
            try
            {
                return _unitOfWork.DocumentoSeccionPwRepository.ObtenerEstructuraCurso(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna las secciones por programa general
        /// </summary>
        /// <param name="idPGeneral"></param>
        /// <returns> List<SeccionDocumentoDTO> </returns>
        public List<SeccionDocumentoDTO> ObtenerDocumentoSeccionCompleto(int idPGeneral)
        {
            try
            {
                return _unitOfWork.DocumentoSeccionPwRepository.ObtenerDocumentoSeccionCompleto(idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene le contenido a editar de DocumentoSeccion
        /// </summary>
        /// <param name="idDocumento"></param>
        /// <returns> List<DocumentoSeccionPwFiltroAgrupadoDTO> </returns>
        public List<DocumentoSeccionPwFiltroAgrupadoDTO> ObtenerDocumentoSeccionEditar(int idDocumento)
        {
            try
            {
                var listaDocumentoSeccion = _unitOfWork.DocumentoSeccionPwRepository.ObtenerDocumentoSeccionPorId(idDocumento);

                List<DocumentoSeccionPwFiltroAgrupadoDTO> agrupado = new List<DocumentoSeccionPwFiltroAgrupadoDTO>();
                agrupado = listaDocumentoSeccion.GroupBy(u => (u.IdDocumentoPW, u.IdSeccionPW, u.IdSeccionTipoContenido))
                                    .Select(group =>
                                    new DocumentoSeccionPwFiltroAgrupadoDTO
                                    {
                                        Id = group.Select(x => x.Id).FirstOrDefault(),
                                        Titulo = group.Select(x => x.Titulo).FirstOrDefault(),
                                        VisibleWeb = group.Select(x => x.VisibleWeb).FirstOrDefault(),
                                        ZonaWeb = group.Select(x => x.ZonaWeb).FirstOrDefault(),
                                        OrdenEeb = group.Select(x => x.OrdenEeb).FirstOrDefault(),
                                        Contenido = group.Select(x => x.Contenido).FirstOrDefault(),
                                        IdPlantillaPW = group.Select(x => x.IdPlantillaPw).FirstOrDefault(),
                                        Posicion = group.Select(x => x.Posicion).FirstOrDefault(),
                                        Tipo = group.Select(x => x.Tipo).FirstOrDefault(),
                                        IdDocumentoPW = group.Key.IdDocumentoPW,
                                        IdSeccionPW = group.Key.IdSeccionPW,
                                        IdSeccionTipoContenido = group.Key.IdSeccionTipoContenido,
                                        Cabecera = group.Select(x => x.Cabecera).FirstOrDefault(),
                                        PiePagina = group.Select(x => x.PiePagina).FirstOrDefault(),
                                        ListaSubSeccionesPw = group.Select(x => new SubSeccionTipoDetallePwDTO { IdSeccionTipoDetallePw = x.IdSeccionTipoDetallePw, NombreSubSeccion = x.NombreSubSeccion, IdSubSeccionTipoContenido = x.IdSubSeccionTipoContenido, ContenidoSubSeccion = x.ContenidoSubSeccion, NumeroFila = x.NumeroFila }).OrderBy(x => x.NumeroFila).ToList()
                                    }).ToList();

                foreach (var item in agrupado)
                {
                    List<SubSeccionTipoDetallePwDTO> eliminar = new List<SubSeccionTipoDetallePwDTO>();
                    foreach (var itemInterno in item.ListaSubSeccionesPw)
                    {
                        if (itemInterno.IdSeccionTipoDetallePw == null)
                        {
                            eliminar.Add(itemInterno);
                        }
                    }
                    foreach (var itemEliminar in eliminar)
                    {
                        item.ListaSubSeccionesPw.Remove(itemEliminar);
                    }
                }
                return agrupado.OrderBy(x => x.IdSeccionPW).ToList();
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Elimina (Actualiza estado a false ) todos las registros de DocumentoSeccion asociados a IdDocumento
        /// </summary>
        /// <param name="idDocumento"></param>
        /// <param name="usuario"></param>
        /// <param name="nuevos"></param>
        public void EliminacionDocumentoSeccionLogicoPorIdDocumento(int idDocumento, string usuario, List<DocumentoSeccionPwFiltroDTO> nuevos)
        {
            try
            {
                var listaBorrar = _unitOfWork.DocumentoSeccionPwRepository.ObtenerPorIdDocumento(idDocumento).ToList();

                foreach (var item in nuevos)
                {
                    if (item.listaGridListaSecciones.Count() > 0 && (item.IdSeccionPW == 91 || item.IdSeccionPW == 108))
                    {
                        foreach (var elemento in item.listaGridListaSecciones)
                        {
                            if (listaBorrar.Exists(w => w.Contenido == elemento.Valor))
                            {
                                listaBorrar.RemoveAll(x => x.Contenido == elemento.Valor);
                            }
                        }
                    }
                }

                //var seccionesAEliminar = nuevos
                //    .Where(item => (item.listaGridListaSecciones.Count() > 0) && (item.IdSeccionPW == 91 || item.IdSeccionPW == 108))
                //    .SelectMany(item => item.listaGridListaSecciones.Select(elemento => elemento.Valor));

                //listaBorrar.RemoveAll(item => seccionesAEliminar.Contains(item.Contenido));

                _unitOfWork.DocumentoSeccionPwRepository.Delete(listaBorrar.Select(x => x.Id), usuario);
                _unitOfWork.Commit();
            }
            catch
            {
                throw;
            }
        }
    }
}
