using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PlantillaMaestroPwRepository
    /// Autor: Gilmer Qm
    /// Fecha: 23/06/2023
    /// <summary>
    /// Gestión general de T_PlantillaMaestroPw
    /// </summary>
    public class PlantillaMaestroPwRepository : GenericRepository<TPlantillaMaestroPw>, IPlantillaMaestroPwRepository
    {
        private Mapper _mapper;
        public PlantillaMaestroPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPlantillaMaestroPw, PlantillaMaestroPw>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPlantillaMaestroPw MapeoEntidad(PlantillaMaestroPw entidad)
        {
            try
            {
                //crea la entidad padre
                TPlantillaMaestroPw modelo = _mapper.Map<TPlantillaMaestroPw>(entidad);

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

        public TPlantillaMaestroPw Add(PlantillaMaestroPw entidad)
        {
            try
            {
                var PlantillaMaestroPw = MapeoEntidad(entidad);
                base.Insert(PlantillaMaestroPw);
                return PlantillaMaestroPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPlantillaMaestroPw Update(PlantillaMaestroPw entidad)
        {
            try
            {
                var PlantillaMaestroPw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PlantillaMaestroPw.RowVersion = entidadExistente.RowVersion;

                base.Update(PlantillaMaestroPw);
                return PlantillaMaestroPw;
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


        public IEnumerable<TPlantillaMaestroPw> Add(IEnumerable<PlantillaMaestroPw> listadoEntidad)
        {
            try
            {
                List<TPlantillaMaestroPw> listado = new List<TPlantillaMaestroPw>();
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

        public IEnumerable<TPlantillaMaestroPw> Update(IEnumerable<PlantillaMaestroPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPlantillaMaestroPw> listado = new List<TPlantillaMaestroPw>();
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
        /// Autor: Gilmer Qm
        /// Fecha: 23/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el combo.
        /// </summary>
        /// <returns> IEnumerable<PlantillaMaestroPwDTO> </returns>
        public IEnumerable<PlantillaMaestroPwDTO> ObtenerCombo()
        {
            try
            {
                var query = @"SELECT Id,
                                   Nombre,
                                   Repeticion
                            FROM pla.T_PlantillaMaestro_PW
                            WHERE Estado = 1
                            ORDER BY Id DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<PlantillaMaestroPwDTO>>(resultado)!;
                return new List<PlantillaMaestroPwDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}");
            }
        }
    }
}
