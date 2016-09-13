using UnityEngine;

class BombBlock : BaseBlock
{
	public void InitBomb()
	{
		RaycastHit hit;
		foreach (Ray ray in rays)
		{
			if (Physics.Raycast(ray, out hit, 2))
			{
				var block = hit.transform.gameObject;
				EmptyBlock controller = block.GetComponent<EmptyBlock>();
				if (controller != null)
				{
					controller.BombVisit();					
				}
			}
		}
	}

    public override void OnClick()
    {
		if (IsMarked())
			return;
		if (!visited)
		{
			Collider[] blocks = Physics.OverlapSphere(me.position, 20);
			foreach (Collider coll in blocks)
			{
				var rigidbody = coll.transform.rigidbody;
				if (rigidbody != null)
				{
					rigidbody.useGravity = true;
					rigidbody.AddExplosionForce(100, me.position, 20);
				}
			}
			base.OnClick();
			Game.instance.GameOver();  
		}		      
    }

	protected override void OnCollision(Collision collider)
	{
		OnClick();
		base.OnCollision(collider);
	}
}

