using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour {

    private RaycastHit _hit;
	// Update is called once per frame
	void Update () {
		if (Game.instance.gameStatus)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Vector3 mousePosition = Input.mousePosition;
				var ray = Camera.main.ScreenPointToRay(mousePosition);
				Debug.DrawRay(ray.origin, ray.direction, Color.cyan, 5);
				if (Physics.Raycast(ray, out _hit, Mathf.Infinity))
				{
					var block = _hit.transform.gameObject;
					BaseBlock block_controller = block.GetComponent<BaseBlock>();
					if (block_controller != null)
					{
						block_controller.OnClick();
					}
				}
			}

			if (Input.GetMouseButtonDown(1))
			{
				Vector3 mousePosition = Input.mousePosition;
				var ray = Camera.main.ScreenPointToRay(mousePosition);
				Debug.DrawRay(ray.origin, ray.direction, Color.cyan, 5);
				if (Physics.Raycast(ray, out _hit, Mathf.Infinity))
				{
					var block = _hit.transform.gameObject;
					BaseBlock block_controller = block.GetComponent<BaseBlock>();
					if (block_controller != null)
					{
						if (!block_controller.IsVisited())
						{
							if (Game.instance.flagsLeft > 0)
							{
								block_controller.MarkAsBomb();
							}							
						}						
					}
				}
			}
		}
	}
}
