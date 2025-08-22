using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: EtiquetaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/08/2022
    /// <summary>
    /// Gestión general de T_Etiqueta
    /// </summary>
    public class EtiquetaService : IEtiquetaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EtiquetaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TEtiquetum, Etiqueta>(MemberList.None).ReverseMap());
            var config2 = new MapperConfiguration(cfg => cfg.CreateMap<Etiqueta, Etiqueta>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public Etiqueta Add(Etiqueta entidad)
        {
            try
            {
                var modelo = _unitOfWork.EtiquetaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Etiqueta>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Etiqueta Update(Etiqueta entidad)
        {
            try
            {
                var modelo = _unitOfWork.EtiquetaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Etiqueta>(modelo);
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
                _unitOfWork.EtiquetaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Etiqueta> Add(List<Etiqueta> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EtiquetaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Etiqueta>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Etiqueta> Update(List<Etiqueta> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EtiquetaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Etiqueta>>(modelo);
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
                _unitOfWork.EtiquetaRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 10/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Etiqueta
        /// </summary>
        /// <returns> List<EtiquetaDTO> </returns>
        public IEnumerable<EtiquetaDTO> ObtenerEtiqueta()
        {
            try
            {
                return _unitOfWork.EtiquetaRepository.ObtenerEtiqueta();
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
        /// Obtiene registros de T_Etiqueta para mostrarse en combo.
        /// </summary>
        /// <returns> List<EtiquetaComboDTO> </returns>
        public IEnumerable<EtiquetaComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.EtiquetaRepository.ObtenerCombo();
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
        /// Obtener datos de Etiquetas asociados a un Nodo Padre especifico.
        /// </summary>
        /// <returns> List<EtiquetaDTO> </returns>
        public IEnumerable<Etiqueta> ObtenerPorIdNodoPadre(int idNodoPadre)
        {
            try
            {
                return _unitOfWork.EtiquetaRepository.ObtenerPorIdNodoPadre(idNodoPadre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<Etiqueta> ObtenerCategoriasPorIdPadre(CategoriasPlantillaDTO Json)
        {
            try
            {
                int padre = 1;
                List<Etiqueta> categorias = new List<Etiqueta>();

                if (Json.Id != 0)
                {
                    categorias = _mapper.Map<List<Etiqueta>>(_unitOfWork.EtiquetaRepository.ObtenerPorIdNodoPadre(Json.Id));
                }
                else
                {
                    categorias = _mapper.Map<List<Etiqueta>>(_unitOfWork.EtiquetaRepository.ObtenerPorIdNodoPadre(padre));
                }

                return categorias;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
