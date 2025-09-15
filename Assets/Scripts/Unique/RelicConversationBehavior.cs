using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// class that references the specified conversation upon meeting the conditions
public class RelicConversationBehavior : MonoBehaviour
{

    public CollisionBehavior CollisionBehavior;
    public NPCConversation FirstConversation;
    public NPCConversation SecondConversation;
    void Update()
    {
        // press E to talk to NPC, 2nd conversation unlocks only if you answer "No" in the first conversation
        if (ConversationManager.Instance != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && CollisionBehavior.isInDarkTrigger)
            {
                if (!DataStorage.HasHadFirstConversation)
                {
                    ConversationManager.Instance.StartConversation(FirstConversation);
                    DataStorage.HasHadFirstConversation = true;
                }
                else
                {
                    if (!ConversationManager.Instance.IsConversationActive)
                        ConversationManager.Instance.StartConversation(SecondConversation);
                }

            }

        }

    }
}
