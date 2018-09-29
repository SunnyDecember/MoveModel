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

        public static MainSceneUI instance;

        void Awake()
        {
            instance = this;
        }

        void Start ()
		{
            
        }

        /// <summary>
        /// 显示info窗口
        /// </summary>
        /// <param name="isShow"></param>
        /// <param name="info"></param>
        public void SetInfoWin(string info = "")
        {
            GameObject infoWin = Instantiate(Resources.Load("Info")) as GameObject;
            infoWin.transform.SetParent(transform);
            infoWin.transform.localPosition = Vector3.zero;
            infoWin.transform.localRotation = Quaternion.identity;
            infoWin.transform.localScale = Vector3.one;
            
            infoWin.GetComponentInChildren<Text>().text = info;
            StartCoroutine(DelayHideInfoWin(infoWin));
        }

        IEnumerator DelayHideInfoWin(GameObject infoWin)
        {
            yield return new WaitForSeconds(2f);
            DestroyImmediate(infoWin);
        }
    }
}