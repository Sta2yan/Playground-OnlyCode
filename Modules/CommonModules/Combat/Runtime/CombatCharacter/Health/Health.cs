using System;

namespace Agava.Combat
{
    internal class Health
    {
        private readonly int _maxHealth;
        private int _currentHealth;
        
        public Health(int maxHealth)
        {
            _maxHealth = maxHealth;
            _currentHealth = maxHealth;
        }

        public bool Alive => _currentHealth > 0;
        public int CurrentHealth => _currentHealth;

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage));
            
            _currentHealth -= damage;
        }
        
        public void Visualize(IHealthView view)
        {
            view.Render(_maxHealth, _currentHealth);
        }
    }
}
