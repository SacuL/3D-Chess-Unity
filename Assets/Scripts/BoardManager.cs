using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> chessmanPrefabs;
    private List<GameObject> activeChessman;

    private Quaternion whiteOrientation = Quaternion.Euler(0, 270, 0);
    private Quaternion blackOrientation = Quaternion.Euler(0, 90, 0);

    // Use this for initialization
    void Start()
    {
        SpawnAllChessmans();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSelection();
        DrawChessBoard();
    }

    private void DrawChessBoard()
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heigthLine = Vector3.forward * 8;

        for (int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);

            start = Vector3.right * i;
            Debug.DrawLine(start, start + heigthLine);
        }

        // Draw selection
        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

            Debug.DrawLine(
                Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
                Vector3.forward * selectionY + Vector3.right * (selectionX + 1));
        }
    }

    private void UpdateSelection()
    {
        if (!Camera.main) return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f, LayerMask.GetMask("ChessPlane")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }

    private void SpawnChessman(int index, Vector3 position, bool isWhite)
    {
        GameObject go;

        if (isWhite)
        {
            go = Instantiate(chessmanPrefabs[index], position, whiteOrientation) as GameObject;
        }
        else
        {
            go = Instantiate(chessmanPrefabs[index], position, blackOrientation) as GameObject;
        }

        go.transform.SetParent(transform);
        activeChessman.Add(go);
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;

        return origin;
    }

    private void SpawnAllChessmans()
    {
        activeChessman = new List<GameObject>();

        /////// White ///////

        // King
        SpawnChessman(0, GetTileCenter(3, 0), true);

        // Queen
        SpawnChessman(1, GetTileCenter(4, 0), true);

        // Rooks
        SpawnChessman(2, GetTileCenter(0, 0), true);
        SpawnChessman(2, GetTileCenter(7, 0), true);

        // Bishops
        SpawnChessman(3, GetTileCenter(2, 0), true);
        SpawnChessman(3, GetTileCenter(5, 0), true);

        // Knights
        SpawnChessman(4, GetTileCenter(1, 0), true);
        SpawnChessman(4, GetTileCenter(6, 0), true);

        // Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(5, GetTileCenter(i, 1), true);
        }


        /////// Black ///////

        // King
        SpawnChessman(6, GetTileCenter(4, 7), false);

        // Queen
        SpawnChessman(7, GetTileCenter(3, 7), false);

        // Rooks
        SpawnChessman(8, GetTileCenter(0, 7), false);
        SpawnChessman(8, GetTileCenter(7, 7), false);

        // Bishops
        SpawnChessman(9, GetTileCenter(2, 7), false);
        SpawnChessman(9, GetTileCenter(5, 7), false);

        // Knights
        SpawnChessman(10, GetTileCenter(1, 7), false);
        SpawnChessman(10, GetTileCenter(6, 7), false);

        // Pawns
        for (int i = 0; i < 8; i++)
        {
            SpawnChessman(11, GetTileCenter(i, 6), false);
        }
    }
}


