using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quadrep.Presenter
{
    public class BBox
    {
        public int ID { get; set; }
        public float Score { get; set; }
        public int X { get => Rectangle.X; set => Rectangle.X = value; }
        public int Y { get => Rectangle.Y; set => Rectangle.Y = value; }
        public int W { get => Rectangle.Width; set => Rectangle.Width = value; }
        public int H { get => Rectangle.Height; set => Rectangle.Height = value; }
        public string Name { get; set; }

        public System.Drawing.Rectangle Rectangle;
        
        public BBox(int id, float score, int x, int y, int w, int h, string name="")
        {
            ID = id; Score = score; Name = name;
            Rectangle = new System.Drawing.Rectangle(x, y, w, h);
        }
        public BBox(int id, float score, System.Drawing.Rectangle rect, string name="")
        {
            ID = id; Score = score; Name = name; Rectangle = rect;
        }
        public override string ToString() => $"{X},{Y},{W},{H}";
        
    }
}
