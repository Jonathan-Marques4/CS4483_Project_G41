using UnityEngine;

public class PlaceholderCharacterMove : MonoBehaviour
{
    public float moveSpeed;

    public Transform camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
        rotateToMouse();
        camera.position = new Vector3(transform.position.x, transform.position.y, camera.position.z);
    }

    void move()
    {
        float x =  Input.GetAxis("Horizontal");
        float y =  Input.GetAxis("Vertical");
        
        Vector2 movement = new Vector2(x, y);
        
        transform.position += (Vector3)movement * (moveSpeed * Time.deltaTime);
    }

    void rotateToMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;
        float rotationAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
    }
}
