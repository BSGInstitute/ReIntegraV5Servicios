using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: EntidadFinancieraRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_EntidadFinanciera
    /// </summary>
    public class EntidadFinancieraRepository : GenericRepository<TEntidadFinanciera>, IEntidadFinancieraRepository
    {
        private Mapper _mapper;

        public EntidadFinancieraRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEntidadFinanciera, EntidadFinanciera>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TEntidadFinanciera MapeoEntidad(EntidadFinanciera entidad)
        {
            try
            {
                //crea la entidad padre
                TEntidadFinanciera modelo = _mapper.Map<TEntidadFinanciera>(entidad);

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

        public TEntidadFinanciera Add(EntidadFinanciera entidad)
        {
            try
            {
                var EntidadFinanciera = MapeoEntidad(entidad);
                base.Insert(EntidadFinanciera);
                return EntidadFinanciera;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntidadFinanciera Update(EntidadFinanciera entidad)
        {
            try
            {
                var EntidadFinanciera = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                EntidadFinanciera.RowVersion = entidadExistente.RowVersion;

                base.Update(EntidadFinanciera);
                return EntidadFinanciera;
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


        public IEnumerable<TEntidadFinanciera> Add(IEnumerable<EntidadFinanciera> listadoEntidad)
        {
            try
            {
                List<TEntidadFinanciera> listado = new List<TEntidadFinanciera>();
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

        public IEnumerable<TEntidadFinanciera> Update(IEnumerable<EntidadFinanciera> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEntidadFinanciera> listado = new List<TEntidadFinanciera>();
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
        /// Obtiene todos los registros de T_EntidadFinanciera.
        /// </summary>
        /// <returns> List<EntidadFinancieraDTO> </returns>
        public IEnumerable<EntidadFinancieraDTO> ObtenerEntidadFinanciera()
        {
            try
            {
                List<EntidadFinancieraDTO> rpta = new List<EntidadFinancieraDTO>();
                var query = @"
                    SELECT 
                        Id,
                        Nombre,
                        Descripcion,
                        IdMoneda,
                        Moneda,
                        UsuarioModificacion,
                        FechaModificacion,
                        UsuarioCreacion,
                        FechaCreacion,
                        Estado 
                    FROM FIN.V_EntidadFinanciera 
                    where Estado = 1 
                    order by Nombre";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EntidadFinancieraDTO>>(resultado);
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
        /// Obtiene registros de T_EntidadFinanciera para mostrarse en combo.
        /// </summary>
        /// <returns> List<EntidadFinancieraComboDTO> </returns>
        public IEnumerable<EntidadFinancieraComboDTO> ObtenerCombo()
        {
            try
            {
                List<EntidadFinancieraComboDTO> rpta = new List<EntidadFinancieraComboDTO>();
                var query = @"SELECT Id,Nombre FROM fin.T_EntidadFinanciera WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EntidadFinancieraComboDTO>>(resultado);
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
        /// Obtiene registros de fin.V_ObtenerEntidadFinancieraConPrestamo para mostrarse en combo.
        /// </summary>
        /// <returns> List<EntidadFinancieraComboDTO> </returns>
        public List<EntidadFinancieraComboDTO> ObtenerListaEntidadFinancieraPrestamo()
        {
            try
            {
                List<EntidadFinancieraComboDTO> EntidadesFinancieras = new List<EntidadFinancieraComboDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM fin.V_ObtenerEntidadFinancieraConPrestamo";
                var respuesta = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    EntidadesFinancieras = JsonConvert.DeserializeObject<List<EntidadFinancieraComboDTO>>(respuesta);
                }
                return EntidadesFinancieras;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<EntidadFinancieraDTO> ObtenerEntidadesFinancieras()
        {
            try
            {
                List<EntidadFinancieraDTO> rpta = new List<EntidadFinancieraDTO>();
                var query = @"
                   SELECT Id,Nombre,Descripcion,IdMoneda,Moneda,UsuarioModificacion,FechaModificacion,Estado FROM FIN.V_EntidadFinanciera where Estado = 1 order by Nombre";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EntidadFinancieraDTO>>(resultado);

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
