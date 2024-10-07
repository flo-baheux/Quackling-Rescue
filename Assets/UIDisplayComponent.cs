using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplayComponent : MonoBehaviour
{
  private Player player;

  [SerializeField] private Image honkFill;
  [SerializeField] private GameObject honkContainer;
  [SerializeField] private Animator animator;
  [SerializeField] private TextMeshProUGUI ducklingCount;
  public float speed = 10f;

  void Start()
  {
    honkFill.fillAmount = 1;
    int a = Duckling.entities.Count;
    int b = player.currentlyFollowingCount;
  }

  void Update()
  {
    if (!player)
    {
      player = FindObjectOfType<Player>();
      if (player)
        player.OnHonk += HandleHonk;
    }

    ducklingCount.text = $"{player.currentlyFollowingCount} / {Duckling.entities.Count}";
  }

  private void HandleHonk(float cooldown)
  {
    StartCoroutine(HookCooldownDisplayCoroutine(cooldown));
  }

  private IEnumerator HookCooldownDisplayCoroutine(float cooldown)
  {
    honkFill.fillAmount = 0;
    float timer = 0f;
    while (timer < cooldown)
    {
      timer += Time.deltaTime;
      honkFill.fillAmount += 1.0f / cooldown * Time.deltaTime;
      yield return null;
    }
    animator.SetTrigger("HonkUp");
  }
}
