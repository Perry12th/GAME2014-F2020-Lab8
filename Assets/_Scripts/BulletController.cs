using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour, IApplyDamage
{
    public float verticalSpeed;
    public float verticalBoundary;
    public int damage;
    public ContactFilter2D contactFilter2D;
    public List<Collider2D> colliders;
    public Vector3 direction;

    private BoxCollider2D boxCollider;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        direction = Vector3.up;
    }
    
    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
        _CheckCollisions();
    }

    private void _CheckCollisions()
    {
        Physics2D.GetContacts(boxCollider, contactFilter2D, colliders);

        if (colliders.Count > 0 && colliders[0] != null)
        {
           // Debug.Log("Hit: " + colliders[0].gameObject.name);
            BulletManager.Instance().ReturnBullet(gameObject);
        }
        
    }

    private void _Move()
    {
        transform.position += direction * verticalSpeed * Time.deltaTime;
    }

    private void _CheckBounds()
    {
        if (transform.position.y > verticalBoundary)
        {
            BulletManager.Instance().ReturnBullet(gameObject);
        }
    }

    //public void OnTriggerEnter2D(Collider2D other)
    //{
    //    //Debug.Log(other.gameObject.name);
    //    BulletManager.Instance().ReturnBullet(gameObject);
    //}

    public int ApplyDamage()
    {
        return damage;
    }
}
