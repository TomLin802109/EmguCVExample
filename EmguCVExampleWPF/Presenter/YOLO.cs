using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Dnn;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Quadrep.Presenter
{
    public class YOLOv3
    {
        private Net darknet = null;
        private string[] ClassName;
        private readonly Dictionary<int, Color> ColorMap = new Dictionary<int, Color>()
        {
            {0,Color.Red},{1,Color.LightGreen},{2,Color.Blue},{3,Color.Beige},{4,Color.Magenta},
            {5,Color.Yellow},{6,Color.RosyBrown}
        };
        
        public YOLOv3(string cfgFile, string weightFile, string nameFile)
        {
            //string[] ObjectNames = File.ReadAllLines(nameFile);
            darknet = DnnInvoke.ReadNetFromDarknet(cfgFile, weightFile);
            darknet.SetPreferableBackend(Emgu.CV.Dnn.Backend.Cuda);
            darknet.SetPreferableTarget(Target.Cuda);
            //darknet.SetPreferableTarget(Emgu.CV.Dnn.Backend.Cuda);
            
            ClassName = File.ReadAllLines("coco.names");
        }

        public BBox[] Detected(Mat image, bool popupResult=true)
        {
            var st = DateTime.Now;
            Mat inputBlob = DnnInvoke.BlobFromImage(image, 1.0 / 255.0, new Size(416, 416), new MCvScalar(0), true, false);
            VectorOfMat output = new VectorOfMat();
            darknet.SetInput(inputBlob);
            darknet.Forward(output, GetOutputsNames(darknet));
            var ct = (DateTime.Now - st).TotalMilliseconds;
            
            //新增三個List，包含物件的Rectangle, 物件分數, 物件的index
            List<Rectangle> rects = new List<Rectangle>();
            List<float> scores = new List<float>();
            List<int> objIndexs = new List<int>();
            //get output of each yolo layer
            for (int l = 0; l < output.Size; l++)
            {
                var boxes = output[l];
                int resultRows = boxes.SizeOfDimension[0];
                int resultCols = boxes.SizeOfDimension[1];

                float[] temp = new float[resultRows * resultCols];
                Marshal.Copy(boxes.DataPointer, temp, 0, temp.Length);

                for (int i = 0; i < resultRows; i++)
                {
                    //取出sub array(represent one bounding box), 從第六個位置開始抓，以此例會抓到80筆資料(對應到coco 80個物件)
                    var row = boxes.Row(i);

                    var subMat = new Mat(boxes.Row(i), new Rectangle(5, 0, resultCols - 5, 1));
                    //Find a class which has higest score
                    subMat.MinMax(out _, out double[] maxValues, out _, out Point[] maxPoints);

                    //若是判斷分數大於0，則進行下一步
                    if (maxValues[0] > 0)
                    {
                        //取出該物件的rectangle
                        
                        int centerX = (int)(temp[i * resultCols + 0] * image.Width);
                        int centerY = (int)(temp[i * resultCols + 1] * image.Height);
                        int width = (int)(temp[i * resultCols + 2] * image.Width);
                        int height = (int)(temp[i * resultCols + 3] * image.Height);
                        var confidence = temp[i * resultCols + 4];
                        int left = centerX - width / 2;
                        int top = centerY - height / 2;
                        Rectangle rect = new Rectangle(left, top, width, height);

                        //將rectangle, score, object index加入List
                        rects.Add(rect);
                        scores.Add((float)maxValues[0]*confidence);
                        objIndexs.Add(maxPoints[0].X);
                    }
                }
                
            }

            //Remove bounding box by NMS method, then drow result
            var IndexOfNMSBoxes = DnnInvoke.NMSBoxes(rects.ToArray(), scores.ToArray(), 0.5f, 0.5f);
            var NMSBoxes = IndexOfNMSBoxes.Select(ind => 
                            new BBox(objIndexs[ind], scores[ind], rects[ind].X, rects[ind].Y, 
                                     rects[ind].Width, rects[ind].Height, ClassName[objIndexs[ind]])).OrderBy(b=>b.ID).ToArray();
            if (popupResult)
            {
                for (int i = 0; i < NMSBoxes.Count(); i++)
                {
                    Color color = i > ColorMap.Count() ? ColorMap[i % ColorMap.Count()] : ColorMap[i];
                    CvInvoke.Rectangle(image, NMSBoxes[i].Rectangle, new Bgr(color).MCvScalar, 2);
                    CvInvoke.PutText(image, $"{ClassName[NMSBoxes[i].ID]} {NMSBoxes[i].Score:F2}",
                                     new Point(NMSBoxes[i].Rectangle.X, NMSBoxes[i].Rectangle.Y - 10),
                                     FontFace.HersheyDuplex, 0.5, new Bgr(ColorMap[i]).MCvScalar);
                }
                CvInvoke.Imshow("Detection Result", image);
            }
            return NMSBoxes;
        }

        //取出DNN Net的output layer的名稱
        private static string[] GetOutputsNames(Net net)
        {
            int[] outLayers = net.UnconnectedOutLayers;
            string[] layerNames = net.LayerNames;
            var names = new string[outLayers.Length];
            for (int i = 0; i < outLayers.Length; i++)
                names[i] = layerNames[outLayers[i] - 1];
            return names;
        }
    }
}
