using PlayerControl.StateMachines;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;
    private StateMachinesController _stateMachinesController;
    private void Awake()
    {
        _stateMachinesController = GetComponent<StateMachinesController>();
    }
    private void OnEnable()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
        _playerInput.Main.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Main.Jump.performed -= Jump;
    }
    private void FixedUpdate()
    {
        _stateMachinesController.InputToStateMachine(_playerInput.Main.MoveX);
    }
    private void Jump(InputAction.CallbackContext callbackContext)
    {
        _stateMachinesController.InputToStateMachine(_playerInput.Main.Jump);
    }
}
