using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GosmaMove : MonoBehaviour{

    public float velocidadeHorizontal;
    public float velocidadeVertical;
    public float min;
    public float max;
    public float espera;

    private GameObject player;
    private bool pontuou;

    void Start(){ 
        StartCoroutine(Move(max));
        player = GameObject.Find("Player");
        pontuou = false;
    }

    void Update(){
        Vector3 velocidadeVetorial = Vector3.left * velocidadeHorizontal;
        transform.position = transform.position + velocidadeVetorial * Time.deltaTime;
        if (GameController.instancia.estado == Estado.Jogando) {
            if (!pontuou && transform.position.x < player.gameObject.transform.position.x) {
                pontuou = true;
                GameController.instancia.acrescentarPontos(1);
            }
        }
        
    }

    IEnumerator Move(float destino){

        while (Mathf.Abs(destino - transform.position.y) > 0.5f){
            Vector3 direcao = (destino == max) ? Vector3.up : Vector3.down;
            Vector3 velocidadeVetorial = direcao * velocidadeVertical;
            transform.position = transform.position + velocidadeVetorial * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(espera);

        destino = (destino == max) ? min : max;

        StartCoroutine(Move(destino));
    }
}