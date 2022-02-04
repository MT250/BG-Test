using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isInvincible;

    [SerializeField] private float invincibilityMaxTime;
    [SerializeField] private GameObject deathPrefab;
    [SerializeField] private GameObject victoryPrefab;
    [Space(20)]

    [SerializeField] private Color defaultColor;

    [SerializeField] private Color invincibleColor;
    private float invincibilityTime;

    private void Awake()
    {
        invincibilityTime = invincibilityMaxTime;
    }

    private void Update()
    {
        //TODO: Remake
        if (isInvincible && invincibilityTime > 0f) invincibilityTime -= Time.deltaTime;
        else if (isInvincible && invincibilityTime <= 0f) SetInvincibility(false);

        if (invincibilityTime <= invincibilityMaxTime && isInvincible == false) invincibilityTime += Time.deltaTime;
    }

    public void SetInvincibility(bool _invincible)
    {
        isInvincible = _invincible;

        if (_invincible)
        {
            var renderer = gameObject.GetComponent<Renderer>();
            renderer.material.color = invincibleColor;
        }
        else
        {
            var renderer = gameObject.GetComponent<Renderer>();
            renderer.material.color = defaultColor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            GameManager.instance.Victory();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("DangerZone"))
        {
            if (!isInvincible || invincibilityTime <= 0f)
            {
                Instantiate(deathPrefab, transform.position, deathPrefab.transform.rotation);
                GameManager.instance.Invoke("ResetPlayer", 2f);
                gameObject.SetActive(false);
            }
        }
    }

    public void spawnVictoryEmmiter()
    {
        Instantiate(victoryPrefab, transform.position, victoryPrefab.transform.rotation);
    }
}
