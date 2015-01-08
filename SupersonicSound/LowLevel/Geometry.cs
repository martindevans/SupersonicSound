using System.Linq;
using System.Runtime.InteropServices;
using FMOD;
using SupersonicSound.Wrapper;
using System;

namespace SupersonicSound.LowLevel
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Geometry
        : IEquatable<Geometry>
    {
        [FieldOffset(0)]
        private readonly FMOD.Geometry _fmodGeometry;

        [FieldOffset(0)]
        private readonly PolygonCollection _polygonCollection;

        public FMOD.Geometry FmodGeometry
        {
            get
            {
                return _fmodGeometry;
            }
        }

        public Geometry(FMOD.Geometry geometry)
            : this()
        {
            if (geometry == null)
                throw new ArgumentNullException("geometry");
            _fmodGeometry = geometry;
        }

        #region equality
        public bool Equals(Geometry other)
        {
            return other.FmodGeometry == FmodGeometry;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Geometry))
                return false;

            return Equals((Geometry)obj);
        }

        public override int GetHashCode()
        {
            return (FmodGeometry != null ? FmodGeometry.GetHashCode() : 0);
        }
        #endregion

        #region Polygon manipulation.
        public int AddPolygon(float directOcclusion, float reverbOcclusion, bool doubleSided, Vector3[] vertices)
        {
            int index;
            FmodGeometry.addPolygon(directOcclusion, reverbOcclusion, doubleSided, vertices.Length, vertices.Select(a => a.ToFmod()).ToArray(), out index).Check();
            return index;
        }

        public int PolygonCount
        {
            get
            {
                int num;
                FmodGeometry.getNumPolygons(out num).Check();
                return num;
            }
        }

        public int MaxPolygons
        {
            get
            {
                int maxvertices;
                int maxpolygons;
                FmodGeometry.getMaxPolygons(out maxpolygons, out maxvertices).Check();
                return maxpolygons;
            }
        }

        public int MaxVertices
        {
            get
            {
                int maxvertices;
                int maxpolygons;
                FmodGeometry.getMaxPolygons(out maxpolygons, out maxvertices).Check();
                return maxvertices;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct PolygonCollection
        {
            [FieldOffset(0)]
            private readonly FMOD.Geometry _fmodGeometry;

            public Poly this[int index]
            {
                get
                {
                    return new Poly(index, _fmodGeometry);
                }
            }
        }

        public struct Poly
        {
            public readonly int Index;
            private readonly FMOD.Geometry _fmodGeometry;

            public Poly(int index, FMOD.Geometry fmodGeometry)
            {
                Index = index;
                _fmodGeometry = fmodGeometry;
            }

            public int VertexCount
            {
                get
                {
                    int num;
                    _fmodGeometry.getPolygonNumVertices(Index, out num).Check();
                    return num;
                }
            }

            public Vector3 this[int vertexIndex]
            {
                get
                {
                    VECTOR vector;
                    _fmodGeometry.getPolygonVertex(Index, vertexIndex, out vector).Check();
                    return new Vector3(vector);
                }
                set
                {
                    VECTOR vector = value.ToFmod();
                    _fmodGeometry.setPolygonVertex(Index, vertexIndex, ref vector).Check();
                }
            }

            public float DirectOcclusion
            {
                get
                {
                    float d;
                    float r;
                    bool s;
                    GetAttributes(out d, out r, out s);
                    return d;
                }
                set
                {
                    float d;
                    float r;
                    bool s;
                    GetAttributes(out d, out r, out s);

                    SetAttributes(value, r, s);
                }
            }

            public float ReverbOcclusion
            {
                get
                {
                    float d;
                    float r;
                    bool s;
                    GetAttributes(out d, out r, out s);
                    return r;
                }
                set
                {
                    float d;
                    float r;
                    bool s;
                    GetAttributes(out d, out r, out s);

                    SetAttributes(d, value, s);
                }
            }

            public bool DoubleSided
            {
                get
                {
                    float d;
                    float r;
                    bool s;
                    GetAttributes(out d, out r, out s);
                    return s;
                }
                set
                {
                    float d;
                    float r;
                    bool s;
                    GetAttributes(out d, out r, out s);

                    SetAttributes(d, r, value);
                }
            }

            public void GetAttributes(out float directOcclusion, out float reverbOcclusion, out bool doubleSided)
            {
                _fmodGeometry.getPolygonAttributes(Index, out directOcclusion, out reverbOcclusion, out doubleSided).Check();
            }

            public void SetAttributes(float directOcclusion, float reverbOcclusion, bool doubleSided)
            {
                if (directOcclusion < 0 || directOcclusion > 1)
                    throw new ArgumentOutOfRangeException("direct occlusion must be 0 < Direct < 1");
                if (reverbOcclusion < 0 || reverbOcclusion > 1)
                    throw new ArgumentOutOfRangeException("reverb occlusion must be 0 < Reverb < 1");

                _fmodGeometry.setPolygonAttributes(Index, directOcclusion, reverbOcclusion, doubleSided).Check();
            }
        }

        public PolygonCollection Polygon
        {
            get
            {
                return _polygonCollection;
            }
        }
        #endregion

        #region Object manipulation.
        public bool Active
        {
            get
            {
                bool active;
                FmodGeometry.getActive(out active).Check();
                return active;
            }
            set
            {
                FmodGeometry.setActive(value).Check();
            }
        }

        public void SetRotation(Vector3 forward, Vector3 up)
        {
            VECTOR f = forward.ToFmod();
            VECTOR u = up.ToFmod();
            FmodGeometry.setRotation(ref f, ref u).Check();
        }

        public void GetRotation(out Vector3 forward, out Vector3 up)
        {
            VECTOR f;
            VECTOR u;
            FmodGeometry.getRotation(out f, out u).Check();

            forward = new Vector3(f);
            up = new Vector3(u);
        }

        /// <summary>
        /// Set rotation from quaternion values
        /// </summary>
        /// <param name="w"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void SetRotation(float w, float x, float y, float z)
        {
            // http://nic-gamedev.blogspot.co.uk/2011/11/quaternion-math-getting-local-axis.html

            var f = new VECTOR {
                x = 2 * (x * z + w * y),
                y = 2 * (y * x - w * x),
                z = 1 - 2 * (x * x + y * y)
            };

            var u = new VECTOR {
                x = 2 * (x * y - w * z), 
                y = 1 - 2 * (x * x + z * z),
                z = 2 * (y * z + w * x)
            };

            FmodGeometry.setRotation(ref f, ref u).Check();
        }

        public Vector3 Position
        {
            get
            {
                VECTOR position;
                FmodGeometry.getPosition(out position).Check();
                return new Vector3(position);
            }
            set
            {
                var pos = value.ToFmod();
                FmodGeometry.setPosition(ref pos).Check();
            }
        }

        public Vector3 Scale
        {
            get
            {
                VECTOR scale;
                FmodGeometry.getScale(out scale).Check();
                return new Vector3(scale);
            }
            set
            {
                var scale = value.ToFmod();
                FmodGeometry.setScale(ref scale).Check();
            }
        }

        public ArraySegment<byte> Save()
        {
            int size;
            FmodGeometry.save(IntPtr.Zero, out size).Check();

            byte[] buffer = new byte[size];
            unsafe
            {
                fixed (byte* ptr = &buffer[0])
                {
                    FmodGeometry.save(new IntPtr(ptr), out size).Check();
                }
            }

            return new ArraySegment<byte>(buffer, 0, size);
        }
        #endregion

        #region Userdata set/get.
        public IntPtr UserData
        {
            get
            {
                IntPtr ptr;
                FmodGeometry.getUserData(out ptr).Check();
                return ptr;
            }
            set
            {
                FmodGeometry.setUserData(value).Check();
            }
        }
        #endregion
    }

    public static class GeometryExtensions
    {
        public static FMOD.Geometry ToFmod(this Geometry geometry)
        {
            return geometry.FmodGeometry;
        }
    }
}
