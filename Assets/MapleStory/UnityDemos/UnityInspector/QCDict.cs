/*
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif


    [Serializable]
    public class QS_Dictionary
    {

#if UNITY_EDITOR
        public static SerializedProperty GetValueProperty(SerializedProperty qsDicProperty, int index)
        {
            return qsDicProperty.FindPropertyRelative("_list").GetArrayElementAtIndex(index).FindPropertyRelative("_Value");
        }
        public static SerializedProperty GetKeyProperty(SerializedProperty qsDicProperty, int index)
        {
            return qsDicProperty.FindPropertyRelative("_list").GetArrayElementAtIndex(index).FindPropertyRelative("_Key");
        }
#endif
    }

    [Serializable]
    public class QS_KeyValuePair { }

    [Serializable]
    public class QS_KeyValuePair<TKey, TValue>: QS_KeyValuePair
    {
        public TKey _Key;
        public TValue _Value;

        public QS_KeyValuePair(TKey key, TValue value)
        {
            _Key = key;
            _Value = value;
        }
    }

    [Serializable]
    public class QS_Dictionary<TKey, TValue> :
        QS_Dictionary,
        ISerializationCallbackReceiver,
        IDictionary<TKey, TValue>
    {
        [SerializeField]
        private List<QS_KeyValuePair<TKey, TValue>> _list = new List<QS_KeyValuePair<TKey, TValue>>();
        private Dictionary<TKey, int> _keyPositions = new Dictionary<TKey, int>();

        public QS_Dictionary()
        {

        }
        private Dictionary<TKey, int> MakeKeyPositions()
        {
            var dictionary = new Dictionary<TKey, int>(_list.Count);
            for (var i = 0; i < _list.Count; i++)
            {
                dictionary[_list[i]._Key] = i;
            }
            return dictionary;
        }

        public void OnBeforeSerialize() { }

        public void OnAfterDeserialize()
        {
            _keyPositions = MakeKeyPositions();
        }

        #region IDictionary<TKey, TValue>

        public TValue this[TKey key]
        {
            get => _list[_keyPositions[key]]._Value;
            set
            {
                var pair = new QS_KeyValuePair<TKey, TValue>(key, value);
                if (_keyPositions.ContainsKey(key))
                {
                    _list[_keyPositions[key]] = pair;
                }
                else
                {
                    _keyPositions[key] = _list.Count;
                    _list.Add(pair);
                }
            }
        }

        public ICollection<TKey> Keys => _list.Select(tuple => tuple._Key).ToArray();
        public ICollection<TValue> Values => _list.Select(tuple => tuple._Value).ToArray();

        public void Add(TKey key, TValue value)
        {
            if (_keyPositions.ContainsKey(key))
                throw new ArgumentException("An element with the same key already exists in the dictionary.");
            else
            {
                _keyPositions[key] = _list.Count;
                _list.Add(new QS_KeyValuePair<TKey, TValue>(key, value));
            }
        }

        public bool ContainsKey(TKey key) => _keyPositions.ContainsKey(key);

        public bool Remove(TKey key)
        {
            if (_keyPositions.TryGetValue(key, out var index))
            {
                _keyPositions.Remove(key);

                _list.RemoveAt(index);
                for (var i = index; i < _list.Count; i++)
                    _keyPositions[_list[i]._Key] = i;

                return true;
            }
            else
                return false;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (_keyPositions.TryGetValue(key, out var index))
            {
                value = _list[index]._Value;
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }

        #endregion

        #region ICollection <KeyValuePair<TKey, TValue>>

        public int Count => _list.Count;
        public bool IsReadOnly => false;

        public void Add(KeyValuePair<TKey, TValue> kvp) => Add(kvp.Key, kvp.Value);

        public void Clear()
        {
            _list.Clear();
            _keyPositions.Clear();
        }
        public bool Contains(KeyValuePair<TKey, TValue> kvp) => _keyPositions.ContainsKey(kvp.Key);

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            var numKeys = _list.Count;
            if (array.Length - arrayIndex < numKeys)
                throw new ArgumentException("arrayIndex");
            for (var i = 0; i < numKeys; i++, arrayIndex++)
            {
                var entry = _list[i];
                array[arrayIndex] = new KeyValuePair<TKey, TValue>(entry._Key, entry._Value);
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> kvp) => Remove(kvp.Key);

        #endregion

        #region IEnumerable <KeyValuePair<TKey, TValue>>

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _list.Select(ToKeyValuePair).GetEnumerator();

            static KeyValuePair<TKey, TValue> ToKeyValuePair(QS_KeyValuePair<TKey, TValue> skvp)
            {
                return new KeyValuePair<TKey, TValue>(skvp._Key, skvp._Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(QS_KeyValuePair), true)]
    public class SerializableKeyValuePairDrawer : PropertyDrawer
    {
        private const int TOP_SPACE = 4;
        private const int BOTTOM_SPACE = 4;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty keyProperty = property.FindPropertyRelative("_Key");
            SerializedProperty valueProperty = property.FindPropertyRelative("_Value");
            GUI.Box(position, GUIContent.none, EditorStyles.helpBox);

            GUI.enabled = false;
            float keyHeight = EditorGUI.GetPropertyHeight(keyProperty, keyProperty.isExpanded);
            Rect keyRect = new Rect(position.x, position.y+TOP_SPACE,position.width, keyHeight);
            fixWidth(ref keyRect, keyProperty);
            EditorGUI.PropertyField(keyRect,keyProperty, keyProperty.isExpanded);
            GUI.enabled = true;

            float valueHeight = EditorGUI.GetPropertyHeight(valueProperty, valueProperty.isExpanded);
            Rect valueRect = new Rect(position.x, keyRect.y+ keyRect.height,position.width, valueHeight);
            fixWidth(ref valueRect, valueProperty);
            EditorGUI.PropertyField(valueRect,valueProperty, valueProperty.isExpanded);
        }
        private void fixWidth(ref Rect originRect, SerializedProperty property)
        {
            if (property.hasChildren)
            {
                originRect.x += 0.04f * originRect.width / 2f;
                originRect.width *= 0.96f;
            }
            else
            {
                originRect.x += 0.01f * originRect.width / 2f;
                originRect.width *= 0.99f;
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = 0;
            SerializedProperty keyProperty = property.FindPropertyRelative("_Key");
            SerializedProperty valueProperty = property.FindPropertyRelative("_Value");
            height += EditorGUI.GetPropertyHeight(keyProperty, keyProperty.isExpanded);
            height += EditorGUI.GetPropertyHeight(valueProperty, valueProperty.isExpanded);
            height += TOP_SPACE + BOTTOM_SPACE;
            return height;
        }
    }
    [CustomPropertyDrawer(typeof(QS_Dictionary), true)]
    public class SerializableDictionaryDrawer : PropertyDrawer
    {
        private const int ELEMENT_SPACE = 2;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty listProperty = property.FindPropertyRelative("_list");
            float posY = position.y;
            Rect foldoutRect = new Rect(position.x,position.y,position.width, EditorGUIUtility.singleLineHeight);
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label);
            posY += foldoutRect.height;
            if (property.isExpanded)
            {
                for (int i = 0; i < listProperty.arraySize; i++)
                {
                    SerializedProperty one = listProperty.GetArrayElementAtIndex(i);
                    float height = EditorGUI.GetPropertyHeight(one, one.isExpanded);
                    Rect keyRect = new Rect(position.x + 0.01f * position.width / 2f, posY,0.99f*position.width,height);
                    EditorGUI.PropertyField(keyRect, one, one.isExpanded);
                    posY += keyRect.height + ELEMENT_SPACE;
                }
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty listProperty = property.FindPropertyRelative("_list");
            float height = 0;
            height += EditorGUIUtility.singleLineHeight;
            if (property.isExpanded)
            {
                for (int i = 0; i < listProperty.arraySize; i++)
                {
                    SerializedProperty one = listProperty.GetArrayElementAtIndex(i);
                    float oneHeight = EditorGUI.GetPropertyHeight(one, one.isExpanded);
                    height += oneHeight + ELEMENT_SPACE;
                }
            }
            return height ;
        }
    }
#endif

*/