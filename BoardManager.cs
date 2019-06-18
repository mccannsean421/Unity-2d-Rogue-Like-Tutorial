using System; // so we can use the serializable attribute (controls how variables appear in the unity inspector)
using System.Collections.Generic; // so we can use lists
using UnityEngine;
using Random = UnityEngine.Random; // because there is a class called random in both unity and system engine name spaces.

public class BoardManager : MonoBehaviour
{


    [Serializable]
    public class Count {
        public int minimum;
        public int maximum;

        public Count (int min, int max) {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 8;
    public int rows = 8;

    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5);

    public GameObject exit; // single game objext as there will only be one exit
    public GameObject[] floorTiles; // Array, as there are many floor tiles
    public GameObject[] wallTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;   
    public GameObject[] outerWallTiles;

    private Transform boardHolder; //used to keep the hierarchy clean
    private List <Vector3> gridPositions = new List<Vector3>(); //Vector 3? 

    void InitialiseList() {
        gridPositions.Clear(); // clear the grid

        for (int x = 1; x < columns - 1; x++) {
            for (int y = 1; y < rows - 1; y++) {
                gridPositions.Add(new Vector3(x,y,0f));
            }
        }
    }

    //Used to set up the outer wall and floor of the map
    void BoardSetup() {
        boardHolder = new GameObject("Board").transform;

        // set to columns-1 and + 1 to build a border around the active grid
        for (int x = -1; x < columns + 1; x++) {
            for (int y = -1; y < rows + 1; y++) {
                
                //generate random floor tile
                GameObject toInstantiate = floorTiles[Random.Range (0, floorTiles.Length)];

                if(x == -1 || x == columns || y == -1 || y == rows) {
                    //toInstantiate = floorTiles[Random.Range (0, wallTiles.Length)];
                    toInstantiate = outerWallTiles[Random.Range (0, outerWallTiles.Length)];
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y,0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }
    }

    // Function to place random items (enemys, power ups etc)
    Vector3 RandomPosition() {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    //place objects
    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum) {
        int objectCount = Random.Range(minimum, maximum + 1);

        for(int i = 0; i < objectCount; i++) {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate (tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetupScene (int level) {
        BoardSetup();
        InitialiseList();
        LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);
        int enemyCount = (int)Mathf.Log(level, 2f);
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
        Instantiate(exit, new Vector3(columns -1, rows -1, 0f), Quaternion.identity);
    }
}
