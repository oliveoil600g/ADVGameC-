using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GetGlass getGlass;
    public static GameManager Instance { get; private set; }
    public Vector2 PlayerSpeed;

    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        CurrentState = GameState.Title;

        Debug.Log("GameManager Initialized.");
        Debug.Log("Current State :" + CurrentState);
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;

        Debug.Log("GameState changed: " + newState);
    }
    private void Update()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            getGlass.PutOnGlass();
        }

    }
}
