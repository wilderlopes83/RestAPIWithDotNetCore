using System.IO;

namespace RestWithASPNetCoreUdemy.Business
{
    public class FileBusinessImpl : IFileBusiness
    {
        public byte[] getPDFFile()
        {
            string path = Directory.GetCurrentDirectory();
            var fullPath = path + "\\Other\\RegrasLiquidacaoAntecipadaFGTS.pdf";
            return File.ReadAllBytes(fullPath);
        }
    }
}
