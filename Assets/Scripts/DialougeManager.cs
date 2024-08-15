using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialougeManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialougeText;
    public Image dialougeImage;
    public Animator animator;

    public GameObject nextButton;

    private Queue<string> sentences;
    private Queue<Sprite> images;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        images = new Queue<Sprite>();
        nextButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialouge(Dialouge dialouge)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialouge.name;

        sentences.Clear();
        images.Clear();

        foreach (string sentence in dialouge.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (Sprite image in dialouge.images)
        {
            images.Enqueue(image);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialouge();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));

        if(images.Count > 0)
        {
            Sprite image = images.Dequeue();
            dialougeImage.sprite = image;
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialougeText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialougeText.text += letter;
            yield return null;
        }
    }

    public void EndDialouge()
    {
        Debug.Log("End of dialouge");
        animator.SetBool("IsOpen", false);
        nextButton.SetActive(true);
    }
}
