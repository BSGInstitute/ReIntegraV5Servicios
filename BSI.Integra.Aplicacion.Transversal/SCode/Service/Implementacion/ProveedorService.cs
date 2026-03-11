using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Transactions;


namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ProveedorService
    /// Autor: Gilmer Quispe.
    /// Fecha: 14/10/2022
    /// <summary>
    /// Gestión general de Informacion de Proveedor
    /// </summary>
    public class ProveedorService : IProveedorService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ProveedorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<TProveedor, Proveedor>(MemberList.None).ReverseMap();
                    cfg.CreateMap<TProveedorTipoServicio, ProveedorTipoServicio>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public Proveedor Add(Proveedor entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Proveedor>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Proveedor Update(Proveedor entidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Proveedor>(modelo);
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
                _unitOfWork.ProveedorRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Proveedor> Add(List<Proveedor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Proveedor>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Proveedor> Update(List<Proveedor> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ProveedorRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Proveedor>>(modelo);
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
                _unitOfWork.ProveedorRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 20/07/2022
        /// Version: 1.0
        /// <summary>
        /// Se obtiene el Id y el Nombre de Proveedor filtrado por Producto.
        /// </summary>
        /// <returns> List<ProveedorComboDTO> </returns>
        /// <paramref name="idProducto"/>Identificador del producto
        public IEnumerable<ProveedorComboDTO> ObtenerProveedorPorProducto(int idProducto)
        {
            try
            {
                return _unitOfWork.ProveedorRepository.ObtenerProveedorPorProducto(idProducto);
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
        /// Se obtiene el Id,NroDocIdentidad,RazonSocial,IdTipoImpuesto,IdDetraccion,IdDetraccion y el IdPais 
        /// de fin.V_ObtenerProveedorRazonSocialRucNombres filtrado por NroDocIdentidad.
        /// </summary>
        /// <returns> List<ProveedorComboDTO> </returns>
        /// <paramref name="valor"/>NroDocIdentidad del Proveedor.
        public IEnumerable<ProveedorRucRazonSocialDTO> ObtenerProveedorPorRuc(string valor)
        {
            try
            {
                return _unitOfWork.ProveedorRepository.ObtenerProveedorPorRuc(valor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Autor Modificación: Jonathan Caipo
        /// Fecha Modificación: 05/05/2023
        /// Version: 1.1
        /// <summary>
        /// Se obtiene el Nombre del ProveedorHistorico
        /// Se Modificó el nombre de la función, de ObtenerNombreProveedorById a ObtenerNombreProveedorPorId.
        /// </summary>
        /// <returns> string </returns>
        /// <paramref name="id"/>Id del ProveedorHistorico.
        public string ObtenerNombreProveedorPorId(int id)
        {
            try
            {
                return _unitOfWork.ProveedorRepository.ObtenerNombreProveedorPorId(id);
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el nombre y Ruc de todos los proveedores, que esten activos segun la descripcion que se ingreso ya sea Ruc o Nombres del Proveedor
        /// </summary>
        /// <returns> List<ProveedorComboDTO> </returns>
        /// <paramref name="valor"/>Nombre o Documento
        public IEnumerable<FiltroRucProveedorDTO> ObtenerProveedorRucAutocomplete()
        {
            try
            {
                return _unitOfWork.ProveedorRepository.ObtenerProveedorRucAutocomplete();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<DTO.ComboDTO> ObtenerTodoCoordinadoresDocentes()
        {
            try
            {
                return _unitOfWork.ProveedorRepository.ObtenerTodoCoordinadoresDocentes();
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
        /// Obtiene lista de proveedores para combo de programa especifico FUR
        /// </summary>
        /// <returns> List<ProveedorProductoDTO> </returns>
        public IEnumerable<ProveedorDTO> ObtenerTodoProveedorById(int? Id)
        {
            try
            {
                return _unitOfWork.ProveedorRepository.ObtenerTodoProveedorById(Id);
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
        /// Obtiene lista de proveedores para combo de programa especifico FUR
        /// </summary>
        /// <returns> List<ProveedorDTO> </returns>
        /// <paramref name="Id"/>Identificador del Proveedor.
        public IEnumerable<ProveedorComboDTO> ObtenerProveedorCombo(string Texto)
        {
            try
            {
                return _unitOfWork.ProveedorRepository.ObtenerProveedorCombo(Texto);
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
        /// Obtiene La lista de Proveedores con estado de Cuenta Pagado o Pendiente (Usado para combobox)
        /// </summary>
        /// <returns> List<ProveedorComboDTO> </returns>
        public IEnumerable<ProveedorComboDTO> ObtenerProveedoresConEstadoCuentaPagadoPendiente()
        {
            try
            {
                return _unitOfWork.ProveedorRepository.ObtenerProveedoresConEstadoCuentaPagadoPendiente();
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
        /// Obtiene Los registros eliminados Logicamente de fin.T_Proveedor
        /// </summary>
        /// <returns> int </returns>
        /// <paramref name="email"/>
        public int? ObtenerProveedorEliminadoEmailRepetido(string email)
        {
            try
            {
                return _unitOfWork.ProveedorRepository.ObtenerProveedorEliminadoEmailRepetido(email);
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
        /// Actualiza el estado de fin.T_Proveedor 
        /// </summary>
        /// <returns> true, correcto  </returns>
        /// <paramref name="Id"/>Identificador de fin.T_Proveedor
        public bool ActivarProveedor(int Id)
        {
            try
            {
                return _unitOfWork.ProveedorRepository.ActivarProveedor(Id);
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
        /// obtiene datos para un combo
        /// </summary>
        /// <returns> List<ProveedorComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerNombreProveedorParaHonorario()
        {
            try
            {
                return _unitOfWork.ProveedorRepository.ObtenerNombreProveedorParaHonorario();
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Insserta Datos en T_Proveedor,T_ProveedorTipoServicio,T_Persona
        /// </summary>
        /// <returns> List<ProveedorComboDTO> </returns>
        /// <paramref name="proveedor"/>
        /// <paramref name="listaCuentaBanco"/>
        public int InsertarProveedorCuentaBanco(ProveedorWEnvioDTO proveedor, List<ProveedorCuentaBancoEnvioDTO> listaCuentaBanco)
        {
            try
            {
                var _repProveedorRep = _unitOfWork.ProveedorRepository;
                var serPersona = new PersonaService(_unitOfWork);
                var repProveedorTipoServicio = _unitOfWork.ProveedorTipoServicioRepository;
                var _repCuentaBancoRep = _unitOfWork.ProveedorCuentaBancoRepository;
                Proveedor objProveedor;
                var IdProveedorEmailRepetido = _repProveedorRep.ObtenerProveedorEliminadoEmailRepetido(proveedor.Email);
                if (IdProveedorEmailRepetido != null && IdProveedorEmailRepetido > 0)
                {
                    _repProveedorRep.ActivarProveedor(IdProveedorEmailRepetido.Value);
                    var ObjTProveedor = _repProveedorRep.FirstById(IdProveedorEmailRepetido.Value);
                    objProveedor = _mapper.Map<Proveedor>(ObjTProveedor);
                }
                else if (_repProveedorRep.ObtenerProveedorEmailRepetido(proveedor.Email) > 0)
                {
                    throw new Exception("Un proveedor ya existe para este correo: " + proveedor.Email.ToUpper());
                }
                else
                {
                    objProveedor = new Proveedor();
                    objProveedor.Estado = true;
                    objProveedor.FechaCreacion = DateTime.Now;
                    objProveedor.UsuarioCreacion = proveedor.UsuarioModificacion;
                }

                objProveedor.IdTipoContribuyente = proveedor.IdTipoContribuyente;
                objProveedor.IdDocumentoIdentidad = proveedor.IdDocumentoIdentidad;
                objProveedor.NroDocIdentidad = proveedor.NroDocumento;
                objProveedor.RazonSocial = proveedor.RazonSocial;
                objProveedor.Nombre1 = proveedor.Nombre1;
                objProveedor.Nombre2 = proveedor.Nombre2 ?? "";
                objProveedor.ApePaterno = proveedor.ApePaterno;
                objProveedor.ApeMaterno = proveedor.ApeMaterno;
                objProveedor.Direccion = proveedor.Direccion;
                objProveedor.Descripcion = proveedor.Descripcion ?? "";
                objProveedor.IdCiudad = proveedor.IdCiudad;
                objProveedor.Telefono = proveedor.Telefono ?? "";
                objProveedor.Email = proveedor.Email;
                objProveedor.Celular1 = proveedor.Celular1;
                objProveedor.Celular2 = proveedor.Celular2 ?? "";
                objProveedor.Contacto1 = proveedor.Contacto1;
                objProveedor.Contacto2 = proveedor.Contacto2 ?? "";
                objProveedor.IdPersonalAsignado = proveedor.IdPersonalAsignado;
                objProveedor.Alias = proveedor.Alias;
                objProveedor.EsDocente = proveedor.esDocente;
                objProveedor.FechaModificacion = DateTime.Now;
                objProveedor.UsuarioModificacion = proveedor.UsuarioModificacion;

                if (IdProveedorEmailRepetido == null || IdProveedorEmailRepetido == 0)
                {
                    objProveedor = this.Add(objProveedor);
                }
                else
                {
                    objProveedor = this.Update(objProveedor);
                }

                var IdPersonaClasificacion = serPersona.InsertarPersona(objProveedor.Id, Aplicacion.Base.Enums.Enums.TipoPersona.Proveedor, objProveedor.UsuarioCreacion);
                if (IdPersonaClasificacion == null)
                {
                    throw new Exception("Error al insertar el Tipo Persona Clasificacion");
                }

                foreach (var item in proveedor.ListaProveedorTipoServicio)
                {
                    var proveedorTipoServicio = new ProveedorTipoServicio
                    {
                        IdTipoServicio = item.IdTipoServicio,
                        UsuarioCreacion = proveedor.UsuarioModificacion,
                        UsuarioModificacion = proveedor.UsuarioModificacion,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        Estado = true
                    };
                    repProveedorTipoServicio.Add(proveedorTipoServicio);
                }

                foreach (var cuenta in listaCuentaBanco)
                {
                    var cuentaBanco = new ProveedorCuentaBanco
                    {
                        IdProveedor = objProveedor.Id,
                        IdEntidadFinanciera = cuenta.IdEntidadFinanciera,
                        IdTipoCuentaBanco = cuenta.IdTipoCuentaBanco,
                        NroCuenta = cuenta.NroCuenta,
                        CuentaInterbancaria = cuenta.CuentaInterbancaria,
                        IdMoneda = cuenta.IdMoneda,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = cuenta.UsuarioModificacion,
                        UsuarioModificacion = cuenta.UsuarioModificacion
                    };

                    _repCuentaBancoRep.Add(cuentaBanco);
                }

                _unitOfWork.Commit();

                return objProveedor.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza Datos en T_Proveedor,T_ProveedorTipoServicio,T_ProveedorCuentaBanco
        /// </summary>
        /// <returns> bool: true,ó mensajes de error </returns>
        /// <paramref name="proveedor"/>
        /// <paramref name="listaCuentaBanco"/>
        public bool ActualizarProveedorCuentaBanco(ProveedorWEnvioDTO proveedor, List<ProveedorCuentaBancoEnvioDTO> listaCuentaBanco)
        {
            try
            {
                var _repProveedorRep = _unitOfWork.ProveedorRepository;
                var _repProveedorTipoServicio = _unitOfWork.ProveedorTipoServicioRepository;
                var _repCuentaBancoRep = _unitOfWork.ProveedorCuentaBancoRepository;

                var objProveedor = _mapper.Map<Proveedor>(_repProveedorRep.FirstById(proveedor.Id));
                if (objProveedor == null)
                    throw new Exception("No se encontró el registro de 'Proveedor' que se quiere actualizar");

                _repProveedorTipoServicio.EliminacionLogicoPorPlantilla(proveedor.Id, proveedor.UsuarioModificacion, proveedor.ListaProveedorTipoServicio.Select(x => x.IdTipoServicio).ToList());

                objProveedor.RazonSocial = proveedor.RazonSocial;
                objProveedor.Nombre1 = proveedor.Nombre1;
                objProveedor.Nombre2 = proveedor.Nombre2;
                objProveedor.ApePaterno = proveedor.ApePaterno;
                objProveedor.ApeMaterno = proveedor.ApeMaterno;
                objProveedor.Direccion = proveedor.Direccion;
                objProveedor.Descripcion = proveedor.Descripcion ?? "";
                objProveedor.IdCiudad = proveedor.IdCiudad;
                objProveedor.Telefono = proveedor.Telefono;
                objProveedor.Email = proveedor.Email;
                objProveedor.Celular1 = proveedor.Celular1;
                objProveedor.Celular2 = proveedor.Celular2;
                objProveedor.Contacto1 = proveedor.Contacto1;
                objProveedor.Contacto2 = proveedor.Contacto2;
                objProveedor.IdTipoImpuestoPreferido = proveedor.IdImpuesto;
                objProveedor.IdRetencionPreferido = proveedor.IdRetencion;
                objProveedor.IdDetraccionPreferido = proveedor.IdDetraccion;
                objProveedor.IdPersonalAsignado = proveedor.IdPersonalAsignado;
                objProveedor.FechaModificacion = DateTime.Now;
                objProveedor.UsuarioModificacion = proveedor.UsuarioModificacion;
                objProveedor.Alias = proveedor.Alias;
                objProveedor.EsDocente = proveedor.esDocente;


                objProveedor = this.Update(objProveedor);

                foreach (var item in proveedor.ListaProveedorTipoServicio)
                {
                    var proveedorTipoServicio = _mapper.Map<ProveedorTipoServicio>(_repProveedorTipoServicio
                        .FirstBy(x => x.IdTipoServicio == item.IdTipoServicio && x.IdProveedor == proveedor.Id));

                    if (proveedorTipoServicio != null)
                    {
                        proveedorTipoServicio.IdTipoServicio = item.IdTipoServicio;
                        proveedorTipoServicio.UsuarioModificacion = proveedor.UsuarioModificacion;
                        proveedorTipoServicio.FechaModificacion = DateTime.Now;

                        _repProveedorTipoServicio.Update(proveedorTipoServicio);
                    }
                    else
                    {
                        proveedorTipoServicio = new ProveedorTipoServicio()
                        {
                            IdTipoServicio = item.IdTipoServicio,
                            UsuarioCreacion = proveedor.UsuarioModificacion,
                            UsuarioModificacion = proveedor.UsuarioModificacion,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            Estado = true
                        };
                        _repProveedorTipoServicio.Add(proveedorTipoServicio);
                    }
                }

                foreach (var cuenta in listaCuentaBanco)
                {
                    if (cuenta.Id == 0)
                    {
                        var cuentaBanco = new ProveedorCuentaBanco()
                        {
                            IdProveedor = objProveedor.Id,
                            IdEntidadFinanciera = cuenta.IdEntidadFinanciera,
                            IdTipoCuentaBanco = cuenta.IdTipoCuentaBanco,
                            NroCuenta = cuenta.NroCuenta,
                            CuentaInterbancaria = cuenta.CuentaInterbancaria,
                            IdMoneda = cuenta.IdMoneda,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = proveedor.UsuarioModificacion,
                            UsuarioModificacion = proveedor.UsuarioModificacion
                        };

                        _repCuentaBancoRep.Add(cuentaBanco);
                    }
                    else
                    {
                        var cuentaProveedor = _repCuentaBancoRep.FirstById(cuenta.Id);

                        if (cuentaProveedor == null)
                            throw new Exception("No se encontró el registro de 'CuentaProveedor' que se quiere actualizar");

                        cuentaProveedor.NroCuenta = cuenta.NroCuenta;
                        cuentaProveedor.CuentaInterbancaria = cuenta.CuentaInterbancaria;
                        cuentaProveedor.IdMoneda = cuenta.IdMoneda;
                        cuentaProveedor.FechaModificacion = DateTime.Now;
                        cuentaProveedor.UsuarioModificacion = proveedor.UsuarioModificacion;

                        _repCuentaBancoRep.Update(cuentaProveedor);
                    }
                }

                _unitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// obtiene El nombre del proveedor
        /// </summary>
        /// <returns> object </returns>
        /// <paramref name="Filtros"/>
        public object ObtenerNombreProveedor()
        {
            try
            {
                var _repProveedorRep = _unitOfWork.ProveedorRepository;
                return _repProveedorRep.GetBy(x => x.Estado, x => new { Id = x.Id, Nombre = "(" + x.NroDocIdentidad + ") " + (x.RazonSocial ?? "") + " " + (x.ApePaterno ?? "") + " " + (x.ApeMaterno ?? "") + ", " + (x.Nombre1 ?? "") + " " + (x.Nombre2 ?? "") }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// obtiene El nombre del proveedor
        /// </summary>
        /// <returns> object </returns>
        /// <paramref name="Filtros"/> Cadena de string que contiene el nombre del Proveedor
        public IEnumerable<ProveedorComboDTO> ObtenerNombreProveedorAutocomplete()
        {
            try
            {
                var _repProveedorRep = _unitOfWork.ProveedorRepository;
                return _repProveedorRep.ObtenerNombreProveedorAutocomplete("");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Realiza una eliminacion logica de T_proveedor,T_TipoServicio
        /// </summary>
        /// <returns> object </returns>
        /// <paramref name="Id"/>Identificador delProveedor
        /// <paramref name="Usuario"/> Usuaraio de eliminacion
        public bool EliminarProveedor(int Id, string Usuario)
        {
            try
            {

                using (TransactionScope scope = new TransactionScope())
                {
                    var serProveedorCuentaBanco = new ProveedorCuentaBancoService(_unitOfWork);
                    var _repProveedorCuentaBanco = _unitOfWork.ProveedorCuentaBancoRepository;
                    var _repProveedorRep = _unitOfWork.ProveedorRepository;
                    var _repProveedorTipoServicioRep = _unitOfWork.ProveedorTipoServicioRepository;
                    if (_repProveedorRep.Exist(Id))
                    {
                        this.Delete(Id, Usuario);
                        _repProveedorTipoServicioRep.Delete(_repProveedorTipoServicioRep.GetBy(x => x.IdProveedor == Id).Select(x => x.Id), Usuario);
                        if (_repProveedorCuentaBanco.Exist(x => x.IdProveedor == Id))
                        {
                            var ProveedorCuentas = serProveedorCuentaBanco.ObtenerCuentasProveedorById(Id);
                            foreach (var cuenta in ProveedorCuentas)
                            {
                                serProveedorCuentaBanco.Delete(cuenta.Id, Usuario);
                            }
                        }
                        scope.Complete();
                        return true;
                    }
                    else
                    {
                        throw new Exception("Registro no existente");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Valida la exitencia del Proveedor.
        /// </summary>
        /// <returns> bool </returns>
        /// <paramref name="DocidentidadEmail"/>NroDocIdentidad,Email.
        public bool ValidarExistenciaProveedor(CadenaStringDTO DocidentidadEmail)
        {
            try
            {
                var _repProveedorRep = _unitOfWork.ProveedorRepository;
                var proveedor = _repProveedorRep.GetBy(x => (x.NroDocIdentidad == DocidentidadEmail.Cadena1) || (x.Email == DocidentidadEmail.Cadena2), x => new { Id = x.Id }).ToList();
                if (proveedor.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna un grupo de objetos {impuesto,retencion,detraccion}.
        /// </summary>
        /// <returns> object </returns>
        public object ObtenerImpuestosProveedor()
        {
            try
            {
                var repImpuestoRep = _unitOfWork.TipoImpuestoRepository;
                var repRetencionRep = _unitOfWork.RetencionRepository;
                var repDetraccionRep = _unitOfWork.DetraccionRepository;

                var impuesto = repImpuestoRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, Nombre = x.Valor + "% - " + x.Nombre, IdPais = x.IdPais }).ToList();
                var retencion = repRetencionRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, Nombre = x.Valor + "% - " + x.Nombre, IdPais = x.IdPais }).ToList();
                var detraccion = repDetraccionRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, Nombre = x.Valor + "% - " + x.Nombre, IdPais = x.IdPais }).ToList();

                return new { impuesto, retencion, detraccion };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos apra el llenado de una grilla.
        /// </summary>
        /// <returns> object </returns>
        /// <param name="IdProveedor">Identificador del proveedor</param>
        public IEnumerable<ProveedorDTO> ObtenerProveedorGrilla(int? IdProveedor)
        {
            try
            {
                var repProveedorRep = _unitOfWork.ProveedorRepository;
                var _repProveedorTipoServicio = _unitOfWork.ProveedorTipoServicioRepository;
                var listadoProveedor = repProveedorRep.ObtenerTodoProveedorById(IdProveedor);
                var listaProveedorTipoServicio = _repProveedorTipoServicio.ObtenerProveedorTipoServicio(listadoProveedor.Select(x => x.Id).ToList());
                foreach (var item in listadoProveedor)
                {
                    item.ListaProveedorTipoServicio = listaProveedorTipoServicio.Where(x => x.IdProveedor == item.Id).ToList();
                }
                return listadoProveedor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// Autor: Gilmer Quispe.
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de Proveedor por su Id
        /// </summary>
        /// <param name="idProveedor"> Id de Expositor </param>
        /// <returns> ProveedorDTO </returns>
        public ProveedorDTO ObtenerProveedorPorId(int idProveedor)
        {
            try
            {
                return _unitOfWork.ProveedorRepository.ObtenerProveedorPorId(idProveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de Proveedor por su Id
        /// </summary>
        /// <param name="idProveedor"> Id de Expositor </param>
        /// <returns> ProveedorDTO </returns>
        public IEnumerable<ComboDTO> ObtenerProveedorFiltro()
        {
            try
            {
                return _unitOfWork.ProveedorRepository.ObtenerProveedorFiltro();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 03/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene lista de proveedores para Filtro
        /// </summary>
        /// <returns>Lista DTO - List<FiltroDTO> - rpta </returns>
        public List<FiltroConvocatoriaPersonalDTO> ObtenerProveedoresConvocatoriaPersonal()
        {
            try
            {
                return _unitOfWork.ProveedorRepository.ObtenerProveedoresConvocatoriaPersonal().ToList();
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 25/05/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtener la lista de docentes 
        /// </summary>
        /// <returns>Lista del docentes para el filtro en un List<ItemComboAutocompleDTO></returns>
        public IEnumerable<ComboDTO> ObtenerListaDocentes()
        {
            try
            {
                return _unitOfWork.ProveedorRepository.ObtenerListaDocentes().ToList();
            }
            catch
            {
                throw;
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar Serruto.
        /// Fecha: 28/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Accesos
        /// </summary>
        /// <param name="Filtro">Filtros de Búsqueda</param>
        /// <returns>Confirmación Bool y DTO de Tipo: List<RespuestaReporteRevisionDocenteDTO></returns>
        public List<RespuestaReporteRevisionDocenteDTO> GenerarReporte(ReporteRevisionDocenteDTO Filtro)
        {
            try
            {
                 
                List<RespuestaReporteRevisionDocenteDTO> resultado = new List<RespuestaReporteRevisionDocenteDTO>();

                var condicion = GenerarCondicionReporteForo(Filtro);
                if (Filtro.IdCategoriaRevision == 1)
                {
                    var listaGeneradaForo = _unitOfWork.ProveedorRepository.GenerarReporteRevisionForo(condicion);
                    resultado.AddRange(listaGeneradaForo);
                }
                else if (Filtro.IdCategoriaRevision == 2)
                {
                    var listaGeneradaProyecto = _unitOfWork.ProveedorRepository.GenerarReporteProyecto(condicion);
                    resultado.AddRange(listaGeneradaProyecto);
                }
                else
                {
                    var listaGeneradaForo = _unitOfWork.ProveedorRepository.GenerarReporteRevisionForo(condicion);
                    resultado.AddRange(listaGeneradaForo);
                    var listaGeneradaProyecto = _unitOfWork.ProveedorRepository.GenerarReporteProyecto(condicion);
                    resultado.AddRange(listaGeneradaProyecto);
                }
                if (resultado.Count > 0)
                    return resultado;
                else
                    return resultado;

            }
            catch (Exception e)
            {
                throw;
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar Serruto.
        /// Fecha: 28/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera Reporte de Accesos
        /// </summary>
        /// <param name="Filtro">Filtros de Búsqueda</param>
        /// <returns>Confirmación Bool y DTO de Tipo: List<RespuestaReporteRevisionDocenteDTO></returns>
        public string GenerarCondicionReporteForo(ReporteRevisionDocenteDTO filtro)
        {
            try
            {
                string condicion = string.Empty;
                var filtros = new
                {
                    ListaArea = filtro.ListaArea == null ? "" : string.Join(",", filtro.ListaArea.Select(x => x)),
                    ListaSubArea = filtro.ListaSubArea == null ? "" : string.Join(",", filtro.ListaSubArea.Select(x => x)),
                    ListaProgramaGeneral = filtro.ListaProgramaGeneral == null ? "" : string.Join(",", filtro.ListaProgramaGeneral.Select(x => x)),
                    ListaProveedor = filtro.ListaDocente == null ? "" : string.Join(",", filtro.ListaDocente)
                };
                if (filtros.ListaArea.Length > 0)
                {
                    if (condicion.Length > 0)
                    {
                        condicion = condicion + " AND IdArea IN (" + filtros.ListaArea + ")";
                    }
                    else
                    {
                        condicion = condicion + " IdArea IN (" + filtros.ListaArea + ")";
                    }
                }
                if (filtros.ListaSubArea.Length > 0)
                {
                    if (condicion.Length > 0)
                    {
                        condicion = condicion + " AND IdSubArea IN (" + filtros.ListaSubArea + ")";
                    }
                    else
                    {
                        condicion = condicion + " IdSubArea IN (" + filtros.ListaSubArea + ")";
                    }
                }
                if (filtros.ListaProgramaGeneral.Length > 0)
                {
                    if (condicion.Length > 0)
                    {
                        condicion = condicion + " AND IdPGeneral IN (" + filtros.ListaProgramaGeneral + ")";
                    }
                    else
                    {
                        condicion = condicion + " IdPGeneral IN (" + filtros.ListaProgramaGeneral + ")";
                    }
                }
                if (filtros.ListaProveedor.Length > 0)
                {
                    if (condicion.Length > 0)
                    {
                        condicion = condicion + " AND IdProveedor IN (" + filtros.ListaProveedor + ")";
                    }
                    else
                    {
                        condicion = condicion + " IdProveedor IN (" + filtros.ListaProveedor + ")";
                    }
                }
                return condicion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        /// Autor: Eliot Arias F.
        /// Fecha: 2024-10-25
        /// Version: 1.0
        /// <summary>
        /// Retorna la lista de proveedores PAGINAS RECLUTADORAS DE CV 
        /// </summary>
        /// <returns>IEnumerable<ProveedorComboDTO></returns>
        public IEnumerable<ProveedorComboDTO> ObtenerProveedoresPaginasReclutadoras()
        {
            try
            {
                return _unitOfWork.ProveedorRepository.ObtenerProveedoresPaginasReclutadoras();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
