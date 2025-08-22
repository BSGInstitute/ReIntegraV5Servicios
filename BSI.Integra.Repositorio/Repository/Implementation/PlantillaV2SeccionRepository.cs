using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PlantillaV2SeccionRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PlantillaV2Seccion
    /// </summary>
    public class PlantillaV2SeccionRepository : GenericRepository<TPlantillaV2seccion>, IPlantillaV2SeccionRepository
    {
        private Mapper _mapper;

        public PlantillaV2SeccionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPlantillaV2seccion, PlantillaV2Seccion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }




        #region Metodos Base
        private TPlantillaV2seccion MapeoEntidad(PlantillaV2Seccion entidad)
        {
            try
            {
                //crea la entidad padre
                TPlantillaV2seccion modelo = _mapper.Map<TPlantillaV2seccion>(entidad);

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

        public TPlantillaV2seccion Add(PlantillaV2Seccion entidad)
        {
            try
            {
                var PlantillaV2Seccion = MapeoEntidad(entidad);
                base.Insert(PlantillaV2Seccion);
                return PlantillaV2Seccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPlantillaV2seccion Update(PlantillaV2Seccion entidad)
        {
            try
            {
                var PlantillaV2Seccion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PlantillaV2Seccion.RowVersion = entidadExistente.RowVersion;

                base.Update(PlantillaV2Seccion);
                return PlantillaV2Seccion;
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


        public IEnumerable<TPlantillaV2seccion> Add(IEnumerable<PlantillaV2Seccion> listadoEntidad)
        {
            try
            {
                List<TPlantillaV2seccion> listado = new List<TPlantillaV2seccion>();
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

        public IEnumerable<TPlantillaV2seccion> Update(IEnumerable<PlantillaV2Seccion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPlantillaV2seccion> listado = new List<TPlantillaV2seccion>();
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
        /// Obtiene registros de T_PlantillaV2Seccion para mostrarse en combo.
        /// </summary>
        /// <returns> List<PlantillaV2SeccionComboDTO> </returns>
        public IEnumerable<PlantillaV2SeccionCombo> ObtenerCombo()
        {
            try
            {
                List<PlantillaV2SeccionCombo> rpta = new List<PlantillaV2SeccionCombo>();

                var query = "SELECT Id, Nombre FROM mkt.T_PlantillaV2Seccion WHERE Estado=1";

                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PlantillaV2SeccionCombo>>(resultado);
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
        /// Obtiene todos los registros de T_PlantillaV2Seccion
        /// </summary>
        /// <returns> List<PlantillaV2Seccion> </returns>
        public IEnumerable<PlantillaV2Seccion> ObtenerPlantillaV2Seccion()
        {
            try
            {
                List<PlantillaV2Seccion> rpta = new List<PlantillaV2Seccion>();
                var query = @"SELECT Id, IdPlantillaV2, IdSeccion FROM mkt.T_PlantillaV2Seccion WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PlantillaV2Seccion>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<PSTodo> ObtenerTodo(int id)
        {
            try
            {
                List<PSTodo> rpta = new List<PSTodo>();
                var query = @"SELECT * FROM mkt.V_Plantilla where IdPlantillaV2 =" + id;
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PSTodo>>(resultado);
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
