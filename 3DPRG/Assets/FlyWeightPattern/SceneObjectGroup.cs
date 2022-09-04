using System;
using UnityEngine;

namespace FlyWeightPattern.Scripts
{
    [Serializable]
    public struct ObjectFlyweightData //享元部分
    {
        public Mesh mesh;//相同的mesh在导入模型后自然就只存在一份，享元中的共享就体现在这里
        public Material[] materials;//这里的材质也是一样的，仅存在一份，这是由模型资源决定的
    }

    [Serializable]
    public struct SceneObject
    {
        public ObjectFlyweightData flyweightData;
        public Matrix4x4 matrix; //4x矩阵，可以直接将PRS信息存储下来，unity的4x矩阵是按列优先填充，但其实访问的索引依然是行列。
        public string name;
        public string tag;
    }
    /// <summary>
    /// 场景中元素的数据
    /// </summary>
    public class SceneObjectGroup : ScriptableObject
    {
        public SceneObject[] SceneObjects; 
    }

}