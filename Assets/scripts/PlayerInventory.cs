using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [Header("General")]
    public List<itemType> inventoryList;
    public int selectedItem = 0;
    public float playerReach;


    [Space(20)]
    [Header("Keys")]
    [SerializeField] KeyCode pickItemKey;


    [Space(20)]
    [Header("Item gameObjects")]
    [SerializeField] GameObject crowbar_item;
    [SerializeField] GameObject key_item;
    [SerializeField] GameObject flashlight_item;

    [Space(20)]
    [Header("Item prefabs")]
    [SerializeField] GameObject crowbar_prefab;
    [SerializeField] GameObject key_prefab;
    [SerializeField] GameObject flashlight_prefab;

    [Space(20)]
    [Header("UI")]
    [SerializeField] Image[] inventorySlotImage = new Image[4];
    [SerializeField] Image[] inventoryBackgroundImage = new Image[4];
    [SerializeField] Sprite emptySlotSprite;

    [SerializeField] Camera cam;
    [SerializeField] GameObject pickUpItem_gameobject;

    private Dictionary<itemType, GameObject> itemSetActive = new Dictionary<itemType, GameObject>() { };

    void Start()
    {
        itemSetActive.Add(itemType.Crowbar, crowbar_item);
        itemSetActive.Add(itemType.Key, key_item);
        itemSetActive.Add(itemType.Flashlight, flashlight_item);

        NewItemSelected();
    }

    void Update()
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, playerReach))
        {
            IPickable item = hitInfo.collider.GetComponent<IPickable>();
            if (item != null)
            {
                pickUpItem_gameobject.SetActive(true);
                if (Input.GetKey(pickItemKey))
                {
                    inventoryList.Add(hitInfo.collider.GetComponent<ItemPickable>().itemScriptableObject.item_type);
                    item.PickItem();
                }
            }
            else
            {
                pickUpItem_gameobject.SetActive(false);
            }
        }
        else
        {
            pickUpItem_gameobject.SetActive(false);
        }

        //UI

        for (int i = 0; i < 3; i++)
        {
            if(i < inventoryList.Count)
            {
                inventorySlotImage[i].sprite = itemSetActive[inventoryList[i]].GetComponent<Item>().itemScriptableObject.item_sprite;
            }
            else
            {
                inventorySlotImage[i].sprite = emptySlotSprite;
            }
        }

        int a = 0;
        foreach(Image image in inventoryBackgroundImage)
        {
            if(a == selectedItem)
            {
                image.color = new Color32(145, 255, 126, 255);
            }
            else
            {
                image.color = new Color32(255, 255, 255, 255);
            }
            a++;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && inventoryList.Count > 0)
        {
            selectedItem = 0;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && inventoryList.Count > 1)
        {
            selectedItem = 1;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && inventoryList.Count > 2)
        {
            selectedItem = 2;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && inventoryList.Count > 3)
        {
            selectedItem = 3;
            NewItemSelected();
        }
    }

    private void NewItemSelected()
    {
        crowbar_item.SetActive(false);
        key_item.SetActive(false);
        flashlight_item.SetActive(false);

        GameObject selectedItemGameObject = itemSetActive[inventoryList[selectedItem]];
        selectedItemGameObject.SetActive(true);
    }
}

public interface IPickable
{
    void PickItem();
}