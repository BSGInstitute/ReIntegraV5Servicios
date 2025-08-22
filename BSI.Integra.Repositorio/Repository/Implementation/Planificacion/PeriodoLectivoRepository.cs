using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: PeriodoLectivoRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PeriodoLectivo
    /// </summary>
    public class PeriodoLectivoRepository : GenericRepository<TPeriodoLectivo>, IPeriodoLectivoRepository
    {
        private Mapper _mapper;

        public PeriodoLectivoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPeriodoLectivo, PeriodoLectivo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }




        #region Metodos Base
        private TPeriodoLectivo MapeoEntidad(PeriodoLectivo entidad)
        {
            try
            {

                TPeriodoLectivo modelo = _mapper.Map<TPeriodoLectivo>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPeriodoLectivo Add(PeriodoLectivo entidad)
        {
            try
            {
                var PeriodoLectivo = MapeoEntidad(entidad);
                base.Insert(PeriodoLectivo);
                return PeriodoLectivo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPeriodoLectivo Update(PeriodoLectivo entidad)
        {
            try
            {
                var PeriodoLectivo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PeriodoLectivo.RowVersion = entidadExistente.RowVersion;

                base.Update(PeriodoLectivo);
                return PeriodoLectivo;
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


        public IEnumerable<TPeriodoLectivo> Add(IEnumerable<PeriodoLectivo> listadoEntidad)
        {
            try
            {
                List<TPeriodoLectivo> listado = new List<TPeriodoLectivo>();
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

        public IEnumerable<TPeriodoLectivo> Update(IEnumerable<PeriodoLectivo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPeriodoLectivo> listado = new List<TPeriodoLectivo>();
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
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PeriodoLectivo para mostrarse en combo.
        /// </summary>
        /// <returns> List<PeriodoLectivoComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> respuesta = new List<ComboDTO>();

                var query = "SELECT Id, Descripcion AS Nombre FROM pla.T_PeriodoLectivo WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Gretel Canasa
        /// Fecha: 03/09/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PeriodoLectivo para filtros
        /// </summary>
        /// <returns> registros para combo IEnumerable PeriodoLectivoComboDTO</returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                string query = @"SELECT Id AS Id,
                                        Descripcion AS Nombre 
                                FROM pla.T_PeriodoLectivo
                                WHERE Estado = 1;";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPeriodoLectivoFiltro(): {ex.Message}", ex);
            }
        }

    }
}
