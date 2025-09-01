using UnityEngine;

public class ItemButton : MonoBehaviour, IButtonParameter
{
    private Item targetItem;

    public void ReceiveParameter(object parameter)
    {
        targetItem = parameter as Item;

        if (targetItem == null)
        {
            Debug.LogWarning("Invalid Item passed to ItemButton.");
        }
    }

    public void Click()
    {
        if (targetItem != null)
        {
            if (targetItem.CompareTag("FlashLight"))
            {
                Inventory.Instance.hasFlashlight = true;
            }
            else
            {
                GameManager.Instance.gameState++;
            }
            Destroy(targetItem.gameObject);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No Item assigned to ItemButton.");
        }
    }
}
