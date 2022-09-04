using System;
using UnityEngine;

namespace FlyWeightPattern.Scripts
{
    [Serializable]
    public struct ObjectFlyweightData //��Ԫ����
    {
        public Mesh mesh;//��ͬ��mesh�ڵ���ģ�ͺ���Ȼ��ֻ����һ�ݣ���Ԫ�еĹ��������������
        public Material[] materials;//����Ĳ���Ҳ��һ���ģ�������һ�ݣ�������ģ����Դ������
    }

    [Serializable]
    public struct SceneObject
    {
        public ObjectFlyweightData flyweightData;
        public Matrix4x4 matrix; //4x���󣬿���ֱ�ӽ�PRS��Ϣ�洢������unity��4x�����ǰ���������䣬����ʵ���ʵ�������Ȼ�����С�
        public string name;
        public string tag;
    }
    /// <summary>
    /// ������Ԫ�ص�����
    /// </summary>
    public class SceneObjectGroup : ScriptableObject
    {
        public SceneObject[] SceneObjects; 
    }

}