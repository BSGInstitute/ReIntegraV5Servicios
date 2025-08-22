namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AvatarDTO
    {
        public int? Id { get; set; }
        public int? IdPersonal { get; set; }
        public string Top { get; set; }
        public string Accessories { get; set; }
        public string HairColor { get; set; }
        public string FacialHair { get; set; }
        public string FacialHairColor { get; set; }
        public string Clothes { get; set; }
        public string Eyes { get; set; }
        public string Eyesbrow { get; set; }
        public string Mouth { get; set; }
        public string Skin { get; set; }
        public string ClothesColor { get; set; }
        public int? IdSexo { get; set; }
        public string Usuario { get; set; }
    }
    public class AvatarAlumnoDTO
    {
        public int IdAvatar { get; set; }
        public int IdAlumno { get; set; }
        public string TopC { get; set; }
        public string Accessories { get; set; }
        public string Hair_Color { get; set; }
        public string Facial_Hair { get; set; }
        public string Facial_Hair_Color { get; set; }
        public string Clothes { get; set; }
        public string Clothes_Color { get; set; }
        public string Eyes { get; set; }
        public string Eyesbrow { get; set; }
        public string Mouth { get; set; }
        public string Skin { get; set; }
    }
}
