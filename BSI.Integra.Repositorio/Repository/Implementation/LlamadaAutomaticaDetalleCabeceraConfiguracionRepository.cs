using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: LlamadaAutomaticaDetalleCabeceraConfiguracionRepository
    /// Autor: Griselberto Huaman
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_LlamadaAutomaticaDetalleCabeceraConfiguracion
    /// </summary>
    public class LlamadaAutomaticaDetalleCabeceraConfiguracionRepository : GenericRepository<TLlamadaAutomaticaDetalleCabeceraConfiguracion>, ILlamadaAutomaticaDetalleCabeceraConfiguracionRepository
    {
        private Mapper _mapper;

        public LlamadaAutomaticaDetalleCabeceraConfiguracionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TLlamadaAutomaticaDetalleCabeceraConfiguracion, LlamadaAutomaticaDetalleCabeceraConfiguracion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TLlamadaAutomaticaDetalleCabeceraConfiguracion MapeoEntidad(LlamadaAutomaticaDetalleCabeceraConfiguracion entidad)
        {
            try
            {
                //crea la entidad padre
                TLlamadaAutomaticaDetalleCabeceraConfiguracion modelo = _mapper.Map<TLlamadaAutomaticaDetalleCabeceraConfiguracion>(entidad);

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

        public TLlamadaAutomaticaDetalleCabeceraConfiguracion Add(LlamadaAutomaticaDetalleCabeceraConfiguracion entidad)
        {
            try
            {
                var LlamadaAutomaticaDetalleCabeceraConfiguracion = MapeoEntidad(entidad);
                base.Insert(LlamadaAutomaticaDetalleCabeceraConfiguracion);
                return LlamadaAutomaticaDetalleCabeceraConfiguracion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TLlamadaAutomaticaDetalleCabeceraConfiguracion Update(LlamadaAutomaticaDetalleCabeceraConfiguracion entidad)
        {
            try
            {
                var LlamadaAutomaticaDetalleCabeceraConfiguracion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                LlamadaAutomaticaDetalleCabeceraConfiguracion.RowVersion = entidadExistente.RowVersion;

                base.Update(LlamadaAutomaticaDetalleCabeceraConfiguracion);
                return LlamadaAutomaticaDetalleCabeceraConfiguracion;
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


        public IEnumerable<TLlamadaAutomaticaDetalleCabeceraConfiguracion> Add(IEnumerable<LlamadaAutomaticaDetalleCabeceraConfiguracion> listadoEntidad)
        {
            try
            {
                List<TLlamadaAutomaticaDetalleCabeceraConfiguracion> listado = new List<TLlamadaAutomaticaDetalleCabeceraConfiguracion>();
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

        public IEnumerable<TLlamadaAutomaticaDetalleCabeceraConfiguracion> Update(IEnumerable<LlamadaAutomaticaDetalleCabeceraConfiguracion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TLlamadaAutomaticaDetalleCabeceraConfiguracion> listado = new List<TLlamadaAutomaticaDetalleCabeceraConfiguracion>();
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
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el registro de T_RecordatorioWebinarIvr de Id enviado.
        /// </summary>
        /// <returns> RecordatorioWebinarIvr </returns>
        public LlamadaAutomaticaDetalleCabeceraConfiguracion ObtenerLlamadaAutomaticaDetalleCabeceraConfiguracionPorId(int Id)
        {
            LlamadaAutomaticaDetalleCabeceraConfiguracion rpta = new LlamadaAutomaticaDetalleCabeceraConfiguracion();
            try
            {

                var query = @"
                   SELECT [Id]
                          ,[IdMatriculaCabecera]
                          ,[IdCabeceraConfiguracionLlamadaAutomatica]
                          ,[IntentoMaximo]
                          ,[Intento]
                          ,[Concluido]
                          ,[Ejecutado]
                          ,[UsuarioCreacion]
                          ,[UsuarioModificacion]
                          ,[Estado]
                          ,[FechaCreacion]
                          ,[FechaModificacion]
                          ,[RowVersion]
                          ,[IdMigracion]
                    FROM [ope].[T_LlamadaAutomaticaDetalleCabeceraConfiguracion]
                    WHERE Estado=1 and Id=@Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = Id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<LlamadaAutomaticaDetalleCabeceraConfiguracion>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
