using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System.Globalization;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: FurRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 01/07/2022
    /// <summary>
    /// Gestión general de T_Fur
    /// </summary>
    public class FurRepository : GenericRepository<TFur>, IFurRepository
    {
        private Mapper _mapper;

        public FurRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFur, Fur>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TFur MapeoEntidad(Fur entidad)
        {
            try
            {
                //crea la entidad padre
                TFur modelo = _mapper.Map<TFur>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFur Add(Fur entidad)
        {
            try
            {
                var Fur = MapeoEntidad(entidad);
                base.Insert(Fur);
                return Fur;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFur Update(Fur entidad)
        {
            try
            {
                var Fur = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Fur.RowVersion = entidadExistente.RowVersion;

                base.Update(Fur);
                return Fur;
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
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TFur> Add(IEnumerable<Fur> listadoEntidad)
        {
            try
            {
                List<TFur> listado = new List<TFur>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TFur> Update(IEnumerable<Fur> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFur> listado = new List<TFur>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                var respuesta = base.Delete(listadoIds, usuario);
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// Autor: Jonathan Caipo
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla Fur mediante le Id
        /// </summary>
        /// <param name="id"> Id de la sesion del Fur (PK de la tabla fin.T_Fur </param>
        /// <returns>Cadena formateada de la URL de ubicaciond e la ciudad</returns>
        public Fur? ObtenerPorId(int id)
        {
            try
            {
                var query = @"SELECT Id,
                               Codigo,
                               IdPEspecifico AS IdPespecifico,
                               IdPersonalAreaTrabajo,
                               IdCiudad,
                               NumeroFur,
                               NumeroSemana,
                               UsuarioSolicitud,
                               UsuarioAutoriza,
                               Observaciones,
                               IdProveedor,
                               IdProducto,
                               Cantidad,
                               Monto,
                               IdProductoPresentacion,
                               IdCentroCosto,
                               IdMoneda_Proveedor AS IdMonedaProveedor,
                               NumeroCuenta,
                               NumeroRecibo,
                               PagoMonedaOrigen,
                               PagoDolares,
                               FechaCobroBanco,
                               ResponsableCobro,
                               FechaPago,
                               Cuenta,
                               Descripcion,
                               PrecioUnitarioMonedaOrigen,
                               PrecioUnitarioDolares,
                               PrecioTotalMonedaOrigen,
                               PrecioTotalDolares,
                               IdFurFaseAprobacion_1 AS IdFurFaseAprobacion1,
                               AprobadoFase2,
                               FechaLimite,
                               IdFurTipoPedido,
                               Cancelado,
                               Antiguo,
                               IdMoneda_PagoReal AS IdMonedaPagoReal,
                               OcupadoSolicitud,
                               OcupadoRendicion,
                               EstadoAprobadoObservado,
                               Estado,
                               UsuarioCreacion,
                               UsuarioModificacion,
                               FechaCreacion,
                               FechaModificacion,
                               RowVersion,
                               IdMigracion,
                               IdMoneda_PagoRealizado AS IdMonedaPagoRealizado,
                               FechaAprobacionProcesoCulminado,
                               MontoProyectado,
                               EsDiferido,
                               IdFurSubFaseAprobacion,
                               FechaLimiteReprogramacion,
                               IdEmpresa 
                            FROM fin.T_Fur 
                            WHERE Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    return JsonConvert.DeserializeObject<Fur>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId() {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtiene codigo, descripcion y el idmonedaPagoReal de todos los fur que no esten cancelados y sean aprobados por jefe de Finanzas
        /// </summary>
        /// <returns></returns>
        public object ObtenerDatosFur()
        {
            try
            {
                var listaFurs = this.GetBy(x => x.Estado == true && x.Cancelado == false && x.OcupadoSolicitud == false && x.IdFurFaseAprobacion1 == 5 && x.Antiguo == 0,
                    x => new { Id = x.Id, Codigo = x.Codigo.ToUpper(), Detalle = x.Descripcion, IdMonedaPagoReal = x.IdMonedaPagoReal, }).ToList();
                return listaFurs;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene codigo, descripcion y el idmonedaPagoReal de todos los fur que no esten cancelados y sean aprobados por jefe de Finanzas
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public object ObtenerDatosFurAutocomplete(string codigo)
        {
            try
            {
                var listaFurs = this.GetBy(x => x.Estado == true && x.Codigo.Contains(codigo) && x.Cancelado == false && x.OcupadoSolicitud == false && x.IdFurFaseAprobacion1 == 5 && x.Antiguo == 0,
                    x => new { Id = x.Id, Codigo = x.Codigo.ToUpper(), Detalle = x.Descripcion, IdMonedaPagoReal = x.IdMonedaPagoReal, }).ToList();
                return listaFurs;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene codigo, descripcion y el idmonedaPagoReal de todos los fur que no esten cancelados y sean aprobados por jefe de Finanzas
        /// </summary>
        /// <param name="codigo">codigo parcial de FUR</param>
        /// <returns></returns>
        public object ObtenerDatosFurSolicitudEfectivo(string codigo)
        {
            try
            {
                var listaFurs = this.GetBy(x => x.Estado == true && x.Codigo.Contains(codigo) && x.Cancelado == false && x.OcupadoSolicitud == false && x.IdFurFaseAprobacion1 == 5 && x.Antiguo == 0,//ValorEstatico.IdFurAprobadoPorJefeFinanzas
                    x => new { Id = x.Id, Codigo = x.Codigo.ToUpper(), Detalle = x.Descripcion, IdMonedaPagoReal = x.IdMonedaPagoReal, }).ToList();
                return listaFurs;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene codigo, descripcion y el idmonedaPagoReal de todos los fur que no esten cancelados, no sean rendidos 
        /// y sean aprobados por jefe de Finanzas, se usa para autocomplete de generacionCajaRec
        /// </summary>
        /// <returns></returns>
        public object ObtenerFursREC()
        {
            try
            {
                var listaFurs = this.GetBy(x => x.Estado == true && x.Cancelado == false && x.OcupadoRendicion == false && x.OcupadoSolicitud == false && x.IdFurFaseAprobacion1 == 5 && x.Antiguo == 0,
                    x => new { Id = x.Id, Codigo = x.Codigo.ToUpper(), Detalle = x.Descripcion, IdMonedaPagoReal = x.IdMonedaPagoReal, }).ToList();
                return listaFurs;
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
		/// Obtiene la lista de los furs segun los parametros de la vista
		/// </summary>
		/// <param name="json">Json que contiene el area, usuario, ciudad, año, semana y estado</param>
		/// <returns>FurDTO</returns>
        public IEnumerable<FurDTO> ObtenerFursParaGrid(ParametrosFurGrillaDTO json)
        {
            try
            {
                var condiciones = "";
                if (json.anio != null && json.anio != 0)
                {
                    condiciones += " AND Anio = @anio ";
                }
                if (json.semana != null && json.semana != 0)
                {
                    condiciones += " AND NumeroSemana = @semana ";
                }
                if (json.idCiudad != null && json.idCiudad != 0)
                {
                    condiciones += " AND IdCiudad = @idCiudad ";
                }
                var _query = "";
                var camposTabla = @"Id,Codigo,IdCentroCosto,CentroCosto,Programa,IdCiudad,IdFurTipoPedido,NumeroSemana,
                                    IdProveedor,RazonSocial,IdProducto,Producto,IdProductoPresentacion,ProductoPresentacion,IdMoneda_Proveedor,
                                    FechaLimite,Descripcion,NumeroCuenta,Cuenta,Cantidad,IdFaseAprobacion1,FaseAprobacion1,PrecioUnitarioMonedaOrigen,
                                    PrecioUnitarioDolares,PrecioTotalMonedaOrigen,PrecioTotalDolares,UsuarioCreacion,UsuarioModificacion,FechaModificacion,FechaCreacion,
                                    Observaciones,IdMonedaPagoReal,IdPersonalAreaTrabajo,IdCondicionTipoPago, MonedaPagoReal,IdEmpresa";

                List<FurDTO> FurFinanzas = new List<FurDTO>();
                if (json.IdEstadoFaseAprobacion1 != 1)
                {
                    if (json.Codigo != null && !json.Codigo.Equals(""))
                    {
                        if (json.IdEstadoFaseAprobacion1 == 7 || json.IdEstadoFaseAprobacion1 == 8)
                        {
                            if (json.UserName.Equals(""))
                            {
                                _query = "SELECT " + camposTabla +
                                    " FROM FIN.V_ObtenerFurFinanzas where Antiguo='1' and Codigo like CONCAT(@Codigo,'%') ORDER BY Id desc";
                            }
                            else
                            {
                                _query = "SELECT " + camposTabla +
                                    " FROM FIN.V_ObtenerFurFinanzas where Antiguo='1' and UsuarioCreacion=@UserName and Codigo like CONCAT(@Codigo,'%') ORDER BY Id desc";
                            }
                        }
                        else
                        {
                            _query = "SELECT " + camposTabla +
                                " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=@IdEstadoFaseAprobacion1 and IdPersonalAreaTrabajo=@IdArea and Codigo like CONCAT(@Codigo,'%') ORDER BY Id desc ";
                        }
                    }
                    else
                    {
                        if (json.IdEstadoFaseAprobacion1 == 7 || json.IdEstadoFaseAprobacion1 == 8)
                        {
                            if (json.UserName.Equals(""))
                            {
                                _query = "SELECT " + camposTabla +
                                    " FROM FIN.V_ObtenerFurFinanzas where Antiguo='1'ORDER BY Id desc";
                            }
                            else
                            {
                                _query = "SELECT " + camposTabla +
                                    " FROM FIN.V_ObtenerFurFinanzas where Antiguo='1' and UsuarioCreacion=@UserName ORDER BY Id desc";
                            }
                        }
                        else
                        {
                            _query = "SELECT " + camposTabla +
                                " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=@IdEstadoFaseAprobacion1 and IdPersonalAreaTrabajo=@IdArea  ORDER BY Id desc";
                        }
                    }

                }
                else
                {

                    switch (json.modo.ModoFur)
                    {
                        case 1:
                            if (json.modo.FurVencido)//Fur vencidos
                            {
                                _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=@IdEstadoFaseAprobacion1 and IdPersonalAreaTrabajo=@IdArea " + condiciones +
                                " and MONTH(FechaLimite)= month(DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0))) and year(FechaLimite)= year(DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0)))  ORDER BY Id desc";
                            }
                            else// fur vigentes
                            {
                                _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=@IdEstadoFaseAprobacion1 and IdPersonalAreaTrabajo=@IdArea " + condiciones +
                                " and MONTH(FechaLimite)= month(DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) and year(FechaLimite)= year(DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ORDER BY Id desc";

                            }
                            break;
                        case 2:
                            _query = "SELECT " + camposTabla +
                            " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=@IdEstadoFaseAprobacion1 " + condiciones +
                            " and MONTH(FechaLimite)= month(DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) and year(FechaLimite)= year(DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ORDER BY Id desc";
                            break;
                        case 10:
                            _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=@IdEstadoFaseAprobacion1 and IdPersonalAreaTrabajo=@IdArea " + condiciones +
                            " ORDER BY Id desc";
                            break;
                        case 11:
                            _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=@IdEstadoFaseAprobacion1 and IdPersonalAreaTrabajo=@IdArea " + condiciones +
                             " and FechaLimite< dateadd(month,2, DATEADD(MM, DATEDIFF(MM,0,GETDATE()), 0) ) order by convert(date,FechaLimite) desc";
                            break;
                        default:
                            break;
                    }
                    //if (json.modo.ModoFur == 1)
                    //{
                    //    if (json.modo.FurVencido)//Fur vencidos
                    //    {
                    //        _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=@IdEstadoFaseAprobacion1 and IdPersonalAreaTrabajo=@IdArea " + condiciones +
                    //        " and MONTH(FechaLimite)= month(DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0))) and year(FechaLimite)= year(DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0)))  ORDER BY Id desc";
                    //    }
                    //    else// fur vigentes
                    //    {
                    //        _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=@IdEstadoFaseAprobacion1 and IdPersonalAreaTrabajo=@IdArea " + condiciones +
                    //        " and MONTH(FechaLimite)= month(DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) and year(FechaLimite)= year(DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ORDER BY Id desc";

                    //    }

                    //}
                    //else
                    //{
                    //    _query = "SELECT " + camposTabla +
                    //        " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=@IdEstadoFaseAprobacion1 " + condiciones +
                    //        " and MONTH(FechaLimite)= month(DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) and year(FechaLimite)= year(DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))) ORDER BY Id desc";
                    //}
                    //if (json.modo.ModoFur == 10)
                    //{
                    //    _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=@IdEstadoFaseAprobacion1 and IdPersonalAreaTrabajo=@IdArea " + condiciones +
                    //        " ORDER BY Id desc";
                    //}
                    //if (json.modo.ModoFur == 11)
                    //{
                    //    _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=@IdEstadoFaseAprobacion1 and IdPersonalAreaTrabajo=@IdArea " + condiciones +
                    //         " and FechaLimite< dateadd(month,2, DATEADD(MM, DATEDIFF(MM,0,GETDATE()), 0) ) order by convert(date,FechaLimite) desc";
                    //}
                }

                var FurDB = _dapperRepository.QueryDapper(_query, new { json.IdArea, json.Codigo, json.IdRol, json.IdEstadoFaseAprobacion1, json.UserName, json.modo, json.anio, json.semana, json.idCiudad });
                if (!FurDB.Contains("[]") && !string.IsNullOrEmpty(FurDB))
                {
                    FurFinanzas = JsonConvert.DeserializeObject<List<FurDTO>>(FurDB);
                }

                return FurFinanzas;
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
		/// Obtiene la lista de los furs segun el codigo de Fur
		/// </summary>
		/// <param name="codigo">Código de FUR</param>
		/// <returns>FurDTO</returns>
        public IEnumerable<FurDTO> ObtenerFursBusquedaCodigo(string codigo)
        {
            try
            {
                List<FurDTO> ListaFurByCodigo = new List<FurDTO>();
                var camposTabla = "SELECT Id,Codigo,IdCentroCosto,CentroCosto,Programa,IdCiudad,IdFurTipoPedido,NumeroSemana,IdProveedor,RazonSocial,IdProducto,Producto,IdProductoPresentacion,ProductoPresentacion,IdMoneda_Proveedor,FechaLimite,Descripcion,NumeroCuenta,Cuenta,Cantidad,IdFaseAprobacion1,FaseAprobacion1,PrecioUnitarioMonedaOrigen,PrecioUnitarioDolares,PrecioTotalMonedaOrigen,PrecioTotalDolares,UsuarioCreacion,UsuarioModificacion,FechaModificacion,Observaciones,IdMonedaPagoReal,IdPersonalAreaTrabajo,IdCondicionTipoPago,MonedaPagoReal, IdEmpresa,FechaCreacion ";
                var _query = camposTabla + " FROM FIN.V_ObtenerFurFinanzas where Codigo like CONCAT(@Codigo,'%') ORDER BY NumeroSemana,Id";
                var productoFurDB = _dapperRepository.QueryDapper(_query, new { codigo });
                if (!productoFurDB.Contains("[]") && !string.IsNullOrEmpty(productoFurDB))
                {
                    ListaFurByCodigo = JsonConvert.DeserializeObject<List<FurDTO>>(productoFurDB);
                }
                return ListaFurByCodigo;
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
		/// Obtiene la lista de servicios de un porveedor
		/// </summary>
		/// <param name="IdProveedor">Id Proveedor</param>
		/// <returns>FurDTO</returns>
        public IEnumerable<ProductoFurDTO> ObtenerProductoFur(int IdProveedor)
        {
            try
            {
                List<ProductoFurDTO> productoInformacion = new List<ProductoFurDTO>();
                var _query = "SELECT IdProducto,Producto,IdProveedor,CuentaContable,Cuenta,IdCantidad,Cantidad,IdMoneda,CostoOriginal,CostoDolares,PrecioProducto,IdCondicionTipoPago FROM FIN.V_ObtenerProductosFur where IdProveedor=@IdProveedor";
                var productoFurDB = _dapperRepository.QueryDapper(_query, new { IdProveedor });
                if (!productoFurDB.Contains("[]") && !string.IsNullOrEmpty(productoFurDB))
                {
                    productoInformacion = JsonConvert.DeserializeObject<List<ProductoFurDTO>>(productoFurDB);
                }
                return productoInformacion;
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
		/// Obtiene datos para la grila de Aprobar Fur
		/// </summary>
		/// <param name="IdArea">Id area</param>
        /// <param name="Codigo">codigo</param>
        /// <param name="IdRol">Id Rol</param>
        /// <param name="tipo">tipo</param>
		/// <returns>FurDTO</returns>
        public IEnumerable<FurPorAprobarDTO> ObtenerFurPorAprobar(int IdArea, string Codigo, int IdRol, int tipo)
        {
            try
            {
                var _query = "";
                var _whereSQL = "";
                var camposTabla = @" Id,Codigo,CentroCosto,Programa,RazonSocial,Producto ,
                                    IdMoneda_Proveedor,Descripcion,Cantidad,PrecioUnitarioMonedaOrigen,PrecioUnitarioDolares,
                                    PrecioTotalMonedaOrigen,PrecioTotalDolares,Observaciones,IdMonedaPagoReal,IdPersonalAreaTrabajo,
                                    MonedaPagoReal , convert(datetime,FechaLimite) as FechaLimite,FurTipoPedido,UsuarioSolicitud,FechaModificacion,NombreArea,FechaCreacion ";
                if (Codigo.Equals(""))
                {
                    if (tipo == 0)
                    {
                        if (IdRol == 22)
                        {
                            _whereSQL = " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=2 and (IdPersonalAreaTrabajo=@IdArea or IdPersonalAreaTrabajo=4) and AprobadoFase2 is null";
                            _query = "Select " + camposTabla + _whereSQL;
                        }
                        else
                        {
                            _whereSQL = " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=2 and IdPersonalAreaTrabajo=@IdArea and AprobadoFase2 is null";
                            _query = "Select " + camposTabla + _whereSQL;
                        }
                    }
                    else
                    {
                        _whereSQL = " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=3 ";//FURs Aprobados por Jefes De Areas, solo para el JefeDeFinanzas
                        _query = "Select " + camposTabla + _whereSQL;
                    }
                }
                else
                {
                    if (tipo == 0)
                    {
                        if (IdRol == 22)
                        {
                            _whereSQL = " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=2 and Codigo like CONCAT(@Codigo,'%') and (IdPersonalAreaTrabajo=@IdArea or IdPersonalAreaTrabajo=4 ) and AprobadoFase2 is null";
                            _query = "Select " + camposTabla + _whereSQL;
                        }
                        else
                        {
                            _whereSQL = " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=2 and Codigo like CONCAT(@Codigo,'%') and IdPersonalAreaTrabajo=@IdArea and AprobadoFase2 is null";
                            _query = "Select " + camposTabla + _whereSQL;
                        }
                    }
                    else
                    {
                        _whereSQL = " FROM FIN.V_ObtenerFurFinanzas where IdFaseAprobacion1=3  and Codigo like CONCAT(@Codigo,'%')";
                        _query = "Select " + camposTabla + _whereSQL;
                    }
                }
                List<FurPorAprobarDTO> ListaFur = new List<FurPorAprobarDTO>();
                _query += " ORDER BY Id desc ";

                var FurDB = _dapperRepository.QueryDapper(_query, new { IdArea, Codigo, IdRol });

                if (!FurDB.Contains("[]") && !string.IsNullOrEmpty(FurDB))
                {
                    ListaFur = JsonConvert.DeserializeObject<List<FurPorAprobarDTO>>(FurDB);
                }
                return ListaFur;
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
        /// Obtiene Furs que estan en proceso de eliminacion, 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FurAprobadoNoEjecutadoDTO> ObtenerFursNoEjecutados()
        {
            try
            {
                List<FurAprobadoNoEjecutadoDTO> ListaFur = new List<FurAprobadoNoEjecutadoDTO>();
                var camposTabla = @"SELECT Id,Codigo,CentroCosto,Programa,Ciudad,TipoPedido,RazonSocial,
                                    Producto,ProductoPresentacion,FechaLimite,Descripcion,NumeroCuenta,Cuenta, 
                                    Cantidad,FaseAprobacion1,PrecioUnitarioMonedaOrigen,PrecioTotalMonedaOrigen,UsuarioSolicitud,
                                    MonedaPagoReal,FechaAprobacionJefeFinanzas ";

                var _query = camposTabla + " FROM FIN.V_ObtenerFurNoEjecutados where idfaseAprobacion1 = " + 9;
                var FurDB = _dapperRepository.QueryDapper(_query, new { });
                if (!FurDB.Contains("[]") && !string.IsNullOrEmpty(FurDB))
                {
                    ListaFur = JsonConvert.DeserializeObject<List<FurAprobadoNoEjecutadoDTO>>(FurDB);
                }
                return ListaFur;
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
        /// Obtiene la Lista de Fur (para autocomplete) apropiados para el modulo de 'RendicionRequerimientos'
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<FurCajaPRDTO> ObtenerDatosFurcajaEgreso(string codigo, int IdCajaPorRendirCabecera)
        {
            try
            {
                List<FurCajaPRDTO> furs = new List<FurCajaPRDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Codigo, Descripcion FROM [fin].[V_ObtenerFursDisponiblesParaRendirCaja] where Codigo like '%" + codigo + "%' AND Estado=1 AND Cancelado = 0 and OcupadoRendicion = 0 AND IdFurFaseAprobacion_1=" + 5 + " AND Antiguo = 0 AND IdCajaPorRendirCabecera=" + IdCajaPorRendirCabecera;//ValorEstatico.IdFurAprobadoPorJefeFinanzas
                var fursDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(fursDB) && !fursDB.Contains("[]"))
                {
                    furs = JsonConvert.DeserializeObject<List<FurCajaPRDTO>>(fursDB);
                }
                return furs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                var query = @"[fin].[SP_EliminarLogicamenteFur]";

                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    FechaInicio = data.FechaInicio,
                    FechaFin = data.FechaFin,
                    IdProducto = data.IdsProducto,
                    IdProveedor = data.IdsProveedor,
                    Usuario = data.Usuario
                });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null") return true;
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. MamaNI Fabian
        /// Fecha: 29/05/2023
        /// <summary>
		/// Obtiene detalle de fur de cronograma de sesiones
		/// </summary>
		/// <param name="idPespecifico"></param>
		/// <param name="esDocente"></param>
		/// <returns>Data programa especifico FUR</returns>
		public IEnumerable<ProgramaEspecificoFURDTO> ObtenerFurProgramaEspecifico(int idPespecifico, bool esDocente)
        {
            try
            {
                string condicionDocente = esDocente ? "AND NumeroCuenta IN ('6314090','6321001') " : string.Empty;
                string condicionIdPespecifico = " AND IdPespecificoPadre = @IdPespecifico";

                var query = $@"
                    SELECT DISTINCT
	                    Id,
	                    Codigo,
	                    Proveedor,
	                    Producto,
	                    CentroCosto,
	                    Unidades,
	                    Descripcion,
	                    Ciudad,
	                    IdProveedor,
	                    IdProducto,
	                    IdCentroCosto,
	                    IdPersonalAreaTrabajo,
	                    IdCiudad,
	                    IdEmpresa
                    FROM fin.V_ObtenerFurGeneradosPEspecifico
                    WHERE
	                    Estado = 1
	                    AND IdFurFaseAprobacion IN (1, 2) {condicionDocente}";
                var resultado = _dapperRepository.QueryDapper(query + condicionIdPespecifico, new { IdPespecifico = idPespecifico });
                if (resultado == "[]")
                {
                    condicionIdPespecifico = " AND IdPespecifico = @IdPespecifico";
                    resultado = _dapperRepository.QueryDapper(query + condicionIdPespecifico, new { IdPespecifico = idPespecifico });
                }
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ProgramaEspecificoFURDTO>>(resultado)!;
                }
                return new List<ProgramaEspecificoFURDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerFurProgramaEspecifico: {ex.Message}", ex);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 31/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el numero de la semana
        /// </summary>
        /// <param name="fecha"></param>
        /// <returns> int - weekNum </returns>
        public int ObtenerNumeroSemana(DateTime fecha)
        {
            var d = fecha;
            CultureInfo cul = CultureInfo.CurrentCulture;

            int weekNum = cul.Calendar.GetWeekOfYear(
                d,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);
            return weekNum;
        }
        /// Autor: Griselberto Huaman 
        /// Fecha: 01/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el nivel de acceso para el modulo GenerarFUR
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public int ObtenerNivelAcceso(string usuario, int idPer)
        {
            try
            {
                FurNivelAccesoDTO Acceso = new FurNivelAccesoDTO();
                var _query = string.Empty;
                _query = @"Select IdPersonal,NivelAcceso as Nivel From [fin].[T_NivelDeAccesoGenerarFur]  Where Estado=1 and IdPersonal=@idPer And  username=@usuario";
                var resultado = _dapperRepository.FirstOrDefault(_query, new
                {
                    idPer= idPer,
                    usuario= usuario
                });
                var NivelAcceso = 0;
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null")
                {
                    Acceso = JsonConvert.DeserializeObject<FurNivelAccesoDTO>(resultado);
                    NivelAcceso = Acceso.Nivel;
                }
                return NivelAcceso;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
