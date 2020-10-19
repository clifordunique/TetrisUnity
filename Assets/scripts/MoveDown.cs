using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{


    void Start()
    {
        InvokeRepeating("moveDown", 1.0f, 1.0f);
    }

    void moveDown()
    {
        //if ((transform.position + new Vector3(0, -1, 0)).y > -3)
        transform.position = transform.position + new Vector3(0, -1, 0);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "BottomPlatform"
            || collision.gameObject.GetComponent<dead>().isDead == true)
        {
            CancelInvoke();
            this.gameObject.GetComponent<dead>().isDead = true;
        }

        Debug.Log(collision.gameObject.name);
    }
}
