using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDialogue : MonoBehaviour
{
    [SerializeField] private GameObject instructionPanel;
    public TextMeshProUGUI dialogueText;
    public string[] lines = new string[]
    {
        "Chào mừng bạn đến với trò chơi!",
        "Dùng phím mũi tên hoặc AD để di chuyển.",
        "Sử dụng phím Space để nhảy",
        "Tránh các chướng ngại vật và quái để sống sót.",
        "Ngoài ra hãy chạm vào key như hình bên dưới để chiến thắng",
        "Chúc bạn may mắn!"
    };
    private int index = 0;

    
    private bool isShowing = false;

    void Start()
    {
        instructionPanel.SetActive(false);
        dialogueText.text = "";
    }

    void Update()
    {
        if (isShowing && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            ShowNextLine();
        }
    }

    public void ShowInstruction()
    {
        index = 0;
        instructionPanel.SetActive(true);
        isShowing = true;
        ShowNextLine();
        Time.timeScale = 0;
    }

    void ShowNextLine()
    {
        if (index < lines.Length)
        {
            dialogueText.text = lines[index];
            index++;
        }
        else
        {
            dialogueText.text = "";
            instructionPanel.SetActive(false);
            isShowing = false;
            Time.timeScale = 1;
            Destroy(this.gameObject);
        }
    }
}
