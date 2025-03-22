//  EllipseCollider2D.cs
//  http://kan-kikuchi.hatenablog.com/entry/EllipseCollider2D
//
//  Created by kan.kikuchi on 2023.05.13.
using UnityEngine;

/// <summary>
/// 楕円形のコライダー(をPolygonCollider2Dで表現するクラス)
/// </summary>
[RequireComponent(typeof(PolygonCollider2D))]
public class EllipseCollider2D : MonoBehaviour
{

    private PolygonCollider2D _polygonCollider2D;

    //XとYの大きさ
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

    //点の数(多いほど滑らかに)
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
    //初期化
    //=================================================================================

    private void Awake()
    {
        UpdateCollider();
    }

    private void OnValidate()
    {
        //設定値が変更されたらコライダーの形を更新
        UpdateCollider();
    }

    //=================================================================================
    //更新
    //=================================================================================

    /// <summary>
    /// 現在のスケールからコライダーの形更新
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