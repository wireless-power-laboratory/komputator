using System;
using System.Runtime.InteropServices;
using QuaternionObjects.Serialization;

namespace QuaternionObjects
{
    /// <summary>
    /// 4D homogeneous vector.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector4 : IParsable
    {
        public double x, y, z, w;

        #region Constructors

        /// <summary>
        ///		Creates a new 4 dimensional Vector.
        /// </summary>
        public Vector4(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }


        public Vector4(ParsedData data)
            : this(data.Text)
        {
        }
        private Vector4(string parsableText)
        {
            if (parsableText == null)
                throw new ArgumentException("The parsableText parameter cannot be null.");
            string[] vals = parsableText.TrimStart('(', '[', '<').TrimEnd(')', ']', '>').Split(',');
            if (vals.Length != 4)
                throw new FormatException(string.Format("Cannot parse the text '{0}' because it does not have 4 parts separated by commas in the form (x,y,z) with optional parenthesis.", parsableText));
            try
            {
                x = double.Parse(vals[0].Trim());
                y = double.Parse(vals[1].Trim());
                z = double.Parse(vals[2].Trim());
                w = double.Parse(vals[3].Trim());
            }
            catch (Exception e)
            {
                throw new FormatException("The parts of the vectors must be decimal numbers. " + e.Message);
            }
        }


        #endregion

        #region Methods


        /// <summary>
        ///     Calculates the dot (scalar) product of this vector with another.
        /// </summary>
        /// <param name="vec">
        ///     Vector with which to calculate the dot product (together with this one).
        /// </param>
        /// <returns>A double representing the dot product value.</returns>
        public double Dot(Vector4 vec)
        {
            return x * vec.x + y * vec.y + z * vec.z + w * vec.w;
        }

        #endregion Methods

        #region Operator overloads + CLS compliant method equivalents
        /// <summary>
        ///		Multiplies a Vector4 by a scalar value.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Vector4 operator *(Vector4 vector, double scalar)
        {
            Vector4 result = new Vector4();

            result.x = vector.x * scalar;
            result.y = vector.y * scalar;
            result.z = vector.z * scalar;
            result.w = vector.w * scalar;

            return result;
        }

        /// <summary>
        ///		User to compare two Vector4 instances for equality.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true or false</returns>
        public static bool operator ==(Vector4 left, Vector4 right)
        {
            return (left.x == right.x &&
                left.y == right.y &&
                left.z == right.z &&
                left.w == right.w);
        }

        /// <summary>
        ///		Used to negate the elements of a vector.
        /// </summary>
        /// <param name="left"></param>
        /// <returns></returns>
        public static Vector4 operator -(Vector4 left)
        {
            return new Vector4(-left.x, -left.y, -left.z, -left.w);
        }

        /// <summary>
        ///		User to compare two Vector4 instances for inequality.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>true or false</returns>
        public static bool operator !=(Vector4 left, Vector4 right)
        {
            return (left.x != right.x ||
                left.y != right.y ||
                left.z != right.z ||
                left.w != right.w);
        }

        /// <summary>
        ///		Used to access a Vector by index 0 = this.X, 1 = this.y, 2 = this.z, 3 = this.w.  
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
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return w;
                    default: throw new IndexOutOfRangeException();
                }
            }
            set
            {

                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    case 3: w = value; break;
                    default: throw new IndexOutOfRangeException();
                }
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
            return string.Format("({0}, {1}, {2}, {3})", this.x, this.y, this.z, this.w);
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
            return string.Format("({0}, {1}, {2}, {3})", (int)this.x, (int)this.y, (int)this.z, (int)this.w);
        }
        /// <summary>
        ///		Overrides the Object.ToString() method to provide a text representation of 
        ///		a Vector3.
        /// </summary>
        /// <returns>A string representation of a vector3.</returns>
        public string ToString(bool shortenDecmialPlaces)
        {
            if (shortenDecmialPlaces)
                return string.Format("({0:0.##}, {1:0.##} ,{2:0.##}, {3:0.##})", this.x, this.y, this.z, this.w);
            return ToString();
        }

        public static Vector4 Parse(string text)
        {
            return new Vector4(text);
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
            return this.x.GetHashCode() ^ this.y.GetHashCode() ^ this.z.GetHashCode() ^ this.w.GetHashCode();
        }

        /// <summary>
        ///		Compares this Vector to another object.  This should be done because the 
        ///		equality operators (==, !=) have been overriden by this class.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is Vector4 && this == (Vector4)obj;
        }

        #endregion
    }
}
