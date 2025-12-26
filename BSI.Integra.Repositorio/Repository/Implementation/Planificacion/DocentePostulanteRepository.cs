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
    /// Autor: Lolo Zaa
    /// Fecha: 26/12/2025
    /// <summary>
    /// Implementación del repositorio para el manejo de docentes postulantes
    /// </summary>
    public class DocentePostulanteRepository : GenericRepository<TDocentePostulante>, IDocentePostulanteRepository
    {
        private Mapper _mapper;

        public DocentePostulanteRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDocentePostulante, DocentePostulante>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        private TDocentePostulante MapeoEntidad(DocentePostulante entidad)
        {
            try
            {
                TDocentePostulante modelo = _mapper.Map<TDocentePostulante>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDocentePostulante Add(DocentePostulante entidad)
        {
            try
            {
                var docentePostulante = MapeoEntidad(entidad);
                base.Insert(docentePostulante);
                return docentePostulante;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TDocentePostulante Update(DocentePostulante entidad)
        {
            try
            {
                var docentePostulante = MapeoEntidad(entidad);
                base.Update(docentePostulante);
                return docentePostulante;
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

        public IEnumerable<TDocentePostulante> Add(IEnumerable<DocentePostulante> listadoEntidad)
        {
            try
            {
                var listadoModelo = new List<TDocentePostulante>();
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

        public IEnumerable<TDocentePostulante> Update(IEnumerable<DocentePostulante> listadoEntidad)
        {
            try
            {
                var listadoModelo = new List<TDocentePostulante>();
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

        public List<DocentePostulanteDTO> ObtenerDocentePostulante()
        {
            try
            {
                List<DocentePostulanteDTO> listaDocentePostulante = new List<DocentePostulanteDTO>();
                var queryText = @"
                    SELECT
                        DP.Id,
                        DP.Nombre1,
                        DP.Nombre2,
                        DP.ApellidoPaterno,
                        DP.ApellidoMaterno,
                        DP.NumeroDocumento,
                        DP.FechaNacimiento,
                        DP.Telefono,
                        DP.Celular,
                        DP.Correo,
                        DP.IdCiudad,
                        C.Nombre AS NombreCiudad,
                        CONCAT(DP.Nombre1, ' ', ISNULL(DP.Nombre2, ''), ' ', DP.ApellidoPaterno, ' ', ISNULL(DP.ApellidoMaterno, '')) AS NombreCompleto
                    FROM pla.T_DocentePostulante AS DP
                    LEFT JOIN conf.T_Ciudad AS C ON C.Id = DP.IdCiudad
                    WHERE DP.Estado = 1
                    ORDER BY DP.Id DESC";

                var datosDocentePostulante = _dapperRepository.QueryDapper(queryText, null);

                if (!string.IsNullOrEmpty(datosDocentePostulante) && !datosDocentePostulante.Contains("[]") && datosDocentePostulante != "null")
                {
                    listaDocentePostulante = JsonConvert.DeserializeObject<List<DocentePostulanteDTO>>(datosDocentePostulante);
                }
                return listaDocentePostulante;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DocentePostulante ObtenerPorId(int id)
        {
            try
            {
                var modelo = FirstById(id);
                if (modelo != null)
                {
                    return _mapper.Map<DocentePostulante>(modelo);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocentePostulante> ObtenerPorIds(List<int> ids)
        {
            try
            {
                var modelos = GetBy(x => ids.Contains(x.Id)).ToList();
                return _mapper.Map<List<DocentePostulante>>(modelos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
