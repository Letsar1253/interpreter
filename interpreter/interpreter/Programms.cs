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
    class Programms
    {
		int b;
		string lexem_buffer;
		int lexem_value;
		private List<string> list = new List<string>();
		public void Programm1(char ch)//Начало лексемы
        {
			lexem_buffer = ch;
        }
		public void Programm2(char ch)//Продолжение лексемы
        {
			lexem_buffer += ch;
        }
		public void Programm3()//Распознано имя
        {
			list.add(lexem_buffer);
        }
		public void Programm4(char ch)//Начало константы
        {
			lexem_value = ch - '0';
        }
		public void Programm5(char ch)//Продолжение константы
        {
			lexem_value = lexem_value * 10 + ch - '0';
        }
		public void Programm6()//Распознана константа
        {
			list.add(lexem_value);
        }
		public void Programm7()//Распознано = с шагом назад
        {
			list.add('=');
        }
		public void Programm8()//Распознан +
        {
			list.add('+');
        }
		public void Programm9()//Распознан -
        {
			list.add('-');
        }
		public void Programm10()//Распознано *
        {
			list.add('*');
        }
		public void Programm11()//Распознано /
        {
			list.add('/');
        }
		public void Programm12()//Распознано [
        {
			list.add('[');
        }
		public void Programm13()//Распознано ]
        {
			list.add(']');
        }
		public void Programm14()//Распознано (
        {
			list.add('(');
        }
		public void Programm15()//Распознано )
        {
			list.add(')');
        }
		public void Programm16()//Распознано <=
        {
			list.add('<=');
        }
		public void Programm17()//Распознано >=
        {
			list.add('>=');
        }
		public void Programm18()//Распознано ==
        {
			list.add('==');
        }
		public void Programm19()//Распознано !=
        {
			list.add('!=');
        }
		public void Programm20()//Распознано ;
        {
			list.add(';');
        }
		public void Programm21()//Пропуск символа
        {

        }
		public void Programm22()//Распознано > с шагом назад
        {
            list.add('>');
        }
		public void Programm23()//Распознано < с шагом назад
        {
            list.add('<');
        }
		public void Programm24()//Переход на новую строку
        {
            list.add('\n');
        }
		public void Programm25()//Конец файла
        {
            list.add('\0');
        }
		public void Programm26()//Распознана лексема c шагом на единицу назад
        {
            list.add(lexem_buffer);
        }
		public void Programm27()//Распознана константа с шагом на единицу назад
        {
            list.add(lexem_value);
        }
		public void Programm28()//Сообщение об ошибке с выходом из цикла 
        {
            throw error;
        }
		public void Programm29()//Распознано {
        {
            list.add('{')
        }
		public void Programm30()//Распознано }
        {
            list.add('}')
        }
    }
}