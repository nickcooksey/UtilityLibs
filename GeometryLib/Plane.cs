namespace GeometryLib
{

    public enum PlaneName
    {
        XY,
        XZ,
        YZ
    }
    public class Plane
    {

        public static Plane XYPlane
        {
            get
            {
                return new Plane(new Vector3(0, 0, 1));
            }

        }
        public static Plane XZPlane
        {
            get
            {
                return new Plane(new Vector3(0, 1, 0));
            }

        }
        public static Plane YZPlane
        {

            get
            {
                return new Plane(new Vector3(1, 0, 0));
            }

        }
        public Vector3 Normal { get; set; }

        public Plane(Vector3 normal)
        {
            Normal = normal;
        }

    }
}
