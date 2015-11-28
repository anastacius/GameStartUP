namespace Gameplay.Unit.Attack
{
    public class WeaponDefinition
    {
        private int ammo;
        private float cooldown;
        private float range;
        private int damage;

        public WeaponDefinition(int ammo, float cooldown, float range, int damage)
        {
            this.ammo = ammo;
            this.cooldown = cooldown;
            this.range = range;
            this.damage = damage;
        }

        public int GetAmmo()
        {
            return this.ammo;
        }

        public float GetCooldown()
        {
            return this.cooldown;
        }

        public void SpentAmmo()
        {
            this.ammo--;
        }

        public float GetRange()
        {
            return this.range;
        }

        public int GetDamage()
        {
            return this.damage;
        }
    }
}