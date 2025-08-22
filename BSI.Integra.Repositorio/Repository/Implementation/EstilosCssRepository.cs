using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: EstilosCssRepository
    /// Autor: Jashin Salazar Taco.
    /// Fecha: 10/06/2022
    /// <summary>
    /// Gestión general de T_EstilosCss
    /// </summary>
    public class EstilosCssRepository : GenericRepository<TEstilo>, IEstilosCssRepository
    {
        private Mapper _mapper;

        public EstilosCssRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEstilo, EstilosCss>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }




        #region Metodos Base
        private TEstilo MapeoEntidad(EstilosCss entidad)
        {
            try
            {
                //crea la entidad padre
                TEstilo modelo = _mapper.Map<TEstilo>(entidad);

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

        public TEstilo Add(EstilosCss entidad)
        {
            try
            {
                var EstilosCss = MapeoEntidad(entidad);
                base.Insert(EstilosCss);
                return EstilosCss;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEstilo Update(EstilosCss entidad)
        {
            try
            {
                var EstilosCss = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                EstilosCss.RowVersion = entidadExistente.RowVersion;

                base.Update(EstilosCss);
                return EstilosCss;
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


        public IEnumerable<TEstilo> Add(IEnumerable<EstilosCss> listadoEntidad)
        {
            try
            {
                List<TEstilo> listado = new List<TEstilo>();
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

        public IEnumerable<TEstilo> Update(IEnumerable<EstilosCss> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEstilo> listado = new List<TEstilo>();
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
        /// Obtiene registros de T_EstilosCss para mostrarse en combo.
        /// </summary>
        /// <returns> List<EstilosCssComboDTO> </returns>
        public IEnumerable<EstiloCombo> ObtenerCombo()
        {
            try
            {
                List<EstiloCombo> rpta = new List<EstiloCombo>();

                var query = "SELECT Id, Nombre, NombreTipo, CodigoCss FROM mkt.T_Estilo WHERE Estado=1";

                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EstiloCombo>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<EstiloCombo> ObtenerComboTagEstilo(int id)
        {
            try
            {
                List<EstiloCombo> rpta = new List<EstiloCombo>();

                var query = "select id,Nombre,NombreTipo from mkt.T_estilo where id not in (select IdEstilo from mkt.T_TagEstilo where IdTag =" + id + " and estado=1) and estado=1";

                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EstiloCombo>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_EstilosCss
        /// </summary>
        /// <returns> List<EstilosCss> </returns>
        public IEnumerable<EstilosCss> ObtenerEstilosCss()
        {
            try
            {
                List<EstilosCss> rpta = new List<EstilosCss>();
                var query = @"SELECT Id, Nombre, CodigoCss, NombreTipo FROM mkt.T_Estilo WHERE Estado=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EstilosCss>>(resultado);
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
