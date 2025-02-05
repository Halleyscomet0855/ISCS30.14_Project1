using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float gridSize = 1f;  // Size of each grid step

    private bool isMoving = false;
    private Vector3 targetPosition;

    void Update()
    {
        if (!isMoving)
        {
            Vector3 moveDirection = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.W)) moveDirection = Vector3.up;
            if (Input.GetKeyDown(KeyCode.S)) moveDirection = Vector3.down;
            if (Input.GetKeyDown(KeyCode.A)) moveDirection = Vector3.left;
            if (Input.GetKeyDown(KeyCode.D)) moveDirection = Vector3.right;

            if (moveDirection != Vector3.zero)
            {
                targetPosition = transform.position + moveDirection * gridSize;
                StartCoroutine(MoveToPosition(targetPosition));
            }
        }
    }

    private System.Collections.IEnumerator MoveToPosition(Vector3 target)
    {
        isMoving = true;
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = target; // Snap to grid
        isMoving = false;
    }
}
