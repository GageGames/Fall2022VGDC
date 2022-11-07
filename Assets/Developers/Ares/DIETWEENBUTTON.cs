using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class DIETWEENBUTTON : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		//GetComponent<Image>().DOFade(0, 3).SetLoops(-1, LoopType.Yoyo);
		//transform.GetChild(0).GetComponent<TextMeshProUGUI>().DOFade(0, 3).SetLoops(-1, LoopType.Yoyo);
		//similar to transform
		//GetComponent<RectTransform>().DOAnchorPos(new Vector3(-200,-200,0), 3).SetEase(Ease.OutBounce);
		//GetComponent<Image>().DOColor(Color.red, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void RedFlashWhenClicked()
	{
		GetComponent<Image>().DOKill();
		GetComponent<Image>().color = Color.white;
		GetComponent<Image>().DOColor(Color.red, 0.5f).SetLoops(2, LoopType.Yoyo);

	}
}
