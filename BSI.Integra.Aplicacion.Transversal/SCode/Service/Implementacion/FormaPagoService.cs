using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: FormaPagoService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class FormaPagoService : IFormaPagoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public FormaPagoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFormaPago, FormaPago>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public FormaPago Add(FormaPago entidad)
        {
            try
            {
                var modelo = _unitOfWork.FormaPagoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FormaPago>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FormaPago Update(FormaPago entidad)
        {
            try
            {
                var modelo = _unitOfWork.FormaPagoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FormaPago>(modelo);
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
                _unitOfWork.FormaPagoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormaPago> Add(List<FormaPago> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FormaPagoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FormaPago>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormaPago> Update(List<FormaPago> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FormaPagoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FormaPago>>(modelo);
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
                _unitOfWork.FormaPagoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Margiory Ramirez.
        /// Fecha: 20/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtien fotrma Pago
        /// </summary>
        /// <returns>TFormaPago</returns>
        public object ObtenerFormasPago()
        {
           
            try
            {
                var _repFormaPagoRepositorio = _unitOfWork.FormaPagoRepository;
                return (_repFormaPagoRepositorio.GetBy(x => x.Estado == true, x => new { x.Id, x.Descripcion }).OrderBy(x => x.Descripcion).ToList());
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }

        /// Autor: Margiory Ramirez.
        /// Fecha: 20/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene una Lista de FormaPago (utilizado para combobox)
        /// </summary>
        /// <returns></returns>
        public List<FormaPagoDTO> ObtenerListaFormaPago()

        {
            try
            {
                return _unitOfWork.FormaPagoRepository.ObtenerListaFormaPago();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}
