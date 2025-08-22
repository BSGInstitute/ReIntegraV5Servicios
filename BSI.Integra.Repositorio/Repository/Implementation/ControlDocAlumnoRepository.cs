using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ControlDocAlumnoRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_ControlDocAlumno
    /// </summary>
    public class ControlDocAlumnoRepository : GenericRepository<TControlDocAlumno>, IControlDocAlumnoRepository
    {
        private Mapper _mapper;

        public ControlDocAlumnoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TControlDocAlumno, ControlDocAlumno>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TControlDocAlumno MapeoEntidad(ControlDocAlumno entidad)
        {
            try
            {
                //crea la entidad padre
                TControlDocAlumno modelo = _mapper.Map<TControlDocAlumno>(entidad);

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

        public TControlDocAlumno Add(ControlDocAlumno entidad)
        {
            try
            {
                var ControlDocAlumno = MapeoEntidad(entidad);
                base.Insert(ControlDocAlumno);
                return ControlDocAlumno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TControlDocAlumno Update(ControlDocAlumno entidad)
        {
            try
            {
                var ControlDocAlumno = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ControlDocAlumno.RowVersion = entidadExistente.RowVersion;

                base.Update(ControlDocAlumno);
                return ControlDocAlumno;
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


        public IEnumerable<TControlDocAlumno> Add(IEnumerable<ControlDocAlumno> listadoEntidad)
        {
            try
            {
                List<TControlDocAlumno> listado = new List<TControlDocAlumno>();
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

        public IEnumerable<TControlDocAlumno> Update(IEnumerable<ControlDocAlumno> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TControlDocAlumno> listado = new List<TControlDocAlumno>();
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

        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obteniene el DTO por idMatriculaCabecera
        /// </summary>
        /// <param name="dtoReporte"></param>
        /// <returns></returns>
        public ControlDocAlumno ObtenerPorIdMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                var respuesta = new ControlDocAlumno();
                var query = @"SELECT Id,
                                IdMatriculaCabecera, 
                                IdCriterioCalificacion,
                                QuienEntrego,
                                FechaEntregaDocumento,
                                Observaciones,
                                ComisionableEditable,
                                MontoComisionable,
                                ObservacionesComisionable,
                                PagadoComisionable,
                                Estado,UsuarioCreacion,
                                UsuarioModificacion, 
                                FechaCreacion,
                                FechaModificacion, 
                                RowVersion,
                                IdMigracion,
                                IdMatriculaObservacion 
                            FROM fin.T_ControlDocAlumno 
                            WHERE Estado = 1 AND IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<ControlDocAlumno>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
