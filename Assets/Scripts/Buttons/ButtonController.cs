using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public static ButtonController Instance { get; private set; }

    private Dictionary<string, GameObject> btns = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Pega todos os botões filhos automaticamente
        btns.Clear();
        foreach (Transform child in transform)
        {
            btns[child.gameObject.name] = child.gameObject;
        }

        DeactivateBtn();
    }

    public void DeactivateBtn()
    {
        foreach (var botao in btns.Values)
        {
            botao.SetActive(false);
        }
    }

    public void ActivateBtn(string nameBtn, object parameter = null)
    {
        DeactivateBtn();

        if (btns.TryGetValue(nameBtn, out GameObject botaoGO))
        {
            botaoGO.SetActive(true);

            var receiver = botaoGO.GetComponent<IButtonParameter>();
            if (receiver != null)
            {
                receiver.ReceiveParameter(parameter);
            }
        }
        else
        {
            Debug.LogWarning("Botão não encontrado: " + nameBtn);
        }
    }
}
