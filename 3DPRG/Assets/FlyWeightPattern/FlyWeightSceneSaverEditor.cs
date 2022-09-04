using System.Collections.Generic;
using FlyWeightPattern.Scripts;
using UnityEditor;
using UnityEngine;

namespace FlyWeightPattern.ScriptsEditor
{
    /// <summary>
    /// 创建编辑器面板
    /// </summary>
    [CustomEditor(typeof(FlyWeightSceneSaver))]
    public class FlyWeightSceneSaverEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            FlyWeightSceneSaver saver = target as FlyWeightSceneSaver;
            if (GUILayout.Button("生成"))
            {

                SceneObjectGroup sceneObjectGroup = ScriptableObject.CreateInstance<SceneObjectGroup>();//创建数据
                MeshRenderer[] list = GameObject.FindObjectsOfType<MeshRenderer>(true);
                if (list.Length == 0)
                {
                    EditorUtility.DisplayDialog("Sorrry", "你没有选中任何包含预制体的物体", "重选");
                    return;
                }
                List<SceneObject> sceneObjects = new List<SceneObject>();
                foreach (MeshRenderer meshRenderer in list)
                {
                    MeshFilter filter = meshRenderer.gameObject.GetComponent<MeshFilter>();
                    var transform = meshRenderer.transform;
                    var matrix = transform.localToWorldMatrix;
                    ObjectFlyweightData flyweightData = new ObjectFlyweightData()
                    {
                        materials = meshRenderer.sharedMaterials,
                        mesh = filter.sharedMesh,
                    };
                    sceneObjects.Add(new SceneObject()
                    {
                        flyweightData = flyweightData,
                        matrix = matrix,
                        name = meshRenderer.name,
                        tag=meshRenderer.tag,
                    });

                }

                sceneObjectGroup.SceneObjects = sceneObjects.ToArray();
                AssetDatabase.CreateAsset(sceneObjectGroup, "Assets/FlyWeightPattern/sceneObjGroup.asset");
                AssetDatabase.Refresh();


            }
        }
    }
}