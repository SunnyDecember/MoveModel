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

        //(小地图制作.)
        public Player player;
        public Transform minMapPlayer;
        public Image minMap;
        public Text floorText;

        public Transform p0;
        public Transform p1;
        public Transform p2;

        void Awake()
        {
            instance = this;
        }

        void Start()
        {
            ShowMap(MainScene.instance.floorIndex);

            _floor.onClick.AddListener(() =>
            {
                int floorIndex = MainScene.instance.UpDownFloor();
                ShowMap(floorIndex);
            });

            _exitButton.onClick.AddListener(() =>
            {
#if !UNITY_EDITOR
                Application.Quit();              
#endif
            });
        }

        void ShowMap(int floorIndex)
        {
            //加载地图
            Sprite mapObj = Resources.Load<Sprite>("MinMap_" + floorIndex);
            minMap.sprite = mapObj;
            floorText.text = floorIndex + " 楼";
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
            (infoWindow.transform as RectTransform).anchoredPosition = Vector3.zero;
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

        private void Update()
        {
            float p01_Length = Vector3.Magnitude(p1.position - p0.position);
            float p02_Length = Vector3.Magnitude(p2.position - p0.position);

            Vector3 playerPos = player.transform.position;
            Vector3 playerPos01 = new Vector3(p0.position.x, p0.position.y, playerPos.z);
            Vector3 playerPos02 = new Vector3(playerPos.x, p0.position.y, p0.position.z);

            float ratio01 = Vector3.Magnitude(playerPos01 - p0.position) / p01_Length;
            float ratio02 = Vector3.Magnitude(playerPos02 - p0.position) / p02_Length;

            //设置箭头在小地图中的位置
            float width = (minMap.transform as RectTransform).sizeDelta.x * minMap.transform.localScale.x;
            float height = (minMap.transform as RectTransform).sizeDelta.y * minMap.transform.localScale.y;
            (minMapPlayer.transform as RectTransform).anchoredPosition = new Vector2(width * ratio01, height * ratio02);

            //设置箭头的方向
            float playerAngle = player.transform.eulerAngles.y;
            Vector3 minMapPlayerVec = minMapPlayer.transform.eulerAngles;
            minMapPlayer.transform.eulerAngles = new Vector3(minMapPlayerVec.x, minMapPlayerVec.y, -playerAngle + 90);
        }
    }
}