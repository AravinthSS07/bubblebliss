using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class PuzzleManager : MonoBehaviour
{
    public static Texture2D SelectedTexture;

    [Header("Game Elements")]
    [SerializeField] private Transform gameHolder;
    [SerializeField] private Transform piecePrefab;

    [Header("UI Elements")]
    [SerializeField] private GameObject playAgainButton;

    private List<Transform> pieces;
    private Vector2Int dimensions = new Vector2Int(3, 2); // Fixed 3x2 grid for 6 pieces
    private float width;
    private float height;

    private Transform draggingPiece = null;
    private Vector3 offset;

    private int piecesCorrect;

    void Start()
    {
        if (SelectedTexture == null)
        {
            Debug.LogError("No texture selected!");
            return;
        }

        // We store a list of the transform for each jigsaw piece so we can track them later.
        pieces = new List<Transform>();

        // Create the pieces of the correct size with the correct texture.
        CreateJigsawPieces(SelectedTexture);

        // Place the pieces randomly into the visible area.
        Scatter();

        // Update the border to fit the chosen puzzle.
        UpdateBorder();

        // As we're starting the puzzle there will be no correct pieces.
        piecesCorrect = 0;
    }

    // Create all the jigsaw pieces
    void CreateJigsawPieces(Texture2D jigsawTexture)
    {
        // Calculate piece sizes based on the dimensions.
        height = 1f / dimensions.y;
        float aspect = (float)jigsawTexture.width / jigsawTexture.height;
        width = aspect / dimensions.x;

        for (int row = 0; row < dimensions.y; row++)
        {
            for (int col = 0; col < dimensions.x; col++)
            {
                // Create the piece in the right location of the right size.
                Transform piece = Instantiate(piecePrefab, gameHolder);
                piece.localPosition = new Vector3(
                    (-width * dimensions.x / 2) + (width * col) + (width / 2),
                    (-height * dimensions.y / 2) + (height * row) + (height / 2),
                    -1);
                piece.localScale = new Vector3(width, height, 1f);

                // We don't have to name them, but always useful for debugging.
                piece.name = $"Piece {(row * dimensions.x) + col}";
                pieces.Add(piece);

                // Assign the correct part of the texture for this jigsaw piece
                float width1 = 1f / dimensions.x;
                float height1 = 1f / dimensions.y;
                Vector2[] uv = new Vector2[4];
                uv[0] = new Vector2(width1 * col, height1 * row);
                uv[1] = new Vector2(width1 * (col + 1), height1 * row);
                uv[2] = new Vector2(width1 * col, height1 * (row + 1));
                uv[3] = new Vector2(width1 * (col + 1), height1 * (row + 1));
                Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                mesh.uv = uv;
                piece.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", jigsawTexture);
            }
        }
    }

    // Place the pieces randomly in the visible area.
    private void Scatter()
    {
        float orthoHeight = Camera.main.orthographicSize;
        float screenAspect = (float)Screen.width / Screen.height;
        float orthoWidth = screenAspect * orthoHeight;

        float pieceWidth = width * gameHolder.localScale.x;
        float pieceHeight = height * gameHolder.localScale.y;

        // Calculate the bounds of the LineRenderer
        float halfWidth = (width * dimensions.x) / 2f;
        float halfHeight = (height * dimensions.y) / 2f;

        // Define the area above and below the LineRenderer where pieces can be placed
        float topY = halfHeight + pieceHeight * 1.5f; // Adjusted to ensure no overlap with LineRenderer
        float bottomY = -halfHeight - pieceHeight * 1.5f; // Adjusted to ensure no overlap with LineRenderer

        // Calculate the horizontal spacing between pieces
        float horizontalSpacing = pieceWidth * 0.2f; // Adjust spacing as needed

        // Calculate the total width of the pieces and the starting X position for centering
        float totalWidth = 3 * pieceWidth + 2 * horizontalSpacing;
        float startX = -totalWidth / 2 + pieceWidth / 2;

        // Ensure the pieces fit within the screen width
        if (totalWidth > orthoWidth * 2)
        {
            Debug.LogError("Pieces do not fit within the screen width. Adjust the piece size or spacing.");
            return;
        }

        // Get the pieces array and shuffle it
        List<Transform> shuffledPieces = pieces.OrderBy(piece => Random.value).ToList();

        // Place 3 pieces above the LineRenderer
        for (int i = 0; i < 3; i++)
        {
            float x = startX + i * (pieceWidth + horizontalSpacing);
            shuffledPieces[i].position = new Vector3(x, topY, -1);
        }

        // Place 3 pieces below the LineRenderer
        for (int i = 3; i < 6; i++)
        {
            float x = startX + (i - 3) * (pieceWidth + horizontalSpacing);
            shuffledPieces[i].position = new Vector3(x, bottomY, -1);
        }
    }

    // Update the border to fit the chosen puzzle.
    private void UpdateBorder()
    {
        LineRenderer lineRenderer = gameHolder.GetComponent<LineRenderer>();

        float halfWidth = (width * dimensions.x) / 2f;
        float halfHeight = (height * dimensions.y) / 2f;
        float borderZ = 0f;

        lineRenderer.positionCount = 5;
        lineRenderer.SetPosition(0, new Vector3(-halfWidth, halfHeight, borderZ));
        lineRenderer.SetPosition(1, new Vector3(halfWidth, halfHeight, borderZ));
        lineRenderer.SetPosition(2, new Vector3(halfWidth, -halfHeight, borderZ));
        lineRenderer.SetPosition(3, new Vector3(-halfWidth, -halfHeight, borderZ));
        lineRenderer.SetPosition(4, new Vector3(-halfWidth, halfHeight, borderZ));

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.loop = true;
        lineRenderer.enabled = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                draggingPiece = hit.transform;
                offset = draggingPiece.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                offset += Vector3.back;
            }
        }

        if (draggingPiece && Input.GetMouseButtonUp(0))
        {
            SnapAndDisableIfCorrect();
            draggingPiece.position += Vector3.forward;
            draggingPiece = null;
        }

        if (draggingPiece)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition += offset;
            draggingPiece.position = newPosition;
        }
    }

    private void SnapAndDisableIfCorrect()
    {
        int pieceIndex = pieces.IndexOf(draggingPiece);
        int col = pieceIndex % dimensions.x;
        int row = pieceIndex / dimensions.x;

        Vector2 targetPosition = new((-width * dimensions.x / 2) + (width * col) + (width / 2),
                                     (-height * dimensions.y / 2) + (height * row) + (height / 2));

        if (Vector2.Distance(draggingPiece.localPosition, targetPosition) < (width / 2))
        {
            draggingPiece.localPosition = targetPosition;
            draggingPiece.GetComponent<BoxCollider2D>().enabled = false;
            piecesCorrect++;
            if (piecesCorrect == pieces.Count)
            {
                playAgainButton.SetActive(true);
                SceneManager.LoadScene("Winpage"); // Load the win scene
            }
        }
    }

    public void RestartGame()
    {
        foreach (Transform piece in pieces)
        {
            Destroy(piece.gameObject);
        }
        pieces.Clear();
        gameHolder.GetComponent<LineRenderer>().enabled = false;
        playAgainButton.SetActive(false);
        SceneManager.LoadScene("SelectionScene"); // Load the selection scene
    }
}