using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: DocumentoPwService
    /// Autor: Jonathan Caipo
    /// Fecha: 20/09/2023
    /// <summary>
    /// Gestión general de T_Documento_PW
    /// </summary>
    public class DocumentoPwService : IDocumentoPwService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public DocumentoPwService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<DocumentoPw, DocumentoPwDTO>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Documento_PW.
        /// </summary>
        /// <returns> List<DocumentoPw> </returns>
        public IEnumerable<DocumentoPwDTO> Obtener()
        {
            try
            {
                return _unitOfWork.DocumentoPwRepository.Obtener().ToList();
            }
            catch
            {
                throw;
            }
        }


        public IEnumerable<DocumentoPwVersionesDTO> ObtenerIntroduccionVersionDocumento(int idDocumentoPW)
        {
            return _unitOfWork.DocumentoPwRepository.ObtenerIntroduccionVersionDocumento(idDocumentoPW);
        }

        /// Autor: Jonathan Caipo
        /// Fecha: 20/09/2023
        /// Version: 1.0
        /// <summary>
        /// Inserta un documento
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns> Entidad - DocumentoPw </returns>
        public async Task<DocumentoPw> InsertarDocumento(CompuestoDocumentoDTO dto, IFormFile? archivoInstruccion, IFormFile? archivoCalificacion, string usuario)
        {
            try
            {
                DocumentoPw documento = new DocumentoPw();

                // Subir archivos antes del INSERT para tener las URLs listas en la entidad
                if (archivoInstruccion != null && archivoInstruccion.Length > 0)
                {
                    byte[] bytes;
                    using (var ms = new MemoryStream())
                    {
                        await archivoInstruccion.CopyToAsync(ms);
                        bytes = ms.ToArray();
                    }
                    documento.UrlArchivoInstruccionTarea = await _unitOfWork.DocumentoPwRepository
                        .SubirArchivoDocumentoPw(bytes, archivoInstruccion.ContentType, archivoInstruccion.FileName);
                }

                if (archivoCalificacion != null && archivoCalificacion.Length > 0)
                {
                    byte[] bytes;
                    using (var ms = new MemoryStream())
                    {
                        await archivoCalificacion.CopyToAsync(ms);
                        bytes = ms.ToArray();
                    }
                    documento.UrlArchivoCalificacionExcelente = await _unitOfWork.DocumentoPwRepository
                        .SubirArchivoDocumentoPw(bytes, archivoCalificacion.ContentType, archivoCalificacion.FileName);
                }

                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    documento.Nombre = dto.ObjetoDocumento.Nombre;
                    documento.IdPlantillaPw = dto.ObjetoDocumento.IdPlantillaPw;
                    documento.EstadoFlujo = dto.ObjetoDocumento.EstadoFlujo;
                    documento.Asignado = dto.ObjetoDocumento.Asignado;
                    documento.Estado = true;
                    documento.UsuarioCreacion = usuario;
                    documento.UsuarioModificacion = usuario;
                    documento.FechaCreacion = DateTime.Now;
                    documento.FechaModificacion = DateTime.Now;

                    documento.DocumentoSeccionPws = new List<DocumentoSeccionPw>();
                    documento.BandejaPendientePws = new List<BandejaPendientePw>();

                    foreach (var item in dto.Lista)
                    {
                        if (item.listaGridListaSecciones.Count() > 0)
                        {
                            foreach (var subseccion in item.listaGridListaSecciones)
                            {
                                DocumentoSeccionPw documentoSeccion = new DocumentoSeccionPw();
                                documentoSeccion.IdDocumentoPw = dto.ObjetoDocumento.Id;
                                if (item.Tipo == 1)
                                {
                                    documentoSeccion.IdSeccionPw = item.IdSeccionMaestraPw;
                                    documentoSeccion.Titulo = "Titulo Maestra";
                                }
                                else
                                {
                                    documentoSeccion.IdSeccionPw = item.Id;
                                    if (string.IsNullOrEmpty(item.Titulo))
                                        documentoSeccion.Titulo = "Titulo";
                                    else
                                        documentoSeccion.Titulo = item.Titulo;
                                }
                                documentoSeccion.IdPlantillaPw = item.IdPlantillaPw;
                                documentoSeccion.Posicion = item.Posicion;
                                documentoSeccion.Tipo = item.Tipo;
                                documentoSeccion.Cabecera = item.Cabecera;
                                documentoSeccion.PiePagina = item.PiePagina;
                                documentoSeccion.Contenido = subseccion.Valor;
                                documentoSeccion.IdSeccionTipoDetallePw = Int32.Parse(subseccion.Clave.Substring(1, subseccion.Clave.IndexOf("_", 1) - 1));
                                documentoSeccion.NumeroFila = subseccion.NumeroFila;

                                documentoSeccion.VisibleWeb = item.VisibleWeb;
                                documentoSeccion.ZonaWeb = item.ZonaWeb;
                                documentoSeccion.OrdenWeb = item.OrdenEeb;

                                documentoSeccion.Estado = true;
                                documentoSeccion.UsuarioCreacion = usuario;
                                documentoSeccion.UsuarioModificacion = usuario;
                                documentoSeccion.FechaCreacion = DateTime.Now;
                                documentoSeccion.FechaModificacion = DateTime.Now;

                                documento.DocumentoSeccionPws.Add(documentoSeccion);
                            }

                        }
                        else if (item.IdSeccionTipoContenido != 1)
                        {

                            byte[] _base64 = Convert.FromBase64String(item.Contenido);
                            var _contenido = Encoding.UTF8.GetString(_base64);
                            DocumentoSeccionPw documentoSeccion = new DocumentoSeccionPw();
                            documentoSeccion.IdDocumentoPw = dto.ObjetoDocumento.Id;
                            if (item.Tipo == 1)
                            {
                                documentoSeccion.IdSeccionPw = item.IdSeccionMaestraPw;
                                documentoSeccion.Titulo = "Titulo Maestra";
                            }
                            else
                            {
                                documentoSeccion.IdSeccionPw = item.Id;
                                if (string.IsNullOrEmpty(item.Titulo))
                                    documentoSeccion.Titulo = "Titulo";
                                else
                                    documentoSeccion.Titulo = item.Titulo;
                            }
                            documentoSeccion.IdPlantillaPw = item.IdPlantillaPw;
                            documentoSeccion.Posicion = item.Posicion;
                            documentoSeccion.Tipo = item.Tipo;
                            documentoSeccion.Cabecera = item.Cabecera;
                            documentoSeccion.PiePagina = item.PiePagina;
                            documentoSeccion.Contenido = _contenido;

                            documentoSeccion.VisibleWeb = item.VisibleWeb;
                            documentoSeccion.ZonaWeb = item.ZonaWeb;
                            documentoSeccion.OrdenWeb = item.OrdenEeb;

                            documentoSeccion.Estado = true;
                            documentoSeccion.UsuarioCreacion = usuario;
                            documentoSeccion.UsuarioModificacion = usuario;
                            documentoSeccion.FechaCreacion = DateTime.Now;
                            documentoSeccion.FechaModificacion = DateTime.Now;

                            documento.DocumentoSeccionPws.Add(documentoSeccion);
                        }

                    }
                    var resultado = _unitOfWork.DocumentoPwRepository.Add(documento);
                    _unitOfWork.Commit();

                    var id = resultado.Id;
                    documento.Id = id;
                    _unitOfWork.DocumentoPwRepository.InsertarDocumentoPwModalidad(dto.SeccionModalidadHorario, id, usuario);
                    _unitOfWork.DocumentoPwRepository.InsertarDocumentoPwDuracion(dto.SeccionDuracion, id, usuario);
                    _unitOfWork.DocumentoPwRepository.InsertarDocumentoPwFechaInicio(dto.SeccionFechaInicio, id, usuario);
                    _unitOfWork.DocumentoPwRepository.InsertarDocumentoPwNotas(dto.SeccionNotas, id, usuario);
                    scope.Complete();


                }
                return documento;
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
        /// Actualiza documentos
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public async Task<DocumentoPw> ActualizarDocumento(CompuestoDocumentoPwDTO dto, IFormFile? archivoInstruccion, IFormFile? archivoCalificacion, string usuario)
        {
            try
            {
                IDocumentoSeccionPwService documentoSeccionPwService = new DocumentoSeccionPwService(_unitOfWork);
                IBandejaPendientePwService bandejaPendientePwService = new BandejaPendientePwService(_unitOfWork);

                DocumentoPw documentoPw = new DocumentoPw();
                List<DocumentoSeccionPw> documentoSeccionPw = new List<DocumentoSeccionPw>();

                string urlInstruccion = string.Empty;
                string urlCalificacion = string.Empty;

                if (archivoInstruccion != null && archivoInstruccion.Length > 0)
                {
                    byte[] bytes;
                    using (var ms = new MemoryStream()) { await archivoInstruccion.CopyToAsync(ms); bytes = ms.ToArray(); }
                    urlInstruccion = await _unitOfWork.DocumentoPwRepository.SubirArchivoDocumentoPw(bytes, archivoInstruccion.ContentType, archivoInstruccion.FileName);
                }
                if (archivoCalificacion != null && archivoCalificacion.Length > 0)
                {
                    byte[] bytes;
                    using (var ms = new MemoryStream()) { await archivoCalificacion.CopyToAsync(ms); bytes = ms.ToArray(); }
                    urlCalificacion = await _unitOfWork.DocumentoPwRepository.SubirArchivoDocumentoPw(bytes, archivoCalificacion.ContentType, archivoCalificacion.FileName);
                }

                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    documentoSeccionPwService.EliminacionDocumentoSeccionLogicoPorIdDocumento(dto.ObjetoDocumento.Id, usuario, dto.Lista);
                    //bandejaPendientePwService.EliminacionBandejaPendienteLogicoPorIdDocumento(dto.ObjetoDocumento.Id, usuario, dto.ListaRevision);

                    documentoPw = _unitOfWork.DocumentoPwRepository.ObtenerPorId(dto.ObjetoDocumento.Id);
                    documentoPw.Nombre = dto.ObjetoDocumento.Nombre;
                    documentoPw.IdPlantillaPw = dto.ObjetoDocumento.IdPlantillaPw;
                    documentoPw.EstadoFlujo = dto.ObjetoDocumento.EstadoFlujo;
                    documentoPw.Asignado = dto.ObjetoDocumento.Asignado;
                    // Instruccion: nuevo archivo → URL nueva; front sin URL → eliminado → null; misma URL → sin cambio
                    if (!string.IsNullOrEmpty(urlInstruccion))
                        documentoPw.UrlArchivoInstruccionTarea = urlInstruccion;
                    else if (string.IsNullOrEmpty(dto.ObjetoDocumento.UrlArchivoInstruccionTarea))
                        documentoPw.UrlArchivoInstruccionTarea = null;

                    // Calificacion: nuevo archivo → URL nueva; front sin URL → eliminado → null; misma URL → sin cambio
                    if (!string.IsNullOrEmpty(urlCalificacion))
                        documentoPw.UrlArchivoCalificacionExcelente = urlCalificacion;
                    else if (string.IsNullOrEmpty(dto.ObjetoDocumento.UrlArchivoCalificacionExcelente))
                        documentoPw.UrlArchivoCalificacionExcelente = null;
                    documentoPw.UsuarioModificacion = usuario;
                    documentoPw.FechaModificacion = DateTime.Now;

                    documentoPw.DocumentoSeccionPws = new List<DocumentoSeccionPw>();
                    documentoPw.BandejaPendientePws = new List<BandejaPendientePw>();


                    List<DocumentoSeccionPw> documentos = new List<DocumentoSeccionPw>();
                    foreach (var item in dto.Lista)
                    {
                        DocumentoSeccionPw documentoSeccion;
                        var temporal2 = _unitOfWork.DocumentoSeccionPwRepository.Exist(w => w.IdSeccionPw == item.IdSeccionPW && w.IdDocumentoPw == dto.ObjetoDocumento.Id);

                        if (temporal2)
                        {
                            if (item.listaGridListaSecciones.Count() > 0)  //&& item.IdSeccionPW == 91
                            {
                                foreach (var item2 in item.listaGridListaSecciones)
                                {
                                    var benExist = _unitOfWork.DocumentoSeccionPwRepository.Exist(w => w.IdSeccionPw == item.IdSeccionPW && w.IdDocumentoPw == dto.ObjetoDocumento.Id && w.Contenido == item2.Valor && w.Estado == true);
                                    var fila = item2.NumeroFila.Value;
                                    if (!benExist)
                                    {
                                        documentoSeccion = new DocumentoSeccionPw();
                                        if (string.IsNullOrEmpty(item.Titulo))
                                            documentoSeccion.Titulo = "Titulo";
                                        else
                                            documentoSeccion.Titulo = item.Titulo;

                                        documentoSeccion.Cabecera = item.Cabecera;
                                        documentoSeccion.PiePagina = item.PiePagina;
                                        documentoSeccion.Contenido = item2.Valor;
                                        documentoSeccion.NumeroFila = fila;
                                        documentoSeccion.IdSeccionTipoDetallePw = int.Parse(item2.Clave.Substring(1, item2.Clave.IndexOf("_", 1) - 1));
                                        documentoSeccion.IdPlantillaPw = item.IdPlantillaPw;
                                        documentoSeccion.Posicion = item.Posicion;
                                        documentoSeccion.Tipo = item.Tipo;
                                        documentoSeccion.IdDocumentoPw = item.IdDocumentoPW;
                                        documentoSeccion.IdSeccionPw = item.IdSeccionPW;
                                        documentoSeccion.VisibleWeb = item.VisibleWeb;
                                        documentoSeccion.ZonaWeb = item.ZonaWeb;
                                        documentoSeccion.OrdenWeb = item.OrdenEeb;
                                        documentoSeccion.UsuarioCreacion = usuario;
                                        documentoSeccion.UsuarioModificacion = usuario;
                                        documentoSeccion.FechaCreacion = DateTime.Now;
                                        documentoSeccion.FechaModificacion = DateTime.Now;
                                        documentoSeccion.Estado = true;
                                        documentoPw.DocumentoSeccionPws.Add(documentoSeccion);
                                    }
                                    else
                                    {
                                        documentoSeccion = _unitOfWork.DocumentoSeccionPwRepository.ObtenerIdSeccionIdDocumentoContenido(item.IdSeccionPW, dto.ObjetoDocumento.Id, item2.Valor);
                                        bool actualizarCabecera = false, actualizarPie = false;

                                        if (documentoSeccion != null)
                                        {
                                            if (documentoSeccion.Cabecera != item.Cabecera)
                                            {
                                                documentoSeccion.Cabecera = item.Cabecera;
                                                actualizarCabecera = true;
                                            }
                                            if (documentoSeccion.PiePagina != item.PiePagina)
                                            {
                                                documentoSeccion.PiePagina = item.PiePagina;
                                                actualizarPie = true;
                                            }
                                            if (actualizarCabecera || actualizarPie)
                                            {
                                                documentoSeccion.UsuarioModificacion = usuario;
                                                documentoSeccion.FechaModificacion = DateTime.Now;
                                            }
                                            documentoSeccion.NumeroFila = fila;
                                            documentoSeccionPw.Add(documentoSeccion);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                documentoSeccion = _unitOfWork.DocumentoSeccionPwRepository.ObtenerPorIdSeccionIdDocumento(item.Id, dto.ObjetoDocumento.Id);
                                if (documentoSeccion != null)
                                {
                                    byte[] _base64 = Convert.FromBase64String(item.Contenido);
                                    var _contenido = Encoding.UTF8.GetString(_base64);
                                    if (string.IsNullOrEmpty(item.Titulo))
                                        documentoSeccion.Titulo = "Titulo";
                                    else
                                        documentoSeccion.Titulo = item.Titulo;
                                    documentoSeccion.Cabecera = item.Cabecera;
                                    documentoSeccion.PiePagina = item.PiePagina;
                                    documentoSeccion.Contenido = _contenido;
                                    documentoSeccion.IdSeccionTipoDetallePw = item.IdSeccionTipoDetallePw;
                                    documentoSeccion.IdPlantillaPw = item.IdPlantillaPw;
                                    documentoSeccion.Posicion = item.Posicion;
                                    documentoSeccion.Tipo = item.Tipo;
                                    documentoSeccion.IdSeccionPw = item.IdSeccionPW;
                                    documentoSeccion.VisibleWeb = item.VisibleWeb;
                                    documentoSeccion.ZonaWeb = item.ZonaWeb;
                                    documentoSeccion.OrdenWeb = item.OrdenEeb;
                                    documentoSeccion.UsuarioModificacion = usuario;
                                    documentoSeccion.FechaModificacion = DateTime.Now;
                                    documentoPw.DocumentoSeccionPws.Add(documentoSeccion);
                                }
                            }
                        }
                        else
                        {
                            if (item.listaGridListaSecciones.Count() > 0)
                            {
                                if (item.Titulo == "Estructura Curricular")
                                {
                                    item.listaGridListaSecciones = item.listaGridListaSecciones.OrderBy(x => x.NumeroFila).ToList();
                                }
                                foreach (var item2 in item.listaGridListaSecciones)
                                {
                                    documentoSeccion = new DocumentoSeccionPw();

                                    if (string.IsNullOrEmpty(item.Titulo))
                                        documentoSeccion.Titulo = "Titulo";
                                    else
                                        documentoSeccion.Titulo = item.Titulo;
                                    documentoSeccion.Cabecera = item.Cabecera;
                                    documentoSeccion.PiePagina = item.PiePagina;
                                    documentoSeccion.Contenido = item2.Valor;
                                    documentoSeccion.NumeroFila = item2.NumeroFila;
                                    documentoSeccion.IdSeccionTipoDetallePw = int.Parse(item2.Clave.Substring(1, item2.Clave.IndexOf("_", 1) - 1));
                                    documentoSeccion.IdPlantillaPw = item.IdPlantillaPw;
                                    documentoSeccion.Posicion = item.Posicion;
                                    documentoSeccion.Tipo = item.Tipo;
                                    documentoSeccion.IdDocumentoPw = item.IdDocumentoPW;
                                    documentoSeccion.IdSeccionPw = item.IdSeccionPW;
                                    documentoSeccion.VisibleWeb = item.VisibleWeb;
                                    documentoSeccion.ZonaWeb = item.ZonaWeb;
                                    documentoSeccion.OrdenWeb = item.OrdenEeb;
                                    documentoSeccion.UsuarioCreacion = usuario;
                                    documentoSeccion.UsuarioModificacion = usuario;
                                    documentoSeccion.FechaCreacion = DateTime.Now;
                                    documentoSeccion.FechaModificacion = DateTime.Now;
                                    documentoSeccion.Estado = true;
                                    documentoPw.DocumentoSeccionPws.Add(documentoSeccion);
                                }
                            }
                            else if (item.IdSeccionTipoContenido != 1)
                            {
                                byte[] _base64 = Convert.FromBase64String(item.Contenido);
                                var _contenido = Encoding.UTF8.GetString(_base64);
                                documentoSeccion = new DocumentoSeccionPw();
                                if (string.IsNullOrEmpty(item.Titulo))
                                    documentoSeccion.Titulo = "Titulo";
                                else
                                    documentoSeccion.Titulo = item.Titulo;
                                documentoSeccion.Cabecera = item.Cabecera;
                                documentoSeccion.PiePagina = item.PiePagina;
                                documentoSeccion.Contenido = _contenido;
                                documentoSeccion.IdSeccionTipoDetallePw = item.IdSeccionTipoDetallePw;
                                documentoSeccion.IdPlantillaPw = item.IdPlantillaPw;
                                documentoSeccion.Posicion = item.Posicion;
                                documentoSeccion.Tipo = item.Tipo;
                                documentoSeccion.IdSeccionPw = item.IdSeccionPW;
                                documentoSeccion.VisibleWeb = item.VisibleWeb;
                                documentoSeccion.ZonaWeb = item.ZonaWeb;
                                documentoSeccion.OrdenWeb = item.OrdenEeb;
                                documentoSeccion.UsuarioCreacion = usuario;
                                documentoSeccion.UsuarioModificacion = usuario;
                                documentoSeccion.FechaCreacion = DateTime.Now;
                                documentoSeccion.FechaModificacion = DateTime.Now;
                                documentoSeccion.Estado = true;
                                documentoPw.DocumentoSeccionPws.Add(documentoSeccion);
                            }
                        }
                    }

                    _unitOfWork.DetachAll();
                    _unitOfWork.DocumentoPwRepository.Update(documentoPw);
                    _unitOfWork.Commit();


                    scope.Complete();
                }
                if (dto.SeccionModalidadHorario != null)
                {
                    ActualizarSeccionModalidadHorario(dto.SeccionModalidadHorario, dto.ObjetoDocumento.Id, usuario);
                }
                if (dto.SeccionDuracion != null)
                {
                    ActualizarSeccionDuracion(dto.SeccionDuracion, dto.ObjetoDocumento.Id, usuario);
                }
                if (dto.SeccionFechaInicio != null)
                {
                    ActualizarSeccionFechaInicio(dto.SeccionFechaInicio, dto.ObjetoDocumento.Id, usuario);
                }
                if (dto.SeccionNotas != null)
                {
                    ActualizarSeccionNotas(dto.SeccionNotas, dto.ObjetoDocumento.Id, usuario);
                }
                _unitOfWork.DocumentoPwRepository.RegularizaIntroduccionPrerrequisito(dto.ObjetoDocumento.Id, usuario);
                if (dto.ListaIntroduccionBeneficios != null)
                {
                    IntDTO value = _unitOfWork.DocumentoPwRepository.ValidarSiTieneRegistro(dto.ObjetoDocumento.Id);
                    if (value?.Valor == null || value?.Valor == 0)
                    {
                        foreach (var element in dto.ListaIntroduccionBeneficios)
                        {
                            DocumentoPwVersionesDTO entidad = new()
                            {
                                IdVersionPrograma = element.IdVersionPrograma,
                                Introduccion = element.Introduccion,
                                IdDocumentoPw = dto.ObjetoDocumento.Id
                            };
                            if (entidad != null)
                            {
                                _unitOfWork.DocumentoPwRepository.InsertarVersionesBeneficiosDocumentosPw(entidad, usuario);
                            }
                        }
                    }
                    else
                    {
                        string introduccion = "";
                        foreach (var element in dto.ListaIntroduccionBeneficios)
                        {
                            StringDTO intro = _unitOfWork.DocumentoPwRepository.ValidarSiTieneIntroduccionLaVersion(dto.ObjetoDocumento.Id, element.IdVersionPrograma);
                            if (element.Introduccion != intro.Valor)
                            {
                                introduccion = element.Introduccion;
                                _unitOfWork.DocumentoPwRepository.ActualizarIntroduccionBeneficioDocumentoPw(introduccion, dto.ObjetoDocumento.Id, element.IdVersionPrograma, usuario);
                            }

                        }
                    }
                }
                if (documentoSeccionPw.Count > 0)
                {
                    foreach (var item in documentoSeccionPw)
                    {
                        _unitOfWork.DetachAll();
                        _unitOfWork.DocumentoSeccionPwRepository.Update(item);
                        _unitOfWork.Commit();
                    }
                }


                return documentoPw;
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
        /// Elimina documento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="usuario"></param>
        /// <returns> bool </returns>
        public bool EliminarDocumento(int id, string usuario)
        {
            try
            {
                var validar = _unitOfWork.DocumentoPwRepository.ObtenerPorId(id);

                if (validar != null)
                {
                    _unitOfWork.DocumentoPwRepository.Delete(id, usuario);
                    _unitOfWork.Commit();
                    var hijosDocumentoSeccion = _unitOfWork.DocumentoSeccionPwRepository.ObtenerDocumentoSeccionPorId(id);
                    foreach (var hijo in hijosDocumentoSeccion)
                    {
                        _unitOfWork.DocumentoSeccionPwRepository.Delete(hijo.Id, usuario);
                    }
                    var hijosBandejaPendiente = _unitOfWork.BandejaPendientePwRepository.ObtenerPorIdDocumento(id);
                    foreach (var hijo in hijosBandejaPendiente)
                    {
                        _unitOfWork.BandejaPendientePwRepository.Delete(hijo.Id, usuario);
                    }
                    return true;
                }
                else return false;
            }
            catch
            {
                throw;
            }
        }


        public IEnumerable<ModalidadPortalDTO> ObtenerModalidadPortal()
        {
            return _unitOfWork.DocumentoPwRepository.ObtenerModalidadPortal();
        }

        public IEnumerable<ComboDTO> ObtenerModoFechaInicio()
        {
            return _unitOfWork.DocumentoPwRepository.ObtenerModoFechaInicio();
        }
        public IEnumerable<ComboDTO> ObtenerNotasTipo()
        {
            return _unitOfWork.DocumentoPwRepository.ObtenerNotasTipo();
        }



        public SeccionModalidadHorarioResponseDTO? ObtenerDocumentoPWModalidad(int idDocumentoPW)
        {
            try
            {
                var rows = (_unitOfWork.DocumentoPwRepository.ObtenerDocumentoPWModalidadRows(idDocumentoPW) ?? Enumerable.Empty<DocumentoPWModalidadRowVM>())
                    .ToList();

                if (!rows.Any())
                    return null;

                var first = rows.First();

                var response = new SeccionModalidadHorarioResponseDTO
                {
                    IdDocumentoPw = first.IdDocumento_PW,
                    Introduccion = first.Introduccion,
                    Modalidades = rows
                        .GroupBy(x => new
                        {
                            x.IdDocumentoPWModalidad,
                            x.IdModalidadPortal,
                            x.SubTitulo,
                            x.Descripcion
                        })
                        .Select(g => new ModalidadHorarioResponseDTO
                        {
                            Id = g.Key.IdDocumentoPWModalidad,
                            IdModalidad = g.Key.IdModalidadPortal,
                            SubTitulo = g.Key.SubTitulo,
                            Descripcion = g.Key.Descripcion,
                            Detalles = g
                                .Where(x => (x.IdDocumentoPWModalidadDetalle ?? 0) > 0)
                                .Select(x => new ModalidadHorarioDetalleResponseDTO
                                {
                                    Id = x.IdDocumentoPWModalidadDetalle ?? 0,
                                    Orden = x.Orden ?? 0,
                                    Tipo = x.Tipo,
                                    IdPais = x.IdPais,
                                    Beneficio = x.Beneficio,
                                    Horario = x.Horario
                                })
                                .OrderBy(x => x.Orden)
                                .ToList()
                        })
                        .ToList()
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ObtenerDocumentoPWModalidad(service) {ex.Message}", ex);
            }
        }


        public SeccionDuracionDTO? ObtenerDocumentoPWDuracion(int idDocumentoPW)
        {
            try
            {
                var rows = (_unitOfWork.DocumentoPwRepository.ObtenerDocumentoPWDuracionRows(idDocumentoPW) ?? Enumerable.Empty<DocumentoPWDuracionRowVM>())
                    .ToList();

                if (!rows.Any())
                    return null;

                var first = rows.First();

                var response = new SeccionDuracionDTO
                {
                    IdDocumentoPw = first.IdDocumentoPW,
                    Titulo = first.Titulo,
                    Introduccion = first.Introduccion,
                    PieDePagina = first.PieDePagina,
                    Detalles = rows
                        .Where(x => (x.IdDocumentoPWDuracionDetalle ?? 0) > 0)
                        .Select(x => new DuracionDetalleDTO
                        {
                            Id = x.IdDocumentoPWDuracionDetalle ?? 0,
                            IdVersionPrograma = x.IdVersionPrograma,
                            Meses = x.DetalleMes,
                            Horas = x.DetalleHora
                        })
                        .ToList()
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ObtenerDocumentoPWDuracion(service) {ex.Message}", ex);
            }
        }


        public SeccionFechaInicioDTO ObtenerDocumentoPWFechaInicio(int idDocumentoPw)
        {
            try
            {
                var rows = _unitOfWork.DocumentoPwRepository.ObtenerDocumentoPWFechaInicioRows(idDocumentoPw);

                if (rows == null || !rows.Any())
                {
                    return new SeccionFechaInicioDTO
                    {
                        IdDocumentoPw = idDocumentoPw,
                        MostrarEnLaWeb = false,
                        Titulo = null,
                        SubTitulo = null,
                        Paises = new List<FechaInicioPaisDTO>(),
                        PaisesEliminados = new List<int>(),
                        DetallesEliminados = new List<int>()
                    };
                }

                var header = rows.First();

                var dto = new SeccionFechaInicioDTO
                {
                    IdDocumentoPw = idDocumentoPw,
                    MostrarEnLaWeb = header.MostrarEnLaWeb,
                    Titulo = header.Titulo,
                    SubTitulo = header.SubTitulo,
                    Paises = rows
                        .GroupBy(x => x.IdDocumentoPWFechaInicio)
                        .Select(g => new FechaInicioPaisDTO
                        {
                            Id = g.Key,
                            IdPais = g.First().IdPais,
                            Detalles = g
                                .Where(x => x.IdDetalle.HasValue && x.IdDetalle.Value > 0)
                                .OrderBy(x => x.IdDetalle.Value)
                                .Select(x => new FechaInicioDetalleDTO
                                {
                                    Id = x.IdDetalle!.Value,
                                    IdModo = x.IdModo,
                                    Fecha = x.Fecha,
                                    Horario = x.Horario
                                })
                                .ToList()
                        })
                        .ToList(),
                    PaisesEliminados = new List<int>(),
                    DetallesEliminados = new List<int>()
                };

                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ObtenerDocumentoPWFechaInicio() {ex.Message}", ex);
            }
        }


        public SeccionNotasDTO ObtenerDocumentoPWNotas(int idDocumentoPW)
        {
            try
            {
                var rows = _unitOfWork.DocumentoPwRepository.ObtenerDocumentoPWNotasRows(idDocumentoPW) ?? new List<DocumentoPWNotasRowDTO>();

                if (!rows.Any())
                {
                    return new SeccionNotasDTO
                    {
                        IdDocumentoPw = idDocumentoPW,
                        MostrarEnLaWeb = false,
                        Notas = new List<NotaDTOV2>()
                    };
                }

                var mostrar = rows.First().MostrarWeb;

                var notas = rows
                    .GroupBy(r => new { r.IdDocumentoPWNota, r.IdDocumentoPWNotaTipo, r.Descripcion })
                    .Select(g => new NotaDTOV2
                    {
                        Id = g.Key.IdDocumentoPWNota,
                        IdNotaTipo = g.Key.IdDocumentoPWNotaTipo,
                        Descripcion = g.Key.Descripcion,
                        Detalles = g
                            .Where(x => (x.IdDocumentoPWNotaDetalle ?? 0) > 0)
                            .Select(x => new NotaDetalleDTO
                            {
                                Id = x.IdDocumentoPWNotaDetalle ?? 0,
                                Orden = x.Orden ?? 0,
                                InformacionExtra = x.InformacionExtra,
                                Horario = x.Horario,
                                IdPais = x.IdPais
                            })
                            .OrderBy(x => x.Orden)
                            .ToList()
                    })
                    .OrderBy(x => x.Id)
                    .ToList();

                return new SeccionNotasDTO
                {
                    IdDocumentoPw = idDocumentoPW,
                    MostrarEnLaWeb = mostrar,
                    Notas = notas
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ObtenerDocumentoPWNotas() {ex.Message}", ex);
            }
        }

        public void ActualizarSeccionModalidadHorario(SeccionModalidadHorarioDTO? dto, int idDocumentoPw, string usuario)
        {
            try
            {
                if (dto == null) return;

                var jsonActual = _unitOfWork.DocumentoPwRepository.ObtenerDocumentoPWModalidadRowsSP(idDocumentoPw);
                var actuales = (!string.IsNullOrWhiteSpace(jsonActual) && !jsonActual.Contains("[]"))
                    ? JsonConvert.DeserializeObject<List<DocumentoPWModalidadRowDTO>>(jsonActual) ?? new List<DocumentoPWModalidadRowDTO>()
                    : new List<DocumentoPWModalidadRowDTO>();

                string Norm(string? s) => (s ?? "").Trim();


                var actualPorModalidad = actuales
                    .Where(x => x.IdDocumentoPwModalidad > 0)
                    .GroupBy(x => x.IdDocumentoPwModalidad)
                    .ToDictionary(g => g.Key, g => g.ToList());


                var idIntroActual = actuales.Select(x => x.IdDocumentoPwModalidadIntroduccion).FirstOrDefault(x => x.HasValue) ?? 0;
                var introActual = Norm(actuales.Select(x => x.Introduccion).FirstOrDefault());


                var introNuevo = Norm(dto.Introduccion);
                int idIntroUsar = idIntroActual;

                if (idIntroActual <= 0)
                {
                    if (!string.IsNullOrEmpty(introNuevo))
                    {
                        var rIntro = _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWModalidadIntroduccion_Insertar(introNuevo, usuario);

                        if (string.IsNullOrWhiteSpace(rIntro) || rIntro.Contains("[]"))
                            throw new Exception("No se pudo insertar Introducción de Modalidad.");

                        var arr = JArray.Parse(rIntro);
                        idIntroUsar = arr.Count > 0 ? (int)(arr[0]["Id"] ?? 0) : 0;

                        if (idIntroUsar <= 0)
                            throw new Exception("Id de Introducción inválido (Modalidad).");
                    }
                }
                else
                {
                    if (introActual != introNuevo)
                    {
                        _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWModalidadIntroduccion_Actualizar(idIntroActual, introNuevo, usuario);
                        idIntroUsar = idIntroActual;
                    }
                }


                foreach (var idDet in (dto.DetallesEliminados ?? new List<int>()).Distinct())
                {
                    if (idDet > 0)
                        _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWModalidadDetalle_Desactivar(idDet, usuario);
                }


                foreach (var idModElim in (dto.ModalidadesEliminadas ?? new List<int>()).Distinct())
                {
                    if (idModElim <= 0) continue;


                    if (actualPorModalidad.TryGetValue(idModElim, out var filasMod))
                    {
                        var idsDetalles = filasMod
                            .Select(x => x.IdDocumentoPwModalidadDetalle ?? 0)
                            .Where(x => x > 0)
                            .Distinct()
                            .ToList();

                        foreach (var idDet in idsDetalles)
                            _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWModalidadDetalle_Desactivar(idDet, usuario);
                    }

                    _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWModalidadConfiguracion_DesactivarPorModalidad(idDocumentoPw, idModElim, usuario);
                    _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWModalidad_Desactivar(idModElim, usuario);
                }

                foreach (var m in (dto.Modalidades ?? new List<ModalidadHorarioDTO>()))
                {
                    var idModalidadDoc = m.Id;

                    var idModalidadPortalNuevo = m.IdModalidad ?? 0;
                    var subNuevo = Norm(m.SubTitulo);
                    var desNuevo = Norm(m.Descripcion);

                    DocumentoPWModalidadRowDTO? modActual = null;
                    List<DocumentoPWModalidadRowDTO> filasActualesMod = new();

                    if (idModalidadDoc > 0 && actualPorModalidad.TryGetValue(idModalidadDoc, out var filas))
                    {
                        filasActualesMod = filas;
                        modActual = filasActualesMod.FirstOrDefault();
                    }

                    if (idModalidadDoc > 0)
                    {
                        if (modActual != null)
                        {
                            var cambio =
                                modActual.IdModalidadPortal != idModalidadPortalNuevo ||
                                Norm(modActual.SubTitulo) != subNuevo ||
                                Norm(modActual.Descripcion) != desNuevo;

                            if (cambio)
                            {
                                _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWModalidad_Actualizar(
                                    idModalidadDoc, idModalidadPortalNuevo, subNuevo, desNuevo, usuario
                                );
                            }
                        }
                    }
                    else
                    {

                        var rMod = _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWModalidad_Insertar(
                            idModalidadPortalNuevo, subNuevo, desNuevo, usuario
                        );

                        if (string.IsNullOrWhiteSpace(rMod) || rMod.Contains("[]"))
                            throw new Exception("No se pudo insertar Modalidad.");

                        var arr = JArray.Parse(rMod);
                        idModalidadDoc = arr.Count > 0 ? (int)(arr[0]["Id"] ?? 0) : 0;

                        if (idModalidadDoc <= 0)
                            throw new Exception("IdDocumentoPWModalidad inválido (insert).");
                    }


                    _unitOfWork.DocumentoPwRepository.SP_DocumentoPWModalidadConfiguracion_RegistrarCambios(
                        idDocumentoPw, idIntroUsar, idModalidadDoc, usuario
                    );


                    var detalles = (m.Detalles ?? new List<ModalidadHorarioDetalleDTO>())
                        .OrderBy(x => x.Orden)
                        .ToList();

                    var detActualPorId = filasActualesMod
                        .Where(x => (x.IdDocumentoPwModalidadDetalle ?? 0) > 0)
                        .ToDictionary(x => x.IdDocumentoPwModalidadDetalle!.Value, x => x);

                    foreach (var d in detalles)
                    {
                        var tipo = Norm(d.Tipo).ToUpper();

                        var idPaisNuevo = (tipo == "HORA") ? d.IdPais : null;
                        var beneficioNuevo = (tipo == "BENEFICIO") ? Norm(d.Beneficio) : null;
                        var horario = d.Horario;
                        if (tipo != "HORA" && tipo != "BENEFICIO")
                        {
                            tipo = "BENEFICIO";
                            beneficioNuevo = Norm(d.Beneficio);
                            idPaisNuevo = null;
                        }

                        if (d.Id > 0)
                        {
                            if (detActualPorId.TryGetValue(d.Id, out var detActual))
                            {

                                var cambio =
                                    (detActual.Orden ?? 0) != d.Orden ||
                                    Norm(detActual.Tipo).ToUpper() != tipo ||
                                    (detActual.IdPais ?? 0) != (idPaisNuevo ?? 0) ||
                                    Norm(detActual.Beneficio) != Norm(beneficioNuevo) ||
                                    Norm(detActual.Horario) != Norm(horario);

                                if (cambio)
                                {

                                    _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWModalidadDetalle_Actualizar(
                                        d.Id, idModalidadDoc, d.Orden, tipo, beneficioNuevo, idPaisNuevo, horario, usuario
                                    );
                                }
                            }
                            else
                            {

                                _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWModalidadDetalle_Actualizar(
                                    d.Id, idModalidadDoc, d.Orden, tipo, beneficioNuevo, idPaisNuevo, horario, usuario
                                );
                            }
                        }
                        else
                        {

                            _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWModalidadDetalle_Insertar(
                                idModalidadDoc, d.Orden, tipo, beneficioNuevo, idPaisNuevo, horario, usuario
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ActualizarSeccionModalidadHorario() {ex.Message}", ex);
            }
        }


        public void ActualizarSeccionDuracion(SeccionDuracionDTO? dto, int idDocumentoPw, string usuario)
        {
            try
            {
                if (dto == null) return;

                string Norm(string? s) => (s ?? "").Trim();

                var jsonActual = _unitOfWork.DocumentoPwRepository.ObtenerDocumentoPWDuracionRowsSP(idDocumentoPw);
                var actuales = (!string.IsNullOrWhiteSpace(jsonActual) && !jsonActual.Contains("[]"))
                    ? JsonConvert.DeserializeObject<List<DocumentoPWDuracionRowDTO>>(jsonActual) ?? new List<DocumentoPWDuracionRowDTO>()
                    : new List<DocumentoPWDuracionRowDTO>();

                var idDuracionActual = actuales.Select(x => x.IdDocumentoPWDuracion).FirstOrDefault(x => x.HasValue) ?? 0;

                var tituloActual = Norm(actuales.Select(x => x.Titulo).FirstOrDefault());
                var introActual = Norm(actuales.Select(x => x.Introduccion).FirstOrDefault());
                var pieActual = Norm(actuales.Select(x => x.PieDePagina).FirstOrDefault());

                var tituloNuevo = Norm(dto.Titulo);
                var introNuevo = Norm(dto.Introduccion);
                var pieNuevo = Norm(dto.PieDePagina);

                var detallesActuales = actuales
                    .Where(x => (x.IdDocumentoPWDuracionDetalle ?? 0) > 0)
                    .ToDictionary(x => x.IdDocumentoPWDuracionDetalle!.Value, x => x);

                var idDuracionUsar = idDuracionActual;

                if (idDuracionActual <= 0)
                {
                    var rIns = _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWDuracion_Insertar(tituloNuevo, introNuevo, pieNuevo, usuario);

                    if (string.IsNullOrWhiteSpace(rIns) || rIns.Contains("[]"))
                        throw new Exception("No se pudo insertar Duración.");

                    var arr = JArray.Parse(rIns);
                    idDuracionUsar = arr.Count > 0 ? (int)(arr[0]["Id"] ?? 0) : 0;

                    if (idDuracionUsar <= 0)
                        throw new Exception("IdDocumentoPWDuracion inválido (insert).");
                }
                else
                {
                    var cambioCabecera =
                        tituloActual != tituloNuevo ||
                        introActual != introNuevo ||
                        pieActual != pieNuevo;

                    if (cambioCabecera)
                    {
                        _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWDuracion_Actualizar(
                            idDuracionActual, tituloNuevo, introNuevo, pieNuevo, usuario
                        );
                    }
                }

                _unitOfWork.DocumentoPwRepository.SP_DocumentoPWDuracionConfiguracion_RegistrarCambios(
                    idDocumentoPw, idDuracionUsar, usuario
                );

                foreach (var idDetElim in (dto.DetallesEliminados ?? new List<int>()).Distinct())
                {
                    if (idDetElim > 0)
                        _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWDuracionDetalle_Desactivar(idDetElim, usuario);
                }

                var detalles = (dto.Detalles ?? new List<DuracionDetalleDTO>()).ToList();

                foreach (var d in detalles)
                {
                    var idDet = d.Id;
                    var idVersion = d.IdVersionPrograma;
                    var meses = Norm(d.Meses);
                    var horas = Norm(d.Horas);

                    if (idDet > 0)
                    {
                        if (detallesActuales.TryGetValue(idDet, out var act))
                        {
                            var cambio =
                                (act.IdVersionPrograma ?? 0) != idVersion ||
                                Norm(act.DetalleMes) != meses ||
                                Norm(act.DetalleHora) != horas;

                            if (cambio)
                            {
                                _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWDuracionDetalle_Actualizar(
                                    idDet, idDuracionUsar, idVersion.Value, meses, horas, usuario
                                );
                            }
                        }
                        else
                        {
                            _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWDuracionDetalle_Actualizar(
                                idDet, idDuracionUsar, idVersion.Value, meses, horas, usuario
                            );
                        }
                    }
                    else
                    {
                        _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWDuracionDetalle_Insertar(
                            idDuracionUsar, idVersion.Value, meses, horas, usuario
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ActualizarSeccionDuracion() {ex.Message}", ex);
            }
        }


        private static int GetIdFromSpResult(string json, string errorIfFail)
        {
            if (string.IsNullOrWhiteSpace(json) || json.Contains("[]"))
                throw new Exception(errorIfFail);

            var arr = JArray.Parse(json);
            var id = (arr.Count > 0) ? (int)(arr[0]["Id"] ?? 0) : 0;

            if (id <= 0) throw new Exception(errorIfFail);
            return id;
        }

        public string ObtenerDocumentoPWFechaInicioV2(int idDocumentoPw)
        {
            try
            {
                var json = _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWFechaInicio_ObtenerRows(idDocumentoPw);

                var filas = (!string.IsNullOrWhiteSpace(json) && !json.Contains("[]"))
                    ? JsonConvert.DeserializeObject<List<DocumentoPWFechaInicioRowDTOV2>>(json) ?? new List<DocumentoPWFechaInicioRowDTOV2>()
                    : new List<DocumentoPWFechaInicioRowDTOV2>();

                var dto = new SeccionFechaInicioDTO
                {
                    IdDocumentoPw = idDocumentoPw
                };

                if (!filas.Any())
                    return JsonConvert.SerializeObject(dto);

                var first = filas.First();
                dto.MostrarEnLaWeb = first.MostrarEnLaWeb ?? false;
                dto.Titulo = first.Titulo;
                dto.SubTitulo = first.SubTitulo;

                dto.Paises = filas
                    .Where(x => (x.IdDocumentoPWFechaInicio ?? 0) > 0)
                    .GroupBy(x => x.IdDocumentoPWFechaInicio!.Value)
                    .Select(g =>
                    {
                        var row0 = g.First();
                        var pais = new FechaInicioPaisDTO
                        {
                            Id = g.Key,
                            IdPais = row0.IdPais
                        };

                        pais.Detalles = g
                            .Where(x => (x.IdDocumentoPWFechaInicioDetalle ?? 0) > 0)
                            .Select(x => new FechaInicioDetalleDTO
                            {
                                Id = x.IdDocumentoPWFechaInicioDetalle ?? 0,
                                IdModo = x.IdModo,
                                Fecha = x.Fecha,
                                Horario = x.Horario
                            })
                            .ToList();

                        return pais;
                    })
                    .ToList();

                return JsonConvert.SerializeObject(dto);
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ObtenerDocumentoPWFechaInicio() {ex.Message}", ex);
            }
        }

        public void InsertarDocumentoPwFechaInicio(SeccionFechaInicioDTO? dto, int idDocumentoPw, string usuario)
        {
            try
            {
                if (dto == null) return;


                var rCab = _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWFechaInicioCabecera_Insertar(
                    dto.Titulo, dto.SubTitulo, dto.MostrarEnLaWeb, usuario
                );

                var idCabecera = GetIdFromSpResult(rCab, "No se pudo obtener IdDocumentoPWFechaInicioCabecera (insert).");


                foreach (var p in (dto.Paises ?? new List<FechaInicioPaisDTO>()))
                {
                    var rPais = _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWFechaInicio_Insertar(p.IdPais, usuario);
                    var idFechaInicio = GetIdFromSpResult(rPais, "No se pudo obtener IdDocumentoPWFechaInicio (insert).");

                    _unitOfWork.DocumentoPwRepository.SP_DocumentoPWFechaInicioConfiguracion_RegistrarCambios(
                        idCabecera, idFechaInicio, idDocumentoPw, usuario
                    );

                    foreach (var d in (p.Detalles ?? new List<FechaInicioDetalleDTO>()))
                    {
                        _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWFechaInicioDetalle_Insertar(
                            idFechaInicio, d.IdModo, d.Fecha, d.Horario, usuario
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en InsertarDocumentoPwFechaInicio() {ex.Message}", ex);
            }
        }

        public void ActualizarSeccionFechaInicio(SeccionFechaInicioDTO? dto, int idDocumentoPw, string usuario)
        {
            try
            {
                if (dto == null) return;

                string Norm(string? s) => (s ?? "").Trim();


                var jsonActual = _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWFechaInicio_ObtenerRows(idDocumentoPw);
                var actuales = (!string.IsNullOrWhiteSpace(jsonActual) && !jsonActual.Contains("[]"))
                    ? JsonConvert.DeserializeObject<List<DocumentoPWFechaInicioRowDTOV2>>(jsonActual) ?? new List<DocumentoPWFechaInicioRowDTOV2>()
                    : new List<DocumentoPWFechaInicioRowDTOV2>();

                var idCabActual = actuales.Select(x => x.IdDocumentoPWFechaInicioCabecera).FirstOrDefault(x => x.HasValue) ?? 0;
                var tituloActual = Norm(actuales.Select(x => x.Titulo).FirstOrDefault());
                var subActual = Norm(actuales.Select(x => x.SubTitulo).FirstOrDefault());
                var mostrarActual = actuales.Select(x => x.MostrarEnLaWeb).FirstOrDefault() ?? false;


                int idCabUsar = idCabActual;

                if (idCabActual <= 0)
                {
                    var rCab = _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWFechaInicioCabecera_Insertar(
                        dto.Titulo, dto.SubTitulo, dto.MostrarEnLaWeb, usuario
                    );
                    idCabUsar = GetIdFromSpResult(rCab, "No se pudo obtener IdDocumentoPWFechaInicioCabecera (insert).");
                }
                else
                {
                    var tituloNuevo = Norm(dto.Titulo);
                    var subNuevo = Norm(dto.SubTitulo);
                    var mostrarNuevo = dto.MostrarEnLaWeb;

                    if (tituloActual != tituloNuevo || subActual != subNuevo || mostrarActual != mostrarNuevo)
                    {
                        _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWFechaInicioCabecera_Actualizar(
                            idCabActual, dto.Titulo, dto.SubTitulo, dto.MostrarEnLaWeb, usuario
                        );
                    }

                    idCabUsar = idCabActual;
                }


                foreach (var idDet in (dto.DetallesEliminados ?? new List<int>()).Distinct())
                {
                    if (idDet > 0)
                        _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWFechaInicioDetalle_Desactivar(idDet, usuario);
                }

                foreach (var idPaisElim in (dto.PaisesEliminados ?? new List<int>()).Distinct())
                {
                    if (idPaisElim <= 0) continue;

                    _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWFechaInicioDetalle_DesactivarPorFechaInicio(idPaisElim, usuario);
                    _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWFechaInicioConfiguracion_DesactivarPorFechaInicio(idDocumentoPw, idPaisElim, usuario);
                    _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWFechaInicio_Desactivar(idPaisElim, usuario);
                }


                var actualPorPais = actuales
                    .Where(x => (x.IdDocumentoPWFechaInicio ?? 0) > 0)
                    .GroupBy(x => x.IdDocumentoPWFechaInicio!.Value)
                    .ToDictionary(g => g.Key, g => g.ToList());

                foreach (var p in (dto.Paises ?? new List<FechaInicioPaisDTO>()))
                {
                    int idFechaInicio = p.Id;


                    DocumentoPWFechaInicioRowDTOV2? paisActual = null;
                    List<DocumentoPWFechaInicioRowDTOV2> filasPaisActual = new();

                    if (idFechaInicio > 0 && actualPorPais.TryGetValue(idFechaInicio, out var filas))
                    {
                        filasPaisActual = filas;
                        paisActual = filasPaisActual.FirstOrDefault();
                    }

                    if (idFechaInicio > 0)
                    {

                        if (paisActual != null && (paisActual.IdPais ?? 0) != (p.IdPais ?? 0))
                        {
                            _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWFechaInicio_Actualizar(idFechaInicio, p.IdPais, usuario);
                        }
                    }
                    else
                    {

                        var rPais = _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWFechaInicio_Insertar(p.IdPais, usuario);
                        idFechaInicio = GetIdFromSpResult(rPais, "IdDocumentoPWFechaInicio inválido (insert).");
                    }

                    _unitOfWork.DocumentoPwRepository.SP_DocumentoPWFechaInicioConfiguracion_RegistrarCambios(
                        idCabUsar, idFechaInicio, idDocumentoPw, usuario
                    );

                    var detActualPorId = filasPaisActual
                        .Where(x => (x.IdDocumentoPWFechaInicioDetalle ?? 0) > 0)
                        .ToDictionary(x => x.IdDocumentoPWFechaInicioDetalle!.Value, x => x);

                    foreach (var d in (p.Detalles ?? new List<FechaInicioDetalleDTO>()))
                    {
                        var fechaNueva = d.Fecha?.Date;
                        var horarioNuevo = Norm(d.Horario);

                        if (d.Id > 0)
                        {

                            if (detActualPorId.TryGetValue(d.Id, out var detAct))
                            {
                                var cambio =
                                    (detAct.IdModo ?? 0) != (d.IdModo ?? 0) ||
                                    (detAct.Fecha?.Date != fechaNueva) ||
                                    Norm(detAct.Horario) != horarioNuevo;

                                if (cambio)
                                {
                                    _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWFechaInicioDetalle_Actualizar(
                                        d.Id, idFechaInicio, d.IdModo, d.Fecha, d.Horario, usuario
                                    );
                                }
                            }
                            else
                            {

                                _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWFechaInicioDetalle_Actualizar(
                                    d.Id, idFechaInicio, d.IdModo, d.Fecha, d.Horario, usuario
                                );
                            }
                        }
                        else
                        {

                            _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWFechaInicioDetalle_Insertar(
                                idFechaInicio, d.IdModo, d.Fecha, d.Horario, usuario
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ActualizarSeccionFechaInicio() {ex.Message}", ex);
            }
        }


        public void ActualizarSeccionNotas(SeccionNotasDTO? dto, int idDocumentoPw, string usuario)
        {
            try
            {
                if (dto == null) return;

                string Norm(string? s) => (s ?? "").Trim();

                var jsonActual = _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWNota_ObtenerRows(idDocumentoPw);

                var actuales = (!string.IsNullOrWhiteSpace(jsonActual) && !jsonActual.Contains("[]"))
                    ? (JsonConvert.DeserializeObject<List<DocumentoPWNotaRowDTO>>(jsonActual) ?? new List<DocumentoPWNotaRowDTO>())
                    : new List<DocumentoPWNotaRowDTO>();


                var actualPorNota = actuales
                    .Where(x => x.IdDocumentoPWNota > 0)
                    .GroupBy(x => x.IdDocumentoPWNota)
                    .ToDictionary(g => g.Key, g => g.ToList());

                var mostrarWebActual = actuales.Select(x => x.MostrarWeb).FirstOrDefault() ?? false;
                var mostrarWebNuevo = dto.MostrarEnLaWeb;

                foreach (var idDet in (dto.DetallesEliminados ?? new List<int>()).Distinct())
                {
                    if (idDet > 0)
                        _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWNotaDetalle_Desactivar(idDet, usuario);
                }

                foreach (var idNotaElim in (dto.NotasEliminadas ?? new List<int>()).Distinct())
                {
                    if (idNotaElim <= 0) continue;


                    if (actualPorNota.TryGetValue(idNotaElim, out var filasNota))
                    {
                        var idsDetalles = filasNota
                            .Select(x => x.IdDocumentoPWNotaDetalle ?? 0)
                            .Where(x => x > 0)
                            .Distinct()
                            .ToList();

                        foreach (var idDet in idsDetalles)
                            _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWNotaDetalle_Desactivar(idDet, usuario);
                    }

                    _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWNotaConfiguracion_DesactivarPorNota(idDocumentoPw, idNotaElim, usuario);
                    _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWNota_Desactivar(idNotaElim, usuario);
                }

                foreach (var n in (dto.Notas ?? new List<NotaDTOV2>()))
                {
                    var idNotaDoc = n.Id;

                    var idTipoNuevo = n.IdNotaTipo ?? 0;
                    var descNuevo = Norm(n.Descripcion);

                    DocumentoPWNotaRowDTO? notaActual = null;
                    List<DocumentoPWNotaRowDTO> filasActualesNota = new();

                    if (idNotaDoc > 0 && actualPorNota.TryGetValue(idNotaDoc, out var filas))
                    {
                        filasActualesNota = filas;
                        notaActual = filasActualesNota.FirstOrDefault();
                    }

                    // 4.1) Nota: update/insert
                    if (idNotaDoc > 0)
                    {
                        if (notaActual != null)
                        {
                            var cambioNota =
                                (notaActual.IdDocumentoPWNotaTipo ?? 0) != idTipoNuevo ||
                                Norm(notaActual.Descripcion) != descNuevo;

                            if (cambioNota)
                            {
                                _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWNota_Actualizar(
                                    idNotaDoc,
                                    idTipoNuevo,
                                    descNuevo,
                                    usuario
                                );
                            }
                        }
                        else
                        {

                            _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWNota_Actualizar(
                                idNotaDoc,
                                idTipoNuevo,
                                descNuevo,
                                usuario
                            );
                        }
                    }
                    else
                    {

                        var rNota = _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWNota_Insertar(
                            idTipoNuevo,
                            descNuevo,
                            usuario
                        );

                        if (string.IsNullOrWhiteSpace(rNota) || rNota.Contains("[]"))
                            throw new Exception("No se pudo insertar Nota.");

                        var arrNota = JArray.Parse(rNota);
                        idNotaDoc = arrNota.Count > 0 ? (int)(arrNota[0]["Id"] ?? 0) : 0;

                        if (idNotaDoc <= 0)
                            throw new Exception("IdDocumentoPWNota inválido (insert nota).");
                    }


                    if (idNotaDoc > 0 && (mostrarWebActual != mostrarWebNuevo || n.Id == 0))
                    {
                        _unitOfWork.DocumentoPwRepository.SP_DocumentoPWNotaConfiguracion_RegistrarCambios(
                            idDocumentoPw,
                            idNotaDoc,
                            mostrarWebNuevo,
                            usuario
                        );
                    }
                    else
                    {

                        _unitOfWork.DocumentoPwRepository.SP_DocumentoPWNotaConfiguracion_RegistrarCambios(
                            idDocumentoPw,
                            idNotaDoc,
                            mostrarWebNuevo,
                            usuario
                        );
                    }


                    var detActualPorId = filasActualesNota
                        .Where(x => (x.IdDocumentoPWNotaDetalle ?? 0) > 0)
                        .ToDictionary(x => x.IdDocumentoPWNotaDetalle!.Value, x => x);

                    var detalles = (n.Detalles ?? new List<NotaDetalleDTO>())
                        .OrderBy(x => x.Orden)
                        .ToList();

                    foreach (var d in detalles)
                    {

                        var idPaisNuevo = d.IdPais;
                        var infoNuevo = Norm(d.InformacionExtra);
                        var horario = Norm(d.Horario);

                        if (idPaisNuevo.HasValue)
                        {

                            infoNuevo = null;
                        }
                        else
                        {

                            idPaisNuevo = null;
                            if (string.IsNullOrWhiteSpace(infoNuevo)) infoNuevo = null;
                        }

                        if (d.Id > 0)
                        {
                            if (detActualPorId.TryGetValue(d.Id, out var detActual))
                            {
                                var cambioDet =
                                    (detActual.Orden ?? 0) != d.Orden ||
                                    (detActual.IdPais ?? null) != (idPaisNuevo ?? null) ||
                                    Norm(detActual.InformacionExtra) != Norm(infoNuevo) ||
                                    Norm(detActual.Horario) != Norm(horario);

                                if (cambioDet)
                                {
                                    _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWNotaDetalle_Actualizar(
                                        d.Id,
                                        idNotaDoc,
                                        d.Orden,
                                        infoNuevo,
                                        idPaisNuevo,
                                        horario,
                                        usuario
                                    );
                                }
                            }
                            else
                            {

                                _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWNotaDetalle_Actualizar(
                                    d.Id,
                                    idNotaDoc,
                                    d.Orden,
                                    infoNuevo,
                                    idPaisNuevo,
                                    horario,
                                    usuario
                                );
                            }
                        }
                        else
                        {
                            _unitOfWork.DocumentoPwRepository.SP_TDocumentoPWNotaDetalle_Insertar(
                                idNotaDoc,
                                d.Orden,
                                infoNuevo,
                                idPaisNuevo,
                                horario,
                                usuario
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ActualizarSeccionNotas() {ex.Message}", ex);
            }
        }

        public async Task<string> SubirArchivoDocumentoPw(int id, IFormFile archivo, string campo, string usuario)
        {
            try
            {
                var tiposPermitidos = new[] { "application/pdf", "application/msword", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" };
                if (!tiposPermitidos.Contains(archivo.ContentType))
                    throw new Exception("Solo se permiten archivos PDF o Word (.doc, .docx).");

                byte[] bytes;
                using (var ms = new MemoryStream())
                {
                    await archivo.CopyToAsync(ms);
                    bytes = ms.ToArray();
                }

                var url = await _unitOfWork.DocumentoPwRepository.SubirArchivoDocumentoPw(bytes, archivo.ContentType, archivo.FileName);

                if (!string.IsNullOrEmpty(url))
                {
                    var documentoPw = _unitOfWork.DocumentoPwRepository.ObtenerPorId(id);
                    if (documentoPw == null)
                        throw new Exception($"No se encontró el documento con Id {id}.");

                    if (campo == "urlArchivoInstruccionTarea")
                        documentoPw.UrlArchivoInstruccionTarea = url;
                    else if (campo == "urlArchivoCalificacionExcelente")
                        documentoPw.UrlArchivoCalificacionExcelente = url;

                    documentoPw.UsuarioModificacion = usuario;
                    documentoPw.FechaModificacion = DateTime.Now;

                    _unitOfWork.DetachAll();
                    _unitOfWork.DocumentoPwRepository.Update(documentoPw);
                    _unitOfWork.Commit();
                }

                return url;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-PLN-001@Error en SubirArchivoDocumentoPw() {ex.Message}", ex);
            }
        }

    }

}
