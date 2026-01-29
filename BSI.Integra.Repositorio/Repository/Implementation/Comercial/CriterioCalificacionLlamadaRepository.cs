using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Comercial;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Comercial
{

    /// Repositorio: CriterioCalificacionRepository
    /// Autor: Joseph LLanque
    /// Fecha: 07/03/2025
    /// <summary>
    /// Gestión general de TCriterioCalificacionLlamadum
    /// </summary>
    public class CriterioCalificacionLlamadaRepository : GenericRepository<TCriterioCalificacionLlamadum>, ICriterioCalificacionLlamadaRepository
    {
        private Mapper _mapper;

        public CriterioCalificacionLlamadaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCriterioCalificacionLlamadum, CriterioCalificacionLlamada>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TCriterioCalificacionLlamadum MapeoEntidad(CriterioCalificacionLlamada entidad)
        {
            try
            {
                //crea la entidad padre
                TCriterioCalificacionLlamadum modelo = _mapper.Map<TCriterioCalificacionLlamadum>(entidad);

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

        public TCriterioCalificacionLlamadum Add(CriterioCalificacionLlamada entidad)
        {
            try
            {
                var CriterioCalificacionLlamada = MapeoEntidad(entidad);
                base.Insert(CriterioCalificacionLlamada);
                return CriterioCalificacionLlamada;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCriterioCalificacionLlamadum Update(CriterioCalificacionLlamada entidad)
        {
            try
            {
                var CriterioCalificacionLlamada = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CriterioCalificacionLlamada.RowVersion = entidadExistente.RowVersion;

                base.Update(CriterioCalificacionLlamada);
                return CriterioCalificacionLlamada;
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


        public IEnumerable<TCriterioCalificacionLlamadum> Add(IEnumerable<CriterioCalificacionLlamada> listadoEntidad)
        {
            try
            {
                List<TCriterioCalificacionLlamadum> listado = new List<TCriterioCalificacionLlamadum>();
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

        public IEnumerable<TCriterioCalificacionLlamadum> Update(IEnumerable<CriterioCalificacionLlamada> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCriterioCalificacionLlamadum> listado = new List<TCriterioCalificacionLlamadum>();
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
        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de com.T_FaseCalificacion por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> CriterioCalificacionLlamada </returns>
        public CriterioCalificacionLlamada ObtenerPorId(int id)
        {
            try
            {
                var rpta = new CriterioCalificacionLlamada();
                var query = @"SELECT Id,
                                       IdFaseCalificacion,
                                       NombreCriterio,
                                       Orden,
                                       Descripcion,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion
                                FROM com.T_CriterioCalificacionLlamada
                                WHERE Estado =1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<CriterioCalificacionLlamada>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var comboDTOs = new List<ComboDTO>();
                var query = @"SELECT Id,NombreCriterio 
                                FROM com.T_CriterioCalificacionLlamada 
                                WHERE Estado =1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    comboDTOs = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
                }
                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Joseph Llanque
        /// Fecha: 07/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary> 
        /// <returns> IEnumerable<ComboDTO> </returns>
        public IEnumerable<CriterioCalificacionLlamada> ObtenerCriterios()
        {
            try
            {
                var comboDTOs = new List<CriterioCalificacionLlamada>();
                var query = @"SELECT Id,IdFaseCalificacion,NombreCriterio,Orden,Descripcion
                                FROM com.T_CriterioCalificacionLlamada 
                                WHERE Estado =1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]"))
                {
                    comboDTOs = JsonConvert.DeserializeObject<List<CriterioCalificacionLlamada>>(resultado);
                }
                return comboDTOs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
