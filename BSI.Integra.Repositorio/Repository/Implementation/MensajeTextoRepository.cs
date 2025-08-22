using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: MensajeTextoRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 29/09/2022
    /// <summary>
    /// Gestión general de T_MensajeTexto
    /// </summary>
    public class MensajeTextoRepository : GenericRepository<TMensajeTexto>, IMensajeTextoRepository
    {
        private Mapper _mapper;

        public MensajeTextoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMensajeTexto, MensajeTexto>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TMensajeTexto MapeoEntidad(MensajeTexto entidad)
        {
            try
            {
                //crea la entidad padre
                TMensajeTexto modelo = _mapper.Map<TMensajeTexto>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMensajeTexto Add(MensajeTexto entidad)
        {
            try
            {
                var agregarEntidad = MapeoEntidad(entidad);
                base.Insert(agregarEntidad);
                return agregarEntidad;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TMensajeTexto Update(MensajeTexto entidad)
        {
            try
            {
                var actualizarEntidad = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                actualizarEntidad.RowVersion = entidadExistente.RowVersion;

                base.Update(actualizarEntidad);
                return actualizarEntidad;
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
        public IEnumerable<TMensajeTexto> Add(IEnumerable<MensajeTexto> listadoEntidad)
        {
            try
            {
                List<TMensajeTexto> listado = new List<TMensajeTexto>();
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

        public IEnumerable<TMensajeTexto> Update(IEnumerable<MensajeTexto> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMensajeTexto> listado = new List<TMensajeTexto>();
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
        /// Autor: Gilmer Quispe.
        /// Fecha: 29/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtener Codigo Matricula segun el Id de una Oportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> MatriculaCabeceraCodigoFechaDTO </returns>
        public MatriculaCabeceraCodigoFechaDTO ObtenerCodigoMatriculaPorOportunidad(int idOportunidad)
        {
            try
            {
                string _query = @"SELECT Id, CodigoMatricula 
                                FROM com.V_TMontoPagoCronograma_CodigoMatricula 
                                WHERE Estado=1 AND IdOportunidad=@IdOportunidad";
                var matriculaDB = _dapperRepository.FirstOrDefault(_query, new { IdOportunidad = idOportunidad });
                var datosMatricula = JsonConvert.DeserializeObject<MatriculaCabeceraCodigoFechaDTO>(matriculaDB);

                if (datosMatricula == null || datosMatricula.CodigoMatricula.Contains("[]"))
                {
                    throw new Exception("No Existe un Codigo Matricula para la Oportunidad con Identificador " + idOportunidad);
                }
                else
                {
                    return datosMatricula;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 29/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Accesos del Portal para mandar por mensaje
        /// </summary>
        /// <param name="IdAlumno"> Id del alumno </param>
        /// <returns> AccesoPortalWebDTO </returns>
        public AccesoPortalWebDTO ObtenerAccesoPorIdAlumno(int idAlumno)
        {
            try
            {
                string _query = "SELECT UserName,Clave FROM com.V_AccesosPortalWeb_MensajeTexto WHERE Estado=1 AND Id=@IdAlumno";
                var resultado = _dapperRepository.FirstOrDefault(_query, new { idAlumno });

                if (!resultado.Equals("null"))
                {
                    var Accesos = JsonConvert.DeserializeObject<AccesoPortalWebDTO>(resultado);
                    return Accesos;
                }
                else
                {
                    throw new Exception("No se crearon los accesos para este Alumno " + idAlumno);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
