using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: PlanContableService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_PlanContable
    /// </summary>
    public class PlanContableService : IPlanContableService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public PlanContableService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPlanContable, PlanContable>(MemberList.None).ReverseMap();
                cfg.CreateMap<PlanContableDatosDTO, PlanContable>(MemberList.None).ReverseMap();
            }
                 );
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public PlanContable Add(PlanContableDatosDTO entidad,string Usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    PlanContable data = _mapper.Map<PlanContable>(entidad);
                    data.Id = 0;
                    data.UsuarioModificacion = Usuario;
                    data.UsuarioCreacion = Usuario;
                    data.FechaCreacion = DateTime.Now;
                    data.FechaModificacion = DateTime.Now;
                    data.Estado = true;

                    var modelo = _unitOfWork.PlanContableRepository.Add(data);
                    _unitOfWork.Commit();
                    scope.Complete();
                    return _mapper.Map<PlanContable>(modelo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PlanContable Update(PlanContableDatosDTO entidad, string Usuario)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var rep = _unitOfWork.PlanContableRepository;
                    var entidadActual = _mapper.Map<PlanContable>(rep.FirstById(entidad.Id));
                    var idFurActual = entidadActual.IdFurTipoSolicitud;
                    entidadActual.UsuarioModificacion = Usuario;
                    entidadActual.FechaModificacion = DateTime.Now;
                    entidadActual.Cuenta = entidad.Cuenta;
                    entidadActual.Descripcion = entidad.Descripcion;
                    entidadActual.Padre = entidad.Padre;
                    entidadActual.Cbal = entidad.Cbal;
                    entidadActual.Debe = entidad.Debe;
                    entidadActual.Haber = entidad.Haber;
                    entidadActual.IdPlanContableTipoCuenta = entidad.IdPlanContableTipoCuenta;
                    entidadActual.Analisis = entidad.Analisis;
                    entidadActual.CentroCosto = entidad.CentroCosto;
                    entidadActual.IdFurTipoSolicitud = entidad.IdFurTipoSolicitud;


                    var modelo = _unitOfWork.PlanContableRepository.Update(entidadActual);
                    _unitOfWork.Commit();

                    if (idFurActual != entidad.IdFurTipoSolicitud)
                    {
                        _unitOfWork.PlanContableRepository.ActualizarRubroPlanContable(entidad.Cuenta, Usuario, entidad.IdFurTipoSolicitud.Value);
                    }

                    scope.Complete();

                    return _mapper.Map<PlanContable>(modelo);
                }

                    
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
                _unitOfWork.PlanContableRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PlanContable> Add(List<PlanContable> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PlanContableRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PlanContable>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PlanContable> Update(List<PlanContable> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.PlanContableRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<PlanContable>>(modelo);
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
                _unitOfWork.PlanContableRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor Modificacion: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PlanContable
        /// </summary>
        /// <returns> List<PlanContableDTO> </returns>
        public IEnumerable<PlanContableDTO> ObtenerPlanContable()
        {
            try
            {
                return _unitOfWork.PlanContableRepository.ObtenerPlanContable();
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
        /// Obtiene registros de T_PlanContable para mostrarse en combo.
        /// </summary>
        /// <returns> List<PlanContableComboDTO> </returns>
        public IEnumerable<PlanContableComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.PlanContableRepository.ObtenerCombo();
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
        /// Obtiene Id,Nombre T_PlanContableTipoCuenta.
        /// </summary>
        /// <returns> List<PlanContableComboDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerPlanContableTipoCuenta()
        {
            try
            {
                return _unitOfWork.PlanContableRepository.ObtenerPlanContableTipoCuenta();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene las cuentas hijas de la clase Actual que se digita.
        /// </summary>
        /// <returns> List<PlanContableDTO> </returns>
        /// <param name="Cuenta"></param>
        public IEnumerable<PlanContableDTO> ObteneCuentasHijo(long cuenta)
        {
            try
            {
                return _unitOfWork.PlanContableRepository.ObteneCuentasHijo(cuenta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de PlanContable dado un Indicio de Nombre, usado para AutoComplete [Cuenta->Id, Descripcion->Nombre]
        /// </summary>
        /// <returns> List<PlanContableComboDTO> </returns>
        /// <param name="NombreParcial"></param>
        public IEnumerable<PlanContableCuentasDTO> ObtenerPlanContableAutoComplete()
        {
            try
            {
                return _unitOfWork.PlanContableRepository.ObtenerPlanContableAutoComplete();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de PlanContable dado un Indicio de Nombre, usado para AutoComplete [Cuenta->Id, Descripcion->Nombre]
        /// </summary>
        /// <returns> List<PlanContableComboDTO> </returns>
        /// <param name="NombreParcial"></param>
        public IEnumerable<PlanContableComboDTO> ObtenerPlanContableFiltro(string NombreParcial)
        {
            try
            {
                return _unitOfWork.PlanContableRepository.ObtenerPlanContableFiltro(NombreParcial);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la lista de Plan Contable con su rubro asociado (para llenado de grilla en modulo AsociarPlanContableRubro)
        /// </summary>
        /// <returns> List<PlanContableComboDTO> </returns>
        public IEnumerable<PlanContableConRubroDTO> ObtenerPlanContableConRubro()
        {
            try
            {
                return _unitOfWork.PlanContableRepository.ObtenerPlanContableConRubro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un registro de Plan Contable con su rubro asociado (para llenado de grilla en modulo AsociarPlanContableRubro)
        /// </summary>
        /// <returns> List<PlanContableComboDTO> </returns>
        /// <param  name="IdPlanContable"></param>
        public IEnumerable<PlanContableConRubroDTO> ObtenerPlanContableConRubro(int IdPlanContable)
        {
            try
            {
                return _unitOfWork.PlanContableRepository.ObtenerPlanContableConRubro(IdPlanContable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta un nuevo registro y actualiza en T_PlanContable
        /// </summary>
        /// <returns> List<PlanContableComboDTO> </returns>
        /// <param  name="Json">entidad PlanContable</param>
        public PlanContable InsertarCuentaContable(PlanContableDatosDTO Json,string Usuario)
        {
            try
            {
                var repPlanContableRep = _unitOfWork.PlanContableRepository;

                bool escuentahijo = repPlanContableRep.ObteneCuentasHijo(Json.Cuenta).Count() == 0 ? true : false;
                Json.Univel = escuentahijo;
                var RespuestaInsert = this.Add(Json,Usuario);
                Json.Id = RespuestaInsert.Id;

                if (Json.Padre != 0)
                {
                    int idPadre = repPlanContableRep.FirstBy(w => w.Cuenta == Json.Padre).Id;
                    var cuentaContable = repPlanContableRep.FirstById(idPadre);
                    escuentahijo = repPlanContableRep.ObteneCuentasHijo(Json.Padre).Count() == 0 ? true : false;
                    using (TransactionScope scope = new TransactionScope())
                    {
                        cuentaContable.Univel = escuentahijo;
                        repPlanContableRep.Update(cuentaContable);
                        _unitOfWork.Commit();
                        scope.Complete();
                    }
                }
                return _mapper.Map<PlanContable>(Json); ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Actualzia un registro y actualiza las cuentas hijos en T_PlanContable
        /// </summary>
        /// <returns> List<PlanContableComboDTO> </returns>
        /// <param  name="Json">entidad PlanContable</param>
        public PlanContable ActualizarCuentaContable(PlanContableDatosDTO Json,string Usuario)
        {

            try
            {
                var repPlanContableRep = _unitOfWork.PlanContableRepository;
                var cuentaContable = repPlanContableRep.FirstById(Json.Id);
                var escuentahijo = repPlanContableRep.ObteneCuentasHijo(Json.Cuenta).Count() == 0 ? true : false;
                Json.Univel = escuentahijo;
                int padreAnterior = cuentaContable.Padre;
                var RespuestaUpdate = this.Update(Json,Usuario);

                if (padreAnterior != Json.Padre && Json.Padre != 0)
                {
                    int idPadre = repPlanContableRep.FirstBy(w => w.Cuenta == Json.Padre).Id;
                    cuentaContable = repPlanContableRep.FirstById(idPadre);
                    escuentahijo = repPlanContableRep.ObteneCuentasHijo(Json.Padre).Count() == 0 ? true : false;
                    using (TransactionScope scope = new TransactionScope())
                    {
                        cuentaContable.Univel = escuentahijo;
                        repPlanContableRep.Update(cuentaContable);
                        _unitOfWork.Commit();
                        scope.Complete();
                    }
                    if (padreAnterior != 0)
                    {
                        int idPadreAnterior = repPlanContableRep.FirstBy(w => w.Cuenta == padreAnterior).Id;
                        cuentaContable = repPlanContableRep.FirstById(idPadreAnterior);
                        escuentahijo = repPlanContableRep.ObteneCuentasHijo(padreAnterior).Count() == 0 ? true : false;
                        using (TransactionScope scope = new TransactionScope())
                        {
                            cuentaContable.Univel = escuentahijo;
                            repPlanContableRep.Update(cuentaContable);
                            _unitOfWork.Commit();
                            scope.Complete();
                        }
                    }
                }

                return _mapper.Map<PlanContable>(Json);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Elimina logicamente un registro en T_PlanContable 
        /// </summary>
        /// <returns> List<PlanContableComboDTO> </returns>
        /// <param  name="id">id Registro a eliminar </param>
        /// <param  name="usuario">usuario que realiza la accion</param>
        public bool EliminarCuentaContable(int id, string usuario)
        {
            try
            {
                var repPlanContableRep = _unitOfWork.PlanContableRepository;
                if (repPlanContableRep.Exist(id))
                {
                    long cuentaPadre = repPlanContableRep.FirstBy(w => w.Id == id).Padre;
                    this.Delete(id, usuario);
                    if (cuentaPadre != 0)
                    {
                        int idPadre = repPlanContableRep.FirstBy(w => w.Cuenta == cuentaPadre).Id;
                        var cuentaContable = repPlanContableRep.FirstById(idPadre);
                        var escuentahijo = repPlanContableRep.ObteneCuentasHijo(cuentaPadre).Count() == 0 ? true : false;
                        cuentaContable.Univel = escuentahijo;
                        repPlanContableRep.Update(cuentaContable);
                        _unitOfWork.Commit();
                    }
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        // Autor Modificacion: Margiory Ramirez
        /// Fecha: 20/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Id,Nombre T_PlanContableTipoCuenta.
        /// </summary>
        /// <returns> List<PlanContableComboDTO> </returns>
        public List<PlanContableFiltroDTO> ObtenerPlanContableAutoComplete(string NombreParcial)
        {
            try
            {
                return _unitOfWork.PlanContableRepository.ObtenerPlanContableAutoComplete(NombreParcial);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // Autor: Rodrigo Montesinos
        /// Fecha: 11/01/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene lsita de elementos pro proveedores
        /// </summary>
        /// <returns> string </returns>
        public string ObtenerListaElementosProveedor(string NombreParcial)
        {
            try
            {
                if (NombreParcial == null || NombreParcial.Trim().Equals(""))
                {
                    Object[] ListaVacia = new object[0];
                    return JsonConvert.SerializeObject(new { Result = "OK", Records = ListaVacia });
                }
                ProveedorService _repoProveedor = new ProveedorService(_unitOfWork);
                var lista = _repoProveedor.ObtenerProveedorPorRuc(NombreParcial);
                return JsonConvert.SerializeObject(new { Result = "OK", Records = lista });
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }

}
