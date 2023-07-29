using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace HomeWorkAttackUnits
{
    class Program
    {
        static void Main(string[] args)
        {

            Unit PlayerOne = new UnitFraction(new LightFraction(), 20, 40, 100).Unit;
            Unit PlayerTwo = new UnitFraction(new DarkFraction(), 50, 60, 100).Unit;

            FightArea Area = new FightArea(PlayerOne, PlayerTwo);

            Area.StartFight();

        }
    }


    public class FightArea
    {
        private readonly Unit _playerOne;
        private readonly Unit _playerTwo;

        public FightArea(Unit playerOne, Unit playerTwo)
        {
            _playerOne = playerOne;
            _playerTwo = playerTwo;
        }

        public void StartFight()
        {
            Console.WriteLine("Оба игрока готовы к битве. Нажмите любую кнопку для продолжения...");
            Console.ReadKey();

            while(_playerOne.Health > 0 && _playerTwo.Health > 0)
            {
                Console.WriteLine("Номер игрока, который наносит урон?");
                int playerNumber = Convert.ToInt32(Console.ReadLine());

                if (playerNumber == 1)
                {

                    _playerTwo.SetDamage(_playerOne.BaseAttackCoef);

                    if (_playerTwo.Health < 0)
                    {
                        Console.WriteLine($"Победил первый игрок");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Игрок 1 наносит удар. Здоровье второго игрока: {_playerTwo.Health}");
                    }
                   

                    if (_playerTwo.Health > 0 && _playerTwo.Health < 40 && !_playerTwo.IsRageMode)
                    {
                        _playerTwo.SetRageMode();
                        Console.WriteLine($"Игрок 2 переходит в режим ульты");
                    }
                }
                else
                {
                   
                    _playerOne.SetDamage(_playerTwo.BaseAttackCoef);

                    if (_playerOne.Health < 0)
                    {
                        Console.WriteLine($"Победил второй игрок");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"Игрок 2 наносит удар. Здоровье первого игрока: {_playerOne.Health}");
                    }
                   

                    if (_playerOne.Health > 0 &&_playerOne.Health < 40 && !_playerOne.IsRageMode)
                    {
                        _playerOne.SetRageMode();
                        Console.WriteLine($"Игрок 1 переходит в режим ульты");
                    }

                }

            }
        }
    }

    public class UnitFraction
    {
        private readonly Unit _unit;

        public Unit Unit => _unit;

        public UnitFraction(IFraction fraction, float baseAttack, float baseDefense, int health)
        {
            float attackUp = ((fraction.BaffPercentage / 100) * baseAttack) + baseAttack;

            _unit = new Unit(attackUp, baseDefense, health);
        }
    }
    public class Unit
    {
        
        private float _baseAttackCoef;
        private float _baseDefenseCoef;
        private float _health;
        private bool _isRageMode;

        public float Health => _health;
        public float BaseAttackCoef => _baseAttackCoef;
        public float BaseDefenseCoef => _baseDefenseCoef;

        public bool IsRageMode => _isRageMode;

        public Unit(float baseAttack, float baseDefense, int health) { 
        
            _baseAttackCoef = baseAttack;
            _baseDefenseCoef = baseDefense;
            _health = health;
        }

        public void SetDamage(float attackCoef)
        {
            _health -= (100 - _baseDefenseCoef) / 100 * attackCoef;

        }

        public void SetRageMode()
        {
            if (!_isRageMode)
            {
                _baseAttackCoef *= 2;
                _baseDefenseCoef -= _baseDefenseCoef * 0.8f;
                _isRageMode = true;
            }
           
        }
    }

    

    public interface IFraction
    {
        int BaffPercentage { get; }
        int DebaffPercentage { get; }
    }


    class LightFraction: IFraction
    {
        public int BaffPercentage => 50;
        public int DebaffPercentage => 50;

    }

    class DarkFraction: IFraction
    {
        public int BaffPercentage => 50;
        public int DebaffPercentage => 50;

    }

    class NeutralFraction: IFraction
    {
        public int BaffPercentage => 0;
        public int DebaffPercentage => 0;
    }

    
}
