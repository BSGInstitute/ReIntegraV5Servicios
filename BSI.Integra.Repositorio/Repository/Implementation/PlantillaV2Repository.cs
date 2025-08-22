using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PlantillaV2Repository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PlantillaV2
    /// </summary>
    public class PlantillaV2Repository : GenericRepository<TPlantillaV2>, IPlantillaV2Repository
    {
        private Mapper _mapper;

        public PlantillaV2Repository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPlantillaV2, PlantillaV2>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }




        #region Metodos Base
        private TPlantillaV2 MapeoEntidad(PlantillaV2 entidad)
        {
            try
            {
                //crea la entidad padre
                TPlantillaV2 modelo = _mapper.Map<TPlantillaV2>(entidad);

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

        public TPlantillaV2 Add(PlantillaV2 entidad)
        {
            try
            {
                var PlantillaV2 = MapeoEntidad(entidad);
                base.Insert(PlantillaV2);
                return PlantillaV2;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPlantillaV2 Update(PlantillaV2 entidad)
        {
            try
            {
                var PlantillaV2 = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PlantillaV2.RowVersion = entidadExistente.RowVersion;

                base.Update(PlantillaV2);
                return PlantillaV2;
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


        public IEnumerable<TPlantillaV2> Add(IEnumerable<PlantillaV2> listadoEntidad)
        {
            try
            {
                List<TPlantillaV2> listado = new List<TPlantillaV2>();
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

        public IEnumerable<TPlantillaV2> Update(IEnumerable<PlantillaV2> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPlantillaV2> listado = new List<TPlantillaV2>();
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
        /// Obtiene registros de T_PlantillaV2 para mostrarse en combo.
        /// </summary>
        /// <returns> List<PlantillaV2ComboDTO> </returns>
        public IEnumerable<PlantillaV2Combo> ObtenerCombo()
        {
            try
            {
                List<PlantillaV2Combo> rpta = new List<PlantillaV2Combo>();

                var query = "SELECT Id, Nombre FROM mkt.T_PlantillaV2 WHERE Estado=1";

                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PlantillaV2Combo>>(resultado);
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
        /// Obtiene todos los registros de T_PlantillaV2
        /// </summary>
        /// <returns> List<PlantillaV2> </returns>
        public IEnumerable<PlantillaV2> ObtenerPlantillaV2()
        {
            try
            {
                List<PlantillaV2> rpta = new List<PlantillaV2>();
                var query = @"SELECT Id, Nombre, Codigo FROM mkt.T_PlantillaV2 WHERE Estado=1 order by Id desc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PlantillaV2>>(resultado);
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
