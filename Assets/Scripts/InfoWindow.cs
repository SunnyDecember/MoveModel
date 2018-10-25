using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoWindow : MonoBehaviour
{
    [SerializeField]
    private Button _closeButton;

    [SerializeField]
    private Text _id;

    [SerializeField]
    private Text _info;

    [SerializeField]
    private Text _state;

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
        _info.text = _nodeData.Info;
        _id.text = nodeData.ID;
        _state.text = nodeData.HasSale ? "<color=red>结缘</color>" : "<color=green>可结缘</color>"; 
    }
}
