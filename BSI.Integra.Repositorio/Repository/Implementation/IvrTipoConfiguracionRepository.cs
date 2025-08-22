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
    /// Repositorio: IvrTipoConfiguracionRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_IvrTipoConfiguracion
    /// </summary>
    public class IvrTipoConfiguracionRepository : GenericRepository<TIvrTipoConfiguracion>, IIvrTipoConfiguracionRepository
    {
        private Mapper _mapper;

        public IvrTipoConfiguracionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TIvrTipoConfiguracion, IvrTipoConfiguracion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TIvrTipoConfiguracion MapeoEntidad(IvrTipoConfiguracion entidad)
        {
            try
            {
                //crea la entidad padre
                TIvrTipoConfiguracion modelo = _mapper.Map<TIvrTipoConfiguracion>(entidad);

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

        public TIvrTipoConfiguracion Add(IvrTipoConfiguracion entidad)
        {
            try
            {
                var IvrTipoConfiguracion = MapeoEntidad(entidad);
                base.Insert(IvrTipoConfiguracion);
                return IvrTipoConfiguracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TIvrTipoConfiguracion Update(IvrTipoConfiguracion entidad)
        {
            try
            {
                var IvrTipoConfiguracion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                IvrTipoConfiguracion.RowVersion = entidadExistente.RowVersion;

                base.Update(IvrTipoConfiguracion);
                return IvrTipoConfiguracion;
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


        public IEnumerable<TIvrTipoConfiguracion> Add(IEnumerable<IvrTipoConfiguracion> listadoEntidad)
        {
            try
            {
                List<TIvrTipoConfiguracion> listado = new List<TIvrTipoConfiguracion>();
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

        public IEnumerable<TIvrTipoConfiguracion> Update(IEnumerable<IvrTipoConfiguracion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TIvrTipoConfiguracion> listado = new List<TIvrTipoConfiguracion>();
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
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_IvrTipoConfiguracion.
        /// </summary>
        /// <returns> List<IvrTipoConfiguracionDTO> </returns>
        public IEnumerable<IvrTipoConfiguracionDTO> ObtenerIvrTipoConfiguracion()
        {
            try
            {
                List<IvrTipoConfiguracionDTO> rpta = new List<IvrTipoConfiguracionDTO>();
                var query = @"
                   SELECT  [Id]
                          ,[Nombre]
                          ,[Descripcion]
                    FROM [ope].[T_IvrTipoConfiguracion]
                    WHERE [Estado]=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<IvrTipoConfiguracionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_IvrTipoConfiguracion para mostrarse en combo.
        /// </summary>
        /// <returns> List<IvrTipoConfiguracionComboDTO> </returns>
        public IEnumerable<ComboDTO> ObtenerCombo()
        {
            try
            {
                List<ComboDTO> rpta = new List<ComboDTO>();
                var query = string.Empty;
                query = @"
                    SELECT [Id]
                          ,[Nombre]
                    FROM [ope].[T_IvrTipoConfiguracion]
                    WHERE [Estado]=1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ComboDTO>>(resultado);
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
