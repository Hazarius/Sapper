using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public abstract class BaseBlock : MonoBehaviour {

    private Texture2D _hideTexture;
	private Texture2D _bombMarker;
    public Texture2D ShowTexture;

	protected bool visited = false;
	protected bool marked = false;
	protected Transform me;
	protected List<Ray> rays = new List<Ray>();

    public void Start()
    {
		me = transform;
		Init();		
		_hideTexture = Resources.Load("System/Textures/Wood_Box",typeof(Texture2D)) as Texture2D;
		_bombMarker = Resources.Load("System/Textures/flag", typeof(Texture2D)) as Texture2D;
        if (_hideTexture != null)
            this.renderer.material.SetTexture(0, _hideTexture);
    }

	protected virtual void Init()
	{
		rays.Add(new Ray(me.position, new Vector3(-1, 0, 1)));
		rays.Add(new Ray(me.position, new Vector3(0, 0, 1)));
		rays.Add(new Ray(me.position, new Vector3(1, 0, 1)));
		rays.Add(new Ray(me.position, new Vector3(1, 0, 0)));
		rays.Add(new Ray(me.position, new Vector3(1, 0, -1)));
		rays.Add(new Ray(me.position, new Vector3(0, 0, -1)));
		rays.Add(new Ray(me.position, new Vector3(-1, 0, -1)));
		rays.Add(new Ray(me.position, new Vector3(-1, 0, 0)));
	}

    public virtual void OnClick()
    {
        if (ShowTexture != null)
            this.renderer.material.SetTexture(0, ShowTexture);		
		visited = true;		
    }

	public bool IsVisited()
	{
		return visited;
	}

	public bool IsMarked()
	{
		return marked;
	}

	public virtual void Reset()
	{
		this.renderer.material.SetTexture(0, _hideTexture);

		visited = false;
	}

	public virtual void MarkAsBomb()
	{		
		marked = !marked;

		Game.instance.flagsLeft += marked ? -1 : 1;

		Texture2D setTexture = marked ? _bombMarker : _hideTexture;

		if (setTexture != null)
            this.renderer.material.SetTexture(0, setTexture);
	}

	void OnCollisionEnter(Collision collision)
	{
		OnCollision(collision);
	}

	protected virtual void OnCollision(Collision collision)
	{

	}
}
