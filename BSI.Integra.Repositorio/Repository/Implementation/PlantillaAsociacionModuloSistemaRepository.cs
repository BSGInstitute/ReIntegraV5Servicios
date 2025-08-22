using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PlantillaAsociacionModuloSistemaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 13/06/2022
    /// <summary>
    /// Gestión general de T_PlantillaAsociacionModuloSistema
    /// </summary>
    public class PlantillaAsociacionModuloSistemaRepository : GenericRepository<TPlantillaAsociacionModuloSistema>, IPlantillaAsociacionModuloSistemaRepository
    {
        private Mapper _mapper;

        public PlantillaAsociacionModuloSistemaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPlantillaAsociacionModuloSistema, PlantillaAsociacionModuloSistema>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPlantillaAsociacionModuloSistema MapeoEntidad(PlantillaAsociacionModuloSistema entidad)
        {
            try
            {
                //crea la entidad padre
                TPlantillaAsociacionModuloSistema modelo = _mapper.Map<TPlantillaAsociacionModuloSistema>(entidad);

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

        public TPlantillaAsociacionModuloSistema Add(PlantillaAsociacionModuloSistema entidad)
        {
            try
            {
                var PlantillaAsociacionModuloSistema = MapeoEntidad(entidad);
                base.Insert(PlantillaAsociacionModuloSistema);
                return PlantillaAsociacionModuloSistema;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPlantillaAsociacionModuloSistema Update(PlantillaAsociacionModuloSistema entidad)
        {
            try
            {
                var PlantillaAsociacionModuloSistema = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PlantillaAsociacionModuloSistema.RowVersion = entidadExistente.RowVersion;

                base.Update(PlantillaAsociacionModuloSistema);
                return PlantillaAsociacionModuloSistema;
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


        public IEnumerable<TPlantillaAsociacionModuloSistema> Add(IEnumerable<PlantillaAsociacionModuloSistema> listadoEntidad)
        {
            try
            {
                List<TPlantillaAsociacionModuloSistema> listado = new List<TPlantillaAsociacionModuloSistema>();
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

        public IEnumerable<TPlantillaAsociacionModuloSistema> Update(IEnumerable<PlantillaAsociacionModuloSistema> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPlantillaAsociacionModuloSistema> listado = new List<TPlantillaAsociacionModuloSistema>();
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
        /// Autor: Erick Marcelo Quispe.

        public void EliminacionLogicoPorPlantilla(int idPlantilla, string usuario, List<int> nuevos)
        {
            try
            {
                var listaBorrar = GetBy(x => x.IdPlantilla == idPlantilla && x.Estado == true).ToList();
                listaBorrar.RemoveAll(x => nuevos.Any(y => y == x.IdModuloSistema));
                Delete(listaBorrar.Select(x => x.Id), usuario);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<PlantillaAsociacionModuloSistema> ObtenerPlantillaAsociacionModuloSistemaPorIdPlantilla(int idPlantilla)
        {
            try
            {
                List<PlantillaAsociacionModuloSistema> plantillaClavevValor = new List<PlantillaAsociacionModuloSistema>();
                var query = @"
                             SELECT * FROM mkt.V_ObtenerPlantillaModuloSistemaNombre
                             WHERE IdPlantilla = @idPlantilla";
                var resultadoQuery = _dapperRepository.QueryDapper(query, new { idPlantilla });
                if (!string.IsNullOrEmpty(resultadoQuery) && !resultadoQuery.Contains("[]"))
                {
                    plantillaClavevValor = JsonConvert.DeserializeObject<List<PlantillaAsociacionModuloSistema>>(resultadoQuery);
                }
                return plantillaClavevValor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PlantillaAsociacionModuloSistema ObtenerPorIdModuloSistemaYPorIdPlantilla(int idModuloSistema, int idPlantilla)
        {
            try
            {
                PlantillaAsociacionModuloSistema rpta = new PlantillaAsociacionModuloSistema();
                var query = @"SELECT Id,
                                   IdPlantilla,
                                   IdModuloSistema,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM mkt.T_PlantillaAsociacionModuloSistema
                            WHERE Estado = 1
                                  AND IdModuloSistema = @IdModuloSistema
                                  AND IdPlantilla = @IdPlantilla;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdModuloSistema = idModuloSistema, IdPlantilla = idPlantilla });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<PlantillaAsociacionModuloSistema>(resultado)!;
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
