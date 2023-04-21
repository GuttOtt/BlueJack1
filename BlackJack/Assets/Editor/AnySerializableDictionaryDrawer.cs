using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SituationComponentDictionary))]
[CustomPropertyDrawer(typeof(IntIntDictionary))]
[CustomPropertyDrawer(typeof(IntCardIconsDictionary))]
public class UserSerializableDictionaryDrawer : SerializableDictionaryPropertyDrawer {}
