using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float destroyWaitTime = 2f;
    // Start is called before the first frame update
    public enum Direction
    {
        NotSet,
        Up,
        Down,
        Left,
        Right,
    }

    public Direction direction = Direction.NotSet;
    public float speed = 1f;
    private void Start()
    {
        StartCoroutine(DestroyItem(destroyWaitTime));
    }

    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case Direction.Up:
                transform.position = transform.position + new Vector3(0f, speed * Time.deltaTime, 0f);
                break;
            case Direction.Down:
                transform.position = transform.position + new Vector3(0f, -speed * Time.deltaTime, 0f);
                break;
            case Direction.Left:
                transform.position = transform.position + new Vector3(-speed * Time.deltaTime, 0f, 0f);
                break;
            case Direction.Right:
                transform.position = transform.position + new Vector3(speed * Time.deltaTime, 0f, 0f);
                break;
        }
    }

    IEnumerator DestroyItem(float destroyWaitTime)
    {
        yield return new WaitForSeconds(destroyWaitTime);
        Destroy(gameObject);
    }
}
