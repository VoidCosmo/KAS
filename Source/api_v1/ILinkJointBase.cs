﻿// Kerbal Attachment System
// Mod's author: KospY (http://forum.kerbalspaceprogram.com/index.php?/profile/33868-kospy/)
// Module author: igor.zavoychinskiy@gmail.com
// License: Public Domain

using UnityEngine;

namespace KASAPIv1 {

/// <summary>Base interface for a physical joint module.</summary>
/// <remarks>
/// The <see cref="ILinkSource"/> implementations uses it to establish and drop the physical links.
/// </remarks>
public interface ILinkJointBase {
  /// <summary>Identifier of the joint on the part.</summary>
  /// <remarks>It's unique in scope of the part.</remarks>
  /// <value>An arbitary string that identifies this joint.</value>
  string cfgJointName { get; }

  /// <summary>Tells the current coupling mode.</summary>
  /// <remarks>
  /// Note, that if this mode set to <c>true</c>, it doesn't mean that the parts are coupled thru
  /// this specific joint module. Iy only means that the parts, linked via this joint, are
  /// guaranteed to be coupled, but the actual docking can be done by any other joint or part.
  /// </remarks>
  /// <value><c>true</c> if the vessels should couple on link (merge them into one).</value>
  /// <seealso cref="SetCoupleOnLinkMode"/>
  bool coupleOnLinkMode { get; }

  /// <summary>Tells the current link source.</summary>
  /// <value>The link's source or <c>null</c> if the link is not established.</value>
  ILinkSource linkSource { get; }

  /// <summary>Tells the current link target.</summary>
  /// <value>The link's target or <c>null</c> if the link is not established.</value>
  ILinkTarget linkTarget { get; }

  /// <summary>Sets up a physical joint between the source and target.</summary>
  /// <remarks>
  /// <para>
  /// This method can be called either to establish a new joint or to restore an existing link on
  /// load.
  /// </para>
  /// <para>
  /// This method will call the <see cref="CheckConstraints"/> method to ensure there are no errors.
  /// If there are some, then the link is not created and the errors are reported to the logs as
  /// errors. However, in case of the part loading, the check is not performed.
  /// </para>
  /// </remarks>
  /// <returns><c>true</c> if joint was successfully created or updated.</returns>
  /// <param name="source">The link's source. This part owns the joint module.</param>
  /// <param name="target">The link's target.</param>
  /// <seealso cref="CheckConstraints"/>
  /// <seealso cref="ILinkSource"/>
  /// <seealso cref="ILinkTarget"/>
  /// <seealso cref="DropJoint"/>
  /// <seealso cref="coupleOnLinkMode"/>
  bool CreateJoint(ILinkSource source, ILinkTarget target);

  /// <summary>Destroys a physical link between the source and the target.</summary>
  /// <remarks>
  /// This is a cleanup method. It must be safe to execute in any joint state, and should not throw
  /// any errors. E.g. it may get called when the part's state is incomplete.
  /// </remarks>
  /// <seealso cref="CreateJoint"/>
  void DropJoint();

  /// <summary>Changes the current parts couple mode.</summary>
  /// <remarks>
  /// If the link is established, then a re-linking event occurs regardless to the current state.
  /// I.e. the source and target are first get unlinked, and then immediately linked back in the new
  /// mode. If the link is not established, then the mode changes on the source without side
  /// effects.
  /// </remarks>
  /// <param name="isCoupleOnLink">The new settings of the mode.</param>
  /// <param name="actor">The actor who initiated the change.</param>
  /// <seealso cref="coupleOnLinkMode"/>
  void SetCoupleOnLinkMode(bool isCoupleOnLink, LinkActorType actor);

  /// <summary>Checks if the joint constraints allow the link to be established.</summary>
  /// <remarks>This method assumes that the <paramref name="targetTransform"/> is a possible
  /// <see cref="ILinkTarget.nodeTransform"/> on the target. For this reason the source's
  /// <see cref="ILinkSource.targetPhysicalAnchor"/> is applied towards it when doing the
  /// calculations.
  /// </remarks>
  /// <param name="source">The source that probes the link.</param>
  /// <param name="targetTransform">The target of the link to check the lmits against.</param>
  /// <returns>
  /// An empty array if the link can be created, or a list of user friendly errors otherwise.
  /// </returns>
  /// <seealso cref="ILinkSource"/>
  /// <include file="Unity3D_HelpIndex.xml" path="//item[@name='T:UnityEngine.Transform']/*"/>
  string[] CheckConstraints(ILinkSource source, Transform targetTransform);
}

}  // namespace
