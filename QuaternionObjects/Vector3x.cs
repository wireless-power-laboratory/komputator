using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using QuaternionObjects.Serialization;

namespace QuaternionObjects
{

    /// <summary>
    ///    A three-dimensional vector.
    /// </summary>
    /// <remarks>
    ///	    A direction in 3D space represented as distances along the 3 orthoganal axes (x, y, z). Note that positions, directions and scaling factors can be represented by a vector, depending on how the values are interpreted.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(Vector3Converter))]
    public struct Vector3 : ICustomTypeDescriptor, IParsable
    {
        #region Fields
        /// <summary>The X component.</summary>
        public double X;
        /// <summary>The Y component.</summary>
        public double Y;
        /// <summary>The Z component.</summary>
        public double Z;
        //public Complex X;
        //public Complex Y;
        //public Complex Z;
        // I don't think this would be allowed, given the architecture rule.

        private static readonly Vector3 positiveInfinityVector = new Vector3(double.PositiveInfinity, double.PositiveInfinity, double.PositiveInfinity);
        private static readonly Vector3 negativeInfinityVector = new Vector3(double.NegativeInfinity, double.NegativeInfinity, double.NegativeInfinity);
        private static readonly Vector3 invalidVector = new Vector3(double.NaN, double.NaN, double.NaN);
        private static readonly Vector3 zeroVector = new Vector3(0.0f, 0.0f, 0.0f);
        private static readonly Vector3 unitX = new Vector3(1.0f, 0.0f, 0.0f);
        private static readonly Vector3 unitY = new Vector3(0.0f, 1.0f, 0.0f);
        private static readonly Vector3 unitZ = new Vector3(0.0f, 0.0f, 1.0f);
        private static readonly Vector3 negativeUnitX = new Vector3(-1.0f, 0.0f, 0.0f);
        private static readonly Vector3 negativeUnitY = new Vector3(0.0f, -1.0f, 0.0f);
        private static readonly Vector3 negativeUnitZ = new Vector3(0.0f, 0.0f, -1.0f);
        private static readonly Vector3 unitVector = new Vector3(1.0f, 1.0f, 1.0f);

        public static Vector3 PositiveInfinity { get { return positiveInfinityVector; } }
        public static Vector3 NegativeInfinity { get { return negativeInfinityVector; } }
        public static Vector3 Invalid { get { return invalidVector; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new 3 dimensional Vector.
        /// </summary>
        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        //public Vector3(Complex xC, Complex yC, Complex zC)
        //{
        //    X = new Complex();
        //    X = xC;
        //    Y = new Complex();
        //    Y = yC;
        //    Z = new Complex();
        //    Z = zC;
        //}


        /// <summary>
        ///	Creates a new 3 dimensional Vector.
        /// </summary>
        public Vector3(double unitDimension)
            : this(unitDimension, unitDimension, unitDimension)
        {
        }

        public Vector3(ParsedData data)
            : this(data.Text)
        {
        }
        private Vector3(string parsableText)
        {
            if (parsableText == null)
                throw new ArgumentException("The parsableText parameter cannot be null.");
            string[] vals = parsableText.TrimStart('(', '[', '<').TrimEnd(')', ']', '>').Split(',');
            if (vals.Length != 3)
                throw new FormatException(string.Format("Cannot parse the text '{0}' because it does not have 3 parts separated by commas in the form (x,y,z) with optional parenthesis.", parsableText));
            try
            {
                X = double.Parse(vals[0].Trim());
                Y = double.Parse(vals[1].Trim());
                Z = double.Parse(vals[2].Trim());
            }
            catch (Exception e)
            {
                throw new FormatException("The parts of the vectors must be decimal numbers. "+e.Message);
            }
        }

        /// <summary>
        ///	Creates a new 3 dimensional Vector.
        /// </summary>
        public Vector3(double[] coordinates)
        {
            if (coordinates.Length != 3)
                throw new ArgumentException("The coordinates array must be of length 3 to specify the x, y, and z coordinates.");
            X = coordinates[0];
            Y = coordinates[1];
            Z = coordinates[2];
        }


        #endregion

        #region Overloaded operators + CLS compliant method equivalents

        /// <summary>
        ///		User to compare two Vector3 instances for equality.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true or false</returns>
        public static bool operator ==(Vector3 left, Vector3 right)
        {
            return (left.X == right.X && left.Y == right.Y && left.Z == right.Z);
        }

        /// <summary>
        ///		User to compare two Vector3 instances for inequality.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true or false</returns>
        public static bool operator !=(Vector3 left, Vector3 right)
        {
            return (left.X != right.X || left.Y != right.Y || left.Z != right.Z);
        }

        /// <summary>
        ///		Used when a Vector3 is multiplied by another vector.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 Multiply(Vector3 left, Vector3 right)
        {
            return left * right;
        }

        /// <summary>
        ///		Used when a Vector3 is multiplied by another vector.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 operator *(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
        }

        /// <summary>
        /// Used to divide a vector by a scalar value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vector3 Divide(Vector3 left, double scalar)
        {
            return left / scalar;
        }

        /// <summary>
        ///		Used when a Vector3 is divided by another vector.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 operator /(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
        }

        /// <summary>
        ///		Used when a Vector3 is divided by another vector.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 Divide(Vector3 left, Vector3 right)
        {
            return left / right;
        }

        /// <summary>
        /// Used to divide a vector by a scalar value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vector3 operator /(Vector3 left, double scalar)
        {
            Debug.Assert(scalar != 0.0f, "Cannot divide a Vector3 by zero.");

            Vector3 vector = new Vector3();

            // get the inverse of the scalar up front to avoid doing multiple divides later
            double inverse = 1.0f / scalar;

            vector.X = left.X * inverse;
            vector.Y = left.Y * inverse;
            vector.Z = left.Z * inverse;

            return vector;
        }

        /// <summary>
        ///		Used when a Vector3 is added to another Vector3.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 Add(Vector3 left, Vector3 right)
        {
            return left + right;
        }

        /// <summary>
        ///		Used when a Vector3 is added to another Vector3.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        /// <summary>
        ///		Used when a Vector3 is multiplied by a scalar value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vector3 Multiply(Vector3 left, double scalar)
        {
            return left * scalar;
        }

        /// <summary>
        ///		Used when a Vector3 is multiplied by a scalar value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vector3 operator *(Vector3 left, double scalar)
        {
            return new Vector3(left.X * scalar, left.Y * scalar, left.Z * scalar);
        }

        /// <summary>
        ///		Used when a scalar value is multiplied by a Vector3.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 Multiply(double scalar, Vector3 right)
        {
            return scalar * right;
        }

        /// <summary>
        ///		Used when a scalar value is multiplied by a Vector3.
        /// </summary>
        /// <param name="scalar"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 operator *(double scalar, Vector3 right)
        {
            return new Vector3(right.X * scalar, right.Y * scalar, right.Z * scalar);
        }

        /// <summary>
        ///		Used to subtract a Vector3 from another Vector3.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 Subtract(Vector3 left, Vector3 right)
        {
            return left - right;
        }

        /// <summary>
        ///		Used to subtract a Vector3 from another Vector3.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }


        /// <summary>
        ///		Used to negate the elements of a vector.
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        public static Vector3 Negate(Vector3 left)
        {
            return -left;
        }

        /// <summary>
        ///		Used to negate the elements of a vector.
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 left)
        {
            return new Vector3(-left.X, -left.Y, -left.Z);
        }

        /// <summary>
        ///    Returns true if the vector's scalar components are all smaller
        ///    that the ones of the vector it is compared against.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator >(Vector3 left, Vector3 right)
        {
            return (left.X > right.X && left.Y > right.Y && left.Z > right.Z);
        }

        /// <summary>
        ///    Returns true if the vector's scalar components are all greater
        ///    that the ones of the vector it is compared against.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator <(Vector3 left, Vector3 right)
        {
            return (left.X < right.X && left.Y < right.Y && left.Z < right.Z);
        }

        /// <summary>
        ///    
        /// </summary>
        /// <param name="vec3"></param>
        /// <returns></returns>
        public static explicit operator Vector4(Vector3 vec3)
        {
            return new Vector4(vec3.X, vec3.Y, vec3.Z, 1.0f);
        }

        /// <summary>
        ///		Used to access a Vector by index 0 = x, 1 = y, 2 = z.  
        /// </summary>
        /// <remarks>
        ///		Uses unsafe pointer arithmetic to reduce the code required.
        ///	</remarks>
        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return X;
                    case 1: return Y;
                    case 2: return Z;
                    default:
                        throw new ArgumentOutOfRangeException("Indexer boundaries overrun in Vector3, index must be from 0 to 2");
                }
            }
            set
            {
                switch (index)
                {
                    case 0: X = value; break;
                    case 1: Y = value; break;
                    case 2: Z = value; break;
                    default:
                        throw new ArgumentOutOfRangeException("Indexer boundaries overrun in Vector3, index must be from 0 to 2");
                }
            }
        }

        #endregion

        #region Public methods

        public object[] ToObjectArray()
        {
            return new object[] { X, Y, Z };
        }
        public double[] ToArray()
        {
            return new double[] { X, Y, Z };
        }

        //public void KeepLessThan(double minX, double minY, double minZ)
        //{
        //    if (!double.IsNaN(minX) && x < minX) this.X = x;
        //    if (!double.IsNaN(minY) && y < minY) this.y = y;
        //    if (!double.IsNaN(minZ) && z < minZ) this.z = z;
        //}
        //public void KeepGreaterThan(double maxX, double maxY, double maxZ)
        //{
        //    if (!double.IsNaN(maxX) && x > maxX) this.X = x;
        //    if (!double.IsNaN(maxY) && y > maxY) this.y = y;
        //    if (!double.IsNaN(maxZ) && z > maxZ) this.z = z;
        //}

        public bool IsAnyComponentGreaterThan(Vector3 vector)
        {
            return (X > vector.X || Y > vector.Y || Z > vector.Z);
        }

        public bool IsAnyComponentGreaterThanOrEqualTo(Vector3 vector)
        {
            return (X >= vector.X || Y >= vector.Y || Z >= vector.Z);
        }

        public bool IsAnyComponentLessThan(Vector3 vector)
        {
            return (X < vector.X || Y < vector.Y || Z < vector.Z);
        }
        public bool IsAnyComponentLessThanOrEqualTo(Vector3 vector)
        {
            return (X <= vector.X || Y <= vector.Y || Z <= vector.Z);
        }

        public Vector3 Offset(double x, double y, double z)
        {
            return new Vector3(X + x, Y + y, Z + z);
        }

        /// <summary>
        ///		Performs a Dot Product operation on 2 vectors, which produces the angle between them.
        /// </summary>
        /// <param name="vector">The vector to perform the Dot Product against.</param>
        /// <returns>The angle between the 2 vectors.</returns>
        public double Dot(Vector3 vector)
        {
            return (double)X * vector.X + Y * vector.Y + Z * vector.Z;
        }

        /// <summary>
        ///		Performs a Cross Product operation on 2 vectors, which returns a vector that is perpendicular
        ///		to the intersection of the 2 vectors.  Useful for finding face normals.
        /// </summary>
        /// <param name="vector">A vector to perform the Cross Product against.</param>
        /// <returns>A new Vector3 perpedicular to the 2 original vectors.</returns>
        public Vector3 Cross(Vector3 vector)
        {
            return new Vector3(
                (Y * vector.Z) - (Z * vector.Y),
                (Z * vector.X) - (X * vector.Z),
                (X * vector.Y) - (Y * vector.X)
                );

        }

        /// <summary>
        ///		Finds a vector perpendicular to this one.
        /// </summary>
        /// <returns></returns>
        public Vector3 Perpendicular()
        {
            Vector3 result = Cross(UnitX);

            // check length
            if (result.LengthSquared < double.Epsilon)
            {
                // This vector is the Y axis multiplied by a scalar, so we have to use another axis
                result = Cross(UnitY);
            }

            return result;
        }

        public static Vector3 SymmetricRandom()
        {
            return SymmetricRandom(1f, 1f, 1f);
        }

        public static Vector3 SymmetricRandom(Vector3 maxComponentMagnitude)
        {
            return SymmetricRandom(maxComponentMagnitude.X, maxComponentMagnitude.Y, maxComponentMagnitude.Z);
        }

        public static Vector3 SymmetricRandom(double maxComponentMagnitude)
        {
            return SymmetricRandom(maxComponentMagnitude, maxComponentMagnitude, maxComponentMagnitude);
        }

        public static Vector3 SymmetricRandom(double xMult, double yMult, double zMult)
        {
            return new Vector3(
                (xMult == 0) ? 0 : xMult * Functions.SymmetricRandom(),
                (yMult == 0) ? 0 : yMult * Functions.SymmetricRandom(),
                (zMult == 0) ? 0 : zMult * Functions.SymmetricRandom()
                );
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="up"></param>
        /// <returns></returns>
        public Vector3 RandomDeviant(double angle, Vector3 up)
        {
            Vector3 newUp = Zero;

            if (up == Zero)
                newUp = Perpendicular();
            else
                newUp = up;

            // rotate up vector by random amount around this
            Quaternion_big q = Quaternion_big.FromAngleAxis(Functions.UnitRandom() * Functions.TWO_PI, this);
            newUp = q * newUp;

            // finally, rotate this by given angle around randomized up vector
            q = Quaternion_big.FromAngleAxis(angle, newUp);

            return q * this;
        }

        /// <summary>
        ///		Finds the midpoint between the supplied Vector and this vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public Vector3 MidPoint(Vector3 vector)
        {
            return new Vector3((X + vector.X) / 2f,
                (Y + vector.Y) / 2f,
                (Z + vector.Z) / 2f);
        }

        /// <summary>
        ///		Compares the supplied vector and updates it's x/y/z components of they are higher in value.
        /// </summary>
        /// <param name="compare"></param>
        public void Ceil(Vector3 compare)
        {
            if (compare.X > X)
                X = compare.X;
            if (compare.Y > Y)
                Y = compare.Y;
            if (compare.Z > Z)
                Z = compare.Z;
        }

        /// <summary>
        ///		Compares the supplied vector and updates it's x/y/z components of they are lower in value.
        /// </summary>
        /// <param name="compare"></param>
        /// <returns></returns>
        public void Floor(Vector3 compare)
        {
            if (compare.X < X)
                X = compare.X;
            if (compare.Y < Y)
                Y = compare.Y;
            if (compare.Z < Z)
                Z = compare.Z;
        }

        /// <summary>
        ///		Gets the shortest arc quaternion to rotate this vector to the destination vector. 
        /// </summary>
        /// <remarks>
        ///		Don't call this if you think the dest vector can be close to the inverse
        ///		of this vector, since then ANY axis of rotation is ok.
        ///	</remarks>
        public Quaternion_big GetRotationTo(Vector3 destination)
        {
            // Based on Stan Melax's article in Game Programming Gems
            Quaternion_big q = new Quaternion_big();

            Vector3 v0 = new Vector3(X, Y, Z);
            Vector3 v1 = destination;

            // normalize both vectors 
            v0.Normalize();
            v1.Normalize();

            // get the cross product of the vectors
            Vector3 c = v0.Cross(v1);

            // If the cross product approaches zero, we get unstable because ANY axis will do
            // when v0 == -v1
            double d = v0.Dot(v1);

            // If dot == 1, vectors are the same
            if (d >= 1.0f)
            {
                return Quaternion_big.Identity;
            }

            double s = Functions.Sqrt((1 + d) * 2);
            double inverse = 1 / s;

            q.X = c.X * inverse;
            q.Y = c.Y * inverse;
            q.Z = c.Z * inverse;
            q.W = s * 0.5f;

            return q;
        }

        public Vector3 ToNormalized()
        {
            Vector3 vec = this;
            vec.Normalize();
            return vec;
        }

        /// <summary>
        ///		Normalizes the vector.
        /// </summary>
        /// <remarks>
        ///		This method normalises the vector such that it's
        ///		length / magnitude is 1. The result is called a unit vector.
        ///		<p/>
        ///		This function will not crash for zero-sized vectors, but there
        ///		will be no changes made to their components.
        ///	</remarks>
        ///	<returns>The previous length of the vector.</returns>
        public double Normalize()
        {
            double length = Functions.Sqrt(X * X + Y * Y + Z * Z);

            // Will also work for zero-sized vectors, but will change nothing
            if (length > double.Epsilon)
            {
                double inverseLength = 1.0f / length;

                X *= inverseLength;
                Y *= inverseLength;
                Z *= inverseLength;
            }

            return length;
        }

        /// <summary>
        ///    Calculates a reflection vector to the plane with the given normal.
        /// </summary>
        /// <remarks>
        ///    Assumes this vector is pointing AWAY from the plane, invert if not.
        /// </remarks>
        /// <param name="normal">Normal vector on which this vector will be reflected.</param>
        /// <returns></returns>
        public Vector3 Reflect(Vector3 normal)
        {
            return this - 2 * Dot(normal) * normal;
        }

        #endregion

        #region Public properties
        public bool IsZero { get { return X == 0f && Y == 0f && Z == 0f; } }

        /// <summary>
        ///    Gets the length (magnitude) of this Vector3.  The Sqrt operation is expensive, so 
        ///    only use this if you need the exact length of the Vector.  If vector lengths are only going
        ///    to be compared, use LengthSquared instead.
        /// </summary>
        public double Length
        {
            get
            {
                return Functions.Sqrt(X * X + Y * Y + Z * Z);
            }
        }

        /// <summary>
        ///    Returns the length (magnitude) of the vector squared.
        /// </summary>
        public double LengthSquared
        {
            get
            {
                return (X * X + Y * Y + Z * Z);
            }
        }

        #endregion

        #region Static Constant Properties

        /// <summary>
        ///		Gets a Vector3 with all components set to 0.
        /// </summary>
        public static Vector3 Zero
        {
            get
            {
                return zeroVector;
            }
        }

        /// <summary>
        ///		Gets a Vector3 with all components set to 1.
        /// </summary>
        public static Vector3 UnitScale
        {
            get
            {
                return unitVector;
            }
        }

        /// <summary>
        ///		Gets a Vector3 with the X set to 1, and the others set to 0.
        /// </summary>
        public static Vector3 UnitX
        {
            get
            {
                return unitX;
            }
        }

        /// <summary>
        ///		Gets a Vector3 with the Y set to 1, and the others set to 0.
        /// </summary>
        public static Vector3 UnitY
        {
            get
            {
                return unitY;
            }
        }

        /// <summary>
        ///		Gets a Vector3 with the Z set to 1, and the others set to 0.
        /// </summary>
        public static Vector3 UnitZ
        {
            get
            {
                return unitZ;
            }
        }

        /// <summary>
        ///		Gets a Vector3 with the X set to -1, and the others set to 0.
        /// </summary>
        public static Vector3 NegativeUnitX
        {
            get
            {
                return negativeUnitX;
            }
        }

        /// <summary>
        ///		Gets a Vector3 with the Y set to -1, and the others set to 0.
        /// </summary>
        public static Vector3 NegativeUnitY
        {
            get
            {
                return negativeUnitY;
            }
        }

        /// <summary>
        ///		Gets a Vector3 with the Z set to -1, and the others set to 0.
        /// </summary>
        public static Vector3 NegativeUnitZ
        {
            get
            {
                return negativeUnitZ;
            }
        }

        #endregion

        #region Object overloads

        /// <summary>
        ///		Overrides the Object.ToString() method to provide a text representation of 
        ///		a Vector3.
        /// </summary>
        /// <returns>A string representation of a vector3.</returns>
        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", X, Y, Z);
        }

        public string ToParsableText()
        {
            return ToString();
        }


        /// <summary>
        ///		Overrides the Object.ToString() method to provide a text representation of 
        ///		a Vector3.
        /// </summary>
        /// <returns>A string representation of a vector3.</returns>
        public string ToIntegerString()
        {
            return string.Format("({0}, {1}, {2})", (int)X, (int)Y, (int)Z);
        }
        /// <summary>
        ///		Overrides the Object.ToString() method to provide a text representation of 
        ///		a Vector3.
        /// </summary>
        /// <returns>A string representation of a vector3.</returns>
        public string ToString(bool shortenDecmialPlaces)
        {
            if (shortenDecmialPlaces)
                return string.Format("({0:0.##}, {1:0.##} ,{2:0.##})", X, Y, Z);
            return ToString();
        }

        public static Vector3 Parse(string text)
        {
            return new Vector3(text);
        }

        /// <summary>
        ///		Provides a unique hash code based on the member variables of this
        ///		class.  This should be done because the equality operators (==, !=)
        ///		have been overriden by this class.
        ///		<p/>
        ///		The standard implementation is a simple XOR operation between all local
        ///		member variables.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }

        /// <summary>
        ///		Compares this Vector to another object.  This should be done because the 
        ///		equality operators (==, !=) have been overriden by this class.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return (obj is Vector3) && (this == (Vector3)obj);
        }

        #endregion

        #region ICustomTypeDescriptor

        #region Public Methods

        object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
        {
            return this; // properties belong to the this object
        }


        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
        {
            return ((ICustomTypeDescriptor)this).GetProperties(null);
        }

        PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(
            Attribute[] attributes)
        {
            // Create the property collection and filter
            return new PropertyDescriptorCollection(
            new PropertyDescriptor[] {	 new FieldPropertyDescriptor(this,"x"),
										 new FieldPropertyDescriptor(this,"y"),
										 new FieldPropertyDescriptor(this,"z")});
        }


        #region Delegated to TypeDescriptor

        AttributeCollection ICustomTypeDescriptor.GetAttributes()
        {
            // Gets the attributes of the this object
            return TypeDescriptor.GetAttributes(this, true);
        }

        string ICustomTypeDescriptor.GetClassName()
        {
            // Gets the class name of the this object
            return TypeDescriptor.GetClassName(this, true);
        }

        TypeConverter ICustomTypeDescriptor.GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
        {
            return ((ICustomTypeDescriptor)this).GetEvents(null);
        }

        string ICustomTypeDescriptor.GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        #endregion
        #endregion
        #endregion
    }
}

