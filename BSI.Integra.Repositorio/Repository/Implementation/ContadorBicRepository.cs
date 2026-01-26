using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ContadorBicRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 29/08/2023
    /// <summary>
    /// Gestión general de T_ContadorBic
    /// </summary>
    public class ContadorBicRepository : GenericRepository<TContadorBic>, IContadorBicRepository
    {
        private Mapper _mapper;

        public ContadorBicRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TContadorBic, ContadorBic>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TContadorBic MapeoEntidad(ContadorBic entidad)
        {
            try
            {
                TContadorBic modelo = _mapper.Map<TContadorBic>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private IEnumerable<TContadorBic> MapeoEntidad(IEnumerable<ContadorBic> entidad)
        {
            try
            {
                IEnumerable<TContadorBic> modelo = _mapper.Map<IEnumerable<TContadorBic>>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TContadorBic Add(ContadorBic entidad)
        {
            try
            {
                var agregarEntidad = MapeoEntidad(entidad);
                Insert(agregarEntidad);
                return agregarEntidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TContadorBic Update(ContadorBic entidad)
        {
            try
            {
                var actualizarEntidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                actualizarEntidad.RowVersion = entidadExistente.RowVersion;
                Update(actualizarEntidad);
                return actualizarEntidad;
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
        public IEnumerable<TContadorBic> Add(IEnumerable<ContadorBic> listadoEntidad)
        {
            try
            {
                IEnumerable<TContadorBic> listado = MapeoEntidad(listadoEntidad);
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<TContadorBic> Update(IEnumerable<ContadorBic> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                IEnumerable<TContadorBic> listado = MapeoEntidad(listadoEntidad);
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/08/2023
        /// Version: 1.0        
        /// <summary> 
        /// Obtiene las Fechas de las Actividades que no hubo contacto con el cliente
        /// </summary>
        /// <returns></returns>
        public List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasPeru()
        {
            try
            {
                List<OportunidadesNoEjecutadasDTO> rpta = new List<OportunidadesNoEjecutadasDTO>();
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ObtenerOportunidadesNoEjecutadasPeru", null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OportunidadesNoEjecutadasDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CBR-OONE-001@Error en ObtenerOportunidadesNoEjecutadas(), {ex.Message}");
            }
        }

        /// Autor: Carlos H. Crispin Riquelme
        /// Fecha: 24/01/2026
        /// Version: 1.0        
        /// <summary> 
        /// Obtiene las Fechas de las Actividades que fueron trabajadas con contesta y corta
        /// </summary>
        /// <returns></returns>
        public List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasPeruBic1()
        {
            try
            {
                List<OportunidadesNoEjecutadasDTO> rpta = new List<OportunidadesNoEjecutadasDTO>();
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ObtenerOportunidadesNoEjecutadasPeruBic1", null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OportunidadesNoEjecutadasDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CBR-OONE-001@Error en ObtenerOportunidadesNoEjecutadas(), {ex.Message}");
            }
        }

        /// Autor: Carlos H. Crispin Riquelme
        /// Fecha: 23/10/2024
        /// Version: 1.0        
        /// <summary> 
        /// Obtiene los id oportunidades a cerrar a BIC
        /// </summary>
        /// <returns></returns>
        public List<OportunidadesACerrarBICDTO> ObtenerOportunidadesACerrarManualmente()
        {
            try
            {
                List<OportunidadesACerrarBICDTO> rpta = new List<OportunidadesACerrarBICDTO>();
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ObtenerIdOportunidadesACerrarBIC", null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OportunidadesACerrarBICDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CBR-OONE-001@Error en ObtenerOportunidadesNoEjecutadas(), {ex.Message}");
            }
        }

        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/08/2023
        /// Version: 1.0        
        /// <summary> 
        /// Obtiene las Fechas de las Actividades que no hubo contacto con el cliente
        /// </summary>
        /// <returns></returns>
        public List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasContador()
        {
            try
            {
                List<OportunidadesNoEjecutadasDTO> rpta = new List<OportunidadesNoEjecutadasDTO>();
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ObtenerOportunidadesNoEjecutadasContador", null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OportunidadesNoEjecutadasDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CBR-OONE-001@Error en ObtenerOportunidadesNoEjecutadas(), {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/08/2023
        /// Version: 1.0        
        /// <summary> 
        /// Obtiene las Fechas de las Actividades que no hubo contacto con el cliente
        /// </summary>
        /// <returns></returns>
        public List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasMexico()
        {
            try
            {
                List<OportunidadesNoEjecutadasDTO> rpta = new List<OportunidadesNoEjecutadasDTO>();
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ObtenerOportunidadesNoEjecutadasMexico", null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OportunidadesNoEjecutadasDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CBR-OONE-001@Error en ObtenerOportunidadesNoEjecutadasMexico(), {ex.Message}");
            }
        }
        /// Autor: Carlos H. Crispin Riquelme
        /// Fecha: 25/01/2026
        /// Version: 1.0        
        /// <summary> 
        /// Obtiene las Fechas de las Actividades que no hubo contacto con el cliente
        /// </summary>
        /// <returns></returns>
        public List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasMexicoBic1()
        {
            try
            {
                List<OportunidadesNoEjecutadasDTO> rpta = new List<OportunidadesNoEjecutadasDTO>();
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ObtenerOportunidadesNoEjecutadasMexicoBic1", null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OportunidadesNoEjecutadasDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CBR-OONE-001@Error en ObtenerOportunidadesNoEjecutadasMexico(), {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/08/2023
        /// Version: 1.0        
        /// <summary> 
        /// Obtiene las Fechas de las Actividades que no hubo contacto con el cliente
        /// </summary>
        /// <returns></returns>
        public List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasChile()
        {
            try
            {
                List<OportunidadesNoEjecutadasDTO> rpta = new List<OportunidadesNoEjecutadasDTO>();
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ObtenerOportunidadesNoEjecutadasChile", null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OportunidadesNoEjecutadasDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CBR-OONE-001@Error en ObtenerOportunidadesNoEjecutadasChile(), {ex.Message}");
            }
        }
        /// Autor: Carlos H. Crispin Riquelme
        /// Fecha: 25/01/2026
        /// Version: 1.0        
        /// <summary> 
        /// Obtiene las Fechas de las Actividades que no hubo contacto con el cliente
        /// </summary>
        /// <returns></returns>
        public List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasChileBic1()
        {
            try
            {
                List<OportunidadesNoEjecutadasDTO> rpta = new List<OportunidadesNoEjecutadasDTO>();
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ObtenerOportunidadesNoEjecutadasChileBic1", null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OportunidadesNoEjecutadasDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CBR-OONE-001@Error en ObtenerOportunidadesNoEjecutadasChile(), {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/08/2023
        /// Version: 1.0        
        /// <summary> 
        /// Obtiene las Fechas de las Actividades que no hubo contacto con el cliente
        /// </summary>
        /// <returns></returns>
        public List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasPorIdOportunida(int idOportunidad)
        {
            try
            {
                List<OportunidadesNoEjecutadasDTO> rpta = new List<OportunidadesNoEjecutadasDTO>();
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ObtenerOportunidadesNoEjecutadasPorId", new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OportunidadesNoEjecutadasDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CBR-OONE-001@Error en ObtenerOportunidadesNoEjecutadasPorIdOportunida(), {ex.Message}");
            }
        }
        /// Autor: Carlos H. Crispin Riquelme
        /// Fecha: 19/01/2024
        /// Version: 1.0        
        /// <summary> 
        /// Obtiene las Fechas de las Actividades que no hubo contacto con el cliente desues que se envio whatsapp
        /// </summary>
        /// <returns></returns>
        public List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadasPorIdOportunidadWhatsapp(int idOportunidad)
        {
            try
            {
                List<OportunidadesNoEjecutadasDTO> rpta = new List<OportunidadesNoEjecutadasDTO>();
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ObtenerOportunidadesNoEjecutadasPorIdWhatsapp", new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OportunidadesNoEjecutadasDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CBR-OONE-001@Error en ObtenerOportunidadesNoEjecutadasPorIdOportunida(), {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/08/2023
        /// Version: 1.0        
        /// <summary> 
        /// Obtiene las Fechas de las Actividades que no hubo contacto con el cliente
        /// </summary>
        /// <returns></returns>
        public List<ConfiguracionBICDTO> ObtenerConfiguracionBicAplicada()
        {
            try
            {
                List<ConfiguracionBICDTO> rpta = new List<ConfiguracionBICDTO>();
                var query = @"SELECT Id, Dias, Llamadas, Aplica
                            FROM mkt.T_ConfiguracionBIC
                            WHERE Estado = 1 AND Aplica = 1;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ConfiguracionBICDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CBR-OCBA-001@Error en ObtenerConfiguracionBicAplicada(), {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/11/2023
        /// Version: 1.0        
        /// <summary> 
        /// Obtiene las asesores configurados para los dias bics
        /// </summary>
        /// <returns></returns>
        public List<PersonalBicConfiguracionDTO> ObtenerConfiguracionBicPersonal()
        {
            try
            {
                List<PersonalBicConfiguracionDTO> rpta = new List<PersonalBicConfiguracionDTO>();
                var query = @"SELECT
	                            DiasBic,
	                            IdAsesores,
	                            IdPais
                            FROM com.T_PersonalBicConfiguracion WHERE Estado = 1 ORDER BY DiasBic ASC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PersonalBicConfiguracionDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CBR-OCBA-001@Error en ObtenerConfiguracionBicPersonal(), {ex.Message}");
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 29/08/2023
        /// Version: 1.0        
        /// <summary> 
        /// Obtiene las Fechas de las Actividades que no hubo contacto con el cliente
        /// </summary>
        /// <returns></returns>
        public List<OportunidadContadorBicDTO> ObtenerDatosParaBicPorPaisNuevaLogica(int idPais)
        {
            try
            {
                List<OportunidadContadorBicDTO> rpta = new List<OportunidadContadorBicDTO>();
                var resultado = _dapperRepository.QuerySPDapper("com.SP_ObtenerDatosParaBicPorPais", new { IdPais = idPais });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<OportunidadContadorBicDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CBR-OONE-001@Error en ObtenerDatosParaBicPorPaisNuevaLogica(), {ex.Message}");
            }
        }
        /// Autor: Gilmer
        /// Fecha: 14/08/2025
        /// Version: 1.0        
        /// <summary> 
        /// Obtiene las ultima fecha de calculo contador dias de BIC
        /// </summary>
        /// <returns></returns>
        public bool ObtenerNuevoCalculoDiasBic()
        {
            try
            {
                bool respuesta = false;
                var resultado = _dapperRepository.QuerySPFirstOrDefault("com.SP_ObtenerNuevoCalculoDiasBic", null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<BoolDTO>(resultado).Valor.Value!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw new Exception($"#CBR-OONE-001@Error en ObtenerDatosParaBicPorPaisNuevaLogica(), {ex.Message}");
            }
        }
    }
}
