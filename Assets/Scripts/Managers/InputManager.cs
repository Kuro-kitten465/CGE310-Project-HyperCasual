using UnityEngine;
using KuroNeko.Utilities.DesignPattern;

public class InputManager : MonoBehaviour
{
    private ICommand currentCommand;
    [SerializeField] private GameLogicManager gameLogicManager;
    [SerializeField] private GameObject startText;

    private void Update()
    {
        if (gameLogicManager.IsGameOver) return;

#if UNITY_ANDROID || UNITY_IOS
        var touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
            TapHandle();
#endif

#if UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
            TapHandle();
#endif

#if UNITY_WEBGL
        if (Input.GetMouseButtonDown(0))
            TapHandle();
#endif
    }

    private void TapHandle()
    {
        if (GameManager.Instance != null)
        {
            startText.SetActive(false);
            gameLogicManager.IsGameStarted = true;
            gameLogicManager.tapSource.Play();
            KnifeController knife = gameLogicManager.SpawnKnife();
            currentCommand = new ThrowKnifeCommand(knife);
            currentCommand.Execute();
        }
    }
}