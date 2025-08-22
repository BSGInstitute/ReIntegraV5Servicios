using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: TipoCuentaBancoRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 01/07/2022
    /// <summary>
    /// Gestión general de T_TipoCuentaBanco
    /// </summary>
    public class TipoCuentaBancoRepository : GenericRepository<TTipoCuentaBanco>, ITipoCuentaBancoRepository
    {
        private Mapper _mapper;

        public TipoCuentaBancoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TTipoCuentaBanco, TipoCuentaBanco>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TTipoCuentaBanco MapeoEntidad(TipoCuentaBanco entidad)
        {
            try
            {
                //crea la entidad padre
                TTipoCuentaBanco modelo = _mapper.Map<TTipoCuentaBanco>(entidad);

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

        public TTipoCuentaBanco Add(TipoCuentaBanco entidad)
        {
            try
            {
                var TipoCuentaBanco = MapeoEntidad(entidad);
                base.Insert(TipoCuentaBanco);
                return TipoCuentaBanco;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TTipoCuentaBanco Update(TipoCuentaBanco entidad)
        {
            try
            {
                var TipoCuentaBanco = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                TipoCuentaBanco.RowVersion = entidadExistente.RowVersion;

                base.Update(TipoCuentaBanco);
                return TipoCuentaBanco;
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


        public IEnumerable<TTipoCuentaBanco> Add(IEnumerable<TipoCuentaBanco> listadoEntidad)
        {
            try
            {
                List<TTipoCuentaBanco> listado = new List<TTipoCuentaBanco>();
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

        public IEnumerable<TTipoCuentaBanco> Update(IEnumerable<TipoCuentaBanco> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TTipoCuentaBanco> listado = new List<TTipoCuentaBanco>();
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
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_TipoCuentaBanco.
        /// </summary>
        /// <returns> List<TipoCuentaBancoDTO> </returns>
        public IEnumerable<TipoCuentaBancoDTO> ObtenerTipoCuentaBanco()
        {
            try
            {
                List<TipoCuentaBancoDTO> rpta = new List<TipoCuentaBancoDTO>();
                var query = @"
                    SELECT [Id]
                          ,[Nombre]
                          ,[Estado]
                          ,[UsuarioCreacion]
                          ,[UsuarioModificacion]
                          ,[FechaCreacion]
                          ,[FechaModificacion]
                      FROM [fin].[T_TipoCuentaBanco]
                      where Estado=1 order By ID desc";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoCuentaBancoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 01/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_TipoCuentaBanco para mostrarse en combo.
        /// </summary>
        /// <returns> List<TipoCuentaBancoComboDTO> </returns>
        public IEnumerable<TipoCuentaBancoComboDTO> ObtenerCombo()
        {
            try
            {
                List<TipoCuentaBancoComboDTO> rpta = new List<TipoCuentaBancoComboDTO>();
                var query = @"SELECT Id,Nombre FROM [fin].[T_TipoCuentaBanco] WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoCuentaBancoComboDTO>>(resultado);
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
