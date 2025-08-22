using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CuentaCorrienteRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_CuentaCorriente
    /// </summary>
    public class CuentaCorrienteRepository : GenericRepository<TCuentaCorriente>, ICuentaCorrienteRepository
    {
        private Mapper _mapper;

        public CuentaCorrienteRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCuentaCorriente, CuentaCorriente>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCuentaCorriente MapeoEntidad(CuentaCorriente entidad)
        {
            try
            {
                //crea la entidad padre
                TCuentaCorriente modelo = _mapper.Map<TCuentaCorriente>(entidad);

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

        public TCuentaCorriente Add(CuentaCorriente entidad)
        {
            try
            {
                var CuentaCorriente = MapeoEntidad(entidad);
                base.Insert(CuentaCorriente);
                return CuentaCorriente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCuentaCorriente Update(CuentaCorriente entidad)
        {
            try
            {
                var CuentaCorriente = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CuentaCorriente.RowVersion = entidadExistente.RowVersion;

                base.Update(CuentaCorriente);
                return CuentaCorriente;
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


        public IEnumerable<TCuentaCorriente> Add(IEnumerable<CuentaCorriente> listadoEntidad)
        {
            try
            {
                List<TCuentaCorriente> listado = new List<TCuentaCorriente>();
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

        public IEnumerable<TCuentaCorriente> Update(IEnumerable<CuentaCorriente> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCuentaCorriente> listado = new List<TCuentaCorriente>();
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
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CuentaCorriente.
        /// </summary>
        /// <returns> List<CuentaCorrientesDTO> </returns>
        public IEnumerable<CuentaCorrientesDTO> ObtenerCuentaCorriente()
        {
            try
            {
                List<CuentaCorrientesDTO> rpta = new List<CuentaCorrientesDTO>();
                var query = @"
                    SELECT 
                        IdCta, 
                        NumeroCuenta, 
                        IdCiudad,Ciudad, 
                        NombreEntidadFinanciera 
                    FROM FIN.V_ObtenerCuentasCorrientes 
                    where EstadoCiudad = 1 and EstadoCuentaCorriente = 1 and EstadoEntidadFinanciera = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CuentaCorrientesDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de V_ObtenerCuentasBancarias.
        /// </summary>
        /// <returns> List<CuentaBancariaDTO> </returns>
        public IEnumerable<CuentaBancariaDTO> ObtenerCuentaBancaria()
        {
            try
            {
                List<CuentaBancariaDTO> rpta = new List<CuentaBancariaDTO>();
                var query = @"
                    SELECT 
                        Id,
                        NumeroCuenta,
                        Moneda,
                        IdMoneda,
                        Ciudad,
                        IdCiudad,
                        NombreBanco,
                        IdBanco,
                        UsuarioModificacion,
                        UsuarioCreacion,
                        FechaModificacion,
                        FechaCreacion,
                    EstadoCuenta FROM FIN.V_ObtenerCuentasBancarias 
                    where EstadoMoneda = 1 and EstadoCiudad = 1 and EstadoEntidadFinanciera = 1 and EstadoCuenta = 1 
                    order by NombreBanco";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CuentaBancariaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de V_ObtenerCuentaEntidadCiudad.
        /// </summary>
        /// <returns> List<CuentaCorrienteEntidadCiudadDTO> </returns>
        public IEnumerable<CuentaCorrienteEntidadCiudadDTO> ObtenerCuentaCorrienteConEntidad()
        {
            try
            {
                List<CuentaCorrienteEntidadCiudadDTO> rpta = new List<CuentaCorrienteEntidadCiudadDTO>();
                string _query = string.Empty;
                _query = "SELECT Id,NumeroCuentaCiudad,EntidadNumeroCuenta FROM fin.V_ObtenerCuentaEntidadCiudad WHERE Estado=1";
                var respuesta = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CuentaCorrienteEntidadCiudadDTO>>(respuesta);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_CuentaCorriente para mostrarse en combo.
        /// </summary>
        /// <returns> List<CuentaCorrienteComboDTO> </returns>
        public IEnumerable<CuentaCorrienteComboDTO> ObtenerCombo()
        {
            try
            {
                List<CuentaCorrienteComboDTO> rpta = new List<CuentaCorrienteComboDTO>();
                var query = @"SELECT Id,NumeroCuenta,IdMoneda,IdBanco FROM fin.T_CuentaCorriente WHERE Estado = 1 ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CuentaCorrienteComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna Solo una Cuenta Corriente Correspondiente al Id.
        /// </summary>
        /// <returns> List<CuentaCorrienteComboDTO> </returns>
        /// <param name="Id"> Corresponde al Id de busqueda apra cuenta corriente </param>
        public string ObtenerCuentaCorrienteById(int Id)
        {
            try
            {
                return this.GetBy(x => x.Estado == true && x.Id == Id).Select(x => x.NumeroCuenta).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Margiory Ramirez.
        /// Fecha: 17/01/2023
        /// Version: 1.0
        /// <summary>
        /// Retorna lista de cuenta corriente.
        /// </summary>
        /// <returns> List<CuentaCorrienteComboDTO> </returns>
        /// <param name="Id"> Corresponde al Id de busqueda apra cuenta corriente </param>
        public List<CuentasCorrienteDTO> ObtenerCuentasCorrientes()
        {
            try
            {
                List<CuentasCorrienteDTO> cuentaCorriente = new List<CuentasCorrienteDTO>();
                var _query = "SELECT IdCta, NumeroCuenta, IdCiudad,Ciudad, NombreEntidadFinanciera FROM FIN.V_ObtenerCuentasCorrientes where EstadoCiudad = 1 and EstadoCuentaCorriente = 1 and EstadoEntidadFinanciera = 1";
                var cuentaCorrienteDB =_dapperRepository.QueryDapper(_query, null);
                if (!cuentaCorrienteDB.Contains("[]") && !string.IsNullOrEmpty(cuentaCorrienteDB))
                {
                    cuentaCorriente = JsonConvert.DeserializeObject<List<CuentasCorrienteDTO>>(cuentaCorrienteDB);
                }
                return cuentaCorriente;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }





    }

}
