using System.Collections;
using System.Collections.Generic;
using BlankProject;
using Improbable.Gdk.Subscriptions;
using Improbable.Item;
using UnityEngine;

[WorkerType(UnityClientConnector.WorkerType)]
public class InventoryVisualizer : MonoBehaviour
{
    [Require]
    private InventoryReader _inventoryReader;

    [SerializeField]
    private GUISkin _skin;

    [SerializeField] private List<Inventory.Update> _updates = new List<Inventory.Update>();

    void OnEnable()
    {
        _inventoryReader.OnUpdate += (update) =>
        {
            _updates.Add(update);

            if (_updates.Count > 5)
            {
                _updates.RemoveAt(0);
            }

        };
    }


    void OnGUI()
    {
        if(_inventoryReader == null)
            return;

        GUILayout.BeginArea(new Rect(5,5, 200, 500), _skin.window);
        GUILayout.Label("Inventory");
        for (int cnt = 0; cnt < _inventoryReader.Data.MaxSlots; cnt++)
        {
            if (_inventoryReader.Data.Slots.ContainsKey(cnt))
            {
                var itemData = _inventoryReader.Data.Slots[cnt];
                GUILayout.Label($"Slot {cnt}: {itemData.ItemId}");
            }
            else
            {
                GUILayout.Label($"Slot {cnt}: [EMPTY]");
            }
        }

        GUILayout.Label("Updates");
        foreach (var update in _updates)
        {
            GUILayout.Label($"Slots Count: {update.Slots.Value.Count}");
        }

        GUILayout.EndArea();
        

    }
}
