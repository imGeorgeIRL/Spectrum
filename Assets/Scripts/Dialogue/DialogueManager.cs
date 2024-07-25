using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using UnityEngine.SearchService;
using Febucci.UI.Core;
using Febucci.UI;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    private float typingSpeed = 0.02f;

    [Header("Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialogueBubble;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;

    [SerializeField] private TMP_FontAsset trashHand;
    [SerializeField] private TMP_FontAsset lineBeam;


    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;

    [Header("Audio")]
    [SerializeField] private AudioClip dialogueTypingSoundClip;
    [SerializeField] private bool stopAudioSource;
    [SerializeField] private bool makePredictable;
    private AudioSource audioSource;

    private TextMeshProUGUI[] choicesText;
    private int choiceID;
    //private SpeechBubble speechBubbleScript;

    private Story currentStory;

    public bool dialogueIsPlaying { get; private set; }

    private Coroutine displayLineCoroutine;

    private bool canContinueToNextLine = false;


    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string FONT_TAG = "font";
    private const string SIZE_TAG = "size";

    private DialogueVariables dialogueVariables;
    private InkExternalFunctions inkExternalFunctions;

    private void Start()
    {
        dialogueIsPlaying = false;
        dialogueBubble.SetActive(false);

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
        choices[choiceID].SetActive(true);
        audioSource = GetComponent<AudioSource>();
    }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one DialogueManager instance in the scene");
        }
        instance = this;
        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        inkExternalFunctions = new InkExternalFunctions();
    }


    public void PlayDialogueAudio()
    {
        if (stopAudioSource)
        {
            audioSource.Stop();
        }
            audioSource.PlayOneShot(dialogueTypingSoundClip);
    }


    public static DialogueManager GetInstance()
    {
        return instance;
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialogueBubble.SetActive(true);

        dialogueVariables.StartListening(currentStory);
        inkExternalFunctions.Bind(currentStory);

        ContinueStory();
    }

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }

        if (canContinueToNextLine 
            && currentStory.currentChoices.Count == 0 
            && Input.GetButtonDown("Submit"))
        {
            ContinueStory();
        }
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
            //ExitDialogueMode();
        //}
    }
    
    private void ExitDialogueMode()
    {
        GameManager.isTalking = false;
        dialogueIsPlaying = false;
        dialogueBubble.SetActive(false);
        dialogueText.text = "";

        dialogueVariables.StopListening(currentStory);
        inkExternalFunctions.Unbind(currentStory);
        

    }
    
    private void ContinueStory()
    {
        
        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            string nextLine = currentStory.Continue();
            if (nextLine.Equals("") && !currentStory.canContinue)
            {
                ExitDialogueMode();
            }
            else
            {
                DisplayChoices();
                HandleTags(currentStory.currentTags);
                displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
            }
        }
        else
        {
            ExitDialogueMode();
            GameManager.isTalking = false;
        }
    }


    private IEnumerator DisplayLine(string line)
    {
        canContinueToNextLine = false;
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;

        bool isAddingRichTextTag = false;
        foreach (char letter in line.ToCharArray())
        {
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            else
            {
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }

        }
        
        canContinueToNextLine = true;
    }



    private void HandleTags(List<string> currentTags)
    {
        foreach(string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case FONT_TAG:
                    if (splitTag[1] == "mum")
                    {
                        dialogueText.font = lineBeam;
                    }
                    else if (splitTag[1] == "other")
                    {
                        dialogueText.font = trashHand;
                    }
                    break;
                case SIZE_TAG:
                    if (splitTag[1] == "small")
                    {
                        dialogueText.fontSize = 10;
                    }
                    else if (splitTag[1] == "normal")
                    {
                        dialogueText.fontSize = 20;
                    }
                    else if (splitTag[1] == "big")
                    {
                        dialogueText.fontSize = 25;
                    }
                    break;

                default:
                    Debug.LogWarning("Tag came in but is not being properly handled: " + tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices Given: " + currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            

            //adding listener for mouse click
            Button choiceButton = choices[index].GetComponent<Button>();
            if (choiceButton != null)
            {
                Debug.Log("Adding listener to choice button: " + index);
                choiceButton.onClick.RemoveAllListeners();
                int choiceIndex = index;
                choiceButton.onClick.AddListener(() =>
                {
                    Debug.Log("Choice " + choiceIndex + " clicked.");
                    MakeChoice(choiceIndex);
                });
            }
            else
            {
                Debug.LogError("No Button component found on choice: " + index);
            }
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
        //StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            Debug.Log("Making choice: " + choiceIndex);
            currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();
        }
        else
        {
            Debug.LogWarning("Can't make choice yet. CanContinueToNextLine: " + canContinueToNextLine);
        }
    }

    public void OnApplicationQuit()
    {
        if (dialogueVariables != null)
        {
            dialogueVariables.SaveVariables();
        }
    }


    public void OnDestroy()
    {
        if (dialogueVariables != null)
        {
            dialogueVariables.SaveVariables();
        }
    }
}
