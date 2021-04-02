using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private const int MOVE_X = 550;
    [SerializeField] private const int MOVE_Y = 50;

    [Header("Pieces")]
    [SerializeField] private GameObject[] pieces;

    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenu;

    [Header("Options Menu")]
    [SerializeField] private GameObject optionsMenu;

    [Header("Victory Menu")]
    [SerializeField] private GameObject victoryMenu;
    [SerializeField] private TextMeshProUGUI textMovesTotal;
    [SerializeField] private TextMeshProUGUI textTimeTotal;

    [Header("Gameplay")]
    [SerializeField] private GameObject gameplay;
    [SerializeField] private TextMeshProUGUI textMovesCounter;
    [SerializeField] private TextMeshProUGUI textTimeCounter;

    private int movesCounter;
    private float timeCounter;

    private StackFILO stackLeft;
    private StackFILO stackMiddle;
    private StackFILO stackRight;

    private bool gameRunning;

    private void Start()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        victoryMenu.SetActive(false);
        gameplay.SetActive(false);
    }

    private void Update()
    {
        if (gameRunning)
        {
            timeCounter += Time.deltaTime;
            textTimeCounter.text = string.Format("{0:0.00}", timeCounter);
        }
    }

    public void Initialize()
    {
        gameRunning = true;

        movesCounter = 0;
        timeCounter = 0f;
        textMovesCounter.text = "0";

        InitializeStacks();
    }

    public StackFILO GetStackLeft()
    {
        return stackLeft;
    }

    public StackFILO GetStackMiddle()
    {
        return stackMiddle;
    }

    public StackFILO GetStackRight()
    {
        return stackRight;
    }

    public bool GetGameStarted()
    {
        return gameRunning;
    }

    public void SetGameRunning(bool value)
    {
        gameRunning = value;
    }

    public int GetMoveX()
    {
        return MOVE_X;
    }

    public int GetMoveY()
    {
        return MOVE_Y;
    }

    public void IncreaseTurnCounter()
    {
        movesCounter++;
        textMovesCounter.text = movesCounter.ToString();
    }

    public void CheckVictory()
    {
        if (stackRight.GetIndexCount() == pieces.Length)
        {
            Victory();
        }
    }

    private void InitializeStacks()
    {
        stackLeft = new StackFILO();
        stackMiddle = new StackFILO();
        stackRight = new StackFILO();

        stackLeft.Initialize(pieces.Length);
        stackMiddle.Initialize(pieces.Length);
        stackRight.Initialize(pieces.Length);

        for (int i = 0; i < pieces.Length; i++)
        {
            InitialPosition(pieces[i]);
        }
    }

    private void InitialPosition(GameObject obj)
    {
        var rect = obj.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector3(-MOVE_X, stackLeft.GetIndexCount() * 50, 0f);
        stackLeft.Push(obj);
    }

    private void Victory()
    {
        gameRunning = false;

        SoundManager.soundManager.PlayEndgame();

        gameplay.SetActive(false);
        victoryMenu.SetActive(true);
        textMovesTotal.text = $"With {movesCounter} moves";
        var msg = string.Format("{0:0.00}", timeCounter);
        textTimeTotal.text = $"in {msg} seconds";
    }
}
