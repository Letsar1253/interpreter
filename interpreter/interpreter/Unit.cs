using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interpreter
{
    class Unit
    {
        private structure _struct;
        private int valueVariable;
        private int[] valueArray;
        private List<int> indexes = new();
        
        public string name;

        public Unit(string name)
        {
            _struct = structure.Unknown;
            this.name = name;
        }

        public structure GetStruct()
        {
            return _struct;
        }

        public void Init(structure structure, int? len = null)
        {
            _struct = structure;
            if (_struct == structure.Array)
                valueArray = new int[(int)len];
        }

        public void Change()
        {
            if (_struct == structure.Variable)
                valueVariable *= -1;
            else if (_struct == structure.Array && indexes.Any())
            {
                valueArray[indexes.Last()] *= -1;
                indexes.RemoveAt(indexes.Count-1);
            }
            else
            {
                throw new ArgumentException("Ошибка смены знака");
            }
        }

        public void SetCurrentIndex(int index)
        {
            if (_struct == structure.Variable)
                throw new ArgumentException("Индекс нельзя присваивать переменной");

            if (index <0 || index >= valueArray.Length)
                throw new ArgumentException("Индекс нельзя присваивать переменной");

            indexes.Add(index);
        }

        public void SetCurrentIndex(Unit unit)
        {
            if (_struct == structure.Variable)
                throw new ArgumentException("Индекс нельзя присваивать переменной");

            if (unit.Get() < 0 || unit.Get() >= valueArray.Length)
                throw new ArgumentException("Индекс нельзя присваивать переменной");

            indexes.Add(unit.Get());
        }

        public void Set(int val)
        {
            if (_struct == structure.Array && indexes.Any())
            {
                valueArray[indexes.Last()] = val;
                indexes.RemoveAt(indexes.Count-1);
            }
            else
                valueVariable = val;
        }

        public void Set(Unit unit)
        {
            if (_struct == structure.Variable && unit._struct == structure.Variable)
                valueVariable = unit.valueVariable;
            else if (_struct == structure.Variable && unit._struct == structure.Array
                                                   && unit.indexes.Any())
            {
                valueVariable = unit.valueArray[unit.indexes.Last()];
                unit.indexes.RemoveAt(unit.indexes.Count-1);
            }
            else if (_struct == structure.Array && indexes.Any()
                                                && unit._struct == structure.Variable)
            {
                valueArray[indexes.Last()]= unit.valueVariable;
                indexes.RemoveAt(indexes.Count - 1);
            }
            else if (_struct == structure.Array && indexes.Any()
                                                && unit._struct == structure.Array
                                                && unit.indexes.Any())
            {
                ref int a = ref valueArray[indexes.Last()];
                indexes.RemoveAt(indexes.Count - 1);
                ref int b = ref unit.valueArray[unit.indexes.Last()];
                unit.indexes.RemoveAt(unit.indexes.Count - 1);
                b = a;
            }
            else if (_struct == structure.Array && !indexes.Any()
                                                && unit._struct == structure.Array
                                                && !unit.indexes.Any())
                valueArray = unit.valueArray;
            else
            {
                throw new ArgumentException("Ошибка присвоения массива и элемента массива");
            }
        }

        public int Get()
        {
            if (_struct == structure.Array && indexes.Any())
            {
                var val = valueArray[indexes.Last()];
                indexes.RemoveAt(indexes.Count-1);
                return val;
            }

            
            return valueVariable;
        }

        public static int operator +(Unit u1, Unit u2)
        {
            int val1, val2;
            if (u1._struct == structure.Variable && u2._struct == structure.Variable)
            {
                val1 = u1.valueVariable;
                val2 = u2.valueVariable;
            }
            else if (u1._struct == structure.Variable && u2._struct == structure.Array
                                                      && u2.indexes.Any())
            {
                val1 = u1.valueVariable;
                val2 = u2.valueArray[u2.indexes.Last()];
                u2.indexes.RemoveAt(u2.indexes.Count-1);
            }
            else if (u1._struct == structure.Array && u1.indexes.Any()
                                                   && u2._struct == structure.Variable)
            {
                val1 = u1.valueArray[u1.indexes.Last()];
                val2 = u2.valueVariable;
                u1.indexes.RemoveAt(u1.indexes.Count - 1);
            }
            else if (u1._struct == structure.Array && u1.indexes.Any()
                                                   && u2._struct == structure.Array
                                                   && u2.indexes.Any())
            {
                val2 = u1.valueArray[u1.indexes.Last()];
                u1.indexes.RemoveAt(u1.indexes.Count-1);
                val1 = u2.valueArray[u2.indexes.Last()];
                u2.indexes.RemoveAt(u2.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return val1 + val2;
        }

        public static int operator +(Unit u, int i)
        {
            int val;
            if (u._struct == structure.Variable)
                val = u.valueVariable;
            else if (u._struct == structure.Array && u.indexes.Any())
            {
                val = u.valueArray[u.indexes.Last()];
                u.indexes.RemoveAt(u.indexes.Count-1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return val + i;
        }

        public static int operator +(int i, Unit u)
        {
            int val;
            if (u._struct == structure.Variable)
                val = u.valueVariable;
            else if (u._struct == structure.Array && u.indexes.Any())
            {
                val = u.valueArray[u.indexes.Last()];
                u.indexes.RemoveAt(u.indexes.Count-1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return i + val;
        }

        public static int operator -(Unit u1, Unit u2)
        {
            int val1, val2;
            if (u1._struct == structure.Variable && u2._struct == structure.Variable)
            {
                val1 = u1.valueVariable;
                val2 = u2.valueVariable;
            }
            else if (u1._struct == structure.Variable && u2._struct == structure.Array
                                                      && u2.indexes.Any())
            {
                val1 = u1.valueVariable;
                val2 = u2.valueArray[u2.indexes.Last()];
                u2.indexes.RemoveAt(u2.indexes.Count - 1);
            }
            else if (u1._struct == structure.Array && u1.indexes.Any()
                                                   && u2._struct == structure.Variable)
            {
                val1 = u1.valueArray[u1.indexes.Last()];
                val2 = u2.valueVariable;
                u1.indexes.RemoveAt(u1.indexes.Count - 1);
            }
            else if (u1._struct == structure.Array && u1.indexes.Any()
                                                   && u2._struct == structure.Array
                                                   && u2.indexes.Any())
            {
                val2 = u1.valueArray[u1.indexes.Last()];
                u1.indexes.RemoveAt(u1.indexes.Count - 1);
                val1 = u2.valueArray[u2.indexes.Last()];
                u2.indexes.RemoveAt(u2.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return val1 - val2;
        }

        public static int operator -(Unit u, int i)
        {
            int val;
            if (u._struct == structure.Variable)
                val = u.valueVariable;
            else if (u._struct == structure.Array && u.indexes.Any())
            {
                val = u.valueArray[u.indexes.Last()];
                u.indexes.RemoveAt(u.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return val - i;
        }

        public static int operator -(int i, Unit u)
        {
            int val;
            if (u._struct == structure.Variable)
                val = u.valueVariable;
            else if (u._struct == structure.Array && u.indexes.Any())
            {
                val = u.valueArray[u.indexes.Last()];
                u.indexes.RemoveAt(u.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return i - val;
        }

        public static int operator *(Unit u1, Unit u2)
        {
            int val1, val2;
            if (u1._struct == structure.Variable && u2._struct == structure.Variable)
            {
                val1 = u1.valueVariable;
                val2 = u2.valueVariable;
            }
            else if (u1._struct == structure.Variable && u2._struct == structure.Array
                                                      && u2.indexes.Any())
            {
                val1 = u1.valueVariable;
                val2 = u2.valueArray[u2.indexes.Last()];
                u2.indexes.RemoveAt(u2.indexes.Count - 1);
            }
            else if (u1._struct == structure.Array && u1.indexes.Any()
                                                   && u2._struct == structure.Variable)
            {
                val1 = u1.valueArray[u1.indexes.Last()];
                val2 = u2.valueVariable;
                u1.indexes.RemoveAt(u1.indexes.Count - 1);
            }
            else if (u1._struct == structure.Array && u1.indexes.Any()
                                                   && u2._struct == structure.Array
                                                   && u2.indexes.Any())
            {
                val2 = u1.valueArray[u1.indexes.Last()];
                u1.indexes.RemoveAt(u1.indexes.Count - 1);
                val1 = u2.valueArray[u2.indexes.Last()];
                u2.indexes.RemoveAt(u2.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return val1 * val2;
        }

        public static int operator *(Unit u, int i)
        {
            int val;
            if (u._struct == structure.Variable)
                val = u.valueVariable;
            else if (u._struct == structure.Array && u.indexes.Any())
            {
                val = u.valueArray[u.indexes.Last()];
                u.indexes.RemoveAt(u.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return val * i;
        }

        public static int operator *(int i, Unit u)
        {
            int val;
            if (u._struct == structure.Variable)
                val = u.valueVariable;
            else if (u._struct == structure.Array && u.indexes.Any())
            {
                val = u.valueArray[u.indexes.Last()];
                u.indexes.RemoveAt(u.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return i * val;
        }

        public static int operator /(Unit u1, Unit u2)
        {
            int val1, val2;
            if (u1._struct == structure.Variable && u2._struct == structure.Variable)
            {
                val1 = u1.valueVariable;
                val2 = u2.valueVariable;
            }
            else if (u1._struct == structure.Variable && u2._struct == structure.Array
                                                      && u2.indexes.Any())
            {
                val1 = u1.valueVariable;
                val2 = u2.valueArray[u2.indexes.Last()];
                u2.indexes.RemoveAt(u2.indexes.Count - 1);
            }
            else if (u1._struct == structure.Array && u1.indexes.Any()
                                                   && u2._struct == structure.Variable)
            {
                val1 = u1.valueArray[u1.indexes.Last()];
                val2 = u2.valueVariable;
                u1.indexes.RemoveAt(u1.indexes.Count - 1);
            }
            else if (u1._struct == structure.Array && u1.indexes.Any()
                                                   && u2._struct == structure.Array
                                                   && u2.indexes.Any())
            {
                val2 = u1.valueArray[u1.indexes.Last()];
                u1.indexes.RemoveAt(u1.indexes.Count - 1);
                val1 = u2.valueArray[u2.indexes.Last()];
                u2.indexes.RemoveAt(u2.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return val1 / val2;
        }

        public static int operator /(Unit u, int i)
        {
            int val;
            if (u._struct == structure.Variable)
                val = u.valueVariable;
            else if (u._struct == structure.Array && u.indexes.Any())
            {
                val = u.valueArray[u.indexes.Last()];
                u.indexes.RemoveAt(u.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return val / i;
        }

        public static int operator /(int i, Unit u)
        {
            int val;
            if (u._struct == structure.Variable)
                val = u.valueVariable;
            else if (u._struct == structure.Array && u.indexes.Any())
            {
                val = u.valueArray[u.indexes.Last()];
                u.indexes.RemoveAt(u.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return i / val;
        }

        public static bool operator >(Unit u1, Unit u2)
        {
            int val1, val2;
            if (u1._struct == structure.Variable && u2._struct == structure.Variable)
            {
                val1 = u1.valueVariable;
                val2 = u2.valueVariable;
            }
            else if (u1._struct == structure.Variable && u2._struct == structure.Array
                                                      && u2.indexes.Any())
            {
                val1 = u1.valueVariable;
                val2 = u2.valueArray[u2.indexes.Last()];
                u2.indexes.RemoveAt(u2.indexes.Count - 1);
            }
            else if (u1._struct == structure.Array && u1.indexes.Any()
                                                   && u2._struct == structure.Variable)
            {
                val1 = u1.valueArray[u1.indexes.Last()];
                val2 = u2.valueVariable;
                u1.indexes.RemoveAt(u1.indexes.Count - 1);
            }
            else if (u1._struct == structure.Array && u1.indexes.Any()
                                                   && u2._struct == structure.Array
                                                   && u2.indexes.Any())
            {
                val2 = u1.valueArray[u1.indexes.Last()];
                u1.indexes.RemoveAt(u1.indexes.Count - 1);
                val1 = u2.valueArray[u2.indexes.Last()];
                u2.indexes.RemoveAt(u2.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return val1 > val2;
        }

        public static bool operator >(Unit u, int i)
        {
            int val;
            if (u._struct == structure.Variable)
                val = u.valueVariable;
            else if (u._struct == structure.Array && u.indexes.Any())
            {
                val = u.valueArray[u.indexes.Last()];
                u.indexes.RemoveAt(u.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return val > i;
        }

        public static bool operator >(int i, Unit u)
        {
            int val;
            if (u._struct == structure.Variable)
                val = u.valueVariable;
            else if (u._struct == structure.Array && u.indexes.Any())
            {
                val = u.valueArray[u.indexes.Last()];
                u.indexes.RemoveAt(u.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return i > val;
        }

        public static bool operator <(Unit u1, Unit u2)
        {
            int val1, val2;
            if (u1._struct == structure.Variable && u2._struct == structure.Variable)
            {
                val1 = u1.valueVariable;
                val2 = u2.valueVariable;
            }
            else if (u1._struct == structure.Variable && u2._struct == structure.Array
                                                      && u2.indexes.Any())
            {
                val1 = u1.valueVariable;
                val2 = u2.valueArray[u2.indexes.Last()];
                u2.indexes.RemoveAt(u2.indexes.Count - 1);
            }
            else if (u1._struct == structure.Array && u1.indexes.Any()
                                                   && u2._struct == structure.Variable)
            {
                val1 = u1.valueArray[u1.indexes.Last()];
                val2 = u2.valueVariable;
                u1.indexes.RemoveAt(u1.indexes.Count - 1);
            }
            else if (u1._struct == structure.Array && u1.indexes.Any()
                                                   && u2._struct == structure.Array
                                                   && u2.indexes.Any())
            {
                val2 = u1.valueArray[u1.indexes.Last()];
                u1.indexes.RemoveAt(u1.indexes.Count - 1);
                val1 = u2.valueArray[u2.indexes.Last()];
                u2.indexes.RemoveAt(u2.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return val1 < val2;
        }

        public static bool operator <(Unit u, int i)
        {
            int val;
            if (u._struct == structure.Variable)
                val = u.valueVariable;
            else if (u._struct == structure.Array && u.indexes.Any())
            {
                val = u.valueArray[u.indexes.Last()];
                u.indexes.RemoveAt(u.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return val < i;
        }

        public static bool operator <(int i, Unit u)
        {
            int val;
            if (u._struct == structure.Variable)
                val = u.valueVariable;
            else if (u._struct == structure.Array && u.indexes.Any())
            {
                val = u.valueArray[u.indexes.Last()];
                u.indexes.RemoveAt(u.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return i < val;
        }

        public static bool operator >=(Unit u1, Unit u2)
        {
            int val1, val2;
            if (u1._struct == structure.Variable && u2._struct == structure.Variable)
            {
                val1 = u1.valueVariable;
                val2 = u2.valueVariable;
            }
            else if (u1._struct == structure.Variable && u2._struct == structure.Array
                                                      && u2.indexes.Any())
            {
                val1 = u1.valueVariable;
                val2 = u2.valueArray[u2.indexes.Last()];
                u2.indexes.RemoveAt(u2.indexes.Count - 1);
            }
            else if (u1._struct == structure.Array && u1.indexes.Any()
                                                   && u2._struct == structure.Variable)
            {
                val1 = u1.valueArray[u1.indexes.Last()];
                val2 = u2.valueVariable;
                u1.indexes.RemoveAt(u1.indexes.Count - 1);
            }
            else if (u1._struct == structure.Array && u1.indexes.Any()
                                                   && u2._struct == structure.Array
                                                   && u2.indexes.Any())
            {
                val2 = u1.valueArray[u1.indexes.Last()];
                u1.indexes.RemoveAt(u1.indexes.Count - 1);
                val1 = u2.valueArray[u2.indexes.Last()];
                u2.indexes.RemoveAt(u2.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return val1 >= val2;
        }

        public static bool operator >=(Unit u, int i)
        {
            int val;
            if (u._struct == structure.Variable)
                val = u.valueVariable;
            else if (u._struct == structure.Array && u.indexes.Any())
            {
                val = u.valueArray[u.indexes.Last()];
                u.indexes.RemoveAt(u.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return val >= i;
        }

        public static bool operator >=(int i, Unit u)
        {
            int val;
            if (u._struct == structure.Variable)
                val = u.valueVariable;
            else if (u._struct == structure.Array && u.indexes.Any())
            {
                val = u.valueArray[u.indexes.Last()];
                u.indexes.RemoveAt(u.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return i >= val;
        }

        public static bool operator <=(Unit u1, Unit u2)
        {
            int val1, val2;
            if (u1._struct == structure.Variable && u2._struct == structure.Variable)
            {
                val1 = u1.valueVariable;
                val2 = u2.valueVariable;
            }
            else if (u1._struct == structure.Variable && u2._struct == structure.Array
                                                      && u2.indexes.Any())
            {
                val1 = u1.valueVariable;
                val2 = u2.valueArray[u2.indexes.Last()];
                u2.indexes.RemoveAt(u2.indexes.Count - 1);
            }
            else if (u1._struct == structure.Array && u1.indexes.Any()
                                                   && u2._struct == structure.Variable)
            {
                val1 = u1.valueArray[u1.indexes.Last()];
                val2 = u2.valueVariable;
                u1.indexes.RemoveAt(u1.indexes.Count - 1);
            }
            else if (u1._struct == structure.Array && u1.indexes.Any()
                                                   && u2._struct == structure.Array
                                                   && u2.indexes.Any())
            {
                val2 = u1.valueArray[u1.indexes.Last()];
                u1.indexes.RemoveAt(u1.indexes.Count - 1);
                val1 = u2.valueArray[u2.indexes.Last()];
                u2.indexes.RemoveAt(u2.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return val1 <= val2;
        }

        public static bool operator <=(Unit u, int i)
        {
            int val;
            if (u._struct == structure.Variable)
                val = u.valueVariable;
            else if (u._struct == structure.Array && u.indexes.Any())
            {
                val = u.valueArray[u.indexes.Last()];
                u.indexes.RemoveAt(u.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return val <= i;
        }

        public static bool operator <=(int i, Unit u)
        {
            int val;
            if (u._struct == structure.Variable)
                val = u.valueVariable;
            else if (u._struct == structure.Array && u.indexes.Any())
            {
                val = u.valueArray[u.indexes.Last()];
                u.indexes.RemoveAt(u.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return i <= val;
        }

        public static bool operator ==(Unit u1, Unit u2)
        {
            int val1, val2;
            if (u1._struct == structure.Variable && u2._struct == structure.Variable)
            {
                val1 = u1.valueVariable;
                val2 = u2.valueVariable;
            }
            else if (u1._struct == structure.Variable && u2._struct == structure.Array
                                                      && u2.indexes.Any())
            {
                val1 = u1.valueVariable;
                val2 = u2.valueArray[u2.indexes.Last()];
                u2.indexes.RemoveAt(u2.indexes.Count - 1);
            }
            else if (u1._struct == structure.Array && u1.indexes.Any()
                                                   && u2._struct == structure.Variable)
            {
                val1 = u1.valueArray[u1.indexes.Last()];
                val2 = u2.valueVariable;
                u1.indexes.RemoveAt(u1.indexes.Count - 1);
            }
            else if (u1._struct == structure.Array && u1.indexes.Any()
                                                   && u2._struct == structure.Array
                                                   && u2.indexes.Any())
            {
                val2 = u1.valueArray[u1.indexes.Last()];
                u1.indexes.RemoveAt(u1.indexes.Count - 1);
                val1 = u2.valueArray[u2.indexes.Last()];
                u2.indexes.RemoveAt(u2.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return val1 == val2;
        }

        public static bool operator ==(Unit u, int i)
        {
            int val;
            if (u._struct == structure.Variable)
                val = u.valueVariable;
            else if (u._struct == structure.Array && u.indexes.Any())
            {
                val = u.valueArray[u.indexes.Last()];
                u.indexes.RemoveAt(u.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return val == i;
        }

        public static bool operator ==(int i, Unit u)
        {
            int val;
            if (u._struct == structure.Variable)
                val = u.valueVariable;
            else if (u._struct == structure.Array && u.indexes.Any())
            {
                val = u.valueArray[u.indexes.Last()];
                u.indexes.RemoveAt(u.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return i == val;
        }

        public static bool operator !=(Unit u1, Unit u2)
        {
            int val1, val2;
            if (u1._struct == structure.Variable && u2._struct == structure.Variable)
            {
                val1 = u1.valueVariable;
                val2 = u2.valueVariable;
            }
            else if (u1._struct == structure.Variable && u2._struct == structure.Array
                                                      && u2.indexes.Any())
            {
                val1 = u1.valueVariable;
                val2 = u2.valueArray[u2.indexes.Last()];
                u2.indexes.RemoveAt(u2.indexes.Count - 1);
            }
            else if (u1._struct == structure.Array && u1.indexes.Any()
                                                   && u2._struct == structure.Variable)
            {
                val1 = u1.valueArray[u1.indexes.Last()];
                val2 = u2.valueVariable;
                u1.indexes.RemoveAt(u1.indexes.Count - 1);
            }
            else if (u1._struct == structure.Array && u1.indexes.Any()
                                                   && u2._struct == structure.Array
                                                   && u2.indexes.Any())
            {
                val2 = u1.valueArray[u1.indexes.Last()];
                u1.indexes.RemoveAt(u1.indexes.Count - 1);
                val1 = u2.valueArray[u2.indexes.Last()];
                u2.indexes.RemoveAt(u2.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return val1 != val2;
        }

        public static bool operator !=(Unit u, int i)
        {
            int val;
            if (u._struct == structure.Variable)
                val = u.valueVariable;
            else if (u._struct == structure.Array && u.indexes.Any())
            {
                val = u.valueArray[u.indexes.Last()];
                u.indexes.RemoveAt(u.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return val != i;
        }

        public static bool operator !=(int i, Unit u)
        {
            int val;
            if (u._struct == structure.Variable)
                val = u.valueVariable;
            else if (u._struct == structure.Array && u.indexes.Any())
            {
                val = u.valueArray[u.indexes.Last()];
                u.indexes.RemoveAt(u.indexes.Count - 1);
            }
            else
            {
                throw new ArgumentException("Ошибка сложения");
            }

            return i != val;
        }


        public int Abs()
        {
            if (_struct == structure.Array && indexes.Any())
            {
                var a = valueArray[indexes.Last()];
                indexes.RemoveAt(indexes.Count - 1);
                return Math.Abs(a);
            }
            else if (_struct == structure.Array && !indexes.Any())
                throw new ArgumentException("Модуль от массива");

            return Math.Abs(valueVariable);
        }

        public int Sqrt()
        {
            if (_struct == structure.Array && indexes.Any())
            {
                var a = (double) valueArray[indexes.Last()];
                indexes.RemoveAt(indexes.Count - 1);
                return (int)Math.Sqrt(a);
            }
            else if (_struct == structure.Array && !indexes.Any())
                throw new ArgumentException("Корень из массива");

            return (int)Math.Sqrt((double)valueVariable);
        }

        public enum structure
        {
            Array,
            Variable,
            Unknown
        }
    }
}
