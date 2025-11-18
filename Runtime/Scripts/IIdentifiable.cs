namespace MoodyLib.SimpleInventory {

    /// <summary>
    /// Defines a unique string identifier for inventory-related objects.
    /// 
    /// This interface is used throughout the inventory library to uniquely
    /// reference item definitions.
    /// Identifiers should remain stable and must not change at runtime.
    /// </summary>
    public interface IIdentifiable {

        /// <summary>
        /// A unique and stable string identifier for the object.
        /// </summary>
        string ID { get; }
    }
}