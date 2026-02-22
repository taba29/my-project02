using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TetrisMini : MonoBehaviour
{
    [Header("Grid")]
    public int width = 10;
    public int height = 18;
    public float tickSec = 0.35f;

    [Header("UI")]
    public TMP_Text scoreText;

    // 盤面：true=埋まってる
    private bool[,] filled;
    private float t;
    private int score;

    // 今落ちてる “1マス” ブロック（最小）
    private int bx, by; // ブロック座標

    // 見た目（Gizmos）
    public float cellSize = 0.4f;
    public Vector2 drawOrigin = new Vector2(-2.2f, -3.5f);

    void Start()
{
    filled = new bool[width, height];
    Spawn();
    UpdateUI();
}


    void Update()
    {
        // 左右（キーでもスマホでも拡張しやすいよう最小）
        if (Input.GetKeyDown(KeyCode.LeftArrow)) Move(-1);
        if (Input.GetKeyDown(KeyCode.RightArrow)) Move(+1);
        if (Input.GetKeyDown(KeyCode.DownArrow)) DropStep();

        t += Time.deltaTime;
        if (t >= tickSec)
        {
            t = 0f;
            DropStep();
        }
    }

    public void DropNow()
    {
        Debug.Log("✅ DropNow called");
        DropStep();
    }

    void Spawn()
    {
        bx = width / 2;
        by = height - 1;
        if (filled[bx, by])
        {
            // GameOver（簡易）
            ClearAll();
        }
    }

    void ClearAll()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                filled[x, y] = false;
        score = 0;
        Spawn();
        UpdateUI();
    }

    void Move(int dx)
    {
        int nx = bx + dx;
        if (nx < 0 || nx >= width) return;
        if (filled[nx, by]) return;
        bx = nx;
    }

    void DropStep()
    {
        if (filled == null)
    {
        Debug.LogWarning("filled was null -> init");
        filled = new bool[width, height];
        Spawn();
        UpdateUI();
    }

    // ↓ここから既存のDropStep

        int ny = by - 1;
        if (ny >= 0 && !filled[bx, ny])
        {
            by = ny;
            return;
        }

        // 着地
        filled[bx, by] = true;

        // ライン消去（横一列全部trueなら消す）
        int cleared = 0;
        for (int y = 0; y < height; y++)
        {
            bool full = true;
            for (int x = 0; x < width; x++)
                full &= filled[x, y];

            if (full)
            {
                cleared++;
                // 上を落とす
                for (int yy = y; yy < height - 1; yy++)
                    for (int x = 0; x < width; x++)
                        filled[x, yy] = filled[x, yy + 1];
                // 最上段クリア
                for (int x = 0; x < width; x++)
                    filled[x, height - 1] = false;

                y--; // 同じ行を再チェック
            }
        }

        if (cleared > 0)
        {
            score += cleared * 100;
            UpdateUI();
        }

        Spawn();
    }

    void UpdateUI()
    {
        if (scoreText != null) scoreText.text = $"SCORE: {score}";
    }

    void OnDrawGizmos()
    {
        // 盤面描画（Sceneビューで見える）
        if (filled == null) return;

        // filled
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (!filled[x, y]) continue;
                Vector3 p = CellToWorld(x, y);
                Gizmos.DrawCube(p, Vector3.one * cellSize);
            }
        }

        // falling block
        Vector3 bp = CellToWorld(bx, by);
        Gizmos.DrawWireCube(bp, Vector3.one * cellSize);
    }

    Vector3 CellToWorld(int x, int y)
    {
        return new Vector3(drawOrigin.x + x * cellSize, drawOrigin.y + y * cellSize, 0);
    }
}
