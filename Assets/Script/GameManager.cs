using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Ref à la tuile
    [SerializeField] GameObject GameTilesPrefab;
    [SerializeField] GameObject enemyPrefab;

    public static GameManager Instance;
    GameTiles[,] gameTiles;
    private GameTiles spawnTile;
    const int colcount = 20;
    const int rowcount = 10;
    private int HP = 10;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    private bool ennemySpawned = false;
    public GameTiles TargetTile { get; internal set; }
    List<GameTiles> pathToGoal = new List<GameTiles>(); 

    private void Awake()
    {
        gameTiles = new GameTiles[colcount, rowcount];

        for (int x = 0; x < colcount; x++)
        {
            for (int y = 0; y < rowcount; y++)
            {
                var spawnPosition = new Vector3(x, y, 0);
                var tile = Instantiate(GameTilesPrefab, spawnPosition, Quaternion.identity);
                gameTiles[x, y] = tile.GetComponent<GameTiles>();
                gameTiles[x, y].GM = this; //Chemin le plus court
                gameTiles[x, y].X = x; //Chemin le plus court
                gameTiles[x, y].Y = y; //Chemin le plus court

                if ((x + y) % 2 == 0)
                {
                    gameTiles[x, y].TurnGrey();
                }
            }
        }
        spawnTile = gameTiles[1, 8];
        spawnTile.SetEnemySpawn();
        StartCoroutine(SpawnEnemyCoroutine());
        TargetTile = gameTiles[16, 3]; //Level design

        
        for(int y = 2; y <= 9; y++)
        {
            gameTiles[5, y].SetWall(); 
        }
        for (int y = 0; y <= 7; y++)
        {
            gameTiles[10, y].SetWall();
        }

        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() //Level design
    {
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);

        foreach (var t in gameTiles)
        {
            t.setPath(false);
        }

        var path = Pathfinding(spawnTile, TargetTile);
        var tile = TargetTile;

        while (tile != null)
        {
            pathToGoal.Add(tile);
            tile.setPath(true);
            tile = path[tile];
        }
        StartCoroutine(SpawnEnemyCoroutine()); //Faire spawn les ennemis
    }

    private void Update()
    {
        if (!RemainingEnemies() && ennemySpawned)
        {
            Time.timeScale = 0;
            WinScreen.SetActive(true);
        }
    }

    private Dictionary<GameTiles, GameTiles> Pathfinding(GameTiles sourceTile, GameTiles targetTile) //Chemin le plus court
    {
        var dist = new Dictionary<GameTiles, int>(); //Distance minimale de la tuile à la source

        var prev = new Dictionary<GameTiles, GameTiles>(); //Tuile precedente qui mène au chemin le plus court

        var Q = new List<GameTiles>(); //Liste de tuiles restantes

        foreach (var v in gameTiles)
        {
            dist.Add(v, 9999);

            prev.Add(v, null);  

            Q.Add(v);
        }
        dist[sourceTile] = 0;

        while (Q.Count > 0)
        {
            GameTiles u = null;
            int minDistance = int.MaxValue;

            foreach (var v in Q)
            {
                if (dist[v] < minDistance)
                {
                    minDistance = dist[v];
                    u = v;
                }
            }
            Q.Remove(u);

            foreach (var v in FindNeighbor(u))
            {
                if (!Q.Contains(v) || v.IsBlocked)
                {
                    continue;
                }
                int alt = dist[u] + 1;

                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }
        return prev;
    }

    private List<GameTiles> FindNeighbor(GameTiles u) //Chemin le plus court
    {
        var result = new List<GameTiles>();

        if (u.X - 1 >= 0)
            result.Add(gameTiles[u.X - 1, u.Y]);
        if (u.X + 1 < colcount)
            result.Add(gameTiles[u.X + 1, u.Y]);
        if (u.Y - 1 >= 0)
            result.Add(gameTiles[u.X, u.Y - 1]);
        if (u.Y + 1 < rowcount)
            result.Add(gameTiles[u.X, u.Y + 1]);
        return result;
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        yield return new WaitForSeconds(3f);
        int enemycount = 0;
        while (enemycount < 25)
        {
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.6f);
                var enemy = Instantiate(enemyPrefab, spawnTile.transform.position, Quaternion.identity);
                enemy.GetComponent<Enemy>().SetPath(pathToGoal);
            }
            enemycount += 5;
            ennemySpawned = true;
            yield return new WaitForSeconds(3f);
        }
    }

    private bool RemainingEnemies()
    {
        return Enemy.allEnemies.Count > 0;
    }
    
    public void PerdreVie()
    {
        HP--;
        if(HP <= 0)
        {
            Time.timeScale = 0;
            LoseScreen.SetActive(true);
        }
    }
}
