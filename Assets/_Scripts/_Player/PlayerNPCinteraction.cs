using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNPCinteraction : MonoBehaviour
{

   
    [SerializeField] private float _interact_range = 2f ;
   
   //variabile fine dialogo settata a false
   static public bool _end_dialogue = false;
    
   
    
    void Update()
    {



        //mi avvicino al NPC e premo E per interagire
        if(Input.GetKeyDown(KeyCode.E)){
                Collider[] coll_array = Physics.OverlapSphere(transform.position, _interact_range);
                foreach (Collider coll in coll_array)
                {
                    if (coll.TryGetComponent(out NpcInteractable npc))
                    {
                      if (_end_dialogue == false){

                       npc.Interact();

                       
                       }else{
                           // se variabile fine dialogo è a true 
                           npc.StopInteract();
                           //rimetto variabile fine dialogo a false
                          _end_dialogue = false;
                      }
                      
                    }
                }

        }
        
    }


}
