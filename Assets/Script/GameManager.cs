using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Ref à la tuile
    [SerializeField] GameObject GameTilesPrefab;
    [SerializeField] GameObject enemyPrefab;
    ADgameTiles[,] gameTiles;
    const int colcount = 20;
    const int rowcount = 10;

    private void Awake()
    {
        gameTiles = new ADgameTiles[colcount, rowcount];

        for (int x = 0; x < colcount; x++)
        {
            for (int y = 0; y < rowcount; y++)
            {
                var spawnPosition = new Vector3(x, y, 0);
                var tile = Instantiate(GameTilesPrefab, spawnPosition, Quaternion.identity);
                gameTiles[x, y] = tile.GetComponent<ADgameTiles>();

                if ((x + y) % 2 == 0)
                {
                    gameTiles[x, y].TurnGrey();
                }
            }
        }
        gameTiles[1, 7].SetEnemySpawn();
    }
}
