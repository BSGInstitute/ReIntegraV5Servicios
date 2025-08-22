using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio FormularioRespuestaRepository
    /// Autor: Margiory Ramirez Neyra.
    /// Fecha: 14/09/2022
    /// <summary>
    /// Gestión general de T_TipoDato
    /// </summary>
    public class FormularioRespuestaRepository : GenericRepository<TFormularioRespuestum>, IFormularioRespuestaRepository
    {
        private Mapper _mapper;

        public FormularioRespuestaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFormularioRespuestum, FormularioRespuesta>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TFormularioRespuestum MapeoEntidad(FormularioRespuesta entidad)
        {
            try
            {
                //crea la entidad padre
                TFormularioRespuestum modelo = _mapper.Map<TFormularioRespuestum>(entidad);

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

        public TFormularioRespuestum Add(FormularioRespuesta entidad)
        {
            try
            {
                var FormularioRespuesta = MapeoEntidad(entidad);
                base.Insert(FormularioRespuesta);
                return FormularioRespuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFormularioRespuestum Update(FormularioRespuesta entidad)
        {
            try
            {
                var FormularioRespuesta = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                FormularioRespuesta.RowVersion = entidadExistente.RowVersion;

                base.Update(FormularioRespuesta);
                return FormularioRespuesta;
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


        public IEnumerable<TFormularioRespuestum> Add(IEnumerable<FormularioRespuesta> listadoEntidad)
        {
            try
            {
                List<TFormularioRespuestum> listado = new List<TFormularioRespuestum>();
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

        public IEnumerable<TFormularioRespuestum> Update(IEnumerable<FormularioRespuesta> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TFormularioRespuestum> listado = new List<TFormularioRespuestum>();
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

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 14/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_FromularioRespuestum para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();

                var query = "SELECT  Id,Nombre FROM mkt.T_FormularioRespuesta";
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
        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 14/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_FormularioRespuestum
        /// <returns> List<FormularioRespuestaDTO> </returns>
        public IEnumerable<FormularioRespuestaDTO> ObtenerFormularioRespuesta()
        {
            try
            {


                List<FormularioRespuestaDTO> rpta = new List<FormularioRespuestaDTO>();
                var query = @"SELECT Id, Nombre,Codigo, IdPgeneral , ProgramaGeneral
                                 FROM  mkt.T_FormularioRespuesta
                                    WHERE Estado=1 ORDER BY FechaModificacion DESC";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FormularioRespuestaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los filtros  de T_FormularioRespuestum
        /// </summary>
        /// <returns> List<FormularioRespuestaFiltroDTO> </returns>
        public IEnumerable<FormularioRespuestaFiltroDTO> ObtenerFiltroFormularioRespuestum()
        {
            try
            {
                List<FormularioRespuestaFiltroDTO> rpta = new List<FormularioRespuestaFiltroDTO>();
                var query = @"SELECT Id,Nombre From mkt.V_TFormularioRespuesta_Filtro
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<FormularioRespuestaFiltroDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez Neyra.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los  registros de T_PGeneral  para Combo Dato
        /// </summary>
        /// <returns> List<FormularioRespuestaFiltroDTO> </returns>
        public IEnumerable<ProgramaGeneralDatoDTO> ObtenerComboDato()
        {
            try
            {
                List<ProgramaGeneralDatoDTO> rpta = new List<ProgramaGeneralDatoDTO>();

                var query = "SELECT  Id,IdPGeneral, Nombre FROM pla.T_PGeneral ";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ProgramaGeneralDatoDTO>>(resultado);
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
