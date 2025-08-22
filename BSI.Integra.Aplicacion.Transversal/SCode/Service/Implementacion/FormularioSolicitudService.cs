using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;
//using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroTipoFormularioSolicitudDTO;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: FormularioSolicitudService
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 12/09/2022
    /// <summary>
    /// Gestión general de T_FormularioSolicitud
    /// </summary>
    public class FormularioSolicitudService : IFormularioSolicitudService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FormularioSolicitudService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFormularioSolicitud, FormularioSolicitud>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public FormularioSolicitud Add(FormularioSolicitud entidad)
        {
            try
            {
                var modelo = _unitOfWork.FormularioSolicitudRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FormularioSolicitud>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FormularioSolicitud Update(FormularioSolicitud entidad)
        {
            try
            {

                var modelo = _unitOfWork.FormularioSolicitudRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FormularioSolicitud>(modelo);
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
                _unitOfWork.FormularioSolicitudRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormularioSolicitud> Add(List<FormularioSolicitud> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FormularioSolicitudRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FormularioSolicitud>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormularioSolicitud> Update(List<FormularioSolicitud> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FormularioSolicitudRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FormularioSolicitud>>(modelo);
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
                _unitOfWork.FormularioSolicitudRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 12/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_FormularioSolicitud para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.FormularioSolicitudRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Adriana Chipana Ampuero.
        /// Fecha: 19/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_FormularioSolicitud por nombre para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>

        public IEnumerable<DTO.ComboDTO> ObtenerComboFs(InsertarFormulario2DTO nombre)
        {
            try
            {
                return _unitOfWork.FormularioSolicitudRepository.ObtenerComboFs(nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 12/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_FormularioSolicitud
        /// </summary>
        /// <returns> List<FormularioSolicitudDTO> </returns>
        public IEnumerable<FormularioSolicitudDTO> ObtenerFormularioSolicitud()
        {
            try
            {
                return _unitOfWork.FormularioSolicitudRepository.ObtenerFormularioSolicitud();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<FormularioSolicitudCompuestoDTO> ObtenerTodo(FiltroCompuestroGrillaDTO paginador)
        {
            try
            {
                return _unitOfWork.FormularioSolicitudRepository.ObtenerTodo(paginador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ConjuntoAnuncioFiltroCompuestoDTO> ObtenerConjuntoAnunciosFiltro(string filtro)
        {
            try
            {
                return _unitOfWork.FormularioSolicitudRepository.ObtenerConjuntoAnunciosFiltro(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<FiltroDTO> FormularioRespuestaFiltro(string filtro)
        {
            try
            {
                return _unitOfWork.FormularioSolicitudRepository.FormularioRespuestaFiltro(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool EliminarFormularioSolicitud(int IdFormulario, string Usuario)
        {
            try
            {
                var _repCampoFormulario = _unitOfWork.CampoFormularioRepository;

                _unitOfWork.FormularioSolicitudRepository.Delete(IdFormulario, Usuario);

                var IdCampos = _repCampoFormulario.GetBy(w => w.Estado == true && w.IdFormularioSolicitud == IdFormulario, w => new { w.Id }).Select(x => x.Id);

                _repCampoFormulario.Delete(IdCampos, Usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 12/09/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta  los registros de T_FormularioSolicitud
        /// </summary>
        /// <returns> List<FormularioSolicitudDTO> </returns>

        public FormularioSolicitud InsertarFormularioSolicitud(InsertarFormularioSolicitudCampoDTO obj)
        {
            try
            {
                var _repCampoFormulario = new CampoFormularioService(_unitOfWork);

                FormularioSolicitud formularioSolicitud = new FormularioSolicitud()
                {
                    IdFormularioRespuesta = obj.Formulario.IdFormularioRespuesta,
                    Nombre = obj.Formulario.Nombre,
                    Codigo = obj.Formulario.Codigo,
                    Campanha = obj.Formulario.NombreCampania,
                    IdConjuntoAnuncio = obj.Formulario.IdCampania,
                    Proveedor = obj.Formulario.Proveedor,
                    IdFormularioSolicitudTextoBoton = (int)obj.Formulario.IdFormularioSolicitudTextoBoton,
                    TipoSegmento = (int)obj.Formulario.TipoSegmento,
                    CodigoSegmento = obj.Formulario.CodigoSegmento,
                    TipoEvento = (int)obj.Formulario.TipoEvento,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = obj.Formulario.Usuario,
                    UsuarioModificacion = obj.Formulario.Usuario
                };

                FormularioSolicitud response = this.Add(formularioSolicitud);

                List<CampoFormulario> camposnuevos = new List<CampoFormulario>();

                foreach (var item in obj.Campo)
                {
                    CampoFormulario campo = new CampoFormulario()
                    {
                        IdFormularioSolicitud = response.Id,
                        IdCampoContacto = item.Id.Value,
                        NroVisitas = item.NroVisitas,
                        Codigo = "LPG-PB",
                        Campo = item.Nombre,
                        Siempre = item.Siempre,
                        Inteligente = item.Inteligente,
                        Probabilidad = item.Probabilidad,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = obj.Formulario.Usuario,
                        UsuarioModificacion = obj.Formulario.Usuario,
                    };
                    camposnuevos.Add(campo);
                }
                _repCampoFormulario.Add(camposnuevos);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 12/09/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta  los registros   de COnjunto de  Anuncio de T_FormularioSolicitud
        /// </summary>
        /// <returns> List<FormularioSolicitudDTO> </returns>
        public CompuestoConjuntoAnuncioDTO InsertarConjuntoAnuncio(CompuestoConjuntoAnuncioDTO Obj)
        {
            try
            {

                var repConjuntoAnuncio = _unitOfWork.ConjuntoAnuncioRepository;

                ConjuntoAnuncio conjuntoAnuncio = new ConjuntoAnuncio();
                var cantidadCampanias = repConjuntoAnuncio.GetBy(w => w.Nombre.Equals(Obj.ConjuntoAnuncio.Nombre));
                if (cantidadCampanias.Count() > 0)
                {
                    throw new ArgumentException("Existen " + cantidadCampanias.Count() + " Capanias con este Nombre");
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    conjuntoAnuncio.Nombre = Obj.ConjuntoAnuncio.Nombre;
                    conjuntoAnuncio.IdCategoriaOrigen = Obj.ConjuntoAnuncio.IdProveedor; ;
                    conjuntoAnuncio.FechaCreacionCampania = DateTime.Now;
                    conjuntoAnuncio.UsuarioCreacion = Obj.Usuario;
                    conjuntoAnuncio.UsuarioModificacion = Obj.Usuario;
                    conjuntoAnuncio.FechaCreacion = DateTime.Now;
                    conjuntoAnuncio.FechaModificacion = DateTime.Now;
                    conjuntoAnuncio.Estado = true;
                    repConjuntoAnuncio.Add(conjuntoAnuncio);
                    scope.Complete();
                }


                return (CompuestoConjuntoAnuncioDTO)cantidadCampanias;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // Autor: Margiory Ramirez Neyra.
        /// Fecha: 12/09/2022
        /// Version: 1.0
        /// <summary>
        /// Actulizar]  los registros   de COnjunto de  Anuncio de T_FormularioSolicitud
        /// </summary>
        /// <returns> List<FormularioSolicitudDTO> </returns>


        public FormularioSolicitud ActualizarFormularioSolicitud(InsertarFormularioSolicitudCampoDTO obj)
        {
            try
            {
                var _repCampoFormulario = _unitOfWork.CampoFormularioRepository;
                var _repFormularioSolicitud = _unitOfWork.FormularioSolicitudRepository;
                var _repCampoFormulario2 = new CampoFormularioService(_unitOfWork);
                FormularioSolicitud formularioSolicitud = new FormularioSolicitud();
                formularioSolicitud = _mapper.Map<FormularioSolicitud>(_repFormularioSolicitud.FirstById(obj.Formulario.Id.Value));

                formularioSolicitud.IdFormularioRespuesta = obj.Formulario.IdFormularioRespuesta;
                formularioSolicitud.Nombre = obj.Formulario.Nombre;
                formularioSolicitud.Codigo = obj.Formulario.Codigo;
                formularioSolicitud.Campanha = obj.Formulario.NombreCampania;
                formularioSolicitud.IdConjuntoAnuncio = obj.Formulario.IdCampania;
                formularioSolicitud.Proveedor = obj.Formulario.Proveedor;
                formularioSolicitud.IdFormularioSolicitudTextoBoton = (int)obj.Formulario.IdFormularioSolicitudTextoBoton;
                formularioSolicitud.TipoSegmento = (int)obj.Formulario.TipoSegmento;
                formularioSolicitud.CodigoSegmento = obj.Formulario.CodigoSegmento;
                formularioSolicitud.TipoEvento = (int)obj.Formulario.TipoEvento;
                //formularioSolicitud.UrlbotonInvitacionPagina = obj.Formulario.UrlbotonInvitacionPagina;
                formularioSolicitud.FechaModificacion = DateTime.Now;
                formularioSolicitud.UsuarioModificacion = obj.Formulario.Usuario;

                var respuesta = this.Update(formularioSolicitud);

                var IdCampos = _repCampoFormulario.GetBy(w => w.Estado == true && w.IdFormularioSolicitud == formularioSolicitud.Id, w => new { w.Id }).Select(x => x.Id);

                _repCampoFormulario.Delete(IdCampos, obj.Formulario.Usuario);

                List<CampoFormulario> camposnuevos = new List<CampoFormulario>();

                foreach (var item in obj.Campo)
                {
                    CampoFormulario campo = new CampoFormulario()
                    {
                        IdFormularioSolicitud = formularioSolicitud.Id,
                        IdCampoContacto = item.Id.Value,
                        NroVisitas = item.NroVisitas,
                        Codigo = "LPG-PB",
                        Campo = item.Nombre,
                        Siempre = item.Siempre,
                        Inteligente = item.Inteligente,
                        Probabilidad = item.Probabilidad,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = obj.Formulario.Usuario,
                        UsuarioModificacion = obj.Formulario.Usuario,
                    };
                    camposnuevos.Add(campo);
                }
                _repCampoFormulario2.Add(camposnuevos);
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}





















