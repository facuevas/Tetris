using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O : Tetrimino, ITetrimino
{

    TetriminoType ITetrimino.GetType()
    {
        return Type;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && CheckLeftEdge())
        {
            Root.transform.Translate(Vector3.left);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && CheckRightEdge())
        {
            Root.transform.Translate(Vector3.right);
        }

        if (Input.GetKeyDown(KeyCode.Space))
            HardDrop();

        RoundVector3();


        //we do not need rotations for cube, therefore we did not add them
    }
}
