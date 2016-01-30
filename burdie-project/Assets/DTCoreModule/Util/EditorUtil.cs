using DT;
using System;
﻿using System.Reflection;
﻿using System.Collections.Generic;
﻿using UnityEngine;

namespace DT {
	public static class EditorUtil {
    /// <summary>
    /// Using reflection, get all derived types of a given type
    /// </summary>
    public static System.Type[] GetAllDerivedTypes(this AppDomain appDomain, Type type) {
      List<Type> result = new List<Type>();
      Assembly[] assemblies = appDomain.GetAssemblies();
      foreach (Assembly assembly in assemblies) {
        Type[] types = assembly.GetTypes();
        foreach (Type assemblyType in types) {
          if (assemblyType.IsSubclassOf(type)) {
            result.Add(assemblyType);
          }
        }
      }
      return result.ToArray();
    }
	}
}