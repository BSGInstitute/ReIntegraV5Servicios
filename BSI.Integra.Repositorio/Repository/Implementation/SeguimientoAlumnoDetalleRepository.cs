using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: AreaTrabajoRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 11/11/2022
    /// <summary>
    /// Gestión general de T_SeguimientoAlumnoDetalle
    /// </summary>
    public class SeguimientoAlumnoDetalleRepository : GenericRepository<TSeguimientoAlumnoDetalle>, ISeguimientoAlumnoDetalleRepository
    {
        private Mapper _mapper;

        public SeguimientoAlumnoDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSeguimientoAlumnoDetalle, SeguimientoAlumnoDetalle>(MemberList.None).ReverseMap();
            //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
        });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSeguimientoAlumnoDetalle MapeoEntidad(SeguimientoAlumnoDetalle entidad)
        {
            try
            {
                //crea la entidad padre
                TSeguimientoAlumnoDetalle modelo = _mapper.Map<TSeguimientoAlumnoDetalle>(entidad);

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

        public TSeguimientoAlumnoDetalle Add(SeguimientoAlumnoDetalle entidad)
        {
            try
            {
                var SeguimientoAlumnoDetalle = MapeoEntidad(entidad);
                base.Insert(SeguimientoAlumnoDetalle);
                return SeguimientoAlumnoDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSeguimientoAlumnoDetalle Update(SeguimientoAlumnoDetalle entidad)
        {
            try
            {
                var SeguimientoAlumnoDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SeguimientoAlumnoDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(SeguimientoAlumnoDetalle);
                return SeguimientoAlumnoDetalle;
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


        public IEnumerable<TSeguimientoAlumnoDetalle> Add(IEnumerable<SeguimientoAlumnoDetalle> listadoEntidad)
        {
            try
            {
                List<TSeguimientoAlumnoDetalle> listado = new List<TSeguimientoAlumnoDetalle>();
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

        public IEnumerable<TSeguimientoAlumnoDetalle> Update(IEnumerable<SeguimientoAlumnoDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSeguimientoAlumnoDetalle> listado = new List<TSeguimientoAlumnoDetalle>();
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

        public List<SeguimientoAlumnoDetalle> ObtenerPorIdSeguimientoAlumnoCategoria(int idSeguimientoAlumnoCategoria, int idEstadoMatricula)
        {
            try
            {
                var query = @"SELECT Id,
                     IdEstadoMatricula,
                     IdSubEstadoMatricula,
                     IdSeguimientoAlumnoCategoria,
                     Estado,
                     UsuarioCreacion,
                     UsuarioModificacion,
                     FechaCreacion,
                     FechaModificacion,
                     RowVersion,
                     IdMigracion FROM ope.T_SeguimientoAlumnoDetalle WHERE Estado=1 AND IdSeguimientoAlumnoCategoria=@Id AND IdEstadoMatricula=@IdEstadoMatricula";
                var resultado = _dapperRepository.QueryDapper(query, new { Id = idSeguimientoAlumnoCategoria, IdEstadoMatricula = idEstadoMatricula });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<SeguimientoAlumnoDetalle>>(resultado);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SeguimientoAlumnoDetalle> ObtenerPorIdSeguimientoAlumnoCategoria(int idSeguimientoAlumnoCategoria)
        {
            try
            {
                var query = @"SELECT Id,
                     IdEstadoMatricula,
                     IdSubEstadoMatricula,
                     IdSeguimientoAlumnoCategoria,
                     Estado,
                     UsuarioCreacion,
                     UsuarioModificacion,
                     FechaCreacion,
                     FechaModificacion,
                     RowVersion,
                     IdMigracion FROM ope.T_SeguimientoAlumnoDetalle WHERE Estado=1 AND IdSeguimientoAlumnoCategoria=@Id";
                var resultado = _dapperRepository.QueryDapper(query, new { Id = idSeguimientoAlumnoCategoria });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<SeguimientoAlumnoDetalle>>(resultado);
                }
                return new List<SeguimientoAlumnoDetalle>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

