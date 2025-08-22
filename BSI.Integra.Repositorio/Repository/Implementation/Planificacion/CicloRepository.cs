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
    /// Repositorio: CicloRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_Ciclo
    /// </summary>
    public class CicloRepository : GenericRepository<TCiclo>, ICicloRepository
    {
        private Mapper _mapper;

        public CicloRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCiclo, Ciclo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }




        #region Metodos Base
        private TCiclo MapeoEntidad(Ciclo entidad)
        {
            try
            {
                TCiclo modelo = _mapper.Map<TCiclo>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCiclo Add(Ciclo entidad)
        {
            try
            {
                var Ciclo = MapeoEntidad(entidad);
                base.Insert(Ciclo);
                return Ciclo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCiclo Update(Ciclo entidad)
        {
            try
            {
                var Ciclo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Ciclo.RowVersion = entidadExistente.RowVersion;

                base.Update(Ciclo);
                return Ciclo;
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


        public IEnumerable<TCiclo> Add(IEnumerable<Ciclo> listadoEntidad)
        {
            try
            {
                List<TCiclo> listado = new List<TCiclo>();
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

        public IEnumerable<TCiclo> Update(IEnumerable<Ciclo> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCiclo> listado = new List<TCiclo>();
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
        /// Obtiene registros de T_Ciclo para mostrarse en combo.
        /// </summary>
        /// <returns> List<CicloComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> respuesta = new List<ComboDTO>();

                var query = "SELECT Id, Descripcion AS Nombre FROM pla.T_Ciclo WHERE Estado=1";
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
        /// Obtiene registros de T_Ciclo para filtros
        /// </summary>
        /// <returns> registros para combo IEnumerable CicloComboDTO</returns>
        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                string query = @"SELECT Id AS Id,
                                        Descripcion AS Nombre 
                                FROM pla.T_Ciclo
                                WHERE Estado = 1;";
                string resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado)!;
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCicloFiltro(): {ex.Message}", ex);
            }
        }
  //      public List<Ciclo> ObtenerPorIds(string ids)
  //      {
  //          try
  //          {
  //              List<Ciclo> rpta = new();
  //              var query = @"
  //                      SELECT
  //                          Id,Codigo,Nombre,IdPais,LongCelular,LongTelefono,Estado,UsuarioCreacion,UsuarioModificacion,FechaCreacion,FechaModificacion,RowVersion,IdMigracion,LongCelularAlterno
  //                      FROM conf.T_Ciclo
  //                      WHERE Estado = 1 AND Id IN (select Item from conf.F_Splitstring(@ids,','))";
  //              var resultado = _dapperRepository.QueryDapper(query, new { ids });
  //              if (!string.IsNullOrEmpty(resultado) && resultado != "null")
  //              {
  //                  rpta = JsonConvert.DeserializeObject<List<Ciclo>>(resultado);
  //              }
  //              return rpta;
  //          }
  //          catch (Exception ex)
  //          {
  //              throw ex;
  //          }
  //      }

    }
}
