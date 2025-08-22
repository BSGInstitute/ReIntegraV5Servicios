using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: EmpresaService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_Empresa
    /// </summary>
    public class EmpresaService : IEmpresaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public EmpresaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TEmpresa, Empresa>(MemberList.None).ReverseMap();
                    cfg.CreateMap<Empresa, EmpresaDTO>(MemberList.None).ReverseMap();
                }
            );

            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public Empresa Add(Empresa entidad)
        {
            try
            {
                var modelo = _unitOfWork.EmpresaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Empresa>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Empresa Update(Empresa entidad)
        {
            try
            {
                var modelo = _unitOfWork.EmpresaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Empresa>(modelo);
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
                _unitOfWork.EmpresaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Empresa> Add(List<Empresa> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EmpresaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Empresa>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Empresa> Update(List<Empresa> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.EmpresaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Empresa>>(modelo);
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
                _unitOfWork.EmpresaRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 10/06/2022
        /// Autor Modificacion: Flavio Rodrigo Mamani Fabian.
        /// Fecha Modificacion: 21/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Empresa
        /// </summary>
        /// <returns> List<EmpresaDTO> </returns>
        public IEnumerable<EmpresaDTO> ObtenerEmpresa()
        {
            try
            {
                return _unitOfWork.EmpresaRepository.ObtenerEmpresa();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmpresaDTO ObtenerEmpresaDtoPorId(int id)
        {
            try
            {
                var empresa = _unitOfWork.EmpresaRepository.ObtenerPorId(id);
                return _mapper.Map<Empresa, EmpresaDTO>(empresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Empresa para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.EmpresaRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoIdentificador para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<TipoIdentificadorComboDTO> ObtenerComboTipoIdentificador()
        {
            try
            {
                return _unitOfWork.EmpresaRepository.ObtenerComboTipoIdentificador();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TamanioEmpresa para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerComboTamanioEmpresa()
        {
            try
            {
                return _unitOfWork.EmpresaRepository.ObtenerComboTamanioEmpresa();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CodigoCiiuIndustria para mostrarse en combo.
        /// </summary>
        /// <returns> List<CodigoCiiuIndustriaComboDTO> </returns>
        public IEnumerable<CodigoCiiuIndustriaComboDTO> ObtenerComboCodigoCiiuIndustria()
        {
            try
            {
                return _unitOfWork.EmpresaRepository.ObtenerComboCodigoCiiuIndustria();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el Id y Nombre de Empresas relacionadas al Nombre Parcial.
        /// </summary>
        /// <param name="nombreParcial">Nombre Parcial de la Empresa</param>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerAutocomplete(string nombreParcial)
        {
            try
            {
                return _unitOfWork.EmpresaRepository.ObtenerAutocomplete(nombreParcial);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 05/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el tamanio de la Empresa asociado a un Id Empresa.
        /// </summary>
        /// <param name="idEmpresa">Id de la Empresa</param>
        /// <returns> ValorIntDTO </returns>
        public ValorIntDTO ObtenerIdTamanioEmpresaPorIdEmpresa(int idEmpresa)
        {
            try
            {
                return _unitOfWork.EmpresaRepository.ObtenerIdTamanioEmpresaPorIdEmpresa(idEmpresa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 06/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una lista de empresas competidoras
        /// IdTipoEmpresa = 1: COMPETIDOR";
        /// IdTipoEmpresa = 0: NO_COMPETIDOR";
        /// </summary>
        public List<ComboDTO> ObtenerTodoCompetidores()
        {

            try
            {
                return _unitOfWork.EmpresaRepository.ObtenerTodoCompetidores();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una empresa para usado para filtro por id 
        /// </summary>
        public ComboDTO ObtenerEmpresaPorId(int id)
        {
            try
            {
                return _unitOfWork.EmpresaRepository.ObtenerEmpresaPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 03/311/2022
        /// <summary>
        /// Obtiene las empresas que contengan el valor nombre.
        /// </summary>
        /// <param name="nombre"> Nombre de Empresa</param>
        /// <returns> List<ComboDTO> </returns>
        public List<ComboDTO> CargarEmpresaAutoComplete(string nombre)
        {
            try
            {
                return _unitOfWork.EmpresaRepository.CargarEmpresaAutoComplete(nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los atributos de empresa.
        /// </summary>
        /// <param name="id">Id de la Empresa</param>
        /// <returns> ValorIntDTO </returns>
        public Empresa ObtenerPorId(int id)
        {

            try
            {
                return _unitOfWork.EmpresaRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Flavio Rodrigo Mamani Fabian.
        /// Fecha: 07/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los atributos de empresa.
        /// </summary>
        /// <returns> ValorIntDTO </returns>
        public List<EmpresaObtenerDTO> ObtenerEmpresas()
        {
            try
            {
                return _unitOfWork.EmpresaRepository.ObtenerEmpresas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Empresa.
        /// </summary>
        /// <returns> List<EmpresaDTO> </returns>
        public List<EmpresaDTO> ObtenerEmpresa2()
        {
            try
            {
                return _unitOfWork.EmpresaRepository.ObtenerEmpresa2().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 20/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Empresa.
        /// </summary>
        public EmpresaFiltroDTO ObtenerEmpresaFiltro(FiltroKendoGridDTO gridState)
        {
            try
            {
                return _unitOfWork.EmpresaRepository.ObtenerEmpresaFiltro(gridState);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
