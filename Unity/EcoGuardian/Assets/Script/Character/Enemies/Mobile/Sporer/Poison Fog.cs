using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonFog : MonoBehaviour
{
    private Player player;

    public void Initialize(Player player){
        this.player = player;
    }
    private void OnTriggerEnter(Collider other) {
        // Check if the object that entered the trigger is a player
        if (other.gameObject.CompareTag("Player")){
            player.PoisonIndefinitely();
        }
    }
    private void OnTriggerExit(Collider other) {
        // Check if the object that exited the trigger is a player
        if (other.gameObject.CompareTag("Player")){
            player.ExitPoisonArea();
        }
    }
    private void OnDestroy() {
        // Destroy the spore when it is destroyed
        if(player!=null){
            player.ExitPoisonArea();
        }
        
    }
}
