using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPhotoService
    {
        Task<string> UploadProfilePhotoAsync(Stream fileStream, string fileName);
    }
}
