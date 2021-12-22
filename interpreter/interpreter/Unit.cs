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

        public void Set(int val, int? index = null)
        {
            if (_struct == structure.Array)
                valueArray[(int) index] = val;
            else
                valueVariable = val;
        }

        public void Set(int[] val)
        {
            if (_struct == structure.Array)
                valueArray = val;
            else
                throw new ArgumentException("Переменной присваивается массив");
        }

        public int Get(int? index = null)
        {
            if (_struct == structure.Array)
                return valueArray[(int) index];
            
            return valueVariable;
        }

        public int[] Get()
        {
            if (_struct == structure.Array)
                return valueArray;
            else
                throw new ArgumentException("Переменной присваивается массив");
        }

        public static int operator +(Unit u1, Unit u2)
        {
            if (u1._struct == structure.Array || u2._struct == structure.Array)
                throw new ArgumentException("Сложение массивов");

            return u1.valueVariable + u2.valueVariable;
        }

        public static int operator +(Unit u, int i)
        {
            if (u._struct == structure.Array)
                throw new ArgumentException("Сложение массива и числа");

            return u.valueVariable + i;
        }

        public static int operator +(int i, Unit u)
        {
            if (u._struct == structure.Array)
                throw new ArgumentException("Сложение массива и числа");

            return u.valueVariable + i;
        }

        public static int operator -(Unit u1, Unit u2)
        {
            if (u1._struct == structure.Array || u2._struct == structure.Array)
                throw new ArgumentException("Вычитание массивов");

            return u1.valueVariable - u2.valueVariable;
        }

        public static int operator -(Unit u, int i)
        {
            if (u._struct == structure.Array)
                throw new ArgumentException("Вычитание массива и числа");

            return u.valueVariable - i;
        }

        public static int operator -(int i, Unit u)
        {
            if (u._struct == structure.Array)
                throw new ArgumentException("Вычитание числа и массива");

            return i - u.valueVariable;
        }

        public static int operator *(Unit u1, Unit u2)
        {
            if (u1._struct == structure.Array || u2._struct == structure.Array)
                throw new ArgumentException("Произведение массивов");

            return u1.valueVariable * u2.valueVariable;
        }

        public static int operator *(Unit u, int i)
        {
            if (u._struct == structure.Array)
                throw new ArgumentException("Произведение массива и числа");

            return u.valueVariable * i;
        }

        public static int operator *(int i, Unit u)
        {
            if (u._struct == structure.Array)
                throw new ArgumentException("Произведение числа и массива");

            return i * u.valueVariable;
        }

        public static int operator /(Unit u1, Unit u2)
        {
            if (u1._struct == structure.Array || u2._struct == structure.Array)
                throw new ArgumentException("Деление массивов");

            return u1.valueVariable / u2.valueVariable;
        }

        public static int operator /(Unit u, int i)
        {
            if (u._struct == structure.Array)
                throw new ArgumentException("Деление массива и числа");

            return u.valueVariable / i;
        }

        public static int operator /(int i, Unit u)
        {
            if (u._struct == structure.Array)
                throw new ArgumentException("Деление числа и массива");

            return i / u.valueVariable;
        }

        public static bool operator >=(Unit u1, Unit u2)
        {
            return u1.valueVariable >= u2.valueVariable;
        }

        public static bool operator <=(Unit u1, Unit u2)
        {
            return u1.valueVariable <= u2.valueVariable;
        }

        public static bool operator >=(int i, Unit u)
        {
            return i >= u.valueVariable;
        }

        public static bool operator <=(int i, Unit u)
        {
            return i <= u.valueVariable;
        }

        public static bool operator >=(Unit u, int i)
        {
            return u.valueVariable >= i;
        }

        public static bool operator <=(Unit u, int i)
        {
            return u.valueVariable <= i;
        }

        public enum structure
        {
            Array,
            Variable,
            Unknown
        }
    }
}
