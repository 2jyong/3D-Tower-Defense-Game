using UnityEngine;
using UnityEngine.UI;

public class Enemy : EnemyTransform
{
    public int Prize;
    public float MaxHp;
    private float currentHp;

    public RectTransform HealthBar { get; set; } = null;
    public Image PrograssBar { get; set; } = null;

    public void SetParameter(int index)
    {
        MaxHp = MaxHp * index;
        Speed = Speed * index;
        Prize = Prize + Mathf.FloorToInt(Prize / 2);

        currentHp = MaxHp;
    }

    public void OnHit(float damage)
    {
        if (PrograssBar == null)
            PrograssBar = HealthBar.GetChild(0).GetChild(0).GetComponent<Image>();

        currentHp -= damage;
        PrograssBar.fillAmount = currentHp / MaxHp;

        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private Vector3 FlexibleVector = new Vector3(0, 1f, 0.25f);
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (HealthBar == null) return;
        HealthBar.position = transform.position + FlexibleVector;
    }

    private void OnDestroy()
    {
#if UNITY_EDITOR
        if (GameManager.Get == null) return;
        if (GameManager.Get.moneyText == null) return;
#endif
        SpawnManager.Get.DestroyEnemy(this);

        if (HealthBar != null)
            Destroy(HealthBar.gameObject);

        if (currentHp <= 0)
            GameManager.Get.Money += Prize;
    }


}
