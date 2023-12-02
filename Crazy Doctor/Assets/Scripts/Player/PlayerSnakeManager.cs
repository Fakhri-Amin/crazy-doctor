using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSnakeManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float turnSpeed = 180f;
    [SerializeField] private float distanceBetween = 0.3f;
    [SerializeField] private List<GameObject> bodyParts = new();

    private List<GameObject> snakeBody = new();
    private Vector2 movementVector;
    private float countUp = 0;

    private void Start()
    {
        snakeBody.Add(player);
        player.GetComponent<MarkerManager>().ClearMarkerList();
        countUp = 0;
    }

    private void Update()
    {
        movementVector = InputManager.Instance.GetMovementVectorNormalized();

        if (bodyParts.Count > 0)
        {
            CreateBodyPart();
        }
    }

    private void FixedUpdate()
    {
        HandleSnakeMovement();
    }

    private void HandleSnakeMovement()
    {
        snakeBody[0].GetComponent<Rigidbody2D>().velocity = moveSpeed * movementVector;

        if (snakeBody.Count > 1)
        {
            for (int i = 1; i < snakeBody.Count; i++)
            {
                MarkerManager markerManager = snakeBody[i - 1].GetComponent<MarkerManager>();
                if (movementVector == Vector2.zero)
                {
                    // snakeBody[i].transform.SetPositionAndRotation(snakeBody[i].transform.position, snakeBody[i].transform.rotation);
                    snakeBody[i].GetComponent<MarkerManager>().ClearMarkerList();
                    continue;
                }

                snakeBody[i].transform.SetPositionAndRotation(markerManager.markerList[0].position, markerManager.markerList[0].rotation);
                markerManager.markerList.RemoveAt(0);
            }
        }
    }

    private void CreateBodyPart()
    {
        MarkerManager markerManager = snakeBody[^1].GetComponent<MarkerManager>(); // snakeBody[snakeBody.Count - 1]
        if (countUp == 0)
        {
            markerManager.ClearMarkerList();
        }
        countUp += Time.deltaTime;
        if (countUp >= distanceBetween)
        {
            GameObject temp = Instantiate(bodyParts[0], markerManager.markerList[0].position, markerManager.markerList[0].rotation, transform);

            snakeBody.Add(temp);
            bodyParts.RemoveAt(0);
            temp.GetComponent<MarkerManager>().ClearMarkerList();
            countUp = 0;
        }
    }

}
