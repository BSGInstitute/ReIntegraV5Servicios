using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CuentaContablePadreRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 01/07/2022
    /// <summary>
    /// Gestión general de T_CuentaContablePadre
    /// </summary>
    public class CuentaContablePadreRepository : GenericRepository<TCuentaContablePadre>, ICuentaContablePadreRepository
    {
        private Mapper _mapper;

        public CuentaContablePadreRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCuentaContablePadre, CuentaContablePadre>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TCuentaContablePadre MapeoEntidad(CuentaContablePadre entidad)
        {
            try
            {
                //crea la entidad padre
                TCuentaContablePadre modelo = _mapper.Map<TCuentaContablePadre>(entidad);

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

        public TCuentaContablePadre Add(CuentaContablePadre entidad)
        {
            try
            {
                var CuentaContablePadre = MapeoEntidad(entidad);
                base.Insert(CuentaContablePadre);
                return CuentaContablePadre;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCuentaContablePadre Update(CuentaContablePadre entidad)
        {
            try
            {
                var CuentaContablePadre = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CuentaContablePadre.RowVersion = entidadExistente.RowVersion;

                base.Update(CuentaContablePadre);
                return CuentaContablePadre;
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


        public IEnumerable<TCuentaContablePadre> Add(IEnumerable<CuentaContablePadre> listadoEntidad)
        {
            try
            {
                List<TCuentaContablePadre> listado = new List<TCuentaContablePadre>();
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

        public IEnumerable<TCuentaContablePadre> Update(IEnumerable<CuentaContablePadre> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCuentaContablePadre> listado = new List<TCuentaContablePadre>();
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
        /// Obtiene todos los registros de T_CuentaContablePadre.
        /// </summary>
        /// <returns> List<CuentaContablePadreDTO> </returns>
        public IEnumerable<CuentaContablePadreDTO> ObtenerCuentaContablePadre()
        {
            try
            {
                List<CuentaContablePadreDTO> rpta = new List<CuentaContablePadreDTO>();
                var query = @"
                    SELECT [Id]
                          ,[CuentaPadre]
                          ,[Descripcion]
                          ,[Estado]
                          ,[UsuarioCreacion]
                          ,[UsuarioModificacion]
                          ,[FechaCreacion]
                          ,[FechaModificacion]
                    FROM [fin].[T_CuentaContablePadre]
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CuentaContablePadreDTO>>(resultado);
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
        /// Obtiene registros de T_CuentaContablePadre para mostrarse en combo.
        /// </summary>
        /// <returns> List<CuentaContablePadreComboDTO> </returns>
        public IEnumerable<CuentaContablePadreComboDTO> ObtenerCombo()
        {
            try
            {
                List<CuentaContablePadreComboDTO> rpta = new List<CuentaContablePadreComboDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    CuentaPadre
                    FROM [fin].[T_CuentaContablePadre]
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<CuentaContablePadreComboDTO>>(resultado);
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
