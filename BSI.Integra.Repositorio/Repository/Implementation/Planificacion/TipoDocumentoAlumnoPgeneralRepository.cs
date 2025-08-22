
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
    public class TipoDocumentoAlumnoPgeneralRepository : GenericRepository<TTipoDocumentoAlumnoPgeneral>, ITipoDocumentoAlumnoPgeneralRepository
    {
        private Mapper _mapper;

        public TipoDocumentoAlumnoPgeneralRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoDocumentoAlumnoPgeneral, TipoDocumentoAlumnoPgeneral>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TTipoDocumentoAlumnoPgeneral MapeoEntidad(TipoDocumentoAlumnoPgeneral entidad)
        {
            try
            {
                TTipoDocumentoAlumnoPgeneral modelo = _mapper.Map<TTipoDocumentoAlumnoPgeneral>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TTipoDocumentoAlumnoPgeneral Add(TipoDocumentoAlumnoPgeneral entidad)
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
        public TTipoDocumentoAlumnoPgeneral Update(TipoDocumentoAlumnoPgeneral entidad)
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
        public IEnumerable<TTipoDocumentoAlumnoPgeneral> Add(IEnumerable<TipoDocumentoAlumnoPgeneral> listadoEntidad)
        {
            try
            {
                List<TTipoDocumentoAlumnoPgeneral> listado = new List<TTipoDocumentoAlumnoPgeneral>();
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
        public IEnumerable<TTipoDocumentoAlumnoPgeneral> Update(IEnumerable<TipoDocumentoAlumnoPgeneral> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoDocumentoAlumnoPgeneral> listado = new List<TTipoDocumentoAlumnoPgeneral>();
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
                query = @"SELECT Id AS Id, IdPgeneral AS Valor FROM ope.T_TipoDocumentoAlumnoPGeneral WHERE Estado = 1 AND IdTipoDocumentoAlumno = @idTipoDocumentoAlumno";
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
        /// Obtiene todos los ids de T_TipoDocumentoAlumnoPGeneral.
        /// </summary>
        /// <returns> IEnumerable<int> </returns>
        public IEnumerable<int> ObtenerIdsProgramaGeneralPorIdTipoDocumentoAlumno(int idTipoDocumentoAlumno)
        {
            try
            {
                IEnumerable<IntDTO>? rpta = new List<IntDTO>();
                var query = string.Empty;
                query = @"SELECT IdPGeneral AS Valor FROM ope.T_TipoDocumentoAlumnoPGeneral WHERE Estado = 1 AND IdTipoDocumentoAlumno = @idTipoDocumentoAlumno";
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
