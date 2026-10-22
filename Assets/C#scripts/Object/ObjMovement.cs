using UnityEngine;

public class ObjMovement : MonoBehaviour
{
    public BackGourndType backGourndType = BackGourndType.Far;
    float percent;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    void Start()
    {
        rb   = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        percent = backGourndType switch
        {
            BackGourndType.Far => 1,
            BackGourndType.Medium => 0.5f,
            BackGourndType.Near => 0,
        };
        rb.linearVelocity = GameManager.Instance.PlayerSpeed * percent;
    }
}

public enum BackGourndType
{
    Far,
    Medium,
    Near
}
