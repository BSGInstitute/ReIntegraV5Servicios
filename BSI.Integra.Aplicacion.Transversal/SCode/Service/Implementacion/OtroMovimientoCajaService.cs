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
    /// Service: OtroMovimientoCajaService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class OtroMovimientoCajaService : IOtroMovimientoCajaService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public OtroMovimientoCajaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TOtroMovimientoCaja, OtroMovimientoCaja>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public OtroMovimientoCaja Add(OtroMovimientoCaja entidad)
        {
            try
            {
                var modelo = _unitOfWork.OtroMovimientoCajaRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OtroMovimientoCaja>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OtroMovimientoCaja Update(OtroMovimientoCaja entidad)
        {
            try
            {
                var modelo = _unitOfWork.OtroMovimientoCajaRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<OtroMovimientoCaja>(modelo);
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
                _unitOfWork.OtroMovimientoCajaRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OtroMovimientoCaja> Add(List<OtroMovimientoCaja> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OtroMovimientoCajaRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OtroMovimientoCaja>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<OtroMovimientoCaja> Update(List<OtroMovimientoCaja> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.OtroMovimientoCajaRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<OtroMovimientoCaja>>(modelo);
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
                _unitOfWork.OtroMovimientoCajaRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Margiory Ramirez
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de TOtroMovimientoCaja para mostrarse en la grilla.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>


        public List<OtroMovimientoCajaDTO> ObtenerListaOtroMovimientoCaja()
        {
            try
            {
                return _unitOfWork.OtroMovimientoCajaRepository.ObtenerListaOtroMovimientoCaja();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Margiory Ramirez
        /// Fecha: 22/12/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta registros .
        /// </summary>
        /// <returns> List<ComboDTO> </returns>


        public List<OtroMovimientoCajaDTO> ObtenerOtroMovimientoCajaPorID(int Id)
        {
            try
            {
                return _unitOfWork.OtroMovimientoCajaRepository.ObtenerOtroMovimientoCajaPorID(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public  object InsertarOtroMovimientoCaja( OtroMovimientoCajaDTO ObjetoDTO)
        {
           
            try
            {
                var repOtroMovimientoCaja = _unitOfWork.OtroMovimientoCajaRepository;

                OtroMovimientoCaja NuevoOtroMovimientoCaja = new OtroMovimientoCaja();


                NuevoOtroMovimientoCaja.IdSubTipoMovimientoCaja = ObjetoDTO.IdSubTipoMovimientoCaja.Value;
                NuevoOtroMovimientoCaja.Precio = ObjetoDTO.Precio;
                NuevoOtroMovimientoCaja.IdMoneda = ObjetoDTO.IdMoneda;
                NuevoOtroMovimientoCaja.FechaPago = ObjetoDTO.FechaPago;
                NuevoOtroMovimientoCaja.IdCentroCosto = ObjetoDTO.IdCentroCosto;
                NuevoOtroMovimientoCaja.IdPlanContable = ObjetoDTO.IdPlanContable;
                NuevoOtroMovimientoCaja.IdCuentaCorriente = ObjetoDTO.IdCuentaCorriente;
                NuevoOtroMovimientoCaja.Observaciones = ObjetoDTO.Observaciones;
                NuevoOtroMovimientoCaja.IdAlumno = ObjetoDTO.IdAlumno;
                NuevoOtroMovimientoCaja.IdFormaPago = ObjetoDTO.IdFormaPago;
                NuevoOtroMovimientoCaja.Estado = true;
                NuevoOtroMovimientoCaja.UsuarioCreacion = ObjetoDTO.Usuario;
                NuevoOtroMovimientoCaja.UsuarioModificacion = ObjetoDTO.Usuario;
                NuevoOtroMovimientoCaja.FechaCreacion = DateTime.Now;
                NuevoOtroMovimientoCaja.FechaModificacion = DateTime.Now;

                NuevoOtroMovimientoCaja = this.Add(NuevoOtroMovimientoCaja);
                _unitOfWork.Commit();

                var Resultados = repOtroMovimientoCaja.ObtenerOtroMovimientoCajaPorID(NuevoOtroMovimientoCaja.Id);
                if (Resultados == null || Resultados.Count < 1)
                    throw new Exception("No se pudo recuperar el registro 'Id' inexistente");

                return Resultados[0];
            }
            catch (Exception Ex)
            {
                return (Ex.Message);
            }
        }




        public  object ActualizarOtroMovimientoCaja( OtroMovimientoCajaDTO ObjetoDTO)
        {
            try
            {
                var _repOtroMovimientoCaja = _unitOfWork.OtroMovimientoCajaRepository;

                var OtroMovimientoCaja = _repOtroMovimientoCaja.GetBy(x => x.Id == ObjetoDTO.Id).FirstOrDefault();
                OtroMovimientoCaja.IdSubTipoMovimientoCaja = ObjetoDTO.IdSubTipoMovimientoCaja.Value;
                OtroMovimientoCaja.Precio = ObjetoDTO.Precio;
                OtroMovimientoCaja.IdMoneda = ObjetoDTO.IdMoneda;
                OtroMovimientoCaja.FechaPago = ObjetoDTO.FechaPago;
                OtroMovimientoCaja.IdCentroCosto = ObjetoDTO.IdCentroCosto;
                OtroMovimientoCaja.IdPlanContable = ObjetoDTO.IdPlanContable;
                OtroMovimientoCaja.IdCuentaCorriente = ObjetoDTO.IdCuentaCorriente;
                OtroMovimientoCaja.Observaciones = ObjetoDTO.Observaciones;
                OtroMovimientoCaja.IdAlumno = ObjetoDTO.IdAlumno;
                OtroMovimientoCaja.IdFormaPago = ObjetoDTO.IdFormaPago;
                OtroMovimientoCaja.Estado = true;
                OtroMovimientoCaja.UsuarioModificacion = ObjetoDTO.Usuario;
                OtroMovimientoCaja.FechaModificacion = DateTime.Now;

                _repOtroMovimientoCaja.Update(OtroMovimientoCaja);
                _unitOfWork.Commit();

                var Resultados = _repOtroMovimientoCaja.ObtenerOtroMovimientoCajaPorID(OtroMovimientoCaja.Id);
                if (Resultados == null || Resultados.Count < 1)
                    throw new Exception("No se pudo recuperar el registro 'Id' inexistente");

                return Resultados[0];
            }
            catch (Exception Ex)
            {
                return (Ex.Message);
            }
        }


    }
}
