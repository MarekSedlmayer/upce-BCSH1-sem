using UnityEngine;

public interface IWeapon
{
    /// <summary>
    /// Attempts to fire the weapon. Fails silently if the weapon is not ready.
    /// </summary>
    /// <param name="position">Shot origin.</param>
    /// <param name="rotation">Shot direction.</param>
    public void TryShoot(Vector3 position, Quaternion rotation);

    /// <summary>
    /// Forces the weapon to fire, ignoring all time delays.
    /// </summary>
    /// <param name="position">Shot origin.</param>
    /// <param name="rotation">Shot direction.</param>
    public void Shoot(Vector3 position, Quaternion rotation);
}
