
using System;

namespace SupersonicSound.LowLevel
{
    public class Geometry
        : IEquatable<Geometry>
    {
        public FMOD.Geometry FmodGeometry { get; private set; }

        public Geometry(FMOD.Geometry geometry)
        {
            FmodGeometry = geometry;
        }

        #region equality
        public bool Equals(Geometry other)
        {
            if (other == null)
                return false;

            return other.FmodGeometry == FmodGeometry;
        }

        public override bool Equals(object obj)
        {
            var c = obj as Geometry;
            if (c == null)
                return false;

            return Equals(c);
        }

        public override int GetHashCode()
        {
            return (FmodGeometry != null ? FmodGeometry.GetHashCode() : 0);
        }
        #endregion

        #region Polygon manipulation.
        //public RESULT addPolygon(float directocclusion, float reverbocclusion, bool doublesided, int numvertices, VECTOR[] vertices, out int polygonindex)
        //{
        //    return FMOD5_Geometry_AddPolygon(rawPtr, directocclusion, reverbocclusion, doublesided, numvertices, vertices, out polygonindex);
        //}
        //public RESULT getNumPolygons(out int numpolygons)
        //{
        //    return FMOD5_Geometry_GetNumPolygons(rawPtr, out numpolygons);
        //}
        //public RESULT getMaxPolygons(out int maxpolygons, out int maxvertices)
        //{
        //    return FMOD5_Geometry_GetMaxPolygons(rawPtr, out maxpolygons, out maxvertices);
        //}
        //public RESULT getPolygonNumVertices(int index, out int numvertices)
        //{
        //    return FMOD5_Geometry_GetPolygonNumVertices(rawPtr, index, out numvertices);
        //}
        //public RESULT setPolygonVertex(int index, int vertexindex, ref VECTOR vertex)
        //{
        //    return FMOD5_Geometry_SetPolygonVertex(rawPtr, index, vertexindex, ref vertex);
        //}
        //public RESULT getPolygonVertex(int index, int vertexindex, out VECTOR vertex)
        //{
        //    return FMOD5_Geometry_GetPolygonVertex(rawPtr, index, vertexindex, out vertex);
        //}
        //public RESULT setPolygonAttributes(int index, float directocclusion, float reverbocclusion, bool doublesided)
        //{
        //    return FMOD5_Geometry_SetPolygonAttributes(rawPtr, index, directocclusion, reverbocclusion, doublesided);
        //}
        //public RESULT getPolygonAttributes(int index, out float directocclusion, out float reverbocclusion, out bool doublesided)
        //{
        //    return FMOD5_Geometry_GetPolygonAttributes(rawPtr, index, out directocclusion, out reverbocclusion, out doublesided);
        //}
        #endregion

        #region Object manipulation.
        //public RESULT setActive(bool active)
        //{
        //    return FMOD5_Geometry_SetActive(rawPtr, active);
        //}
        //public RESULT getActive(out bool active)
        //{
        //    return FMOD5_Geometry_GetActive(rawPtr, out active);
        //}
        //public RESULT setRotation(ref VECTOR forward, ref VECTOR up)
        //{
        //    return FMOD5_Geometry_SetRotation(rawPtr, ref forward, ref up);
        //}
        //public RESULT getRotation(out VECTOR forward, out VECTOR up)
        //{
        //    return FMOD5_Geometry_GetRotation(rawPtr, out forward, out up);
        //}
        //public RESULT setPosition(ref VECTOR position)
        //{
        //    return FMOD5_Geometry_SetPosition(rawPtr, ref position);
        //}
        //public RESULT getPosition(out VECTOR position)
        //{
        //    return FMOD5_Geometry_GetPosition(rawPtr, out position);
        //}
        //public RESULT setScale(ref VECTOR scale)
        //{
        //    return FMOD5_Geometry_SetScale(rawPtr, ref scale);
        //}
        //public RESULT getScale(out VECTOR scale)
        //{
        //    return FMOD5_Geometry_GetScale(rawPtr, out scale);
        //}
        //public RESULT save(IntPtr data, out int datasize)
        //{
        //    return FMOD5_Geometry_Save(rawPtr, data, out datasize);
        //}
        #endregion

        #region Userdata set/get.
        //public RESULT setUserData(IntPtr userdata)
        //{
        //    return FMOD5_Geometry_SetUserData(rawPtr, userdata);
        //}
        //public RESULT getUserData(out IntPtr userdata)
        //{
        //    return FMOD5_Geometry_GetUserData(rawPtr, out userdata);
        //}
        #endregion
    }

    public static class GeometryExtensions
    {
        public static FMOD.Geometry ToFmod(this Geometry geometry)
        {
            if (geometry == null)
                return null;

            return geometry.FmodGeometry;
        }
    }
}
