using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    /// Repositorio: SedeTrabajoRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_SedeTrabajo
    /// </summary>
    public class SedeTrabajoRepository : GenericRepository<TSedeTrabajo>, ISedeTrabajoRepository
    {
        private Mapper _mapper;

        public SedeTrabajoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSedeTrabajo, SedeTrabajo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSedeTrabajo MapeoEntidad(SedeTrabajo entidad)
        {
            try
            {
                //crea la entidad padre
                TSedeTrabajo modelo = _mapper.Map<TSedeTrabajo>(entidad);

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

        public TSedeTrabajo Add(SedeTrabajo entidad)
        {
            try
            {
                var SedeTrabajo = MapeoEntidad(entidad);
                Insert(SedeTrabajo);
                return SedeTrabajo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSedeTrabajo Update(SedeTrabajo entidad)
        {
            try
            {
                var SedeTrabajo = MapeoEntidad(entidad);
                var entidadExistente = FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SedeTrabajo.RowVersion = entidadExistente.RowVersion;

                Update(SedeTrabajo);
                return SedeTrabajo;
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


        public IEnumerable<TSedeTrabajo> Add(IEnumerable<SedeTrabajo> listadoEntidad)
        {
            try
            {
                List<TSedeTrabajo> listado = new List<TSedeTrabajo>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TSedeTrabajo> Update(IEnumerable<SedeTrabajo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSedeTrabajo> listado = new List<TSedeTrabajo>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                Update(listado);
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
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_SedeTrabajo.
        /// </summary>
        /// <returns> List<SedeTrabajoDTO> </returns>
        public IEnumerable<SedeTrabajoComboDTO> ObtenerSedeTrabajoCombo()
        {
            try
            {
                List<SedeTrabajoComboDTO> rpta = new List<SedeTrabajoComboDTO>();
                var query = @"select id,Nombre,IdPais from gp.T_SedeTrabajo where Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SedeTrabajoComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 02/05/20204
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_SedeTrabajo para combo
        /// </summary>
        /// <returns> List<SedeTrabajoDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"SELECT Id, Nombre FROM gp.T_SedeTrabajo WHERE Estado = 1 ORDER BY Nombre";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 10/06/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_SedeTrabajo para combo
        /// </summary>
        /// <returns> List<SedeTrabajoDTO> </returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"SELECT Id, Nombre FROM gp.T_SedeTrabajo WHERE Estado = 1 ORDER BY Nombre";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
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
