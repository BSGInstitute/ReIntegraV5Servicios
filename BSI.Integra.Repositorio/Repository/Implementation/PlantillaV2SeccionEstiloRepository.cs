using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PlantillaV2SeccionEstiloRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PlantillaV2SeccionEstilo
    /// </summary>
    public class PlantillaV2SeccionEstiloRepository : GenericRepository<TPlantillaV2seccionEstilo>, IPlantillaV2SeccionEstiloRepository
    {
        private Mapper _mapper;

        public PlantillaV2SeccionEstiloRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPlantillaV2seccionEstilo, PlantillaV2SeccionEstilo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }




        #region Metodos Base
        private TPlantillaV2seccionEstilo MapeoEntidad(PlantillaV2SeccionEstilo entidad)
        {
            try
            {
                //crea la entidad padre
                TPlantillaV2seccionEstilo modelo = _mapper.Map<TPlantillaV2seccionEstilo>(entidad);

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

        public TPlantillaV2seccionEstilo Add(PlantillaV2SeccionEstilo entidad)
        {
            try
            {
                var PlantillaV2SeccionEstilo = MapeoEntidad(entidad);
                base.Insert(PlantillaV2SeccionEstilo);
                return PlantillaV2SeccionEstilo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPlantillaV2seccionEstilo Update(PlantillaV2SeccionEstilo entidad)
        {
            try
            {
                var PlantillaV2SeccionEstilo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PlantillaV2SeccionEstilo.RowVersion = entidadExistente.RowVersion;

                base.Update(PlantillaV2SeccionEstilo);
                return PlantillaV2SeccionEstilo;
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


        public IEnumerable<TPlantillaV2seccionEstilo> Add(IEnumerable<PlantillaV2SeccionEstilo> listadoEntidad)
        {
            try
            {
                List<TPlantillaV2seccionEstilo> listado = new List<TPlantillaV2seccionEstilo>();
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

        public IEnumerable<TPlantillaV2seccionEstilo> Update(IEnumerable<PlantillaV2SeccionEstilo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPlantillaV2seccionEstilo> listado = new List<TPlantillaV2seccionEstilo>();
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
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PlantillaV2SeccionEstilo para mostrarse en combo.
        /// </summary>
        /// <returns> List<PlantillaV2SeccionEstiloComboDTO> </returns>
        public IEnumerable<PlantillaV2SeccionEstiloCombo> ObtenerCombo()

        {
            try
            {
                List<PlantillaV2SeccionEstiloCombo> rpta = new List<PlantillaV2SeccionEstiloCombo>();

                var query = "SELECT Id, Nombre FROM mkt.T_PlantillaV2SeccionEstilo WHERE Estado=1";

                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PlantillaV2SeccionEstiloCombo>>(resultado);
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
        /// Obtiene todos los registros de T_PlantillaV2SeccionEstilo
        /// </summary>
        /// <returns> List<PlantillaV2SeccionEstilo> </returns>
        public IEnumerable<PlantillaV2SeccionEstilo> ObtenerPlantillaV2SeccionEstilo()
        {
            try
            {
                List<PlantillaV2SeccionEstilo> rpta = new List<PlantillaV2SeccionEstilo>();
                var query = @"SELECT Id, IdPlanitillav2Seccion, IdEstilo, Valor FROM mkt.T_PlantillaV2SeccionEstilo WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PlantillaV2SeccionEstilo>>(resultado);
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
