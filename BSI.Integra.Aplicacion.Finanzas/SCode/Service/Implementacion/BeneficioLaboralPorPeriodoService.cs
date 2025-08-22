using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: BeneficioLaboralPorPeriodoService
    /// Autor Modificacion: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_BeneficioLaboralPorPeriodo
    /// </summary>
    public class BeneficioLaboralPorPeriodoService : IBeneficioLaboralPorPeriodoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public BeneficioLaboralPorPeriodoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TBeneficioLaboralPorPeriodo, BeneficioLaboralPorPeriodo>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public BeneficioLaboralPorPeriodo Add(BeneficioLaboralPorPeriodo entidad)
        {
            try
            {
                var modelo = _unitOfWork.BeneficioLaboralPorPeriodoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<BeneficioLaboralPorPeriodo>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BeneficioLaboralPorPeriodo Update(BeneficioLaboralPorPeriodo entidad)
        {
            try
            {
                var modelo = _unitOfWork.BeneficioLaboralPorPeriodoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<BeneficioLaboralPorPeriodo>(modelo);
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
                _unitOfWork.BeneficioLaboralPorPeriodoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BeneficioLaboralPorPeriodo> Add(List<BeneficioLaboralPorPeriodo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.BeneficioLaboralPorPeriodoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<BeneficioLaboralPorPeriodo>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BeneficioLaboralPorPeriodo> Update(List<BeneficioLaboralPorPeriodo> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.BeneficioLaboralPorPeriodoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<BeneficioLaboralPorPeriodo>>(modelo);
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
                _unitOfWork.BeneficioLaboralPorPeriodoRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public bool InsertarBeneficioLaboralPorPeriodo(ListaBeneficioLaboralDTO ObjetoDTO)
        {

            try
            {
                var _repBeneficioLaboralPeriodoRep = _unitOfWork.BeneficioLaboralPorPeriodoRepository;
                var _repTipoBeneficioLaboralRep = _unitOfWork.BeneficioLaboralTipoRepository;

                bool ExistePeriodoEnBeneficioLaboral = _repBeneficioLaboralPeriodoRep.GetBy(x => x.IdPeriodo == ObjetoDTO.IdPeriodo).ToList().Count() > 0;
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var itemBeneficio in ObjetoDTO.ListaBeneficiados)
                    {
                        if (!ExistePeriodoEnBeneficioLaboral)
                        {

                            foreach (var _property in itemBeneficio.GetType().GetProperties()
                                .Where(x => x.PropertyType == typeof(decimal)))
                            {
                                var prop = itemBeneficio.GetType().GetProperty(_property.Name);
                                Decimal MontoDecimal = Convert.ToDecimal(prop.GetValue(itemBeneficio, null).ToString());

                                var IdBeneficio = _repTipoBeneficioLaboralRep.GetBy(x => x.Nombre.Replace(" ", "").Equals(_property.Name)).Select(x => x.Id).FirstOrDefault();

                                BeneficioLaboralPorPeriodo beneficioBO = new BeneficioLaboralPorPeriodo();

                                beneficioBO.Estado = true;
                                beneficioBO.UsuarioCreacion = ObjetoDTO.UsuarioModificacion;
                                beneficioBO.UsuarioModificacion = ObjetoDTO.UsuarioModificacion;
                                beneficioBO.FechaCreacion = DateTime.Now;
                                beneficioBO.FechaModificacion = DateTime.Now;
                                beneficioBO.IdAgendaTipoUsuario = itemBeneficio.IdAgendaTipoUsuario;
                                beneficioBO.IdPeriodo = ObjetoDTO.IdPeriodo;
                                beneficioBO.IdBeneficioLaboralTipo = IdBeneficio;
                                beneficioBO.Monto = MontoDecimal;

                                beneficioBO = this.Add(beneficioBO);
                            }
                        }
                        else
                        {
                            foreach (var _property in itemBeneficio.GetType().GetProperties()
                                .Where(x => x.PropertyType == typeof(decimal)))
                            {
                                var prop = itemBeneficio.GetType().GetProperty(_property.Name);
                                Decimal MontoDecimal = Convert.ToDecimal(prop.GetValue(itemBeneficio, null).ToString());

                                var IdBeneficio = _repTipoBeneficioLaboralRep.GetBy(x => x.Nombre.Replace(" ", "").Equals(_property.Name)).Select(x => x.Id).FirstOrDefault();

                                BeneficioLaboralPorPeriodo beneficioBO = _mapper.Map<BeneficioLaboralPorPeriodo>(_repBeneficioLaboralPeriodoRep.GetBy(x => x.IdPeriodo == ObjetoDTO.IdPeriodo && x.IdAgendaTipoUsuario == itemBeneficio.IdAgendaTipoUsuario && x.IdBeneficioLaboralTipo == IdBeneficio).FirstOrDefault());

                                beneficioBO.Estado = true;
                                beneficioBO.UsuarioModificacion = ObjetoDTO.UsuarioModificacion;
                                beneficioBO.FechaModificacion = DateTime.Now;
                                beneficioBO.Monto = MontoDecimal;

                                _repBeneficioLaboralPeriodoRep.Update(beneficioBO);
                            }
                        }
                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        public IEnumerable<BeneficioLaboralVentasDTO> ObtenerBeneficioLaboralSegunPeriodo(int IdPeriodo)
        {
            try
            {
                var _repBeneficioLaboralPeriodoRep = _unitOfWork.BeneficioLaboralPorPeriodoRepository;

                var listadoBeneficioLaboralVentas = _repBeneficioLaboralPeriodoRep.ObtenerBeneficioLaboralVentasPorPeriodo(IdPeriodo);
                if (listadoBeneficioLaboralVentas.Count() == 0)
                {
                    return null;
                }
                return listadoBeneficioLaboralVentas;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

    }
}
