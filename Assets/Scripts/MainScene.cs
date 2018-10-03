using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using System;

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
        
        void Start ()
		{
            TryAddScriptForNode();
        }

        /// <summary>
        /// 待优化 todo
        /// </summary>
        /// <returns></returns>
        void TryAddScriptForNode()
        {
            //把当前场景中所有模型的节点和配置表的节点比较，一样的节点名字便添加脚本和碰撞体。
            _readConfig = new ReadConfig();
            Dictionary<string, NodeData> nodeDataDic = _readConfig.nodeDataDic;
            Transform[] array = modelRoot.GetComponentsInChildren<Transform>();

            foreach (Transform modelNode in array)
            {
                if (nodeDataDic.ContainsKey(modelNode.name))
                {
                    NodeData data = nodeDataDic[modelNode.name];
                    ModelNode modelNodeObject = modelNode.gameObject.AddComponent<ModelNode>();
                    modelNodeObject.nodeData = data;
                    MainSceneUI.instance.CreateGridUI(data, modelNodeObject.transform.position, modelNodeObject.transform.rotation);
                }
            }
        }

        /// <summary>
        /// 获取root模型下的所有节点
        /// </summary>
        /// <param name="tran"></param>
        /// <returns></returns>
        //Transform[] TranGetChild(Transform tran)
        //IEnumerable TranGetChild(Transform tran)
        //{
        //    Queue<Transform> result = new Queue<Transform>();
        //    Queue<Transform> queue = new Queue<Transform>();
        //    queue.Enqueue(tran);

        //    while (queue.Count > 0)
        //    {
        //        Transform front = queue.Dequeue();
        //        yield return front;
        //        //result.Enqueue(front);
        //        //tryAddCollider(front);

        //        for (int i = 0; i < front.childCount; i++)
        //        {
        //            queue.Enqueue(front.GetChild(i));
        //        }
        //    }
        //}

        /// <summary>
        /// 尝试添加collider碰撞体
        /// </summary>
        /// <param name="tran"></param>
        //void tryAddCollider(Transform tran)
        //{
        //    if (null == tran)
        //        return;

        //    if (null != tran.GetComponent<MeshFilter>() && null == tran.GetComponent<BoxCollider>())
        //    {
        //        tran.gameObject.AddComponent<BoxCollider>();
        //    }
        //}
        
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
                    MainSceneUI.instance.SetInfoWin(modelNode.nodeData);
                }
            }
        }
    }
}