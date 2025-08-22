using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: NuevoAlumnoCongeladoRepository
    /// Autor: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_NuevoAlumnoCongelado
    /// </summary>
    public class NuevoAlumnoCongeladoRepository : GenericRepository<TNuevoAlumnoCongelado>, INuevoAlumnoCongeladoRepository
    {
        private Mapper _mapper;

        public NuevoAlumnoCongeladoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TNuevoAlumnoCongelado, NuevoAlumnoCongelado>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TNuevoAlumnoCongelado MapeoEntidad(NuevoAlumnoCongelado entidad)
        {
            try
            {
                //crea la entidad padre
                TNuevoAlumnoCongelado modelo = _mapper.Map<TNuevoAlumnoCongelado>(entidad);

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TNuevoAlumnoCongelado Add(NuevoAlumnoCongelado entidad)
        {
            try
            {
                var NuevoAlumnoCongelado = MapeoEntidad(entidad);
                base.Insert(NuevoAlumnoCongelado);
                return NuevoAlumnoCongelado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TNuevoAlumnoCongelado Update(NuevoAlumnoCongelado entidad)
        {
            try
            {
                var NuevoAlumnoCongelado = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                NuevoAlumnoCongelado.RowVersion = entidadExistente.RowVersion;

                base.Update(NuevoAlumnoCongelado);
                return NuevoAlumnoCongelado;
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


        public IEnumerable<TNuevoAlumnoCongelado> Add(IEnumerable<NuevoAlumnoCongelado> listadoEntidad)
        {
            try
            {
                List<TNuevoAlumnoCongelado> listado = new List<TNuevoAlumnoCongelado>();
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

        public IEnumerable<TNuevoAlumnoCongelado> Update(IEnumerable<NuevoAlumnoCongelado> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TNuevoAlumnoCongelado> listado = new List<TNuevoAlumnoCongelado>();
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
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_NuevoAlumnoCongelado.
        /// </summary>
        /// <returns> List<NuevoAlumnoCongeladoDTO> </returns>
        public IEnumerable<NuevoAlumnoCongeladoDTO> ObtenerListaNuevoAlumnoCongelado()
        {
            try
            {
                List<NuevoAlumnoCongeladoDTO> respuesta = new List<NuevoAlumnoCongeladoDTO>();
                var _queryfiltro = @"
                        SELECT [Id]
                              ,[IdMatriculaCabecera]
                              ,[CodigoMatricula]
                              ,[NroCuota]
                              ,[NroSubCuota]
                              ,[FechaVencimiento]
                              ,[TotalPagar]
                              ,[Cuota]
                              ,[Saldo]
                              ,[Mora]
                              ,[MontoPagado]
                              ,[Cancelado]
                              ,[TipoCuota]
                              ,[Moneda]
                              ,[FechaPago]
                              ,[FechaCongelamiento]
                              ,[IdPeriodo]
                         FROM [fin].[V_NuevoAlumnoCongelado] order by IdMatriculaCabecera ";
                var resultado = _dapperRepository.QueryDapper(_queryfiltro, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<List<NuevoAlumnoCongeladoDTO>>(resultado);
                }
                return respuesta;
            }
            catch (Exception E)
            {
                throw new Exception(E.Message);
            }

        }

        /// Autor: Griselberto Huaman.
        /// Fecha: 28/06/2022
        /// Version: 1.0
        /// <summary>
        /// Inserta los datos del CSV , cargados en la grilla.
        /// </summary>
        /// <returns> boolean </returns>
        public bool InsertarExcelNuevoAlumnoCongelado(List<NuevoAlumnoCongeladoExcelDTO> datos, DateTime FechaCongelamiento, int IdPeriodo, string User)
        {
            try
            {
                string Json = JsonConvert.SerializeObject(datos);
                var registroDB = _dapperRepository.QuerySPFirstOrDefault("[fin].[SP_InsertarNuevoAlumnoCongelado]", new { Json, FechaCongelamiento, IdPeriodo, User });

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
