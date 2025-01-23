using UnityEngine;
using KuroNeko.Utilities.DesignPattern;

public class InputManager : MonoBehaviour
{
    private ICommand currentCommand;
    [SerializeField] private GameLogicManager gameLogicManager;

    private void Update()
    {
        if (gameLogicManager.IsGameOver) return;
        
        if (Input.GetMouseButtonDown(0)) // On click or tap
        {
            if (GameManager.Instance != null)
            {
                KnifeController knife = gameLogicManager.SpawnKnife();
                currentCommand = new ThrowKnifeCommand(knife);
                currentCommand.Execute();
            }
        }
    }
}