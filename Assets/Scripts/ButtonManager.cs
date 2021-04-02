using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private Button optionsButton;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject victoryMenu;
    [SerializeField] private GameObject gameplay;

    [SerializeField] private Button leftPillarRightButton;
    [SerializeField] private Button leftPillarDoubleRightButton;
    [SerializeField] private Button middlePillarLeftButton;
    [SerializeField] private Button middlePillarRightButton;
    [SerializeField] private Button rightPillarDoubleLeftButton;
    [SerializeField] private Button rightPillarLeftButton;

    private StackFILO stackLeft;
    private StackFILO stackMiddle;
    private StackFILO stackRight;

    public void PlayButton()
    {
        SoundManager.soundManager.PlayClick();

        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        victoryMenu.SetActive(false);
        gameplay.SetActive(true);

        gameManager.Initialize();

        stackLeft = gameManager.GetStackLeft();
        stackMiddle = gameManager.GetStackMiddle();
        stackRight = gameManager.GetStackRight();

        CheckTopItem();
    }

    public void LeftPillarRightButton()
    {
        MoveItem(stackLeft, stackMiddle, 1);
        MoveButton();
    }

    public void LeftPillarDoubleRightButton()
    {
        MoveItem(stackLeft, stackRight, 2);
        MoveButton();
        gameManager.CheckVictory();
    }

    public void MiddlePillarLeftButton()
    {
        MoveItem(stackMiddle, stackLeft, -1);
        MoveButton();
    }

    public void MiddlePillarRightButton()
    {
        MoveItem(stackMiddle, stackRight, 1);
        MoveButton();
        gameManager.CheckVictory();
    }

    public void RightPillarLeftButton()
    {
        MoveItem(stackRight, stackMiddle, -1);
        MoveButton();
    }

    public void RightPillarDoubleLeftButton()
    {
        MoveItem(stackRight, stackLeft, -2);
        MoveButton();
    }

    public void OptionsButton()
    {
        gameManager.SetGameRunning(false);

        SoundManager.soundManager.PlayClick();

        optionsMenu.SetActive(true);
        gameplay.SetActive(false);
    }

    public void ResumeButton()
    {
        gameManager.SetGameRunning(true);

        SoundManager.soundManager.PlayClick();

        optionsMenu.SetActive(false);
        gameplay.SetActive(true);
    }

    public void ExitButton()
    {
        SoundManager.soundManager.PlayClick();

        Invoke(nameof(ExitApp), 0.418f);
    }

    private void CheckTopItem()
    {
        var itemLeftSize = stackLeft.IsEmpty() ? 0 : stackLeft.Peek().gameObject.GetComponent<RectTransform>().sizeDelta.x;
        var itemMiddleSize = stackMiddle.IsEmpty() ? 0 : stackMiddle.Peek().gameObject.GetComponent<RectTransform>().sizeDelta.x;
        var itemRightSize = stackRight.IsEmpty() ? 0: stackRight.Peek().gameObject.GetComponent<RectTransform>().sizeDelta.x;
        
        leftPillarRightButton.interactable = (((itemLeftSize < itemMiddleSize) || (itemMiddleSize == 0)) && itemLeftSize != 0);
        leftPillarDoubleRightButton.interactable = (((itemLeftSize < itemRightSize) || (itemRightSize == 0)) && itemLeftSize != 0);
        middlePillarLeftButton.interactable = (((itemMiddleSize < itemLeftSize) || (itemLeftSize == 0)) && itemMiddleSize != 0);
        middlePillarRightButton.interactable = (((itemMiddleSize < itemRightSize) || (itemRightSize == 0)) && itemMiddleSize != 0);
        rightPillarDoubleLeftButton.interactable = (((itemRightSize < itemLeftSize) || (itemLeftSize == 0)) && itemRightSize != 0);
        rightPillarLeftButton.interactable = (((itemRightSize < itemMiddleSize) || (itemMiddleSize == 0)) && itemRightSize != 0);
    }

    private void MoveItem(StackFILO stackOrigin, StackFILO stackDestiny, int amountMovement)
    {
        var obj = stackOrigin.Pop();
        var rect = obj.GetComponent<RectTransform>();
        var moveX = rect.anchoredPosition.x + (gameManager.GetMoveX() * amountMovement);
        var moveY = stackDestiny.GetIndexCount() * gameManager.GetMoveY();
        rect.anchoredPosition = new Vector3(moveX, moveY, 0f);
        stackDestiny.Push(obj);
    }

    private void MoveButton()
    {
        SoundManager.soundManager.PlayClick();
        gameManager.IncreaseTurnCounter();
        CheckTopItem();
    }

    private void ExitApp()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
