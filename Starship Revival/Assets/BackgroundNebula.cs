using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundNebula : MonoBehaviour
{

    public float movespeed;
    // Use this for initialization
    void Start()
    {
        //StartCoroutine(nebula());
    }
    public IEnumerator nebula()
    {
        movespeed = Random.Range(1f, 2f);
        ////while (GetComponentInParent<Canvas>().pixelRect.yMin - 5 <= transform.position.y)
        ////{
        GetComponent<Rigidbody2D>().mass = movespeed;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -25), ForceMode2D.Impulse);
        //Debug.Log("alive");
        //transform.position -= new Vector3(0, movespeed);
        //yield return new WaitForSeconds(.05f);
        yield return new WaitUntil(() => GetComponentInParent<Canvas>().pixelRect.yMin - 5 >= transform.position.y);
        //}
        //Debug.Log("ded");
        GetComponentInParent<Background>().starlist.Remove(gameObject);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
