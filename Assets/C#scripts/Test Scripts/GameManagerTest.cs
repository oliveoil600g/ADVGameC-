using UnityEngine;

public class GameManagerTest : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("===== GameManager Test Start =====");

        // 現在のゲーム状態を確認
        Debug.Log("現在のゲーム状態 : " + GameManager.Instance.CurrentState);

        // 調査状態に変更
        GameManager.Instance.ChangeState(GameState.Investigation);

        // フラグをON
        FlagManager.Instance.SetFlag("FoundClock");

        // フラグを確認
        if (FlagManager.Instance.HasFlag("FoundClock"))
        {
            Debug.Log("時計を発見済みです！");
        }

        Debug.Log("現在のゲーム状態 : " + GameManager.Instance.CurrentState);

        Debug.Log("===== GameManager Test End =====");
    }
}
