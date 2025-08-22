using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.SCode.Service.Implementacion;
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

    /// Repositorio:PreguntaEncuestaRepository
    /// Autor: Gilmer Quispe
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión general de T_SolicitudTipoReporte
    /// </summary>
    public class PreguntaEncuestaRepository : GenericRepository<TPreguntaEncuestum>, IPreguntaEncuestaRepository
    {
        private Mapper _mapper;

        public PreguntaEncuestaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPreguntaEncuestum, PreguntaEncuesta>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TPreguntaEncuestum MapeoEntidad(PreguntaEncuesta entidad)
        {
            try
            {
                //crea la entidad padre
                TPreguntaEncuestum modelo = _mapper.Map<TPreguntaEncuestum>(entidad);

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

        public TPreguntaEncuestum Add(PreguntaEncuesta entidad)
        {
            try
            {
                var PreguntaEncuesta = MapeoEntidad(entidad);
                base.Insert(PreguntaEncuesta);
                return PreguntaEncuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPreguntaEncuestum Update(PreguntaEncuesta entidad)
        {
            try
            {
                var PreguntaEncuesta = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PreguntaEncuesta.RowVersion = entidadExistente.RowVersion;

                base.Update(PreguntaEncuesta);
                return PreguntaEncuesta;
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


        public IEnumerable<TPreguntaEncuestum> Add(IEnumerable<PreguntaEncuesta> listadoEntidad)
        {
            try
            {
                List<TPreguntaEncuestum> listado = new List<TPreguntaEncuestum>();
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

        public IEnumerable<TPreguntaEncuestum> Update(IEnumerable<PreguntaEncuesta> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPreguntaEncuestum> listado = new List<TPreguntaEncuestum>();
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
        public PreguntaEncuesta ObtenerPorId(int id)
        {
            try
            {
                var rpta = new PreguntaEncuesta();
                var query = @"SELECT Id
		                             ,IdPreguntaEncuestaCategoria
		                            ,IdPreguntaEncuestaTipo
		                            ,Pregunta
		                            ,ActivarDescripcion
		                            ,Descripcion
		                            ,PreguntaObligatoria
		                            ,PreguntaActiva
		                            ,Estado
		                            ,UsuarioCreacion
		                            ,UsuarioModificacion
		                            ,FechaCreacion
		                            ,FechaModificacion
		                            ,RowVersion 
                                FROM pla.T_PreguntaEncuesta
                                WHERE
                                	Estado=1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<PreguntaEncuesta>(resultado);
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
                var query = @"SELECT Id,Nombre FROM pla.T_PreguntaEncuesta WHERE Estado=1";
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
        public List<BancoPreguntaEncuestaDTO> ObtenerPreguntaEncuesta()
        {
            try
            {
                var rpta = new List<BancoPreguntaEncuestaDTO>();
                var query = @"SELECT IdPreguntaEncuesta
		                                ,IdPreguntaEncuestaCategoria
		                                ,Categoria
		                                ,IdPreguntaEncuestaTipo
		                                ,Tipo
		                                ,Pregunta
		                                ,Descripcion
		                                ,ActivarDescripcion
		                                ,PreguntaObligatoria
		                                ,PreguntaActiva
                                FROM [pla].[V_PreguntaEncuesta]
                                ORDER BY IdPreguntaEncuesta DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<BancoPreguntaEncuestaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 07/05/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos las preguntas asincronicas
        /// </summary> 
        /// <returns> List<PreguntaEncuestaAsincronicaDTO> </returns>
        public List<PreguntaEncuestaAsincronicaDTO> ObtenerPreguntaEncuestaAsincronica()
        {
            try
            {
                var rpta = new List<PreguntaEncuestaAsincronicaDTO>();
                var query = @"SELECT
	                                IdPregunta,
	                                EnunciadoPregunta,
	                                IdTipoRespuesta,
	                                TipoRespuesta,
	                                IdPreguntaTipo,
	                                PreguntaTipo
                                FROM
	                                pla.V_PreguntasEncuestasAsincronica
                                WHERE
	                                Estado = 1 AND IdPreguntaCategoria = 3";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<PreguntaEncuestaAsincronicaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 07/05/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos las preguntas asincronicas
        /// </summary>
        /// <param name="idEncuesta"> Id de la entidad </param>
        /// <returns> List<PreguntaEncuestaAsincronicaDTO> </returns>
        public List<BancoPreguntaEncuestaAsincronicaDTO> ObtenerPreguntaEncuestaAsincronicaPorId(int idEncuesta)
        {
            try
            {
                var rpta = new List<BancoPreguntaEncuestaAsincronicaDTO>();

                var query = @"pla.SP_ObtenerPreguntaEncuestaAsincronicaPorExamen";

                var resultado = _dapperRepository.QuerySPDapper(query, new {IdExamen=idEncuesta});
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<BancoPreguntaEncuestaAsincronicaDTO>>(resultado);
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
