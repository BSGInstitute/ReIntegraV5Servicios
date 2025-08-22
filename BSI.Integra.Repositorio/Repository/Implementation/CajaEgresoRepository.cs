using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CajaEgresoRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 01/07/2022
    /// <summary>
    /// Gestión general de T_CajaEgreso
    /// </summary>
    public class CajaEgresoRepository : GenericRepository<TCajaEgreso>, ICajaEgresoRepository
    {
        private Mapper _mapper;

        public CajaEgresoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCajaEgreso, CajaEgreso>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCajaEgreso MapeoEntidad(CajaEgreso entidad)
        {
            try
            {
                //crea la entidad padre
                TCajaEgreso modelo = _mapper.Map<TCajaEgreso>(entidad);

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

        public TCajaEgreso Add(CajaEgreso entidad)
        {
            try
            {
                var CajaEgreso = MapeoEntidad(entidad);
                base.Insert(CajaEgreso);
                return CajaEgreso;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCajaEgreso Update(CajaEgreso entidad)
        {
            try
            {
                var CajaEgreso = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CajaEgreso.RowVersion = entidadExistente.RowVersion;

                base.Update(CajaEgreso);
                return CajaEgreso;
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


        public IEnumerable<TCajaEgreso> Add(IEnumerable<CajaEgreso> listadoEntidad)
        {
            try
            {
                List<TCajaEgreso> listado = new List<TCajaEgreso>();
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

        public IEnumerable<TCajaEgreso> Update(IEnumerable<CajaEgreso> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCajaEgreso> listado = new List<TCajaEgreso>();
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
                base.Delete(listadoIds, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor : Griselberto Huaman. 
        /// Fecha: 20/09/2022
        /// Version: 1.0
        /// <summary>
        /// Devuelve los registros  de Caja Egreso
        /// </summary>
        /// <returns> List<CajaEgresoDTO> </returns>
        public IEnumerable<CajaEgresoDTO> ObtenerCajaEgresoEnviado(FiltroCajaEgresoDTO filtro)
        {
            try
            {
                var _query = "";
                var camposTabla = "Id,IdComprobantePago,IdProveedor,NombreProveedor,RucProveedor,IdSunatDocumento,NombreSunatDocumento,Serie,Numero,IdFur,CodigoFur,Descripcion,IdMoneda,TotalEfectivo,FechaEmision,MontoFur,MontoPendiente,EsCancelado,IdPersonalSolicitante,PersonalSolicitante";

                List<CajaEgresoDTO> listaRegistroEgreso = new List<CajaEgresoDTO>();
                if (filtro.idCaja == null && filtro.idSolicitante == null)
                {
                    _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerCajaEgreso where EsEnviado=1 and IdCajaEgresoAprobado is null and IdPersonalResponsable=@idPersonalResponsable";

                }
                else
                {
                    if (filtro.idSolicitante == null)
                    {
                        _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerCajaEgreso where EsEnviado=1 and IdCajaEgresoAprobado is null and IdCaja=@idCaja";
                    }
                    else
                    {
                        _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerCajaEgreso where EsEnviado=1 and IdCajaEgresoAprobado is null and IdCaja=@idCaja and IdPersonalSolicitante=@idSolicitante";
                    }

                }
                var cajaEgresoDB = _dapperRepository.QueryDapper(_query, new { filtro.idPersonalResponsable, filtro.idCaja, filtro.idSolicitante });
                if (!cajaEgresoDB.Contains("[]") && !string.IsNullOrEmpty(cajaEgresoDB))
                {
                    listaRegistroEgreso = JsonConvert.DeserializeObject<List<CajaEgresoDTO>>(cajaEgresoDB);
                }
                return listaRegistroEgreso;
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
        /// Obtiene todos los nombres de los solicitantes correspondientes al Encargado de caja
        /// </summary>
        /// <returns> List<CajaPorRendirDTO> </returns>
        public IEnumerable<CajaPorRendirCombosDTO> ObtenerCajaPorRendirSolicitanteREC(int idPersonalResponsable)
        {
            try
            {
                var _query = @"
                    SELECT distinct
                        IdPersonalSolicitante as Id ,
                        PersonalSolicitante as Nombre
                   FROM FIN.V_ObtenerCajaEgreso where EsEnviado=1 and IdCajaEgresoAprobado is null and IdPersonalResponsable=@idPersonalResponsable ";
                List<CajaPorRendirCombosDTO> rpt = new List<CajaPorRendirCombosDTO>();

                var respuesta = _dapperRepository.QueryDapper(_query, new { idPersonalResponsable });
                if (!respuesta.Contains("[]") && !string.IsNullOrEmpty(respuesta))
                {
                    rpt = JsonConvert.DeserializeObject<List<CajaPorRendirCombosDTO>>(respuesta);
                }
                return rpt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 16/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos para el llenado de GRilla en ResumenCaja - Tab(REC)
        /// </summary>
        /// <returns> List<CajaEgresoGenerarPdfDTO> </returns>
        public IEnumerable<CajaEgresoGenerarPdfDTO> ObtenerCajaEgresoAprobadoByFecha(DateTime FechaInicial, DateTime FechaFinal, int IdCaja)
        {
            try
            {
                var _query = "";
                var cajaEgresoDB = "";
                var camposTabla = "IdCajaEgresoAprobado,CodigoEgresoCaja,CodigosPorRendir,MontoTotal,FechaGeneracionREC,CodigoFur,Origen,RucProveedor,NombreProveedor, TipoDocumentosSunat,Comprobantes, FechaEmisionRecibo,EntregadoA,Detalle, Observacion,IdMoneda,Moneda  ";

                List<CajaEgresoGenerarPdfDTO> listaEgreso = new List<CajaEgresoGenerarPdfDTO>();

                _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerDatosCajaEgresoPDF WHERE IdCaja=@idCaja and Convert(Date,FechaGeneracionREC)>=@fechaInicial and Convert(Date, FechaGeneracionREC)  <= @fechaFinal Order By CodigoCaja,IdCajaEgresoAprobado Asc";
                cajaEgresoDB = _dapperRepository.QueryDapper(_query, new { fechaInicial = FechaInicial.Date, fechaFinal = FechaFinal.Date, idCaja = IdCaja });

                if (!string.IsNullOrEmpty(cajaEgresoDB) && !cajaEgresoDB.Contains("[]"))
                {
                    listaEgreso = JsonConvert.DeserializeObject<List<CajaEgresoGenerarPdfDTO>>(cajaEgresoDB);
                }

                return listaEgreso;
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
        /// Obtiene datos para Generar los Bytes
        /// </summary>
        /// <returns> List<CajaEgresoGenerarPdfDTO> </returns>
        public IEnumerable<CajaEgresoGenerarPdfDTO> ObtenerDatosCajaEgreso(int[] IdEgresoCaja)
        {
            try
            {
                var _query = "";
                var cajaRecDB = "";
                var camposTabla = "SELECT IdCajaEgresoAprobado,CodigoEgresoCaja,RazonSocial,Direccion,Ruc,Central,NumeroCuenta,FechaGeneracionREC,CodigoFur,EntregadoA,NombreProveedor,RucProveedor,TipoDocumentosSunat,Comprobantes,MontoTotal,Moneda,Detalle,Responsable ";

                List<CajaEgresoGenerarPdfDTO> listaNIC = new List<CajaEgresoGenerarPdfDTO>();
                _query = camposTabla + " FROM FIN.V_ObtenerDatosCajaEgresoPDF where IdCajaEgresoAprobado IN @IdsRec";
                cajaRecDB = _dapperRepository.QueryDapper(_query, new { IdsRec = IdEgresoCaja });

                if (!string.IsNullOrEmpty(cajaRecDB) && !cajaRecDB.Contains("[]"))
                {
                    listaNIC = JsonConvert.DeserializeObject<List<CajaEgresoGenerarPdfDTO>>(cajaRecDB);
                }

                return listaNIC;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene  registros con Estado=1  dado un IdCajaPorRendirCabecera de fin.T_CajaEgreso (para llenado de grilla en RendicionRequerimiento)
        /// </summary>
        /// <returns></returns>
        public List<CajaEgresoDTO> ObtenerRegistrosCajaEgreso(int IdCajaPorRendirCabecera)
        {
            try
            {
                List<CajaEgresoDTO> CajaPorRendirFinanzas = new List<CajaEgresoDTO>();
                var _query = @"SELECT 
                                Id,
                                IdFur,
                                CodigoFur,
                                Descripcion,
                                IdMoneda,
                                NombreMoneda,
                                IdProveedor,
                                NombreProveedor,
                                RucProveedor,
                                IdSunatDocumento,
                                NombreSunatDocumento,
                                Serie,Numero,FechaEmision,
                                TotalEfectivo 
                                FROM  [fin].[V_ObtenerRegistrosCajaEgreso] 
                                where Estado=1 AND EsEnviado=0 And IdCajaPorRendirCabecera=" + IdCajaPorRendirCabecera + "  order by id desc";
                var CajaPorRendirFinanzasDB = _dapperRepository.QueryDapper(_query, null);
                if (!CajaPorRendirFinanzasDB.Contains("[]") && !string.IsNullOrEmpty(CajaPorRendirFinanzasDB))
                {
                    CajaPorRendirFinanzas = JsonConvert.DeserializeObject<List<CajaEgresoDTO>>(CajaPorRendirFinanzasDB);
                }
                return CajaPorRendirFinanzas;
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
            /// Obtiene un (1)  registro con Estado=1  dado un Id de fin.T_CajaEgreso (para llenado de grilla en RendicionRequerimiento)
            /// </summary>
            /// <returns></returns>
            public List<CajaEgresoDTO> ObtenerRegistroCajaEgreso(int Id)
        {
            try
            {
                List<CajaEgresoDTO> CajaEgreso = new List<CajaEgresoDTO>();
                var _query = @"SELECT 
                                Id,
                                IdFur,
                                CodigoFur,
                                Descripcion,
                                IdMoneda,
                                NombreMoneda,
                                IdProveedor,
                                NombreProveedor,
                                RucProveedor,
                                IdSunatDocumento,
                                NombreSunatDocumento,
                                Serie,Numero,
                                FechaEmision,
                                TotalEfectivo 
                                FROM  [fin].[V_ObtenerRegistrosCajaEgreso] 
                                where Estado=1 AND EsEnviado=0 And Id=" + Id;
                var CajaEgresoDB = _dapperRepository.QueryDapper(_query, null);
                if (!CajaEgresoDB.Contains("[]") && !string.IsNullOrEmpty(CajaEgresoDB))
                {
                    CajaEgreso = JsonConvert.DeserializeObject<List<CajaEgresoDTO>>(CajaEgresoDB);
                }
                return CajaEgreso;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
