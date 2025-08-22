using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ModoPersonalFurRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 13/06/2022
    /// <summary>
    /// Gestión general de T_ModoPersonalFur
    /// </summary>
    public class ModoPersonalFurRepository : GenericRepository<TModoPersonalFur>, IModoPersonalFurRepository
    {
        private Mapper _mapper;

        public ModoPersonalFurRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TModoPersonalFur, ModoPersonalFur>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TModoPersonalFur MapeoEntidad(ModoPersonalFur entidad)
        {
            try
            {
                //crea la entidad padre
                TModoPersonalFur modelo = _mapper.Map<TModoPersonalFur>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModoPersonalFur Add(ModoPersonalFur entidad)
        {
            try
            {
                var ModoPersonalFur = MapeoEntidad(entidad);
                base.Insert(ModoPersonalFur);
                return ModoPersonalFur;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TModoPersonalFur Update(ModoPersonalFur entidad)
        {
            try
            {
                var ModoPersonalFur = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ModoPersonalFur.RowVersion = entidadExistente.RowVersion;

                base.Update(ModoPersonalFur);
                return ModoPersonalFur;
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


        public IEnumerable<TModoPersonalFur> Add(IEnumerable<ModoPersonalFur> listadoEntidad)
        {
            try
            {
                List<TModoPersonalFur> listado = new List<TModoPersonalFur>();
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

        public IEnumerable<TModoPersonalFur> Update(IEnumerable<ModoPersonalFur> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TModoPersonalFur> listado = new List<TModoPersonalFur>();
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
        /// Obtiene El permiso del Personal Logeado, a traves del IdPersonal.
        /// </summary>
        /// <returns> ModoPersonalFurDTO </returns>
        public ModoPersonalFurDTO ObtenerPermisosFurByIdPersonal(int IdPersonal)
        {
            try
            {
                List<ModoPersonalFurDTO> rpta = new List<ModoPersonalFurDTO>();
                var query = @"
                    SELECT [Id]
                          ,[IdPer]
                          ,[Nombres]
                          ,[ModoFur]
                          ,[FurVencido]
                      FROM [fin].[V_PermisosFur]
                      WHERE [IdPer]=@IdPersonal";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ModoPersonalFurDTO>>(resultado);
                    return rpta.FirstOrDefault();
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
