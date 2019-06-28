using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    public GameObject gmj;
    private float moveSpeed=20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            gmj.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.RightArrow))
            gmj.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

    }
}
