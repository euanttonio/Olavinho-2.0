using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GameController : MonoBehaviour {

    public GameObject gameOverPanel;
    public GameObject pontosPanel;
    public Text txtMaiorPontuacao;

    private int pontos;
    public Text txtPontos;

    public GameObject menuCamera;
    public GameObject menuPanel;

    public Estado estado;

    public float espera;
    public GameObject obstaculo;
    public float tempoDestruicao;
    
    public static GameController instancia = null;

    public void incrementarPontos(int x) {
        atualizarPontos(pontos + x);
    }

    void Awake () {
        if(instancia == null){
            instancia = this;
        }
        else if (instancia != null){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}

    void Start(){
        estado = Estado.AguardoComecar;
        PlayerPrefs.SetInt("HighScore", 0);
        menuCamera.SetActive(true);
        menuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        pontosPanel.SetActive(false);
    }

    IEnumerator GerarObstaculos(){
        while (GameController.instancia.estado == Estado.Jogando){
            Vector3 pos = new Vector3(17f, Random.Range(-3f, 9f), 7.8f);
            GameObject obj = Instantiate(obstaculo, pos, Quaternion.Euler(0f,180f,0)) as GameObject;
            Destroy(obj, tempoDestruicao);
            yield return new WaitForSeconds(espera);
        }
        yield return null;
    }

    public void PlayerComecou(){
        estado = Estado.Jogando;
        menuCamera.SetActive(false);
        menuPanel.SetActive(false);
        pontosPanel.SetActive(true);
        atualizarPontos(0);
        StartCoroutine(GerarObstaculos());
    }

    public void PlayerMorreu(){
        estado = Estado.GameOver;
        if (pontos > PlayerPrefs.GetInt("HighScore")) {
            PlayerPrefs.SetInt("HighScore", pontos);
            txtMaiorPontuacao.text = "" + pontos;
        }
        gameOverPanel.SetActive(true);
    }

    private void atualizarPontos(int x) {
        pontos = x;
        txtPontos.text = "" + x;
    }

    public void acrescentarPontos(int x) {
        atualizarPontos(pontos + x);
    }

    public void PlayerVoltou() {
        estado = Estado.AguardoComecar;
        menuCamera.SetActive(true);
        menuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        pontosPanel.SetActive(false);
        GameObject.Find("meninoassutado").GetComponent<PlayerController>().recomecar();
    }


}
