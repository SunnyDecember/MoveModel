using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private NodeData _nodeData;

	void Start ()
    {
		
	}

    public void UpdateData(NodeData nodeData)
    {
        _nodeData = nodeData;
    }
}
