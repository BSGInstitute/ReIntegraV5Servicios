using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
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
    /// Repositorio: AreaCentroCostoRepository
    /// Autor Modificacion: Klebert Layme.
    /// Fecha: 26/04/2023
    /// <summary>
    /// Gestión general de T_AreaCc
    /// </summary>
    public class AreaCcRepository : GenericRepository<TAreaCc>, IAreaCcRepository
    {
        private Mapper _mapper;
        public AreaCcRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAreaCc, AreaCC>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TAreaCc MapeoEntidad(AreaCC entidad)
        {
            try
            {
                //crea la entidad padre
                TAreaCc modelo = _mapper.Map<TAreaCc>(entidad);

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

        public TAreaCc Add(AreaCC entidad)
        {
            try
            {
                var AreaCC = MapeoEntidad(entidad);
                base.Insert(AreaCC);
                return AreaCC;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAreaCc Update(AreaCC entidad)
        {
            try
            {
                var AreaCC = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AreaCC.RowVersion = entidadExistente.RowVersion;

                base.Update(AreaCC);
                return AreaCC;
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


        public IEnumerable<TAreaCc> Add(IEnumerable<AreaCC> listadoEntidad)
        {
            try
            {
                List<TAreaCc> listado = new List<TAreaCc>();
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

        public IEnumerable<TAreaCc> Update(IEnumerable<AreaCC> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAreaCc> listado = new List<TAreaCc>();
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

        /// Autor Modificacion: Klebert Layme.
        /// Fecha: 26/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_AreaCc.
        /// </summary>
        /// <returns> List<AreaCentroCostoDTO> </returns>
        public IEnumerable<AreaCC> Obtener()
        {
            try
            {
                List<AreaCC> rpta = new();
                var query = @"
                    SELECT Id,
                            Nombre,
                            codigo,
                            estado
                            FROM pla.V_TAreaCC_ObtenerDatos WHERE Estado=1  ORDER BY id DESC ";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<AreaCC>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor Modificacion: Gretel Canasa.
        /// Fecha: 06/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_AreaCc.
        /// </summary>
        /// <returns> List<AreaCentroCostoDTO> </returns>

        public async Task<IEnumerable<ComboDTO>> ObtenerComboAsync()
        {
            try
            {
                var comboDTOs = new List<ComboDTO>();
                var query = @"SELECT Id,Nombre from pla.T_AreaCC Where Estado=1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    comboDTOs = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);

                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}");
            }
        }

        /// Autor Modificacion: Gretel Canasa.
        /// Fecha: 06/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_AreaCc.
        /// </summary>
        /// <returns> List<AreaCentroCostoDTO> </returns>

        public async Task<IEnumerable<AreaCCDTO>> ObtenerAreaCCAsync()
        {
            try
            {
                var comboDTOs = new List<AreaCCDTO>();
                var query = @"SELECT Id,Nombre,Concat(Id,'-',Codigo)as Codigo FROM pla.V_TAreaCC_ObtenerDatos WHERE Estado=1";
                var resultado = await _dapperRepository.QueryDapperAsync(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    comboDTOs = JsonConvert.DeserializeObject<List<AreaCCDTO>>(resultado);

                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCombo(): {ex.Message}");
            }
        }

        /// Autor Modificacion: Gretel Canasa.
        /// Fecha: 06/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_AreaCc.
        /// </summary>
        /// <returns> List<AreaCentroCostoDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new();
                var query = @"SELECT Id,Nombre from pla.T_AreaCC Where Estado=1";
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


        /// Autor: Klebert Layme.
        /// Fecha: 26/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de AreaCentroCostoDTO.
        /// </summary>
        /// <returns> List<AreaCentroCostoDTO> </returns>
        public AreaCC ObtenerPorId(int id)
        {
            try
            {
                AreaCC rpta = new();
                var query = @"
                        SELECT 
                Id,
                Nombre,
                Codigo,
                Estado,
                UsuarioCreacion,
                UsuarioModificacion,
                FechaCreacion,
                FechaModificacion,
                RowVersion,
                IdMigracion
                    FROM pla.T_AreaCC
                        WHERE Estado = 1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<AreaCC>(resultado)!;
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
