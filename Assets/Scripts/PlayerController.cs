using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveValue;
    public float speed;
    private Vector3 oldPosition;
    private int count;
    private int numPickups = 5;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI positionText;
    public TextMeshProUGUI velocityText;

    private void Start()
    {
        count = 0;
        winText.text = "";
        positionText.text = "(0, 0, 0)";
        velocityText.text = "0 m/s";
        oldPosition = transform.position;
        SetCountText();
        SetText();
    }

    void OnMove(InputValue value)
    {
        moveValue = value.Get<Vector2>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(moveValue.x, 0.0f, moveValue.y);

        GetComponent<Rigidbody>().AddForce(movement * speed * Time.fixedDeltaTime);
        SetText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        scoreText.text = "Score: " + count.ToString();
        if (count >= numPickups)
        {
            winText.text = "You win!";
        }
    }

    private void SetText()
    {
        var velocity = ((transform.position - oldPosition).magnitude) / Time.deltaTime;
        oldPosition = transform.position;
        positionText.text = "Position: " + transform.position.ToString();
        velocityText.text = "Velocity: " + velocity + " m/s";
    }
}
