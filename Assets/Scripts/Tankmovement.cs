using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tankmovement : MonoBehaviour
{

    public GameObject obj;
    public float range = 5f, moveSpeed = 3f, turnSpeed = 40f;
    private Light rise;

    void Start()
    {
        rise = GameObject.Find("Light").GetComponent<Light>();
    }

    void Update(){
        BodyMovement();
        if (Input.GetKeyUp(KeyCode.L))
        {
            print("Light");
            rise.enabled = !rise.enabled;
        }
   
    }
    void BodyMovement() {
       // if (Input.GetKey(KeyCode.UpArrow))
         //   obj.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        //if (Input.GetKey(KeyCode.DownArrow))
          //  obj.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftArrow))
            obj.transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.RightArrow))
            obj.transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
    }

}

