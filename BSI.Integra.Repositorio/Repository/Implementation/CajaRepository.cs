using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CajaRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 24/06/2022
    /// <summary>
    /// Gestión general de T_Caja
    /// </summary>
    public class CajaRepository : GenericRepository<TCaja>, ICajaRepository
    {
        private Mapper _mapper;

        public CajaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCaja, Caja>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCaja MapeoEntidad(Caja entidad)
        {
            try
            {
                //crea la entidad padre
                TCaja modelo = _mapper.Map<TCaja>(entidad);

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

        public TCaja Add(Caja entidad)
        {
            try
            {
                var Caja = MapeoEntidad(entidad);
                base.Insert(Caja);
                return Caja;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCaja Update(Caja entidad)
        {
            try
            {
                var Caja = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Caja.RowVersion = entidadExistente.RowVersion;

                base.Update(Caja);
                return Caja;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        bool Delete(int id, string usuario)
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


        public IEnumerable<TCaja> Add(IEnumerable<Caja> listadoEntidad)
        {
            try
            {
                List<TCaja> listado = new List<TCaja>();
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

        public IEnumerable<TCaja> Update(IEnumerable<Caja> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCaja> listado = new List<TCaja>();
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
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Caja.
        /// </summary>
        /// <returns> List<CajaDTO> </returns>
        public IEnumerable<CajaDTO> ObtenerCaja()
        {
            try
            {
                List<CajaDTO> rpta = new List<CajaDTO>();
                var query = @"
                    SELECT 
	                    Id,
	                    CodigoCaja,
	                    IdEmpresa,
	                    Empresa,
	                    IdBanco,
	                    Banco,
	                    IdCuenta,
	                    Cuenta,
	                    IdMoneda,
	                    Moneda,
	                    IdPais,
	                    Pais,
	                    IdCiudad,
	                    Ciudad,
	                    IdPersonal,
	                    Personal,
	                    Activo,
                        FechaCreacion,
                        UsuarioCreacion,
                        FechaModificacion
                    FROM [fin].[V_ObtenerCajasFinanzas]
                    ORDER BY Id DESC ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CajaDTO>>(resultado);
                }
                return rpta;
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
        /// Obtiene al Personal responsable de cada Caja.
        /// </summary>
        /// <returns> List<CajaResponsableDTO> </returns>
        public IEnumerable<CajaComboDTO> ObtenerCombo()
        {
            try
            {
                List<CajaComboDTO> rpt = new List<CajaComboDTO>();
                var _query = @"
                        SELECT 
                            Id,
                            CodigoCaja AS Nombre,
                            Personal as PersonalResponsable,
                            IdPersonal as IdPersonalResponsable,
                            IdMoneda,
                            Moneda
                        FROM fin.V_ObtenerCajasFinanzas 
                        where activo=1 order by Id desc";
                var resultado = _dapperRepository.QueryDapper(_query, null);
                if (!resultado.Contains("[]") && !string.IsNullOrEmpty(resultado))
                {
                    rpt = JsonConvert.DeserializeObject<List<CajaComboDTO>>(resultado);
                }
                return rpt;
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
        /// Obtiene la Lista de Responsables de Caja
        /// </summary>
        /// <returns> List<CajaResponsableDTO> </returns>
        public IEnumerable<CajaResponsableComboDTO> ObtenerListaCajaResponsable()
        {
            try
            {
                List<CajaResponsableComboDTO> rpt = new List<CajaResponsableComboDTO>();
                //var _query = "SELECT Id, Nombre FROM [fin].[V_ObtenerResponsablesCajaFinanzas] where Id is not null"; // falta confirmar si se debe verificar el 'Estado' y 'Activo'
                var _query = "SELECT Id, CONCAT (Nombres, ', ', Apellidos) as Nombre FROM [gp].[T_Personal] where estado = 1 Order By Nombres";
                var respuesta = _dapperRepository.QueryDapper(_query, null);
                if (!respuesta.Contains("[]") && !string.IsNullOrEmpty(respuesta))
                {
                    rpt = JsonConvert.DeserializeObject<List<CajaResponsableComboDTO>>(respuesta);
                }
                return rpt;
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
        /// Obtiene el primer registro que conincida con IdCaja,
        /// </summary>
        /// <returns> IdCuentaCorriente </returns>
        /// <param name="IdCaja">identificador de caja</param>
        public int obtenerIdCuentaCorriente(int IdCaja)
        {

            try
            {
                return this.GetBy(x => x.Estado == true && x.Id == IdCaja).Select(x => x.IdCuentaCorriente).FirstOrDefault();
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los resgistro de FIN.V_ObtenerResumenCaja
        /// </summary>
        /// <returns> ResumenCajaDTO </returns>
        public IEnumerable<ResumenCajaDTO> ObtenerResumenCaja()
        {
            try
            {
                List<ResumenCajaDTO> resumenCaja = new List<ResumenCajaDTO>();
                var _query = @"
                    SELECT [IdCaja]
                          ,[CodigoCaja]
                          ,[IdEmpresaAutorizada]
                          ,[EmpresaAutorizada]
                          ,[Direccion]
                          ,[Central]
                          ,[Ruc]
                          ,[IdEntidadFinanciera]
                          ,[EntidadFinanciera]
                          ,[IdCuentaCorriente]
                          ,[CuentaCorriente]
                          ,[IdMoneda]
                          ,[Moneda]
                          ,[IdCiudad]
                          ,[Ciudad]
                          ,[IdPais]
                          ,[Pais]
                          ,[Estado]
                          ,[Activo]
                          ,[PersonalResponsable]
                    FROM FIN.V_ObtenerResumenCaja where Activo=1";
                var cajaFinanzasDB = _dapperRepository.QueryDapper(_query, null);
                if (!cajaFinanzasDB.Contains("[]") && !string.IsNullOrEmpty(cajaFinanzasDB))
                {
                    resumenCaja = JsonConvert.DeserializeObject<List<ResumenCajaDTO>>(cajaFinanzasDB);
                }
                return resumenCaja;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
