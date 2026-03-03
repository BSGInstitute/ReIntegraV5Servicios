using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    public class ProgramaGeneralArgumentoDetalleMotivacionRepository : GenericRepository<TProgramaGeneralArgumentoDetalleMotivacion>, IProgramaGeneralArgumentoDetalleMotivacionRepository
    {
        private Mapper _mapper;
        public ProgramaGeneralArgumentoDetalleMotivacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TProgramaGeneralArgumentoDetalleMotivacion, ProgramaGeneralArgumentoDetalleMotivacion>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TProgramaGeneralArgumentoDetalleMotivacion MapeoEntidad(ProgramaGeneralArgumentoDetalleMotivacion entidad)
        {
            try
            {
                //crea la entidad padre
                TProgramaGeneralArgumentoDetalleMotivacion modelo = _mapper.Map<TProgramaGeneralArgumentoDetalleMotivacion>(entidad);

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

        public TProgramaGeneralArgumentoDetalleMotivacion Add(ProgramaGeneralArgumentoDetalleMotivacion entidad)
        {
            try
            {
                var ProgramaGeneralArgumentoDetalleMotivacion = MapeoEntidad(entidad);
                base.Insert(ProgramaGeneralArgumentoDetalleMotivacion);
                return ProgramaGeneralArgumentoDetalleMotivacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TProgramaGeneralArgumentoDetalleMotivacion Update(ProgramaGeneralArgumentoDetalleMotivacion entidad)
        {
            try
            {
                var ProgramaGeneralArgumento = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ProgramaGeneralArgumento.RowVersion = entidadExistente.RowVersion;

                base.Update(ProgramaGeneralArgumento);
                return ProgramaGeneralArgumento;
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


        public IEnumerable<TProgramaGeneralArgumentoDetalleMotivacion> AddList(IEnumerable<ProgramaGeneralArgumentoDetalleMotivacion> listadoEntidad)
        {
            try
            {
                List<TProgramaGeneralArgumentoDetalleMotivacion> listado = new List<TProgramaGeneralArgumentoDetalleMotivacion>();
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

        public IEnumerable<TProgramaGeneralArgumentoDetalleMotivacion> Update(IEnumerable<ProgramaGeneralArgumentoDetalleMotivacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TProgramaGeneralArgumentoDetalleMotivacion> listado = new List<TProgramaGeneralArgumentoDetalleMotivacion>();
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





        public bool ActualizarProgramaGeneralArgumentoDetalleMotivacion(int id, string usuario, int idMotivacion)
        {
            try
            {
                var query = "pla.SP_TProgramaGeneralArgumentoDetalleMotivacion_Actualizar";
                var parametros = new
                {
                    Id = id,
                    Idmotivacion = idMotivacion,
                    UsuarioModificacion = usuario
                };

                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, parametros);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"#IOSF-MKT-001@Error en ActualizarProgramaGeneralArgumentoDetalleMotivacion() {ex.Message}", ex);
            }
        }
    }
}
