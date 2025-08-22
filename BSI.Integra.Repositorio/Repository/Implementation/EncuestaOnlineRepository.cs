using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Linkedin;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: EncuestaOnlineRepository
    /// Autor: Gilmer Quispe
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión general de T_Encuesta Online
    /// </summary>
    public class EncuestaOnlineRepository : GenericRepository<TEncuestaOnline>, IEncuestaOnlineRepository
    {
        private Mapper _mapper;

        public EncuestaOnlineRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEncuestaOnline, EncuestaOnline>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TEncuestaOnline MapeoEntidad(EncuestaOnline entidad)
        {
            try
            {
                //crea la entidad padre
                TEncuestaOnline modelo = _mapper.Map<TEncuestaOnline>(entidad);

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

        public TEncuestaOnline Add(EncuestaOnline entidad)
        {
            try
            {
                var EncuestaOnline = MapeoEntidad(entidad);
                base.Insert(EncuestaOnline);
                return EncuestaOnline;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEncuestaOnline Update(EncuestaOnline entidad)
        {
            try
            {
                var EncuestaOnline = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                EncuestaOnline.RowVersion = entidadExistente.RowVersion;

                base.Update(EncuestaOnline);
                return EncuestaOnline;
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


        public IEnumerable<TEncuestaOnline> Add(IEnumerable<EncuestaOnline> listadoEntidad)
        {
            try
            {
                List<TEncuestaOnline> listado = new List<TEncuestaOnline>();
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

        public IEnumerable<TEncuestaOnline> Update(IEnumerable<EncuestaOnline> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEncuestaOnline> listado = new List<TEncuestaOnline>();
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
        public EncuestaOnline ObtenerPorId(int id)
        {
            try
            {
                var rpta = new EncuestaOnline();
                var query = @"SELECT Id
			                    ,Nombre
			                    ,Codigo
			                    ,Descripcion
			                    ,Estado
			                    ,UsuarioCreacion
			                    ,UsuarioModificacion
			                    ,FechaCreacion
			                    ,FechaModificacion
			                    ,RowVersion
			                    ,Version
			                    ,IdTipoEncuesta
			                    ,IdModalidadCurso
                                FROM pla.T_EncuestaOnline WHERE Estado=1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<EncuestaOnline>(resultado);
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
                var query = @"	SELECT Id,Nombre FROM pla.T_EncuestaOnline WHERE Estado=1";
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
        public List<EncuestaRegistradaDTO> ObtenerEncuestaOnline()
        {
            try
            {
                var rpta = new List<EncuestaRegistradaDTO>();
                var query = @"SELECT
		                            Id,
		                            Nombre	,
		                            Codigo,
		                            Descripcion,
		                            Estado,
		                            UsuarioCreacion,
		                            UsuarioModificacion,
		                            FechaCreacion,
		                            FechaModificacion,
		                            RowVersion,
		                            Version,
		                            IdTipoEncuesta,
		                            IdModalidadCurso
                            FROM
		                            pla.T_EncuestaOnline
                            WHERE	Estado = 1
                            UNION ALL
                            SELECT
	                            Id	,
	                            Titulo,
	                            '',
	                            Instrucciones	,
	                            Estado,
	                            UsuarioCreacion,
	                            UsuarioModificacion,
	                            FechaCreacion,
	                            FechaModificacion,
	                            RowVersion,
	                            Version,
	                            IdTipoEncuesta,
	                            1
                            FROM
	                            gp.T_Examen
                            WHERE
	                            Nombre	= 'Encuesta' AND Estado = 1;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<EncuestaRegistradaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 27/03/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene las versiones actuales de encuestas sincrónicas de vista V_ObtenerVersionEncuestaOnline
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> SolicitudTipoReporte </returns>
        public List<VersionEncuestaSincronicaDTO> ObtenerVersionEncuestaSincronico()
        {
            try
            {
                var rpta = new List<VersionEncuestaSincronicaDTO>();
                var query = @"SELECT *
                    FROM pla.V_ObtenerVersionEncuestaOnline
                    ORDER BY version";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<VersionEncuestaSincronicaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jeremy Pacheco
        /// Fecha: 28/04/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos de cursos,capitulos,sesiones y subsesiones asociados
        /// </summary>
        /// <param name="IdPGeneral"> Id Especifico del Programa </param>
        /// <returns> List<DatoEstructuraCursoAsincronicaDTO> </returns>
        public List<EncuestaEstructuraAsincronicaDTO> ObtenerEncuestaAsincronicaAsignada(int IdPGeneral)
        {
            try
            {
                List<EncuestaEstructuraAsincronicaDTO> rpta = new List<EncuestaEstructuraAsincronicaDTO>();
                var query = @"SELECT 
                                    CE.Id,
                                    CE.IdExamen,
                                    CE.IdPGeneral, 
                                    CE.IdSeccionPW,
                                    CE.Fila,
                                    CE.OrdenCapitulo,
                                    CE.FechaModificacion,
		                            E.Titulo,
		                            CE.EncuestaObligatoria,
		                            CE.EncuestaActiva,
		                            CE.IdTipoPersona,
		                            CE.UbicacionEncuesta
                                    FROM pla.T_ConfigurarExamenesEncuestasEstructura CE
		                            INNER JOIN gp.T_Examen E ON
		                            CE.IdExamen = E.ID AND E.Estado=1
                                    WHERE CE.IdPGeneral= @IdPGeneral AND CE.Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPGeneral });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return rpta = JsonConvert.DeserializeObject<List<EncuestaEstructuraAsincronicaDTO>>(resultado);
                }

                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 09/06/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combo de encuestas asincronica
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public List<ComboDTO> ObtenerEncuestaAsincronica()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = @"SELECT Id,Titulo AS Nombre FROM gp.T_Examen WHERE Nombre = 'Encuesta' AND Estado=1";

                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && resultado!=null )
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);

                    return rpta;
                }

                return rpta;
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Inserta una encuesta a un curso asincronica
        /// </summary>
        /// <param name="encuesta"> Id de la entidad </param>
        /// <returns> true o false </returns>
        public bool InsertarEncuestaSesionProgramaAsincronica(EncuestaAsincronicaDTO encuesta)
        {
            try
            {
                var query = @"pla.SP_ConfigurarExamenesEncuestasEstructura_Insertar";
                var resultado = _dapperRepository.QuerySPDapper(query, encuesta);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 09/06/2025
        /// Versión: 1.0
        /// <summary>
        /// Elimina una encuesta a un curso asincronica
        /// </summary>
        /// <param name="id"> id de la entidad </param>
        /// <param name="usuario"> Autor de la modificacion </param>
        /// <returns> true o false </returns>
        public bool EliminarEncuestaAsincronicaAsignada(int Id, string Usuario)
        {
            try
            {
                var query = @"pla.SP_ConfigurarExamenesEncuestasEstructura_ActualizarEstado";

                var resultado = _dapperRepository.QuerySPDapper(query, new {Id,Usuario});

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jorge Gamero
        /// Fecha: 25/04/2025
        /// Version: 1.0
        /// <summary>
        /// Consulta si existe tipo de encuesta con versión
        /// </summary>
        /// <param name="idTipoEncuesta"> Id del tipo de encuesta </param>
        /// <param name="version"> Número de versión </param>
        /// <returns> bool </returns>
        public bool ExisteEncuestaOnlineTipoEncuestaVersion(int? idTipoEncuesta, int? version)
        {
            try
            {
                var rpta = new List<EncuestaRegistradaDTO>();
                var query = @"SELECT Id
                    FROM pla.T_EncuestaOnline
                    WHERE IdTipoEncuesta = @idTipoEncuesta AND Version = @version AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { idTipoEncuesta, version });
                return !string.IsNullOrEmpty(resultado) && resultado != "null" && resultado != "[]";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PreguntaExamenAsincronicaDTO> InsertarEncuestaAsincronica(EncuestaAsincronicaEntradaDTO encuestaAsincronica)
        {
            try
            {
                
                var query = @"gp.SP_Examen_InsertarEncuestaAsincronica";

                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    Titulo = encuestaAsincronica.Nombre,
                    Instrucciones = encuestaAsincronica.Descripcion,
                    Usuario = encuestaAsincronica.Usuario,
                    IdTipoEncuesta = encuestaAsincronica.IdTipoEncuesta,
                    Version = encuestaAsincronica.Version,

                });

                if (!string.IsNullOrWhiteSpace(resultado))
                {
                    var lista = JsonConvert.DeserializeObject<List<PreguntaExamenAsincronicaDTO>>(resultado);
                    return lista;
                }

                return new List<PreguntaExamenAsincronicaDTO>();
            }
            catch (Exception)
            {
                throw; 
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Inserta una lista preguntas a una encuesta asincronica
        /// </summary>
        /// <param name="encuestaAsincronicaEntradaDTO"> datos de la encuesta asincrona </param>
        /// <returns> true o false </returns>
        public bool InsertarListaPreguntaAsincronica(List<PreguntaExamenAsincronicaDTO> encuestaAsincronica)
        {
            try
            {

                foreach (var pregunta in encuestaAsincronica)
                {

                    InsertarPreguntaEncuestaAsincronica(pregunta);

                }

                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }


        /// Tipo Función: POST
        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Inserta una pregunta a una encuesta asincronica
        /// </summary>
        /// <param name="encuestaAsincronicaEntradaDTO"> datos de la encuesta asincrona </param>
        /// <returns> true o false </returns>
        public bool InsertarPreguntaEncuestaAsincronica(PreguntaExamenAsincronicaDTO encuestaAsincronica)
        {
            try
            {
                var query = "gp.SP_AsignacionPreguntaExamen_Insertar";

                _dapperRepository.QuerySPDapper(query, new { 
                    IdExamen=encuestaAsincronica.IdExamen,
                    IdPregunta= encuestaAsincronica.IdPregunta,
                    Usuario=encuestaAsincronica.Usuario,

                });

                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Elimina una pregunta de una encuesta asincronica
        /// </summary>
        /// <param name="id"> id de la pregunta relacionada con la encuesta asincrona </param>
        /// <param name="usuario"> usuario modificacion </param>
        /// <returns> true o false </returns>
        public bool DeletePreguntaEncuestaAsincronica(int id, string usuario)
        {
            try
            {
                var query = "gp.SP_AsignacionPreguntaExamen_ActualizarEstado";

                _dapperRepository.QuerySPDapper(query, new { id, usuario });

                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Tipo Función: DELETE
        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Elimina una encuesta asincronica
        /// </summary>
        /// <param name="id"> id de la encuesta asincrona </param>
        /// <param name="usuario"> usuario modificacion </param>
        /// <returns> true o false </returns>
        public bool DeleteEncuestaAsincronica(int id, string usuario)
        {
            try
            {
                var query = "gp.SP_Examen_ActualizarEstado";

                _dapperRepository.QuerySPDapper(query, new { id, usuario });

                return true;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Jeremy Pacheco
        /// Fecha: 09/07/2025
        /// Versión: 1.0
        /// <summary>
        /// Actualiza una encuesta asincronica
        /// </summary>
        /// <param name="encuestaAsincronica"> datos de la encuesta asincrona </param>
        /// <returns> true o false </returns>
        public bool UpdateEncuestaAsincronica(EncuestaAsincronicaEntradaDTO encuestaAsincronica)
        {
            try
            {
                var query = "gp.SP_Examen_ActualizarEncuestaAsincronica";

                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    Id = encuestaAsincronica.Id,
                    Titulo = encuestaAsincronica.Nombre,
                    Instrucciones = encuestaAsincronica.Descripcion,
                    Usuario = encuestaAsincronica.Usuario,
                    IdTipoEncuesta = encuestaAsincronica.IdTipoEncuesta,
                    Version = encuestaAsincronica.Version
                });
       
                 return true;

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
