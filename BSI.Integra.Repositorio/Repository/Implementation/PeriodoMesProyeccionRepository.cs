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
    /// Repositorio: PeriodoMesProyeccionRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_PeriodoMesProyeccion
    /// </summary>
    public class PeriodoMesProyeccionRepository : GenericRepository<TPeriodoMesProyeccion>, IPeriodoMesProyeccionRepository
    {
        private Mapper _mapper;

        public PeriodoMesProyeccionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPeriodoMesProyeccion, PeriodoMesProyeccion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TPeriodoMesProyeccion MapeoEntidad(PeriodoMesProyeccion entidad)
        {
            try
            {
                //crea la entidad padre
                TPeriodoMesProyeccion modelo = _mapper.Map<TPeriodoMesProyeccion>(entidad);

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

        public TPeriodoMesProyeccion Add(PeriodoMesProyeccion entidad)
        {
            try
            {
                var PeriodoMesProyeccion = MapeoEntidad(entidad);
                base.Insert(PeriodoMesProyeccion);
                return PeriodoMesProyeccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPeriodoMesProyeccion Update(PeriodoMesProyeccion entidad)
        {
            try
            {
                var PeriodoMesProyeccion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PeriodoMesProyeccion.RowVersion = entidadExistente.RowVersion;

                base.Update(PeriodoMesProyeccion);
                return PeriodoMesProyeccion;
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


        public IEnumerable<TPeriodoMesProyeccion> Add(IEnumerable<PeriodoMesProyeccion> listadoEntidad)
        {
            try
            {
                List<TPeriodoMesProyeccion> listado = new List<TPeriodoMesProyeccion>();
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

        public IEnumerable<TPeriodoMesProyeccion> Update(IEnumerable<PeriodoMesProyeccion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPeriodoMesProyeccion> listado = new List<TPeriodoMesProyeccion>();
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

        /// Autor: Margiory Ramirez
        /// Fecha:08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro por Id
        /// </summary>
        /// <returns> PeriodoMesProyeccion </returns>
        public PeriodoMesProyeccion ObtenerPeriodoMesProyeccionById(int id)
        {
            try
            {
                PeriodoMesProyeccion rpta = new PeriodoMesProyeccion();
                var query = @"SELECT * FROM fin.T_PeriodoMesProyeccion
                            WHERE Estado=1 AND Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new {Id = id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<PeriodoMesProyeccion>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Margiory Ramirez
        /// Fecha:08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_PeridoMesProyeccion
        /// </summary>
        /// <returns> List<TipoDatoDTO> </returns>
        public IEnumerable<PeriodoMesProyeccionDTO> ObtenerPeriodoMesProyeccion()
        {
            try
            {
                List<PeriodoMesProyeccionDTO> rpta = new List<PeriodoMesProyeccionDTO>();
                var query = @"SELECT Id,Periodo,Cantidad, Estado, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion
                            FROM fin.T_PeriodoMesProyeccion
                            WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PeriodoMesProyeccionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Margiory Ramirez
        /// Fecha:08/03/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_PeridoMesProyeccion para mostrarse en combo.
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        public IEnumerable<PeriodoMesProyeccionCombo> ObtenerPeriodoMesProyeccionCombo()
        {
            try
            {
                List<PeriodoMesProyeccionCombo> rpta = new List<PeriodoMesProyeccionCombo>();

                var query = "SELECT Id,concat(Cantidad ,' ',Periodo)as Periodo from [fin].[T_PeriodoMesProyeccion] WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PeriodoMesProyeccionCombo>>(resultado)!;
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
