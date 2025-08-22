using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Runtime.InteropServices;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: PanelIngresoDisponibleService
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PanelIngresoDisponible
    /// </summary>
    public class PanelIngresoDisponibleService : IPanelIngresoDisponibleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PanelIngresoDisponibleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TPanelIngresoDisponible, PanelIngresoDisponible>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public PanelIngresoDisponible Add(PanelIngresoDisponible entidad)
        {
            try
            {
                var modelo = _unitOfWork.PanelIngresoDisponibleRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PanelIngresoDisponible>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PanelIngresoDisponible Update(PanelIngresoDisponible entidad)
        {
            try
            {
                var modelo = _unitOfWork.PanelIngresoDisponibleRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<PanelIngresoDisponible>(modelo);
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
                _unitOfWork.PanelIngresoDisponibleRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PanelIngresoDisponible> Add(List<PanelIngresoDisponible> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PanelIngresoDisponibleRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PanelIngresoDisponible>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PanelIngresoDisponible> Update(List<PanelIngresoDisponible> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PanelIngresoDisponibleRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PanelIngresoDisponible>>(modelo);
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
                _unitOfWork.PanelIngresoDisponibleRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 06/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ConfiguracionDatoRemarketing para mostrarse en datos Grilla.
        /// </summary>
        /// <returns> List<> </returns>

        public List<PanelDepositoDisponibleDTO> ObtenerPanelDepositoDisponible()
        {
            try
            {
                return _unitOfWork.PanelIngresoDisponibleRepository.ObtenerPanelDepositoDisponible();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 06/10/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta registros de T_ConfiguracionDatoRemarketing .
        /// </summary>
        /// <returns> List<> </returns>

        public object InsertarPanelDepositoDisponible( PanelDepositoDisponibleDTO Json)
        {
          
            try
            {
                var repPanelDepositoRep = _unitOfWork.PanelIngresoDisponibleRepository;
                PanelIngresoDisponible panelDeposito = new PanelIngresoDisponible();

                using (TransactionScope scope = new TransactionScope())
                {
                    panelDeposito.IdFormaPago = Json.IdFormaPago;
                    panelDeposito.DiasDeposito = Json.DiasDeposito;
                    panelDeposito.DiasDisponible = Json.DiasDisponible;
                    panelDeposito.CuentaFeriados = Json.CuentaFeriados;
                    panelDeposito.CuentaFeriadosEstatales = Json.CuentaFeriadosEstatales;
                    panelDeposito.ConsideraVsd = Json.ConsideraVSD;
                    panelDeposito.ConsideraDiasHabilesLunesViernes = Json.ConsideraDiasHabilesLunesViernes;
                    panelDeposito.ConsideraDiasHabilesLunesSabado = Json.ConsideraDiasHabilesLunesSabado;
                    panelDeposito.ConsideraDiasFijoSemana = Json.ConsideraDiasFijoSemana;
                    panelDeposito.IdDiaSemanaFijo = Json.ConsideraDiasFijoSemana == true ? Json.IdDiaSemanaFijo : null;
                    panelDeposito.HoraCorte = Json.HoraCorte;
                    panelDeposito.MinutoCorte = Json.MinutoCorte;
                    panelDeposito.PorcentajeCobro = Json.PorcentajeCobro;
                    panelDeposito.Estado = true;
                    panelDeposito.UsuarioCreacion = Json.UsuarioModificacion;
                    panelDeposito.FechaCreacion = DateTime.Now;
                    panelDeposito.UsuarioModificacion = Json.UsuarioModificacion;
                    panelDeposito.FechaModificacion = DateTime.Now;

                    repPanelDepositoRep.Add(panelDeposito);
                    _unitOfWork.Commit();




                    scope.Complete();
                }
                Json.Id = panelDeposito.Id;
                return Json;
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }
        }



        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 06/10/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza registros de T_ConfiguracionDatoRemarketing para mostrarse en datos Grilla.
        /// </summary>
        /// <returns> List<> </returns>

        public object ActualizarPanelDepositoDisponible( PanelDepositoDisponibleDTO Json)
        {
            try
            {
                //PanelIngresoDisponibleRepositorio repPanelDepositoRep = new PanelIngresoDisponibleRepositorio();
                var repPanelDepositoRep = _unitOfWork.PanelIngresoDisponibleRepository;
                //PanelIngresoDisponible panelDeposito = new PanelIngresoDisponible();
                PanelIngresoDisponible panelDeposito = _mapper.Map<PanelIngresoDisponible>(repPanelDepositoRep.FirstById(Json.Id));

                //panelDeposito = repPanelDepositoRep.FirstById(Json.Id);

                using (TransactionScope scope = new TransactionScope())
                {
                    panelDeposito.IdFormaPago = Json.IdFormaPago;
                    panelDeposito.DiasDeposito = Json.DiasDeposito;
                    panelDeposito.DiasDisponible = Json.DiasDisponible;
                    panelDeposito.CuentaFeriados = Json.CuentaFeriados;
                    panelDeposito.CuentaFeriadosEstatales = Json.CuentaFeriadosEstatales;
                    panelDeposito.ConsideraVsd = Json.ConsideraVSD;
                    panelDeposito.ConsideraDiasHabilesLunesViernes = Json.ConsideraDiasHabilesLunesViernes;
                    panelDeposito.ConsideraDiasHabilesLunesSabado = Json.ConsideraDiasHabilesLunesSabado;
                    panelDeposito.ConsideraDiasFijoSemana = Json.ConsideraDiasFijoSemana;
                    panelDeposito.IdDiaSemanaFijo = Json.ConsideraDiasFijoSemana == true ? Json.IdDiaSemanaFijo : null;
                    panelDeposito.HoraCorte = Json.HoraCorte;
                    panelDeposito.MinutoCorte = Json.MinutoCorte;
                    panelDeposito.PorcentajeCobro = Json.PorcentajeCobro;
                    panelDeposito.UsuarioModificacion = Json.UsuarioModificacion;
                    panelDeposito.FechaModificacion = DateTime.Now;
                    repPanelDepositoRep.Update(panelDeposito);
                    _unitOfWork.Commit();
                    scope.Complete();
                }
                Json.IdDiaSemanaFijo = panelDeposito.IdDiaSemanaFijo;
                return (Json);

            }
            catch (Exception Ex)
            {
                return (Ex.Message);
            }
        }



        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 06/10/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina de T_ConfiguracionDatoRemarketing para mostrarse en datos Grilla.
        /// </summary>
        /// <returns> List<> </returns>


        public object EliminarPanelDepositoDisponible( EliminarDTO eliminarDTO)
        {

            try
            {
             
                using (TransactionScope scope = new TransactionScope())
                {
                    var repPanelDepositoRep = _unitOfWork.PanelIngresoDisponibleRepository;
                    if (repPanelDepositoRep.Exist(eliminarDTO.Id))
                    {
                        repPanelDepositoRep.Delete(eliminarDTO.Id, eliminarDTO.NombreUsuario);
                        _unitOfWork.Commit();
                        scope.Complete();
                        return (true);
                    }
                    else
                    {
                        return ("Registro no existente");
                    }
                }
            }
            catch (Exception e)
            {
                return (e.Message);
            }
        }

    }
   }


       