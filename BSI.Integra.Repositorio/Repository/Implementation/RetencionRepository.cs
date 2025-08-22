using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: RetencionRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_Retencion
    /// </summary>
    public class RetencionRepository : GenericRepository<TRetencion>, IRetencionRepository
    {
        private Mapper _mapper;

        public RetencionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TRetencion, Retencion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TRetencion MapeoEntidad(Retencion entidad)
        {
            try
            {
                //crea la entidad padre
                TRetencion modelo = _mapper.Map<TRetencion>(entidad);

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

        public TRetencion Add(Retencion entidad)
        {
            try
            {
                var Retencion = MapeoEntidad(entidad);
                base.Insert(Retencion);
                return Retencion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TRetencion Update(Retencion entidad)
        {
            try
            {
                var Retencion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Retencion.RowVersion = entidadExistente.RowVersion;

                base.Update(Retencion);
                return Retencion;
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


        public IEnumerable<TRetencion> Add(IEnumerable<Retencion> listadoEntidad)
        {
            try
            {
                List<TRetencion> listado = new List<TRetencion>();
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

        public IEnumerable<TRetencion> Update(IEnumerable<Retencion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TRetencion> listado = new List<TRetencion>();
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
        /// Obtiene todos los registros de T_Retencion.
        /// </summary>
        /// <returns> List<RetencionDTO> </returns>
        public IEnumerable<RetencionDTO> ObtenerRetencion()
        {
            try
            {
                List<RetencionDTO> rpta = new List<RetencionDTO>();
                var query = @"
                    SELECT  
                        R.Id,R.Nombre,
                        R.Descripcion,
                        IdPais,
                        R.Valor,
                        NombrePais AS Pais,
                        R.UsuarioCreacion,
                        R.FechaCreacion,
                        R.FechaModificacion
                        FROM fin.T_Retencion AS R 
                        INNER JOIN conf.T_Pais AS p 
                        ON P.id=R.IdPais 
                    WHERE R.Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<RetencionDTO>>(resultado);
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
        /// Obtiene registros de T_Retencion para mostrarse en combo.
        /// </summary>
        /// <returns> List<RetencionComboDTO> </returns>
        public IEnumerable<RetencionComboDTO> ObtenerCombo()
        {
            try
            {
                List<RetencionComboDTO> rpta = new List<RetencionComboDTO>();
                var query = string.Empty;
                query = @"
                    SELECT Id,CONCAT(valor,'% - ',Nombre) AS Nombre,IdPais,Valor
                    FROM fin.T_Retencion
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<RetencionComboDTO>>(resultado);
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
        /// Obtiene [Id, Valor] de Retencion segun el pais
        /// </summary>
        /// <param name=”idDePais”>Identificador de la tabla T_Pais</param>
        /// <returns> List<RetencionComboDTO> </returns>
        public List<RetencionComboDTO> ObtenerValorRetencionPorPais(int idDePais)
        {
            try
            {
                List<RetencionComboDTO> rpt = new List<RetencionComboDTO>();
                var query = string.Empty;
                query = @"
                    SELECT  
                        IdRetencion as Id, 
                        ValorRetencion as Valor 
                    FROM [fin].[V_ObtenerRetencionAsociadoPais] 
                    WHERE IdPais =" + idDePais;
                var respuesta = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    rpt = JsonConvert.DeserializeObject<List<RetencionComboDTO>>(respuesta);
                }
                return rpt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
