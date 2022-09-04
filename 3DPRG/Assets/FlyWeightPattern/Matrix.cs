using UnityEngine;

namespace FlyWeightPattern
{
    /// <summary>
    /// 拓展方法类
    /// </summary>
    public static class Matrix
    {
        /// <summary>
        /// 拓展方法：获取旋转信息
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static Quaternion ExtractRotation(this Matrix4x4 matrix)
        {
            Vector3 forward;
            forward.x = matrix.m02;
            forward.y = matrix.m12;
            forward.z = matrix.m22;

            Vector3 upwards;
            upwards.x = matrix.m01;
            upwards.y = matrix.m11;
            upwards.z = matrix.m21;

            return Quaternion.LookRotation(forward, upwards);//LookRotation同时使用forward和upward两个参数的时候，就相当于指定了vz和vy两个向量，
                                                             //根据这两个向量可以直接算出对应的vx，然后再用这三个向量去set对应的3×3旋转矩阵的列向量
                                                             //即可获得一个旋转矩阵，再接着可以将其转换到四元数。
        }
        /// <summary>
        /// 拓展方法：获取位置信息
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static Vector3 ExtractPosition(this Matrix4x4 matrix)
        {
            Vector3 position;
            position.x = matrix.m03;
            position.y = matrix.m13;
            position.z = matrix.m23;
            return position;
        }
        
        /// <summary>
        /// 拓展方法：获取缩放信息
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static Vector3 ExtractScale(this Matrix4x4 matrix)
        {
            Vector3 scale = new Vector3(
                matrix.GetColumn(0).magnitude,
                matrix.GetColumn(1).magnitude,
                matrix.GetColumn(2).magnitude
            );
            //叉乘判断
            if (Vector3.Cross(matrix.GetColumn(0), matrix.GetColumn(1)).normalized != (Vector3)matrix.GetColumn(2).normalized)
            {
                scale.x *= -1;
            }
            return scale;
        }
    }
}