using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private GameManager gm;
    private Vector2 movementInput;
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool attackInput { get; private set; }
    private bool pauseInput;

    private void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>(); ;
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();

        NormInputX = Mathf.RoundToInt(movementInput.x);
        NormInputY = Mathf.RoundToInt(movementInput.y);
    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            attackInput = true;
        }

        if (context.canceled)
        {
            attackInput = false;
        }
    }

    public void OnPauseInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            pauseInput = !pauseInput;
        }

        if (pauseInput)
        {
            gm.PauseGame();
        }
        else
        {
            gm.ResumeGame();
        }
    }
}
