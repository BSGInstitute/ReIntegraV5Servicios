using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class CompetenciaTecnicaRepository : GenericRepository<TCompetenciaTecnica>, ICompetenciaTecnicaRepository
    {
        private Mapper _mapper;

        public CompetenciaTecnicaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCompetenciaTecnica, CompetenciaTecnica>(MemberList.None).ReverseMap();
                cfg.CreateMap<CompetenciaTecnica, CompetenciaTecnicaDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<CompetenciaTecnica, TCompetenciaTecnica>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCompetenciaTecnica MapeoEntidad(CompetenciaTecnica entidad)
        {
            try
            {
                //crea la entidad padre
                TCompetenciaTecnica modelo = _mapper.Map<TCompetenciaTecnica>(entidad);

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

        public TCompetenciaTecnica Add(CompetenciaTecnica entidad)
        {
            try
            {
                var CompetenciaTecnica = MapeoEntidad(entidad);
                base.Insert(CompetenciaTecnica);
                return CompetenciaTecnica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCompetenciaTecnica Update(CompetenciaTecnica entidad)
        {
            try
            {
                var CompetenciaTecnica = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CompetenciaTecnica.RowVersion = entidadExistente.RowVersion;

                base.Update(CompetenciaTecnica);
                return CompetenciaTecnica;
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


        public IEnumerable<TCompetenciaTecnica> Add(IEnumerable<CompetenciaTecnica> listadoEntidad)
        {
            try
            {
                List<TCompetenciaTecnica> listado = new List<TCompetenciaTecnica>();
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

        public IEnumerable<TCompetenciaTecnica> Update(IEnumerable<CompetenciaTecnica> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCompetenciaTecnica> listado = new List<TCompetenciaTecnica>();
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

        /// Autor:  Marco Jose Villanueva Torres.
        /// Fecha: 04/04/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PreguntaCategoria.
        /// </summary>
        /// <returns> List<CategoriaPregunta> </returns>
        public IEnumerable<CursoComplementarioDTO> Obtener()
        {
            try
            {
                List<CursoComplementarioDTO> rpta = new List<CursoComplementarioDTO>();
                var query = @"
                   SELECT Id, Nombre, IdTipoCursoComplementario, TipoCursoComplementario FROM [gp].[V_TCompetenciaTecnica_ObtenerListaCompetenciaTecnica] WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CursoComplementarioDTO>>(resultado);

                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Marco Jose Villanueva Torres.
        /// Fecha: 04/04/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns >CategoriaPregunta || null</returns>
        public CompetenciaTecnica? ObtenerPorIdCursoComplementario(int idCursoComplementario)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
                        IdTipoCompetenciaTecnica,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_CompetenciaTecnica
                    WHERE Id=@idCursoComplementario AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idCursoComplementario });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<CompetenciaTecnica>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }

        /// Autor: Marco Jose Villanueva Torres.
        /// Fecha: 04/04/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene el registro por el Primary Key
        /// </summary>
        /// <returns >CategoriaPregunta || null</returns>
        public CompetenciaTecnica? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
                        IdTipoCompetenciaTecnica,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_CompetenciaTecnica
                    WHERE Id=@id AND estado=1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<CompetenciaTecnica>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }


    }
}
