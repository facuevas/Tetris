using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{

    //Initialize tetrimino prefabs
    public GameObject[] TetriminoPrefabs;

    //initialize gameobject for current tetrimino falling for game and the next tetrinimo in buffer
    private GameObject currentTetriminoFalling = null;

    //initialize gameobject for next tetrimino in queue
    private GameObject nextTetrimino = null;

    //initialize next tetrimino updates
    public int nextTetriminoIndex;
    //public Texture2D[] TetriminoTextures;

    //initialize timers
    public float speed = 0.3f;
    public float coolOffTime;

    //intialize grid check
    public static bool[,] isSpotFilled;
    public static Transform[,] grid;

    //initialize scoring
    private static int score;
    private static int lines;
    private static int level;

    //check for hard drops
    private bool hardDrop;

    //check for game over
    public static bool gameOver;

    //check if game is paused
    public static bool gamePaused;

    //line counter to help update the level of the game
    private int lineCounter;
    

    // Start is called before the first frame update
    void Start()
    {
        //GenerateEdges();
        coolOffTime = 0.0f;
        nextTetriminoIndex = Random.Range(0, 7);
        isSpotFilled = new bool[10, 28];
        grid = new Transform[10, 28];
        score = 0;
        lines = 0;
        level = 1;
        lineCounter = 0;
        gamePaused = false;
        gameOver = false;
        hardDrop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            if (Input.GetKeyUp(KeyCode.Space))
                hardDrop = true;

            //check if the game has been lost
            if (IsGameOver())
                gameOver = true;


            //this calls the function that stops game movement and displays the game menu

            //this region creates a new controlle tetrimino if there is no current tetrimino falling
            if (coolOffTime < Time.time)
            {
                //instantiate a new tetrimino and move it down
                if (currentTetriminoFalling == null)
                {
                    //if there is no current tetrimino falling, we want to score the points and clear the lines and update the current level
                    DeleteRows();

                    //after we clear the rows, we spawn in a new tetrimino.
                    currentTetriminoFalling = GameObject.Instantiate(TetriminoPrefabs[nextTetriminoIndex], 
                        new Vector3(5, 22, 0), 
                        Quaternion.identity) as GameObject;

                    //select next tetrinimo and update in display. we also disable the nextTetrimino gameobject so the object cannot be moved.
                    nextTetriminoIndex = Random.Range(0, 7);
                    nextTetrimino = GameObject.Instantiate(TetriminoPrefabs[nextTetriminoIndex], new Vector3(13.1f, 21.51f, 0f), Quaternion.identity) as GameObject;
                    nextTetrimino.GetComponent<Tetrimino>().enabled = false;

                    Debug.Log(currentTetriminoFalling.GetComponent<ITetrimino>().GetType());
                }
                else
                {
                    if (!hardDrop)
                    {
                        currentTetriminoFalling.transform.position += Vector3.down;
                    }
                    Vector3Round(currentTetriminoFalling);
                    DisplayChildrenCoordinates(currentTetriminoFalling);

                    if (BottomCheck(currentTetriminoFalling.transform))
                    {
                        Debug.Log("Bottom Check");
                        currentTetriminoFalling.transform.Translate(Vector3.up);
                        FillGridBoolean(currentTetriminoFalling);
                        currentTetriminoFalling.GetComponent<Tetrimino>().enabled = false;
                        currentTetriminoFalling = null;
                        Destroy(nextTetrimino);
                        UpdateLevel();
                    }
                }

                coolOffTime = Time.time + speed;

                if (hardDrop == true)
                    hardDrop = false;
            }
        }

        //This region checks if the player pauses the game and changes the boolean value
        if (Input.GetKeyDown(KeyCode.Escape))
            if (gamePaused == false)
                gamePaused = true;
            else
                gamePaused = false;

        //pause the game
        GamePaused(gamePaused);
    }


    /*
     * This function is used for debugging purposes. It sends a log to the console display the cube coordinates.
     */ 
    private void DisplayChildrenCoordinates(GameObject go)
    {
        Transform[] children = go.transform.GetComponentsInChildren<Transform>();
        
        for (int i = 2; i < children.Length; i++)
        {
            Debug.Log(string.Format("Coordinates for {0} are ({1}, {2}, {3})", children[i].name, children[i].position.x, children[i].position.y, children[i].position.z));
        }
    }

    /*
     * This function is only used to round the current gameobjects vector3 position.
     * There is another function that is similar in Tetrimino.cs, however we create another one here due to protection levels.
     */
     private void Vector3Round(GameObject go)
    {
        Transform[] children = go.transform.GetComponentsInChildren<Transform>();

        for (int i = 2; i < children.Length; i++)
        {
            float roundX = Mathf.Round(children[i].transform.position.x);
            float roundY = Mathf.Round(children[i].transform.position.y);
            float roundZ = Mathf.Round(children[i].transform.position.z);

            children[i].position = new Vector3(roundX, roundY, roundZ);
        }

    }

    /*
     * When a row gets deleted, we want to make sure that anything above the deleted row gets moved down
     */ 
    private void MoveRowDown(int y)
    {
        for (int i = 0; i < 10; i++)
        {
            if (isSpotFilled[i, y] == true)
            {
                //change boolean grid
                isSpotFilled[i, y - 1] = isSpotFilled[i, y];
                isSpotFilled[i, y] = false;

                //change transform grid
                grid[i, y - 1] = grid[i, y];
                grid[i, y] = null;

                //update block positions
                grid[i, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }


    /*
     * This function checks for the rows above the current row we are deleting to move down.
     */
    private void MoveRowsAbove(int y)
    {
        for (int i = y; i < 20; i++)
        {
            MoveRowDown(i);
        }
    }

    /*
     * This function iterates through the row to see if the row is filled or not
     */
    private bool IsRowFilled(int y)
    {
        for (int i = 0; i < 10; i++)
        {
            if (isSpotFilled[i, y] == false)
            {
                return false;
            }
        }
        return true;
    }

    /*
     * This function deletes the current filled row and marks our gird boolean back to false
     */
    private void DeleteRow(int y)
    {
        for (int i = 0; i < 10; i++)
        {
            Destroy(grid[i, y].gameObject);
            isSpotFilled[i, y] = false;
        }
    }

    /*
     * This function iterates through the board to check if the row is filled and then deletes the row
     */
    private void DeleteRows()
    {
        int multiplier = 1;
        for (int i = 0; i < 20; i++)
        {
            if (IsRowFilled(i))
            {
                DeleteRow(i);
                MoveRowsAbove(i + 1);
                i--;
                lines++;
                lineCounter++;
                score += 100 * level * multiplier;
                multiplier++;
                Debug.Log("Line score: " + lines + "\nPoint score: " + score);
            }
        }
    }

    /*
     * This function marks the grid spots true if a cube is in that space.
     */ 
    private void FillGridBoolean(GameObject go)
    {
        Transform[] c = go.transform.GetComponentsInChildren<Transform>();

        for (int i = 2; i < c.Length; i++)
        {
            int x = (int)Mathf.Round(c[i].position.x);
            int y = (int)Mathf.Round(c[i].position.y);
            grid[x, y] = c[i];
            isSpotFilled[x, y] = true;

            Debug.Log(string.Format("Grid filled at ({0}, {1})",x ,y ));
        }
    }

    /*
     * this function checks if there is either a tetrimino filling the spot underneath it to keep moving
     * or if we hit 0.
     */
    private bool BottomCheck(Transform t)
    {
        Transform[] children = t.GetComponentsInChildren<Transform>();

    
        for (int i = 2; i < children.Length; i++)
        {
            if (Mathf.RoundToInt(children[i].position.y) == -1 || isSpotFilled[(int)Mathf.RoundToInt(children[i].position.x), (int)Mathf.RoundToInt(children[i].position.y)])
            {
                return true;
            }
        }

        return false;
    }

    /*
     * This function checks if the game is over
     */ 
     private bool IsGameOver()
    {
        for (int i = 0; i < 10; i++)
        {
            if (isSpotFilled[i, 20])
                return true;
        }
        return false;
    }

    /*
     * This function pauses the game
     */
    private void GamePaused(bool gp)
    {
        if (gp == true)
        {
            Debug.Log("Game paused");
            if (currentTetriminoFalling != null)
                currentTetriminoFalling.GetComponent<Tetrimino>().enabled = false;
            Time.timeScale = 0;
        }
        else
        {
            Debug.Log("Game resumed");
            if (currentTetriminoFalling != null)
                currentTetriminoFalling.GetComponent<Tetrimino>().enabled = true;
            Time.timeScale = 1;
        }
    }

    public void UpdateLevel()
    {
        if (lineCounter >= 10)
        {
            lineCounter = 0;
            level++;
            speed /= level * 0.45f;
        }
    }

    /*
     * These are setters/getters
     */
     public static int Level
    {
        get { return level; }
    }

    public static int Score
    {
        get { return score; }
    }

    public static int Lines
    {
        get { return lines;  }
    }
}
