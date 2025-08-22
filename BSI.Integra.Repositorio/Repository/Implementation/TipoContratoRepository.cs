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
    /// Repositorio: TipoContratoRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_TipoContrato
    /// </summary>
    public class TipoContratoRepository : GenericRepository<TTipoContrato>, ITipoContratoRepository
    {
        private Mapper _mapper;

        public TipoContratoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoContrato, TipoContrato>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTipoContrato MapeoEntidad(TipoContrato entidad)
        {
            try
            {
                //crea la entidad padre
                TTipoContrato modelo = _mapper.Map<TTipoContrato>(entidad);

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

        public TTipoContrato Add(TipoContrato entidad)
        {
            try
            {
                var TipoContrato = MapeoEntidad(entidad);
                base.Insert(TipoContrato);
                return TipoContrato;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoContrato Update(TipoContrato entidad)
        {
            try
            {
                var TipoContrato = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoContrato.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoContrato);
                return TipoContrato;
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


        public IEnumerable<TTipoContrato> Add(IEnumerable<TipoContrato> listadoEntidad)
        {
            try
            {
                List<TTipoContrato> listado = new List<TTipoContrato>();
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

        public IEnumerable<TTipoContrato> Update(IEnumerable<TipoContrato> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoContrato> listado = new List<TTipoContrato>();
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
        /// Obtiene registros de T_TipoContrato para mostrarse en combo.
        /// </summary>
        /// <returns> List<TipoContratoComboDTO> </returns>
        public IEnumerable<TipoContratoDTO> ObtenerTipoContrato()
        {
            try
            {
                List<TipoContratoDTO> rpta = new List<TipoContratoDTO>();
                var query = string.Empty;
                query = @"
                    SELECT Id,Nombre,Comentario,IdPais
                    FROM gp.T_TipoContrato
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoContratoDTO>>(resultado);
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
        /// Obtiene registros de T_TipoContrato para mostrarse en combo.
        /// </summary>
        /// <returns> List<TipoContratoComboDTO> </returns>
        public IEnumerable<TipoContratoComboDTO> ObtenerCombo()
        {
            try
            {
                List<TipoContratoComboDTO> rpta = new List<TipoContratoComboDTO>();
                var query = string.Empty;
                query = @"
                    SELECT Id,Nombre,Idpais
                    FROM gp.T_TipoContrato
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoContratoComboDTO>>(resultado);
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
