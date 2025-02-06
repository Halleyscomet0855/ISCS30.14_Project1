using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public float moveSpeed = 3f; // Speed of movement
    public float gridSize = 1f;  // Size of each grid step

    private bool isMoving = false;
    private Vector3 targetPosition;

    private MapManager mapManager;
    public Animator anim;

    private void Awake()
    {mapManager = FindObjectOfType<MapManager>();
    anim = GetComponent<Animator> ();
    anim.SetBool("isWalking", false);}

    void Update()
    {
        if (!isMoving)
        {
            Vector3 moveDirection = Vector3.zero;
            Vector3 MoveDirection;
            int OneWayTest;

            if (Input.GetKey(KeyCode.W)) 
            {
              anim.SetInteger("Direction", 0);
              moveDirection = Vector3.up;
              OneWayTest = 0;
            }
            
            if (Input.GetKey(KeyCode.S)) 
            { 
              anim.SetInteger("Direction", 2);
              moveDirection = Vector3.down;
              OneWayTest = 2;
            }

            if (Input.GetKey(KeyCode.A)) {
              anim.SetInteger("Direction", 3);
              moveDirection = Vector3.left;
              OneWayTest = 3;
              }
            if (Input.GetKey(KeyCode.D)) {
              anim.SetInteger("Direction", 1);
              moveDirection =  Vector3.right;
              OneWayTest = 1;
              }


            if (moveDirection != Vector3.zero)
            {
              targetPosition = transform.position + moveDirection * gridSize;

              bool PassableCheck = mapManager.GetPassable(targetPosition);

              if (PassableCheck == true) {
                
                if (mapManager.GetSpeedy(transform.position)) {
                  moveSpeed = 10f;
                }
                else {
                  moveSpeed = 3f;
                }
                while (mapManager.GetOneWay(targetPosition) == true) {
                  anim.SetBool("isWalking", false);
                  targetPosition += moveDirection*gridSize;
                  if (mapManager.GetPassable(targetPosition) == false) {
                    targetPosition -= moveDirection*gridSize;
                    break;
                  }
                
                }
                
                StartCoroutine(MoveToPosition(targetPosition));
                
             
                
              }
            }
        }
    }




    private System.Collections.IEnumerator MoveToPosition(Vector3 target)
    {

      isMoving = true;
      anim.SetBool("isWalking", true);
      Vector3 MoveDirection;
      while (Vector3.Distance(transform.position, target) > 0.01f)
      {
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        yield return null;
      }
      transform.position = target; // Snap to grid
      while (mapManager.GetForcedWay(transform.position)) {
        anim.SetBool("isWalking", false);
        anim.SetInteger("Direction", 1);
        print("test");
        MoveDirection = mapManager.GetForcedDirection(transform.position);
        target = transform.position + MoveDirection * gridSize;
        print(targetPosition);
        print(mapManager.GetPassable(target));
        if (mapManager.GetPassable(target) == false) {
          break; 
          }
        else {
          while (Vector3.Distance(transform.position, target) > 0.01f)
            {
              transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
              yield return null;
            }
        }
        transform.position = target;
      }
      isMoving = false;
      anim.SetBool("isWalking", false);
      anim.SetInteger("Direction", 0);
    }
}
