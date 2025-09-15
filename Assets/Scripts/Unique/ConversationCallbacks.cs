using UnityEngine;
using DialogueEditor;
using Platformer.Model;
using Platformer.Core;

// this class handles the parameters in conversations upon start and end
public class ConversationCallbacks:MonoBehaviour
 {
    readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();
    public CollisionBehavior CollisionBehavior;

    private void OnEnable()
    {
        ConversationManager.OnConversationStarted += ConversationStart;
        ConversationManager.OnConversationEnded += ConversationEnd;
    }
       
    private void OnDisable()
    {
        ConversationManager.OnConversationStarted -= ConversationStart;
        ConversationManager.OnConversationEnded -= ConversationEnd;
    }

    private void ConversationStart()
    {
        model.player.controlEnabled = false;
        
    }
        
    private void ConversationEnd()
    {
        model.player.controlEnabled = true;

        if (ConversationManager.Instance.GetBool("choice") == true)
        {
            DataStorage.DarkPower = true;
            DataStorage.DarkFollow = true;
            Object.Destroy(CollisionBehavior.SpeechBubble);
            CollisionBehavior.DarkPowerText.enabled = true;
            CollisionBehavior.isInDarkTrigger = false;
        }
      
    }
}
