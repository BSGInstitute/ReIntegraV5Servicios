using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: DocumentoLegalService
    /// Autor Modificacion: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_DocumentoLegal
    /// </summary>
    public class DocumentoLegalService : IDocumentoLegalService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DocumentoLegalService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TDocumentoLegal, DocumentoLegal>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public DocumentoLegal Add(DocumentoLegal entidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoLegalRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DocumentoLegal>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentoLegal Update(DocumentoLegal entidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoLegalRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<DocumentoLegal>(modelo);
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
                _unitOfWork.DocumentoLegalRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentoLegal> Add(List<DocumentoLegal> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoLegalRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DocumentoLegal>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentoLegal> Update(List<DocumentoLegal> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.DocumentoLegalRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<DocumentoLegal>>(modelo);
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
                _unitOfWork.DocumentoLegalRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor Modificacion: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_DocumentoLegal
        /// </summary>
        /// <returns> List<DocumentoLegalDTO> </returns>
        public IEnumerable<DocumentoLegalDTO> ObtenerDocumentoLegal()
        {
            try
            {
                return _unitOfWork.DocumentoLegalRepository.ObtenerDocumentoLegal();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor Modificacion: Erick Marcelo Quispe.
        /// Fecha: 14/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_DocumentoLegal para mostrarse en combo.
        /// </summary>
        /// <returns> List<DocumentoLegalComboDTO> </returns>
        public IEnumerable<DocumentoLegalComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.DocumentoLegalRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los documentos legales y los agrupa para mostrar las areas
        /// </summary>
        /// <returns> List<DocumentoLegalV2DTO> </returns>
        public IEnumerable<DocumentoLegalV2DTO> ObtenerDocumentosLegalesV2()
        {
            try
            {
                return _unitOfWork.DocumentoLegalRepository.ObtenerDocumentosLegales();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los documentos legales para agenda
        /// </summary>
        /// <returns> List<DocumentoLegalDTO> </returns>
        /// <paramref name="area"/> Identificador del area
        /// <paramref name="rol"/> nomrbe del rol
        /// <paramref name="idpais"/>Identificador del pais
        public IEnumerable<DocumentoLegalV3DTO> ObtenerDocumentoLegalAgenda(int area, string rol, int idpais)
        {
            try
            {
                return _unitOfWork.DocumentoLegalRepository.ObtenerDocumentoLegalAgenda(area, rol, idpais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta en T_DocumentoLegal,T_DocumentoLegalPais, T_DocumentoLegalAreaTrabajo
        /// </summary>
        /// <returns> true , ó error segun el nivel. </returns>
        public int InsertarDocumentoLegal(DocumentoLegalV3DTO DocumentoLegal,string Usuario)
        {
            try
            {
                var _serAreaDocumento = new DocumentoLegalAreaTrabajoService(_unitOfWork);
                var _serPaisDocumento = new DocumentoLegalPaisService(_unitOfWork);
                var Roles = string.Join(",", DocumentoLegal.Roles);
                DocumentoLegal documento = new DocumentoLegal()
                {
                    Nombre = DocumentoLegal.Nombre,
                    Descripcion = DocumentoLegal.Descripcion,
                    IdPais = DocumentoLegal.IdPais,
                    Url = DocumentoLegal.Url,
                    VisualizarAgenda = DocumentoLegal.VisualizarAgenda,
                    DescargarAgenda = DocumentoLegal.DescargarAgenda,
                    Estado = true,
                    UsuarioCreacion = Usuario,
                    UsuarioModificacion = Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    Roles = Roles
                };
                var RptDL = this.Add(documento);
                documento = RptDL;
                if (RptDL != null)
                {
                    var idDocumento = RptDL.Id;
                    foreach (var item in DocumentoLegal.Areas)
                    {
                        DocumentoLegalAreaTrabajo areaDocumento = new DocumentoLegalAreaTrabajo()
                        {
                            IdDocumentoLegal = idDocumento,
                            IdPersonalAreaTrabajo = item,
                            Estado = true,
                            UsuarioCreacion = Usuario,
                            UsuarioModificacion = Usuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                        var RptDLAT = _serAreaDocumento.Add(areaDocumento);
                        if (RptDLAT == null)
                        {
                            throw new Exception("Fallo al insertar en T_DocumentoLegalAreaTrabajo");
                        }
                        else RptDLAT = null;
                    }
                    foreach (var item in DocumentoLegal.Paises)
                    {
                        DocumentoLegalPais paisDocumento = new DocumentoLegalPais();
                        paisDocumento.IdDocumentoLegal = idDocumento;
                        paisDocumento.IdPais = item;
                        paisDocumento.Estado = true;
                        paisDocumento.UsuarioCreacion = Usuario;
                        paisDocumento.UsuarioModificacion = Usuario;
                        paisDocumento.FechaCreacion = DateTime.Now;
                        paisDocumento.FechaModificacion = DateTime.Now;

                        var RptDLP = _serPaisDocumento.Add(paisDocumento);
                        if (RptDLP == null)
                        {
                            throw new Exception("Fallo al insertar en T_DocumentoLegalPais");
                        }
                        else RptDLP = null;
                    }
                }
                else throw new Exception("Fallo al insertar en T_DocumentoLegal");
                return RptDL.Id;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta o Actulaliza en T_DocumentoLegal,T_DocumentoLegalPais, T_DocumentoLegalAreaTrabajo
        /// </summary>
        /// <returns> true , ó error segun el nivel. </returns>
        /// <param name="DocumentoLegal">Grupo de parametros</param>
        public bool ActualizarDocumentoLegal(DocumentoLegalV3DTO DocumentoLegal,string Usuario)
        {
            try
            {
                var _serAreaDocumento = new DocumentoLegalAreaTrabajoService(_unitOfWork);
                var _serPaisDocumento = new DocumentoLegalPaisService(_unitOfWork);
                var _repDocumento = _unitOfWork.DocumentoLegalRepository;
                var _repAreaDocumento = _unitOfWork.DocumentoLegalAreaTrabajoRepository;
                var _repPaisDocumento = _unitOfWork.DocumentoLegalPaisRepository;
                TDocumentoLegal documento = new TDocumentoLegal();
                var Roles = string.Join(",", DocumentoLegal.Roles);
                documento = _repDocumento.FirstById(DocumentoLegal.Id);
                if (documento == null) throw new Exception("No se encontro ningun registro con ese Id");
                documento.Nombre = DocumentoLegal.Nombre;
                documento.Descripcion = DocumentoLegal.Descripcion;
                documento.IdPais = DocumentoLegal.IdPais;
                documento.Url = DocumentoLegal.Url;
                documento.VisualizarAgenda = DocumentoLegal.VisualizarAgenda;
                documento.DescargarAgenda = DocumentoLegal.DescargarAgenda;
                documento.UsuarioModificacion = Usuario;
                documento.FechaModificacion = DateTime.Now;
                documento.Roles = Roles;
                var RptU_DL = false;
                RptU_DL = _repDocumento.Update(documento);
                _unitOfWork.Commit();
                if (RptU_DL == true)
                {
                    var idDocumento = documento.Id;
                    var areasDocumentoBD = _repAreaDocumento.GetBy(x => x.IdDocumentoLegal == idDocumento).Select(z => z.IdPersonalAreaTrabajo).ToList();
                    if (areasDocumentoBD.Count < DocumentoLegal.Areas.Count)
                    {
                        var diferencia = DocumentoLegal.Areas.Except(areasDocumentoBD).ToList();
                        //InsertarDiferencia
                        foreach (var item in diferencia)
                        {
                            DocumentoLegalAreaTrabajo areaDocumento = new DocumentoLegalAreaTrabajo()
                            {
                                IdDocumentoLegal = idDocumento,
                                IdPersonalAreaTrabajo = item,
                                Estado = true,
                                UsuarioCreacion = Usuario,
                                UsuarioModificacion = Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };
                            var Rpt_DLAT = _serAreaDocumento.Add(areaDocumento);
                            if (Rpt_DLAT == null) throw new Exception("Fallo al Insertar T_DocumentoLegalAreaTrabajo");
                            else Rpt_DLAT = null;
                        }
                    }
                    else if (areasDocumentoBD.Count > DocumentoLegal.Areas.Count)
                    {
                        var diferencia = areasDocumentoBD.Except(DocumentoLegal.Areas).ToList();
                        //Eliminar diferencia
                        var eliminarItem = _repAreaDocumento.FirstBy(x => x.IdDocumentoLegal == idDocumento && x.IdPersonalAreaTrabajo == diferencia.ElementAt(0));
                        var RptD_DLAT = false;
                        RptD_DLAT = _serAreaDocumento.Delete(eliminarItem.Id, DocumentoLegal.Usuario);
                        if (RptD_DLAT == false) throw new Exception("Fallo al Eliminar T_DocumentoLegalAreaTrabajo -- Eliminar Direferencia");

                    }
                    else if (areasDocumentoBD.Count == 1 && DocumentoLegal.Areas.Count == 1)

                    {
                        var diferenciaInsertar = DocumentoLegal.Areas.ToList().Except(areasDocumentoBD).ToList();
                        if (diferenciaInsertar.Count > 0)
                        {
                            var diferenciaEliminar = areasDocumentoBD.Except(DocumentoLegal.Areas).ToList();
                            var eliminarItem = _repAreaDocumento.FirstBy(x => x.IdDocumentoLegal == idDocumento && x.IdPersonalAreaTrabajo == diferenciaEliminar.ElementAt(0));
                            var RptD_DLAT = false;
                            RptD_DLAT = _serAreaDocumento.Delete(eliminarItem.Id, DocumentoLegal.Usuario);
                            if (RptD_DLAT == false) throw new Exception("Fallo al Eliminar T_DocumentoLegalAreaTrabajo");
                            foreach (var item in diferenciaInsertar)
                            {
                                DocumentoLegalAreaTrabajo areaDocumento = new DocumentoLegalAreaTrabajo()
                                {
                                    IdDocumentoLegal = idDocumento,
                                    IdPersonalAreaTrabajo = item,
                                    Estado = true,
                                    UsuarioCreacion = Usuario,
                                    UsuarioModificacion = Usuario,
                                    FechaCreacion = DateTime.Now,
                                    FechaModificacion = DateTime.Now
                                };
                                var RptA_DLAT = _serAreaDocumento.Add(areaDocumento);
                                if (RptA_DLAT == null) throw new Exception("Fallo al insertar T_DocumentoLegalAreaTrabajo");
                                else RptA_DLAT = null;
                            }
                        }
                    }
                    var paiseDocumentoBD = _repPaisDocumento.GetBy(x => x.IdDocumentoLegal == idDocumento).Select(z => z.Id).ToList();
                    var RptD_DLP = false;
                    RptD_DLP = _serPaisDocumento.Delete(paiseDocumentoBD, DocumentoLegal.Usuario);
                    if (RptD_DLP == false) throw new Exception("Fallo al Eliminar T_DocumentoLegalPais");
                    foreach (var item in DocumentoLegal.Paises)
                    {
                        DocumentoLegalPais paisDocumento = new DocumentoLegalPais();
                        paisDocumento.IdDocumentoLegal = idDocumento;
                        paisDocumento.IdPais = item;
                        paisDocumento.Estado = true;
                        paisDocumento.UsuarioCreacion = Usuario;
                        paisDocumento.UsuarioModificacion = Usuario;
                        paisDocumento.FechaCreacion = DateTime.Now;
                        paisDocumento.FechaModificacion = DateTime.Now;

                        var RPTA_DLP = _serPaisDocumento.Add(paisDocumento);
                        if (RPTA_DLP == null) throw new Exception("Fallo al Insertar T_DocumentoLegalPais");
                        else RPTA_DLP = null;
                    }
                }
                else throw new Exception("Fallo al Actualizar T_DocumentoLegal");
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 22/07/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina los documentos legales
        /// </summary>
        /// <returns> true , ó error segun el nivel. </returns>
        /// <param name="IdDocumentoLegal">Identificador del T_DocumentoLegal</param>
        /// /// <param name="Usuario">Nombre del Usuario</param>
        public bool EliminarDocumentoLegal(int IdDocumentoLegal, string Usuario)
        {
            try
            {
                var _serAreaDocumento = new DocumentoLegalAreaTrabajoService(_unitOfWork);
                var _repDocumentoArea = _unitOfWork.DocumentoLegalAreaTrabajoRepository;
                var _serPaisDocumento = new DocumentoLegalPaisService(_unitOfWork);
                var _repDocumentoPais = _unitOfWork.DocumentoLegalPaisRepository;
                var areasDocumentoBD = _repDocumentoArea.GetBy(x => x.IdDocumentoLegal == IdDocumentoLegal).ToList();
                var paisDocumentoBD = _repDocumentoPais.GetBy(x => x.IdDocumentoLegal == IdDocumentoLegal).ToList();
                var RPT_ADOC = false;

                foreach (var item in areasDocumentoBD)
                {
                    RPT_ADOC = _serAreaDocumento.Delete(item.Id, Usuario);
                    if (RPT_ADOC == false) throw new Exception("Fallo al Eliminar T_DocumentoLegalAreaTrabajo");
                    else RPT_ADOC = false;
                }
                foreach (var item in paisDocumentoBD)
                {
                    RPT_ADOC = _serPaisDocumento.Delete(item.Id, Usuario);
                    if (RPT_ADOC == false) throw new Exception("Fallo al Eliminar T_DocumentoLegalPais");
                    else RPT_ADOC = false;
                }

                var RPT_DOC = false;
                RPT_DOC = this.Delete(IdDocumentoLegal, Usuario);
                if (RPT_DOC == false) throw new Exception("Fallo al Eliminar T_DocumentoLegal");
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
