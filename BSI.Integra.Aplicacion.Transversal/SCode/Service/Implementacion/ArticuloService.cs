using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ArticuloService
    /// Autor: Max Mantilla.
    /// Fecha: 25/10/2022
    /// <summary>
    /// Gestión general de T_Articulo
    /// </summary>
    public class ArticuloService : IArticuloService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ArticuloService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TArticulo, Articulo>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public Articulo Add(Articulo entidad)
        {
            try
            {
                var modelo = _unitOfWork.ArticuloRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Articulo>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Articulo Update(Articulo entidad)
        {
            try
            {
                var modelo = _unitOfWork.ArticuloRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Articulo>(modelo);
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
                _unitOfWork.ArticuloRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Articulo> Add(List<Articulo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ArticuloRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Articulo>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Articulo> Update(List<Articulo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ArticuloRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Articulo>>(modelo);
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
                _unitOfWork.ArticuloRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Articulo
        /// </summary>
        /// <returns> List<ArticuloCompuestoDTO> </returns>
        public ArticuloCompuestFiltroTotalDTO ObtenerTodo(filtroPrueba paginador)
        {
            try
            {
                return _unitOfWork.ArticuloRepository.ObtenerTodo(paginador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el siguiente IdWeb para pla.T_Articulo
        /// </summary>
        /// <returns> int </returns>
        public int? ObtenerMaximaIdWeb()
        {
            try
            {
                return _unitOfWork.ArticuloRepository.ObtenerMaximaIdWeb();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los programas no asociados para pla.T_Articulo
        /// </summary>
        /// <returns> List<FiltroDTO> </returns>
        public List<FiltroDTO> ObtenerProgramasNoAsociadosArticulo(int IdArticulo)
        {
            try
            {
                return _unitOfWork.ArticuloRepository.ObtenerProgramasNoAsociadosArticulo(IdArticulo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los programas asociados para pla.T_Articulo
        /// </summary>
        /// <returns> List<FiltroDTO> </returns>
        public List<FiltroDTO> ObtenerProgramasAsociadosArticulo(int IdArticulo)
        {
            try
            {
                return _unitOfWork.ArticuloRepository.ObtenerProgramasAsociadosArticulo(IdArticulo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Version: 1.0
        /// <summary>
        /// Registra los articulos con su respectivo parametroSeo
        /// </summary>
        /// <returns> Articulo </returns>
        public Articulo InsertarArticuloParametroSeo(InsertarArticuloParametroSeoDTO obj)
        {
            try
            {
                var _repArticuloSeoRepository = new ArticuloSeoService(_unitOfWork);

                Articulo formularioArticulo = new Articulo()
                {
                    IdWeb = obj.Formulario.IdWeb,
                    Nombre = obj.Formulario.Nombre,
                    Titulo = obj.Formulario.Titulo,
                    ImgPortada = obj.Formulario.ImgPortada,
                    ImgPortadaAlt = obj.Formulario.ImgPortadaAlt,
                    ImgSecundaria = obj.Formulario.ImgSecundaria,
                    ImgSecundariaAlt = obj.Formulario.ImgSecundariaAlt,
                    UrlWeb = obj.Formulario.UrlWeb,
                    UrlDocumento = obj.Formulario.UrlDocumento,
                    Autor = obj.Formulario.Autor,
                    IdTipoArticulo = obj.Formulario.IdTipoArticulo,
                    IdArea = obj.Formulario.IdArea,
                    IdSubArea = obj.Formulario.IdSubArea,
                    IdExpositor = obj.Formulario.IdExpositor,
                    IdCategoria = obj.Formulario.IdCategoria,
                    Contenido = obj.Formulario.Contenido,
                    DescripcionGeneral = obj.Formulario.DescripcionGeneral,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = obj.Formulario.Usuario,
                    UsuarioModificacion = obj.Formulario.Usuario
                };

                Articulo response = this.Add(formularioArticulo);

                List<ArticuloSeo> parametroSeoNuevos = new List<ArticuloSeo>();

                foreach (var item in obj.parametroSeo)
                {
                    ArticuloSeo parametroSeo = new ArticuloSeo()
                    {
                        IdArticulo = response.Id,
                        IdParametroSeo = item.Id,
                        Descripcion = item.Descripcion,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = obj.Formulario.Usuario,
                        UsuarioModificacion = obj.Formulario.Usuario,
                    };
                    parametroSeoNuevos.Add(parametroSeo);
                }
                _repArticuloSeoRepository.Add(parametroSeoNuevos);

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza los articulos con su respectivo parametroSeo
        /// </summary>
        /// <returns> Articulo </returns>
        public Articulo ActualizarArticuloParametroSeo(InsertarArticuloParametroSeoDTO obj)
        {
            try
            {
                var _repArticuloSeo = _unitOfWork.ArticuloSeoRepository;
                var _repArticulo = _unitOfWork.ArticuloRepository;
                var _repArticuloSeoRepository = new ArticuloSeoService(_unitOfWork);
                Articulo formularioArticuloSeo = new Articulo();

                formularioArticuloSeo = _mapper.Map<Articulo>(_repArticulo.FirstById(obj.Formulario.Id.Value));
                formularioArticuloSeo.IdWeb = formularioArticuloSeo.IdWeb;
                formularioArticuloSeo.Nombre = obj.Formulario.Nombre;
                formularioArticuloSeo.Titulo = obj.Formulario.Titulo;
                formularioArticuloSeo.ImgPortada = obj.Formulario.ImgPortada;
                formularioArticuloSeo.ImgPortadaAlt = obj.Formulario.ImgPortadaAlt;
                formularioArticuloSeo.ImgSecundaria = obj.Formulario.ImgSecundaria;
                formularioArticuloSeo.ImgSecundariaAlt = obj.Formulario.ImgSecundariaAlt;
                formularioArticuloSeo.UrlDocumento = obj.Formulario.UrlDocumento;
                formularioArticuloSeo.Autor = obj.Formulario.Autor;
                formularioArticuloSeo.IdTipoArticulo = obj.Formulario.IdTipoArticulo;
                formularioArticuloSeo.IdArea = obj.Formulario.IdArea;
                formularioArticuloSeo.IdSubArea = obj.Formulario.IdSubArea;
                formularioArticuloSeo.IdExpositor = obj.Formulario.IdExpositor;
                formularioArticuloSeo.IdCategoria = obj.Formulario.IdCategoria;
                formularioArticuloSeo.Contenido = obj.Formulario.Contenido;
                formularioArticuloSeo.DescripcionGeneral = obj.Formulario.DescripcionGeneral;
                formularioArticuloSeo.Estado = true;
                formularioArticuloSeo.FechaModificacion = DateTime.Now;
                formularioArticuloSeo.UsuarioModificacion = obj.Formulario.Usuario;

                var respuesta = this.Update(formularioArticuloSeo);

                var IdCampos = _repArticuloSeo.GetBy(w => w.Estado == true && w.IdArticulo == formularioArticuloSeo.Id, w => new { w.Id }).Select(x => x.Id);

                _repArticuloSeo.Delete(IdCampos, obj.Formulario.Usuario);

                List<ArticuloSeo> camposnuevos = new List<ArticuloSeo>();

                foreach (var item in obj.parametroSeo)
                {
                    ArticuloSeo parametroSeo = new ArticuloSeo()
                    {
                        IdArticulo = formularioArticuloSeo.Id,
                        IdParametroSeo = item.Id,
                        Descripcion = item.Descripcion,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = obj.Formulario.Usuario,
                        UsuarioModificacion = obj.Formulario.Usuario,
                    };
                    camposnuevos.Add(parametroSeo);
                }
                _repArticuloSeoRepository.Add(camposnuevos);
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Articulo
        /// </summary>
        /// <returns> List<ArticuloCompuestoDTO> </returns>
        public List<ArticuloCompuestoDTO> ObtenerTodoArticulo()
        {
            try
            {
                return _unitOfWork.ArticuloRepository.ObtenerTodoArticulo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
