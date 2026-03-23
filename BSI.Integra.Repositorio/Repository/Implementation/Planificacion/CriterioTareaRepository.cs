using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: CriterioTareaRepository
    /// <summary>
    /// Gestión de T_CriterioTarea usando stored procedures (sin ORM)
    /// </summary>
    public class CriterioTareaRepository : ICriterioTareaRepository
    {
        private readonly IDapperRepository _dapperRepository;

        public CriterioTareaRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }

        /// <summary>
        /// Inserta un nuevo CriterioTarea mediante SP
        /// </summary>
        public bool Insertar(CriterioTareaDTO criterioDTO, string usuario)
        {
            try
            {
                var sp = "[pla].[SP_TareaCriterio_Insertar]";
                _dapperRepository.QuerySPDapper(sp, new
                {
                    Nombre             = criterioDTO.Nombre,
                    Descripcion        = criterioDTO.Descripcion,
                    Escala             = criterioDTO.Escala,
                    UsuarioCreacion    = usuario,
                    UsuarioModificacion = usuario
                });
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en CriterioTareaRepository.Insertar: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Actualiza un CriterioTarea existente mediante SP
        /// </summary>
        public bool Actualizar(CriterioTareaDTO criterioDTO, string usuario)
        {
            try
            {
                var sp = "[pla].[SP_TareaCriterio_Actualizar]";
                _dapperRepository.QuerySPDapper(sp, new
                {
                    Id                  = criterioDTO.Id,
                    Nombre              = criterioDTO.Nombre,
                    Descripcion         = criterioDTO.Descripcion,
                    Escala              = criterioDTO.Escala,
                    UsuarioModificacion = usuario
                });
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en CriterioTareaRepository.Actualizar: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Elimina lógicamente un CriterioTarea mediante SP
        /// </summary>
        public bool Eliminar(int id, string usuario)
        {
            try
            {
                var sp = "[pla].[SP_TareaCriterio_Eliminar]";
                _dapperRepository.QuerySPDapper(sp, new
                {
                    Id                  = id,
                    UsuarioModificacion = usuario
                });
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en CriterioTareaRepository.Eliminar: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtiene un CriterioTarea activo por su Id
        /// </summary>
        public CriterioTareaDTO ObtenerPorId(int idCriterio)
        {
            try
            {
                var query = @"SELECT Id, Nombre, Descripcion, Escala, Estado AS Activo
                              FROM pla.T_TareaCriterio
                              WHERE Estado = 1 AND Id = @Id";

                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = idCriterio });

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                    return JsonConvert.DeserializeObject<CriterioTareaDTO>(resultado);

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en CriterioTareaRepository.ObtenerPorId: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lista todos los CriterioTarea activos
        /// </summary>
        public List<CriterioTareaDTO> ListarCriterios()
        {
            try
            {
                var query = @"SELECT Id, Nombre, Descripcion, Escala, Estado AS Activo
                              FROM pla.T_TareaCriterio
                              WHERE Estado = 1
                              ORDER BY Id DESC";

                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    return JsonConvert.DeserializeObject<List<CriterioTareaDTO>>(resultado);

                return new List<CriterioTareaDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en CriterioTareaRepository.ListarCriterios: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lista los SubCriterioTarea activos asignados a un CriterioTarea
        /// </summary>
        public List<SubCriterioTareaDTO> ListarSubCriteriosPorCriterio(int idCriterio)
        {
            try
            {
                var sp = "pla.SP_CriterioSubCriterioObtener";

                var resultado = _dapperRepository.QuerySPDapper(sp, new { IdTareaCriterio = idCriterio });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    resultado = resultado.Replace("\"IdTareaSubCriterio\":", "\"Id\":");
                    return JsonConvert.DeserializeObject<List<SubCriterioTareaDTO>>(resultado);

                return new List<SubCriterioTareaDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en CriterioTareaRepository.ListarSubCriteriosPorCriterio: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Asigna un SubCriterioTarea a un CriterioTarea mediante SP
        /// </summary>
        public bool AsignarSubCriterio(int idCriterio, int idSubCriterio, string usuario)
        {
            try
            {
                var sp = "pla.SP_TareaCriterioSubCriterio_Insertar";
                _dapperRepository.QuerySPDapper(sp, new
                {
                    IdTareaCriterio     = idCriterio,
                    IdTareaSubCriterio  = idSubCriterio,
                    UsuarioCreacion     = usuario,
                    UsuarioModificacion = usuario
                });
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en CriterioTareaRepository.AsignarSubCriterio: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Desasigna (eliminación lógica) un SubCriterioTarea de un CriterioTarea mediante SP
        /// </summary>
        public bool DesasignarSubCriterio(int idCriterio, int idSubCriterio, string usuario)
        {
            try
            {
                var sp = "pla.SP_TareaCriterioSubCriterio_Eliminar";
                _dapperRepository.QuerySPDapper(sp, new
                {
                    IdTareaCriterio     = idCriterio,
                    IdTareaSubCriterio  = idSubCriterio,
                    UsuarioModificacion = usuario
                });
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en CriterioTareaRepository.DesasignarSubCriterio: {ex.Message}", ex);
            }
        }
    }
}
