using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public const int MOVE_X = 550;

    [Header("Menus & Gameplay")]
    [SerializeField] private Button optionsButton;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject victoryMenu;
    [SerializeField] private GameObject gameplay;
    [SerializeField] private TextMeshProUGUI textMovesCounter;
    [SerializeField] private TextMeshProUGUI textMovesTotal;
    [SerializeField] private TextMeshProUGUI textTimeCounter;
    [SerializeField] private TextMeshProUGUI textTimeTotal;
    private int movesCounter;
    private float timeCounter;

    [Header("Pieces")]
    //[SerializeField] private int amountPieces = 5;
    [SerializeField] private GameObject yellowPiece;
    [SerializeField] private GameObject bluePiece;
    [SerializeField] private GameObject greenPiece;
    [SerializeField] private GameObject redPiece;
    [SerializeField] private GameObject fuchsiaPiece;

    private StackFILO stackLeft;
    private StackFILO stackMiddle;
    private StackFILO stackRight;

    private bool gameStarted;

    private void Awake()
    {
        gameStarted = false;
    }

    private void Start()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        victoryMenu.SetActive(false);
        gameplay.SetActive(false);
    }

    private void Update()
    {
        if (gameStarted)
        {
            timeCounter += Time.deltaTime;
            string msg = string.Format("{0:0.00}", timeCounter);
            textTimeCounter.text = msg;
        }
    }

    public void Initialize()
    {
        gameStarted = true;

        movesCounter = 0;
        timeCounter = 0f;
        textMovesCounter.text = movesCounter.ToString();

        InitializeStacks();
    }

    public void PeekStacks()
    {
        if (!stackLeft.IsEmpty())
        {
            var aux = stackLeft.Peek();
            print("L: " + aux);
        }
        else
        {
            print("L: ");
        }
        if (!stackMiddle.IsEmpty())
        {
            var aux = stackMiddle.Peek();
            print("M: " + aux);
        }
        else
        {
            print("M: ");
        }
        if (!stackRight.IsEmpty())
        {
            var aux = stackRight.Peek();
            print("R: " + aux);
        }
        else
        {
            print("R: ");
        }
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
        return gameStarted;
    }

    public void SetGameStarted(bool value)
    {
        gameStarted = value;
    }

    public int GetMoveX()
    {
        return MOVE_X;
    }

    public void IncreaseTurnCounter()
    {
        movesCounter++;
        textMovesCounter.text = movesCounter.ToString();
    }

    public void CheckVictory()
    {
        if (stackRight.GetIndexCount() == 5)
        {
            Victory();
        }
    }

    private void InitializeStacks()
    {
        stackLeft = new StackFILO();
        stackMiddle = new StackFILO();
        stackRight = new StackFILO();

        stackLeft.Initialize();
        stackMiddle.Initialize();
        stackRight.Initialize();

        InitialPosition(fuchsiaPiece);
        InitialPosition(redPiece);
        InitialPosition(greenPiece);
        InitialPosition(bluePiece);
        InitialPosition(yellowPiece);
    }

    private void InitialPosition(GameObject obj)
    {
        var rect = obj.GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector3(-MOVE_X, stackLeft.GetIndexCount() * 50, 0f);
        stackLeft.Push(obj);
    }

    private void Victory()
    {
        gameStarted = false;

        SoundManager.soundManager.PlaySound("Endgame", 1f);

        gameplay.SetActive(false);
        victoryMenu.SetActive(true);
        textMovesTotal.text = $"With {movesCounter} moves";
        string msg = string.Format("{0:0.00}", timeCounter);
        textTimeTotal.text = $"in {msg} seconds";
    }
}
