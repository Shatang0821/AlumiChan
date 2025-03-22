//  EllipseCollider2D.cs
//  http://kan-kikuchi.hatenablog.com/entry/EllipseCollider2D
//
//  Created by kan.kikuchi on 2023.05.13.
using UnityEngine;

/// <summary>
/// �ȉ~�`�̃R���C�_�[(��PolygonCollider2D�ŕ\������N���X)
/// </summary>
[RequireComponent(typeof(PolygonCollider2D))]
public class EllipseCollider2D : MonoBehaviour
{

    private PolygonCollider2D _polygonCollider2D;

    //X��Y�̑傫��
    [SerializeField]
    private float _xSize = 1.0f, _ySize = 1.0f;
    public float XSize
    {
        get => _xSize;
        set
        {
            _xSize = value;
            UpdateCollider();
        }
    }
    public float YSize
    {
        get => _ySize;
        set
        {
            _ySize = value;
            UpdateCollider();
        }
    }

    //�_�̐�(�����قǊ��炩��)
    [SerializeField]
    private int _points = 64;
    public int Points
    {
        get => _points;
        set
        {
            _points = value;
            UpdateCollider();
        }
    }

    //=================================================================================
    //������
    //=================================================================================

    private void Awake()
    {
        UpdateCollider();
    }

    private void OnValidate()
    {
        //�ݒ�l���ύX���ꂽ��R���C�_�[�̌`���X�V
        UpdateCollider();
    }

    //=================================================================================
    //�X�V
    //=================================================================================

    /// <summary>
    /// ���݂̃X�P�[������R���C�_�[�̌`�X�V
    /// </summary>
    public void UpdateCollider()
    {
        if (_polygonCollider2D == null)
        {
            _polygonCollider2D = GetComponent<PolygonCollider2D>();
        }

        var points = new Vector2[_points];
        var angle = 0f;
        var deltaAngle = 2f * Mathf.PI / _points;

        for (var i = 0; i < _points; i++)
        {
            var x = Mathf.Cos(angle) * _xSize * 0.5f;
            var y = Mathf.Sin(angle) * _ySize * 0.5f;

            points[i] = new Vector2(x, y);

            angle += deltaAngle;
        }

        _polygonCollider2D.points = points;
    }

}