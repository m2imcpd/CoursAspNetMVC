using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace ECommerce.Tools
{
    public class ResizeImageService : IResizeImageService
    {
        public Image ResizeImage(Image img, int maxWidth, int maxHeight)
        {
            if(img.Width < maxWidth && img.Height < maxHeight)
            {
                return img;
            }

            double xRatio = (double)img.Width / maxWidth;
            double yRatio = (double)img.Height / maxHeight;
            double ratio = Math.Max(xRatio, yRatio);
            int nx = (int)Math.Floor(img.Width / ratio);
            int ny = (int)Math.Floor(img.Height / ratio);
            Bitmap newImg = new Bitmap(nx, ny, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(newImg);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            
            g.DrawImage(img, new Rectangle(0, 0, nx, ny), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
            return newImg;
        }

        public void SaveImage(byte[] imgBytes, string FilePath, int maxWidth, int maxHeight)
        {
            Image img = ResizeImage(Image.FromStream(new MemoryStream(imgBytes,0,imgBytes.Length)),maxWidth, maxHeight);
            img.Save(FilePath, ImageFormat.Jpeg);
        }
    }
}
