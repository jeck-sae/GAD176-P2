using UnityEngine;

public class ButtonNumber : MonoBehaviour
{
    public int  buttonNumber;
    public void PresButton()
    {
        DialogueManager.Instance.ChooseOption(buttonNumber);
    }
}
