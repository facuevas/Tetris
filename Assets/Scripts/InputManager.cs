using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    GameObject controlledTermino;
    Transform[] controlledBlocks;
    private float moveDelay = 0.1f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("InputManager is on");
    }

    // Update is called once per frame
    void Update()
    {
        
        controlledTermino = GameObject.FindWithTag("currentTermino");
        
        timer += Time.deltaTime;

        if (Input.GetKey(KeyCode.RightArrow) && timer > moveDelay)
        {
            controlledTermino.transform.position += new Vector3(1, 0, 0);
            timer = timer - moveDelay;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && timer > moveDelay)
        {
            controlledTermino.transform.position -= new Vector3(1, 0, 0);
            timer = timer - moveDelay;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            controlledTermino.transform.Rotate(0, 0, 90);

        }

        if (Input.GetKey(KeyCode.DownArrow) && timer > moveDelay)
        {
            controlledTermino.transform.position -= new Vector3(0, 1, 0);
            timer = timer - moveDelay;
        }
    }
}
