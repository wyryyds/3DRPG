using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FlyWeightPattern.Scripts
{
    public class FlyWeightInstanceGeneration : MonoBehaviour
    {
        [SerializeField]
        public SceneObjectGroup sceneObjectGroup;
        public int batchCount = 10; //合批数量


        private GameObject _Root; //父节点


        private void Awake()
        {
            StartCoroutine(StartGenerate());
        }
        /// <summary>
        /// 加载场景的协程
        /// </summary>
        /// <returns></returns>
        private IEnumerator StartGenerate()
        {
            int initCount = 0;
            int totalCount = 0;

            if (_Root != null)
            {
                GameObject.Destroy(_Root);
            }
            _Root = new GameObject("root");

            while (initCount < batchCount)
            {
                initCount++;
                yield return null;
                var sceneObjects = sceneObjectGroup.SceneObjects[totalCount];
                var go = new GameObject(sceneObjects.name);

                go.tag = sceneObjects.tag;
                MeshRenderer renderer = go.AddComponent<MeshRenderer>();
                MeshFilter filter = go.AddComponent<MeshFilter>();
                MeshCollider meshCollider = go.AddComponent<MeshCollider>();

                renderer.sharedMaterials = sceneObjects.flyweightData.materials;
                go.transform.rotation = sceneObjects.matrix.ExtractRotation();
                go.transform.position = sceneObjects.matrix.ExtractPosition();
                go.transform.localScale = sceneObjects.matrix.ExtractScale();
                go.transform.SetParent(_Root.transform, true);
                filter.mesh = sceneObjects.flyweightData.mesh;
                meshCollider.sharedMesh = sceneObjects.flyweightData.mesh;
                totalCount++;

                if (totalCount >= sceneObjectGroup.SceneObjects.Length) break;

                if (initCount == batchCount)initCount = 0;
                
            }
        }

    }
}