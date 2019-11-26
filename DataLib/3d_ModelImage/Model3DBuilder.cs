using IDataLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
namespace DataLib
{
    public class Model3DBuilder : IModel3DBuilder
    {
        public string GreenValue { get; private set; }
        public string RedValue { get; private set; }
        public string YellowValue { get; private set; }
        public string AquaValue { get; private set; }
        public string BlueValue { get; private set; }


        protected string textureFilename;
        protected string solidFilename;
        protected double maxToleranceValue;


        protected Model3DGroup model_group;

        protected int maxArraySize;




        public void BuildModel(ref Model3DGroup modelgroup, ICylGridData data, double radialDirection, double nominalRadius,
     double minToleranceValue, double maxToleranceValue, double scalingFactor, COLORCODE colorCode)
        {
            try
            {


                maxArraySize = 1000000;
                model_group = modelgroup;
                this.maxToleranceValue = maxToleranceValue;
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                DefineLights();
                var mapBuilder = new CylMapBuilder(data, scalingFactor, radialDirection, nominalRadius);
                mapBuilder.CreateAltitudeMap(minToleranceValue * scalingFactor, maxToleranceValue * scalingFactor, colorCode);
                SetMapValues(mapBuilder);

                model_group.Children.Add(mapBuilder.DefineModel());

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void BuildModel(ref Model3DGroup modelgroup, ICartGridData data,
               double minToleranceValue, double maxToleranceValue, double scalingFactor, COLORCODE colorCode)
        {
            try
            {
                
                maxArraySize = 1000000;
                model_group = modelgroup;
                this.maxToleranceValue = maxToleranceValue;
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                DefineLights();
                var mapBuilder = new CartMapBuilder(data, scalingFactor);
                mapBuilder.CreateAltitudeMap(minToleranceValue * scalingFactor, maxToleranceValue * scalingFactor, colorCode);
                SetMapValues(mapBuilder);

                model_group.Children.Add(mapBuilder.DefineModel());

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void SetMapValues(CartMapBuilder mapBuilder)
        {
            try
            {
                BlueValue = mapBuilder.BlueValue.ToString("f4");
                RedValue = mapBuilder.RedValue.ToString("f4");
                GreenValue = mapBuilder.GreenValue.ToString("f4");
                YellowValue = mapBuilder.YellowValue.ToString("f4");
                AquaValue = mapBuilder.YellowValue.ToString("f4");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void SetMapValues(CylMapBuilder mapBuilder)
        {
            try
            {
                BlueValue = mapBuilder.BlueValue.ToString("f4");
                RedValue = mapBuilder.RedValue.ToString("f4");
                GreenValue = mapBuilder.GreenValue.ToString("f4");
                YellowValue = mapBuilder.YellowValue.ToString("f4");
                AquaValue = mapBuilder.YellowValue.ToString("f4");
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void DefineLights()
        {
            try
            {
                AmbientLight ambient_light = new AmbientLight(Colors.DarkGray);
                DirectionalLight directional_light = new DirectionalLight(Colors.Gray, new Vector3D(-1.0, -3.0, -2.0));
                model_group.Children.Add(ambient_light);
                model_group.Children.Add(directional_light);
                
            }
            catch (Exception)
            {

                throw;
            }

        }

        // Create the altitude map texture bitmap.



    }

    internal class CartMapBuilder : ColorMapBuilder<ICartGridData>
    {
        internal CartMapBuilder(ICartGridData data, double scalingFactor) : base(data, scalingFactor)
        {

        }
        protected override void BuildHeightArray()
        {
            try
            {
                heightArr = new double[XInCount, ZInCount];
                int zi = 0;
                //move strip values into array of heights
                double aveHeight = 0;
                foreach (CartData strip in data)
                {
                    for (int xi = 0; xi < XInCount; xi++)
                    {
                        heightArr[xi, zi] = scalingFactor * strip[xi].Y;
                        aveHeight += heightArr[xi, zi];
                    }
                    zi++;
                }
                aveHeight /= heightArr.Length;
                OffsetArray(aveHeight);
                SetIndices();
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected double GetXInputSpacing()
        {
            try
            {
                return Math.Abs((data[0][0].X - data[0][1].X));
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected double GetZInputSpacing()
        {
            try
            {
                return Math.Abs(data[0][0].Z - data[1][0].Z);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    internal class CylMapBuilder : ColorMapBuilder<ICylGridData>
    {
        protected double radialDirection;
        protected double nominalRadius;

        internal CylMapBuilder(ICylGridData data, double scalingFactor, double radialDirection, double nominalRadius)
            : base(data, scalingFactor)
        {
            this.radialDirection = radialDirection;
            this.nominalRadius = nominalRadius;

        }
        protected override void BuildHeightArray()
        {
            try
            {
                int zInCount = data.Count;
                int xInCount = data[0].Count;
                //find smallest strip to dim array
                DxInput = GetXInputSpacing(nominalRadius);
                DzInput = GetZInputSpacing();
                DxTarget = DxInput;
                DzTarget = DzInput;
                xInCount = GetMinStripCount();
                heightArr = new double[xInCount, zInCount];
                int zi = 0;
                //move strip values into array of heights
                double aveHeight = 0;
                foreach (CylData strip in data)
                {
                    for (int xi = 0; xi < xInCount; xi++)
                    {
                        heightArr[xi, zi] = scalingFactor * Math.Sign(radialDirection) * strip[xi].R;
                        aveHeight += heightArr[xi, zi];
                    }
                    zi++;
                }
                aveHeight /= heightArr.Length;
                OffsetArray(aveHeight);
                SetIndices();

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected int GetMinStripCount()
        {
            try
            {
                int xInCount = data[0].Count;
                foreach (CylData strip in data)
                {
                    if (strip.Count < xInCount)
                    {
                        xInCount = strip.Count;
                    }
                }
                return xInCount;
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected double GetXInputSpacing(double nominalRadius)
        {
            try
            {
                return Math.Abs(nominalRadius * (data[0][0].ThetaRad - data[0][1].ThetaRad));
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected double GetZInputSpacing()
        {
            try
            {
                return Math.Abs(data[0][0].Z - data[1][0].Z);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
    internal class ColorMapBase : IColorMapBase
    {
        public double ScaledMaxValue { get; protected set; }
        public double ScaledMinValue { get; protected set; }
        public double BlueValue { get; protected set; }
        public double AquaValue { get; protected set; }
        public double GreenValue { get; protected set; }
        public double YellowValue { get; protected set; }
        public double RedValue { get; protected set; }
        public int XInCount { get; protected set; }
        public int ZInCount { get; protected set; }
        public double DxInput { get; protected set; }
        public double DxTarget { get; protected set; }
        public double DzInput { get; protected set; }
        public double DzTarget { get; protected set; }
        public int XIndexMin { get; protected set; }
        public int XIndexMax { get; protected set; }
        public WriteableBitmap AltitudeMap { get; protected set; }
        public int ZIndexMin { get; protected set; }
        public int ZIndexMax { get; protected set; }
        public double Texture_xscale { get; protected set; }
        public double Texture_zscale { get; protected set; }
        public double GetHeight(int xIndex, int zIndex)
        {
            try
            {
                return heightArr[xIndex, zIndex];
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected double scalingFactor;
        protected int dxIndex, dzIndex;

        protected double[,] heightArr;

    }
    internal abstract class ColorMapBuilder<T> : ColorMapBase, IColorMapBuilder<T>
    {
        protected T data;

        // A dictionary to hold points for fast lookup.
        // protected Dictionary<Point3D, int> pointDictionary;
        public ColorMapBuilder(T data, double scalingFactor)
        {
            this.scalingFactor = scalingFactor;
            this.data = data;
        }

        protected void OffsetArray(double offset)
        {
            try
            {
                for (int i = 0; i < heightArr.GetLength(0); i++)
                {
                    for (int j = 0; j < heightArr.GetLength(1); j++)
                    {
                        heightArr[i, j] -= offset;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void SetIndices()
        {
            try
            {
                XIndexMin = 0;
                XIndexMax = heightArr.GetUpperBound(0);
                dxIndex = 1;
                ZIndexMin = 0;
                ZIndexMax = heightArr.GetUpperBound(1);
                dzIndex = 1;
                Texture_xscale = (XIndexMax - XIndexMin);
                Texture_zscale = (ZIndexMax - ZIndexMin);
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected abstract void BuildHeightArray();
        public void CreateAltitudeMap(double minToleranceValue, double maxToleranceValue, COLORCODE colorcode)
        {
            try
            {
                BuildHeightArray();
                // Calculate the function's value over the area.
                int xwidth = heightArr.GetUpperBound(0) + 1;
                int zwidth = heightArr.GetUpperBound(1) + 1;
                double dx = (XIndexMax - XIndexMin) / xwidth;
                double dz = (ZIndexMax - ZIndexMin) / zwidth;

                // Get the upper and lower bounds on the values.
                SetColors();

                // Make the BitmapPixelMaker.
                BitmapPixelMaker bm_texture_maker = new BitmapPixelMaker(xwidth, zwidth);
                var bm_solid_maker = new BitmapPixelMaker(xwidth, zwidth);

                // Set the pixel colors.
                for (int ix = 0; ix < xwidth; ix++)
                {
                    for (int iz = 0; iz < zwidth; iz++)
                    {
                        System.Drawing.Color color = System.Drawing.Color.FromArgb(100, 100, 100);
                        var cc = new ColorCoder(colorcode);

                        switch (colorcode)
                        {
                            case COLORCODE.GREEN_RED:
                                cc.SetValues(minToleranceValue, maxToleranceValue);
                                break;
                            case COLORCODE.MONO:

                                break;
                            case COLORCODE.RAINBOW:
                            default:
                                cc.SetValues(ScaledMinValue, ScaledMaxValue);
                                break;
                        }
                        color = cc.MapColor(heightArr[ix, iz]);
                        bm_texture_maker.SetPixel(ix, iz, color);

                    }
                }

                // Convert the BitmapPixelMaker into a WriteableBitmap.
                AltitudeMap = bm_texture_maker.MakeBitmap(96, 96);
            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void SetColors()
        {
            try
            {
                var get_values =
                   from double value in heightArr
                   select value;
                ScaledMinValue = get_values.Min();
                ScaledMaxValue = get_values.Max();
                double unscaledMaxValue = ScaledMaxValue / scalingFactor;
                double unscaledMinValue = ScaledMinValue / scalingFactor;
                var cc = new ColorCoder(COLORCODE.RAINBOW);
                cc.SetValues(unscaledMinValue, unscaledMaxValue);
                BlueValue = cc.GetBlueValue();
                AquaValue = cc.GetAquaValue();
                GreenValue = cc.GetGreenValue();
                YellowValue = cc.GetYellowValue();
                RedValue = cc.GetRedValue();
            }
            catch (Exception)
            {

                throw;
            }
        }
        // Add the model to the Model3DGroup.
        public GeometryModel3D DefineModel()
        {
            try
            {
                // Make a mesh to hold the surface.
                MeshGeometry3D mesh = new MeshGeometry3D();
                var pointDictionary = new Dictionary<Point3D, int>();
                // Make the surface's points and triangles.
                var midIndex_x = XIndexMax / 2.0;
                var midIndex_z = ZIndexMax / 2.0;
                Point3D p00;
                Point3D p10;
                Point3D p01;
                Point3D p11;
                for (int x = XIndexMin; x < XIndexMax; x++)
                {
                    for (int z = ZIndexMin; z < ZIndexMax; z++)
                    {
                        // Make points at the corners of the surface
                        // over (x, z) - (x + dx, z + dz).
                        double x0 = (x - midIndex_x) * DxTarget;
                        double z0 = (z - midIndex_z) * DzTarget;
                        double x1 = x0 + DxTarget;
                        double z1 = z0 + DzTarget;
                        p00 = new Point3D(x0, GetHeight(x, z), z0);
                        p10 = new Point3D(x1, GetHeight(x + 1, z), z0);
                        p01 = new Point3D(x0, GetHeight(x, z + 1), z1);
                        p11 = new Point3D(x1, GetHeight(x + 1, z + 1), z1);

                        // Add the triangles
                        AddTriangle(pointDictionary, mesh, p00, p01, p11);
                        AddTriangle(pointDictionary, mesh, p00, p11, p10);
                    }
                }


                // Make the surface's material using an image brush.
                ImageBrush texture_brush = new ImageBrush();

                texture_brush.ImageSource = AltitudeMap;
                var surface_material = new DiffuseMaterial(texture_brush);


                // Make the mesh's model.
                GeometryModel3D surface_model = new GeometryModel3D(mesh, surface_material)
                {
                    // Make the surface visible from both sides.
                    BackMaterial = surface_material
                };

                // Add the model to the model groups.
                return surface_model;

            }
            catch (Exception)
            {

                throw;
            }
        }
        protected void AddTriangle(Dictionary<Point3D, int> pointDictionary, MeshGeometry3D mesh, Point3D point1, Point3D point2, Point3D point3)
        {
            try
            {
                // Get the points' indices.
                int index1 = AddPoint(pointDictionary, mesh.Positions, mesh.TextureCoordinates, point1);
                int index2 = AddPoint(pointDictionary, mesh.Positions, mesh.TextureCoordinates, point2);
                int index3 = AddPoint(pointDictionary, mesh.Positions, mesh.TextureCoordinates, point3);

                // Create the triangle.
                mesh.TriangleIndices.Add(index1);
                mesh.TriangleIndices.Add(index2);
                mesh.TriangleIndices.Add(index3);
            }
            catch (Exception)
            {

                throw;
            }

        }

        // If the point already exists, return its index.
        // Otherwise create the point and return its new index.
        protected int AddPoint(Dictionary<Point3D, int> pointDictionary, Point3DCollection points, PointCollection texture_coords, Point3D point)
        {
            try
            {
                // If the point is in the point dictionary,
                // return its saved index.
                if (pointDictionary.ContainsKey(point))
                    return pointDictionary[point];

                // We didn't find the point. Create it.
                points.Add(point);
                pointDictionary.Add(point, points.Count - 1);

                // Set the point's texture coordinates.
                texture_coords.Add(
                    new System.Windows.Point(
                        (point.X - XIndexMin) * Texture_xscale,
                        (point.Z - ZIndexMax) * Texture_zscale));

                // Return the new point's index.
                return points.Count - 1;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }

}
