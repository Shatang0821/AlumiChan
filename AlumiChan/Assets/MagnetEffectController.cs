using UnityEngine;

public class MagnetEffectController : MonoBehaviour
{
    public Transform target; // 対象ブロック
    public Transform origin; // プレイヤー or 発射点
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (target == null || origin == null) return;

        // 向きの更新
        Vector3 dir = (target.position - origin.position).normalized;
        transform.position = origin.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
    }
}