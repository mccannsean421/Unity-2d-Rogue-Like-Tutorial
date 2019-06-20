using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null; //Static instance of GameManager which allows it to be accessed by any other script.
    private BoardManager boardScript; //Store a reference to our BoardManager which will set up the level.
    private int level = 3; //Current level number, expressed in game as "Day 1".
    public int playerFoodPoints = 100;
    [HideInInspector] public bool playersTurn = true; // hidden in inspector

    void Awake() {

        // Check if the instance already exists
        if (instance == null)
            instance = this; // if not, set instance to this

        //If instance already exists and it's not this:
        else if (instance != this)
            Destroy(gameObject); 

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Get a component reference to the attached BoardManager script
        boardScript = GetComponent<BoardManager>();

        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    void InitGame() {
        //Call the SetupScene function of the BoardManager script, pass it current level number.
        boardScript.SetupScene(level);
    }

    public void GameOver() {
        enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
