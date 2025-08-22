using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: NotaIngresoCajaRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 13/09/2022
    /// <summary>
    /// Gestión general de T_NotaIngresoCaja
    /// </summary>
    public class NotaIngresoCajaRepository : GenericRepository<TNotaIngresoCaja>, INotaIngresoCajaRepository
    {
        private Mapper _mapper;

        public NotaIngresoCajaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TNotaIngresoCaja, NotaIngresoCaja>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TNotaIngresoCaja MapeoEntidad(NotaIngresoCaja entidad)
        {
            try
            {
                //crea la entidad padre
                TNotaIngresoCaja modelo = _mapper.Map<TNotaIngresoCaja>(entidad);

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

        public TNotaIngresoCaja Add(NotaIngresoCaja entidad)
        {
            try
            {
                var NotaIngresoCaja = MapeoEntidad(entidad);
                base.Insert(NotaIngresoCaja);
                return NotaIngresoCaja;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TNotaIngresoCaja Update(NotaIngresoCaja entidad)
        {
            try
            {
                var NotaIngresoCaja = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                NotaIngresoCaja.RowVersion = entidadExistente.RowVersion;

                base.Update(NotaIngresoCaja);
                return NotaIngresoCaja;
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


        public IEnumerable<TNotaIngresoCaja> Add(IEnumerable<NotaIngresoCaja> listadoEntidad)
        {
            try
            {
                List<TNotaIngresoCaja> listado = new List<TNotaIngresoCaja>();
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

        public IEnumerable<TNotaIngresoCaja> Update(IEnumerable<NotaIngresoCaja> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TNotaIngresoCaja> listado = new List<TNotaIngresoCaja>();
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
        /// Obtiene registros de T_NotaIngresoCaja para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<NotaIngresoCajaComboDTO> ObtenerCombo()
        {
            try
            {
                List<NotaIngresoCajaComboDTO> rpta = new List<NotaIngresoCajaComboDTO>();

                var query = "SELECT Id, CodigoNic FROM fin.T_NotaIngresoCaja WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<NotaIngresoCajaComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 13/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_NotaIngresoCaja
        /// </summary>
        /// <returns> List<NotaIngresoCajaDTO> </returns>
        public IEnumerable<NotaIngresoCajaDTO> ObtenerNotaIngresoCaja(int id)
        {
            try
            {
                var query = "";
                var camposTabla = "Id,CodigoNic,IdCaja,CodigoCaja,ResponsableCaja,IdOrigenIngresoCaja,OrigenIngresoCaja,IdPersonalEmitido,PersonalEmitido,Monto,FechaGiro,Concepto,FechaCobro";

                List<NotaIngresoCajaDTO> rpta = new List<NotaIngresoCajaDTO>();
                if (id == 0)
                {
                    query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerNotaIngresoCaja order by CodigoCaja,Id Asc";
                }
                else
                {
                    query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerNotaIngresoCaja where IdCaja=@idCaja order by CodigoCaja,Id asc ";
                }
                var resultado = _dapperRepository.QueryDapper(query, new { idCaja = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<NotaIngresoCajaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 13/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_NotaIngresoCaja netre las Fechas
        /// </summary>
        /// <returns> List<NotaIngresoCajaDTO> </returns>
        public IEnumerable<NotaIngresoCajaDTO> ObtenerCajaIngresoByFecha(DateTime FechaInicial, DateTime FechaFinal, int IdCaja)
        {
            try
            {
                var _query = "";
                var cajaNicDB = "";
                var camposTabla = "Id,CodigoNic,IdCaja,CodigoCaja,ResponsableCaja,IdOrigenIngresoCaja,OrigenIngresoCaja,IdPersonalEmitido,PersonalEmitido,Monto,FechaGiro,Concepto,FechaCobro,Moneda,IdMoneda  ";

                List<NotaIngresoCajaDTO> listaNIC = new List<NotaIngresoCajaDTO>();

                _query = "SELECT " + camposTabla + " FROM FIN.V_ObtenerNotaIngresoCaja WHERE IdCaja=@idCaja and Convert(Date,FechaGiro)>=@fechaInicial and Convert(Date, FechaGiro)  <= @fechaFinal Order By  CodigoCaja,Id Asc";
                cajaNicDB = _dapperRepository.QueryDapper(_query, new { fechaInicial = FechaInicial.Date, fechaFinal = FechaFinal.Date, idCaja = IdCaja });


                if (!string.IsNullOrEmpty(cajaNicDB) && !cajaNicDB.Contains("[]"))
                {
                    listaNIC = JsonConvert.DeserializeObject<List<NotaIngresoCajaDTO>>(cajaNicDB);
                }

                return listaNIC;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// Autor: Griselberto Huaman.
        /// Fecha: 13/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_NotaIngresoCaja netre las Fechas
        /// </summary>
        /// <returns> List<NotaIngresoCajaDatosPdfDTO> </returns>
        public IEnumerable<NotaIngresoCajaDatosPdfDTO> ObtenerDatosCajaIngreso(int[] IdIngresoCaja)
        {
            try
            {
                var _query = "";
                var cajaNicDB = "";
                var camposTabla = "SELECT IdNotaIngresoCaja,CodigoNic,CodigoCaja,RazonSocial,Direccion,Ruc,Central,CuentaCaja,Origen,FechaGiro,PersonalEmitido,Concepto,Monto,Moneda,PersonalResponsable,DniResponsable ";

                List<NotaIngresoCajaDatosPdfDTO> listaNIC = new List<NotaIngresoCajaDatosPdfDTO>();
                _query = camposTabla + " FROM FIN.V_ObtenerDatosCajaIngresoPDF where IdNotaIngresoCaja IN @IdsNic";
                cajaNicDB = _dapperRepository.QueryDapper(_query, new { IdsNic = IdIngresoCaja });

                if (!string.IsNullOrEmpty(cajaNicDB) && !cajaNicDB.Contains("[]"))
                {
                    listaNIC = JsonConvert.DeserializeObject<List<NotaIngresoCajaDatosPdfDTO>>(cajaNicDB);
                }

                return listaNIC;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
