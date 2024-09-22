using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[CustomEditor(typeof(GameInput))]
public class PlayerInputEditor : Editor
{
    private GameInput gameInput;

    private string newBindingJump;
    private string newBindingShoot;

    private void OnEnable()
    {
        gameInput = (GameInput)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (gameInput != null)
        {
            if (gameInput != null && gameInput.GetType().GetField("playerInput", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance) != null)
            {
                PlayerInput playerInput = (PlayerInput)gameInput.GetType().GetField("playerInput", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(gameInput);

                if (playerInput != null)
                {
                    ShowBindings("Jump", playerInput.Player.Jump);
                    ShowBindings("Shoot", playerInput.Player.Shoot);
                    ShowBindings("Movement", playerInput.Player.Movement);
                }
                else
                {
                    GUILayout.Label("PlayerInput no está inicializado");
                }
            }
        }


        if (Application.isPlaying) 
        {
            Vector2 movementVector = gameInput.GetMovementVectorNormalize();
            GUILayout.Label("Movement Vector: " + movementVector);

            if (GUILayout.Button("Jump"))
            {
                gameInput.SimulateJump();
            }

            if (GUILayout.Button("Shoot"))
            {
                gameInput.SimulateShoot();
            }
        }
        else
        {
            GUILayout.Label("El juego no está corriendo. Las acciones solo pueden ser simuladas en modo de juego.");
        }
    }

    private void ShowBindings(string actionName, InputAction inputAction)
    {
        GUILayout.Box($"Action: {actionName}");
        foreach (InputBinding binding in inputAction.bindings)
        {
            GUILayout.Label($" Binding: {binding.path}");
        }
    }
}
