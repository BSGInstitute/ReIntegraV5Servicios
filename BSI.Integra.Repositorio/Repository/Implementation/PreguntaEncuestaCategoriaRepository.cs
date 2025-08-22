using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SolicitudTipoReporteRepository
    /// Autor: Gilmer Quispe
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión general de T_SolicitudTipoReporte
    /// </summary>
    public class PreguntaEncuestaCategoriaRepository : GenericRepository<TPreguntaEncuestaCategorium>, IPreguntaEncuestaCategoriaRepository
    {
        private Mapper _mapper;

        public PreguntaEncuestaCategoriaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreguntaEncuestaCategorium, PreguntaEncuestaCategoria>(MemberList.None).ReverseMap();
            //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
        });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPreguntaEncuestaCategorium MapeoEntidad(PreguntaEncuestaCategoria entidad)
        {
            try
            {
                //crea la entidad padre
                TPreguntaEncuestaCategorium modelo = _mapper.Map<TPreguntaEncuestaCategorium>(entidad);

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

        public TPreguntaEncuestaCategorium Add(PreguntaEncuestaCategoria entidad)
        {
            try
            {
                var PreguntaEncuestaCategoria = MapeoEntidad(entidad);
                base.Insert(PreguntaEncuestaCategoria);
                return PreguntaEncuestaCategoria;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreguntaEncuestaCategorium Update(PreguntaEncuestaCategoria entidad)
        {
            try
            {
                var PreguntaEncuestaCategoria = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PreguntaEncuestaCategoria.RowVersion = entidadExistente.RowVersion;

                base.Update(PreguntaEncuestaCategoria);
                return PreguntaEncuestaCategoria;
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


        public IEnumerable<TPreguntaEncuestaCategorium> Add(IEnumerable<PreguntaEncuestaCategoria> listadoEntidad)
        {
            try
            {
                List<TPreguntaEncuestaCategorium> listado = new List<TPreguntaEncuestaCategorium>();
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

        public IEnumerable<TPreguntaEncuestaCategorium> Update(IEnumerable<PreguntaEncuestaCategoria> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPreguntaEncuestaCategorium> listado = new List<TPreguntaEncuestaCategorium>();
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
        /// Fecha: 21/12/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudTipoReporte </returns>
        public PreguntaEncuestaCategoria ObtenerPorId(int id)
        {
            try
            {
                var rpta = new PreguntaEncuestaCategoria();
                var query = @"SELECT
                                	Id
                                	,Nombre
                                	,Descripcion
                                	,Estado
                                	,UsuarioCreacion
                                	,UsuarioModificacion
                                	,FechaCreacion
                                	,FechaModificacion
                                	,RowVersion
                                FROM
                                	pla.T_PreguntaEncuestaCategoria
                                WHERE
                                	Estado=1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<PreguntaEncuestaCategoria>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 21/12/2024
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
                var query = @"SELECT Id,Nombre FROM pla.T_PreguntaEncuestaCategoria WHERE Estado=1";
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
        /// Fecha: 21/12/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_SolicitudCategoria por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudTipoReporte </returns>
        public List<PreguntaEncuestaCategoriaDTO> ObtenerCategoriaEncuesta()
        {
            try
            {
                var rpta = new List<PreguntaEncuestaCategoriaDTO>();
                var query = @"SELECT
                                	Id
                                	,Nombre
                                	,Descripcion
                                	,Estado
                                	,UsuarioCreacion
                                	,UsuarioModificacion
                                	,FechaCreacion
                                	,FechaModificacion
                                	,RowVersion
                                FROM
                                	pla.T_PreguntaEncuestaCategoria
                                WHERE
                                	Estado=1";
                var resultado = _dapperRepository.QueryDapper(query,null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject <List<PreguntaEncuestaCategoriaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PreguntaCategoriaAsincronicaDTO> ObtenerPreguntaCategoriaAsincronica()
        {
            try
            {
                var rpta = new List<PreguntaCategoriaAsincronicaDTO>();
                var query = @"  SELECT Id,Nombre,Descripcion FROM gp.T_TipoRespuesta WHERE Estado=1;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<PreguntaCategoriaAsincronicaDTO>>(resultado);
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
