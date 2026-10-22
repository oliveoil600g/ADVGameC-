using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    public static FlagManager Instance { get; private set; }

    private HashSet<string> flags = new HashSet<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("FlagManager Initialized");
    }

    //フラグを追加する
    public void SetFlag(string flagName)
    {
        flags.Add(flagName);

        Debug.Log("Flag ON: " + flagName);
    }
    //今どのフラグを持っているかを確認する
    public bool HasFlag(string flagName)
    {
        return flags.Contains(flagName);
    }
    //既存のフラグを削除する
    public void RemoveFlag(string flagName)
    {
        flags.Remove(flagName);

        Debug.Log("Flag OFF: " + flagName);
    }
}
