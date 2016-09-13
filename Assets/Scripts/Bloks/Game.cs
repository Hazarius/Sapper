using UnityEngine;
class Game
{
    private static Game _instance;
	public bool gameStatus = true;

	private static LevelGenerator generator;

	public int bombTotal;
	
	public int flagsLeft;
	
    public static Game instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Game();
				generator = GameObject.FindObjectOfType<LevelGenerator>();
            }
            return _instance;
        }
    }

    private Game()
    {
		gameStatus = false;
    }

	public void NewGame(int bombCount)
	{
		this.bombTotal = bombCount;
		this.flagsLeft = bombCount;
		gameStatus = true;
	}

    public void GameOver()
    {
		if (gameStatus)
		{
			Debug.Log("<color=red>Game Over!</color>");
			gameStatus = false;
		}
    }

	public void Win()
	{
		if (gameStatus)
		{
			gameStatus = false;
			Debug.Log("<color=green>Win!!</color>");
		}		
	}

	public bool CheckGameCondition()
	{
		Cell cell;
		for (int i = 0; i < generator.width; i++)
		{
			for (int j = 0; j < generator.height; j++)
			{
				cell = generator.cellMap[i,j];
				if (cell.cellType == ECellType.BOMB && !cell.block.IsMarked())
				{
					return false;
				}				
			}
		}
		Win();
		return true;
	}
}