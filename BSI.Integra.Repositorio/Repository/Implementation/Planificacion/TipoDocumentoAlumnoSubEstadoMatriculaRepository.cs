using AutoMapper;

using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;

using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class TipoDocumentoAlumnoSubEstadoMatriculaRepository : GenericRepository<TTipoDocumentoAlumnoSubEstadoMatricula>, ITipoDocumentoAlumnoSubEstadoMatriculaRepository
    {
        private Mapper _mapper;

        public TipoDocumentoAlumnoSubEstadoMatriculaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoDocumentoAlumnoSubEstadoMatricula, TipoDocumentoAlumnoSubEstadoMatricula>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TTipoDocumentoAlumnoSubEstadoMatricula MapeoEntidad(TipoDocumentoAlumnoSubEstadoMatricula entidad)
        {
            try
            {
                TTipoDocumentoAlumnoSubEstadoMatricula modelo = _mapper.Map<TTipoDocumentoAlumnoSubEstadoMatricula>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTipoDocumentoAlumnoSubEstadoMatricula Add(TipoDocumentoAlumnoSubEstadoMatricula entidad)
        {
            try
            {
                var TipoDocumentoAlumno = MapeoEntidad(entidad);
                base.Insert(TipoDocumentoAlumno);
                return TipoDocumentoAlumno;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTipoDocumentoAlumnoSubEstadoMatricula Update(TipoDocumentoAlumnoSubEstadoMatricula entidad)
        {
            try
            {
                var TipoDocumentoAlumno = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoDocumentoAlumno.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoDocumentoAlumno);
                return TipoDocumentoAlumno;
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
        public IEnumerable<TTipoDocumentoAlumnoSubEstadoMatricula> Add(IEnumerable<TipoDocumentoAlumnoSubEstadoMatricula> listadoEntidad)
        {
            try
            {
                List<TTipoDocumentoAlumnoSubEstadoMatricula> listado = new List<TTipoDocumentoAlumnoSubEstadoMatricula>();
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
        public IEnumerable<TTipoDocumentoAlumnoSubEstadoMatricula> Update(IEnumerable<TipoDocumentoAlumnoSubEstadoMatricula> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoDocumentoAlumnoSubEstadoMatricula> listado = new List<TTipoDocumentoAlumnoSubEstadoMatricula>();
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
        /// Autor: Christian Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoDocumentoAlumnoModalidadCurso.
        /// </summary>
        /// <returns> IEnumerable<int> </returns>
        public IEnumerable<ValorDTO> ObtenerIdsPorIdTipoDocumentoAlumno(int idTipoDocumentoAlumno)
        {
            try
            {
                IEnumerable<ValorDTO> rpta = new List<ValorDTO>();
                string? query = string.Empty;
                query = @"SELECT Id AS Id, IdSubEstadoMatricula AS Valor FROM ope.T_TipoDocumentoAlumnoSubEstadoMatricula WHERE Estado = 1 AND IdTipoDocumentoAlumno = @idTipoDocumentoAlumno";
                var resultado = _dapperRepository.QueryDapper(query, new { idTipoDocumentoAlumno });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<ValorDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Christian Quispe Mamani
        /// Fecha: 16/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los ids de T_TipoDocumentoAlumnoSubEstadoMatricula.
        /// </summary>
        /// <returns> IEnumerable<int> </returns>
        public IEnumerable<int> ObtenerIdsSubEstadoMatriculaPorIdTipoDocumentoAlumno(int idTipoDocumentoAlumno)
        {
            try
            {
                IEnumerable<IntDTO> rpta = new List<IntDTO>();
                var query = string.Empty;
                query = @"SELECT IdSubEstadoMatricula AS Valor FROM ope.T_TipoDocumentoAlumnoSubEstadoMatricula WHERE Estado = 1 AND IdTipoDocumentoAlumno = @idTipoDocumentoAlumno";
                var resultado = _dapperRepository.QueryDapper(query, new { idTipoDocumentoAlumno });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<IntDTO>>(resultado)!;
                    return rpta.Select(x => x.Valor.GetValueOrDefault());
                }
                return new List<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
