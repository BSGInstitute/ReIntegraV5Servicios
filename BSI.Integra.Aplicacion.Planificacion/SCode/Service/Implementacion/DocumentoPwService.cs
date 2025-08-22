using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.Repository.Implementation.Planificacion;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Text;
using System.Transactions;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

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
        public DocumentoPw InsertarDocumento(CompuestoDocumentoDTO dto, string usuario)
        {
            try
            {
                DocumentoPw documento = new DocumentoPw();
                using (TransactionScope scope = new TransactionScope())
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
                    _unitOfWork.DocumentoPwRepository.Add(documento);
                    _unitOfWork.Commit();
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
        public DocumentoPw ActualizarDocumento(CompuestoDocumentoPwDTO dto, string usuario)
        {
            try
            {
                IDocumentoSeccionPwService documentoSeccionPwService = new DocumentoSeccionPwService(_unitOfWork);
                IBandejaPendientePwService bandejaPendientePwService = new BandejaPendientePwService(_unitOfWork);

                DocumentoPw documentoPw = new DocumentoPw();
                List<DocumentoSeccionPw> documentoSeccionPw = new List<DocumentoSeccionPw>();

                using (TransactionScope scope = new TransactionScope())
                {
                    documentoSeccionPwService.EliminacionDocumentoSeccionLogicoPorIdDocumento(dto.ObjetoDocumento.Id, usuario, dto.Lista);
                    //bandejaPendientePwService.EliminacionBandejaPendienteLogicoPorIdDocumento(dto.ObjetoDocumento.Id, usuario, dto.ListaRevision);

                    documentoPw = _unitOfWork.DocumentoPwRepository.ObtenerPorId(dto.ObjetoDocumento.Id);
                    documentoPw.Nombre = dto.ObjetoDocumento.Nombre;
                    documentoPw.IdPlantillaPw = dto.ObjetoDocumento.IdPlantillaPw;
                    documentoPw.EstadoFlujo = dto.ObjetoDocumento.EstadoFlujo;
                    documentoPw.Asignado = dto.ObjetoDocumento.Asignado;
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
                            StringDTO intro =_unitOfWork.DocumentoPwRepository.ValidarSiTieneIntroduccionLaVersion(dto.ObjetoDocumento.Id, element.IdVersionPrograma);
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
    }
}
