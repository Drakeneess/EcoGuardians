using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounterUI : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh=GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text=$"{EnemyGameController.EnemiesDefeated()} / {EnemyGameController.normalEnemies.Count}";
        if(EnemyGameController.EnemiesDefeated()>=EnemyGameController.normalEnemies.Count/4){
            textMesh.color=Color.green;
        }
        else{
            textMesh.color=Color.white;
        }   
    }

}
