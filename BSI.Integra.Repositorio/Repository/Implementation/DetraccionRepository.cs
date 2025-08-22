using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: DetraccionRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 06/07/2022
    /// <summary>
    /// Gestión general de T_Detraccion
    /// </summary>
    public class DetraccionRepository : GenericRepository<TDetraccion>, IDetraccionRepository
    {
        private Mapper _mapper;

        public DetraccionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDetraccion, Detraccion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        #region Metodos Base
        private TDetraccion MapeoEntidad(Detraccion entidad)
        {
            try
            {
                //crea la entidad padre
                TDetraccion modelo = _mapper.Map<TDetraccion>(entidad);

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

        public TDetraccion Add(Detraccion entidad)
        {
            try
            {
                var Detraccion = MapeoEntidad(entidad);
                base.Insert(Detraccion);
                return Detraccion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDetraccion Update(Detraccion entidad)
        {
            try
            {
                var Detraccion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Detraccion.RowVersion = entidadExistente.RowVersion;

                base.Update(Detraccion);
                return Detraccion;
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


        public IEnumerable<TDetraccion> Add(IEnumerable<Detraccion> listadoEntidad)
        {
            try
            {
                List<TDetraccion> listado = new List<TDetraccion>();
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

        public IEnumerable<TDetraccion> Update(IEnumerable<Detraccion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TDetraccion> listado = new List<TDetraccion>();
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
        /// Fecha: 06/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene [Id, Valor] de las Detracciones existentes en una lista 
        /// para ser mostradas en un ComboBox (utilizado en CRUD 'RendicionRequerimientos')
        /// </summary>
        /// <returns> List<DetraccionComboDTO> </returns>
        public IEnumerable<DetraccionComboDTO> ObtenerCombo()
        {
            try
            {
                List<DetraccionComboDTO> rpta = new List<DetraccionComboDTO>();

                var query = "SELECT Id,CONCAT(valor,'% - ',Nombre) AS Nombre,IdPais,Valor FROM fin.T_Detraccion WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DetraccionComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Griselberto Huaman.
        /// Fecha: 06/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_Detraccion
        /// </summary>
        /// <returns> List<DetraccionDTO> </returns>
        /// <param name="IdsOrigen">Indetificadores de Origen</param>
        /// <param name="FechaEmision">Fecha de emision de Detraccion</param>
        /// <param name="FechaVencimiento">Fecha de vencimiento de Detraccion</param>
        public IEnumerable<ReporteDetraccionDTO> ObtenerReporteDetraccion(string? idSede, string? FechaInicial, string? FechaFinal,int? idProveedor)
        {
            try
            {
                List<ReporteDetraccionDTO> rpta = new List<ReporteDetraccionDTO>();
                var query = @"
                SELECT 
                    Sede, NroDocIdentidad, 
                    NombreProveedor, NumeroComprobante,
                    NombreMoneda,MontoBruto,MontoIgv,MontoNeto,
                    PorcentajeDetraccion,MontoDetraccion,
                    FechaEmision,FechaVencimiento,
                    PeriodoTributario 
                FROM fin.V_ReporteDetraccion 
                where  Estado=1  ";

                if (idSede !=null && idSede != "")
                    query += "  AND IdSede IN (" + idSede + ") ";
                if (FechaInicial!=null && FechaFinal!=null)
                    query += "  AND  FechaEmision >= '" + FechaInicial.ToString() + "' AND FechaEmision<= '" + FechaFinal.ToString() + "'  ";
                if (idProveedor != null)
                    query += "  AND  IdProveedor = " + idProveedor;

                query += " order by FechaEmision desc";

                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ReporteDetraccionDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 06/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene [Id, Valor] de Detraccion segun el pais
        /// </summary>
        /// <returns> List<DetraccionComboDTO> </returns>
        /// <param name="IdPais">Identificador del Pais</param>
        public IEnumerable<DetraccionComboDTO> ObtenerValorDetraccionPorPais(int IdPais)
        {
            try
            {
                List<DetraccionComboDTO> rpta = new List<DetraccionComboDTO>();
                var query = "SELECT  IdDetraccion as Id, ValorDetraccion as Nombre FROM [fin].[V_ObtenerDetraccionAsociadoPais] WHERE IdPais =" + IdPais;
                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<DetraccionComboDTO>>(resultado);
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
