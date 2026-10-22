using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using System.Collections;

public class GetGlass : MonoBehaviour
{
    [Header("GlassAnimationTime")]
    public int openanimationtime = 2;
    private bool IsGlass = false;
    [Header("Animation")]
    [SerializeField] public GameObject glass;
    [Header("MapInfomation")]
    [SerializeField] public GameObject NotPutonGlassMap;
    [SerializeField] public GameObject PutonGlassMap;

    private void Awake()
    {
        NotPutonGlassMap.SetActive(true);
        PutonGlassMap.SetActive(false);
        glass.SetActive(false);
    }

    public void PutOnGlass()
    {
        StartCoroutine(SetGlass());
    }
    private IEnumerator SetGlass()
    {
        glass.SetActive(true);
        yield return new WaitForSeconds(openanimationtime);
        glass.SetActive(false);
        NotPutonGlassMap.SetActive(IsGlass);
        PutonGlassMap.SetActive(!IsGlass);
        IsGlass = !IsGlass;
    }
}
