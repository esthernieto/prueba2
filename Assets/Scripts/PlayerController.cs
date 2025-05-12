using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputSystem_Actions inputSystem;
    private NavMeshAgent agent;

    [SerializeField] private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        inputSystem = new InputSystem_Actions();
        inputSystem.Enable();

        inputSystem.Player.MouseClick.performed += (InputAction.CallbackContext cb) =>
        {
            MovePlayer();
        };
    }

    void MovePlayer()
    {
        var mouseClick = Camera.main.ScreenPointToRay(inputSystem.Player.Mouse.ReadValue<Vector2>());
        RaycastHit hit;
        if (Physics.Raycast(mouseClick, out hit))
        {
            agent.destination = hit.point;
        }
    }

    void Update()
    {
        bool isWalking = agent.velocity.magnitude > 0.1f && agent.remainingDistance > agent.stoppingDistance;
        animator.SetBool("isWalking", isWalking);
    }
}
