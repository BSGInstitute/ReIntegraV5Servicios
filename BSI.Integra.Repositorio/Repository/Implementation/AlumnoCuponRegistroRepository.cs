using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: AlumnoCuponRegistroRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 21/06/2022
    /// <summary>
    /// Gestión general de T_AlumnoCuponRegistro
    /// </summary>
    public class AlumnoCuponRegistroRepository : GenericRepository<TAlumnoCuponRegistro>, IAlumnoCuponRegistroRepository
    {
        private Mapper _mapper;

        public AlumnoCuponRegistroRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAlumnoCuponRegistro, AlumnoCuponRegistro>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TAlumnoCuponRegistro MapeoEntidad(AlumnoCuponRegistro entidad)
        {
            try
            {
                //crea la entidad padre
                TAlumnoCuponRegistro modelo = _mapper.Map<TAlumnoCuponRegistro>(entidad);

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

        public TAlumnoCuponRegistro Add(AlumnoCuponRegistro entidad)
        {
            try
            {
                var AlumnoCuponRegistro = MapeoEntidad(entidad);
                base.Insert(AlumnoCuponRegistro);
                return AlumnoCuponRegistro;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAlumnoCuponRegistro Update(AlumnoCuponRegistro entidad)
        {
            try
            {
                var AlumnoCuponRegistro = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AlumnoCuponRegistro.RowVersion = entidadExistente.RowVersion;

                base.Update(AlumnoCuponRegistro);
                return AlumnoCuponRegistro;
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


        public IEnumerable<TAlumnoCuponRegistro> Add(IEnumerable<AlumnoCuponRegistro> listadoEntidad)
        {
            try
            {
                List<TAlumnoCuponRegistro> listado = new List<TAlumnoCuponRegistro>();
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

        public IEnumerable<TAlumnoCuponRegistro> Update(IEnumerable<AlumnoCuponRegistro> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAlumnoCuponRegistro> listado = new List<TAlumnoCuponRegistro>();
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_AlumnoCuponRegistro.
        /// </summary>
        /// <returns> List<AlumnoCuponRegistroDTO> </returns>
        public IEnumerable<AlumnoCuponRegistroDTO> ObtenerAlumnoCuponRegistro()
        {
            try
            {
                List<AlumnoCuponRegistroDTO> rpta = new List<AlumnoCuponRegistroDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdAlumno,
	                    CodigoCupon,
	                    IdPersonal,
	                    AreaVentas,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM mkt.T_AlumnoCuponRegistro
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<AlumnoCuponRegistroDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 21/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_AlumnoCuponRegistro para mostrarse en combo.
        /// </summary>
        /// <returns> List<AlumnoCuponRegistroComboDTO> </returns>
        public IEnumerable<AlumnoCuponRegistroComboDTO> ObtenerCombo()
        {
            try
            {
                List<AlumnoCuponRegistroComboDTO> rpta = new List<AlumnoCuponRegistroComboDTO>();
                var query = @"SELECT Id,CodigoCupon FROM mkt.T_AlumnoCuponRegistro WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<AlumnoCuponRegistroComboDTO>>(resultado);
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
