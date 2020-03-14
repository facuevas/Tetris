using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I : Tetrimino, ITetrimino
{
    TetriminoType ITetrimino.GetType()
    {
        return Type;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && CheckLeftEdge())
            Root.transform.Translate(Vector3.left);

        if (Input.GetKeyDown(KeyCode.RightArrow) && CheckRightEdge())
            Root.transform.Translate(Vector3.right);


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Pivot.transform.Rotate(Vector3.forward, 90);
            if (!ICheckLeftEdge())
                Root.transform.position += new Vector3(4, 0, 0);
            if (!ICheckRightEdge())
                Root.transform.position -= new Vector3(4, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Pivot.transform.Rotate(Vector3.forward, -90);
            if (!ICheckLeftEdge())
                Root.transform.position += new Vector3(4, 0, 0);
            if (!ICheckRightEdge())
                Root.transform.position -= new Vector3(4, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
            HardDrop();

        RoundVector3();

    }

    protected bool ICheckRightEdge()
    {
        foreach (GameObject cube in cubes)
        {
            if (Mathf.RoundToInt((cube.transform.position.x + 2)) > 9 || GameMaster.isSpotFilled[(int)Mathf.RoundToInt(cube.transform.position.x) + 1, (int)Mathf.RoundToInt(cube.transform.position.y)])
            {
                return false;
            }
        }
        return true;
    }

    protected bool ICheckLeftEdge()
    {
        foreach (GameObject cube in cubes)
        {
            if ((Mathf.RoundToInt((cube.transform.position.x - 2)) < 0) || GameMaster.isSpotFilled[(int)Mathf.RoundToInt(cube.transform.position.x) - 2, (int)Mathf.RoundToInt(cube.transform.position.y)])
                return false;
        }
        return true;
    }
}
