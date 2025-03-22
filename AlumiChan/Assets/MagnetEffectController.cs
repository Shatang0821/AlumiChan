using UnityEngine;

public class MagnetEffectController : MonoBehaviour
{
    public Transform target; // �Ώۃu���b�N
    public Transform origin; // �v���C���[ or ���˓_
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (target == null || origin == null) return;

        // �����̍X�V
        Vector3 dir = (target.position - origin.position).normalized;
        transform.position = origin.position;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
    }
}