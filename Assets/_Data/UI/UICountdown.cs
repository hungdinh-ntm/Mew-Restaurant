using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICountdown : LoadMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI countdownText;
    [SerializeField] protected int countdownTime = 5;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(CountdownRoutine());
    }

    protected override void LoadComponent()
    {
        this.LoadCountdownText();
    }

    protected virtual void LoadCountdownText()
    {
        if (this.countdownText != null) return;
        this.countdownText = transform.Find("TextCountdown").GetComponent<TextMeshProUGUI>();
    }

    protected virtual IEnumerator CountdownRoutine()
    {
        for (int i = this.countdownTime; i > 0; i--)
        {
            countdownText.SetText(i.ToString());

            yield return new WaitForSeconds(1f);
        }

        countdownText.SetText("GO");
        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);

        GameManager.Instance.StartGame();
    }
}
