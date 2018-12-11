using RestWithASPNetCoreUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNetCoreUdemy.Business
{
    public interface IFileBusiness
    {
        byte[] getPDFFile();
    }
}
