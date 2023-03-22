using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public static Dialogue instance;

    public TextMeshProUGUI dialogueText;
    public GameObject commandPrompt, box;

    public string[] dialogueLines, shootLines;
    private float textSpeed;
    private int lineIndex = 0;
    private int shootIndex = 0;
    private int resetCounter = 0;

    private bool isTyping = false;

    private void Start()
    {
        instance = this;

        textSpeed = 1f / 25;

        StartLine();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && commandPrompt.activeInHierarchy)
        {
            box.GetComponent<Image>().enabled = true;
            box.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
            commandPrompt.SetActive(false);

            if(!isTyping)
            NextLine();
        }
    }
    private void StartLine()
    {
        lineIndex = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.text = string.Empty;

        Invoke("SkipInvoke", 1f);

        foreach (char character in dialogueLines[lineIndex])
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine()
    {
        StopAllCoroutines();

        if(lineIndex < dialogueLines.Length - 1)
        {
            dialogueText.text = string.Empty;
            lineIndex += 1;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogueText.text = string.Empty;
            lineIndex = dialogueLines.Length - 1;
            StartCoroutine(TypeLine());
            resetCounter++;

            if(resetCounter >= 5)
            {
                lineIndex = 0;
                resetCounter = 0;
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!isTyping)
            commandPrompt.SetActive(true);
        }
    }

    public void SkipInvoke()
    {
        isTyping = false;
    }

    IEnumerator ShootLineOff()
    {
        yield return new WaitForSeconds(2f);
        box.GetComponent<Image>().enabled = false;
        box.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if(commandPrompt.activeInHierarchy)
        commandPrompt.SetActive(false);

        box.GetComponent<Image>().enabled = false;
        box.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
    }

    IEnumerator ShootLine()
    {
        isTyping = true;
        dialogueText.text = string.Empty;
        box.GetComponent<Image>().enabled = true;
        box.GetComponentInChildren<TextMeshProUGUI>().enabled = true;

        foreach (char character in shootLines[Random.Range(0,shootLines.Length)])
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(textSpeed);
        }

        Invoke("SkipInvoke", 1f);

        StartCoroutine(ShootLineOff());
    }

    public void ShootLineCoRoutine()
    {
        StopAllCoroutines();
        StartCoroutine(ShootLine());
    }

}
