using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnSliderSelectedChangeBackgroundColor : MonoBehaviour, ISelectHandler, IDeselectHandler
{
  private Image background;
  private Sprite defaultBackgroundSprite;

  [SerializeField]
  private Sprite selectedBackgroundSprite;

  void Awake()
  {
    background = transform.GetChild(0).GetComponent<Image>();

    defaultBackgroundSprite = background.sprite;
  }

  public void OnSelect(BaseEventData eventData)
  {
    background.sprite = selectedBackgroundSprite;
  }

  public void OnDeselect(BaseEventData eventData)
  {
    background.sprite = defaultBackgroundSprite;
  }
}
