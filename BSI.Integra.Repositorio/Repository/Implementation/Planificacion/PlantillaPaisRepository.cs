using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PlantillaPaisRepository
    /// Autor: Gilmer Qm
    /// Fecha: 27/06/2023
    /// <summary>
    /// Gestión general de T_PlantillaPais
    /// </summary>
    public class PlantillaPaisRepository : GenericRepository<TPlantillaPai>, IPlantillaPaisRepository
    {
        private Mapper _mapper;
        public PlantillaPaisRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPlantillaPai, PlantillaPais>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPlantillaPai MapeoEntidad(PlantillaPais entidad)
        {
            try
            {
                //crea la entidad padre
                TPlantillaPai modelo = _mapper.Map<TPlantillaPai>(entidad);

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
        public TPlantillaPai Add(PlantillaPais entidad)
        {
            try
            {
                var PlantillaPais = MapeoEntidad(entidad);
                base.Insert(PlantillaPais);
                return PlantillaPais;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TPlantillaPai Update(PlantillaPais entidad)
        {
            try
            {
                var PlantillaPais = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PlantillaPais.RowVersion = entidadExistente.RowVersion;

                base.Update(PlantillaPais);
                return PlantillaPais;
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
        public IEnumerable<TPlantillaPai> Add(IEnumerable<PlantillaPais> listadoEntidad)
        {
            try
            {
                List<TPlantillaPai> listado = new List<TPlantillaPai>();
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
        public IEnumerable<TPlantillaPai> Update(IEnumerable<PlantillaPais> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPlantillaPai> listado = new List<TPlantillaPai>();
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
        /// Autor: Gilmer Qm.
        /// Fecha: 27/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PlantillaPais Asociados al IdPlantilla.
        /// </summary>
        /// <param name="idPlantillaPw"> (PK) de T_Plantilla_PW </param>
        /// <returns> IEnumerable<PlantillaPais> </returns>
        public IEnumerable<PlantillaPais> ObtenerPorIdPlantillaPw(int idPlantillaPw)
        {
            try
            {
                var query = @"SELECT Id,
                                   IdPlantilla,
                                   IdPais,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PlantillaPais
                            WHERE Estado = 1
                            AND IdPlantilla = @IdPlantillaPw;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPlantillaPw = idPlantillaPw });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PlantillaPais>>(resultado);
                }
                return new List<PlantillaPais>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 27/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PlantillaPais Asociados al IdPlantilla y el IdPais.
        /// </summary>
        /// <param name="idPlantillaPw"> (PK) de T_Plantilla_PW </param>
        /// <returns> IEnumerable<PlantillaPais> </returns>
        public PlantillaPais ObtenerPorIdPaisYIdPlantillaPw(int idPais, int idPlantillaPw)
        {
            try
            {
                var query = @"SELECT Id,
                                   IdPlantilla,
                                   IdPais,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PlantillaPais
                            WHERE Estado = 1
                                  AND IdPais = @IdPais
                                  AND IdPlantilla = @IdPlantillaPw;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPais = idPais, IdPlantillaPw = idPlantillaPw });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<PlantillaPais>(resultado);
                }
                return new PlantillaPais();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
