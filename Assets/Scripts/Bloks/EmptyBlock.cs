using UnityEngine;
using System.Collections.Generic;

class EmptyBlock : BaseBlock
{
	private TextMesh Text3D;
	private int bombAround = 0;

	private GameObject t3dPrefab;

	public override void OnClick()
	{		
		if (!visited)
		{
			visited = true;
			if (bombAround == 0)
			{			
				OpenNeighborhood();
			}
			else
			{				
				ShowBlockInfo();				
			}
		}
		else
		{
			if (BombsMarked())
			{
				OpenNeighborhood();
			}
		}

		base.OnClick();			
	}

	bool BombsMarked()
	{
		int marketBombs = 0;
		RaycastHit hit;
		foreach (Ray ray in rays)
		{
			if (Physics.Raycast(ray, out hit, 2))
			{
				GameObject block = hit.transform.gameObject;
				BaseBlock controller = block.GetComponent<BaseBlock>();
				if (controller != null)
				{
					if (controller.IsMarked())
					{
						marketBombs++;
					}					
				}
			}
		}
		return marketBombs == bombAround ? true : false;
	}

	private void OpenNeighborhood()
	{			
		RaycastHit hit;
		foreach (Ray ray in rays)
		{
			if (Physics.Raycast(ray, out hit, 2))
			{
				GameObject block = hit.transform.gameObject;
				BaseBlock controller = block.GetComponent<BaseBlock>();
				if (controller != null)
				{
					if (! (controller.IsVisited() || controller.IsMarked()))
					{
						controller.OnClick();
					}					
				}
			}
		}	
	}

	void ShowBlockInfo()
	{
		t3dPrefab = GameObject.Instantiate(Resources.Load("System/TextBlock", typeof(GameObject))) as GameObject;
		t3dPrefab.transform.position = me.position + new Vector3(0, 0.5f, 0);
		t3dPrefab.transform.parent = me.transform;
		Text3D = t3dPrefab.GetComponent<TextMesh>();
		Text3D.text = bombAround.ToString();
	}

	public void BombVisit()
	{
		bombAround++;
	}

	public override void Reset()
	{
		bombAround = 0;
		base.Reset();
	}
}