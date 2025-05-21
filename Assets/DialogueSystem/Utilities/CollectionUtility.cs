using System.Collections.Generic;
public static class CollectionUtility {
	public static void AddItem<T, K>(this SerializedDictionary<T, List<K>> serializableDictionary, T key, K value) {
		if (serializableDictionary.ContainsKey(key)) {
			serializableDictionary[key].Add(value);

			return;
		}
		serializableDictionary.Add(key, new List<K>() { value });
	}
}