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
    [SerializeField] private RectTransform m_goldTarget;
    [SerializeField] private RectTransform m_foodTarget;
    [SerializeField] private RectTransform m_waterTarget;
    [SerializeField] private RectTransform m_energyTarget;
    [SerializeField] private RectTransform m_researchTarget;

    [Header("resource icons")]

    [SerializeField] private Sprite m_goldSprite;
    [SerializeField] private Sprite m_foodSprite;
    [SerializeField] private Sprite m_waterSprite;
    [SerializeField] private Sprite m_energySprite;
    [SerializeField] private Sprite m_researchSprite;

    [Header("Animation")]
    [SerializeField] private const int m_particleCount = 8;
    [SerializeField] private float m_delayBetweenParticle = 0.04f;
    [SerializeField] private float m_punchScale = 3f;

    private Camera m_uiCamera;

    private void Awake()
    {
        Instance = this;
        if (m_canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            m_uiCamera = null;
        }
        else
        {
            m_uiCamera = m_canvas.worldCamera;
        }


    }

    public void PlayCollectAnimation(Transform worldStart, ERessourceType ressourceType,int particleCount = m_particleCount)
    {
        StartCoroutine(CollectionRoutine(worldStart.position, ressourceType, particleCount));

    }
    public void PlayConsumeAnimation(Transform worldDestination,ERessourceType ressourceType, float particleSize,int particleCount = m_particleCount)
    {
        RectTransform source = GetTarget(ressourceType);
        Sprite sprite = GetSprite(ressourceType);
        Camera worldCamera = Camera.main;

        if (worldDestination == null || source == null || sprite == null ||
            worldCamera == null || particleCount <= 0)
        {
            return;
        }

        Vector3 destinationScreen = worldCamera.WorldToScreenPoint(worldDestination.position);

        if (destinationScreen.z <= 0f)
        {
            return;
        }

        Vector2 sourceScreen = RectTransformUtility.WorldToScreenPoint(
            m_uiCamera, source.TransformPoint(source.rect.center));

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(m_particleParent, sourceScreen, m_uiCamera, out Vector2 startLocal) 
            ||!RectTransformUtility.ScreenPointToLocalPointInRectangle(m_particleParent, destinationScreen, m_uiCamera, out Vector2 endLocal))
        {
            return;
        }

        StartCoroutine(ConsumeRoutine(sprite, startLocal, endLocal, particleCount, particleSize));
    }

    private IEnumerator ConsumeRoutine(Sprite sprite, Vector2 startLocal,Vector2 endLocal, int particleCount,float particleSize)
    {
        RectTransform destination = new GameObject("Consume Particle Target", typeof(RectTransform)).GetComponent<RectTransform>();

        destination.SetParent(m_particleParent, false);
        destination.sizeDelta = Vector2.zero;
        destination.localPosition = endLocal;

        int particlesRemaining = particleCount;
        var spawnDelay = new WaitForSecondsRealtime(m_delayBetweenParticle);

        
        for (int i = 0; i < particleCount; i++)
        {
            
            UiResourceParticle particle = Instantiate(m_particlePrefab, m_particleParent);
           
            particle.Initialize(sprite, startLocal, destination, () =>
            {
                particlesRemaining--;

                if (particlesRemaining == 0)
                {
                    Destroy(destination.gameObject);
                }
            });
            //scale particles
            RectTransform rect = (RectTransform)particle.transform;
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, particleSize);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, particleSize);
            if (i < particleCount - 1)
            {
                yield return spawnDelay;
            }
        }
    }
    private IEnumerator CollectionRoutine(Vector3 worldPos, ERessourceType ressourceType, int particleCount = m_particleCount)
    {
        RectTransform target = GetTarget(ressourceType);
        Sprite sprite = GetSprite(ressourceType);

        if (target == null || sprite == null)
        {
            yield break;
        }

        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_particleParent, screenPos, m_uiCamera, out Vector2 localPosition);


        for (int i = 0; i < particleCount; i++)
        {
            UiResourceParticle particle = Instantiate(m_particlePrefab, m_particleParent);
            particle.Initialize(sprite, localPosition, target, () => AnimateTarget(target));
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

            case ERessourceType.GOLD:
                return m_goldTarget;

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

            case ERessourceType.GOLD:
                return m_goldSprite;

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
