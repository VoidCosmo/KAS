﻿// This is an intermediate module for methods and classes that are considred as candidates for
// KSPDev Utilities. Ideally, this module is always empty but there may be short period of time
// when new functionality lives here and not in KSPDev.

using KSPDev.GUIUtils;
using KSPDev.LogUtils;
using UnityEngine;
using KSPDev.ConfigUtils;
using System;
using System.Linq;

namespace KSPDev {

/// <summary>Generic interface for the modules that implement a UI context menu.</summary>
/// <seealso href="https://kerbalspaceprogram.com/api/class_game_events.html#ae6daaa6f39473078514543a2f8485513">
/// KPS: GameEvents.onPartActionUICreate</seealso>
/// <seealso href="https://kerbalspaceprogram.com/api/class_game_events.html#a7ccbd16e2aee0176a4431f0b5f9d63e5">
/// KPS: GameEvents.onPartActionUIDismiss</seealso>
public interface IHasContextMenu {
  /// <summary>
  /// A callback that is called every time the module's conetxt menu items need to update. 
  /// </summary>
  /// <remarks>
  /// It's very implementation dependent when and why the update is needed. However, at the very
  /// least this callback must be called on the parts load to let the module to update the state and
  /// the titles.
  /// </remarks>
  void UpdateContextMenu();
}

}  // namespace

namespace KSPDev.GUIUtils {

/// TODO: Drop it.
public static class A {
  /// TODO: Merge into KSPDev.GUIUtils.Messages
  public static string Format(this Message msg) {
    return (string) msg;
  }
}

}  // namepsace

namespace KSPDev.ResourceUtils {

/// <summary>
/// A helper class that holds string and ID defintions for all the game stock resources. 
/// </summary>
/// <seealso href="http://wiki.kerbalspaceprogram.com/wiki/Resource">KSP Wiki: Resource</seealso>
public static class StockResourceNames {
  /// <summary>Electric charge resource name.</summary>
  /// <seealso href="http://wiki.kerbalspaceprogram.com/wiki/Electric_charge">
  /// KSP Wiki: Electric charge</seealso>
  public const string ElectricCharge = "ElectricCharge";

  /// <summary>Liquid fuel resource name.</summary>
  /// <seealso href="http://wiki.kerbalspaceprogram.com/wiki/Liquid_fuel">
  /// KSP Wiki: Liquid fuel</seealso>
  public const string LiquidFuel = "LiquidFuel";

  /// <summary>Oxidizer resource name.</summary>
  /// <seealso href="http://wiki.kerbalspaceprogram.com/wiki/Oxidizer">
  /// KSP Wiki: Oxidizer</seealso>
  public const string Oxidizer = "Oxidizer";

  /// <summary>Intake air resource name.</summary>
  /// <seealso href="http://wiki.kerbalspaceprogram.com/wiki/Intake_air">
  /// KSP Wiki: Intake air</seealso>
  public const string IntakeAir = "IntakeAir";

  /// <summary>Solid fuel resource name.</summary>
  /// <seealso href="http://wiki.kerbalspaceprogram.com/wiki/Solid_fuel">
  /// KSP Wiki: Solid fuel</seealso>
  public const string SolidFuel = "SolidFuel";

  /// <summary>Monopropellant resource name.</summary>
  /// <seealso href="http://wiki.kerbalspaceprogram.com/wiki/Monopropellant">
  /// KSP Wiki: Monopropellant</seealso>
  public const string MonoPropellant = "MonoPropellant";

  /// <summary>EVA Propellant resource name.</summary>
  /// <remarks>It's the fuel that powers the EVA jetpack.</remarks>
  /// <seealso href="http://wiki.kerbalspaceprogram.com/wiki/Extra-Vehicular_Activity">
  /// KSP Wiki: Extra-Vehicular Activity</seealso>
  public const string EvaPropellant = "EVA Propellant";

  /// <summary>Xenon gas resource name.</summary>
  /// <seealso href="http://wiki.kerbalspaceprogram.com/wiki/Xenon_gas">
  /// KSP Wiki: Xenon gas</seealso>
  public const string XenonGas = "XenonGas";

  /// <summary>Ore resource name.</summary>
  /// <seealso href="http://wiki.kerbalspaceprogram.com/wiki/Ore">
  /// KSP Wiki: Ore</seealso>
  public const string Ore = "Ore";

  /// <summary>Ablator resource name.</summary>
  /// <seealso href="http://wiki.kerbalspaceprogram.com/wiki/Ablator">
  /// KSP Wiki: Ablator</seealso>
  public const string Ablator = "Ablator";

  /// <summary>Returns an ID for the specified resource name.</summary>
  /// <remarks>This ID can be used in the methods that can only work with IDs.</remarks>
  /// <param name="resourceName">The name of the stock resource.</param>
  /// <returns>An ID of the resource.</returns>
  public static int GetId(string resourceName) {
    return resourceName.GetHashCode();
  }

  /// <summary>Returns a user friendly name of the resource.</summary>
  /// <param name="resourceName">The resource common name.</param>
  /// <returns>A user friendly string that identifies the resource.</returns>
  public static string GetResourceTitle(string resourceName) {
    return PartResourceLibrary.Instance.GetDefinition(resourceName).title;
  }

  /// <summary>Returns a user friendly name of the resource.</summary>
  /// <param name="resourceId">The resource ID.</param>
  /// <returns>A user friendly string that identifies the resource.</returns>
  public static string GetResourceTitle(int resourceId) {
    return PartResourceLibrary.Instance.GetDefinition(resourceId).title;
  }
}

}  // namepsace

namespace KSPDev.ModelUtils {

/// TODO: Merge with KSPDev.ModelUtils.Hierarchy
public static class Hierarchy2 {
  /// TODO: Replace default FindTransformByPath version.
  public static Transform FindTransformByPath(Transform parent, string path, Transform defValue = null) {
    var obj = Hierarchy.FindTransformByPath(parent, path);
    if (obj == null && defValue != null) {
      Debug.LogWarningFormat(
          "Cannot find model object: root={0}, path={1}. Using a fallback: {2}",
          DbgFormatter.TranformPath(parent), path, DbgFormatter.TranformPath(defValue));
      return defValue;
    }
    return obj;
  }

  /// <summary>Finds an object in the part's model.</summary>
  /// <param name="part">The part to look for the objects in.</param>
  /// <param name="path">The path to look for.</param>
  /// <param name="defValue">The default value to return when no object found.</param>
  /// <returns>The found object or <c>null</c>.</returns>
  public static Transform FindPartModelByPath(Part part, string path, Transform defValue = null) {
    return FindTransformByPath(Hierarchy.GetPartModelTransform(part), path, defValue: defValue);
  }
}
  
}  // namespace

namespace KSPDev.LogUtils {

/// TODO: Merge with KSPDev.LogUtils.DbgFormatter
public static class DbgFormatter2 {
  /// <summary>Formats a string providing a reference to the host part.</summary>
  /// <param name="host">The part that outputs into the log.</param>
  /// <param name="format">The format string.</param>
  /// <param name="args">The format arguments.</param>
  /// <returns>A logging string.</returns>
  public static string HostedLog(Part host, string format, params object[] args) {
    return string.Format("[Part:" + DbgFormatter.PartId(host) + "] " + format, args);
  }

  /// <summary>Formats a string providing a reference to the host part module.</summary>
  /// <param name="host">The part module that outputs into the log.</param>
  /// <param name="format">The format string.</param>
  /// <param name="args">The format arguments.</param>
  /// <returns>A logging string.</returns>
  public static string HostedLog(PartModule host, string format, params object[] args) {
    return string.Format(
        "[Part:" + DbgFormatter.PartId(host.part) + "#Module:" + host.moduleName + "] " + format,
        args);
  }

  /// <summary>Formats a string providing a reference to the host object.</summary>
  /// <param name="host">The object that outputs into the log.</param>
  /// <param name="format">The format string.</param>
  /// <param name="args">The format arguments.</param>
  /// <returns>A logging string.</returns>
  public static string HostedLog(Transform host, string format, params object[] args) {
    return string.Format("[Object:" + host.name + "] " + format, args);
  }

  /// <summary>Formats a string providing a reference to the host object.</summary>
  /// <param name="host">The object that outputs into the log.</param>
  /// <param name="format">The format string.</param>
  /// <param name="args">The format arguments.</param>
  /// <returns>A logging string.</returns>
  public static string HostedLog(GameObject host, string format, params object[] args) {
    return HostedLog(host.transform, format, args);
  }
}

}  // namespace

namespace KSPDev.Types {

/// <summary>Type to hold position and rotation of a transform. It can be serialized.</summary>
/// <remarks>
/// The value serializes into 6 numbers separated by a comma. They form two triplets:
/// <list type="bullet">
/// <item>The first triplet is a position: x, y, z.</item>
/// <item>
/// The second triplet is a Euler rotaion around each axis: x, y, z.
/// </item>
/// </list>
/// </remarks>
public sealed class PosAndRot2 : IPersistentField {
  /// <summary>Position of the transform.</summary>
  public Vector3 pos;
  
  /// <summary>Euler rotation.</summary>
  /// <remarks>
  /// The rotation angles are automatically adjusted to stay within the [0; 360) range.
  /// </remarks>
  public Vector3 euler {
    get { return _euler; }
    set {
      _euler = value;
      NormlizeAngles();
      rot = Quaternion.Euler(_euler.x, _euler.y, _euler.z);
    }
  }
  Vector3 _euler;

  /// <summary>Orientation of the transform.</summary>
  public Quaternion rot { get; private set; }

  /// <summary>Constructs a default instance.</summary>
  /// <remarks>Required for the persistence to work correctly.</remarks>
  /// <para>
  /// By default position is <c>(0,0,0)</c>, Euler angles are <c>(0,0,0)</c>, and the rotation is
  /// <c>Quaternion.identity</c>.  
  /// </para>
  /// <seealso href="https://docs.unity3d.com/ScriptReference/Vector3.html">
  /// Unity3D: Vector3</seealso>
  /// <seealso href="https://docs.unity3d.com/ScriptReference/Quaternion-identity.html">
  /// Unity3D: Quaternion</seealso>
  public PosAndRot2() {
  }

  /// <summary>Constructs a copy of an object of the same type.</summary>
  /// <param name="from">Source object.</param>
  public PosAndRot2(PosAndRot2 from) {
    pos = from.pos;
    euler = from.euler;
  }

  /// <summary>Constructs an object from a transform properties.</summary>
  /// <param name="pos">Position of the transform.</param>
  /// <param name="euler">Euler rotation of the transform.</param>
  /// <seealso href="https://docs.unity3d.com/ScriptReference/Vector3.html">
  /// Unity3D: Vector3</seealso>
  /// <seealso href="https://docs.unity3d.com/ScriptReference/Transform.html">
  /// Unity3D: Transform</seealso>
  public PosAndRot2(Vector3 pos, Vector3 euler) {
    this.pos = pos;
    this.euler = euler;
  }

  /// <summary>Gives a deep copy of the object.</summary>
  /// <returns>New object.</returns>
  public PosAndRot2 Clone() {
    return new PosAndRot2(this);
  }

  /// <inheritdoc/>
  public string SerializeToString() {
    return string.Format(
        "{0},{1},{2}, {3},{4},{5}", pos.x, pos.y, pos.z, euler.x, euler.y, euler.z);
  }

  /// <inheritdoc/>
  public void ParseFromString(string value) {
    var elements = value.Split(',');
    if (elements.Length != 6) {
      throw new ArgumentException(
          "PosAndRot type needs exactly 6 elements separated by a comma but found: " + value);
    }
    var args = elements.Select(float.Parse).ToArray();
    pos = new Vector3(args[0], args[1], args[2]);
    euler = new Vector3(args[3], args[4], args[5]);
  }

  /// <summary>Shows a human readable representation.</summary>
  /// <returns>String value.</returns>
  public override string ToString() {
    return string.Format(
        "[PosAndRot Pos={0}, Euler={1}]", DbgFormatter.Vector(pos), DbgFormatter.Vector(euler));
  }

  /// <summary>Creates a new instance from the provided string.</summary>
  /// <param name="strValue">The value to parse.</param>
  /// <param name="failOnError">
  /// If <c>true</c> then a parsing error will fail the creation. Otherwise, a default isntance will
  /// be returned.
  /// </param>
  /// <returns>An instance, intialized from the string.</returns>
  public static PosAndRot2 FromString(string strValue, bool failOnError = false) {
    var res = new PosAndRot2();
    try {
      res.ParseFromString(strValue);
    } catch (ArgumentException ex) {
      if (failOnError) {
        throw;
      }
      Debug.LogWarningFormat("Cannot parse PosAndRot, using default: {0}", ex.Message);
    }
    return res;
  }
  
  /// <summary>
  /// Ensures that all the angles are in the range of <c>[0; 360)</c>. 
  /// </summary>
  void NormlizeAngles() {
    while (_euler.x > 360) _euler.x -= 360;
    while (_euler.x < 0) _euler.x += 360;
    while (_euler.y > 360) _euler.y -= 360;
    while (_euler.y < 0) _euler.y += 360;
    while (_euler.z > 360) _euler.z -= 360;
    while (_euler.z < 0) _euler.z += 360;
  }
}

}  // namespace