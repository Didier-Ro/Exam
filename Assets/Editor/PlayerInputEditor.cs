using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using static GameInput;

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
                    /*ShowBindings("Jump", playerInput.Player.Jump);
                    ShowBindings("Shoot", playerInput.Player.Shoot);
                    ShowBindings("Movement", playerInput.Player.Movement);*/
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Box("Jump Keyboard: ");
                    GUILayout.Box(gameInput.GetBindingText(GameInput.Binding.Jump));
                    EditorGUILayout.EndHorizontal();
                    GUILayout.Space(10);
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Box("Jump Gamepad: ");
                    GUILayout.Box(gameInput.GetBindingText(GameInput.Binding.Gamepad_Jump));
                    EditorGUILayout.EndHorizontal();
                    GUILayout.Space(10);
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Box("Shoot Keyboard: ");
                    GUILayout.Box(gameInput.GetBindingText(GameInput.Binding.Shoot));
                    EditorGUILayout.EndHorizontal();
                    GUILayout.Space(10);
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Box("Shoot Gamepad: ");
                    GUILayout.Box(gameInput.GetBindingText(GameInput.Binding.Gamepad_Shoot));
                    EditorGUILayout.EndHorizontal();
                }
                else
                {
                    GUILayout.Label("PlayerInput no está inicializado");
                }
            }
        }


        if (Application.isPlaying) 
        {
            GUILayout.Space(10);
            GUILayout.Box("Movement Vector");

            Vector2 movementVector = gameInput.GetMovementVectorNormalize();
            GUILayout.Label("Movement Vector: " + movementVector);

            GUILayout.Space(10);
            GUILayout.Box("Simulate Actions");

            if (GUILayout.Button("Jump"))
            {
                gameInput.SimulateJump();
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Shoot"))
            {
                gameInput.SimulateShoot();
            }

            GUILayout.Space(25);
            GUILayout.Box("Rebind Zone");

            if (GUILayout.Button("Rebind Jump Keyboard"))
            {
                gameInput.RebindBinding(GameInput.Binding.Jump, () => { });
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Rebind Jump Gamepad"))
            {
                gameInput.RebindBinding(GameInput.Binding.Gamepad_Jump, () => { });
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Rebind Shoot Keyboard"))
            {
                gameInput.RebindBinding(GameInput.Binding.Shoot, () => { });
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Rebind Shoot Gamepad"))
            {
                gameInput.RebindBinding(GameInput.Binding.Gamepad_Shoot, () => { });
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
