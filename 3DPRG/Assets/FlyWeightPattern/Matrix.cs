using UnityEngine;

namespace FlyWeightPattern
{
    /// <summary>
    /// ��չ������
    /// </summary>
    public static class Matrix
    {
        /// <summary>
        /// ��չ��������ȡ��ת��Ϣ
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

            return Quaternion.LookRotation(forward, upwards);//LookRotationͬʱʹ��forward��upward����������ʱ�򣬾��൱��ָ����vz��vy����������
                                                             //������������������ֱ�������Ӧ��vx��Ȼ����������������ȥset��Ӧ��3��3��ת�����������
                                                             //���ɻ��һ����ת�����ٽ��ſ��Խ���ת������Ԫ����
        }
        /// <summary>
        /// ��չ��������ȡλ����Ϣ
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
        /// ��չ��������ȡ������Ϣ
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
            //����ж�
            if (Vector3.Cross(matrix.GetColumn(0), matrix.GetColumn(1)).normalized != (Vector3)matrix.GetColumn(2).normalized)
            {
                scale.x *= -1;
            }
            return scale;
        }
    }
}