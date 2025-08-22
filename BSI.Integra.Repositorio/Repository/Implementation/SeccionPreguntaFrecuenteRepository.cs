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
    /// Repositorio: SeccionPreguntaFrecuenterRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_SeccionPreguntaFrecuente
    /// </summary>
    public class SeccionPreguntaFrecuenteRepository : GenericRepository<TSeccionPreguntaFrecuente>, ISeccionPreguntaFrecuenteRepository
    {
        private Mapper _mapper;

        public SeccionPreguntaFrecuenteRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSeccionPreguntaFrecuente, SeccionPreguntaFrecuente>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TSeccionPreguntaFrecuente MapeoEntidad(SeccionPreguntaFrecuente entidad)
        {
            try
            {
                //crea la entidad padre
                TSeccionPreguntaFrecuente modelo = _mapper.Map<TSeccionPreguntaFrecuente>(entidad);

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

        public TSeccionPreguntaFrecuente Add(SeccionPreguntaFrecuente entidad)
        {
            try
            {
                var SeccionPreguntaFrecuente = MapeoEntidad(entidad);
                base.Insert(SeccionPreguntaFrecuente);
                return SeccionPreguntaFrecuente;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSeccionPreguntaFrecuente Update(SeccionPreguntaFrecuente entidad)
        {
            try
            {
                var SeccionPreguntaFrecuente = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SeccionPreguntaFrecuente.RowVersion = entidadExistente.RowVersion;

                base.Update(SeccionPreguntaFrecuente);
                return SeccionPreguntaFrecuente;
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


        public IEnumerable<TSeccionPreguntaFrecuente> Add(IEnumerable<SeccionPreguntaFrecuente> listadoEntidad)
        {
            try
            {
                List<TSeccionPreguntaFrecuente> listado = new List<TSeccionPreguntaFrecuente>();
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

        public IEnumerable<TSeccionPreguntaFrecuente> Update(IEnumerable<SeccionPreguntaFrecuente> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSeccionPreguntaFrecuente> listado = new List<TSeccionPreguntaFrecuente>();
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

        /// Autor: Gilmer Qm
        /// Fecha: 21/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el combo de SeccionPreguntaFrecuente
        /// </summary> 
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                var query = @"SELECT Id,
                                   Nombre
                            FROM pla.T_SeccionPreguntaFrecuente
                            WHERE Estado = 1;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<ComboDTO>>(resultado);
                }
                return new List<ComboDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
