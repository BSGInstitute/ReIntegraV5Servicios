using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: CertificadoGeneradoAutomaticoContenidoRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 09/06/2022
    /// <summary>
    /// Gestión general de T_CertificadoGeneradoAutomaticoContenido
    /// </summary>
    public class CertificadoGeneradoAutomaticoContenidoRepository : GenericRepository<TCertificadoGeneradoAutomaticoContenido>, ICertificadoGeneradoAutomaticoContenidoRepository
    {
        private Mapper _mapper;

        public CertificadoGeneradoAutomaticoContenidoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCertificadoGeneradoAutomaticoContenido, CertificadoGeneradoAutomaticoContenido>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TCertificadoGeneradoAutomaticoContenido MapeoEntidad(CertificadoGeneradoAutomaticoContenido entidad)
        {
            try
            {
                //crea la entidad padre
                TCertificadoGeneradoAutomaticoContenido modelo = _mapper.Map<TCertificadoGeneradoAutomaticoContenido>(entidad);

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

        public TCertificadoGeneradoAutomaticoContenido Add(CertificadoGeneradoAutomaticoContenido entidad)
        {
            try
            {
                var CertificadoGeneradoAutomaticoContenido = MapeoEntidad(entidad);
                base.Insert(CertificadoGeneradoAutomaticoContenido);
                return CertificadoGeneradoAutomaticoContenido;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TCertificadoGeneradoAutomaticoContenido Update(CertificadoGeneradoAutomaticoContenido entidad)
        {
            try
            {
                var CertificadoGeneradoAutomaticoContenido = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                CertificadoGeneradoAutomaticoContenido.RowVersion = entidadExistente.RowVersion;

                base.Update(CertificadoGeneradoAutomaticoContenido);
                return CertificadoGeneradoAutomaticoContenido;
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


        public IEnumerable<TCertificadoGeneradoAutomaticoContenido> Add(IEnumerable<CertificadoGeneradoAutomaticoContenido> listadoEntidad)
        {
            try
            {
                List<TCertificadoGeneradoAutomaticoContenido> listado = new List<TCertificadoGeneradoAutomaticoContenido>();
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

        public IEnumerable<TCertificadoGeneradoAutomaticoContenido> Update(IEnumerable<CertificadoGeneradoAutomaticoContenido> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TCertificadoGeneradoAutomaticoContenido> listado = new List<TCertificadoGeneradoAutomaticoContenido>();
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
    
        public List<ContenidoCertificadoSinFondoDTO> ObtenerDatosParaCertificadoFisico(int IdCertificadoGeneradoAutomatico)
        {
            var rpta = new List<ContenidoCertificadoSinFondoDTO>();
            string _query = "ope.SP_ContenidoCertificadoFisico";
            string query = _dapperRepository.QuerySPDapper(_query, new { IdCertificadoGeneradoAutomatico });

            if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
            {
                rpta = JsonConvert.DeserializeObject<List<ContenidoCertificadoSinFondoDTO>>(query);
            }
            return rpta;
        }
    }
}
