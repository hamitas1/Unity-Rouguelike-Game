using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;


public class BoardManager : MonoBehaviour
{
    [SerializeField]
    public class Count
    {
        public int maximum;
        public int minimum;

        public Count(int min,int max)
        {
            minimum = min;
            maximum = max;
        }
    }
    public int columns = 8;
    public int rows = 8;
    public Count wallCount = new Count(5, 9);
    public Count foodCount= new Count(1, 5);
    public GameObject exit;
    public GameObject[] floorTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;
    public GameObject[] wallTiles;

    private Transform boardHolder;
    private List <Vector3> gridPositions=new List<Vector3>();

    void InitialiseList()
    {
        gridPositions.Clear();
        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }

        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        for (int x = -1;x < columns + 1; x++)
        {
            for(int y = -1;y < rows + 1; y++)
            {
                GameObject toInsantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x==-1|| x== columns || y==-1 ||y==rows)
                    toInsantiate = outerWallTiles[Random.Range(0,outerWallTiles.Length)];

                GameObject instance= Instantiate(toInsantiate, new Vector3(x, y, 0f) , Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }
    }

    private Vector3 RandomPosition()
    {
        int randomIndex= Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }
    void LayoutObjectRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }
    public void SetupScene(int level)
    {
        BoardSetup();
        InitialiseList();
        LayoutObjectRandom(wallTiles,wallCount.minimum,wallCount.maximum);
        LayoutObjectRandom(foodTiles,foodCount.minimum,foodCount.maximum);
        int enemyCount = (int)Mathf.Log(level, 2f);
        LayoutObjectRandom(enemyTiles, enemyCount, enemyCount);
        Instantiate(exit,new Vector3 (columns -1, rows -1,0f), Quaternion.identity);
    }
}
        
