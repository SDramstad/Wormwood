using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class UIManager : MonoBehaviour {

    private GameObject ConversationPanel;

    private Image ConversationImage_CharacterFace;

    private TextMeshProUGUI ConversationText;

    private Text CharacterName;

    private GameObject AlertPanel;

    private TextMeshProUGUI AlertText;

    private bool MouseControl;

    private GameObject player;



    // Use this for initialization
    void Start () {
        ConversationPanel = GameObject.Find("ConversationPanel");
        ConversationImage_CharacterFace = GameObject.Find("ConversationImage1").GetComponent<Image>();
        ConversationText = GameObject.Find("ConversationText").GetComponent<TextMeshProUGUI>();
        CharacterName = GameObject.Find("CharacterName").GetComponent<Text>();
        player = GameObject.Find("Player");

        AlertPanel = GameObject.Find("AlertPanel");
        AlertText = GameObject.Find("AlertText").GetComponent<TextMeshProUGUI>();
        MouseControl = true;
        ConversationPanel.SetActive(false);
        AlertPanel.SetActive(false);
    }

    public void SetPickUpText(string _pickupText)
    {
        AlertPanel.SetActive(true);
        AlertText.text = _pickupText;
        StopCoroutine("HidePickup");
        StartCoroutine("HidePickup");
    }

    IEnumerator HidePickup()
    {
        yield return new WaitForSeconds(5f);
        AlertText.text = "";
        AlertPanel.SetActive(false);
    }

    public void SetConversationText(string talker, string text, Sprite face)
    {
        ConversationPanel.SetActive(true);
        ConversationImage_CharacterFace.overrideSprite = face;
        CharacterName.text = talker;
        ConversationText.text = text;
        StopCoroutine("HideConversation");
        StartCoroutine("HideConversation");
    }

    IEnumerator HideConversation()
    {
        yield return new WaitForSeconds(5f);
        ConversationPanel.SetActive(false);
    }

    public void HideMouse()
    {
        player.GetComponent<RigidbodyFirstPersonController>().mouseLook.SetCursorLock(true);
    }
    public void ShowMouse()
    {
        player.GetComponent<RigidbodyFirstPersonController>().mouseLook.SetCursorLock(false);
    }

    public void ToggleFPSController()
    {
        MouseControl = !MouseControl;

        if (!MouseControl)
        {
            Cursor.visible = true;
        }
        else
        {
            player.GetComponent<RigidbodyFirstPersonController>().mouseLook.SetCursorLock(true);
            Cursor.visible = false;
        }

    }
}
