using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiResourceParticle : MonoBehaviour
{
    [SerializeField] private Image m_image;
    [SerializeField] private CanvasGroup m_canvasGroup;

    [SerializeField] private float m_travelDuration = 0.6f;
    [SerializeField] private float m_arcHeight = 100f;
    [SerializeField] private float m_randomRadius = 80f;

    [SerializeField] private float m_minIconScale = 0.7f;
    [SerializeField] private float m_minIconAlpha = 0.6f;

    private RectTransform m_rectTransform;

    private void Awake()
    {
        m_rectTransform = GetComponent<RectTransform>();

    }

    /// <summary>
    /// Initialize the particle 
    /// </summary>
    /// <param name="resourceSprite"> sprite</param>
    /// <param name="startPos">current world pos</param>
    /// <param name="target">target rectransform</param>
    /// <param name="onComplete">action to call </param>
    public void Initialize(Sprite resourceSprite,Vector2 startPos,RectTransform target, Action onComplete = null)
    {
        m_image.sprite = resourceSprite;
        m_rectTransform.anchoredPosition = startPos;
        StartCoroutine(MoveRoutine(target,onComplete));
    }

    private IEnumerator MoveRoutine(RectTransform target,Action onComplete)
    {
        float elapse = 0;

        Vector2 starPosition = m_rectTransform.anchoredPosition;

        Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * m_randomRadius;

        RectTransform particleParent = m_rectTransform.parent as RectTransform;

        while(elapse < m_travelDuration)
        {
            elapse += Time.unscaledDeltaTime;

            float progress = Mathf.Clamp01(elapse/m_travelDuration);

            //adding more smoothness
            float smoothProgress = Mathf.SmoothStep(0,1,progress);

          
            //get the target in screenspace
            Vector2 targetScreenPos = RectTransformUtility.WorldToScreenPoint(null, target.position);
           


            RectTransformUtility.ScreenPointToLocalPointInRectangle(particleParent, targetScreenPos,null,out Vector2 targetPosition);


            //move lerp
            Vector2 pos = Vector2.Lerp(starPosition + randomOffset,targetPosition,smoothProgress);



            // adding an Arc 
            float arc = Mathf.Sin(progress * Mathf.PI)* m_arcHeight;

            pos.y += arc;

            m_rectTransform.anchoredPosition=pos;

            float scale = Mathf.Lerp(m_minIconScale, 1, progress);
            m_rectTransform.localScale = Vector3.one * scale;


            if(m_canvasGroup != null)
            {
                m_canvasGroup.alpha = Mathf.Lerp(1f, m_minIconAlpha, progress);
            }

            yield return null;
        }

        onComplete?.Invoke();
        Destroy(gameObject);
    }
}
