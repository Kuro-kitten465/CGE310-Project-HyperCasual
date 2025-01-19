using UnityEngine;
using KuroNeko.Utilities.DesignPattern;

public class InputManager : MonoBehaviour
{
    private ICommand currentCommand;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // On click or tap
        {
            if (GameManager.Instance != null)
            {
                KnifeController knife = GameManager.Instance.SpawnKnife();
                currentCommand = new ThrowKnifeCommand(knife);
                currentCommand.Execute();
            }
        }
    }
}