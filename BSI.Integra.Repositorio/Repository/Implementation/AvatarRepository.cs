using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: AvatarRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 07/09/2022
    /// <summary>
    /// Gestión general de la tabla T_Avatar
    /// </summary>
    public class AvatarRepository : GenericRepository<TAvatar>, IAvatarRepository
    {
        private Mapper _mapper;

        public AvatarRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAvatar, Avatar>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 07/09/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene avatar por usuario
        /// </summary>
        /// <param name="Usuario"> Nombre de usuario </param>
        /// <returns> AvatarCaracteristicaAgrupadoDTO </returns>
        public AvatarDTO ObtenerAvatar(string usuario)
        {
            try
            {
                AvatarDTO avatar = new AvatarDTO();
                var query = @"SELECT Accessories, Clothes, ClothesColor,  Estado,  Eyes,  Eyesbrow, FacialHair,  
                            FacialHairColor, HairColor,  Id,  IdPersonal,  IdSexo, Mouth, Skin,   [Top],  UserName 
                            FROM com.V_ObtenerAvatarPorUsuario WHERE UserName=@Usuario";
                var respuesta = _dapperRepository.QueryDapper(query, new { usuario });
                if (respuesta != "[]")
                {
                    respuesta = _dapperRepository.FirstOrDefault(query, new { usuario });
                    if (!string.IsNullOrEmpty(respuesta) && respuesta != "null")
                    {
                        avatar = JsonConvert.DeserializeObject<AvatarDTO>(respuesta);
                    }
                }
                if (avatar.Id == null)
                {
                    if (avatar.IdSexo == 2)
                    {
                        avatar.Top = "LongHairStraight";
                        avatar.Accessories = "Blank";
                        avatar.HairColor = "Brown";
                        avatar.FacialHair = "Blank";
                        avatar.FacialHairColor = "Brown";
                        avatar.Clothes = "ShirtScoopNeck";
                        avatar.ClothesColor = "Pink";
                        avatar.Eyes = "Default";
                        avatar.Eyesbrow = "Default";
                        avatar.Mouth = "Default";
                        avatar.Skin = "Light";
                    }
                    else
                    {
                        avatar.Top = "ShortHairTheCaesar";
                        avatar.Accessories = "Blank";
                        avatar.HairColor = "Auburn";
                        avatar.FacialHair = "Blank";
                        avatar.FacialHairColor = "Auburn";
                        avatar.Clothes = "CollarSweater";
                        avatar.ClothesColor = "Blue02";
                        avatar.Eyes = "Default";
                        avatar.Eyesbrow = "Default";
                        avatar.Mouth = "Default";
                        avatar.Skin = "Tanned";
                    }
                }
                return avatar;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
