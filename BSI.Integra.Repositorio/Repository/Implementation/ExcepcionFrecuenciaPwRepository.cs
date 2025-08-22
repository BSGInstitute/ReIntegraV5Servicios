using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ExcepcionFrecuenciaPwRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 11/08/2022
    /// <summary>
    /// Gestión general de T_ExcepcionFrecuenciaPw
    /// </summary>
    public class ExcepcionFrecuenciaPwRepository : GenericRepository<TExcepcionFrecuenciaPw>, IExcepcionFrecuenciaPwRepository
    {
        private Mapper _mapper;

        public ExcepcionFrecuenciaPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TExcepcionFrecuenciaPw, ExcepcionFrecuenciaPw>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TExcepcionFrecuenciaPw MapeoEntidad(ExcepcionFrecuenciaPw entidad)
        {
            try
            {
                //crea la entidad padre
                TExcepcionFrecuenciaPw modelo = _mapper.Map<TExcepcionFrecuenciaPw>(entidad);

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

        public TExcepcionFrecuenciaPw Add(ExcepcionFrecuenciaPw entidad)
        {
            try
            {
                var ExcepcionFrecuenciaPw = MapeoEntidad(entidad);
                base.Insert(ExcepcionFrecuenciaPw);
                return ExcepcionFrecuenciaPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TExcepcionFrecuenciaPw Update(ExcepcionFrecuenciaPw entidad)
        {
            try
            {
                var ExcepcionFrecuenciaPw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ExcepcionFrecuenciaPw.RowVersion = entidadExistente.RowVersion;

                base.Update(ExcepcionFrecuenciaPw);
                return ExcepcionFrecuenciaPw;
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


        public IEnumerable<TExcepcionFrecuenciaPw> Add(IEnumerable<ExcepcionFrecuenciaPw> listadoEntidad)
        {
            try
            {
                List<TExcepcionFrecuenciaPw> listado = new List<TExcepcionFrecuenciaPw>();
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

        public IEnumerable<TExcepcionFrecuenciaPw> Update(IEnumerable<ExcepcionFrecuenciaPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TExcepcionFrecuenciaPw> listado = new List<TExcepcionFrecuenciaPw>();
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
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ExcepcionFrecuenciaPw.
        /// </summary>
        /// <returns> List<ExcepcionFrecuenciaPwDTO> </returns>
        public IEnumerable<ExcepcionFrecuenciaPwDTO> ObtenerExcepcionFrecuenciaPw()
        {
            try
            {
                List<ExcepcionFrecuenciaPwDTO> rpta = new List<ExcepcionFrecuenciaPwDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdPEspecifico,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM pla.T_ExcepcionFrecuencia_PW
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ExcepcionFrecuenciaPwDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda las excepciones de programas generales.
        /// </summary>
        /// <returns> Lista Excepciones Programa generales: List<ExcepcionFrecuenciaPGeneralDTO></returns> 
        public IEnumerable<ExcepcionFrecuenciaPGeneralDTO> ObtenerTodoProgramaGeneral()
        {
            try
            {
                List<ExcepcionFrecuenciaPGeneralDTO> rpta = new List<ExcepcionFrecuenciaPGeneralDTO>();
                var query = @"SELECT Id, IdPEspecifico FROM pla.V_TExcepcionFrecuenciaPW_ObtenerExcepcionesPEspecifico WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ExcepcionFrecuenciaPGeneralDTO>>(resultado);
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
