using UnityEngine;

public class DrugCabinet : OpenPopup
{
	public override void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			base.OnCollisionEnter2D (coll);
			InventoryManager.instance.itemSlotsParentWhenInDrugCabinet.gameObject.SetActive(true);
			for (int i = 0; i < InventoryManager.instance.itemSlotsParentWhenNotInDrugCabinet.childCount; i ++)
			{
				Transform itemSlotTrs = InventoryManager.instance.itemSlotsParentWhenNotInDrugCabinet.GetChild(i);
				itemSlotTrs.SetParent(InventoryManager.instance.itemSlotsParentWhenInDrugCabinet);
				itemSlotTrs.localScale = Vector3.one;
				i --;
			}
		}
	}

	public override void IfClicked () 
	{
		base.IfClicked ();
		InventoryManager.instance.itemSlotsParentWhenInDrugCabinet.gameObject.SetActive(false);
		for (int i = 0; i < InventoryManager.instance.itemSlotsParentWhenInDrugCabinet.childCount; i ++)
		{
			Transform itemSlotTrs = InventoryManager.instance.itemSlotsParentWhenInDrugCabinet.GetChild(i);
			itemSlotTrs.SetParent(InventoryManager.instance.itemSlotsParentWhenNotInDrugCabinet);
			itemSlotTrs.localScale = Vector3.one;
			i --;
		}
	}
}
