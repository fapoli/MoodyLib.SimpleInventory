using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoodyLib.SimpleInventory {

    /// <summary>
    /// Simple inventory component that stores items identified by <see cref="IIdentifiable"/>.
    /// 
    /// Can be attached to any GameObject (player, chest, container, etc.).
    /// Provides basic operations to add, remove and query items, and exposes
    /// instance events when items are added or removed.
    /// </summary>
    public class Inventory : MonoBehaviour {

        /// <summary>
        /// Invoked whenever an item is added to this inventory instance.
        /// </summary>
        public event Action<IIdentifiable> OnItemAdded;

        /// <summary>
        /// Invoked whenever an item is removed from this inventory instance.
        /// </summary>
        public event Action<IIdentifiable> OnItemRemoved;

        private List<IIdentifiable> _items = new List<IIdentifiable>();

        /// <summary>
        /// Checks whether this inventory contains at least one item with the given ID.
        /// </summary>
        public bool HasItem(string itemId) {
            foreach (var item in _items) {
                if (item.ID == itemId) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns a list of item counts for all items of type <typeparamref name="T"/>
        /// stored in this inventory. Items that do not implement <typeparamref name="T"/>
        /// are ignored.
        /// </summary>
        public List<ItemCount<T>> ListItems<T>() where T : IIdentifiable {
            var count = new Dictionary<string, ItemCount<T>>();

            foreach (var item in _items) {
                if (item is not T typed)
                    continue;

                if (count.TryGetValue(typed.ID, out var entry)) {
                    count[typed.ID] = new ItemCount<T>(typed, entry.count + 1);
                } else {
                    count[typed.ID] = new ItemCount<T>(typed, 1);
                }
            }

            return new List<ItemCount<T>>(count.Values);
        }


        /// <summary>
        /// Adds an item to this inventory and raises <see cref="OnItemAdded"/>.
        /// </summary>
        public void AddItem(IIdentifiable item) {
            _items.Add(item);
            OnItemAdded?.Invoke(item);
        }

        /// <summary>
        /// Removes the first item that matches the given ID and raises <see cref="OnItemRemoved"/>.
        /// </summary>
        public void RemoveItem(string itemId) {
            for (int i = _items.Count - 1; i >= 0; i--) {
                if (_items[i].ID != itemId) continue;

                var item = _items[i];
                _items.RemoveAt(i);
                OnItemRemoved?.Invoke(item);

                return;
            }
        }

        /// <summary>
        /// Removes all items that match the given ID and raises <see cref="OnItemRemoved"/>
        /// for each removed item.
        /// </summary>
        public void RemoveAllItems(string itemId) {
            for (int i = _items.Count - 1; i >= 0; i--) {
                if (_items[i].ID != itemId) continue;

                var item = _items[i];
                _items.RemoveAt(i);
                OnItemRemoved?.Invoke(item);
            }
        }

        /// <summary>
        /// Clears this inventory. Optionally triggers <see cref="OnItemRemoved"/> for each removed item.
        /// </summary>
        public void Clear(bool triggerEvents = true) {
            for (int i = _items.Count - 1; i >= 0; i--) {
                var item = _items[i];
                _items.RemoveAt(i);

                if (triggerEvents) {
                    OnItemRemoved?.Invoke(item);
                }
            }
        }
    }
}
