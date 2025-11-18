namespace MoodyLib.SimpleInventory {

    /// <summary>
    /// Represents an immutable summary of how many times a specific item appears
    /// in an inventory. This is typically used when grouping or aggregating
    /// items by their identifier.
    /// </summary>
    /// <typeparam name="T">
    /// The identifiable item type. Must implement <see cref="IIdentifiable"/>.
    /// </typeparam>
    public readonly struct ItemCount<T> where T : IIdentifiable {

        /// <summary>
        /// The item being counted. This usually represents a single instance
        /// or item definition associated with the group.
        /// </summary>
        public T item { get; }

        /// <summary>
        /// The number of occurrences of the item inside the inventory.
        /// Always a non-negative integer.
        /// </summary>
        public int count { get; }

        /// <summary>
        /// Creates a new immutable item count entry.
        /// </summary>
        /// <param name="item">The item being counted.</param>
        /// <param name="count">How many occurrences were found.</param>
        public ItemCount(T item, int count) {
            this.item = item;
            this.count = count;
        }
    }
}