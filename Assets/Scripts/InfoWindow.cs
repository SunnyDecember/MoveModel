using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindow : MonoBehaviour
{
    [SerializeField]
    private Button _closeButton;

    [SerializeField]
    private Text _text;

    private NodeData _nodeData;

    void Start ()
    {
        _closeButton.onClick.AddListener(()=> 
        {
            Destroy(gameObject);
        });

    }

    public void UpdateData(NodeData nodeData)
    {
        _nodeData = nodeData;
        _text.text = _nodeData.Info;
    }
}
