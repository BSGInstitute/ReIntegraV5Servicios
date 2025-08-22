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
    /// Repositorio: TroncalCiudadRepository
    /// Autor: Margiory Meiss Ramirez Neyra..
    /// Fecha: 17/10/2022
    /// <summary>
    /// Gestión general de T_TroncalCiudad
    /// </summary>
    public class TroncalCiudadRepository : GenericRepository<TTroncalCiudad>, ITroncalCiudadRepository
    {
        private Mapper _mapper;
        public TroncalCiudadRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTroncalCiudad, TroncalCiudad>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTroncalCiudad MapeoEntidad(TroncalCiudad entidad)
        {
            try
            {
                //crea la entidad padre
                TTroncalCiudad modelo = _mapper.Map<TTroncalCiudad>(entidad);

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

        public TTroncalCiudad Add(TroncalCiudad entidad)
        {
            try
            {
                var TroncalCiudad = MapeoEntidad(entidad);
                base.Insert(TroncalCiudad);
                return TroncalCiudad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTroncalCiudad Update(TroncalCiudad entidad)
        {
            try
            {
                var TroncalCiudad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TroncalCiudad.RowVersion = entidadExistente.RowVersion;

                base.Update(TroncalCiudad);
                return TroncalCiudad;
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


        public IEnumerable<TTroncalCiudad> Add(IEnumerable<TroncalCiudad> listadoEntidad)
        {
            try
            {
                List<TTroncalCiudad> listado = new List<TTroncalCiudad>();
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

        public IEnumerable<TTroncalCiudad> Update(IEnumerable<TroncalCiudad> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTroncalCiudad> listado = new List<TTroncalCiudad>();
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
       
        /// Autor: Gretel Canasa
        /// Fecha: 19/05/2023
        /// <summary>
        /// Obtiene la lista de troncales con su region
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboTroncalCiudadAsync()
        {
            try
            {
                var query = "SELECT Id, Nombre FROM pla.T_TroncalCiudad WHERE Estado = 1";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerLocacionTroncalAsync(): {ex.Message}", ex);
            }
        }
    }
}