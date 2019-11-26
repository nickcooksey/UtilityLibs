using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DrawingIO
{
    struct Tri
    {
        uint v1;
        uint v2;
        uint v3;
        uint Index;
        List<uint> neighbors;

        public bool IsNeighbor(Tri tri)
        {
            bool result = false;
            if (tri.neighbors.Contains(Index))
            {
                result = true;
            }
            else
            {
                int score = 0;
                if (tri.v1 - v1 == 0)
                {
                    score++;
                }
                if (tri.v2 - v2 == 0)
                {
                    score++;
                }
                if (tri.v3 - v3 == 0)
                {
                    score++;
                }
                if (score >= 2)
                {
                    neighbors.Add(tri.Index);
                    tri.neighbors.Add(Index);
                    result= true;

                }
                else
                {
                    result= false;
                }
            }
            return result;
        }

    }
    class SurfaceModel
    {
        
        List<Vector3> points;
        List<Tri> triangles;

    }
}
