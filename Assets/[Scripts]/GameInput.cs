using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnJumpAction;
    public event EventHandler OnShootAction;

    private PlayerInput playerInput;


    private void Awake()
    {
        Instance = this;

        playerInput = new PlayerInput();

        playerInput.Enable();

        playerInput.Player.Jump.performed += Jump_performed;
        playerInput.Player.Shoot.performed += Shoot_performed;
    }


    private void OnValidate()
    {
        if (!Application.isPlaying && playerInput == null)
        {
            playerInput = new PlayerInput();
        }
    }

    private void Shoot_performed(InputAction.CallbackContext obj)
    {
        OnShootAction?.Invoke(this, EventArgs.Empty);
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalize()
    {
        Vector2 inputVector = playerInput.Player.Movement.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }

    public void SimulateJump()
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    public void SimulateShoot()
    {
        OnShootAction?.Invoke(this, EventArgs.Empty);
    }

    public void ReassignJumpBinding(InputBinding newBinding)
    {
        playerInput.Player.Jump.ApplyBindingOverride(newBinding);
    }

    public void ReassignShootBinding(InputBinding newBinding)
    {
        playerInput.Player.Shoot.ApplyBindingOverride(newBinding);
    }
}