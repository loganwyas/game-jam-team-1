using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private new  Rigidbody2D rigidbody;
    private new Collider2D collider;
    public GameObject[] walls;
    private Collider2D[] overlaps = new Collider2D[4];
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        foreach(GameObject wall in walls){
            Physics2D.IgnoreCollision(wall.GetComponent<Collider2D>(), collider, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
