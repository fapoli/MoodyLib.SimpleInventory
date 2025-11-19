# MoodyLib.SimpleInventory

A lightweight and flexible inventory system for Unity.  
Designed around simple identifiers, instance-based inventories, and clean data aggregation.

## Contents
- **IIdentifiable** – Base interface used for item identification.
- **Inventory** – Component that stores, adds, removes and lists items.
- **ItemCount<T>** – Immutable struct representing grouped item summaries.
- **Inventory Prefab** – A preconfigured GameObject containing an `Inventory` component, ready to be dropped into the scene.

## Install via Git URL

1. In Unity, open **Window > Package Manager**.
2. Click the **+** button in the top-left corner.
3. Select **Add package from Git URL…**
4. Paste the following URL and click **Add**:

   ```text
   https://github.com/fapoli/MoodyLib.SimpleInventory.git
   ```

Unity will download and install the package. After installation, it will appear under the **Packages** folder.

## Using the Inventory Prefab

The package includes a prefab located in:

```
Runtime/Prefabs/Inventory.prefab
```

This prefab contains a fully configured `Inventory` component and is the recommended way to add an inventory to your scene or player object.

### To use it:

1. Drag **Inventory.prefab** into your scene.
2. Assign it to your player, container, or any GameObject.
3. Access it through:
   ```csharp
   var inventory = FindObjectOfType<Inventory>();
   ```
   or via tag, or via serialized references.

If you need multiple inventories (players, chests, vendors), simply duplicate the prefab or add additional `Inventory` components.

## How to use

### 1. Define an identifiable item

```csharp
public class Apple : IIdentifiable {
    public string ID => "apple";
}
```

### ✔ Recommendation: Use ScriptableObjects for items

While any class can implement `IIdentifiable`, using **ScriptableObjects** is highly recommended:

- Easy to edit in the Unity Inspector
- Persistent and stable asset-based data
- Lightweight and efficient (no runtime instantiation needed)
- Perfect for building item catalogs
- IDs remain consistent across play sessions

Example:

```csharp
[CreateAssetMenu(menuName = "Inventory/Item")]
public class ItemDefinition : ScriptableObject, IIdentifiable {
    public string ID => name;
    
    public string label;
    public GameObject prefab;
    public Texture2D icon;
}
```

Add it to the inventory:

```csharp
inventory.AddItem(someItemDefinition);
```

---

### 2. Add an Inventory component or use the prefab

```csharp
[SerializeField] private Inventory inventory;
```

Or find it by tag:

```csharp
var inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
```

### 3. Add and remove items

```csharp
inventory.AddItem(new Apple());
inventory.RemoveItem("apple");
```

### 4. Listen for events

```csharp
inventory.OnItemAdded += item => Debug.Log("Added: " + item.ID);
inventory.OnItemRemoved += item => Debug.Log("Removed: " + item.ID);
```

### 5. List items grouped by type

```csharp
var apples = inventory.ListItems<Apple>();

foreach (var entry in apples) {
    Debug.Log($"{entry.item.ID} x {entry.count}");
}
```

Returns aggregated counts using `ItemCount<T>`.

## Best Practices

### ✔ Use simple, stable item IDs
Keep IDs consistent and never change them at runtime.

### ✔ Prefer ScriptableObjects for item definitions
They are asset-based, stable, efficient, and ideal for item catalogs.

### ✔ Keep inventories instance-based
Attach `Inventory` wherever needed (players, chests, shops, etc.).

### ✔ Avoid heavy logic inside items
Treat items as lightweight data containers unless extended intentionally.


## API Reference

### `Inventory.AddItem(IIdentifiable item)`
Adds an item and triggers `OnItemAdded`.

### `Inventory.RemoveItem(string itemId)`
Removes the first matching item and triggers `OnItemRemoved`.

### `Inventory.RemoveAllItems(string itemId)`
Removes all matching items.

### `Inventory.HasItem(string itemId)`
Checks existence by ID.

### `Inventory.Clear(bool triggerEvents = true)`
Empties the inventory.

### `Inventory.ListItems<T>()`
Groups items of type `T` into immutable `ItemCount<T>` entries.

