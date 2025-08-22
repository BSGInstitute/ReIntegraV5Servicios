using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PeriodoService
    /// Autor: Griselberto Huaman.
    /// Fecha: 07/07/2022
    /// <summary>
    /// Gestión general de T_Periodo
    /// </summary>
    public class PeriodoService : IPeriodoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PeriodoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPeriodo, Periodo>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public Periodo Add(PeriodoDTO entidad,string Usuario)
        {
            try
            {
                Periodo periodo = new Periodo();

                periodo.Id = 0;
                periodo.Nombre = entidad.Nombre;
                periodo.FechaInicial = entidad.FechaInicial;
                periodo.FechaFin = entidad.FechaFin;
                periodo.FechaInicialFinanzas = entidad.FechaInicialFinanzas;
                periodo.FechaFinFinanzas = entidad.FechaFinFinanzas;
                periodo.FechaInicialRepIngresos = entidad.FechaInicialRepIngresos;
                periodo.FechaFinRepIngresos = entidad.FechaFinRepIngresos;
                periodo.Estado = true;

                periodo.FechaModificacion = DateTime.Now;
                periodo.FechaCreacion = DateTime.Now;
                periodo.UsuarioModificacion = Usuario;
                periodo.UsuarioCreacion = Usuario;

                var modelo = _unitOfWork.PeriodoRepository.Add(periodo);
                _unitOfWork.Commit();
                return _mapper.Map<Periodo>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Periodo Update(PeriodoDTO entidad,string Usuario)
        {
            try
            {
                Periodo periodo = new Periodo();
                var _repPeriodo = _unitOfWork.PeriodoRepository;
                periodo = _mapper.Map<Periodo>(_repPeriodo.FirstById(entidad.Id));

                periodo.Nombre = entidad.Nombre;
                periodo.FechaInicial = entidad.FechaInicial;
                periodo.FechaFin = entidad.FechaFin;
                periodo.FechaInicialFinanzas = entidad.FechaInicialFinanzas;
                periodo.FechaFinFinanzas = entidad.FechaFinFinanzas;
                periodo.FechaInicialRepIngresos = entidad.FechaInicialRepIngresos;
                periodo.FechaFinRepIngresos = entidad.FechaFinRepIngresos;

                periodo.FechaModificacion = DateTime.Now;
                periodo.UsuarioModificacion = Usuario;

                var modelo = _unitOfWork.PeriodoRepository.Update(periodo);
                _unitOfWork.Commit();
                return _mapper.Map<Periodo>(modelo);
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
                _unitOfWork.PeriodoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Periodo> Add(List<Periodo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PeriodoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Periodo>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Periodo> Update(List<Periodo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PeriodoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Periodo>>(modelo);
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
                _unitOfWork.PeriodoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Griselberto Huaman.
        /// Fecha: 07/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Periodo.
        /// </summary>
        /// <returns> List<PeriodoDTO> </returns>
        public IEnumerable<PeriodoDTO> ObtenerPeriodo()
        {
            try
            {
                return _unitOfWork.PeriodoRepository.ObtenerPeriodo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_Periodo para mostrarse en combo.
        /// </summary>
        /// <returns> List<PeriodoComboDTO> </returns>
        public IEnumerable<PeriodoComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.PeriodoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 11/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda la lista de Periodos para llenar en un combo en donde el estado es true
        /// </summary>
        /// <returns> Lista de periodos FiltroDTO </returns>
        public List<FiltroDTO> ObtenerPeriodosPendiente()
        {
            try
            {
                return _unitOfWork.PeriodoRepository.ObtenerPeriodosPendiente();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 13/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene fecha inicial del periodo.
        /// </summary>
        /// <returns> DateTime: lista </returns>
        public StringDTO ObtenerFechaInicial(int idPeriodo)
        {
            try
            {
                return _unitOfWork.PeriodoRepository.ObtenerFechaInicial(idPeriodo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 13/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene fecha final del periodo
        /// </summary>
        /// <returns> DateTime: lista </returns>
        public StringDTO ObtenerFechaFinal(int idPeriodo)
        {
            try
            {
                return _unitOfWork.PeriodoRepository.ObtenerFechaFinal(idPeriodo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
