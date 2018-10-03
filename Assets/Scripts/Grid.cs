using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private Text _idText_0;
    
    [SerializeField]
    private Image _saleImage_0;

    [SerializeField]
    private Text _idText_1;

    [SerializeField]
    private Image _saleImage_1;

    private NodeData _nodeData;

    public void UpdateData(NodeData nodeData)
    {
        _nodeData = nodeData;
        _idText_0.text = _nodeData.ID;
        _saleImage_0.enabled = nodeData.HasSale;

        _idText_1.text = _nodeData.ID;
        _saleImage_1.enabled = nodeData.HasSale;
    }
}
