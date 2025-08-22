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
    /// Repositorio: ConjuntoAnuncioRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_ConjuntoAnuncio
    /// </summary>
    public class RegionCiudadRepository : GenericRepository<TRegionCiudad>, IRegionCiudadRepository
    {
        private Mapper _mapper;


        public RegionCiudadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRegionCiudad, RegionCiudad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public IEnumerable<TRegionCiudad> Add(IEnumerable<RegionCiudad> listadoEntidad)
        {
            try
            {
                List<TRegionCiudad> listado = new List<TRegionCiudad>();
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

        public IEnumerable<TRegionCiudad> Update(IEnumerable<RegionCiudad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TRegionCiudad> listado = new List<TRegionCiudad>();
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


        #region Metodos Base
        private TRegionCiudad MapeoEntidad(RegionCiudad entidad)
        {
            try
            {
                //crea la entidad padre
                TRegionCiudad modelo = _mapper.Map<TRegionCiudad>(entidad);

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

        public TRegionCiudad Add(RegionCiudad entidad)
        {
            try
            {
                var RegionCiudad = MapeoEntidad(entidad);
                base.Insert(RegionCiudad);
                return RegionCiudad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRegionCiudad Update(RegionCiudad entidad)
        {
            try
            {
                var RegionCiudad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                RegionCiudad.RowVersion = entidadExistente.RowVersion;

                base.Update(RegionCiudad);
                return RegionCiudad;
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
        /// Fecha: 18/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ConjuntoAnuncio para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCiudadBs()
        {
            try
            {
                var query = "SELECT Id, Nombre FROM conf .V_TRegionCiudad_ObtenerCiudadBs WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCiudadBs(): {ex.Message}", ex);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ConjuntoAnuncio para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerCiudadBsAsync()
        {
            try
            {
                var query = "SELECT Id, Nombre FROM conf .V_TRegionCiudad_ObtenerCiudadBs WHERE Estado=1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCiudadBsAsync(): {ex.Message}", ex);
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ConjuntoAnuncio para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboRegionCiudadDTO> ObtenerComboCiudad()
        {
            try
            {
                List<ComboRegionCiudadDTO> rpta = new List<ComboRegionCiudadDTO>();

                var query = "SELECT Id, Nombre, IdCiudad FROM conf.T_RegionCiudad WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboRegionCiudadDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ConjuntoAnuncio
        /// </summary>
        /// <returns> List<ConjuntoAnuncioDTO> </returns>
        public IEnumerable<RegionCiudadPanelDTO> ObtenerRegionCiudad()
        {
            try
            {
                List<RegionCiudadPanelDTO> rpta = new List<RegionCiudadPanelDTO>();
                var query = @"select Id,Nombre,CodigoBS,DenominacionBS,NombreCorto from conf.T_RegionCiudad where estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<RegionCiudadPanelDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public IEnumerable<RegionCiudadPanelDTO2> filtroPaisCiudad(int idPais, int idCiudad)
        {
            try
            {
                List<RegionCiudadPanelDTO2> rpta = new List<RegionCiudadPanelDTO2>();
                var query = @"select idPais, nombrePais, idCiudad, nombreCiudad , nombreRegion, CodigoBS, DenominacionBS, NombreCorto from mkt.V_RegionCiudad where idPais =" + idPais + "and idCiudad=" + idCiudad;
                var resultado = _dapperRepository.QueryDapper(query, null);


                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<RegionCiudadPanelDTO2>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Tipo Función: GET
        /// Autor: GIlmer Quispe
        /// Fecha: 07/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de Region ciudad filtrado por el Estado=1
        /// </summary> 
        /// <returns> List<RegionCiudadComboDTO> </returns>
        public IEnumerable<RegionCiudadComboDTO> ObtenerPorEstado()
        {
            try
            {
                var rpta = new List<RegionCiudadComboDTO>();
                var query = @"SELECT Id,Nombre,IdCiudad FROM conf.T_RegionCiudad WHERE Estado =1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<RegionCiudadComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Tipo Función: GET
        /// Autor: Christian Alex Quispe Mamani
        /// Fecha: 07/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de Region ciudad filtrado por el Estado=1
        /// </summary> 
        /// <returns> List<RegionCiudadComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCiudadBsCombo()
        {
            try
            {
                IEnumerable<ComboDTO>? rpta = new List<ComboDTO>();
                var query = @"SELECT DISTINCT RC.Id AS Id, RC.Nombre AS Nombre FROM conf.T_RegionCiudad AS RC INNER JOIN pla.T_Locacion AS LO ON LO.IdRegion = RC.Id AND LO.ESTADO = 1 WHERE RC.ESTADO = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
