using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    public static GameInput Instance { get; private set; }

    public event EventHandler OnJumpAction;
    public event EventHandler OnShootAction;

    public event EventHandler OnBindingRebind;

    private PlayerInput playerInput;

    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Jump,
        Shoot,
        Gamepad_Jump,
        Gamepad_Shoot,
    }

    private void Awake()
    {
        Instance = this;

        playerInput = new PlayerInput();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInput.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        playerInput.Enable();

        playerInput.Player.Jump.performed += Jump_performed;
        playerInput.Player.Shoot.performed += Shoot_performed;
    }


    private void OnValidate()
    {
        if (!Application.isPlaying && playerInput == null)
        {
            playerInput = new PlayerInput();

            if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
            {
                playerInput.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
            }
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

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                return playerInput.Player.Movement.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return playerInput.Player.Movement.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return playerInput.Player.Movement.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return playerInput.Player.Movement.bindings[4].ToDisplayString();
            case Binding.Jump:
                return playerInput.Player.Jump.bindings[0].ToDisplayString();
            case Binding.Shoot:
                return playerInput.Player.Shoot.bindings[0].ToDisplayString();
            case Binding.Gamepad_Jump:
                return playerInput.Player.Jump.bindings[1].ToDisplayString();
            case Binding.Gamepad_Shoot:
                return playerInput.Player.Shoot.bindings[1].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        playerInput.Player.Disable();

        InputAction inputAction;
        int bindingIndex;
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = playerInput.Player.Movement;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerInput.Player.Movement;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = playerInput.Player.Movement;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = playerInput.Player.Movement;
                bindingIndex = 4;
                break;
            case Binding.Jump:
                inputAction = playerInput.Player.Jump;
                bindingIndex = 0;
                break;
            case Binding.Shoot:
                inputAction = playerInput.Player.Shoot;
                bindingIndex = 0;
                break;
            case Binding.Gamepad_Jump:
                inputAction = playerInput.Player.Jump;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_Shoot:
                inputAction = playerInput.Player.Shoot;
                bindingIndex = 1;
                break;
        }
        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback => {
                callback.Dispose();
                playerInput.Player.Enable();
                onActionRebound();

                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInput.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();

                OnBindingRebind?.Invoke(this, EventArgs.Empty);
            })
            .Start();
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