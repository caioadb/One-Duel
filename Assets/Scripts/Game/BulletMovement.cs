using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [Range(4f, 7f)]
    public float _speed = 5f;
    private void Update()
    {
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
            collision.gameObject.SendMessage("TakeDamage");
        }
        else if (collision.gameObject.CompareTag("Enemy") && this.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            collision.gameObject.SendMessage("TakeDamage");
        }
    }

}
