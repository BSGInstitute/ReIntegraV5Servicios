using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
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
    /// Repositorio: PreguntaEncuestaOnlineRepository
    /// Autor: Joseph Llanque
    /// Fecha: 22/08/2024
    /// <summary>
    /// Gestión general de T_SolicitudTipoReporte
    /// </summary>
    public class PreguntaEncuestaOnlineRepository : GenericRepository<TPreguntaEncuestaOnline>, IPreguntaEncuestaOnlineRepository
    {
        private Mapper _mapper;

        public PreguntaEncuestaOnlineRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreguntaEncuestaOnline, PreguntaEncuestaOnline>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPreguntaEncuestaOnline MapeoEntidad(PreguntaEncuestaOnline entidad)
        {
            try
            {
                //crea la entidad padre
                TPreguntaEncuestaOnline modelo = _mapper.Map<TPreguntaEncuestaOnline>(entidad);

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

        public TPreguntaEncuestaOnline Add(PreguntaEncuestaOnline entidad)
        {
            try
            {
                var PreguntaEncuestaOnline = MapeoEntidad(entidad);
                base.Insert(PreguntaEncuestaOnline);
                return PreguntaEncuestaOnline;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreguntaEncuestaOnline Update(PreguntaEncuestaOnline entidad)
        {
            try
            {
                var PreguntaEncuestaOnline = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PreguntaEncuestaOnline.RowVersion = entidadExistente.RowVersion;

                base.Update(PreguntaEncuestaOnline);
                return PreguntaEncuestaOnline;
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


        public IEnumerable<TPreguntaEncuestaOnline> Add(IEnumerable<PreguntaEncuestaOnline> listadoEntidad)
        {
            try
            {
                List<TPreguntaEncuestaOnline> listado = new List<TPreguntaEncuestaOnline>();
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

        public IEnumerable<TPreguntaEncuestaOnline> Update(IEnumerable<PreguntaEncuestaOnline> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPreguntaEncuestaOnline> listado = new List<TPreguntaEncuestaOnline>();
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
        public PreguntaEncuestaOnline ObtenerPorId(int id)
        {
            try
            {
                var rpta = new PreguntaEncuestaOnline();
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
                    rpta = JsonConvert.DeserializeObject<PreguntaEncuestaOnline>(resultado);
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
                var query = @"SELECT Id,Nombre FROM pla.T_PreguntaEncuestaOnline WHERE Estado=1";
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
        public List<PreguntaAsociadaEncuestaOnlineDTO> ObtenerPreguntaEncuestaOnline(int idEncuestaOnline)
        {
            try
            {


                var rpta = new List<PreguntaAsociadaEncuestaOnlineDTO>();
                var query = @"SELECT  IdPreguntaEncuesta
		                                ,IdEncuestaOnline
		                                ,IdPreguntaEncuestaOnline
		                                ,IdPreguntaEncuestaCategoria
		                                ,Categoria
		                                ,IdPreguntaEncuestaTipo
		                                ,Tipo
		                                ,Pregunta
		                                ,Descripcion
		                                ,ActivarDescripcion
		                                ,PreguntaObligatoria
		                                ,PreguntaActiva
                                FROM [pla].[V_BancoPreguntaEncuesta] WHERE IdEncuestaOnline=@idEncuestaOnline";
                var resultado = _dapperRepository.QueryDapper(query,  new { idEncuestaOnline} );
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<PreguntaAsociadaEncuestaOnlineDTO>>(resultado);
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
