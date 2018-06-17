using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueTree {

    /*
     * DIALOGUE TREE SYSTEM
     * 
     * Dialogues must have an exit node, which is called when choosing to leave a conversation.
     * ID for that should be "exit".
     * 
     * Dialogues require a starter node. 
     * ID for that should be "starter".
     * 
     * 
    */
    public Dialogue[] dialogues;

}
