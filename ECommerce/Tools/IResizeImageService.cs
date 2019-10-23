using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Tools
{
    public interface IResizeImageService
    {
        Image ResizeImage(Image img, int maxWidth, int maxHeight);

        void SaveImage(byte[] imgBytes, string FilePath, int maxWidth, int maxHeight);
    }
}
