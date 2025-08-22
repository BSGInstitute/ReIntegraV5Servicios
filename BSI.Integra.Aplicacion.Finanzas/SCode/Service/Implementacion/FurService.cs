using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Globalization;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: FurService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_Fur
    /// </summary>
    public class FurService : IFurService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public Mapper mapperFur;

        public FurService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TFur, Fur>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
            mapperFur = new Mapper(config);
        }

        #region Metodos Base
        public Fur Add(Fur entidad)
        {
            try
            {
                var modelo = _unitOfWork.FurRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Fur>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Fur Update(Fur entidad)
        {
            try
            {
                var modelo = _unitOfWork.FurRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<Fur>(modelo);
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
                _unitOfWork.FurRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Fur> Add(List<Fur> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FurRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Fur>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Fur> Update(List<Fur> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FurRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<Fur>>(modelo);
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
                var respuesta = _unitOfWork.FurRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Griselberto Huaman.
        /// Fecha: 18/09/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el estado de OcupadoSolicitud = 0
        /// </summary>
        /// <returns> List<CajaPorRendirDTO> </returns>
        public bool CambiarEstadoFurSolicitudCajaPR(int idFur, bool estado)
        {
            try
            {
                var repFur = _unitOfWork.FurRepository;
                Fur fur = new Fur();
                fur = _mapper.Map<Fur>(repFur.FirstById(idFur));
                fur.OcupadoSolicitud = estado;
                this.Update(fur);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Furs Para CajaPorRendir.
        /// </summary>
        /// <returns> List<CajaResponsableDTO> </returns>
        public object ObtenerDatosFur()
        {
            try
            {
                return _unitOfWork.FurRepository.ObtenerDatosFur();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Furs Para AutoComplete
        /// </summary>
        /// <returns> List<CajaResponsableDTO> </returns>
        public object ObtenerDatosFurAutocomplete(string codigo)
        {
            try
            {
                return _unitOfWork.FurRepository.ObtenerDatosFurAutocomplete(codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Furs para RegistroEgresoCaja
        /// </summary>
        /// <returns> List<CajaResponsableDTO> </returns>
        public object ObtenerFursREC()
        {
            try
            {
                return _unitOfWork.FurRepository.ObtenerFursREC();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Furs para RegistroEgresoCaja
        /// </summary>
        /// <returns> List<CajaResponsableDTO> </returns>
        public object ObtenerDatosFurSolicitudEfectivo(string codigo)
        {
            try
            {
                return _unitOfWork.FurRepository.ObtenerDatosFurSolicitudEfectivo(codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
		/// Fecha: 01/06/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene la lista de los furs segun los parametros de la vista
		/// </summary>
		/// <param name="json">Json que contiene el area, usuario, ciudad, año, semana y estado</param>
		/// <returns>FurDTO</returns>
        public IEnumerable<FurDTO> ObtenerFursParaGrid(ParametrosFurDTO json)
        {
            var repFur = _unitOfWork.FurRepository;
            var repModoPersonalFur = _unitOfWork.ModoPersonalFurRepository;
            IEnumerable<FurDTO> ListFur = new List<FurDTO>();
            var modoFur = repModoPersonalFur.ObtenerPermisosFurByIdPersonal(json.IdPersonal);
            if (modoFur == null)
            {
                modoFur = new ModoPersonalFurDTO
                {
                    IdPer = json.IdPersonal,
                    ModoFur = 1,
                    Nombres = "",
                    FurVencido = false
                };
            }
            if (json.IdRol == 1 || json.IdRol == 2)
            {
                if (json.IdEstadoFaseAprobacion1 == 5) //aprobados por jefe de finanzas
                {
                    modoFur.ModoFur = 2; // //mostrar de todas las áreas los FURs aprobados por el jefe de finanzas
                }
            }
            var parametros = new ParametrosFurGrillaDTO
            {
                IdArea = json.IdArea,
                Codigo = json.Codigo,
                IdRol = json.IdRol,
                IdEstadoFaseAprobacion1 = json.IdEstadoFaseAprobacion1,
                UserName = "",
                modo = modoFur,
                anio = json.Anio,
                semana = json.Semana,
                idCiudad = json.IdCiudad
            };

            if (json.IdEstadoFaseAprobacion1 == 7)
            {
                ListFur = repFur.ObtenerFursParaGrid(parametros);
            }
            else
            {
                parametros.UserName = json.Usuario;
                ListFur = repFur.ObtenerFursParaGrid(parametros);
            }
            return ListFur;
        }


        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Fur por Codigo
        /// </summary>
        /// <returns> List<CajaResponsableDTO> </returns>
        public IEnumerable<FurDTO> ObtenerFursBusquedaCodigo(string codigo)
        {
            try
            {
                return _unitOfWork.FurRepository.ObtenerFursBusquedaCodigo(codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Fur por Caja egreso
        /// </summary>
        /// <returns> List<CajaResponsableDTO> </returns>
        public List<FurCajaPRDTO> ObtenerDatosFurcajaEgreso(string codigo, int IdCajaPorRendirCabecera)
        {
            try
            {
                return _unitOfWork.FurRepository.ObtenerDatosFurcajaEgreso(codigo, IdCajaPorRendirCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Fur servicios de proveedor
        /// </summary>
        /// <returns> List<CajaResponsableDTO> </returns>
        public IEnumerable<ProductoFurDTO> ObtenerProductoFur(int IdProveedor)
        {
            try
            {
                return _unitOfWork.FurRepository.ObtenerProductoFur(IdProveedor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        ///  Obtiene Furs que estan en proceso de eliminacion, 
        /// </summary>
        /// <returns> List<CajaResponsableDTO> </returns>
        public IEnumerable<FurAprobadoNoEjecutadoDTO> ObtenerFursNoEjecutados()
        {
            try
            {
                return _unitOfWork.FurRepository.ObtenerFursNoEjecutados();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Actualiza el FUR
        /// </summary>
        /// <returns>Fur </returns>
        public Fur ActualizarFur(FurDTO Json)
        {

            try
            {
                var repFur = _unitOfWork.FurRepository;
                var fur = _mapper.Map<Fur>(repFur.FirstById(Json.Id));

                using (TransactionScope scope = new TransactionScope())
                {
                    fur.IdCiudad = Json.IdCiudad;
                    fur.IdFurTipoPedido = Json.IdFurTipoPedido;
                    fur.NumeroSemana = Json.NumeroSemana.Value;
                    fur.IdCentroCosto = Json.IdCentroCosto.Value;
                    fur.NumeroCuenta = Json.NumeroCuenta;
                    fur.Cuenta = Json.Cuenta;
                    fur.IdProveedor = Json.IdProveedor;
                    fur.IdProducto = Json.IdProducto;
                    fur.Cantidad = Json.Cantidad;
                    fur.IdProductoPresentacion = Json.IdProductoPresentacion.Value;
                    fur.Descripcion = Json.Descripcion;
                    fur.FechaLimite = Json.FechaLimite;
                    fur.PrecioUnitarioMonedaOrigen = Json.PrecioUnitarioMonedaOrigen;
                    fur.PrecioUnitarioDolares = Json.PrecioUnitarioDolares;
                    fur.IdMonedaProveedor = Json.IdMoneda_Proveedor;
                    fur.PrecioTotalMonedaOrigen = Json.PrecioUnitarioMonedaOrigen * Json.Cantidad;
                    fur.PrecioTotalDolares = Json.PrecioUnitarioDolares * Json.Cantidad;
                    fur.IdMonedaPagoReal = Json.IdMonedaPagoReal;
                    fur.UsuarioModificacion = Json.UsuarioModificacion;
                    fur.FechaModificacion = DateTime.Now;
                    fur.IdEmpresa = Json.IdEmpresa;

                    fur = this.Update(fur);
                    scope.Complete();
                }

                return fur;

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta el FUR
        /// </summary>
        /// <returns> Fur </returns>
        public Fur InsertarFur(FurDTO Json)
        {
            try
            {
                var repFur = _unitOfWork.FurRepository;
                Fur fur = new Fur();
                int correlativo = 0;
                var listCodigos = repFur.GetBy(x => x.Estado == true && x.Codigo.Contains(Json.Codigo), x => new { x.Codigo }).ToList();
                if (listCodigos != null)
                {
                    foreach (var Codigos in listCodigos)
                    {
                        if (Int32.Parse(Codigos.Codigo.Substring(Codigos.Codigo.LastIndexOf("-") + 1).Trim()) > correlativo)
                        {
                            correlativo = Int32.Parse(Codigos.Codigo.Substring(Codigos.Codigo.LastIndexOf("-") + 1).Trim());
                        }
                    }
                    correlativo++;
                }
                else
                {
                    correlativo = 1;
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    fur.Codigo = Json.Codigo + correlativo;
                    fur.IdPersonalAreaTrabajo = Json.IdPersonalAreaTrabajo;
                    fur.IdCiudad = Json.IdCiudad;
                    fur.IdFurTipoPedido = Json.IdFurTipoPedido;
                    fur.NumeroSemana = Json.NumeroSemana.Value;
                    fur.NumeroFur = Json.Codigo + correlativo;
                    fur.UsuarioSolicitud = Json.UsuarioModificacion;
                    fur.IdCentroCosto = Json.IdCentroCosto.Value;
                    fur.NumeroCuenta = Json.NumeroCuenta;
                    fur.Cuenta = Json.Cuenta;
                    fur.IdProveedor = Json.IdProveedor.Value;
                    fur.IdProducto = Json.IdProducto.Value;
                    fur.Cantidad = Json.Cantidad;
                    fur.IdProductoPresentacion = Json.IdProductoPresentacion.Value;
                    fur.Descripcion = Json.Descripcion;
                    fur.FechaLimite = Json.FechaLimite;
                    fur.PrecioUnitarioMonedaOrigen = Json.PrecioUnitarioMonedaOrigen;
                    fur.PrecioUnitarioDolares = Json.PrecioUnitarioDolares;
                    fur.IdMonedaProveedor = Json.IdMoneda_Proveedor;
                    fur.IdFurFaseAprobacion1 = 2;
                    fur.PrecioTotalMonedaOrigen = Json.PrecioUnitarioMonedaOrigen * Json.Cantidad;
                    fur.PrecioTotalDolares = Json.PrecioUnitarioDolares * Json.Cantidad;
                    fur.PagoMonedaOrigen = Json.PrecioUnitarioMonedaOrigen * Json.Cantidad;
                    fur.PagoDolares = Json.PrecioUnitarioDolares * Json.Cantidad;
                    fur.Cancelado = false;
                    fur.Antiguo = 0;
                    fur.IdMonedaPagoReal = Json.IdMonedaPagoReal;
                    fur.IdMonedaPagoRealizado = Json.IdMoneda_Proveedor;
                    fur.EstadoAprobadoObservado = false;
                    fur.OcupadoSolicitud = false;
                    fur.OcupadoRendicion = false;
                    fur.UsuarioCreacion = Json.UsuarioModificacion;
                    fur.FechaCreacion = DateTime.Now;
                    fur.UsuarioModificacion = Json.UsuarioModificacion;
                    fur.FechaModificacion = DateTime.Now;
                    fur.Estado = true;
                    fur.IdEmpresa = Json.IdEmpresa;
                    fur = this.Add(fur);
                    scope.Complete();
                }
                return fur;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna Furs Por Aprobar
        /// </summary>
        /// <returns>  IEnumerable<FurPorAprobarDTO> </returns>
        public IEnumerable<FurPorAprobarDTO> ObtenerFurPorAprobar(int IdArea, string Codigo, int IdRol, int tipo)
        {
            try
            {
                return _unitOfWork.FurRepository.ObtenerFurPorAprobar(IdArea, Codigo, IdRol, tipo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Aprueba o obeserva el FUR
        /// </summary>
        /// <returns> true,False </returns>
        public bool AprobarObservarFur(int Id, string Usuario, int IdRol, int CheckedIsFurGeneral, bool IsAprobar, string? Observacion)
        {
            try
            {
                var repFur = _unitOfWork.FurRepository;
                // Se verifica si es jefe de Finanzas para la aprobacion Final de Furs,
                int isJefeFinanzas = 0;
                if (IdRol != 19)
                {
                    isJefeFinanzas = 0;
                }
                else
                {
                    if (CheckedIsFurGeneral == 0)
                    {
                        isJefeFinanzas = 0;  // Rol como jefe de Area 
                    }
                    else if (CheckedIsFurGeneral == 1)
                    {
                        isJefeFinanzas = 1; //Rol como Jefe de Finanzas
                    }
                }

                var fur = _mapper.Map<Fur>(repFur.FirstById(Id));
                if (isJefeFinanzas == 0) // Si no es Jefe de Finanzas o solo Es Jefe de Algun Area
                {
                    fur.IdFurFaseAprobacion1 = IsAprobar ? 3 : IsAprobar == false ? 4 : fur.IdFurFaseAprobacion1; //Si el Fur ess Aprobado pasa a AprobadoPor Jefe de Area, de lo Contrario pasa a Observado por Jefe de Area
                    fur.UsuarioAutoriza = IsAprobar ? Usuario : fur.UsuarioAutoriza;
                }
                else
                {
                    if (isJefeFinanzas == 1)
                    {// Si es Jefe de Finanzas
                        //Si el Fur es Aprobado pasa a AprobadoPor Jefe de Area, de lo Contrario pasa a Observado por Jefe de Area
                        fur.IdFurFaseAprobacion1 = IsAprobar ? 5 : IsAprobar == false ? 6 : fur.IdFurFaseAprobacion1;
                        if (IsAprobar)
                        { //Solo se llena este campo cuando el jefe de Finanzas aprueba un fur
                            fur.FechaAprobacionProcesoCulminado = DateTime.Now;
                        }
                        fur.EstadoAprobadoObservado = IsAprobar ? true : false;
                    }
                    if (isJefeFinanzas == 2)
                    { //Proyectados
                        fur.EstadoAprobadoObservado = false;
                        fur.IdFurFaseAprobacion1 = IsAprobar ? 2 : fur.IdFurFaseAprobacion1;
                    }
                }
                fur.Observaciones = Observacion;
                fur.FechaModificacion = DateTime.Now;
                fur.UsuarioModificacion = Usuario;
                this.Update(fur);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Aprueba o obeserva el FUR
        /// </summary>
        /// <returns> true,False </returns>
        public object AprobarObservarFurService(AprobarObservaFurDTO data)
        {

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var id in data.Ids)
                    {
                        var respuesta = this.AprobarObservarFur(id, data.Usuario, data.IdRol, data.CheckedIsFurGeneral, data.IsAprobar, data.Observacion);
                        if (respuesta == false) return new { result = false, tipo = "", error = "Ocurrio un error con el FUR Id :" + id };
                    }
                    scope.Complete();
                }
                if (!data.IsAprobar)
                {
                    return new { result = true, tipo = "Oberservado" };
                }
                return new { result = true, tipo = "Aprobado" };
            }
            catch (Exception e)
            {
                return new { result = false, tipo = "", error = "Ocurrio un error en la Operacion!" };
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Activa el FUR el FUR
        /// </summary>
        /// <returns> true,False </returns>
        public bool ActivarFurNoEjecutado(int id, string usuario)
        {
            try
            {
                var repFur = _unitOfWork.FurRepository;

                Fur fur = new Fur();
                fur = _mapper.Map<Fur>(repFur.FirstById(id));
                fur.IdFurFaseAprobacion1 = 5;
                fur.Observaciones = "Fur Activado por Finanzas, antes en Estado Aprobado No Ejecutado";
                fur.FechaModificacion = DateTime.Now;
                fur.UsuarioModificacion = usuario;

                this.Update(fur);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool AprobarFurProyectado(FurAprobarPoryectadosDTO json)
        {
            try
            {
                var repFurRep = _unitOfWork.FurRepository;
                var repAreaTrabajoRep = _unitOfWork.PersonalAreaTrabajoRepository;
                var repCiudadRep = _unitOfWork.CiudadRepository;
                using (TransactionScope scope = new TransactionScope())
                {
                    int idAreaTrabajo = 0;
                    int idCiudad = 0;
                    string AreaTrabajo = "";
                    string Ciudad = "";
                    AreaTrabajo = repAreaTrabajoRep.FirstById(3).Codigo.Trim();
                    foreach (var idFur in json.ListaIdFur)
                    {
                        idAreaTrabajo = repFurRep.FirstById(idFur).IdPersonalAreaTrabajo.Value;
                        idCiudad = repFurRep.FirstById(idFur).IdCiudad;
                        AreaTrabajo = repAreaTrabajoRep.FirstById(idAreaTrabajo).Codigo.Trim();
                        Ciudad = repCiudadRep.FirstById(idCiudad).Nombre.Trim();
                        this.AprobarFurProyectadoGuardar(idFur, AreaTrabajo, Ciudad, json.Usuario);
                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Griselberto Huaman 
        /// Fecha: 01/06/2021
        /// Version: 1.0
        /// <summary>
        /// Aprueba los furs Proyectados
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        private bool AprobarFurProyectadoGuardar(int idFur, string AreaTrabajo, string Ciudad, string usuario)
        {
            try
            {

                var repFurRep = _unitOfWork.FurRepository;
                Fur fur = new Fur();
                fur = _mapper.Map<Fur>(repFurRep.FirstById(idFur));

                int semana = System.Globalization.CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(fur.FechaLimite.Value, CalendarWeekRule.FirstDay, fur.FechaLimite.Value.DayOfWeek);
                fur.NumeroSemana = (fur.NumeroSemana == null || fur.NumeroSemana == 0) ? semana : fur.NumeroSemana;
                string anio = fur.FechaLimite.Value.Year.ToString();
                string codigo = "";
                if (fur.NumeroSemana > 0 && fur.NumeroSemana < 10)
                {
                    codigo = anio.Substring(2) + "-" + Ciudad.Substring(0, 1) + "-" + AreaTrabajo + "-" + "0" + fur.NumeroSemana + "-";
                }
                else
                {
                    codigo = anio.Substring(2) + "-" + Ciudad.Substring(0, 1) + "-" + AreaTrabajo + "-" + fur.NumeroSemana + "-";
                }
                int correlativo = 0;
                var listCodigos = repFurRep.GetBy(x => x.Estado == true && x.Codigo.Contains(codigo), x => new { x.Codigo }).ToList();
                if (listCodigos != null)
                {
                    foreach (var Codigos in listCodigos)
                    {
                        if (Int32.Parse(Codigos.Codigo.Substring(Codigos.Codigo.LastIndexOf("-") + 1).Trim()) > correlativo)
                        {
                            correlativo = Int32.Parse(Codigos.Codigo.Substring(Codigos.Codigo.LastIndexOf("-") + 1).Trim());
                        }
                    }
                    correlativo++;
                }
                else
                {
                    correlativo = 1;
                }
                fur.Codigo = (fur.Codigo == null || fur.Codigo == "") ? codigo + correlativo : fur.Codigo;
                fur.NumeroFur = fur.NumeroFur == null ? fur.Codigo : fur.NumeroFur;
                fur.IdFurFaseAprobacion1 = 2;//ValorEstatico.IdFurEstadoPorAprobar;
                fur.UsuarioSolicitud = usuario;
                fur.FechaModificacion = DateTime.Now;
                fur.UsuarioModificacion = usuario;

                this.Update(fur);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/03/2023
        /// Version: 1.0
        /// <summary>
        /// elimina logicamente los FURS
        /// </summary>
        /// <returns> bool </returns>
        public bool EliminarLogicamenteFurProyectadoPorHistorico(EliminarFurProyectadoDTO data)
        {
            try
            {
                return _unitOfWork.FurRepository.EliminarLogicamenteFurProyectadoPorHistorico(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman
        /// Fecha: 28/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el nivel de Acceso.
        /// </summary>
        /// <returns> bool </returns>
        public int ObtenerNivelAcceso(string usuario, int idPer)
        {
            try
            {
                return _unitOfWork.FurRepository.ObtenerNivelAcceso(usuario, idPer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<ProgramaEspecificoFURDTO> ObtenerFurProgramaEspecifico(int idPespecifico, bool esDocente)
        {
            return _unitOfWork.FurRepository.ObtenerFurProgramaEspecifico(idPespecifico, esDocente);
        }
    }
}
