using UnityEngine;
using TMPro;

public class ReadableNote : MonoBehaviour
{
    [Header("UI Ayarlari")]
    [SerializeField] private GameObject noteUIPanel;
    [SerializeField] private TextMeshProUGUI noteTextDisplay;

    [Header("Icerik Ayari")]
    [TextArea(3, 10)]
    [SerializeField] private string noteContent;

    private bool isPlayerNear;
    private bool isNoteOpen;

    void Start()
    {
        if (noteUIPanel != null)
        {
            noteUIPanel.SetActive(false);
        }
    }

    void Update()
    {
        bool interactInput = Input.GetKeyDown(KeyCode.E) ||
                             Input.GetKeyDown(KeyCode.Return) ||
                             Input.GetKeyDown(KeyCode.KeypadEnter);

        if (isPlayerNear && interactInput)
        {
            if (isNoteOpen)
            {
                CloseNote();
            }
            else
            {
                OpenNote();
            }
        }
    }

    private void OpenNote()
    {
        isNoteOpen = true;
        noteTextDisplay.text = noteContent;
        noteUIPanel.SetActive(true);
    }

    public void CloseNote()
    {
        isNoteOpen = false;
        noteUIPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = false;
            CloseNote();
        }
    }
}