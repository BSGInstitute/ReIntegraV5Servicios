using AutoMapper;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// <summary>
    /// Implementación del repositorio para el manejo de fases de gestión de contactos
    /// </summary>
    public class FaseGestionContactoRepository : GenericRepository<TFaseGestionContacto>, IFaseGestionContactoRepository
    {
        private Mapper _mapper;

        public FaseGestionContactoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TFaseGestionContacto, FaseGestionContacto>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        private TFaseGestionContacto MapeoEntidad(FaseGestionContacto entidad)
        {
            try
            {
                TFaseGestionContacto modelo = _mapper.Map<TFaseGestionContacto>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFaseGestionContacto Add(FaseGestionContacto entidad)
        {
            try
            {
                var faseGestionContacto = MapeoEntidad(entidad);
                base.Insert(faseGestionContacto);
                return faseGestionContacto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFaseGestionContacto Update(FaseGestionContacto entidad)
        {
            try
            {
                var faseGestionContacto = MapeoEntidad(entidad);
                base.Update(faseGestionContacto);
                return faseGestionContacto;
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
                var entidad = FirstById(id);
                if (entidad != null)
                {
                    entidad.Estado = false;
                    entidad.UsuarioModificacion = usuario;
                    entidad.FechaModificacion = DateTime.Now;
                    base.Update(entidad);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TFaseGestionContacto> Add(IEnumerable<FaseGestionContacto> listadoEntidad)
        {
            try
            {
                var listadoModelo = new List<TFaseGestionContacto>();
                foreach (var entidad in listadoEntidad)
                {
                    listadoModelo.Add(MapeoEntidad(entidad));
                }
                base.Insert(listadoModelo);
                return listadoModelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TFaseGestionContacto> Update(IEnumerable<FaseGestionContacto> listadoEntidad)
        {
            try
            {
                var listadoModelo = new List<TFaseGestionContacto>();
                foreach (var entidad in listadoEntidad)
                {
                    listadoModelo.Add(MapeoEntidad(entidad));
                }
                base.Update(listadoModelo);
                return listadoModelo;
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
                foreach (var id in listadoIds)
                {
                    Delete(id, usuario);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FaseGestionContactoDTO> ObtenerFaseGestionContacto()
        {
            try
            {
                List<FaseGestionContactoDTO> listaFaseGestionContacto = new List<FaseGestionContactoDTO>();
                var queryText = @"
                    SELECT
                        Id,
                        Codigo,
                        Nombre,
                        Descripcion
                    FROM pla.T_FaseGestionContacto
                    WHERE Estado = 1
                    ORDER BY Nombre ASC";

                var datosFaseGestionContacto = _dapperRepository.QueryDapper(queryText, null);

                if (!string.IsNullOrEmpty(datosFaseGestionContacto) && !datosFaseGestionContacto.Contains("[]") && datosFaseGestionContacto != "null")
                {
                    listaFaseGestionContacto = JsonConvert.DeserializeObject<List<FaseGestionContactoDTO>>(datosFaseGestionContacto);
                }
                return listaFaseGestionContacto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public FaseGestionContacto ObtenerPorId(int id)
        {
            try
            {
                var modelo = FirstById(id);
                if (modelo != null)
                {
                    return _mapper.Map<FaseGestionContacto>(modelo);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FaseGestionContacto> ObtenerPorIds(List<int> ids)
        {
            try
            {
                var modelos = GetBy(x => ids.Contains(x.Id)).ToList();
                return _mapper.Map<List<FaseGestionContacto>>(modelos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
