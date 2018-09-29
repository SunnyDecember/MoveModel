﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.EventSystems;

/* Author		: Runing
** Time			: 18.9.28
** Describtion	: 
*/

namespace Runing
{
	public class MainScene : MonoBehaviour
	{
        public Transform modelRoot;

        private ReadConfig _readConfig;
        
        void Awake ()
		{
            TryAddScriptForNode();
        }

        async Task TryAddScriptForNode()
        {
            Transform[] modelNodeArray = TranGetChild(modelRoot);

            //把当前场景中所有模型的节点和配置表的节点比较，一样的节点名字便添加脚本和碰撞体。
            _readConfig = new ReadConfig();
            for (int i = 0; i < _readConfig.nodeDataList.Count; i++)
            {
                NodeData data = _readConfig.nodeDataList[i];
                Debug.Log(data.Name + "  " + data.Info);

                for (int j = 0; j < modelNodeArray.Length; j++)
                {
                    Transform modelNode = modelNodeArray[j];
                    if (modelNode.name.Trim() == data.Name)
                    {
                        ModelNode modelNodeObject = modelNode.gameObject.AddComponent<ModelNode>();
                        modelNodeObject.nodeData = data;
                    }

                    await Task.Delay(1);
                }
            }
        }

        /// <summary>
        /// 获取root模型下的所有节点
        /// </summary>
        /// <param name="tran"></param>
        /// <returns></returns>
        Transform[] TranGetChild(Transform tran)
        {
            Queue<Transform> result = new Queue<Transform>();
            Queue<Transform> queue = new Queue<Transform>();
            queue.Enqueue(tran);

            while (queue.Count > 0)
            {
                Transform front = queue.Dequeue();
                result.Enqueue(front);
                tryAddCollider(front);

                for (int i = 0; i < front.childCount; i++)
                {
                    queue.Enqueue(front.GetChild(i));
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// 尝试添加collider碰撞体
        /// </summary>
        /// <param name="tran"></param>
        void tryAddCollider(Transform tran)
        {
            if (null == tran)
                return;

            if (null != tran.GetComponent<MeshFilter>() && null == tran.GetComponent<MeshCollider>())
            {
                tran.gameObject.AddComponent<MeshCollider>();
            }
        }
        
        void Update()
        {
            //同时支持鼠标和touch触摸，因为不知道他们的触摸屏电脑是用那种方式。
            if (!EventSystem.current.IsPointerOverGameObject() && (Input.GetMouseButtonDown(0) || Input.touchCount > 0))
            {
                TrySetInfo();
            }
        }

        /// <summary>
        /// 判断是否点击到带有ModelNode脚本的节点。
        /// 当点击场景时候，如果点击的是ModelNode，那么需要显示info窗口。
        /// </summary>
        void TrySetInfo()
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out raycastHit))
            {
                ModelNode modelNode = raycastHit.transform.GetComponent<ModelNode>();
                if (null != modelNode)
                {
                    MainSceneUI.instance.SetInfoWin(modelNode.nodeData.Info);
                }
            }
        }
    }
}