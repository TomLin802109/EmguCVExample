using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quadrep.Presenter
{
    public static class EmguCVExtension
    {
        public static Mat Draw(this Mat img, BBox box, Color color)
        {
            CvInvoke.Rectangle(img, box.Rectangle, new Bgr(color).MCvScalar, 2);
            var name = box.Name.Equals("") ? $"{box.ID}" : box.Name;
            CvInvoke.PutText(img, $"{name} {box.Score:F2}",
                                     new Point(box.Rectangle.X, box.Rectangle.Y - 10),
                                     FontFace.HersheyDuplex, 0.5, new Bgr(color).MCvScalar);
            return img;
        }
    }
}
