using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: EmpresaAutorizadaRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 02/07/2022
    /// <summary>
    /// Gestión general de T_EmpresaAutorizadum
    /// </summary>
    public class EmpresaAutorizadaRepository : GenericRepository<TEmpresaAutorizadum>, IEmpresaAutorizadaRepository
    {
        private Mapper _mapper;

        public EmpresaAutorizadaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TEmpresaAutorizadum, EmpresaAutorizada>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TEmpresaAutorizadum MapeoEntidad(EmpresaAutorizada entidad)
        {
            try
            {
                TEmpresaAutorizadum modelo = _mapper.Map<TEmpresaAutorizadum>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TEmpresaAutorizadum Add(EmpresaAutorizada entidad)
        {
            try
            {
                var EmpresaAutorizadum = MapeoEntidad(entidad);
                base.Insert(EmpresaAutorizadum);
                return EmpresaAutorizadum;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TEmpresaAutorizadum Update(EmpresaAutorizada entidad)
        {
            try
            {
                var EmpresaAutorizadum = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                EmpresaAutorizadum.RowVersion = entidadExistente.RowVersion;

                base.Update(EmpresaAutorizadum);
                return EmpresaAutorizadum;
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
        public IEnumerable<TEmpresaAutorizadum> Add(IEnumerable<EmpresaAutorizada> listadoEntidad)
        {
            try
            {
                List<TEmpresaAutorizadum> listado = new List<TEmpresaAutorizadum>();
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
        public IEnumerable<TEmpresaAutorizadum> Update(IEnumerable<EmpresaAutorizada> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TEmpresaAutorizadum> listado = new List<TEmpresaAutorizadum>();
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
        /// Fecha: 02/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_EmpresaAutorizadum.
        /// </summary>
        /// <returns> List<EmpresaAutorizadumDTO> </returns>
        public IEnumerable<EmpresaAutorizadaDTO> Obtener()
        {
            try
            {
                List<EmpresaAutorizadaDTO> rpta = new List<EmpresaAutorizadaDTO>();
                var query = @"select * from fin.V_EmpresaAutorizada";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EmpresaAutorizadaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 02/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_EmpresaAutorizadum para mostrarse en combo.
        /// </summary>
        /// <returns> List<EmpresaAutorizadumComboDTO> </returns>
        public IEnumerable<EmpresaAutorizadaComboDTO> ObtenerCombo()
        {
            try
            {
                var query = @"SELECT Id, RazonSocial, Direccion, Ruc, Central FROM fin.T_EmpresaAutorizada WHERE Estado=1;";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<EmpresaAutorizadaComboDTO>>(resultado)!;
                }
                return new List<EmpresaAutorizadaComboDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 02/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_EmpresaAutorizadum segun el IdPais, para mostrarse en combo.
        /// </summary>
        /// <returns> List<EmpresaAutorizadumComboDTO> </returns>
        public IEnumerable<EmpresaAutorizadaComboDTO> ObtenerComboPorCiudad(int idCiudad)
        {
            try
            {
                var query = @"
                    SELECT EA.Id AS Id, EA.RazonSocial AS RazonSocial 
                    FROM fin.T_EmpresaAutorizada AS EA 
                    INNER JOIN conf.T_Pais AS P ON EA.IdPais=P.Id 
                    INNER JOIN conf.T_Ciudad AS C ON C.IdPais=P.Id 
                    WHERE EA.Estado=1 AND C.Id=@idCiudad 
                    GROUP BY EA.Id, EA.RazonSocial;";
                var resultado = _dapperRepository.QueryDapper(query, new { idCiudad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<EmpresaAutorizadaComboDTO>>(resultado);
                }
                return new List<EmpresaAutorizadaComboDTO>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
