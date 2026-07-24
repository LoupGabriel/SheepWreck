using System.Collections;
using UnityEngine;

public class UiResourceAnimationManager : MonoBehaviour
{
   public static UiResourceAnimationManager Instance { get; private set; }

    [Header("Reference")]
    [SerializeField] private Canvas m_canvas;
    [SerializeField] private RectTransform m_particleParent;
    [SerializeField] private UiResourceParticle m_particlePrefab;

    [Header("Resource Target")]
    [SerializeField] private RectTransform m_foodTarget;
    [SerializeField] private RectTransform m_waterTarget;
    [SerializeField] private RectTransform m_energyTarget;
    [SerializeField] private RectTransform m_researchTarget;

    [Header("resource icons")]
    [SerializeField] private Sprite m_foodSprite;
    [SerializeField] private Sprite m_waterSprite;
    [SerializeField] private Sprite m_energySprite;
    [SerializeField] private Sprite m_researchSprite;

    [Header("Animation")]
    [SerializeField] private int m_particleCount = 8;
    [SerializeField] private float m_delayBetweenParticle = 0.04f;
    [SerializeField] private float m_punchScale = 3f;

    private Camera m_uiCamera;

    private void Awake()
    {
        Instance = this;
        if(m_canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            m_uiCamera = null;
        }else
        {
            m_uiCamera = m_canvas.worldCamera;
        }


    }



    public void PlayCollectAnimation(Transform worldStart,ERessourceType ressourceType)
    {
        StartCoroutine(CollectionRoutine(worldStart.position, ressourceType));

    }

    private IEnumerator CollectionRoutine(Vector3 worldPos,ERessourceType ressourceType)
    {
        RectTransform target = GetTarget(ressourceType);
        Sprite sprite = GetSprite(ressourceType);

        if (target == null || sprite == null)
        {
            yield break;
        }

        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_particleParent, screenPos, m_uiCamera, out Vector2 localPosition);
        

        for(int i = 0; i < m_particleCount; i++)
        {
            UiResourceParticle particle = Instantiate(m_particlePrefab,m_particleParent);
            particle.Initialize(sprite, localPosition,target,()=> AnimateTarget(target));
            yield return new WaitForSecondsRealtime(m_delayBetweenParticle);
        }
    }

    private RectTransform GetTarget(ERessourceType ressourceType)
    {
        switch (ressourceType)
        {
            case ERessourceType.FOOD:
                return m_foodTarget;

            case ERessourceType.WATER:
                return m_waterTarget;

            case ERessourceType.ENERGY:
                return m_energyTarget;

            case ERessourceType.RESEARCH:
                return m_researchTarget;
                
                default: return null;
        }
    }
    private Sprite GetSprite(ERessourceType ressourceType)
    {
        switch (ressourceType)
        {
            case ERessourceType.FOOD:
                return m_foodSprite;

            case ERessourceType.WATER:
                return m_waterSprite;

            case ERessourceType.ENERGY:
                return m_energySprite;

            case ERessourceType.RESEARCH:
                return m_researchSprite;
                
                default: return null;
        }
    }

    private void AnimateTarget(RectTransform target)
    {
        StopCoroutine(nameof(TargetPunchRoutine));
        StartCoroutine(TargetPunchRoutine(target));
    }

    /// <summary>
    /// Animate the target on particle hit
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private IEnumerator TargetPunchRoutine(RectTransform target)
    {
        Vector3 initialScale = Vector3.one;
        Vector3 punchScale = Vector3.one * m_punchScale;

        float elapsed = 0f;
        float duration = 0.12f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float progress = elapsed / duration;

            target.localScale = Vector3.Lerp(
                initialScale,
                punchScale,
                progress
            );

            yield return null;
        }

        elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float progress = elapsed / duration;

            target.localScale = Vector3.Lerp(
                punchScale,
                initialScale,
                progress
            );

            yield return null;
        }
        SfxManager.PlaySfx("Click");
        target.localScale = initialScale;
    }


}
