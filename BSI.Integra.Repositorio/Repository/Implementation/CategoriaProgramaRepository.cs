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
    /// Repositorio: ArticuloRepository
    /// Autor: Max Mantilla.
    /// Fecha: 25/10/2022
    /// <summary>
    /// Gestión general de T_CategoriaPrograma
    /// </summary>
    public class CategoriaProgramaRepository : GenericRepository<TCategoriaPrograma>, ICategoriaProgramaRepository
    {
        private Mapper _mapper;

        public CategoriaProgramaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCategoriaPrograma, CategoriaPrograma>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TCategoriaPrograma MapeoEntidad(CategoriaPrograma entidad)
        {
            try
            {
                //crea la entidad padre
                TCategoriaPrograma modelo = _mapper.Map<TCategoriaPrograma>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCategoriaPrograma Add(CategoriaPrograma entidad)
        {
            try
            {
                var CategoriaPrograma = MapeoEntidad(entidad);
                base.Insert(CategoriaPrograma);
                return CategoriaPrograma;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCategoriaPrograma Update(CategoriaPrograma entidad)
        {
            try
            {
                var CategoriaPrograma = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CategoriaPrograma.RowVersion = entidadExistente.RowVersion;

                base.Update(CategoriaPrograma);
                return CategoriaPrograma;
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


        public IEnumerable<TCategoriaPrograma> Add(IEnumerable<CategoriaPrograma> listadoEntidad)
        {
            try
            {
                List<TCategoriaPrograma> listado = new List<TCategoriaPrograma>();
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

        public IEnumerable<TCategoriaPrograma> Update(IEnumerable<CategoriaPrograma> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCategoriaPrograma> listado = new List<TCategoriaPrograma>();
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

        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 25/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_CategoriaPrograma
        /// </summary>
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"Select Id,Categoria AS Nombre FROM pla.T_CategoriaPrograma WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var query = @"Select Id,Categoria AS Nombre FROM pla.T_CategoriaPrograma WHERE Estado=1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
