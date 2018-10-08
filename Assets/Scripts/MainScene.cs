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
        class PlayerPosition
        {
            public Vector3 position;
            public Quaternion rotation;
        }

        public Transform modelRoot;

        public Player player;

        private ReadConfig _readConfig;

        public static MainScene instance;

        private Dictionary<int, PlayerPosition> _floorPositionMap = new Dictionary<int, PlayerPosition>();

        private int _floorIndex = 1;
             
        void Awake()
        {
            instance = this;

            //初始化一楼的位置
            PlayerPosition pos1 = new PlayerPosition();
            pos1.position = new Vector3(-11.275f, 1.079f, 2.131f);
            pos1.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            _floorPositionMap.Add(1, pos1);

            //初始化二楼位置
            PlayerPosition pos2 = new PlayerPosition();
            pos2.position = new Vector3(-14.8323f, 4.15f, 2.131f);
            pos2.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
            _floorPositionMap.Add(2, pos2);

            //初始化角色位置
            UpdatePlayerPosition(_floorIndex);
        }

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
            Transform[] array = TranGetChild(modelRoot);

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
        //void TranGetChild(Transform tran)
        //{
        //    Queue<Transform> result = new Queue<Transform>();
        //    Queue<Transform> queue = new Queue<Transform>();
        //    queue.Enqueue(tran);

        //    while (queue.Count > 0)
        //    {
        //        Transform front = queue.Dequeue();
        //        yield return front;
        //        result.Enqueue(front);
        //        //tryAddCollider(front);

        //        for (int i = 0; i < front.childCount; i++)
        //        {
        //            queue.Enqueue(front.GetChild(i));
        //        }
        //    }

        //    //return result.ToArray();
        //}
        Transform[] TranGetChild(Transform tran)
        {
            Queue<Transform> result = new Queue<Transform>();
            Queue<Transform> queue = new Queue<Transform>();
            queue.Enqueue(tran);

            while (queue.Count > 0)
            {
                Transform front = queue.Dequeue();
                result.Enqueue(front);

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

        /// <summary>
        /// 上下楼
        /// </summary>
        public void UpDownFloor()
        {
            _floorIndex = (_floorIndex == 1 ? 2 : 1);
            UpdatePlayerPosition(_floorIndex);
        }

        void UpdatePlayerPosition(int floorIndex)
        {
            player.transform.position = _floorPositionMap[_floorIndex].position;
            player.transform.rotation = _floorPositionMap[_floorIndex].rotation;
        }
    }
}