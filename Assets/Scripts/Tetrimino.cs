using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TetriminoType
{
    I, J, L, O, S, T, Z
};

public class Tetrimino : MonoBehaviour
{
    
    public TetriminoType Type;

    [Tooltip("Used for translation")]
    public GameObject Root;

    [Tooltip("Used for rotation")]
    public GameObject Pivot;

    [SerializeField]
    protected GameObject[] cubes = new GameObject[4];

    /*
     * This function rounds the vector3 coordinates of the children cubes.
     */
    protected void RoundVector3()
    {
        foreach (GameObject cube in cubes)
        {
            float roundX = Mathf.Round(cube.transform.position.x);
            float roundY = Mathf.Round(cube.transform.position.y);

            cube.transform.position = new Vector3(roundX, roundY, 0);
        }
    }

    /*
     * This function checks the left edge of the playboard 
     * We have this function in the Tetrimino.cs class over the GameMaster.cs class so we can easily access the children cubes.
     */
    protected bool CheckLeftEdge()
    {
        foreach (GameObject cube in cubes)
        {
            if ((Mathf.RoundToInt((cube.transform.position.x - 1)) < 0) || GameMaster.isSpotFilled[(int) Mathf.RoundToInt(cube.transform.position.x) - 1, (int)Mathf.RoundToInt(cube.transform.position.y)])
                return false;
        }
        return true;
    }


    /*
    * This function checks the right edge of the playboard 
    * We have this function in the Tetrimino.cs class over the GameMaster.cs class so we can easily access the children cubes.
     */
    protected bool CheckRightEdge()
    {
        foreach (GameObject cube in cubes)
        {
            if (Mathf.RoundToInt((cube.transform.position.x + 1)) > 9 || GameMaster.isSpotFilled[(int) Mathf.RoundToInt(cube.transform.position.x) + 1, (int) Mathf.RoundToInt(cube.transform.position.y)])
            {
                return false;
            }
        }
        return true;
    }


    /*
     * This function is used when the user wants to "hard drop" a tetrimino. 
     */
    protected void HardDrop()
    {
            while (!HardBottomCheck(Root.transform))
            {
                Root.transform.Translate(Vector3.down);
                RoundVector3();
            }
    }

    /*
     * This function checks is like the bottom check function in GameMaster.cs but it checks for a "harddrop" bottom
     */ 
    bool HardBottomCheck(Transform t)
    {
        Transform[] children = t.GetComponentsInChildren<Transform>();


        for (int i = 2; i < children.Length; i++)
        {
            if (Mathf.RoundToInt(children[i].position.y) == 0 || GameMaster.isSpotFilled[(int)Mathf.RoundToInt(children[i].position.x), (int)Mathf.RoundToInt(children[i].position.y) - 1])
            {
                return true;
            }
        }

        return false;
    }
}
