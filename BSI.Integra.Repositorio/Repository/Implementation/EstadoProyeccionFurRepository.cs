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
    /// Repositorio: EstadoProyeccionFurRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_EstadoProyeccionFur
    /// </summary>
    public class EstadoProyeccionFurRepository : GenericRepository<TEstadoProyeccionFur>, IEstadoProyeccionFurRepository
    {
        private Mapper _mapper;

        public EstadoProyeccionFurRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEstadoProyeccionFur, EstadoProyeccionFur>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TEstadoProyeccionFur MapeoEntidad(EstadoProyeccionFur entidad)
        {
            try
            {
                //crea la entidad padre
                TEstadoProyeccionFur modelo = _mapper.Map<TEstadoProyeccionFur>(entidad);

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

        public TEstadoProyeccionFur Add(EstadoProyeccionFur entidad)
        {
            try
            {
                var EstadoProyeccionFur = MapeoEntidad(entidad);
                base.Insert(EstadoProyeccionFur);
                return EstadoProyeccionFur;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEstadoProyeccionFur Update(EstadoProyeccionFur entidad)
        {
            try
            {
                var EstadoProyeccionFur = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                EstadoProyeccionFur.RowVersion = entidadExistente.RowVersion;

                base.Update(EstadoProyeccionFur);
                return EstadoProyeccionFur;
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


        public IEnumerable<TEstadoProyeccionFur> Add(IEnumerable<EstadoProyeccionFur> listadoEntidad)
        {
            try
            {
                List<TEstadoProyeccionFur> listado = new List<TEstadoProyeccionFur>();
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

        public IEnumerable<TEstadoProyeccionFur> Update(IEnumerable<EstadoProyeccionFur> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEstadoProyeccionFur> listado = new List<TEstadoProyeccionFur>();
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


        /// Autor:Griselberto huamanc
        /// Fecha:08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro por id
        /// </summary>
        /// <returns> EstadoProyeccionFur </returns>
        public EstadoProyeccionFur ObtenerEstadoProyeccionFurById(int Id)
        {
            try
            {
                EstadoProyeccionFur rpta = new EstadoProyeccionFur();
                var query = @"SELECT * FROM fin.T_EstadoProyeccionFur
                            WHERE Estado=1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new {Id= Id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<EstadoProyeccionFur>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha:08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_EstadoProyeccionFur
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        public IEnumerable<EstadoProyeccionFurDTO> ObtenerComboEstadoProyeccionFur()
        {
            try
            {
                List<EstadoProyeccionFurDTO> rpta = new List<EstadoProyeccionFurDTO>();
                var query = @"SELECT Id,Nombre
                            FROM fin.T_EstadoProyeccionFur 
                            WHERE Estado=1 ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EstadoProyeccionFurDTO>>(resultado);
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
