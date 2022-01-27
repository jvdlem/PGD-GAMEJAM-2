using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideShow : MonoBehaviour
{
    [SerializeField] List<Sprite> slides;
    [SerializeField] Image currentSlide;
    [SerializeField] float timeBetweenFrames;
    private void Start()
    {
        ShowSlides();
    }

    public void ShowSlides()
    {
        //cool recursive method to show slides always starting over after the last slide
        StartCoroutine(SlideChange());
        IEnumerator SlideChange()
        {
            for (int i = 0; i < slides.Count; i++)
            {
                
                currentSlide.sprite = slides[i];
                yield return new WaitForSeconds(timeBetweenFrames);
                if (i >= slides.Count - 1) { i = -1; }
            }
        }



    }

}
