using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z : Tetrimino, ITetrimino
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
            if (!CheckLeftEdge())
                Root.transform.Translate(Vector3.right);
            if (!CheckRightEdge())
                Root.transform.Translate(Vector3.left);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Pivot.transform.Rotate(Vector3.forward, -90);
            if (!CheckLeftEdge())
                Root.transform.Translate(Vector3.right);
            if (!CheckRightEdge())
                Root.transform.Translate(Vector3.left);
        }

        if (Input.GetKeyDown(KeyCode.Space))
            HardDrop();

        RoundVector3();
    }
}
