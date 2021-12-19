using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interpreter
{
    /// <summary>
    /// Класс программ, которые будут выполнять программы таблицы переходов.
    /// </summary>
    class Programs
    {
        string buffer;
		private List<string> list = new ();

        #region Программы

        /// <summary>
        /// Начало лексемы
        /// </summary>
        /// <param name="ch"></param>
        public void Program1(char ch)
        {
            buffer = ch.ToString();
        }

        /// <summary>
        /// Продолжение лексемы
        /// </summary>
        /// <param name="ch"></param>
        public void Program2(char ch)
        {
            buffer += ch;
        }

        /// <summary>
        /// Распознано имя
        /// </summary>
        public void Program3()
        {
            list.Add(buffer);
        }

        /// <summary>
        /// Начало константы
        /// </summary>
        /// <param name="ch"></param>
        public void Program4(char ch)
        {
            buffer = ch.ToString();
        }

        /// <summary>
        /// Продолжение константы
        /// </summary>
        /// <param name="ch"></param>
        public void Program5(char ch)
        {
            buffer += ch;
        }

        /// <summary>
        /// Распознана константа
        /// </summary>
        public void Program6()
        {
            list.Add(buffer);
        }

        /// <summary>
        /// Распознано = с шагом назад
        /// </summary>
        public void Program7(char ch)
        {
            list.Add("=");
            NeedBack.Invoke(ch);
        }

        /// <summary>
        /// Распознан +
        /// </summary>
        public void Program8()
        {
            list.Add("+");
        }

        /// <summary>
        /// Распознан -
        /// </summary>
        public void Program9()
        {
            list.Add("-");
        }

        /// <summary>
        /// Распознано *
        /// </summary>
        public void Program10()
        {
            list.Add("*");
        }

        /// <summary>
        /// Распознано /
        /// </summary>
        public void Program11()
        {
            list.Add("/");
        }

        /// <summary>
        /// Распознано [
        /// </summary>
        public void Program12()
        {
            list.Add("[");
        }

        /// <summary>
        /// Распознано ]
        /// </summary>
        public void Program13()
        {
            list.Add("]");
        }

        /// <summary>
        /// Распознано (
        /// </summary>
        public void Program14()
        {
            list.Add("(");
        }

        /// <summary>
        /// Распознано )
        /// </summary>
        public void Program15()
        {
            list.Add(")");
        }

        /// <summary>
        /// Распознано <=
        /// </summary>
        public void Program16()
        {
            list.Add("<=");
        }

        /// <summary>
        /// Распознано >=
        /// </summary>
        public void Program17()
        {
            list.Add(">=");
        }

        /// <summary>
        /// Распознано ==
        /// </summary>
        public void Program18()
        {
            list.Add("==");
        }

        /// <summary>
        /// Распознано !=
        /// </summary>
        public void Program19()
        {
            list.Add("!=");
        }

        /// <summary>
        /// Распознано ;
        /// </summary>
        public void Program20()
        {
            list.Add(";");
        }

        /// <summary>
        /// Пропуск символа
        /// </summary>
        public void Program21()
        {

        }

        /// <summary>
        /// Распознано > с шагом назад
        /// </summary>
        public void Program22(char ch)
        {
            list.Add(">");
            NeedBack.Invoke(ch);
        }

        /// <summary>
        /// Распознано '<' с шагом назад
        /// </summary>
        public void Program23(char ch)
        {
            list.Add("<");
            NeedBack.Invoke(ch);
        }

        /// <summary>
        /// Переход на новую строку
        /// </summary>
        public void Program24()
        {
            list.Add("\n");
        }

        /// <summary>
        /// Конец файла
        /// </summary>
        public void Program25()
        {
            list.Add("\0");
        }

        /// <summary>
        /// Распознана лексема c шагом на единицу назад
        /// </summary>
        public void Program26(char ch)
        {
            list.Add(buffer);
            NeedBack.Invoke(ch);
        }

        /// <summary>
        /// Распознана константа с шагом на единицу назад
        /// </summary>
        public void Program27(char ch)
        {
            list.Add(buffer);
            NeedBack.Invoke(ch);
        }

        /// <summary>
        /// Сообщение об ошибке с выходом из цикла 
        /// </summary>
        public void Program28()
        {
            throw new ArgumentException("Ошибка в таблице переходов");
        }

        /// <summary>
        /// Распознано {
        /// </summary>
        public void Program29()
        {
            list.Add("{");
        }

        /// <summary>
        /// Распознано }
        /// </summary>
        public void Program30()
        {
            list.Add("}");
        }

        /// <summary>
        /// Распознано ,
        /// </summary>
        public void Program31()
        {
            list.Add(",");
        }

        #endregion

        /// <summary>
        /// Получение списка 
        /// </summary>
        /// <returns></returns>
        public List<string> GetList()
        {
            return list;
        }

        /// <summary>
        /// Событие для шага назад
        /// </summary>
        public event Action<char> NeedBack;
    }
}