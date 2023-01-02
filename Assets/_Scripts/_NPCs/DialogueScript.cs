using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;

    private int _index;

    [SerializeField] private GameObject _press_to_End_text;
    private GameObject _clone_press_to_End_text;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
        
    }

    // Update is called once per frame
    void Update()
    
    {
        //se premo invio, se c'è altro, mostra nuovo testo : se è finito chiude il dialogBox
        if (Input.GetKeyDown(KeyCode.Return)){
            if (textComponent.text == lines[_index]){
                NextLine();
            }
            else{
                StopAllCoroutines();
                textComponent.text = lines[_index];   
            }
        }
    }

    void StartDialogue(){
        _index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){
        foreach(char letter in lines[_index].ToCharArray()){ //prende stringa e la divide into char array
            textComponent.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }


    void NextLine (){
        if (_index < lines.Length - 1){
            _index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else{
            
            
            //mostro a schermo per 2 secondi la scritta dove si dice di premere E per chiudere il dialogo
            _clone_press_to_End_text = (GameObject)GameObject.Instantiate(_press_to_End_text, transform.position, Quaternion.identity);
            Destroy(_clone_press_to_End_text, 1f);
            
            gameObject.SetActive(false);
            PlayerNPCinteraction._end_dialogue = true;

        }
    }

}
