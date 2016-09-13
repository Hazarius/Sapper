using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public enum ECellType
{
	EMPTY = 0,
	WALL = 1,
	BOMB = 2
}
public class Cell
{
	public ECellType cellType;
	public bool isBusy;
	public BaseBlock block;

	public Cell()
	{
		this.cellType = ECellType.EMPTY;
		this.isBusy = false;
	}
}

public class LevelGenerator : MonoBehaviour {
	List<BombBlock> blockList;	
	public Cell[,] cellMap;
	private int bombCount = 30;
	
	public GameObject wall;
	public GameObject bomb;
	public GameObject emptyBlock;
	private GameObject map;		 

	public int height = 20;
	public int width = 20;

	private string strHeight;
	private string strWidth;
	private string strBombCount; 
	
	void OnGUI()
	{
		GUI.Label(new Rect(20, 70, 150, 25), "Flags left :" + Game.instance.flagsLeft);

		if (GUI.Button(new Rect(20, 20, 150, 50), "Start new game"))
		{
			NewGame();
		}

		GUI.Label(new Rect(20, 120, 80, 25), "Height");
		strHeight = height.ToString();
		strHeight = GUI.TextField(new Rect(90, 120, 40, 25), strHeight);
		if (int.TryParse(strHeight, out height))
		{

		}

		GUI.Label(new Rect(20, 145, 80, 25), "Width");
		strWidth = width.ToString();
		strWidth = GUI.TextField(new Rect(90, 145, 40, 25), strWidth);
		if (int.TryParse(strWidth, out width))
		{

		}

		GUI.Label(new Rect(20, 170, 80, 25), "Bomb count");
		strBombCount = bombCount.ToString();
		strBombCount = GUI.TextField(new Rect(90, 170, 40, 25), strBombCount);
		if (int.TryParse(strBombCount, out bombCount))
		{

		}
	}
	float timer = 0;
	bool needInit = false;
	float conditionTimer = 0;
	void Update()
	{
		if (needInit)
		{
			timer += Time.deltaTime;
			if (timer >= 0.5f)
			{
				timer = 0;
				InitBombs();
				needInit = false;
			}
		}

		if (Game.instance.gameStatus)
		{
			conditionTimer += Time.deltaTime;
			if (conditionTimer >= 0.5f)
			{
				conditionTimer = 0;
				Game.instance.CheckGameCondition();
			}
		}		
	}

	private void GenerateMap()
	{
		if (map != null)
		{
			DestroyImmediate(map);			
		}
		cellMap = new Cell[width, height];
		for (int i = 0; i < width; i++)
			for (int j = 0; j < height; j++)
				cellMap[i,j] = new Cell();
		
		map = new GameObject("map");

		int bc = 0;
		int pi ,pj;		
		while (bc != bombCount)
		{
			pi = Random.Range(1, width - 2);
			pj = Random.Range(1, height - 2);
			if (cellMap[pi, pj].isBusy)
				continue;
			
			cellMap[pi, pj].isBusy = true;
			cellMap[pi, pj].cellType = ECellType.BOMB;
			bc++;
		}

		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				if (i == 0 || i == height - 1 || j==0 || j == width -1)
				{
					GameObject newObj = GameObject.Instantiate(wall, new Vector3(i, 0, j), Quaternion.identity) as GameObject;
					newObj.transform.parent = map.transform;
					cellMap[i, j].block = newObj.GetComponent<BaseBlock>() as BaseBlock;
					cellMap[i, j].cellType = ECellType.WALL;
					cellMap[i, j].isBusy = true;
				}
				else
				{
					GameObject newObj;
					switch (cellMap[i, j].cellType)
					{
						case ECellType.BOMB:
							newObj = GameObject.Instantiate(bomb, new Vector3(i, 0, j), Quaternion.identity) as GameObject;
							cellMap[i, j].block = newObj.GetComponent<BaseBlock>() as BaseBlock;
							newObj.transform.parent = map.transform;
							break;

						case ECellType.EMPTY:
							newObj = GameObject.Instantiate(emptyBlock, new Vector3(i, 0, j), Quaternion.identity) as GameObject;
							cellMap[i, j].block = newObj.GetComponent<BaseBlock>() as BaseBlock;
							newObj.transform.parent = map.transform;
							break;
					}
					cellMap[i, j].isBusy = true;
				}
			}
		}		
	}

	void InitBombs()
	{
		BombBlock[] bombBlocks = map.GetComponentsInChildren<BombBlock>();
		foreach (BombBlock block in bombBlocks)
		{
			block.InitBomb();
		}
	}

	public void NewGame()
	{
		Game.instance.NewGame(bombCount);
		GenerateMap();
		needInit = true;
	}
}
