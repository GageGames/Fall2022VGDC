using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class uiscreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // GetComponent<Image>().DOFade(0, 3);
        //transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOFade(0, 3);
        //GetComponent<RectTransform>().DOAnchorPos(new Vector3(-200,-200,0), 3).SetEase(Ease.OutBack);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void redflashwhenclicked()
	{
        GetComponent<Image>().DOKill();
        GetComponent<Image>().color = Color.white;
		GetComponent<Image>().DOColor(Color.red, 0.5f).SetLoops(2, LoopType.Yoyo);

	}
}
