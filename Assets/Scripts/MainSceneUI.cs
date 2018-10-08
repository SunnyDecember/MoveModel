using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/* Author		: Runing
** Time			: 18.9.29
** Describtion	: 主场景的UI
*/

namespace Runing
{
	public class MainSceneUI : MonoBehaviour
	{
        //[SerializeField]
        //GameObject _infoObject;

        //[SerializeField]
        //Text _text;
        [SerializeField]
        private Transform _gridCanvas;

        [SerializeField]
        private Button _exitButton;

        /// <summary>
        /// 上下楼按钮
        /// </summary>
        [SerializeField]
        private Button _floor;

        public static MainSceneUI instance;

        void Awake()
        {
            instance = this;
        }

        void Start ()
		{
            _floor.onClick.AddListener(()=> 
            {
                MainScene.instance.UpDownFloor();
            });

            _exitButton.onClick.AddListener(()=> 
            {
#if !UNITY_EDITOR
                Application.Quit();              
#endif
            });
        }

        /// <summary>
        /// 显示info窗口
        /// </summary>
        /// <param name="isShow"></param>
        /// <param name="info"></param>
        public void SetInfoWin(NodeData nodeData)
        {
            InfoWindow infoWindow = (Instantiate(Resources.Load("InfoWindow")) as GameObject).GetComponent<InfoWindow>();
            infoWindow.transform.SetParent(transform);
            //infoWindow.transform.localPosition = Vector3.zero;
            (infoWindow.transform  as RectTransform).anchoredPosition = Vector3.zero;
            infoWindow.transform.localRotation = Quaternion.identity;
            infoWindow.transform.localScale = Vector3.one;
            infoWindow.UpdateData(nodeData);
        }
        
        public void CreateGridUI(NodeData data, Vector3 position, Quaternion rotation)
        {
            //生成Grid
            Grid grid = (Instantiate(Resources.Load("Grid")) as GameObject).GetComponent<Grid>();
            //grid.name = data.Name;
            grid.transform.SetParent(_gridCanvas);
            grid.transform.position = position;
            grid.transform.rotation = rotation;
            grid.transform.localScale = Vector3.one;
            grid.UpdateData(data);
        }
    }
}