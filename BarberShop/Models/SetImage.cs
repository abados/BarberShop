namespace BarberShop.Models
{
    public class SetImage
    {
        public SetImage(IFormFile file) {

            if (file == null) return;
            MemoryStream stream= new MemoryStream();
            file.CopyTo(stream);
            Image= stream.ToArray();
        
        }

        public byte[] Image { get; }
    }
}
