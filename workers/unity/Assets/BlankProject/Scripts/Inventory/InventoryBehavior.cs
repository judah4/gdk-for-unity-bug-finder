using System.Collections;
using System.Collections.Generic;
using BlankProject;
using Improbable.Gdk.Subscriptions;
using Improbable.Item;
using UnityEngine;

[WorkerType(UnityGameLogicConnector.WorkerType)]
public class InventoryBehavior : MonoBehaviour
{

    [Require]
    private InventoryWriter _inventoryWriter;

    [SerializeField]
    private GUISkin _skin;

    void OnGUI()
    {
        if (_inventoryWriter == null)
            return;

        GUILayout.BeginArea(new Rect(5, 515, 200, 200), _skin.window);
        if (GUILayout.Button("Add Item"))
        {
            var inv = _inventoryWriter.Data.Slots;
            var item = new ItemData("item123", "", 0, "", 1, new Dictionary<DataMapping, string>() );

            for (int cnt = 0; cnt < _inventoryWriter.Data.MaxSlots; cnt++)
            {
                if (inv.ContainsKey(cnt) == false)
                {
                    inv.Add(cnt, item);
                    break;
                }
            }

            _inventoryWriter.SendUpdate(new Inventory.Update() {Slots = inv});

            
        }

        if (GUILayout.Button("Remove Item"))
        {
            var inv = _inventoryWriter.Data.Slots;

            for (int cnt = 0; cnt < _inventoryWriter.Data.MaxSlots; cnt++)
            {
                if (inv.ContainsKey(cnt))
                {
                    inv.Remove(cnt);
                    break;
                }
            }

            _inventoryWriter.SendUpdate(new Inventory.Update() { Slots = inv });


        }

        if (GUILayout.Button("Remove Item + Integer Update"))
        {
            var inv = _inventoryWriter.Data.Slots;

            for (int cnt = 0; cnt < _inventoryWriter.Data.MaxSlots; cnt++)
            {
                if (inv.ContainsKey(cnt))
                {
                    inv.Remove(cnt);
                    break;
                }
            }

            _inventoryWriter.SendUpdate(new Inventory.Update() { Slots = inv, ContainerId = _inventoryWriter.Data.ContainerId + 1, });


        }

        GUILayout.EndArea();


    }
}
