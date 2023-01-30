using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedGamesUI : MonoBehaviour
{
    public string name;
    public int age;
    public Text logTxt;
    public Text outPutTxt;
    public InputField nameInputField;
    public InputField ageInputField;

    public void OnValueChangeName(string field)
    {
        name = nameInputField.text;
    }

    public void OnValueChangeAge(string field)
    {
        age = int.Parse(nameInputField.text);
    }

}
