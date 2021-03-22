/* Raymath.cs
*
* Copyright 2019 Chris Dill
*
* Release under zLib License.
* See LICENSE for details.
*/

using System.Runtime.InteropServices;
using Quaternion = Raylib.RLVector4;

namespace Raylib
{
    // NOTE: Helper types to be used instead of array return types for *ToFloat functions
    public unsafe struct float3
    {
        public fixed float v[3];
    }

    public unsafe struct float16
    {
        public fixed float v[16];
    }

    public static partial class Raylib
    {
        // Clamp float value
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Clamp(float value, float min, float max);

        // Calculate linear interpolation between two vectors
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Lerp(float start, float end, float amount);

        // Vector with components value 0.0f
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern RLVector2 Vector2Zero();

        // Vector with components value 1.0f
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern RLVector2 Vector2One();

        // Add two vectors (v1 + v2)
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern RLVector2 Vector2Add(RLVector2 v1, RLVector2 v2);

        // Subtract two vectors (v1 - v2)
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern RLVector2 Vector2Subtract(RLVector2 v1, RLVector2 v2);

        // Calculate vector length
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern float Vector2Length(RLVector2 v);

        // Calculate two vectors dot product
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern float Vector2DotProduct(RLVector2 v1, RLVector2 v2);

        // Calculate distance between two vectors
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern float Vector2Distance(RLVector2 v1, RLVector2 v2);

        // Calculate angle from two vectors in X-axis
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern float Vector2Angle(RLVector2 v1, RLVector2 v2);

        // Scale vector (multiply by value)
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern RLVector2 Vector2Scale(RLVector2 v, float scale);

        // Multiply vector by vector
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern RLVector2 Vector2MultiplyV(RLVector2 v1, RLVector2 v2);

        // Negate vector
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern RLVector2 Vector2Negate(RLVector2 v);

        // Divide vector by a float value
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern RLVector2 Vector2Divide(RLVector2 v, float div);

        // Divide vector by vector
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern RLVector2 Vector2DivideV(RLVector2 v1, RLVector2 v2);

        // Normalize provided vector
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern RLVector2 Vector2Normalize(RLVector2 v);

        // Calculate linear interpolation between two vectors
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
		public static extern RLVector2 Vector2Lerp(RLVector2 v1, RLVector2 v2, float amount);

        // Vector with components value 0.0f
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 Vector3Zero();

        // Vector with components value 1.0f
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 Vector3One();

        // Add two vectors
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 Vector3Add(RLVector3 v1, RLVector3 v2);

        // Subtract two vectors
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 Vector3Subtract(RLVector3 v1, RLVector3 v2);

        // Multiply vector by scalar
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 Vector3Multiply(RLVector3 v, float scalar);

        // Multiply vector by vector
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 Vector3MultiplyV(RLVector3 v1, RLVector3 v2);

        // Calculate two vectors cross product
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 Vector3CrossProduct(RLVector3 v1, RLVector3 v2);

        // Calculate one vector perpendicular vector
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 Vector3Perpendicular(RLVector3 v);

        // Calculate vector length
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Vector3Length(RLVector3 v);

        // Calculate two vectors dot product

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Vector3DotProduct(RLVector3 v1, RLVector3 v2);

        // Calculate distance between two vectors
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float Vector3Distance(RLVector3 v1, RLVector3 v2);

        // Scale provided vector

        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 Vector3Scale(RLVector3 v, float scale);

        // Negate provided vector (invert direction)
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 Vector3Negate(RLVector3 v);

        // Divide vector by a float value
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 Vector3Divide(RLVector3 v, float div);

        // Divide vector by vector
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 Vector3DivideV(RLVector3 v1, RLVector3 v2);

        // Normalize provided vector
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 Vector3Normalize(RLVector3 v);

        // Orthonormalize provided vectors
        // Makes vectors normalized and orthogonal to each other
        // Gram-Schmidt function implementation
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Vector3OrthoNormalize(ref RLVector3 v1, ref RLVector3 v2);

        // Transforms a Vector3 by a given Matrix
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 Vector3Transform(RLVector3 v, RLMatrix mat);

        // Transform a vector by quaternion rotation
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 Vector3RotateByQuaternion(RLVector3 v, Quaternion q);

        // Calculate linear interpolation between two vectors
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 Vector3Lerp(RLVector3 v1, RLVector3 v2, float amount);

        // Calculate reflected vector to normal
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 Vector3Reflect(RLVector3 v, RLVector3 normal);

        // Return min value for each pair of components
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 Vector3Min(RLVector3 v1, RLVector3 v2);

        // Return max value for each pair of components
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 Vector3Max(RLVector3 v1, RLVector3 v2);

        // Compute barycenter coordinates (u, v, w) for point p with respect to triangle (a, b, c)
        // NOTE: Assumes P is on the plane of the triangle
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 Vector3Barycenter(RLVector3 p, RLVector3 a, RLVector3 b, RLVector3 c);

        // Returns Vector3 as float array
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float3 Vector3ToFloatV(RLVector3 v);

        // Compute matrix determinant
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float MatrixDeterminant(RLMatrix mat);

        // Returns the trace of the matrix (sum of the values along the diagonal)
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float MatrixTrace(RLMatrix mat);

        // Transposes provided matrix
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLMatrix MatrixTranspose(RLMatrix mat);

        // Invert provided matrix
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLMatrix MatrixInvert(RLMatrix mat);

        // Normalize provided matrix
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLMatrix MatrixNormalize(RLMatrix mat);

        // Returns identity matrix
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLMatrix MatrixIdentity();

        // Add two matrices
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLMatrix MatrixAdd(RLMatrix left, RLMatrix right);

        // Subtract two matrices (left - right)
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLMatrix MatrixSubtract(RLMatrix left, RLMatrix right);

        // Returns translation matrix
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLMatrix MatrixTranslate(float x, float y, float z);

        // Create rotation matrix from axis and angle
        // NOTE: Angle should be provided in radians
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLMatrix MatrixRotate(RLVector3 axis, float angle);

        // Returns x-rotation matrix (angle in radians)
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLMatrix MatrixRotateX(float angle);

        // Returns y-rotation matrix (angle in radians)
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLMatrix MatrixRotateY(float angle);

        // Returns z-rotation matrix (angle in radians)
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLMatrix MatrixRotateZ(float angle);

        // Returns scaling matrix
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLMatrix MatrixScale(float x, float y, float z);

        // Returns two matrix multiplication
        // NOTE: When multiplying matrices... the order matters!
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLMatrix MatrixMultiply(RLMatrix left, RLMatrix right);

        // Returns perspective projection matrix
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLMatrix MatrixFrustum(double left, double right, double bottom, double top, double near, double far);

        // Returns perspective projection matrix
        // NOTE: Angle should be provided in radians
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLMatrix MatrixPerspective(double fovy, double aspect, double near, double far);

        // Returns orthographic projection matrix
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLMatrix MatrixOrtho(double left, double right, double bottom, double top, double near, double far);

        // Returns camera look-at matrix (view matrix)
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLMatrix MatrixLookAt(RLVector3 eye, RLVector3 target, RLVector3 up);

        // Returns float array of matrix data
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float16 MatrixToFloatV(RLMatrix mat);

        // Returns identity quaternion
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Quaternion QuaternionIdentity();

        // Computes the length of a quaternion
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern float QuaternionLength(Quaternion q);

        // Normalize provided quaternion
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Quaternion QuaternionNormalize(Quaternion q);

        // Invert provided quaternion
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Quaternion QuaternionInvert(Quaternion q);

        // Calculate two quaternion multiplication
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Quaternion QuaternionMultiply(Quaternion q1, Quaternion q2);

        // Calculate linear interpolation between two quaternions
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Quaternion QuaternionLerp(Quaternion q1, Quaternion q2, float amount);

        // Calculate slerp-optimized interpolation between two quaternions
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Quaternion QuaternionNlerp(Quaternion q1, Quaternion q2, float amount);

        // Calculates spherical linear interpolation between two quaternions
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Quaternion QuaternionSlerp(Quaternion q1, Quaternion q2, float amount);

        // Calculate quaternion based on the rotation from one vector to another
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Quaternion QuaternionFromVector3ToVector3(RLVector3 from, RLVector3 to);

        // Returns a quaternion for a given rotation matrix
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Quaternion QuaternionFromMatrix(RLMatrix mat);

        // Returns a matrix for a given quaternion
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLMatrix QuaternionToMatrix(Quaternion q);

        // Returns rotation quaternion for an angle and axis
        // NOTE: angle must be provided in radians
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Quaternion QuaternionFromAxisAngle(RLVector3 axis, float angle);

        // Returns the rotation angle and axis for a given quaternion
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void QuaternionToAxisAngle(Quaternion q, ref RLVector3 outAxis, ref float outAngle);

        // Returns he quaternion equivalent to Euler angles
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Quaternion QuaternionFromEuler(float roll, float pitch, float yaw);

        // Return the Euler angles equivalent to quaternion (roll, pitch, yaw)
        // NOTE: Angles are returned in a Vector3 struct in degrees
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern RLVector3 QuaternionToEuler(Quaternion q);

        // Transform a quaternion given a transformation matrix
        [DllImport(nativeLibName, CallingConvention = CallingConvention.Cdecl)]
        public static extern Quaternion QuaternionTransform(Quaternion q, RLMatrix mat);
    }
}
