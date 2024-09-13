using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class puzzle : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private List<Texture2D> imageTextures;
    [SerializeField] private Transform levelSelectPanel;
    [SerializeField] private Image levelSelectPrefab;

    void Start()
    {
        // Create the UI
        foreach (Texture2D texture in imageTextures)
        {
            Image image = Instantiate(levelSelectPrefab, levelSelectPanel);
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            // Assign button action
            image.GetComponent<Button>().onClick.AddListener(delegate { SelectImage(texture); });
        }
    }

    public void SelectImage(Texture2D selectedTexture)
    {
        // Store the selected texture in a static variable
        PuzzleManager.SelectedTexture = selectedTexture;
        // Load the puzzle scene
        SceneManager.LoadScene("PuzzleManager");
    }
}