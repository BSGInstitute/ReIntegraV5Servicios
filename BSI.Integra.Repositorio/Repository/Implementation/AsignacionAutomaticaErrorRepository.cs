using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
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
    /// Repositorio: AsignacionAutomaticaErrorRepository
    /// Autor: Max Mantilla Rodríguez.
    /// Fecha: 03/11/2022
    /// <summary>
    /// Gestión general de T_AsignacionAutomaticaError
    /// </summary>
    public class AsignacionAutomaticaErrorRepository : GenericRepository<TAsignacionAutomaticaError>, IAsignacionAutomaticaErrorRepository
    {
        private Mapper _mapper;

        public AsignacionAutomaticaErrorRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAsignacionAutomaticaError, AsignacionAutomaticaError>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TAsignacionAutomaticaError MapeoEntidad(AsignacionAutomaticaError entidad)
        {
            try
            {
                //crea la entidad padre
                TAsignacionAutomaticaError modelo = _mapper.Map<TAsignacionAutomaticaError>(entidad);

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

        public TAsignacionAutomaticaError Add(AsignacionAutomaticaError entidad)
        {
            try
            {
                var AsignacionAutomaticaError = MapeoEntidad(entidad);
                base.Insert(AsignacionAutomaticaError);
                return AsignacionAutomaticaError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAsignacionAutomaticaError Update(AsignacionAutomaticaError entidad)
        {
            try
            {
                var AsignacionAutomaticaError = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                AsignacionAutomaticaError.RowVersion = entidadExistente.RowVersion;

                base.Update(AsignacionAutomaticaError);
                return AsignacionAutomaticaError;
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


        public IEnumerable<TAsignacionAutomaticaError> Add(IEnumerable<AsignacionAutomaticaError> listadoEntidad)
        {
            try
            {
                List<TAsignacionAutomaticaError> listado = new List<TAsignacionAutomaticaError>();
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

        public IEnumerable<TAsignacionAutomaticaError> Update(IEnumerable<AsignacionAutomaticaError> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAsignacionAutomaticaError> listado = new List<TAsignacionAutomaticaError>();
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
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        ///  Obtiene la lista de errores ségún el idAsignaciónAutomática
        /// </summary>
        /// <param name="idAsignacionAutomatica">Id de la Asignación Automática </param>
        /// <returns> List<AsignacionAutomaticaErrorDTO> </returns>
        public List<AsignacionAutomaticaErrorDTO> ObtenerError(int idAsignacionAutomatica)
        {
            try
            {
                string _queryError = "Select Id,Campo,Descripcion,IdContacto,IdAsignacionAutomatica,IdAsignacionAutomaticaTipoError From com.V_TAsignacionAutomaticaError_ObtenerError where Estado=1 and IdAsignacionAutomaticaTipoError=1 and IdAsignacionAutomatica = @IdAsignacionAutomatica  ";
                var queryError = _dapperRepository.QueryDapper(_queryError, new { IdAsignacionAutomatica = idAsignacionAutomatica });
                return JsonConvert.DeserializeObject<List<AsignacionAutomaticaErrorDTO>>(queryError);


            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Max Mantilla Rodríguez.
        /// Fecha: 03/22/22
        /// Version: 1.0
        /// <summary>
        ///  Obtiene la lista de valores del error ségún su Id
        /// </summary>
        /// <param name="Id">Id del Error </param>
        /// <returns> List<ValorIntDTO> </returns>
        public List<ValorIntDTO> ObtenerErrorAsignacionAutomatica(int Id)
        {
            try
            {
                List<ValorIntDTO> rpta = new List<ValorIntDTO>();
                string _queryError = "Select Id From mkt.T_AsignacionAutomaticaError where Estado=1 and IdAsignacionAutomatica=@Id";
                var resultado = _dapperRepository.QueryDapper(_queryError, new { Id = Id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Equals("[]") && !resultado.Equals("null"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ValorIntDTO>>(resultado);
                }
                return rpta;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
