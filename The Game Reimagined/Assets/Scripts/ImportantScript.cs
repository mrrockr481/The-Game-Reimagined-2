using UnityEngine;
using TMPro; // <-- needed for TMP_InputField

public class ImportantScript : MonoBehaviour
{
    public TMP_InputField inputField; // drag your TMP_InputField here
    public GameObject door1;
    public GameObject door1anim;
    public GameObject door2;
    public GameObject door2anim;
    public GameObject door3;
    public GameObject door3anim;
    public bool puzzleOne = false;
    public bool puzzleTwo = false;

    void Update()
    {
        foreach (char c in Input.inputString) // Unity captures every key pressed this frame
        {
            if (char.IsDigit(c)) // only allow numbers
            {
                inputField.text += c; // add it to the TMP_InputField text
            }
            else if (c == '\b' && inputField.text.Length > 0) // handle backspace
            {
                inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
            }
        }

        // optional: handle Enter key
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Input submitted: " + inputField.text);
            if (inputField.text == "384")
            {
                door1.SetActive(false);
                door1anim.SetActive(true);
                puzzleOne = true;
            }
            if (inputField.text == "33")
            {
                if (puzzleOne)
                {
                    door2.SetActive(false);
                    door2anim.SetActive(true);
                    puzzleTwo = true;
                }
            }
            if (inputField.text == "23")
            {
                if (puzzleTwo)
                {
                    door3.SetActive(false);
                    door3anim.SetActive(true);
                }
            }
            inputField.text = ""; // clear after submit
        }
    }
}