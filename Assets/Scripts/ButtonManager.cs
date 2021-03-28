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

    private void Update()
    {
        if (gameManager.GetGameStarted())
        {
            CheckTopItem();
        }
    }

    public void PlayButton()
    {
        SoundManager.soundManager.PlaySound("Click", 1f);

        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        victoryMenu.SetActive(false);
        gameManager.Initialize();
        gameplay.SetActive(true);

        stackLeft = gameManager.GetStackLeft();
        stackMiddle = gameManager.GetStackMiddle();
        stackRight = gameManager.GetStackRight();
    }

    public void LeftPillarRightButton()
    {
        SoundManager.soundManager.PlaySound("Click", 1f);

        MovePosition(stackLeft, stackMiddle, 1);
        gameManager.IncreaseTurnCounter();
    }

    public void LeftPillarDoubleRightButton()
    {
        SoundManager.soundManager.PlaySound("Click", 1f);

        MovePosition(stackLeft, stackRight, 2);
        gameManager.IncreaseTurnCounter();

        gameManager.CheckVictory();
    }

    public void MiddlePillarLeftButton()
    {
        SoundManager.soundManager.PlaySound("Click", 1f);

        MovePosition(stackMiddle, stackLeft, -1);
        gameManager.IncreaseTurnCounter();
    }

    public void MiddlePillarRightButton()
    {
        SoundManager.soundManager.PlaySound("Click", 1f);

        MovePosition(stackMiddle, stackRight, 1);
        gameManager.IncreaseTurnCounter();
        gameManager.CheckVictory();

    }

    public void RightPillarLeftButton()
    {
        SoundManager.soundManager.PlaySound("Click", 1f);

        MovePosition(stackRight, stackMiddle, -1);
        gameManager.IncreaseTurnCounter();
    }

    public void RightPillarDoubleLeftButton()
    {
        SoundManager.soundManager.PlaySound("Click", 1f);

        MovePosition(stackRight, stackLeft, -2);
        gameManager.IncreaseTurnCounter();
    }

    public void OptionsButton()
    {
        gameManager.SetGameStarted(false);

        SoundManager.soundManager.PlaySound("Click", 1f);

        optionsMenu.SetActive(true);
        gameplay.SetActive(false);
    }

    public void ResumeButton()
    {
        gameManager.SetGameStarted(true);

        SoundManager.soundManager.PlaySound("Click", 1f);

        optionsMenu.SetActive(false);
        gameplay.SetActive(true);
    }

    public void ExitButton()
    {
        SoundManager.soundManager.PlaySound("Click", 1f);

        Invoke(nameof(ExitApp), 0.418f);
    }

    private void CheckTopItem()
    {
        var itemLeftSize = stackLeft.Peek() != null ? stackLeft.Peek().gameObject.GetComponent<RectTransform>().sizeDelta.x : 0;
        var itemMiddleSize = stackMiddle.Peek() != null ? stackMiddle.Peek().gameObject.GetComponent<RectTransform>().sizeDelta.x : 0;
        var itemRightSize = stackRight.Peek() != null ? stackRight.Peek().gameObject.GetComponent<RectTransform>().sizeDelta.x : 0;
        
        leftPillarRightButton.interactable = (((itemLeftSize < itemMiddleSize) || (itemMiddleSize == 0)) && itemLeftSize != 0);
        leftPillarDoubleRightButton.interactable = (((itemLeftSize < itemRightSize) || (itemRightSize == 0)) && itemLeftSize != 0);
        middlePillarLeftButton.interactable = (((itemMiddleSize < itemLeftSize) || (itemLeftSize == 0)) && itemMiddleSize != 0);
        middlePillarRightButton.interactable = (((itemMiddleSize < itemRightSize) || (itemRightSize == 0)) && itemMiddleSize != 0);
        rightPillarDoubleLeftButton.interactable = (((itemRightSize < itemLeftSize) || (itemLeftSize == 0)) && itemRightSize != 0);
        rightPillarLeftButton.interactable = (((itemRightSize < itemMiddleSize) || (itemMiddleSize == 0)) && itemRightSize != 0);
    }

    private void MovePosition(StackFILO stackOrigin, StackFILO stackDestiny, int amountMovement)
    {
        var obj = stackOrigin.Pop();
        var rect = obj.GetComponent<RectTransform>();
        var move = rect.anchoredPosition.x + (gameManager.GetMoveX() * amountMovement);
        rect.anchoredPosition = new Vector3(move, stackDestiny.GetIndexCount() * 50, 0f);
        stackDestiny.Push(obj);
    }

    private void ExitApp()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
}
