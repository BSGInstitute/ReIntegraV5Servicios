using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CajaPorRendirRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 13/09/2022
    /// <summary>
    /// Gestión general de T_CajaPorRendir
    /// </summary>
    public class CajaPorRendirRepository : GenericRepository<TCajaPorRendir>, ICajaPorRendirRepository
    {
        private Mapper _mapper;

        public CajaPorRendirRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCajaPorRendir, CajaPorRendir>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TCajaPorRendir MapeoEntidad(CajaPorRendir entidad)
        {
            try
            {
                //crea la entidad padre
                TCajaPorRendir modelo = _mapper.Map<TCajaPorRendir>(entidad);

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

        public TCajaPorRendir Add(CajaPorRendir entidad)
        {
            try
            {
                var CajaPorRendir = MapeoEntidad(entidad);
                base.Insert(CajaPorRendir);
                return CajaPorRendir;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCajaPorRendir Update(CajaPorRendir entidad)
        {
            try
            {
                var CajaPorRendir = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CajaPorRendir.RowVersion = entidadExistente.RowVersion;

                base.Update(CajaPorRendir);
                return CajaPorRendir;
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


        public IEnumerable<TCajaPorRendir> Add(IEnumerable<CajaPorRendir> listadoEntidad)
        {
            try
            {
                List<TCajaPorRendir> listado = new List<TCajaPorRendir>();
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

        public IEnumerable<TCajaPorRendir> Update(IEnumerable<CajaPorRendir> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCajaPorRendir> listado = new List<TCajaPorRendir>();
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




        /// Autor: Griselberto Huaman.
        /// Fecha: 13/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CajaPorRendir
        /// </summary>
        /// <returns> List<CajaPorRendirDTO> </returns>
        public IEnumerable<CajaPorRendirDTO> ObtenerCajaPorRendir(CajaPorRendirFiltroDTO filtro)
        {
            try
            {
                var _query = @"
                            SELECT
                                Id,
                                IdFur,
                                CodigoFur,
                                IdPersonalSolicitante,
                                NombrePersonalSolicitante,
                                Descripcion,
                                IdMoneda,
                                NombreMoneda,
                                TotalEfectivo,
                                FechaEntregaEfectivo
                            FROM FIN.V_ObtenerCajasPorRendirFinanzas 
                            where 
                                IdCaja is null and 
                                EsEnviado=1 and 
                                IdCajaPorRendirCabecera is null and     
                                Estado=1 and 
                                IdPersonalResponsable=@idPersonalResponsable ";

                List<CajaPorRendirDTO> rpta = new List<CajaPorRendirDTO>();
                if (filtro.idMonedaCaja != null && filtro.idMonedaCaja != 0)
                {
                    _query = _query + " and IdMoneda=@idMonedaCaja ";
                }
                if (filtro.idPersonalSolicitante != null && filtro.idPersonalSolicitante != 0)
                {
                    _query = _query + " and IdPersonalSolicitante=@idPersonalSolicitante ";
                }

                var cajaPorRendirDB = _dapperRepository.QueryDapper(_query, new { filtro.idPersonalResponsable, filtro.idMonedaCaja, filtro.idPersonalSolicitante });
                if (!cajaPorRendirDB.Contains("[]") && !string.IsNullOrEmpty(cajaPorRendirDB))
                {
                    rpta = JsonConvert.DeserializeObject<List<CajaPorRendirDTO>>(cajaPorRendirDB);
                }
                return rpta;
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
        /// Obtiene todos los nombres de los solicitantes correspondientes al Encargado de caja
        /// </summary>
        /// <returns> List<CajaPorRendirDTO> </returns>
        public IEnumerable<CajaPorRendirCombosDTO> ObtenerCajaPorRendirSolicitante(int idPersonalResponsable)
        {
            try
            {
                var _query = @"
                SELECT distinct
                    IdPersonalSolicitante as Id ,
                    NombrePersonalSolicitante as Nombre
                FROM FIN.V_ObtenerCajasPorRendirFinanzas
                where IdCaja is null and EsEnviado=1 and IdCajaPorRendirCabecera is null and IdPersonalResponsable=@idPersonalResponsable";

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
        /// Obtiene los montos totales de la Caja
        /// </summary>
        /// <returns> List<MontoCajaDTO> </returns>
        public MontoCajaDTO ObtenerMontoTotalCaja(int IdCaja)
        {
            try
            {
                MontoCajaDTO rpt = new MontoCajaDTO();
                var respuesta = _dapperRepository.QuerySPFirstOrDefault("[fin].[SP_EstadoCaja]", new { IdCaja });
                if (!respuesta.Contains("[]") && !string.IsNullOrEmpty(respuesta))
                {
                    rpt = JsonConvert.DeserializeObject<MontoCajaDTO>(respuesta);
                }
                return rpt;
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
        /// Obtiene el registro de la vista de CajaPorRendirCabecera con filtros por fechas
        /// </summary>
        /// <param name="FechaInicial"></param>
        /// <param name="FechaFinal"></param>
        /// <param name="IdCaja"></param>
        /// <returns> IEnumerable<CajaPorRendirGenerarPdfDTO> </returns>
        public IEnumerable<CajaPorRendirGenerarPdfDTO> ObtenerCajaPorRendirByFecha(DateTime FechaInicial, DateTime FechaFinal, int IdCaja)
        {
            try
            {
                var _query = "";
                var cajaPorRendirDB = "";
                var camposTabla = "IdPorRendirCabecera ,CodigoPorRendir,CodigoFur,EntregadoA,MontoTotal,MontoPendienteRendicion,FechaAprobacion,MontoDevolucion,FechasRendicion,CodigoCajaEgreso,Detalle,Observacion,IdMoneda, Moneda  ";

                List<CajaPorRendirGenerarPdfDTO> listaPorRendir = new List<CajaPorRendirGenerarPdfDTO>();

                _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerDatosCajaPorRendirPDF WHERE IdCaja=@idCaja and Convert(Date,FechaAprobacion)>=@fechaInicial and Convert(Date, FechaAprobacion)  <= @fechaFinal Order By CodigoCaja,IdPorRendirCabecera Asc";
                cajaPorRendirDB = _dapperRepository.QueryDapper(_query, new { fechaInicial = FechaInicial.Date, fechaFinal = FechaFinal.Date, idCaja = IdCaja });

                if (!string.IsNullOrEmpty(cajaPorRendirDB) && !cajaPorRendirDB.Contains("[]"))
                {
                    listaPorRendir = JsonConvert.DeserializeObject<List<CajaPorRendirGenerarPdfDTO>>(cajaPorRendirDB);
                }

                return listaPorRendir;
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
        /// Obtiene datos para la generacion de PDF
        /// </summary>
        /// <param name="IdPorRendirCabecera">Lista de Ids </param>
        /// <returns></returns>
        public IEnumerable<CajaPorRendirGenerarPdfDTO> ObtenerDatosCajaPorRendir(int[] IdPorRendirCabecera)
        {
            try
            {
                var _query = "";
                var cajaRecDB = "";
                var camposTabla = "SELECT  IdPorRendirCabecera,CodigoPorRendir,RazonSocial,Direccion,Ruc,Central,CuentaCaja,FechaAprobacion,CodigoFur,EntregadoA,MontoTotal,Moneda,Detalle,PersonalResponsable";

                List<CajaPorRendirGenerarPdfDTO> listaPorRendir = new List<CajaPorRendirGenerarPdfDTO>();
                _query = camposTabla + " FROM FIN.V_ObtenerDatosCajaPorRendirPDF where IdPorRendirCabecera IN @IdsPorRendir";
                cajaRecDB = _dapperRepository.QueryDapper(_query, new { IdsPorRendir = IdPorRendirCabecera });

                if (!string.IsNullOrEmpty(cajaRecDB) && !cajaRecDB.Contains("[]"))
                {
                    listaPorRendir = JsonConvert.DeserializeObject<List<CajaPorRendirGenerarPdfDTO>>(cajaRecDB);
                }

                return listaPorRendir;
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
        /// Obtiene datos par el detalle por rendir
        /// </summary>
        /// <param name="IdCajaPorRendirCabecera">ID CabeceraPR</param>
        /// <returns></returns>
        public IEnumerable<CajaPorRendirDTO> ObtenerCajasPorRendirPorIdPorRendirCabecera(int IdCajaPorRendirCabecera)
        {
            try
            {
                List<CajaPorRendirDTO> CajaPorRendirFinanzas = new List<CajaPorRendirDTO>();
                var _query = "SELECT Id, CodigoCaja, CodigoFur, NombrePersonalSolicitante,Descripcion,NombreMoneda,TotalEfectivo,FechaEntregaEfectivo FROM  [fin].[V_ObtenerCajasPorRendirFinanzas] where Estado=1 AND IdCajaPorRendirCabecera=" + IdCajaPorRendirCabecera;
                var CajaPorRendirFinanzasDB = _dapperRepository.QueryDapper(_query, null);
                if (!CajaPorRendirFinanzasDB.Contains("[]") && !string.IsNullOrEmpty(CajaPorRendirFinanzasDB))
                {
                    CajaPorRendirFinanzas = JsonConvert.DeserializeObject<List<CajaPorRendirDTO>>(CajaPorRendirFinanzasDB);
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
        /// Obtiene datos para la generacion de PDF
        /// </summary>
        /// <param name="IdUsuario">Idusuario </param>
        /// <returns></returns>
        public IEnumerable<CajaPorRendirCabeceraRendicionDTO> ObtenerCajasPorRendirParaRendicion(int IdUsuario)
        {
            try
            {
                List<CajaPorRendirCabeceraRendicionDTO> respuesta = new List<CajaPorRendirCabeceraRendicionDTO>();

                var respuestaDB = "";

                if (IdUsuario == 213) // 213 Usuario de Juan C. Martinez D.
                    respuestaDB = _dapperRepository.QuerySPDapper("fin.SP_ObtenerRegistrosPorRendirTodoPersonal",null);
                else
                    respuestaDB = _dapperRepository.QuerySPDapper("fin.SP_ObtenerRegistrosPorRendir", new { IdPersonalParam = IdUsuario });

                if (!respuestaDB.Contains("[]") && !string.IsNullOrEmpty(respuestaDB))
                {
                    respuesta = JsonConvert.DeserializeObject<List<CajaPorRendirCabeceraRendicionDTO>>(respuestaDB);
                }
                return respuesta;
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
        /// Obtiene la lista de registros con Estado=1 de un Usuario dado un IdPersonal de fin.T_CajasPorRendir (para llenado de grilla en SolicitudEfectivo)
        /// </summary>
        /// <returns></returns>
        public List<CajaPorRendirDTO> ObtenerCajasPorRendirFinanzas(int IdUsuario)
        {
            try
            {
                List<CajaPorRendirDTO> resultado = new List<CajaPorRendirDTO>();
                var campos = @"
                        Id, IdFur, 
                        CodigoFur,IdPersonalSolicitante,
                        NombrePersonalSolicitante,IdPersonalResponsable,
                        NombrePersonalResponsable,Descripcion,
                        IdMoneda,NombreMoneda,TotalEfectivo,
                        FechaEntregaEfectivo";
                var _query = "";

                if (IdUsuario == 213) // 213 Usuario de Juan C. Martinez D.
                    _query = @"SELECT "+ campos  + " FROM  [fin].[V_ObtenerCajasPorRendirFinanzas] where Estado=1 AND EsEnviado=0 order by id desc ";
                else
                    _query = @"SELECT " + campos + " FROM  [fin].[V_ObtenerCajasPorRendirFinanzas] where Estado=1 AND EsEnviado=0 And IdPersonalSolicitante=" + IdUsuario + " order by id desc";

                var respuesta = _dapperRepository.QueryDapper(_query, null);
                if (!respuesta.Contains("[]") && !string.IsNullOrEmpty(respuesta))
                {
                    resultado = JsonConvert.DeserializeObject<List<CajaPorRendirDTO>>(respuesta);
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        

    }
}
