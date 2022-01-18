using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideShow : MonoBehaviour
{
    [SerializeField] List<Sprite> slides;
    [SerializeField] Image currentSlide;
    public int  test;
    private void Start()
    {
        ShowSlides();
    }

    public void ShowSlides()
    {

        StartCoroutine(SlideChange());
        IEnumerator SlideChange()
        {
            for (int i = 0; i < slides.Count; i++)
            {
                test = i;
                currentSlide.sprite = slides[i];
                yield return new WaitForSeconds(5f);
                if (i >= slides.Count - 1) { i = 0; }
            }
        }



    }

}
