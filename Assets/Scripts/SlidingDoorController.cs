using System.Collections;
using UnityEngine;
using TMPro;

public class SlidingDoorController : MonoBehaviour
{
    public bool open;
    public float slideDistance;
    public Transform door;
    public TextMeshPro text;
    private Vector2 originalPos;
    private Coroutine doorStateRoutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.transform.rotation = Quaternion.Euler(0, 0, 0);
        text.transform.position = (Vector2)door.position + (Vector2.up * 2f);
        text.text = "";
        open = false;
        originalPos = door.position;
        slideDistance = door.lossyScale.y;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (doorStateRoutine != null)
            {
                StopCoroutine(doorStateRoutine);
            }
            doorStateRoutine = StartCoroutine(ChangeDoorState(true));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (doorStateRoutine != null)
            {
                StopCoroutine(doorStateRoutine);
            }
            doorStateRoutine = StartCoroutine(ChangeDoorState(false));
        }
    }

    void moveDoor()
    {
        if ((Vector2)door.position != originalPos && !open)
        {
            door.position = Vector2.Lerp(door.position, originalPos, Time.deltaTime * 2f);
        }
        else if ((Vector2)door.position != originalPos + (slideDistance * (Vector2)door.up) && open)
        {
            door.position = Vector2.Lerp(door.position, originalPos + (slideDistance * (Vector2)door.up), Time.deltaTime * 2f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        moveDoor();
    }

    private IEnumerator ChangeDoorState(bool state)
    {
        if (state)
        {
            text.text = "SCANNING...";
        }
        yield return new WaitForSeconds(3f);
        open = state;
        if (state)
        {
            text.text = "ID CONFIRMED: OPENING";
        }
        else
        {
            text.text = "CLOSING";
            yield return new WaitForSeconds(1f);
            text.text = "";
        }
        Debug.Log(open);
    }
    
}
