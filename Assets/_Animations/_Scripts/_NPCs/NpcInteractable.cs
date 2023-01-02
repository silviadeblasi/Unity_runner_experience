using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcInteractable : MonoBehaviour
{

    private Animator _animator;
     private GameObject dialogueBoxClone ;

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private GameObject _dialogue_ended;

    private GameObject _clone_dialogue_ended;


    void Start(){
        _animator = GetComponent<Animator>();
    }
    


    public void Interact(){
        Debug.Log("interazione con NPC");
        _animator.SetBool("talk", true);
        
        dialogueBoxClone = (GameObject)GameObject.Instantiate(dialogueBox, transform.position, Quaternion.identity);
        Debug.Log("istanziato dialogBox");

    }


    public void StopInteract(){
        Debug.Log("stop interazione con NPC");
        _animator.SetBool("talk", false);
        
        //chiudo dialogue Box
        Destroy(dialogueBoxClone);
        Debug.Log("distrutto dialogBox");

        //confermo chiusura dialogo 
         //mostro a schermo per 2 secondi la scritta dove si dice di premere E per chiudere il dialogo
        _clone_dialogue_ended = (GameObject)GameObject.Instantiate(_dialogue_ended, transform.position, Quaternion.identity);
        Destroy(_clone_dialogue_ended, 2f);

        
    }
    

    
}
