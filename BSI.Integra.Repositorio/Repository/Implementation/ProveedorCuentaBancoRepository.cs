



using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ProveedorCuentaBancoRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 07/07/2022
    /// <summary>
    /// Gestión general de T_ProveedorCuentaBanco
    /// </summary>
    public class ProveedorCuentaBancoRepository : GenericRepository<TProveedorCuentaBanco>, IProveedorCuentaBancoRepository
    {
        private Mapper _mapper;

        public ProveedorCuentaBancoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProveedorCuentaBanco, ProveedorCuentaBanco>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TProveedorCuentaBanco MapeoEntidad(ProveedorCuentaBanco entidad)
        {
            try
            {
                //crea la entidad padre
                TProveedorCuentaBanco modelo = _mapper.Map<TProveedorCuentaBanco>(entidad);

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

        public TProveedorCuentaBanco Add(ProveedorCuentaBanco entidad)
        {
            try
            {
                var ProveedorCuentaBanco = MapeoEntidad(entidad);
                base.Insert(ProveedorCuentaBanco);
                return ProveedorCuentaBanco;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProveedorCuentaBanco Update(ProveedorCuentaBanco entidad)
        {
            try
            {
                var ProveedorCuentaBanco = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProveedorCuentaBanco.RowVersion = entidadExistente.RowVersion;

                base.Update(ProveedorCuentaBanco);
                return ProveedorCuentaBanco;
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


        public IEnumerable<TProveedorCuentaBanco> Add(IEnumerable<ProveedorCuentaBanco> listadoEntidad)
        {
            try
            {
                List<TProveedorCuentaBanco> listado = new List<TProveedorCuentaBanco>();
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

        public IEnumerable<TProveedorCuentaBanco> Update(IEnumerable<ProveedorCuentaBanco> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProveedorCuentaBanco> listado = new List<TProveedorCuentaBanco>();
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
        /// Fecha: 07/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene datos para llenar grilla
        /// </summary>
        /// /// <param name="IdProveedor">Identificador del Proveedor</param>
        /// <returns> List<ProveedorCuentaBancoDTO> </returns>
        public IEnumerable<ProveedorCuentaBancoDTO> ObtenerCuentasProveedorById(int IdProveedor)
        {
            try
            {
                var camposTabla = "SELECT Id,IdProveedor,IdTipoCuentaBanco,TipoCuenta,IdEntidadFinanciera,NombreBanco,IdMoneda,Moneda,NroCuenta,CuentaInterbancaria ";
                List<ProveedorCuentaBancoDTO> ProveedorCuenta = new List<ProveedorCuentaBancoDTO>();
                var _query = camposTabla + " FROM  [fin].[V_ObtenerProveedorCuentaBanco] where Estado=1 and IdProveedor = " + IdProveedor;
                var ProveedorDB = _dapperRepository.QueryDapper(_query, null);
                if (!ProveedorDB.Contains("[]") && !string.IsNullOrEmpty(ProveedorDB))
                {
                    ProveedorCuenta = JsonConvert.DeserializeObject<List<ProveedorCuentaBancoDTO>>(ProveedorDB);
                }
                return ProveedorCuenta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}